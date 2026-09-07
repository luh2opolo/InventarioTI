// Modelo que representa una marca dentro del inventario.
// Se inicializa Name para evitar advertencias de nulabilidad.

namespace InventarioTI.Models
{
    public class Brand
    {
        public int Id { get; set; }

        // Nombre de la marca (ej: Dell, HP, Lenovo)
        public string Name { get; set; } = "";
    }
}