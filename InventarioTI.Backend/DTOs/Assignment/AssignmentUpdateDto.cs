namespace InventarioTI.DTOs.Assignment
{
    public class AssignmentUpdateDto
    {
        public string AssignedTo { get; set; } = "";
        public int DeviceId { get; set; }
        public string Status { get; set; } = "Activo";
        public DateTime? ReturnedAt { get; set; }
    }
}