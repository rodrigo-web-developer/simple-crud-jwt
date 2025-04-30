using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using StoreAPI.Services;

namespace StoreAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CrudODataController<T> : ControllerBase where T : class, IEntity
    {
        protected readonly Service<T> _service;

        protected CrudODataController(Service<T> service)
        {
            _service = service;
        }

        protected async Task<IActionResult> GetAll()
        {
            var result = _service.GetAll();
            if (!result.Success) return BadRequest(result.Errors);
            return Ok(result.Data);
        }

        protected async Task<IActionResult> GetByIdentifier(long id)
        {
            var result = await _service.GetByIdAsync(id);
            if (!result.Success) return NotFound(result.Errors);
            return Ok(result.Data);
        }

        protected async Task<IActionResult> Post([FromBody] T entity)
        {
            var result = await _service.CreateAsync(entity);
            if (!result.Success) return BadRequest(result.Errors);
            return StatusCode(201, new { id = entity.Id });
        }

        protected async Task<IActionResult> Put(long id, [FromBody] T entity)
        {
            var result = await _service.UpdateAsync(id, entity);
            if (!result.Success) return BadRequest(result.Errors);
            return NoContent();
        }

        protected async Task<IActionResult> Delete(long id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result.Success) return NotFound(result.Errors);
            return NoContent();
        }
    }

}
