# ✅ Configuración Actualizada - TaskService

## 🗄️ Base de Datos

**Nombre de la Base de Datos:** `UserServiceDB_Dev`

## 📝 Archivos Actualizados

### 1. `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=controldb.cpc2m0c022b3.us-east-2.rds.amazonaws.com;Database=UserServiceDB_Dev;User Id=admin;Password=ProyectoIS2023;MultipleActiveResultSets=true;TrustServerCertificate=true"
  }
}
```

### 2. `appsettings.Development.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=controldb.cpc2m0c022b3.us-east-2.rds.amazonaws.com;Database=UserServiceDB_Dev;User Id=admin;Password=ProyectoIS2023;MultipleActiveResultSets=true;TrustServerCertificate=true"
  }
}
```

## ⚠️ IMPORTANTE: Esquema de Tablas

Como estás usando la misma base de datos que el **UserService**, las tablas del TaskService se crearán en **UserServiceDB_Dev** junto con las tablas de usuarios existentes.

### Tablas Existentes (UserService):
- Users
- Roles
- UserRoles
- __EFMigrationsHistory (de UserService)

### Tablas Nuevas (TaskService):
- Tasks
- TaskSubmissions
- __EFMigrationsHistory (de TaskService)

## 🎯 Consideraciones

### ✅ Ventajas de compartir la BD:
1. Facilita las relaciones entre usuarios y tareas
2. Simplifica la infraestructura
3. Reduce costos
4. Facilita las consultas JOIN

### ⚠️ Precauciones:
1. Asegúrate de que los nombres de las tablas no colisionen
2. Cada servicio debería tener su propio contexto (DbContext)
3. No mezclar migraciones entre servicios
4. Mantener separación lógica aunque compartan BD física

## 🚀 Pasos para Ejecutar

### 1. Verificar la Configuración
```bash
cd "D:\Ing. Software II\Proyecto IS\TaskService\src\TaskService.API"

# Ver la configuración actual
type appsettings.json
```

### 2. Ejecutar el Script de Configuración
```powershell
cd "D:\Ing. Software II\Proyecto IS\TaskService"
.\setup-database.ps1
```

### 3. Verificar las Tablas Creadas
```sql
USE UserServiceDB_Dev;
GO

-- Ver todas las tablas
SELECT 
    TABLE_SCHEMA,
    TABLE_NAME,
    TABLE_TYPE
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;

-- Deberías ver:
-- dbo.Roles              (UserService)
-- dbo.Users              (UserService)
-- dbo.UserRoles          (UserService)
-- dbo.Tasks              (TaskService) ← NUEVA
-- dbo.TaskSubmissions    (TaskService) ← NUEVA
-- dbo.__EFMigrationsHistory
```

### 4. Verificar las Migraciones
```sql
-- Ver el historial de migraciones
SELECT * FROM __EFMigrationsHistory
ORDER BY MigrationId;

-- Deberías ver migraciones de ambos servicios:
-- 20241104053057_InitialCreate (UserService)
-- 20241111XXXXXX_InitialCreate (TaskService)
```

## 🔗 Relaciones entre Servicios

Aunque las tablas están en la misma base de datos, NO se deben crear **foreign keys físicas** entre las tablas de diferentes servicios. En su lugar:

### ✅ Correcto:
```csharp
// En TaskService.Domain/Entities/Task.cs
public class Task
{
    public int Id { get; set; }
    public int CreatedByUserId { get; set; }  // Solo el ID, sin FK física
    public int? AssignedToUserId { get; set; } // Solo el ID, sin FK física
    // ... resto de propiedades
}
```

### ❌ Incorrecto:
```csharp
// NO hacer esto en arquitectura de microservicios
public class Task
{
    public int CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } // ❌ No crear navegación entre servicios
}
```

## 📊 Verificación de Datos

### Ejemplo de Consulta Cruzada (Solo para verificación):
```sql
-- Ver usuarios y sus tareas creadas
SELECT 
    u.Id AS UserId,
    u.Email AS UserEmail,
    t.Id AS TaskId,
    t.Title AS TaskTitle,
    t.Status,
    t.CreatedAt
FROM Users u
LEFT JOIN Tasks t ON t.CreatedByUserId = u.Id
ORDER BY u.Id, t.CreatedAt DESC;

-- Ver entregas de estudiantes
SELECT 
    u.Id AS StudentId,
    u.Email AS StudentEmail,
    t.Title AS TaskTitle,
    ts.SubmittedAt,
    ts.Grade,
    ts.Feedback
FROM Users u
INNER JOIN TaskSubmissions ts ON ts.SubmittedByUserId = u.Id
INNER JOIN Tasks t ON t.Id = ts.TaskId
ORDER BY ts.SubmittedAt DESC;
```

## 🔐 Seguridad

Aunque compartas la base de datos:

1. **UserService** solo debe acceder a: Users, Roles, UserRoles
2. **TaskService** solo debe acceder a: Tasks, TaskSubmissions
3. Usa diferentes DbContexts para cada servicio
4. No expongas directamente consultas JOIN entre servicios en la API

## 🎯 Próximos Pasos Recomendados

1. ✅ Ejecutar migraciones del TaskService
2. ✅ Verificar que las tablas se crearon correctamente
3. ✅ Probar CRUD de tareas
4. ✅ Validar integridad de datos
5. ⏳ Implementar validación de UserId contra UserService via API (no DB directa)
6. ⏳ Considerar separar a bases de datos independientes en producción

## 📝 Notas Importantes

### Para Desarrollo (Actual):
✅ Está bien compartir la BD para simplificar el desarrollo

### Para Producción (Futuro):
⚠️ Considera separar en bases de datos independientes:
- `UserServiceDB_Prod` - Para usuarios
- `TaskServiceDB_Prod` - Para tareas
- Comunicación entre servicios solo via APIs

Esto mejora:
- Escalabilidad independiente
- Aislamiento de fallas
- Seguridad
- Mantenibilidad

---

**Estado Actual:** ✅ Configuración actualizada y lista para usar

**Base de Datos:** UserServiceDB_Dev (compartida entre UserService y TaskService)

**Última actualización:** Noviembre 11, 2024
