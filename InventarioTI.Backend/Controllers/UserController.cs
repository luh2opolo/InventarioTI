using InventarioTI.DTOs.User;
using InventarioTI.Models;
using InventarioTI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventarioTI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        // ============================================================
        // 🔵 CREAR USUARIO
        // ============================================================
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
        {
            // Mapeo DTO → Modelo
            var user = new User
            {
                FullName = dto.FullName,
                Dpi = dto.Dpi,
                Password = dto.Password,
                Role = dto.Role
            };

            var result = await _service.CreateAsync(user);
            return Ok(result);
        }

        // ============================================================
        // 🔵 OBTENER TODOS LOS USUARIOS
        // ============================================================
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // ============================================================
        // 🔵 OBTENER USUARIO POR ID
        // ============================================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // ============================================================
        // 🔵 ACTUALIZAR USUARIO
        // ============================================================
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto dto)
        {
            // Mapeo DTO → Modelo
            var user = new User
            {
                Id = id,
                FullName = dto.FullName,
                Dpi = dto.Dpi,
                Password = dto.Password,
                Role = dto.Role
            };

            var result = await _service.UpdateAsync(user);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // ============================================================
        // 🔵 ELIMINAR USUARIO
        // ============================================================
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result)
                return NotFound();

            return Ok("Usuario eliminado");
        }
    }
}