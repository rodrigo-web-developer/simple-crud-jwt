using Microsoft.EntityFrameworkCore;
using StoreAPI.Context;
using StoreAPI.Models;

public static class DbInitializer
{
    public static async Task SeedAsync(this AppDbContext context)
    {
        if (await context.Parceiros.AnyAsync()) return; // já populado

        var parceiroA = new Parceiro { Nome = "Parceiro A" };
        var parceiroB = new Parceiro { Nome = "Parceiro B" };

        context.Parceiros.AddRange(parceiroA, parceiroB);
        await context.SaveChangesAsync();

        var produtosA = new List<Produto>
        {
            new Produto { Nome = "Produto A1", Preco = 10, Descricao = "Produto A1", PartnerId = parceiroA.Id },
            new Produto { Nome = "Produto A2", Preco = 20, Descricao = "Produto A2", PartnerId = parceiroA.Id },
            new Produto { Nome = "Produto A3", Preco = 30, Descricao = "Produto A3", PartnerId = parceiroA.Id }
        };

        var produtosB = new List<Produto>
        {
            new Produto { Nome = "Produto B1", Preco = 15, Descricao = "Produto B1", PartnerId = parceiroB.Id },
            new Produto { Nome = "Produto B2", Preco = 25, Descricao = "Produto B2", PartnerId = parceiroB.Id },
            new Produto { Nome = "Produto B3", Preco = 35, Descricao = "Produto B3", PartnerId = parceiroB.Id }
        };

        context.Produtos.AddRange(produtosA);
        context.Produtos.AddRange(produtosB);
        await context.SaveChangesAsync();

        var adminUser = new Usuario
        {
            Nome = "Usuário Ademir",
            Email = "admin@admin.com",
            Senha = BCrypt.Net.BCrypt.HashPassword("admin@123"),
            IsAdmin = true,
            PartnerId = parceiroA.Id
        };

        var userA = new Usuario
        {
            Nome = "Usuário Parceiro 1",
            Email = "partner1@teste.com",
            Senha = BCrypt.Net.BCrypt.HashPassword("partner1@password"),
            IsAdmin = false,
            PartnerId = parceiroA.Id
        };

        var userB = new Usuario
        {
            Nome = "Usuário Parceiro 2",
            Email = "partner2@teste.com",
            Senha = BCrypt.Net.BCrypt.HashPassword("partner2@password"),
            IsAdmin = false,
            PartnerId = parceiroB.Id
        };

        context.Usuarios.AddRange(adminUser, userA, userB);
        await context.SaveChangesAsync();
    }

}
