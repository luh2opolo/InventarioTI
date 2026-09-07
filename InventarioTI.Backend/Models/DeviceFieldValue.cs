// Modelo que almacena el valor de un campo dinámico para un dispositivo.

namespace InventarioTI.Models
{
    public class DeviceFieldValue
    {
        public int Id { get; set; }

        // Valor del campo dinámico
        public string Value { get; set; } = "";

        // Relación con Device
        public int DeviceId { get; set; }
        public Device Device { get; set; } = new Device();

        // Relación con DynamicField
        public int DynamicFieldId { get; set; }
        public DynamicField DynamicField { get; set; } = new DynamicField();
    }
}