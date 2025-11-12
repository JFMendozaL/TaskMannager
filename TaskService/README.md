# Task Service - Microservicio de Tareas

## 📋 Descripción

Microservicio para la gestión de tareas, entregas y calificaciones del sistema de Control de Tareas y Calificaciones.

## 🏗️ Arquitectura

El proyecto sigue una arquitectura en capas (Clean Architecture):

```
TaskService/
├── src/
│   ├── TaskService.API/          # Capa de presentación (Controllers, Middleware)
│   ├── TaskService.Application/  # Lógica de aplicación (Services, DTOs)
│   ├── TaskService.Domain/       # Entidades de dominio e interfaces
│   └── TaskService.Infrastructure/ # Implementación de datos (EF Core, Repositorios)
├── setup-database.ps1             # Script de configuración BD (PowerShell)
├── setup-database.bat             # Script de configuración BD (CMD)
└── DATABASE_SETUP_GUIDE.md        # Guía detallada de configuración
```

## 🚀 Tecnologías

- **.NET 8** - Framework principal
- **Entity Framework Core 8** - ORM
- **SQL Server** - Base de datos (AWS RDS)
- **JWT** - Autenticación
- **Swagger/OpenAPI** - Documentación de API
- **BCrypt** - Hashing (para futuras implementaciones)

## 📦 Requisitos Previos

- .NET 8 SDK
- SQL Server (AWS RDS configurado)
- Visual Studio 2022 o VS Code (opcional)
- dotnet-ef CLI tool

## 🛠️ Instalación y Configuración

### Opción 1: Script Automático (Recomendado) ⚡

#### Para PowerShell:
```powershell
cd "D:\Ing. Software II\Proyecto IS\TaskService"
.\setup-database.ps1
```

#### Para CMD:
```cmd
cd "D:\Ing. Software II\Proyecto IS\TaskService"
setup-database.bat
```

### Opción 2: Configuración Manual 📝

#### 1. Instalar herramientas de Entity Framework
```bash
dotnet tool install --global dotnet-ef
```

#### 2. Navegar al directorio del proyecto
```bash
cd "D:\Ing. Software II\Proyecto IS\TaskService\src\TaskService.API"
```

#### 3. Restaurar paquetes NuGet
```bash
dotnet restore
```

#### 4. Crear la migración inicial
```bash
dotnet ef migrations add InitialCreate --project ..\TaskService.Infrastructure --startup-project .
```

#### 5. Aplicar la migración a la base de datos
```bash
dotnet ef database update --project ..\TaskService.Infrastructure --startup-project .
```

#### 6. Ejecutar el proyecto
```bash
dotnet run
```

La API estará disponible en:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`

## 🔍 Verificación de la Base de Datos

Después de ejecutar las migraciones, verifica que las tablas se crearon correctamente:

```sql
USE UserServiceDB_Dev;
GO

-- Listar todas las tablas
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE';

