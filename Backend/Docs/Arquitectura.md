# 🏛 Arquitectura del Backend — InventarioTI

## Diagrama general

[Frontend] → [Controllers] → [Services] → [DbContext] → [SQL Server]

Código

## Flujo de una petición

1. El usuario envía una solicitud.
2. El controlador recibe la petición.
3. El controlador valida JWT.
4. El controlador llama al servicio.
5. El servicio ejecuta la lógica.
6. El servicio usa DbContext para acceder a la base de datos.
7. Se devuelve la respuesta al usuario.

## Capas

### Controllers

- Reciben peticiones
- Validan roles
- Llaman servicios

### Services

- Lógica del negocio
- Auditoría
- Validaciones

### Data

- EF Core
- Migraciones
- Relaciones

### Models

- Representan tablas

### Docs

- Documentación oficial
