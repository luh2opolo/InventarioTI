# 📘 Documentación de Modelos — InventarioTI

## Device

| Campo        | Tipo   | Descripción            |
| ------------ | ------ | ---------------------- |
| Id           | int    | Identificador          |
| SerialNumber | string | Número de serie        |
| Status       | string | Estado del dispositivo |
| BrandId      | int    | Marca                  |
| DeviceTypeId | int    | Tipo                   |
| LocationId   | int    | Ubicación              |

## Assignment

| Campo      | Tipo      | Descripción          |
| ---------- | --------- | -------------------- |
| Id         | int       | Identificador        |
| AssignedTo | string    | Persona asignada     |
| AssignedAt | DateTime  | Fecha de asignación  |
| ReturnedAt | DateTime? | Fecha de devolución  |
| Status     | string    | Estado               |
| DeviceId   | int       | Dispositivo asignado |

## Brand

| Campo | Tipo   | Descripción        |
| ----- | ------ | ------------------ |
| Id    | int    | Identificador      |
| Name  | string | Nombre de la marca |

## DeviceType

| Campo | Tipo   | Descripción         |
| ----- | ------ | ------------------- |
| Id    | int    | Identificador       |
| Name  | string | Tipo de dispositivo |

## Location

| Campo | Tipo   | Descripción   |
| ----- | ------ | ------------- |
| Id    | int    | Identificador |
| Name  | string | Ubicación     |

## DynamicField

| Campo     | Tipo   | Descripción      |
| --------- | ------ | ---------------- |
| Id        | int    | Identificador    |
| Name      | string | Nombre del campo |
| FieldType | string | Tipo de dato     |

## DeviceFieldValue

| Campo          | Tipo   | Descripción    |
| -------------- | ------ | -------------- |
| Id             | int    | Identificador  |
| DeviceId       | int    | Dispositivo    |
| DynamicFieldId | int    | Campo dinámico |
| Value          | string | Valor          |
