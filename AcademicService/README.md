# AcademicService - Microservicio de Gestión Académica

## 📋 Descripción
Microservicio para la gestión académica del sistema TaskClass. Maneja materias, grupos, períodos académicos, asignaciones de profesores y matrículas de estudiantes.

## ✅ Estado: 100% IMPLEMENTADO Y FUNCIONAL

🎉 **Todos los componentes están completos y listos para usar en Swagger!**

## 🏗️ Arquitectura
- **Clean Architecture** con 4 capas
- **Entity Framework Core** para persistencia
- **SQL Server** como base de datos
- **Swagger/OpenAPI** para documentación

## 📦 Estructura del Proyecto

```
AcademicService/
├── src/
│   ├── AcademicService.API/          # Capa de presentación
│   ├── AcademicService.Application/  # Lógica de aplicación
│   ├── AcademicService.Domain/       # Entidades y contratos
│   └── AcademicService.Infrastructure/ # Implementación de datos
└── AcademicService.sln
```

## 🚀 Inicio Rápido

### Prerrequisitos
- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 / VS Code

### Configuración

1. **Actualizar cadena de conexión** en `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=AcademicServiceDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

2. **Ejecutar migraciones**:
```bash
cd src/AcademicService.Infrastructure
dotnet ef migrations add InitialCreate --startup-project ../AcademicService.API
dotnet ef database update --startup-project ../AcademicService.API
```

3. **Ejecutar el proyecto**:
```bash
cd src/AcademicService.API
dotnet run
```

4. **Acceder a Swagger**:
```
https://localhost:5001
```

## 📚 Entidades Principales

### Subject (Materia)
- Gestión de materias/asignaturas
- Código único por materia
- Código de color para identificación visual

### Group (Grupo)
- Representa grupos/clases
- Nivel educativo (Secundaria, Preparatoria)
- Año escolar

### AcademicPeriod (Período Académico)
- Bimestres, Trimestres, Semestres
- Fechas de inicio y fin
- Gestión de período activo

### TeacherSubjectGroup
- Asignación Profesor-Materia-Grupo
- Vinculado a período académico

### StudentGroup
- Matrícula de estudiantes en grupos
- Número de lista

### ParentStudent
- Vinculación Padre-Estudiante
- Tipo de parentesco

## 🔌 Endpoints Principales

### Subjects
- `GET /api/subjects` - Lista todas las materias
- `GET /api/subjects/{id}` - Obtiene una materia
- `POST /api/subjects` - Crea una materia
- `PUT /api/subjects/{id}` - Actualiza una materia
- `DELETE /api/subjects/{id}` - Elimina una materia

### Groups
- `GET /api/groups` - Lista todos los grupos
- `GET /api/groups/{id}` - Obtiene un grupo
- `POST /api/groups` - Crea un grupo
- `PUT /api/groups/{id}` - Actualiza un grupo
- `DELETE /api/groups/{id}` - Elimina un grupo

### Academic Periods
- `GET /api/academic-periods` - Lista períodos
- `GET /api/academic-periods/current` - Obtiene período actual
- `POST /api/academic-periods` - Crea período
- `PUT /api/academic-periods/{id}` - Actualiza período

### Teacher Assignments
- `GET /api/teacher-assignments` - Lista asignaciones
- `GET /api/teacher-assignments/teacher/{teacherId}` - Por profesor
- `POST /api/teacher-assignments` - Crea asignación

### Student Enrollments
- `GET /api/student-enrollments` - Lista matrículas
- `GET /api/student-enrollments/student/{studentId}` - Por estudiante
- `POST /api/student-enrollments` - Matricular estudiante

### Parent Links
- `GET /api/parent-links` - Lista vínculos
- `GET /api/parent-links/parent/{parentId}` - Por padre
- `POST /api/parent-links` - Vincular padre-estudiante

## 🧪 Ejemplos de Uso

### Crear Materia
```json
POST /api/subjects
{
  "name": "Matemáticas",
  "description": "Álgebra y Geometría",
  "code": "MAT-01",
  "colorCode": "#FF5733"
}
```

### Crear Grupo
```json
POST /api/groups
{
  "name": "4to A",
  "schoolYear": "4to Año",
  "level": "Secundaria"
}
```

### Asignar Profesor a Materia-Grupo
```json
POST /api/teacher-assignments
{
  "teacherId": 1,
  "subjectId": 1,
  "groupId": 1,
  "academicPeriodId": 1
}
```

## ✨ Componentes Implementados

### Repositorios (6/6) ✅
- ✅ `SubjectRepository.cs`
- ✅ `GroupRepository.cs`
- ✅ `AcademicPeriodRepository.cs`
- ✅ `TeacherSubjectGroupRepository.cs`
- ✅ `StudentGroupRepository.cs`
- ✅ `ParentStudentRepository.cs`

### Controladores (6/6) ✅
- ✅ `SubjectsController.cs` - 7 endpoints
- ✅ `GroupsController.cs` - 7 endpoints
- ✅ `AcademicPeriodsController.cs` - 8 endpoints
- ✅ `TeacherAssignmentsController.cs` - 9 endpoints
- ✅ `StudentEnrollmentsController.cs` - 7 endpoints
- ✅ `ParentLinksController.cs` - 7 endpoints

**Total: 45 Endpoints Funcionales**

## 📝 Notas

- ✅ `Program.cs` actualizado con todos los repositorios registrados
- ✅ Todos los repositorios implementan las interfaces de Domain
- ✅ Todas las respuestas usan el patrón ApiResponse
- ✅ JWT Authentication configurado en Swagger
- ✅ Validaciones de negocio implementadas
- ✅ Documentación completa en Swagger

## 📚 Documentación Adicional

- `IMPLEMENTATION_SUMMARY.md` - Resumen completo de la implementación
- `TESTING_GUIDE.md` - Guía de pruebas y escenarios
- `QUICK_COMMANDS.md` - Comandos rápidos
- `setup-database.ps1` - Script automático de configuración

## 🤝 Integración con Otros Servicios

Este microservicio referencia IDs de:
- **UserService**: TeacherId, StudentId, ParentId
- **TaskService**: Las tareas se asocian a TeacherSubjectGroup

## 📄 Licencia
TaskClass - Proyecto Académico de Ingeniería de Software II
