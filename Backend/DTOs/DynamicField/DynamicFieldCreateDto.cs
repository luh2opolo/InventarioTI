// DTO para crear un campo dinámico.

namespace InventarioTI.DTOs.DynamicField
{
    public class DynamicFieldCreateDto
    {
        // Nombre del campo dinámico (ej: RAM, Procesador, IP)
        public required string FieldName { get; set; } = "";

        // Tipo del campo dinámico (texto, número, fecha, etc.)
        public required string FieldType { get; set; } = "";

        // Tipo de dispositivo al que pertenece este campo
        public required int DeviceTypeId { get; set; }
    }
}