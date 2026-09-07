// DTO usado para actualizar una marca existente.
// Se inicializa Name para evitar advertencias de nulabilidad.

namespace InventarioTI.DTOs.Brand
{
    public class BrandUpdateDto
    {
        public required string Name { get; set; } = "";
    }
}