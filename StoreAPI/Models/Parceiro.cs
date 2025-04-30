using System.ComponentModel.DataAnnotations;

namespace StoreAPI.Models
{
    public class Parceiro : IEntity
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
    }
}
