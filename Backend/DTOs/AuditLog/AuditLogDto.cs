namespace InventarioTI.DTOs.AuditLog
{
    public class AuditLogDto
    {
        public int Id { get; set; }

        public string Action { get; set; } = "";
        public string Description { get; set; } = "";

        public int UserId { get; set; }
        public string UserName { get; set; } = "";

        // Nueva propiedad obligatoria para eliminar el error
        public DateTime CreatedAt { get; set; }
    }
}