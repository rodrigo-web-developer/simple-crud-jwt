using StoreAPI.ValueObjects;

namespace StoreAPI.Models
{
    public class Pedido : IEntity
    {
        public long ProdutoId { get; set; }
        public long ClienteId { get; set; }
        public int Quantidade { get; set; }
        public DateTime? DataPedido { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataEntrega { get; set; }
        public Produto Produto { get; set; }
        public Cliente Cliente { get; set; }
        public StatusPedido Status { get; set; }
        public decimal ValorTotal { get; set; }
        public long Id { get; set; }
    }
}
