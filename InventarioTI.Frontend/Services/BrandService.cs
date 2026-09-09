using System.Net.Http.Json;
using InventarioTI.Frontend.Models;   // ✔ Aquí están BrandDto, BrandCreateDto y BrandUpdateDto

namespace InventarioTI.Frontend.Services;

/* ============================================================
   BrandService.cs
   Servicio encargado de consumir la API de marcas.
   Este servicio se comunica con el backend mediante HTTP.
   ============================================================ */

public class BrandService
{
    private readonly HttpClient _http;

    // Constructor: recibe HttpClient inyectado por Blazor
    public BrandService(HttpClient http)
    {
        _http = http;
    }

    /* ============================================================
       🔵 OBTENER LISTADO DE MARCAS
       ============================================================
       - Llama al endpoint GET /api/brands
       - Devuelve una lista de BrandDto
       - Si la API falla, devolvemos una lista vacía para evitar null
       ============================================================ */
    public async Task<List<BrandDto>> GetBrandsAsync()
    {
        var result = await _http.GetFromJsonAsync<List<BrandDto>>(
            "http://localhost:5139/api/brands"
        );

        return result ?? new List<BrandDto>();   // ✔ Nunca devolvemos null
    }

    /* ============================================================
       🔵 OBTENER UNA MARCA POR ID
       ============================================================
       - Necesario para cargar datos en BrandEdit.razor
       - Llama al endpoint GET /api/brands/{id}
       ============================================================ */
    public async Task<BrandDto?> GetBrandByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<BrandDto>(
            $"http://localhost:5139/api/brands/{id}"
        );
    }

    /* ============================================================
       🔵 CREAR UNA NUEVA MARCA
       ============================================================
       - Envía un POST a /api/brands
       - El backend espera BrandCreateDto
       - Se devuelve true/false según el código HTTP
       ============================================================ */
    public async Task<bool> CreateBrandAsync(BrandCreateDto dto)
    {
        var response = await _http.PostAsJsonAsync(
            "http://localhost:5139/api/brands",
            dto
        );

        return response.IsSuccessStatusCode;
    }

    /* ============================================================
       🔵 ACTUALIZAR UNA MARCA
       ============================================================
       - Envía un PUT a /api/brands/{id}
       - El backend espera BrandUpdateDto
       - Se devuelve true/false según el código HTTP
       ============================================================ */
    public async Task<bool> UpdateBrandAsync(int id, BrandUpdateDto dto)
    {
        var response = await _http.PutAsJsonAsync(
            $"http://localhost:5139/api/brands/{id}",
            dto
        );

        return response.IsSuccessStatusCode;
    }
}