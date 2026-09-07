namespace InventarioTI.DTOs.Device
{
    public class DeviceCreateDto
    {
        public string SerialNumber { get; set; } = "";
        public int BrandId { get; set; }
        public int DeviceTypeId { get; set; }
        public string Status { get; set; } = "";
        public int LocationId { get; set; }   // 🔥 ESTA PROPIEDAD FALTABA
    }
}