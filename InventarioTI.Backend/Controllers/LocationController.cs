using InventarioTI.DTOs.Location;
using InventarioTI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventarioTI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _service;

        public LocationController(LocationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(LocationCreateDto dto)
        {
            return Ok(await _service.CreateAsync(dto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LocationUpdateDto dto)
        {
            return await _service.UpdateAsync(id, dto)
                ? Ok("Ubicación actualizada.")
                : NotFound("Ubicación no encontrada.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _service.DeleteAsync(id)
                ? Ok("Ubicación eliminada.")
                : NotFound("Ubicación no encontrada.");
        }
    }
}