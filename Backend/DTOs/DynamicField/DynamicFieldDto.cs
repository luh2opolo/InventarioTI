// DTO usado para devolver información de campos dinámicos al frontend.
// Se inicializan strings para evitar advertencias de nulabilidad.

namespace InventarioTI.DTOs.DynamicField
{
    public class DynamicFieldDto
    {
        public int Id { get; set; }

        // Nombre del campo dinámico (ej: RAM, Procesador, IP)
        public required string Name { get; set; } = "";

        // Tipo de dato del campo (texto, número, fecha, etc.)
        public required string DataType { get; set; } = "";

        // Relación con el tipo de dispositivo
        public required int DeviceTypeId { get; set; }
    }
}