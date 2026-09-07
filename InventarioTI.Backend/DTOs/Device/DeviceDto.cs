// DTO que representa un dispositivo completo para el frontend.
// Se incluyen los campos dinámicos usando DeviceFieldValueDto.

using InventarioTI.DTOs.DeviceFieldValue;

namespace InventarioTI.DTOs.Device
{
    public class DeviceDto
    {
        public int Id { get; set; }

        public required string SerialNumber { get; set; } = "";
        public required string Status { get; set; } = "";

        public required string BrandName { get; set; } = "";
        public required string DeviceTypeName { get; set; } = "";

        // Lista de campos dinámicos del dispositivo
        public required List<DeviceFieldValueDto> DynamicFields { get; set; } = new();
    }
}