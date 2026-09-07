// Controlador de búsqueda avanzada.
// Expone endpoints HTTP para realizar búsquedas por diferentes criterios
// dentro del inventario de dispositivos.

using InventarioTI.Services.Search;
using Microsoft.AspNetCore.Mvc;

namespace InventarioTI.Controllers.Search
{
    // Indicamos que este controlador es de tipo API.
    [ApiController]

    // Ruta base del controlador: /api/Search
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        // Servicio de búsqueda que contiene la lógica de negocio.
        private readonly SearchService _service;

        // El servicio se inyecta por dependencia.
        public SearchController(SearchService service)
        {
            _service = service;
        }

        // ============================================================
        // BÚSQUEDA POR NÚMERO DE SERIE
        // GET: /api/Search/serial?serial=ABC123
        // ============================================================
        [HttpGet("serial")]
        public async Task<IActionResult> SearchBySerial([FromQuery] string serial)
        {
            var result = await _service.SearchBySerialAsync(serial);
            return Ok(result);
        }

        // ============================================================
        // BÚSQUEDA POR MARCA
        // GET: /api/Search/brand?brand=HP
        // ============================================================
        [HttpGet("brand")]
        public async Task<IActionResult> SearchByBrand([FromQuery] string brand)
        {
            var result = await _service.SearchByBrandAsync(brand);
            return Ok(result);
        }

        // ============================================================
        // BÚSQUEDA POR TIPO
        // GET: /api/Search/type?type=Laptop
        // ============================================================
        [HttpGet("type")]
        public async Task<IActionResult> SearchByType([FromQuery] string type)
        {
            var result = await _service.SearchByTypeAsync(type);
            return Ok(result);
        }

        // ============================================================
        // BÚSQUEDA POR ESTADO
        // GET: /api/Search/status?status=Disponible
        // ============================================================
        [HttpGet("status")]
        public async Task<IActionResult> SearchByStatus([FromQuery] string status)
        {
            var result = await _service.SearchByStatusAsync(status);
            return Ok(result);
        }

        // ============================================================
        // BÚSQUEDA POR USUARIO
        // GET: /api/Search/user?user=Juan
        // ============================================================
        [HttpGet("user")]
        public async Task<IActionResult> SearchByUser([FromQuery] string user)
        {
            var result = await _service.SearchByUserAsync(user);
            return Ok(result);
        }

        // ============================================================
        // BÚSQUEDA POR UBICACIÓN
        // GET: /api/Search/location?location=Oficina
        // ============================================================
        [HttpGet("location")]
        public async Task<IActionResult> SearchByLocation([FromQuery] string location)
        {
            var result = await _service.SearchByLocationAsync(location);
            return Ok(result);
        }

        // ============================================================
        // BÚSQUEDA POR CAMPO DINÁMICO
        // GET: /api/Search/dynamic?fieldName=RAM&value=16GB
        // ============================================================
        [HttpGet("dynamic")]
        public async Task<IActionResult> SearchByDynamicField(
            [FromQuery] string fieldName,
            [FromQuery] string value)
        {
            var result = await _service.SearchByDynamicFieldAsync(fieldName, value);
            return Ok(result);
        }

        // ============================================================
        // BÚSQUEDA GENERAL (texto libre)
        // GET: /api/Search/general?text=Servidor
        // ============================================================
        [HttpGet("general")]
        public async Task<IActionResult> SearchGeneral([FromQuery] string text)
        {
            var result = await _service.SearchGeneralAsync(text);
            return Ok(result);
        }
    }
}