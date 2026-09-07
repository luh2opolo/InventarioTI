using InventarioTI.Data;
using InventarioTI.DTOs.DeviceFieldValue;
using InventarioTI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioTI.Services
{
    public class DeviceFieldValueService
    {
        private readonly AppDbContext _context;

        public DeviceFieldValueService(AppDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // OBTENER TODOS LOS VALORES DE CAMPOS DINÁMICOS
        // ============================================================
        public async Task<List<DeviceFieldValueDto>> GetAllAsync()
        {
            return await _context.DeviceFieldValues
                .Include(v => v.Device)
                .Include(v => v.DynamicField)
                .Select(v => new DeviceFieldValueDto
                {
                    Id = v.Id,
                    Value = v.Value ?? "",                           // 🔵 Evita CS8601
                    DeviceId = v.DeviceId,
                    DynamicFieldId = v.DynamicFieldId,
                    DeviceSerial = v.Device != null ? v.Device.SerialNumber ?? "" : "",   // 🔵 Evita null
                    FieldName = v.DynamicField != null ? v.DynamicField.FieldName ?? "" : "", // 🔵 Evita null
                    FieldType = v.DynamicField != null ? v.DynamicField.FieldType ?? "" : ""  // 🔵 Evita null
                })
                .ToListAsync();
        }

        // ============================================================
        // OBTENER UN VALOR POR ID
        // ============================================================
        public async Task<DeviceFieldValueDto?> GetByIdAsync(int id)
        {
            var value = await _context.DeviceFieldValues
                .Include(v => v.Device)
                .Include(v => v.DynamicField)
                .FirstOrDefaultAsync(v => v.Id == id);

            // 🔵 Evita CS8603 (retorno nulo sin validar)
            if (value == null)
                return null;

            return new DeviceFieldValueDto
            {
                Id = value.Id,
                Value = value.Value ?? "",
                DeviceId = value.DeviceId,
                DynamicFieldId = value.DynamicFieldId,
                DeviceSerial = value.Device?.SerialNumber ?? "",        // 🔵 Evita CS8601
                FieldName = value.DynamicField?.FieldName ?? "",        // 🔵 Evita CS8601
                FieldType = value.DynamicField?.FieldType ?? ""         // 🔵 Evita CS8601
            };
        }

        // ============================================================
        // CREAR UN VALOR DE CAMPO DINÁMICO
        // ============================================================
        public async Task<DeviceFieldValueDto> CreateAsync(DeviceFieldValueCreateDto dto)
        {
            var value = new DeviceFieldValue
            {
                Value = dto.Value,
                DeviceId = dto.DeviceId,
                DynamicFieldId = dto.DynamicFieldId
            };

            _context.DeviceFieldValues.Add(value);
            await _context.SaveChangesAsync();

            return new DeviceFieldValueDto
            {
                Id = value.Id,
                Value = value.Value ?? "",
                DeviceId = value.DeviceId,
                DynamicFieldId = value.DynamicFieldId,
                DeviceSerial = "",       // 🔵 No se incluye Device aquí
                FieldName = "",          // 🔵 No se incluye DynamicField aquí
                FieldType = ""
            };
        }

        // ============================================================
        // ACTUALIZAR UN VALOR DE CAMPO DINÁMICO
        // ============================================================
        public async Task<DeviceFieldValueDto?> UpdateAsync(int id, DeviceFieldValueUpdateDto dto)
        {
            var value = await _context.DeviceFieldValues.FindAsync(id);

            // 🔵 Evita CS8603
            if (value == null)
                return null;

            value.Value = dto.Value;
            value.DeviceId = dto.DeviceId;
            value.DynamicFieldId = dto.DynamicFieldId;

            await _context.SaveChangesAsync();

            return new DeviceFieldValueDto
            {
                Id = value.Id,
                Value = value.Value ?? "",
                DeviceId = value.DeviceId,
                DynamicFieldId = value.DynamicFieldId,
                DeviceSerial = "",
                FieldName = "",
                FieldType = ""
            };
        }

        // ============================================================
        // ELIMINAR UN VALOR DE CAMPO DINÁMICO
        // ============================================================
        public async Task<bool> DeleteAsync(int id)
        {
            var value = await _context.DeviceFieldValues.FindAsync(id);

            if (value == null)
                return false;

            _context.DeviceFieldValues.Remove(value);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}