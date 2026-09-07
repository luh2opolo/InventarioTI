namespace InventarioTI.Models
{
    public class AuditLog
    {
        public int Id { get; set; }

        public string Action { get; set; } = "";
        public string Description { get; set; } = "";

        public int UserId { get; set; }

        // Relación con User
        public User User { get; set; } = new User();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}