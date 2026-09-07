namespace InventarioTI.DTOs.Auth
{
    // DTO que devuelve la respuesta del login
    public class AuthResponseDto
    {
        // Token JWT generado
        public string Token { get; set; } = "";

        // Nombre del usuario
        public string FullName { get; set; } = "";

        // Rol del usuario
        public string Role { get; set; } = "";

        // ID del usuario
        public int UserId { get; set; } 
    }
}