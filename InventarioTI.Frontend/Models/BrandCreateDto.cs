namespace InventarioTI.Frontend.Models;

/* ============================================================
   BrandCreateDto.cs
   Representa los datos necesarios para crear una marca.
   ============================================================ */

public class BrandCreateDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}