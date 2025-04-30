using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StoreAPI.Configurations;
using StoreAPI.DTOs;
using StoreAPI.Models;
using StoreAPI.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    private readonly IRepository<Usuario> _usuarioRepository;
    private readonly JwtSettings _jwtSettings;

    public AuthService(IRepository<Usuario> usuarioRepository, IOptions<JwtSettings> jwtSettings)
    {
        _usuarioRepository = usuarioRepository;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<string> AuthenticateAsync(string email, string senha)
    {
        var user = await _usuarioRepository.Query().Where(u => u.Email == email).FirstOrDefaultAsync();
        if (user == null) return null;

        if (!BCrypt.Net.BCrypt.Verify(senha, user.Senha))
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("partnerId", user.PartnerId.ToString()),
            new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "Partner")
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<OperationResult> RegisterAsync(RegisterUserDto request)
    {
        try
        {
            var existing = _usuarioRepository.Query().Where(u => u.Email == request.Email).FirstOrDefault();
            if (existing != null)
                return OperationResult.FailException(new Exception("Usuário já existe com este e-mail."));

            var newUser = new Usuario
            {
                Email = request.Email,
                Nome = request.Username,
                Senha = BCrypt.Net.BCrypt.HashPassword(request.Password),
                PartnerId = request.PartnerId,
                IsAdmin = request.IsAdmin
            };

            await _usuarioRepository.CreateAsync(newUser);
            await _usuarioRepository.SaveChangesAsync();

            return OperationResult.Ok(newUser);
        }
        catch (Exception ex)
        {
            return OperationResult.FailException(ex);
        }
    }
}
