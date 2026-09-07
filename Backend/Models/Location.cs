// Modelo que representa una ubicación física dentro de la institución.
// Ejemplos: Oficina 3, Bodega Central, Departamento de TI, Recepción.

namespace InventarioTI.Models
{
    public class Location
    {
        public int Id { get; set; }

        // Nombre de la ubicación
        public string Name { get; set; } = "";

        // Descripción opcional
        public string Description { get; set; } = "";

        // Lista de dispositivos en esta ubicación
        public List<Device> Devices { get; set; } = new List<Device>();
    }
}