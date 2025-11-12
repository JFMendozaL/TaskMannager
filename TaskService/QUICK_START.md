# 🚀 Guía Rápida - Integración de Base de Datos TaskService

## ⚡ Inicio Rápido (3 Pasos)

### 1️⃣ Ejecuta el Script Automático
```powershell
cd "D:\Ing. Software II\Proyecto IS\TaskService"
.\setup-database.ps1
```

### 2️⃣ Ejecuta el Proyecto
```bash
cd src\TaskService.API
dotnet run
```

### 3️⃣ Abre Swagger
```
https://localhost:5001/swagger
```

---

## 📚 Documentación Completa

| Archivo | Descripción |
|---------|-------------|
| 📖 [README.md](README.md) | Documentación general del proyecto |
| 🗄️ [DATABASE_SETUP_GUIDE.md](DATABASE_SETUP_GUIDE.md) | Guía detallada de configuración de BD |
| 🧪 [TEST_EXAMPLES.md](TEST_EXAMPLES.md) | Ejemplos de prueba con API |
| ⚡ [setup-database.ps1](setup-database.ps1) | Script PowerShell de configuración |
| ⚡ [setup-database.bat](setup-database.bat) | Script CMD de configuración |

---

## 🗂️ Estructura del Proyecto

```
TaskService/
├── 📂 src/
│   ├── 🎮 TaskService.API/              # Controllers & Endpoints
│   │   ├── Controllers/
│   │   │   ├── HomeController.cs        # Health check
│   │   │   ├── TasksController.cs       # CRUD Tareas
│   │   │   └── TaskSubmissionsController.cs
│   │   ├── Program.cs                   # Configuración
│   │   └── appsettings.json             # Connection String
│   │
│   ├── 💼 TaskService.Application/      # Lógica de Negocio
│   │   ├── DTOs/
│   │   │   ├── TaskDto.cs
│   │   │   ├── TaskSubmissionDto.cs
│   │   │   └── ApiResponse.cs
│   │   └── Services/
│   │       ├── ITaskService.cs
│   │       ├── TaskService.cs
│   │       ├── ITaskSubmissionService.cs
│   │       └── TaskSubmissionService.cs
│   │
│   ├── 🏗️ TaskService.Domain/           # Entidades & Interfaces
│   │   ├── Entities/
│   │   │   ├── Task.cs
│   │   │   └── TaskSubmission.cs
│   │   └── Interfaces/
│   │       ├── ITaskRepository.cs
│   │       └── ITaskSubmissionRepository.cs
│   │
│   └── 🗄️ TaskService.Infrastructure/   # Datos & Repositorios
│       ├── Data/
│       │   └── TaskDbContext.cs
│       ├── Migrations/                  # ⚠️ Se crea automáticamente
│       └── Repositories/
│           ├── TaskRepository.cs
│           └── TaskSubmissionRepository.cs
│
├── 📄 README.md                         # Documentación principal
├── 📄 DATABASE_SETUP_GUIDE.md           # Guía de BD
├── 📄 TEST_EXAMPLES.md                  # Ejemplos de uso
├── ⚡ setup-database.ps1                # Script configuración
├── ⚡ setup-database.bat                # Script alternativo
└── 🐳 Dockerfile                        # Docker config
```

---

## 🎯 Endpoints Principales

### 🏠 Health Check (Sin autenticación)
```
GET /api/home
```

### 📝 Tareas (Con autenticación JWT)
```
GET    /api/tasks                    # Listar todas
GET    /api/tasks/{id}               # Obtener por ID
GET    /api/tasks/course/{courseId}  # Por curso
GET    /api/tasks/user/{userId}      # Creadas por usuario
GET    /api/tasks/assigned/{userId}  # Asignadas a usuario
POST   /api/tasks                    # Crear nueva
PUT    /api/tasks/{id}               # Actualizar
DELETE /api/tasks/{id}               # Eliminar
```

### 📤 Entregas (Con autenticación JWT)
```
GET    /api/tasksubmissions/{id}              # Por ID
GET    /api/tasksubmissions/task/{taskId}     # Por tarea
GET    /api/tasksubmissions/user/{userId}     # Por usuario
GET    /api/tasksubmissions/task/{taskId}/user/{userId}
POST   /api/tasksubmissions                   # Crear entrega
POST   /api/tasksubmissions/{id}/grade        # Calificar
DELETE /api/tasksubmissions/{id}              # Eliminar
```

---

## 🔑 Autenticación

### Obtener Token (desde UserService)
```http
POST http://localhost:5002/api/auth/login
Content-Type: application/json

{
  "email": "usuario@universidad.edu",
  "password": "tu-password"
}
```

### Usar Token en TaskService
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

---

## 🗄️ Tablas de Base de Datos

