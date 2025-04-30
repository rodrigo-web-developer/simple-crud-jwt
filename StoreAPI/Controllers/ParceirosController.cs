using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using StoreAPI.Services;

namespace StoreAPI.Controllers
{

    public class ParceirosController : CrudODataController<Parceiro>
    {
        public ParceirosController(Service<Parceiro> service) : base(service)
        {
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return await base.GetAll();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            return await base.GetByIdentifier(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Parceiro Parceiro)
        {
            return await base.Post(Parceiro);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Parceiro Parceiro)
        {
            return await base.Put(Parceiro.Id, Parceiro);
        }
    }
}
