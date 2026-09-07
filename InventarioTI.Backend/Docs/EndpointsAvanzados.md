# 📘 Endpoints Avanzados — InventarioTI

Este documento describe los endpoints avanzados del backend de InventarioTI.  
Incluyen paginación, búsqueda, filtros y ordenamiento dinámico para dispositivos y asignaciones.

---

## 🔵 1. Device — Endpoint Avanzado

### 📌 Ruta

GET `/api/device/advanced`

### 📌 Descripción

Devuelve una lista paginada de dispositivos con soporte para búsqueda por texto, filtros y ordenamiento dinámico.

### 📌 Parámetros

| Parámetro    | Tipo   | Obligatorio | Descripción                                                |
| ------------ | ------ | ----------- | ---------------------------------------------------------- |
| page         | int    | No          | Número de página (por defecto 1)                           |
| pageSize     | int    | No          | Cantidad de registros por página (por defecto 10)          |
| search       | string | No          | Búsqueda por texto (serial, marca, tipo, ubicación)        |
| status       | string | No          | Estado del dispositivo                                     |
| deviceTypeId | int?   | No          | Filtro por tipo de dispositivo                             |
| brandId      | int?   | No          | Filtro por marca                                           |
| locationId   | int?   | No          | Filtro por ubicación                                       |
| orderBy      | string | No          | Campo para ordenar (ej: `serialnumber`, `brand`, `status`) |
| desc         | bool   | No          | `true` para orden descendente, `false` para ascendente     |

### 📌 Ejemplo de uso

```http
GET /api/device/advanced?page=1&pageSize=20&search=laptop&orderBy=brand&desc=true
```
