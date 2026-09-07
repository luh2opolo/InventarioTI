namespace InventarioTI.Models
{
    public class DynamicField
    {
        public int Id { get; set; }

        // Nombre del campo dinámico (ej: RAM, Procesador, IP)
        public string FieldName { get; set; } = "";

        // Tipo del campo dinámico (texto, número, fecha, etc.)
        public string FieldType { get; set; } = "";

        // Relación con DeviceType
        public int DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; } = null!;

        // Lista de valores asociados a este campo dinámico
        public List<DeviceFieldValue> FieldValues { get; set; } = new();
    }
}