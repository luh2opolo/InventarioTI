// DTO para actualizar un campo dinámico existente.

namespace InventarioTI.DTOs.DynamicField
{
    public class DynamicFieldUpdateDto
    {
        // Nombre del campo dinámico
        public required string FieldName { get; set; } = "";

        // Tipo del campo dinámico
        public required string FieldType { get; set; } = "";

        // Tipo de dispositivo al que pertenece este campo
        public required int DeviceTypeId { get; set; }
    }
}