// Modelo que almacena el valor de un campo dinámico para un dispositivo.
// Cada registro representa un valor específico (ej: "16GB", "Intel i5", "Rojo")
// asociado a un DynamicField definido para un DeviceType.

namespace InventarioTI.Models
{
    public class DeviceFieldValue
    {
        // Identificador único del valor del campo dinámico
        public int Id { get; set; }

        // Valor almacenado (texto, número, fecha, etc. según el tipo del campo)
        public string Value { get; set; } = "";

        // ============================================================
        // RELACIÓN CON DEVICE
        // Cada valor pertenece a un dispositivo específico.
        // DeviceId = FK hacia la tabla Devices
        // Device = navegación hacia el objeto Device
        // ============================================================
        public int DeviceId { get; set; }

        // Se inicializa en null para evitar crear objetos vacíos
        // y permitir que EF Core maneje la carga de navegación.
        public Device Device { get; set; } = null!;

        // ============================================================
        // RELACIÓN CON DYNAMICFIELD
        // DynamicFieldId = FK hacia la tabla DynamicFields
        // DynamicField = navegación hacia el campo dinámico definido
        // ============================================================
        public int DynamicFieldId { get; set; }

        // Igual que arriba, se deja en null para evitar instancias innecesarias.
        public DynamicField DynamicField { get; set; } = null!;
    }
}