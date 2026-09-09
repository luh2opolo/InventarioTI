// ============================================================
// BrandUpdateDto.cs (FRONTEND)
// DTO utilizado para enviar datos al backend cuando se actualiza una marca.
// Este archivo pertenece al FRONTEND y NO depende del backend.
// ============================================================

namespace InventarioTI.Frontend.Models
{
    public class BrandUpdateDto
    {
        // Nombre de la marca (obligatorio)
        public string Nombre { get; set; } = string.Empty;

        // Descripción de la marca (opcional)
        public string Descripcion { get; set; } = string.Empty;
    }
}