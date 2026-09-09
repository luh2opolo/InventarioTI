// DTO para actualizar una marca.
// Este archivo es NUEVO y NO reemplaza nada existente.
// Se usa únicamente para recibir datos desde el frontend al actualizar una marca.

// IMPORTANTE:
// El namespace DEBE ser InventarioTI.DTOs.Brand
// porque así lo usa el controlador BrandsController.

namespace InventarioTI.DTOs.Brand
{
    public class BrandUpdateDto
    {
        // Nombre de la marca (obligatorio)
        public string Nombre { get; set; } = string.Empty;

        // Descripción de la marca (opcional)
        public string Descripcion { get; set; } = string.Empty;
    }
}