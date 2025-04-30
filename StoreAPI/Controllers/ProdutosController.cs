using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using StoreAPI.Extensions;
using StoreAPI.Models;
using StoreAPI.Services;

namespace StoreAPI.Controllers
{
    public class ProdutosController : CrudODataController<Produto>
    {
        public ProdutosController(Service<Produto> service) : base(service)
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

            var authFilterResults = result.Get<IQueryable<Produto>>().Where(e => e.PartnerId == partnerId);
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
        public async Task<IActionResult> Create([FromBody] Produto Produto)
        {
            Produto.PartnerId = User.IsAdmin() ? Produto.PartnerId : User.GetPartnerId().Value;
            return await base.Post(Produto);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Produto Produto)
        {
            //todo: validar se produto pertence ao partner
            return await base.Put(Produto.Id, Produto);
        }
    }
}
