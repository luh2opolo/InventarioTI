namespace InventarioTI.DTOs.Auth
{
    // DTO para iniciar sesión
    public class AuthLoginDto
    {
        // DPI del usuario (se usará como "username")
        public string Dpi { get; set; } = "";

        // Contraseña del usuario
        public string Password { get; set; } = "";
    }
}