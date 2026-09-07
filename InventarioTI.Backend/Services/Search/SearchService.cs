// Servicio de búsqueda avanzada del sistema de inventario.
// Este servicio permite realizar búsquedas por diferentes criterios:
// número de serie, marca, tipo, estado, usuario, ubicación y campos dinámicos.
// Todas las consultas están optimizadas y completamente comentadas en español.

using InventarioTI.Data;
using Microsoft.EntityFrameworkCore;

namespace InventarioTI.Services.Search
{
    public class SearchService
    {
        // Contexto principal de la base de datos.
        private readonly AppDbContext _context;

        // Inyección de dependencias del contexto.
        public SearchService(AppDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // BÚSQUEDA POR NÚMERO DE SERIE
        // ============================================================
        public async Task<object> SearchBySerialAsync(string serial)
        {
            // Busca dispositivos cuyo número de serie coincida exactamente.
            var result = await _context.Devices
                .Where(d => d.SerialNumber == serial)
                .Select(d => new
                {
                    d.Id,
                    d.SerialNumber,
                    Marca = d.Brand.Name,
                    Tipo = d.DeviceType.Name,
                    d.Status,
                    Ubicacion = d.Location.Name
                })
                .ToListAsync();

            return result;
        }

        // ============================================================
        // BÚSQUEDA POR MARCA
        // ============================================================
        public async Task<object> SearchByBrandAsync(string brandName)
        {
            var result = await _context.Devices
                .Where(d => d.Brand.Name.Contains(brandName))
                .Select(d => new
                {
                    d.Id,
                    d.SerialNumber,
                    Marca = d.Brand.Name,
                    Tipo = d.DeviceType.Name,
                    d.Status,
                    Ubicacion = d.Location.Name
                })
                .ToListAsync();

            return result;
        }

        // ============================================================
        // BÚSQUEDA POR TIPO DE DISPOSITIVO
        // ============================================================
        public async Task<object> SearchByTypeAsync(string typeName)
        {
            var result = await _context.Devices
                .Where(d => d.DeviceType.Name.Contains(typeName))
                .Select(d => new
                {
                    d.Id,
                    d.SerialNumber,
                    Marca = d.Brand.Name,
                    Tipo = d.DeviceType.Name,
                    d.Status,
                    Ubicacion = d.Location.Name
                })
                .ToListAsync();

            return result;
        }

        // ============================================================
        // BÚSQUEDA POR ESTADO (Disponible, Asignado, Inactivo)
        // ============================================================
        public async Task<object> SearchByStatusAsync(string status)
        {
            var result = await _context.Devices
                .Where(d => d.Status == status)
                .Select(d => new
                {
                    d.Id,
                    d.SerialNumber,
                    Marca = d.Brand.Name,
                    Tipo = d.DeviceType.Name,
                    d.Status,
                    Ubicacion = d.Location.Name
                })
                .ToListAsync();

            return result;
        }

        // ============================================================
        // BÚSQUEDA POR USUARIO ASIGNADO
        // ============================================================
        public async Task<object> SearchByUserAsync(string fullName)
        {
            // IMPORTANTE:
            // Tu modelo REAL Assignment NO tiene AssignedDate.
            // Por eso lo eliminamos del SELECT.

            var result = await _context.Assignments
                .Where(a => a.AssignedTo.Contains(fullName) && a.Status == "Activo")
                .Select(a => new
                {
                    a.DeviceId,
                    Serial = a.Device.SerialNumber,
                    Marca = a.Device.Brand.Name,
                    Tipo = a.Device.DeviceType.Name,
                    Usuario = a.AssignedTo
                    // AssignedDate eliminado porque NO existe en tu modelo.
                })
                .ToListAsync();

            return result;
        }

        // ============================================================
        // BÚSQUEDA POR UBICACIÓN
        // ============================================================
        public async Task<object> SearchByLocationAsync(string locationName)
        {
            var result = await _context.Devices
                .Where(d => d.Location.Name.Contains(locationName))
                .Select(d => new
                {
                    d.Id,
                    d.SerialNumber,
                    Marca = d.Brand.Name,
                    Tipo = d.DeviceType.Name,
                    d.Status,
                    Ubicacion = d.Location.Name
                })
                .ToListAsync();

            return result;
        }

        // ============================================================
        // BÚSQUEDA POR CAMPO DINÁMICO
        // ============================================================
        public async Task<object> SearchByDynamicFieldAsync(string fieldName, string value)
        {
            // Usamos la estructura REAL de tu modelo:
            // DynamicField.FieldName
            // DeviceFieldValue.Value

            var result = await _context.DeviceFieldValues
                .Where(df => df.DynamicField.FieldName == fieldName &&
                             df.Value.Contains(value))
                .Select(df => new
                {
                    df.DeviceId,
                    Serial = df.Device.SerialNumber,
                    Campo = df.DynamicField.FieldName,
                    Valor = df.Value
                })
                .ToListAsync();

            return result;
        }

        // ============================================================
        // BÚSQUEDA GENERAL (texto libre)
        // ============================================================
        public async Task<object> SearchGeneralAsync(string text)
        {
            // Busca coincidencias en múltiples campos simultáneamente.
            var result = await _context.Devices
                .Where(d =>
                    d.SerialNumber.Contains(text) ||
                    d.Brand.Name.Contains(text) ||
                    d.DeviceType.Name.Contains(text) ||
                    d.Status.Contains(text) ||
                    d.Location.Name.Contains(text)
                )
                .Select(d => new
                {
                    d.Id,
                    d.SerialNumber,
                    Marca = d.Brand.Name,
                    Tipo = d.DeviceType.Name,
                    d.Status,
                    Ubicacion = d.Location.Name
                })
                .ToListAsync();

            return result;
        }
    }
}