namespace InventarioTI.DTOs.Location
{
    public class LocationDto
    {
        public int Id { get; set; }
        public required string Name { get; set; } = "";
        public required string Description { get; set; } = "";
    }
}