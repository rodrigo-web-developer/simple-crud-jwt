using Microsoft.EntityFrameworkCore;
using StoreAPI.Models;

namespace StoreAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Parceiro> Parceiros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Produto>()
                .HasOne<Parceiro>() // relação sem navigation property
                .WithMany()
                .HasForeignKey(p => p.PartnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Produto);

            modelBuilder.Entity<Usuario>()
                .HasOne<Parceiro>()
                .WithMany()
                .HasForeignKey(u => u.PartnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
