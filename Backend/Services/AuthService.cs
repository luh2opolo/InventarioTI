using InventarioTI.Data;
using InventarioTI.DTOs.Auth;
using InventarioTI.Helpers;
using InventarioTI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioTI.Services
{
    // Servicio que maneja el login y generación de tokens JWT
    // Ahora incluye auditoría de intentos fallidos y logins exitosos.
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtHelper _jwt;
        private readonly AuditLogService _audit;

        public AuthService(AppDbContext context, JwtHelper jwt, AuditLogService audit)
        {
            _context = context;
            _jwt = jwt;
            _audit = audit;
        }

        // ============================================================
        // 🔵 LOGIN
        // ============================================================
        public async Task<AuthResponseDto?> LoginAsync(AuthLoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Dpi == dto.Dpi);

            // Usuario no encontrado
            if (user == null)
            {
                await _audit.LogAsync(0, "LOGIN_FAIL", $"Intento de login fallido. DPI: {dto.Dpi}");
                return null;
            }

            // Contraseña incorrecta
            if (user.Password != dto.Password)
            {
                await _audit.LogAsync(user.Id, "LOGIN_FAIL", $"Contraseña incorrecta para DPI: {dto.Dpi}");
                return null;
            }

            // Generar token JWT real
            string token = _jwt.GenerateToken(user.Id, user.FullName, user.Role);

            // Registrar login exitoso
            await _audit.LogAsync(user.Id, "LOGIN_SUCCESS", $"Inicio de sesión exitoso para {user.FullName}");

            // Respuesta segura
            return new AuthResponseDto
            {
                Token = token,
                FullName = user.FullName,
                Role = user.Role,
                UserId = user.Id
            };
        }
    }
}