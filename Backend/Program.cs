using InventarioTI.Data;
using InventarioTI.Helpers;
using InventarioTI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// 🔵 CONFIGURACIÓN DE BASE DE DATOS
// ============================================================
// Registra el DbContext y conecta a SQL Server usando la cadena
// de conexión definida en appsettings.json.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ============================================================
// 🔵 INYECCIÓN DE SERVICIOS (Dependency Injection)
// ============================================================
// Todos los servicios del backend se registran aquí para que
// puedan ser usados en controladores y otros componentes.
builder.Services.AddScoped<JwtHelper>();
builder.Services.AddScoped<AuditLogService>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DeviceService>();
builder.Services.AddScoped<DeviceTypeService>();
builder.Services.AddScoped<DynamicFieldService>();
builder.Services.AddScoped<AssignmentService>();

builder.Services.AddHttpContextAccessor();

// ============================================================
// 🔵 CONFIGURACIÓN DE AUTENTICACIÓN JWT
// ============================================================
// Se obtiene la clave secreta desde appsettings.json.
// Si no existe, se usa una clave por defecto.
var secretKey = builder.Configuration["Jwt:Key"] ?? "ClaveSuperSecreta123456789";

// Convertir clave a bytes para generar la firma del token.
var keyBytes = Encoding.UTF8.GetBytes(secretKey);

// Registrar autenticación JWT.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Validación del token JWT.
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // No usamos issuer.
        ValidateAudience = false, // No usamos audience.
        ValidateLifetime = true, // Validar expiración del token.
        ValidateIssuerSigningKey = true, // Validar firma.
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
    };
});

// ============================================================
// 🔵 CONFIGURACIÓN DE AUTORIZACIÓN
// ============================================================
// Permite usar [Authorize] y roles en controladores.
builder.Services.AddAuthorization();

// ============================================================
// 🔵 CONFIGURACIÓN DE SWAGGER (Documentación de API)
// ============================================================
// ⚠️ ESTA LÍNEA ES CRÍTICA ⚠️
// Sin esto, Swagger NO se publica en producción ni en IIS.
builder.Services.AddEndpointsApiExplorer();

// Configuración completa de Swagger.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "InventarioTI API",
        Version = "v1",
        Description = "API del sistema de inventario con autenticación JWT y roles"
    });

    // Botón de autorización para JWT.
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Ingrese el token JWT con el formato: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// ============================================================
// 🔵 CONTROLADORES
// ============================================================
// Registra los controladores para que la API funcione.
builder.Services.AddControllers();

var app = builder.Build();

// ============================================================
// 🔵 SWAGGER (SIEMPRE ACTIVADO)
// ============================================================
// Esto permite que Swagger funcione en producción e IIS.
// Si lo dejás dentro de "IsDevelopment()", NO funciona en IIS.
app.UseSwagger();
app.UseSwaggerUI();

// ============================================================
// 🔵 MIDDLEWARE DE AUTENTICACIÓN Y AUTORIZACIÓN
// ============================================================
// Deben ir en este orden para que JWT funcione correctamente.
app.UseAuthentication();
app.UseAuthorization();

// ============================================================
// 🔵 ENDPOINTS
// ============================================================
// Registra todos los controladores automáticamente.
app.MapControllers();

// ============================================================
// 🔵 EJECUTAR APP
// ============================================================
// Inicia la aplicación.
app.Run();