-- Debería mostrar:
-- Tasks
-- TaskSubmissions
-- __EFMigrationsHistory
```

## 📚 Endpoints Principales

### 🏠 Home
```http
GET /api/home
```
Endpoint de salud del servicio (no requiere autenticación).

### 📝 Tareas (Requiere autenticación)

#### Obtener todas las tareas
```http
GET /api/tasks
Authorization: Bearer {token}
```

#### Obtener tarea por ID
```http
GET /api/tasks/{id}
Authorization: Bearer {token}
```

#### Obtener tareas por curso
```http
GET /api/tasks/course/{courseId}
Authorization: Bearer {token}
```

#### Obtener tareas creadas por usuario
```http
GET /api/tasks/user/{userId}
Authorization: Bearer {token}
```

#### Obtener tareas asignadas a un usuario
```http
GET /api/tasks/assigned/{userId}
Authorization: Bearer {token}
```

#### Crear tarea
```http
POST /api/tasks
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Tarea de Matemáticas",
  "description": "Resolver ejercicios del capítulo 5",
  "courseId": 1,
  "createdByUserId": 1,
  "assignedToUserId": 2,
  "priority": 2,
  "dueDate": "2024-12-31T23:59:59"
}
```

**Status Enum:**
- 1 = Pending (Pendiente)
- 2 = InProgress (En Progreso)
- 3 = Completed (Completada)
- 4 = Cancelled (Cancelada)

**Priority Enum:**
- 1 = Low (Baja)
- 2 = Medium (Media)
- 3 = High (Alta)
- 4 = Urgent (Urgente)

#### Actualizar tarea
```http
PUT /api/tasks/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Tarea de Matemáticas Actualizada",
  "status": 2,
  "grade": 95.5,
  "comments": "Revisión completada"
}
```

#### Eliminar tarea
```http
DELETE /api/tasks/{id}
Authorization: Bearer {token}
```

### 📤 Entregas (Requiere autenticación)

#### Obtener entrega por ID
```http
GET /api/tasksubmissions/{id}
Authorization: Bearer {token}
```

#### Obtener todas las entregas de una tarea
```http
GET /api/tasksubmissions/task/{taskId}
Authorization: Bearer {token}
```

#### Obtener todas las entregas de un usuario
```http
GET /api/tasksubmissions/user/{userId}
Authorization: Bearer {token}
```

#### Obtener entrega específica por tarea y usuario
```http
GET /api/tasksubmissions/task/{taskId}/user/{userId}
Authorization: Bearer {token}
```

#### Crear entrega
```http
POST /api/tasksubmissions
Authorization: Bearer {token}
Content-Type: application/json

{
  "taskId": 1,
  "submittedByUserId": 2,
  "submissionContent": "Aquí está mi solución a los ejercicios...",
  "fileUrl": "https://example.com/file.pdf"
}
```

#### Calificar entrega
```http
POST /api/tasksubmissions/{id}/grade
Authorization: Bearer {token}
Content-Type: application/json

{
  "grade": 95.5,
  "feedback": "Excelente trabajo, muy bien explicado",
  "gradedByUserId": 1
}
```

#### Eliminar entrega
```http
DELETE /api/tasksubmissions/{id}
Authorization: Bearer {token}
```

## 🗄️ Modelo de Datos

### Tabla: Tasks
| Columna | Tipo | Descripción |
|---------|------|-------------|
| Id | int (PK) | Identificador único |
| Title | nvarchar(200) | Título de la tarea |
| Description | nvarchar(2000) | Descripción detallada |
| CourseId | int | ID del curso |
| CreatedByUserId | int | ID del usuario creador |
| AssignedToUserId | int (nullable) | ID del usuario asignado |
| Status | int | Estado: 1=Pending, 2=InProgress, 3=Completed, 4=Cancelled |
| Priority | int | Prioridad: 1=Low, 2=Medium, 3=High, 4=Urgent |
| DueDate | datetime2 | Fecha de vencimiento |
| CreatedAt | datetime2 | Fecha de creación |
| UpdatedAt | datetime2 (nullable) | Última actualización |
| CompletedAt | datetime2 (nullable) | Fecha de completado |
| Grade | decimal(5,2) (nullable) | Calificación (0-100) |
| Comments | nvarchar(max) (nullable) | Comentarios adicionales |

### Tabla: TaskSubmissions
| Columna | Tipo | Descripción |
|---------|------|-------------|
| Id | int (PK) | Identificador único |
| TaskId | int (FK) | Referencia a la tarea |
| SubmittedByUserId | int | ID del usuario que entrega |
| SubmissionContent | nvarchar(5000) | Contenido de la entrega |
| FileUrl | nvarchar(500) (nullable) | URL del archivo adjunto |
| SubmittedAt | datetime2 | Fecha de entrega |
| Grade | decimal(5,2) (nullable) | Calificación (0-100) |
| Feedback | nvarchar(max) (nullable) | Retroalimentación del profesor |
| GradedAt | datetime2 (nullable) | Fecha de calificación |
| GradedByUserId | int (nullable) | ID del usuario que califica |

## 🧪 Pruebas con Swagger

1. Ejecutar el proyecto: `dotnet run --project src/TaskService.API`
2. Abrir navegador en `https://localhost:5001/swagger`
3. Obtener un token JWT del UserService (endpoint `/api/auth/login`)
4. Clic en el botón "Authorize" en Swagger
5. Ingresar: `Bearer {tu-token-jwt}`
6. Probar los endpoints

