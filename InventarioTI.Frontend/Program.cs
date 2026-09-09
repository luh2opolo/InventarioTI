using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using InventarioTI.Frontend;
using InventarioTI.Frontend.Services;

using MudBlazor.Services;

namespace InventarioTI.Frontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Componente raíz
            builder.RootComponents.Add<App>("#app");

            // MudBlazor
            builder.Services.AddMudServices();

            // Servicios propios
            builder.Services.AddScoped<BrandService>();

            // HttpClient apuntando al BACKEND (CORRECTO)
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5139/")
            });

            await builder.Build().RunAsync();
        }
    }
}