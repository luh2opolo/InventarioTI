// Modelo que representa un dispositivo dentro del inventario.
// Incluye marca, tipo, número de serie, estado y campos dinámicos.

namespace InventarioTI.Models
{
    public class Device
    {
        public int Id { get; set; }

        // Número de serie del dispositivo
        public string SerialNumber { get; set; } = "";

        // Estado del dispositivo (ej: Disponible, Asignado, En reparación)
        public string Status { get; set; } = "";

        // Relación con la marca
        public int BrandId { get; set; }
        public Brand Brand { get; set; } = new Brand();

        // Relación con el tipo de dispositivo
        public int DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; } = new DeviceType();

        // Ubicación física del dispositivo
            public int LocationId { get; set; }
            public Location Location { get; set; } = new Location();

        // Lista de valores de campos dinámicos
        public List<DeviceFieldValue> DynamicFields { get; set; } = new List<DeviceFieldValue>();
    }
}