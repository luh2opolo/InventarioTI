using InventarioTI.DTOs.Auth;
using InventarioTI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventarioTI.Controllers
{
    // Controlador API para manejar el login
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }

        // ============================================================
        // 🔵 LOGIN
        // ============================================================
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthLoginDto dto)
        {
            var response = await _service.LoginAsync(dto);

            if (response == null)
                return Unauthorized("Credenciales inválidas");

            return Ok(response);
        }
    }
}