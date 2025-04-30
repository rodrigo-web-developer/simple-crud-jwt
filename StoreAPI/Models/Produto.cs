using System.ComponentModel.DataAnnotations;

namespace StoreAPI.Models
{
    public class Produto : IEntity
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [MaxLength(500)]
        public string Descricao { get; set; }

        [Required]
        public long PartnerId { get; set; }
        public Parceiro Parceiro { get; set; }
    }
}
