using InventarioTI.DTOs.DeviceFieldValue;
using InventarioTI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventarioTI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceFieldValueController : ControllerBase
    {
        private readonly DeviceFieldValueService _service;

        public DeviceFieldValueController(DeviceFieldValueService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            // 🔵 Corrección: reemplaza "!result"
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DeviceFieldValueCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DeviceFieldValueUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}