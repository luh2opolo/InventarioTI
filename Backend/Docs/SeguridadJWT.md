# 🔐 Seguridad JWT — InventarioTI

El backend usa JWT para autenticación y roles para autorización.

## Flujo de autenticación

1. El usuario envía credenciales.
2. El backend valida usuario y contraseña.
3. Se genera un token JWT con:
   - Id del usuario
   - Rol
   - Fecha de expiración
4. El frontend envía el token en cada petición.

## Encabezado requerido

Authorization: Bearer TU_TOKEN

Código

## Roles disponibles

- Admin
- Auditor
- Usuario

## Protección de endpoints

```csharp
[Authorize(Roles = "Admin,Auditor")]
Expiración del token
Configurada en appsettings.json.

Validación del token
Se realiza en Program.cs con:

Issuer

Audience

Key
```
