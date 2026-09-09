using InventarioTI.Data;
using InventarioTI.Helpers;
using InventarioTI.Services;
using InventarioTI.Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

/* ============================================================
   🔵 CONFIGURACIÓN DE BASE DE DATOS
   ============================================================ */
// Registra el DbContext y conecta a SQL Server usando la cadena
// de conexión definida en appsettings.json.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

/* ============================================================
   🔵 INYECCIÓN DE SERVICIOS (Dependency Injection)
   ============================================================ */
// JwtHelper requiere la clave secreta, por eso se inyecta manualmente.
builder.Services.AddScoped<JwtHelper>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var key = config["Jwt:Key"] ?? "ClaveSuperSecreta123456789";
    return new JwtHelper(key);
});

// Servicios del sistema
builder.Services.AddScoped<AuditLogService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DeviceService>();
builder.Services.AddScoped<DeviceTypeService>();
builder.Services.AddScoped<DynamicFieldService>();
builder.Services.AddScoped<AssignmentService>();
builder.Services.AddScoped<BrandService>();          // Servicio de marcas
builder.Services.AddScoped<DeviceQueryService>();    // Servicio de consultas de dispositivos

// Acceso al contexto HTTP
builder.Services.AddHttpContextAccessor();

/* ============================================================
   🔵 CONFIGURACIÓN DE AUTENTICACIÓN JWT
   ============================================================ */
// Se obtiene la clave secreta desde appsettings.json.
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
        ValidateIssuer = false,            // No usamos issuer.
        ValidateAudience = false,          // No usamos audience.
        ValidateLifetime = true,           // Validar expiración del token.
        ValidateIssuerSigningKey = true,   // Validar firma.
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes)
    };
});

/* ============================================================
   🔵 CONFIGURACIÓN DE AUTORIZACIÓN
   ============================================================ */
// Permite usar [Authorize] y roles en controladores.
builder.Services.AddAuthorization();

/* ============================================================
   🔵 CONFIGURACIÓN DE CORS (CRÍTICO PARA EL FRONTEND)
   ============================================================ */
// Permite que el frontend (localhost:5296) llame a la API (localhost:5139).
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5296")   // URL del frontend
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

/* ============================================================
   🔵 CONFIGURACIÓN DE SWAGGER (Documentación de API)
   ============================================================ */
// Swagger siempre activo (importante para IIS y producción).
builder.Services.AddEndpointsApiExplorer();

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

/* ============================================================
   🔵 CONTROLADORES
   ============================================================ */
// Registra los controladores para que la API funcione.
builder.Services.AddControllers();

var app = builder.Build();

/* ============================================================
   🔵 SWAGGER (SIEMPRE ACTIVADO)
   ============================================================ */
app.UseSwagger();
app.UseSwaggerUI();

/* ============================================================
   🔵 MIDDLEWARE DE CORS
   ============================================================ */
// Debe ir ANTES de Authentication y Authorization.
app.UseCors("FrontendPolicy");

/* ============================================================
   🔵 MIDDLEWARE DE AUTENTICACIÓN Y AUTORIZACIÓN
   ============================================================ */
app.UseAuthentication();
app.UseAuthorization();

/* ============================================================
   🔵 ENDPOINTS
   ============================================================ */
// Registra todos los controladores automáticamente.
app.MapControllers();

/* ============================================================
   🔵 EJECUTAR APP
   ============================================================ */
app.Run();