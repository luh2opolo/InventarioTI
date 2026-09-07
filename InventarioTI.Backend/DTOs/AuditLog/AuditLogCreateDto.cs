namespace InventarioTI.DTOs.AuditLog
{
    public class AuditLogCreateDto
    {
        public string Action { get; set; } = "";
        public string Description { get; set; } = "";
        public int UserId { get; set; }

        // Se agrega para que el servicio pueda asignarlo sin error
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}