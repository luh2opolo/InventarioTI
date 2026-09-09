using System.Net.Http.Json;
using InventarioTI.Frontend.Models;

namespace InventarioTI.Frontend.Services;

/* ============================================================
   BrandService.cs
   Servicio encargado de consumir la API de marcas.
   ============================================================ */

public class BrandService
{
    private readonly HttpClient _http;

    public BrandService(HttpClient http)
    {
        _http = http;
    }

    /* ============================================================
       🔵 OBTENER LISTADO DE MARCAS
       ============================================================
       - GetFromJsonAsync puede devolver null si el backend falla.
       - Para evitar la advertencia CS8603, devolvemos una lista vacía
         cuando el resultado sea null.
       ============================================================ */
    public async Task<List<BrandDto>> GetBrandsAsync()
    {
        var result = await _http.GetFromJsonAsync<List<BrandDto>>(
            "http://localhost:5139/api/brands"
        );

        return result ?? new List<BrandDto>();   // ✔ Nunca devolvemos null
    }

    /* ============================================================
       🔵 CREAR UNA NUEVA MARCA
       ============================================================
       - PostAsJsonAsync nunca devuelve null, pero sí puede fallar.
       - Se devuelve true/false según el código HTTP.
       ============================================================ */
    public async Task<bool> CreateBrandAsync(BrandCreateDto dto)
    {
        var response = await _http.PostAsJsonAsync(
            "http://localhost:5139/api/brands",
            dto
        );

        return response.IsSuccessStatusCode;     // ✔ Limpio y seguro
    }
}