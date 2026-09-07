public class AssignmentDto
{
    public int Id { get; set; }
    public required string AssignedTo { get; set; } = "";
    public required string Status { get; set; } = "";
    public DateTime AssignedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public required int DeviceId { get; set; }
    public required string DeviceSerial { get; set; } = "";
}