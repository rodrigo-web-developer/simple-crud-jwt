using System.ComponentModel.DataAnnotations;

namespace StoreAPI.Models
{
    public class Usuario : IEntity
    {
        public long Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Senha { get; set; }

        [Required]
        public string Nome { get; set; }

        public bool IsAdmin { get; set; }

        public long? PartnerId { get; set; }
        public Parceiro Parceiro { get; set; }
    }
}
