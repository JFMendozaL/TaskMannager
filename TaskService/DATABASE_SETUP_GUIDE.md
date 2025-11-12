# 🗄️ Guía de Configuración de Base de Datos - TaskService

## 📋 Índice
1. [Requisitos Previos](#requisitos-previos)
2. [Métodos de Configuración](#métodos-de-configuración)
3. [Verificación](#verificación)
4. [Comandos Útiles](#comandos-útiles)
5. [Troubleshooting](#troubleshooting)

---

## 🎯 Requisitos Previos

- ✅ .NET 8.0 SDK instalado
- ✅ Acceso al servidor SQL Server en AWS RDS
- ✅ Visual Studio o Visual Studio Code (opcional)

---

## 🚀 Métodos de Configuración

### **Método 1: Script Automático (Recomendado)**

#### **Para PowerShell:**
```powershell
cd "D:\Ing. Software II\Proyecto IS\TaskService"
.\setup-database.ps1
```

#### **Para CMD:**
```cmd
cd "D:\Ing. Software II\Proyecto IS\TaskService"
setup-database.bat
```

### **Método 2: Comandos Manuales**

#### **Paso 1: Instalar herramientas EF Core**
```bash
dotnet tool install --global dotnet-ef
```

#### **Paso 2: Navegar al proyecto**
```bash
cd "D:\Ing. Software II\Proyecto IS\TaskService\src\TaskService.API"
```

#### **Paso 3: Crear migración**
```bash
dotnet ef migrations add InitialCreate --project ..\TaskService.Infrastructure --startup-project .
```

#### **Paso 4: Aplicar migración**
```bash
dotnet ef database update --project ..\TaskService.Infrastructure --startup-project .
```

---

## ✅ Verificación

### **1. Verificar que las tablas se crearon correctamente**

Conecta a tu base de datos SQL Server y ejecuta:

```sql
USE UserServiceDB_Dev;
GO

-- Listar todas las tablas
SELECT TABLE_NAME 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_TYPE = 'BASE TABLE';

-- Verificar estructura de Tasks
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Tasks';

-- Verificar estructura de TaskSubmissions
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'TaskSubmissions';
```

### **2. Insertar datos de prueba (Opcional)**

```sql
-- Insertar una tarea de ejemplo
INSERT INTO Tasks (Title, Description, CourseId, CreatedByUserId, Status, Priority, DueDate, CreatedAt)
VALUES ('Tarea de Prueba', 'Esta es una tarea de prueba', 1, 1, 1, 2, GETDATE() + 7, GETUTCDATE());

-- Verificar que se insertó correctamente
SELECT * FROM Tasks;
```

---

## 🛠️ Comandos Útiles de Entity Framework

### **Listar todas las migraciones**
```bash
dotnet ef migrations list --project ..\TaskService.Infrastructure --startup-project .
```

### **Crear una nueva migración**
```bash
dotnet ef migrations add NombreDeLaMigracion --project ..\TaskService.Infrastructure --startup-project .
```

### **Revertir la última migración**
```bash
dotnet ef migrations remove --project ..\TaskService.Infrastructure --startup-project .
```

### **Actualizar a una migración específica**
```bash
dotnet ef database update NombreDeLaMigracion --project ..\TaskService.Infrastructure --startup-project .
```

### **Revertir todas las migraciones (limpiar BD)**
```bash
dotnet ef database update 0 --project ..\TaskService.Infrastructure --startup-project .
```

### **Generar script SQL sin ejecutar**
```bash
dotnet ef migrations script --project ..\TaskService.Infrastructure --startup-project . --output migration.sql
```

### **Ver información de la base de datos actual**
```bash
dotnet ef dbcontext info --project ..\TaskService.Infrastructure --startup-project .
```

---

## 🔧 Troubleshooting

### **Error: "No se puede conectar al servidor"**

**Solución:**
1. Verifica que el servidor RDS esté activo
2. Revisa la cadena de conexión en `appsettings.json`
3. Verifica que tu IP esté en el security group de AWS
4. Prueba la conexión con SQL Server Management Studio

**Cadena de conexión actual:**
```
Data Source=controldb.cpc2m0c022b3.us-east-2.rds.amazonaws.com;
Database=UserServiceDB_Dev;
User Id=admin;
Password=ProyectoIS2023;
MultipleActiveResultSets=true;
TrustServerCertificate=true
```

### **Error: "La tabla ya existe"**

**Solución:**
```bash
# Eliminar todas las migraciones y empezar de nuevo
dotnet ef database update 0 --project ..\TaskService.Infrastructure --startup-project .
dotnet ef migrations remove --project ..\TaskService.Infrastructure --startup-project .
dotnet ef migrations add InitialCreate --project ..\TaskService.Infrastructure --startup-project .
dotnet ef database update --project ..\TaskService.Infrastructure --startup-project .
```

### **Error: "Build failed"**

**Solución:**
```bash
# Limpiar y reconstruir el proyecto
dotnet clean
dotnet build
# Intentar nuevamente la migración
```

### **Error: "dotnet-ef no se reconoce"**

**Solución:**
```bash
# Reinstalar la herramienta
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef

# Verificar instalación
dotnet ef --version
```

---

## 📊 Estructura de la Base de Datos

### **Tabla: Tasks**
| Columna | Tipo | Descripción |
|---------|------|-------------|
| Id | int (PK) | Identificador único |
| Title | nvarchar(200) | Título de la tarea |
| Description | nvarchar(2000) | Descripción detallada |
| CourseId | int | ID del curso |
| CreatedByUserId | int | ID del creador |
| AssignedToUserId | int (nullable) | ID del asignado |
| Status | int | Estado (Enum) |
| Priority | int | Prioridad (Enum) |
| DueDate | datetime2 | Fecha de vencimiento |
| CreatedAt | datetime2 | Fecha de creación |
| UpdatedAt | datetime2 (nullable) | Última actualización |
| CompletedAt | datetime2 (nullable) | Fecha de completado |
| Grade | decimal(5,2) (nullable) | Calificación |
| Comments | nvarchar(max) | Comentarios |

### **Tabla: TaskSubmissions**
| Columna | Tipo | Descripción |
|---------|------|-------------|
| Id | int (PK) | Identificador único |
| TaskId | int (FK) | Referencia a Tasks |
| SubmittedByUserId | int | ID del que entrega |
| SubmissionContent | nvarchar(5000) | Contenido de la entrega |
| FileUrl | nvarchar(500) | URL del archivo |
| SubmittedAt | datetime2 | Fecha de entrega |
| Grade | decimal(5,2) (nullable) | Calificación |
| Feedback | nvarchar(max) | Retroalimentación |
| GradedAt | datetime2 (nullable) | Fecha de calificación |
| GradedByUserId | int (nullable) | ID del calificador |

---

## 📝 Enums

### **TaskStatus**
```csharp
1 = Pending (Pendiente)
2 = InProgress (En Progreso)
3 = Completed (Completada)
4 = Cancelled (Cancelada)
```

### **TaskPriority**
```csharp
1 = Low (Baja)
2 = Medium (Media)
3 = High (Alta)
4 = Urgent (Urgente)
```

---

## 🎯 Próximos Pasos

Una vez configurada la base de datos:

1. ✅ Ejecuta el proyecto: `dotnet run --project TaskService.API`
2. ✅ Accede a Swagger: `https://localhost:7xxx/swagger`
3. ✅ Prueba los endpoints
4. ✅ Verifica la creación de datos en la BD

---

## 📞 Soporte

Si encuentras problemas:
1. Revisa los logs de la aplicación
2. Verifica la conexión a la base de datos
3. Consulta la documentación oficial de Entity Framework Core

---

**Última actualización:** Noviembre 2024