### 📋 Tasks
```
Tasks
├── Id (PK)
├── Title (required, max 200)
├── Description (max 2000)
├── CourseId
├── CreatedByUserId
├── AssignedToUserId (nullable)
├── Status (1=Pending, 2=InProgress, 3=Completed, 4=Cancelled)
├── Priority (1=Low, 2=Medium, 3=High, 4=Urgent)
├── DueDate
├── CreatedAt
├── UpdatedAt (nullable)
├── CompletedAt (nullable)
├── Grade (decimal 5,2, nullable)
└── Comments (nullable)
```

### 📤 TaskSubmissions
```
TaskSubmissions
├── Id (PK)
├── TaskId (FK → Tasks)
├── SubmittedByUserId
├── SubmissionContent (required, max 5000)
├── FileUrl (max 500, nullable)
├── SubmittedAt
├── Grade (decimal 5,2, nullable)
├── Feedback (nullable)
├── GradedAt (nullable)
└── GradedByUserId (nullable)
```

---

## 🔧 Comandos EF Core Esenciales

```bash
# Navegar al directorio API
cd "D:\Ing. Software II\Proyecto IS\TaskService\src\TaskService.API"

# Ver migraciones
dotnet ef migrations list --project ..\TaskService.Infrastructure --startup-project .

# Crear migración
dotnet ef migrations add NombreMigracion --project ..\TaskService.Infrastructure --startup-project .

# Aplicar migraciones
dotnet ef database update --project ..\TaskService.Infrastructure --startup-project .

# Revertir todo
dotnet ef database update 0 --project ..\TaskService.Infrastructure --startup-project .

# Info del contexto
dotnet ef dbcontext info --project ..\TaskService.Infrastructure --startup-project .
```

---

## 🎨 Ejemplos Rápidos

### Crear Tarea
```json
POST /api/tasks
{
  "title": "Tarea de Programación",
  "description": "Implementar algoritmo QuickSort",
  "courseId": 1,
  "createdByUserId": 1,
  "assignedToUserId": 3,
  "priority": 2,
  "dueDate": "2024-12-31T23:59:59"
}
```

### Entregar Tarea
```json
POST /api/tasksubmissions
{
  "taskId": 1,
  "submittedByUserId": 3,
  "submissionContent": "Aquí está mi implementación...",
  "fileUrl": "https://drive.google.com/..."
}
```

### Calificar Entrega
```json
POST /api/tasksubmissions/1/grade
{
  "grade": 95.5,
  "feedback": "Excelente trabajo!",
  "gradedByUserId": 1
}
```

---

## ⚠️ Troubleshooting Rápido

| Problema | Solución |
|----------|----------|
| ❌ No conecta a BD | Verificar IP en AWS Security Group |
| ❌ dotnet-ef no existe | `dotnet tool install --global dotnet-ef` |
| ❌ Build failed | `dotnet clean && dotnet build` |
| ❌ Migraciones duplicadas | Eliminar carpeta Migrations y recrear |
| ❌ Token inválido | Verificar que el token sea del UserService |

---

## 📊 Estados y Prioridades

### Estados (Status)
```
1 = Pending    (Pendiente) 🟡
2 = InProgress (En Progreso) 🔵
3 = Completed  (Completada) 🟢
4 = Cancelled  (Cancelada) 🔴
```

### Prioridades (Priority)
```
1 = Low    (Baja) ⬇️
2 = Medium (Media) ➡️
3 = High   (Alta) ⬆️
4 = Urgent (Urgente) 🔥
```

---

## 🎯 Checklist de Configuración

- [ ] .NET 8 SDK instalado
- [ ] Clonar/descargar proyecto
- [ ] Verificar `appsettings.json` (connection string)
- [ ] Ejecutar script de BD (`setup-database.ps1`)
- [ ] Verificar migraciones aplicadas
- [ ] Ejecutar proyecto (`dotnet run`)
- [ ] Abrir Swagger (`https://localhost:5001/swagger`)
- [ ] Obtener token JWT del UserService
- [ ] Probar endpoint `/api/home`
- [ ] Probar creación de tarea
- [ ] Probar entrega de tarea
- [ ] Verificar datos en SQL Server

---

## 🚀 Próximos Pasos

1. ✅ Integrar con UserService (validar usuarios)
2. ⏳ Crear CourseService
3. ⏳ Implementar notificaciones
4. ⏳ Agregar pruebas unitarias
5. ⏳ Implementar caché con Redis
6. ⏳ Configurar Docker Compose
7. ⏳ Deploy a Azure/AWS

---

## 📞 Enlaces Útiles

- [ASP.NET Core Docs](https://learn.microsoft.com/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/)
- [JWT Authentication](https://jwt.io/)
- [Swagger/OpenAPI](https://swagger.io/)

---

**✨ ¡Listo para comenzar! ✨**

**Última actualización:** Noviembre 11, 2024
