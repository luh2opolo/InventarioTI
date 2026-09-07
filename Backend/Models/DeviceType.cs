// Modelo que representa un tipo de dispositivo dentro del inventario.
// Se inicializa Name para evitar advertencias de nulabilidad.

namespace InventarioTI.Models
{
    public class DeviceType
    {
        public int Id { get; set; }

        // Nombre del tipo de dispositivo (ej: Laptop, Desktop, Router)
        public string Name { get; set; } = "";
    }
}