## 📝 Comandos Útiles de EF Core

### Ver todas las migraciones
```bash
dotnet ef migrations list --project src/TaskService.Infrastructure --startup-project src/TaskService.API
```

### Crear nueva migración
```bash
dotnet ef migrations add NombreMigracion --project src/TaskService.Infrastructure --startup-project src/TaskService.API
```

### Aplicar migraciones
```bash
dotnet ef database update --project src/TaskService.Infrastructure --startup-project src/TaskService.API
```

### Revertir migración
```bash
dotnet ef migrations remove --project src/TaskService.Infrastructure --startup-project src/TaskService.API
```

### Generar script SQL
```bash
dotnet ef migrations script --project src/TaskService.Infrastructure --startup-project src/TaskService.API --output migration.sql
```

### Limpiar base de datos
```bash
dotnet ef database update 0 --project src/TaskService.Infrastructure --startup-project src/TaskService.API
```

### Ver información del contexto
```bash
dotnet ef dbcontext info --project src/TaskService.Infrastructure --startup-project src/TaskService.API
```

## 🔧 Configuración

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=controldb.cpc2m0c022b3.us-east-2.rds.amazonaws.com;Database=UserServiceDB_Dev;User Id=admin;Password=ProyectoIS2023;MultipleActiveResultSets=true;TrustServerCertificate=true"
  },
  "Jwt": {
    "Key": "SuperSecretKeyForTaskServiceMicroservice12345",
    "Issuer": "TaskServiceAPI",
    "Audience": "TaskServiceClient"
  }
}
```

## 🤝 Comunicación con otros Microservicios

Este microservicio se integra con:
- **UserService**: Para validar tokens JWT y obtener información de usuarios
- **CourseService**: Para validar existencia de cursos (próximamente)

## 🔐 Seguridad

- **JWT Authentication**: Todos los endpoints (excepto `/api/home`) requieren autenticación
- **Bearer Token**: El token debe incluirse en el header `Authorization: Bearer {token}`
- **Password Hashing**: Las contraseñas se almacenan hasheadas con BCrypt (implementado en UserService)

## 🐳 Docker

```bash
# Construir imagen
docker build -t taskservice:latest .

# Ejecutar contenedor
docker run -p 5000:80 -p 5001:443 taskservice:latest
```

## 🔍 Troubleshooting

### Error: "No se puede conectar a la base de datos"
- Verifica que el servidor RDS esté activo en AWS
- Confirma que tu IP esté en el Security Group
- Verifica las credenciales en `appsettings.json`

### Error: "dotnet-ef no se reconoce"
```bash
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```

### Error: "Build failed"
```bash
dotnet clean
dotnet restore
dotnet build
```

## 📖 Documentación Adicional

- [Guía Completa de Configuración de BD](DATABASE_SETUP_GUIDE.md)
- [Documentación de Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [ASP.NET Core Documentation](https://learn.microsoft.com/aspnet/core/)

## 📊 Estado del Proyecto

- ✅ Arquitectura limpia implementada
- ✅ Endpoints CRUD completos
- ✅ Autenticación JWT
- ✅ Base de datos configurada
- ✅ Swagger documentado
- ⏳ Pruebas unitarias (pendiente)
- ⏳ Integración con CourseService (pendiente)

## 📄 Licencia

Proyecto educativo - Universidad Nacional Autónoma de Honduras

## 👨‍💻 Equipo de Desarrollo

Desarrollado para el curso de Ingeniería de Software II - 2024

---

**Última actualización:** Noviembre 2024
