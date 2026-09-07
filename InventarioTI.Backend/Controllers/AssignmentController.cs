// Controlador de asignaciones.
// Expone el endpoint avanzado con paginación, búsqueda, filtros y ordenamiento.

using InventarioTI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventarioTI.Controllers
{
    [Authorize(Roles = "Admin,Auditor")]
    [ApiController]
    [Route("api/[controller]")]
    public class AssignmentController : ControllerBase
    {
        private readonly AssignmentService _service;

        public AssignmentController(AssignmentService service)
        {
            _service = service;
        }

        // ============================================================
        // 🔵 ENDPOINT AVANZADO: PAGINACIÓN + BÚSQUEDA + FILTROS + ORDEN
        // ============================================================
        [HttpGet("advanced")]
        public async Task<IActionResult> GetAdvanced(
            int page = 1,
            int pageSize = 10,
            string search = "",
            string status = "",
            int? deviceId = null,
            int? locationId = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string orderBy = "Id",
            bool desc = false)
        {
            var result = await _service.GetAdvancedAsync(
                page,
                pageSize,
                search,
                status,
                deviceId,
                locationId,
                fromDate,
                toDate,
                orderBy,
                desc
            );

            return Ok(result);
        }
    }
}