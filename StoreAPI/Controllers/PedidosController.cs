using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using StoreAPI.Extensions;
using StoreAPI.Models;
using StoreAPI.Services;
using System.Linq;

namespace StoreAPI.Controllers
{
    public class PedidosController : CrudODataController<Pedido>
    {
        public PedidosController(Service<Pedido> service) : base(service)
        {
        }

        [Authorize]
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            if (User.IsAdmin())
            {
                return await base.GetAll();
            }
            var partnerId = User.GetPartnerId();
            var result = _service.GetAll();
            if (!result.Success) return BadRequest(result.Errors);
            var authFilterResults = result.Get<IEnumerable<Pedido>>().Where(e => e.Produto.PartnerId == partnerId);
            return Ok(authFilterResults);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return await base.GetByIdentifier(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Pedido pedido)
        {
            //todo: validar se produto pertence ao partner
            //todo: calcular valor do pedido no momento da criação
            return await base.Post(pedido);
        }

    }
}
