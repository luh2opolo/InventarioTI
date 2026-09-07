namespace InventarioTI.DTOs.User
{
    public class UserUpdateDto
    {
        public string Dpi { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Gender { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string Role { get; set; } = "";
        public string Status { get; set; } = "";
        public string Password { get; set; } = "";
        public DateTime? BirthDate { get; set; }

        // Foto opcional
        public IFormFile? Photo { get; set; }
    }
}