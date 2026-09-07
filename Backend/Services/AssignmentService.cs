// Servicio que maneja la lógica de asignaciones.
// Incluye paginación, búsqueda, filtros y ordenamiento dinámico.

using InventarioTI.Data;
using InventarioTI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioTI.Services
{
    public class AssignmentService
    {
        private readonly AppDbContext _context;

        public AssignmentService(AppDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // 🔵 ENDPOINT AVANZADO: PAGINACIÓN + BÚSQUEDA + FILTROS + ORDEN
        // ============================================================
        public async Task<List<Assignment>> GetAdvancedAsync(
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
            // Query base con relaciones
            var query = _context.Assignments
                .Include(a => a.Device)
                    .ThenInclude(d => d.Brand)
                .Include(a => a.Device)
                    .ThenInclude(d => d.DeviceType)
                .Include(a => a.Device)
                    .ThenInclude(d => d.Location)
                .AsQueryable();

            // 🔵 Búsqueda por texto (persona asignada, serial, marca, tipo, ubicación)
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(a =>
                    a.AssignedTo.Contains(search) ||
                    a.Device.SerialNumber.Contains(search) ||
                    a.Device.Brand.Name.Contains(search) ||
                    a.Device.DeviceType.Name.Contains(search) ||
                    a.Device.Location.Name.Contains(search)
                );
            }

            // 🔵 Filtro por estado (Activo, Devuelto)
            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(a => a.Status == status);
            }

            // 🔵 Filtro por dispositivo
            if (deviceId.HasValue)
            {
                query = query.Where(a => a.DeviceId == deviceId.Value);
            }

            // 🔵 Filtro por ubicación
            if (locationId.HasValue)
            {
                query = query.Where(a => a.Device.LocationId == locationId.Value);
            }

            // 🔵 Filtro por rango de fechas
            if (fromDate.HasValue)
            {
                query = query.Where(a => a.AssignedAt >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(a => a.AssignedAt <= toDate.Value);
            }

            // 🔵 Orden dinámico
            query = orderBy.ToLower() switch
            {
                "assignedto" => desc ? query.OrderByDescending(a => a.AssignedTo) : query.OrderBy(a => a.AssignedTo),
                "serialnumber" => desc ? query.OrderByDescending(a => a.Device.SerialNumber) : query.OrderBy(a => a.Device.SerialNumber),
                "brand" => desc ? query.OrderByDescending(a => a.Device.Brand.Name) : query.OrderBy(a => a.Device.Brand.Name),
                "devicetype" => desc ? query.OrderByDescending(a => a.Device.DeviceType.Name) : query.OrderBy(a => a.Device.DeviceType.Name),
                "location" => desc ? query.OrderByDescending(a => a.Device.Location.Name) : query.OrderBy(a => a.Device.Location.Name),
                "assignedat" => desc ? query.OrderByDescending(a => a.AssignedAt) : query.OrderBy(a => a.AssignedAt),
                "returnedat" => desc ? query.OrderByDescending(a => a.ReturnedAt) : query.OrderBy(a => a.ReturnedAt),
                _ => desc ? query.OrderByDescending(a => a.Id) : query.OrderBy(a => a.Id)
            };

            // 🔵 Paginación
            query = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return await query.ToListAsync();
        }
    }
}