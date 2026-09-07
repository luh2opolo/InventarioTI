// Servicio encargado de manejar los usuarios del sistema.
// Incluye auditoría en todas las operaciones CRUD y métodos de consulta.
// Totalmente comentado para que entiendas cada paso.

using InventarioTI.Data;
using InventarioTI.Helpers;
using InventarioTI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioTI.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly AuditLogService _audit;
        private readonly IHttpContextAccessor _http;

        public UserService(AppDbContext context, AuditLogService audit, IHttpContextAccessor http)
        {
            _context = context;
            _audit = audit;
            _http = http;
        }

        // ============================================================
        // 🔵 MÉTODO INTERNO: Obtener UserId desde el token JWT
        // ============================================================
        private int GetUserId()
        {
            var header = _http.HttpContext?.Request.Headers["Authorization"].ToString() ?? "";
            return UserContextHelper.GetUserIdFromToken(header);
        }

        // ============================================================
        // 🔵 CREAR USUARIO
        // ============================================================
        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            int adminId = GetUserId();
            await _audit.LogAsync(adminId, "CREATE", $"Se creó el usuario {user.FullName} (DPI: {user.Dpi})");

            return user;
        }

        // ============================================================
        // 🔵 ACTUALIZAR USUARIO
        // ============================================================
        public async Task<User?> UpdateAsync(User user)
        {
            var existing = await _context.Users.FindAsync(user.Id);

            if (existing == null)
                return null;

            existing.FullName = user.FullName;
            existing.Dpi = user.Dpi;
            existing.Password = user.Password;
            existing.Role = user.Role;

            await _context.SaveChangesAsync();

            int adminId = GetUserId();
            await _audit.LogAsync(adminId, "UPDATE", $"Se actualizó el usuario {user.FullName} (ID: {user.Id})");

            return existing;
        }

        // ============================================================
        // 🔵 ELIMINAR USUARIO
        // ============================================================
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            int adminId = GetUserId();
            await _audit.LogAsync(adminId, "DELETE", $"Se eliminó el usuario {user.FullName} (ID: {user.Id})");

            return true;
        }

        // ============================================================
        // 🔵 OBTENER TODOS LOS USUARIOS
        // ============================================================
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .OrderBy(u => u.FullName)
                .ToListAsync();
        }

        // ============================================================
        // 🔵 OBTENER USUARIO POR ID
        // ============================================================
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // ============================================================
        // 🔵 OBTENER USUARIO POR DPI
        // ============================================================
        public async Task<User?> GetByDpiAsync(string dpi)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Dpi == dpi);
        }
    }
}