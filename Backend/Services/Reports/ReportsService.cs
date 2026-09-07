// Servicio de reportes del sistema de inventario.
// Genera consultas avanzadas para obtener información útil
// sobre dispositivos, marcas, tipos, ubicaciones, usuarios
// y campos dinámicos.

using InventarioTI.Data;
using Microsoft.EntityFrameworkCore;

namespace InventarioTI.Services.Reports
{
    public class ReportsService
    {
        private readonly AppDbContext _context;

        public ReportsService(AppDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // REPORTE GENERAL DEL INVENTARIO
        // ============================================================
        public async Task<object> GetInventorySummaryAsync()
        {
            var totalDevices = await _context.Devices.CountAsync();
            var available = await _context.Devices.CountAsync(d => d.Status == "Disponible");
            var assigned = await _context.Assignments.CountAsync(a => a.Status == "Activo");
            var inactive = await _context.Devices.CountAsync(d => d.Status == "Inactivo");

            return new
            {
                TotalDispositivos = totalDevices,
                Disponibles = available,
                Asignados = assigned,
                Inactivos = inactive
            };
        }

        // ============================================================
        // REPORTE POR MARCA
        // ============================================================
        public async Task<object> GetDevicesByBrandAsync()
        {
            var result = await _context.Brands
                .Select(b => new
                {
                    Marca = b.Name,
                    Total = _context.Devices.Count(d => d.BrandId == b.Id),
                    Disponibles = _context.Devices.Count(d => d.BrandId == b.Id && d.Status == "Disponible"),
                    Asignados = _context.Assignments.Count(a => a.Device.BrandId == b.Id && a.Status == "Activo")
                })
                .ToListAsync();

            return result;
        }

        // ============================================================
        // REPORTE POR TIPO DE DISPOSITIVO
        // ============================================================
        public async Task<object> GetDevicesByTypeAsync()
        {
            var result = await _context.DeviceTypes
                .Select(t => new
                {
                    Tipo = t.Name,
                    Total = _context.Devices.Count(d => d.DeviceTypeId == t.Id),
                    Disponibles = _context.Devices.Count(d => d.DeviceTypeId == t.Id && d.Status == "Disponible"),
                    Asignados = _context.Assignments.Count(a => a.Device.DeviceTypeId == t.Id && a.Status == "Activo")
                })
                .ToListAsync();

            return result;
        }

        // ============================================================
        // REPORTE POR UBICACIÓN
        // ============================================================
        public async Task<object> GetDevicesByLocationAsync()
        {
            var result = await _context.Locations
                .Select(l => new
                {
                    Ubicacion = l.Name,
                    Total = _context.Devices.Count(d => d.LocationId == l.Id),
                    Disponibles = _context.Devices.Count(d => d.LocationId == l.Id && d.Status == "Disponible"),
                    Asignados = _context.Assignments.Count(a => a.Device.LocationId == l.Id && a.Status == "Activo")
                })
                .ToListAsync();

            return result;
        }

        // ============================================================
        // REPORTE POR USUARIO
        // ============================================================
        public async Task<object> GetAssignmentsByUserAsync()
        {
            var result = await _context.Users
                .Select(u => new
                {
                    Usuario = u.FullName,
                    TotalAsignados = _context.Assignments.Count(a => a.AssignedTo == u.FullName && a.Status == "Activo")
                })
                .ToListAsync();

            return result;
        }

        // ============================================================
        // REPORTE POR CAMPO DINÁMICO (CORREGIDO)
        // ============================================================
        public async Task<object> GetDevicesByDynamicFieldAsync(string fieldName, string value)
        {
            // Usamos la estructura REAL de tu modelo:
            // DynamicField.FieldName
            // DeviceFieldValue.Value

            var result = await _context.DeviceFieldValues
                .Where(df => df.DynamicField.FieldName == fieldName && df.Value == value)
                .Select(df => new
                {
                    DeviceId = df.DeviceId,
                    Serial = df.Device.SerialNumber,
                    Campo = df.DynamicField.FieldName,
                    Valor = df.Value
                })
                .ToListAsync();

            // Devolvemos "object" para evitar el error CS0029
            return result;
        }
    }
}