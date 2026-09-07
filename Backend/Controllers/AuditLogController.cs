// Controlador del historial de auditoría.
// Permite consultar el historial completo o filtrado por usuario.

using InventarioTI.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventarioTI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditLogController : ControllerBase
    {
        private readonly AuditLogService _service;

        public AuditLogController(AuditLogService service)
        {
            _service = service;
        }

        // ============================================================
        // GET: /api/AuditLog
        // Obtener todo el historial
        // ============================================================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        // ============================================================
        // GET: /api/AuditLog/user/5
        // Obtener historial por usuario
        // ============================================================
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(await _service.GetByUserAsync(userId));
        }
    }
}