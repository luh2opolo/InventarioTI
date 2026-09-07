// Modelo que representa un campo dinámico del dispositivo.
// Este modelo usa FieldName y FieldType porque así lo requiere
// DeviceFieldValueService y otros servicios de tu proyecto.

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
        public DeviceType DeviceType { get; set; } = new DeviceType();
    }
}