// Servicio encargado de registrar acciones en el historial de auditoría.
// Este servicio usa tu modelo REAL AuditLog.cs sin modificarlo.

using InventarioTI.Data;
using InventarioTI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioTI.Services
{
    public class AuditLogService
    {
        private readonly AppDbContext _context;

        public AuditLogService(AppDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // MÉTODO: Registrar acción en auditoría
        // ============================================================
        public async Task LogAsync(int userId, string action, string description)
        {
            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                Description = description,
                CreatedAt = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        // ============================================================
        // MÉTODO: Obtener todo el historial
        // ============================================================
        public async Task<List<AuditLog>> GetAllAsync()
        {
            return await _context.AuditLogs
                .Include(a => a.User)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        // ============================================================
        // MÉTODO: Obtener historial por usuario
        // ============================================================
        public async Task<List<AuditLog>> GetByUserAsync(int userId)
        {
            return await _context.AuditLogs
                .Where(a => a.UserId == userId)
                .Include(a => a.User)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }
    }
}