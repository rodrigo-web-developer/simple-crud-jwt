using System.ComponentModel.DataAnnotations;

namespace StoreAPI.DTOs
{
    public class RegisterUserDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required, MaxLength(100)]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public long? PartnerId { get; set; }
    }
}
