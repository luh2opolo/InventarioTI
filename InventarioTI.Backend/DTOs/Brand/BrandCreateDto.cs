// DTO usado para crear una nueva marca.
// Se inicializa Name para evitar advertencias de nulabilidad.

namespace InventarioTI.DTOs.Brand
{
    public class BrandCreateDto
    {
        public required string Name { get; set; } = "";
    }
}