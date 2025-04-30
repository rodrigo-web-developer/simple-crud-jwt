using System.ComponentModel.DataAnnotations;

namespace StoreAPI.Models
{
    public class Cliente : IEntity
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(200)]
        public string Endereco { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Telefone { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
    }
}
