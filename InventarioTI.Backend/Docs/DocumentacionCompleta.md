🟦 1. Arquitectura del Sistema
El backend está organizado en capas claras:

📁 Models/
Contiene todas las entidades del sistema:

Device

Assignment

Brand

DeviceType

Location

DynamicField

DeviceFieldValue

Cada modelo representa una tabla en la base de datos.

📁 DTOs/
Objetos de transferencia de datos para evitar exponer modelos completos.

📁 Services/
Aquí vive la lógica del negocio:

DeviceService

AssignmentService

BrandService

LocationService

DeviceTypeService

DynamicFieldService

Incluye:

CRUD

Validaciones

Auditoría

Endpoints avanzados (paginación, filtros, búsqueda, ordenamiento)

📁 Controllers/
Exponen los endpoints REST:

DeviceController

AssignmentController

BrandController

LocationController

DeviceTypeController

DynamicFieldController

Todos protegidos con JWT y roles.

📁 Data/
Contiene:

AppDbContext

Configuración de EF Core

Relaciones

Migraciones

📁 Docs/
Documentación del sistema (incluye este archivo).

🟦 2. Modelos del Sistema (FULL EXPLICADOS)
📌 Device
Representa un dispositivo físico del inventario.

csharp
public class Device
{
public int Id { get; set; }
public string SerialNumber { get; set; } = "";
public string Status { get; set; } = "";
public int BrandId { get; set; }
public Brand Brand { get; set; } = new Brand();
public int DeviceTypeId { get; set; }
public DeviceType DeviceType { get; set; } = new DeviceType();
public int LocationId { get; set; }
public Location Location { get; set; } = new Location();
public List<DeviceFieldValue> DynamicFields { get; set; } = new List<DeviceFieldValue>();
}
Explicación:
SerialNumber: número de serie único.

Status: Disponible, Asignado, En reparación, etc.

Brand: marca del dispositivo.

DeviceType: tipo (Laptop, Monitor, Radio, etc.).

Location: ubicación física.

DynamicFields: campos personalizados (ej: RAM, CPU, Color).

📌 Assignment
Representa la asignación de un dispositivo a una persona.

csharp
public class Assignment
{
public int Id { get; set; }
public string AssignedTo { get; set; } = "";
public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
public DateTime? ReturnedAt { get; set; }
public string Status { get; set; } = "Activo";
public int DeviceId { get; set; }
public Device Device { get; set; } = new Device();
}
Explicación:
AssignedTo: persona que recibe el dispositivo.

AssignedAt: fecha de entrega.

ReturnedAt: fecha de devolución.

Status: Activo o Devuelto.

Device: dispositivo asignado.

📌 Brand
csharp
public class Brand
{
public int Id { get; set; }
public string Name { get; set; } = "";
}
📌 DeviceType
csharp
public class DeviceType
{
public int Id { get; set; }
public string Name { get; set; } = "";
}
📌 Location
csharp
public class Location
{
public int Id { get; set; }
public string Name { get; set; } = "";
}
📌 DynamicField
Campos personalizados.

csharp
public class DynamicField
{
public int Id { get; set; }
public string Name { get; set; } = "";
public string FieldType { get; set; } = "";
}
📌 DeviceFieldValue
Valores de los campos dinámicos.

csharp
public class DeviceFieldValue
{
public int Id { get; set; }
public int DeviceId { get; set; }
public int DynamicFieldId { get; set; }
public string Value { get; set; } = "";
}
🟦 3. Seguridad del Sistema (JWT + Roles)
El backend usa:

JWT para autenticación.

Roles para autorización:

Admin

Auditor

Usuario

Cómo funciona:
El usuario inicia sesión.

El backend genera un token JWT.

El token contiene:

Id del usuario

Rol

Fecha de expiración

El frontend envía el token en cada petición:

Código
Authorization: Bearer TU_TOKEN
Protección de endpoints:
csharp
[Authorize(Roles = "Admin,Auditor")]
🟦 4. Auditoría del Sistema
Cada acción queda registrada:

Login exitoso

Login fallido

Crear

Actualizar

Eliminar

Asignar

Devolver

Campos registrados:

Usuario

Acción

Fecha

IP

Endpoint

Datos afectados

🟦 5. Endpoints Avanzados (FULL DOCUMENTADOS)
Ya los tienes en:

Código
Docs/EndpointsAvanzados.md
Incluyen:

Paginación

Búsqueda

Filtros

Ordenamiento

Ejemplos

Parámetros

🟦 6. Flujo Profesional de Ramas
main
Código estable.

dev
Desarrollo.

feature/reportes
Reportes avanzados.

feature/dashboard
Estadísticas.

feature/auditoria-avanzada
Filtros y exportación.

🟦 7. Cómo levantar el backend

1. Restaurar paquetes
   bash
   dotnet restore
2. Aplicar migraciones
   bash
   dotnet ef database update
3. Ejecutar el backend
   bash
   dotnet run
4. Abrir Swagger
   Código
   https://localhost:5001/swagger
   🟦 8. Estructura final del proyecto
   Código
   InventarioTI/
   ├── Controllers/
   ├── Services/
   ├── Models/
   ├── DTOs/
   ├── Data/
   ├── Docs/
   │ ├── DocumentacionCompleta.md
   │ └── EndpointsAvanzados.md
   ├── Program.cs
   ├── appsettings.json
   └── InventarioTI.csproj
   🟦 9. Estado actual del backend
   ✔ Backend completo
   ✔ Seguridad JWT
   ✔ Auditoría
   ✔ Endpoints avanzados
   ✔ Documentación interna
   ✔ Listo para frontend
   ✔ Listo para despliegue
