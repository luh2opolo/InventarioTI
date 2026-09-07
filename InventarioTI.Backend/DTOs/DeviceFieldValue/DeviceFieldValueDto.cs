// DTO que representa un valor de campo dinámico dentro de un dispositivo.
// Se incluyen todas las propiedades que el servicio DeviceFieldValueService requiere.

namespace InventarioTI.DTOs.DeviceFieldValue
{
    public class DeviceFieldValueDto
    {
        public int Id { get; set; }

        // Valor del campo dinámico
        public required string Value { get; set; } = "";

        // Relación con el dispositivo
        public required int DeviceId { get; set; }
        public required string DeviceSerial { get; set; } = "";

        // Relación con el campo dinámico
        public required int DynamicFieldId { get; set; }

        // Nombre del campo dinámico
        public required string FieldName { get; set; } = "";

        // Tipo del campo dinámico
        public required string FieldType { get; set; } = "";
    }
}