namespace InventarioTI.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Dpi { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Gender { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string PhotoUrl { get; set; } = "";
        public string Role { get; set; } = "";
        public string Status { get; set; } = "";

        // Nueva propiedad
        public DateTime? BirthDate { get; set; }
    }
}