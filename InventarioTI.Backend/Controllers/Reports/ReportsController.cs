using InventarioTI.Services.Reports;
using Microsoft.AspNetCore.Mvc;

namespace InventarioTI.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportsService _service;

        public ReportsController(ReportsService service)
        {
            _service = service;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            return Ok(await _service.GetInventorySummaryAsync());
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetByBrand()
        {
            return Ok(await _service.GetDevicesByBrandAsync());
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetByType()
        {
            return Ok(await _service.GetDevicesByTypeAsync());
        }

        [HttpGet("locations")]
        public async Task<IActionResult> GetByLocation()
        {
            return Ok(await _service.GetDevicesByLocationAsync());
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetByUser()
        {
            return Ok(await _service.GetAssignmentsByUserAsync());
        }

        [HttpGet("dynamic")]
        public async Task<IActionResult> GetByDynamicField([FromQuery] string fieldName, [FromQuery] string value)
        {
            return Ok(await _service.GetDevicesByDynamicFieldAsync(fieldName, value));
        }
    }
}