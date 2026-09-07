using InventarioTI.Data;
using InventarioTI.DTOs.Location;
using InventarioTI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioTI.Services
{
    public class LocationService
    {
        private readonly AppDbContext _context;

        public LocationService(AppDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // CREAR UBICACIÓN
        // ============================================================
        public async Task<LocationDto> CreateAsync(LocationCreateDto dto)
        {
            var location = new Location
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                Description = location.Description
            };
        }

        // ============================================================
        // OBTENER TODAS LAS UBICACIONES
        // ============================================================
        public async Task<List<LocationDto>> GetAllAsync()
        {
            return await _context.Locations
                .Select(l => new LocationDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    Description = l.Description
                })
                .ToListAsync();
        }

        // ============================================================
        // OBTENER UBICACIÓN POR ID
        // ============================================================
        public async Task<LocationDto?> GetByIdAsync(int id)
        {
            var l = await _context.Locations.FindAsync(id);

            if (l == null)
                return null;

            return new LocationDto
            {
                Id = l.Id,
                Name = l.Name,
                Description = l.Description
            };
        }

        // ============================================================
        // ACTUALIZAR UBICACIÓN
        // ============================================================
        public async Task<bool> UpdateAsync(int id, LocationUpdateDto dto)
        {
            var l = await _context.Locations.FindAsync(id);

            if (l == null)
                return false;

            l.Name = dto.Name;
            l.Description = dto.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        // ============================================================
        // ELIMINAR UBICACIÓN
        // ============================================================
        public async Task<bool> DeleteAsync(int id)
        {
            var l = await _context.Locations.FindAsync(id);

            if (l == null)
                return false;

            _context.Locations.Remove(l);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}