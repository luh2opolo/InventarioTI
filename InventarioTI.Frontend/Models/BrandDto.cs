namespace InventarioTI.Frontend.Models;

/* ============================================================
   BrandDto.cs
   Representa una marca en el listado.
   ============================================================ */

public class BrandDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}