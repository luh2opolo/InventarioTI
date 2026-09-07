// Program.cs
// Archivo principal de configuración del proyecto Blazor WebAssembly (Frontend).
// Aquí se inicializa MudBlazor y el HttpClient que apunta a tu API backend.

using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

// IMPORTANTE:
// Este using permite que Program.cs encuentre el componente App.razor.
// Sin esto aparece el error CS0246: 'App' no se encontró.
using InventarioTI.Frontend;

// MudBlazor: servicios de UI modernos
using MudBlazor.Services;

namespace InventarioTI.Frontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Crear host de Blazor WebAssembly
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Registrar el componente raíz de la aplicación (App.razor)
            builder.RootComponents.Add<App>("#app");

            // Activar MudBlazor
            builder.Services.AddMudServices();

            // Registrar HttpClient para consumir tu API backend
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/")
            });

            // Ejecutar la aplicación
            await builder.Build().RunAsync();
        }
    }
}