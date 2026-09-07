// DTO usado para devolver información de marcas al frontend.
// Se inicializa Name para evitar advertencias de nulabilidad.

namespace InventarioTI.DTOs.Brand
{
    public class BrandDto
    {
        public int Id { get; set; }
        public required string Name { get; set; } = "";
    }
}