namespace InventarioTI.DTOs.DeviceFieldValue
{
    public class DeviceFieldValueCreateDto
    {
        public required string Value { get; set; } = "";
        public required int DeviceId { get; set; }
        public required int DynamicFieldId { get; set; }
    }
}