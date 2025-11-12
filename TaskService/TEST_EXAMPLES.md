# 🧪 Ejemplos de Prueba - TaskService API

## 📋 Índice
1. [Configuración Inicial](#configuración-inicial)
2. [Ejemplos de Tareas](#ejemplos-de-tareas)
3. [Ejemplos de Entregas](#ejemplos-de-entregas)
4. [Flujo Completo](#flujo-completo)
5. [Casos de Prueba](#casos-de-prueba)

---

## 🔧 Configuración Inicial

### **Paso 1: Obtener Token JWT**

Primero debes autenticarte en el **UserService** para obtener un token:

```http
POST http://localhost:5002/api/auth/login
Content-Type: application/json

{
  "email": "profesor@universidad.edu",
  "password": "ProfesorPass123"
}
```

**Respuesta esperada:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "profesor@universidad.edu",
  "userId": 1,
  "roles": ["Teacher"]
}
```

### **Paso 2: Configurar Header de Autorización**

En todas las peticiones posteriores, incluye el header:
```
Authorization: Bearer {tu-token-jwt}
```

---

## 📝 Ejemplos de Tareas

### **1. Crear Tarea - Profesor asigna tarea a estudiante**

```http
POST http://localhost:5000/api/tasks
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Investigación sobre Algoritmos de Ordenamiento",
  "description": "Realizar una investigación detallada sobre los algoritmos QuickSort, MergeSort y HeapSort. Incluir análisis de complejidad temporal y espacial.",
  "courseId": 1,
  "createdByUserId": 1,
  "assignedToUserId": 3,
  "priority": 3,
  "dueDate": "2024-12-15T23:59:59"
}
```

**Respuesta esperada:**
```json
{
  "success": true,
  "message": "Tarea creada exitosamente",
  "data": {
    "id": 1,
    "title": "Investigación sobre Algoritmos de Ordenamiento",
    "description": "Realizar una investigación...",
    "courseId": 1,
    "createdByUserId": 1,
    "assignedToUserId": 3,
    "status": 1,
    "priority": 3,
    "dueDate": "2024-12-15T23:59:59",
    "createdAt": "2024-11-11T10:30:00",
    "grade": null,
    "comments": null,
    "submissions": []
  }
}
```

### **2. Crear Tarea General (Sin asignar a estudiante específico)**

```http
POST http://localhost:5000/api/tasks
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Proyecto Final - Sistema de Gestión",
  "description": "Desarrollar un sistema completo de gestión usando arquitectura de microservicios",
  "courseId": 1,
  "createdByUserId": 1,
  "assignedToUserId": null,
  "priority": 4,
  "dueDate": "2024-12-31T23:59:59"
}
```

### **3. Crear Tarea Urgente**

```http
POST http://localhost:5000/api/tasks
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Corrección de Bug Crítico",
  "description": "Corregir el bug en el módulo de autenticación que permite bypass del login",
  "courseId": 2,
  "createdByUserId": 1,
  "assignedToUserId": 4,
  "priority": 4,
  "dueDate": "2024-11-12T18:00:00"
}
```

### **4. Obtener Todas las Tareas**

```http
GET http://localhost:5000/api/tasks
Authorization: Bearer {token}
```

### **5. Obtener Tarea Específica**

```http
GET http://localhost:5000/api/tasks/1
Authorization: Bearer {token}
```

### **6. Obtener Tareas por Curso**

```http
GET http://localhost:5000/api/tasks/course/1
Authorization: Bearer {token}
```

### **7. Obtener Tareas Creadas por un Profesor**

```http
GET http://localhost:5000/api/tasks/user/1
Authorization: Bearer {token}
```

### **8. Obtener Tareas Asignadas a un Estudiante**

```http
GET http://localhost:5000/api/tasks/assigned/3
Authorization: Bearer {token}
```

### **9. Actualizar Estado de Tarea**

```http
PUT http://localhost:5000/api/tasks/1
Authorization: Bearer {token}
Content-Type: application/json

{
  "status": 2,
  "comments": "El estudiante ha comenzado a trabajar en la tarea"
}
```

**Estados disponibles:**
- `1` = Pending (Pendiente)
- `2` = InProgress (En Progreso)
- `3` = Completed (Completada)
- `4` = Cancelled (Cancelada)

### **10. Actualizar Tarea Completa**

```http
PUT http://localhost:5000/api/tasks/1
Authorization: Bearer {token}
Content-Type: application/json

{
  "title": "Investigación sobre Algoritmos - ACTUALIZADA",
  "description": "Descripción actualizada...",
  "priority": 4,
  "status": 3,
  "grade": 95.5,
  "comments": "Excelente trabajo, muy completo",
  "completedAt": "2024-11-10T20:30:00"
}
```

### **11. Eliminar Tarea**

```http
DELETE http://localhost:5000/api/tasks/1
Authorization: Bearer {token}
```

---

## 📤 Ejemplos de Entregas

### **1. Estudiante Entrega Tarea**

```http
POST http://localhost:5000/api/tasksubmissions
Authorization: Bearer {token}
Content-Type: application/json

{
  "taskId": 1,
  "submittedByUserId": 3,
  "submissionContent": "He completado la investigación sobre algoritmos de ordenamiento. Los resultados muestran que QuickSort tiene una complejidad promedio de O(n log n)...",
  "fileUrl": "https://drive.google.com/file/d/abc123/investigacion_algoritmos.pdf"
}
```

**Respuesta esperada:**
```json
{
  "success": true,
  "message": "Entrega creada exitosamente",
  "data": {
    "id": 1,
    "taskId": 1,
    "submittedByUserId": 3,
    "submissionContent": "He completado la investigación...",
    "fileUrl": "https://drive.google.com/file/d/abc123/investigacion_algoritmos.pdf",
    "submittedAt": "2024-11-11T14:30:00",
    "grade": null,
    "feedback": null,
    "gradedAt": null,
    "gradedByUserId": null
  }
}
```

### **2. Entrega sin Archivo Adjunto**

```http
POST http://localhost:5000/api/tasksubmissions
Authorization: Bearer {token}
Content-Type: application/json

{
  "taskId": 2,
  "submittedByUserId": 3,
  "submissionContent": "Aquí está mi análisis del problema propuesto. La solución óptima utiliza programación dinámica...",
  "fileUrl": null
}
```

### **3. Obtener Entrega por ID**

```http
GET http://localhost:5000/api/tasksubmissions/1
Authorization: Bearer {token}
```

### **4. Obtener Todas las Entregas de una Tarea**

```http
GET http://localhost:5000/api/tasksubmissions/task/1
Authorization: Bearer {token}
```

### **5. Obtener Todas las Entregas de un Estudiante**

```http
GET http://localhost:5000/api/tasksubmissions/user/3
Authorization: Bearer {token}
```

### **6. Obtener Entrega Específica por Tarea y Usuario**

```http
GET http://localhost:5000/api/tasksubmissions/task/1/user/3
Authorization: Bearer {token}
```

### **7. Calificar Entrega - Nota Alta**

```http
POST http://localhost:5000/api/tasksubmissions/1/grade
Authorization: Bearer {token}
Content-Type: application/json

{
  "grade": 95.5,
  "feedback": "Excelente trabajo! La investigación está muy completa y bien documentada. Los análisis de complejidad son correctos y las conclusiones son apropiadas.",
  "gradedByUserId": 1
}
```

### **8. Calificar Entrega - Necesita Mejoras**

```http
POST http://localhost:5000/api/tasksubmissions/2/grade
Authorization: Bearer {token}
Content-Type: application/json

{
  "grade": 70.0,
  "feedback": "Buen intento, pero falta profundidad en el análisis. Por favor revisa los casos especiales y mejora la documentación del código.",
  "gradedByUserId": 1
}
```

### **9. Calificar Entrega - Reprobada**

```http
POST http://localhost:5000/api/tasksubmissions/3/grade
Authorization: Bearer {token}
Content-Type: application/json

{
  "grade": 45.0,
  "feedback": "El trabajo no cumple con los requisitos mínimos. Falta investigación y el código presentado tiene múltiples errores. Por favor revisar las instrucciones y volver a entregar.",
  "gradedByUserId": 1
}
```

### **10. Eliminar Entrega**

```http
DELETE http://localhost:5000/api/tasksubmissions/1
Authorization: Bearer {token}
```

---

## 🔄 Flujo Completo

### **Escenario: Profesor asigna tarea → Estudiante entrega → Profesor califica**

#### **1. Profesor crea tarea**
```http
POST http://localhost:5000/api/tasks
Authorization: Bearer {token-profesor}

{
  "title": "Implementar API REST con .NET",
  "description": "Crear una API REST completa usando .NET 8 y Entity Framework Core",
  "courseId": 1,
  "createdByUserId": 1,
  "assignedToUserId": 3,
  "priority": 3,
  "dueDate": "2024-11-20T23:59:59"
}
```
✅ **Resultado:** Tarea ID = 5 creada

#### **2. Estudiante consulta sus tareas asignadas**
```http
GET http://localhost:5000/api/tasks/assigned/3
Authorization: Bearer {token-estudiante}
```
✅ **Resultado:** Ve la tarea ID = 5

#### **3. Estudiante actualiza estado a "En Progreso"**
```http
PUT http://localhost:5000/api/tasks/5
Authorization: Bearer {token-estudiante}

{
  "status": 2
}
```
✅ **Resultado:** Estado cambia a InProgress

#### **4. Estudiante entrega la tarea**
```http
POST http://localhost:5000/api/tasksubmissions
Authorization: Bearer {token-estudiante}

{
  "taskId": 5,
  "submittedByUserId": 3,
  "submissionContent": "He completado la API REST solicitada. Implementé endpoints CRUD, autenticación JWT y documentación con Swagger.",
  "fileUrl": "https://github.com/estudiante/proyecto-api"
}
```
✅ **Resultado:** Entrega ID = 10 creada

#### **5. Profesor revisa entregas de la tarea**
```http
GET http://localhost:5000/api/tasksubmissions/task/5
Authorization: Bearer {token-profesor}
```
✅ **Resultado:** Ve la entrega ID = 10

#### **6. Profesor califica la entrega**
```http
POST http://localhost:5000/api/tasksubmissions/10/grade
Authorization: Bearer {token-profesor}

{
  "grade": 92.0,
  "feedback": "Muy buen trabajo! La API está bien estructurada. Solo falta validación en algunos endpoints.",
  "gradedByUserId": 1
}
```
✅ **Resultado:** Entrega calificada con 92.0

#### **7. Profesor marca tarea como completada**
```http
PUT http://localhost:5000/api/tasks/5
Authorization: Bearer {token-profesor}

{
  "status": 3,
  "grade": 92.0,
  "completedAt": "2024-11-11T16:00:00"
}
```
✅ **Resultado:** Tarea completada

#### **8. Estudiante consulta su entrega calificada**
```http
GET http://localhost:5000/api/tasksubmissions/10
Authorization: Bearer {token-estudiante}
```
✅ **Resultado:** Ve su calificación y feedback

---

## 🧪 Casos de Prueba

### **Caso 1: Tareas Múltiples por Curso**
```http
# Crear 3 tareas para el mismo curso
POST http://localhost:5000/api/tasks (Tarea 1)
POST http://localhost:5000/api/tasks (Tarea 2)
POST http://localhost:5000/api/tasks (Tarea 3)

# Listar todas las tareas del curso
GET http://localhost:5000/api/tasks/course/1
```

### **Caso 2: Mismo Estudiante Múltiples Tareas**
```http
# Asignar 5 tareas al mismo estudiante
POST http://localhost:5000/api/tasks (assignedToUserId: 3)

# Ver todas las tareas del estudiante
GET http://localhost:5000/api/tasks/assigned/3
```

### **Caso 3: Tarea con Múltiples Entregas (Re-entregas)**
```http
# Primera entrega
POST http://localhost:5000/api/tasksubmissions
{
  "taskId": 1,
  "submittedByUserId": 3,
  "submissionContent": "Primera versión..."
}

# Calificar con nota baja
POST http://localhost:5000/api/tasksubmissions/1/grade
{
  "grade": 60.0,
  "feedback": "Necesita mejoras"
}

# Segunda entrega (corrección)
POST http://localhost:5000/api/tasksubmissions
{
  "taskId": 1,
  "submittedByUserId": 3,
  "submissionContent": "Versión mejorada..."
}

# Calificar nueva entrega
POST http://localhost:5000/api/tasksubmissions/2/grade
{
  "grade": 90.0,
  "feedback": "Mucho mejor!"
}

# Ver todas las entregas de la tarea
GET http://localhost:5000/api/tasksubmissions/task/1
```

### **Caso 4: Tarea Urgente con Fecha Próxima**
```http
POST http://localhost:5000/api/tasks
{
  "title": "Corrección Urgente",
  "priority": 4,
  "dueDate": "2024-11-12T12:00:00" // Mañana
}
```

### **Caso 5: Tarea Sin Estudiante Asignado (General)**
```http
POST http://localhost:5000/api/tasks
{
  "title": "Lectura Opcional - Capítulo 10",
  "assignedToUserId": null
}
```

### **Caso 6: Cambios de Prioridad**
```http
# Crear tarea con prioridad baja
POST http://localhost:5000/api/tasks
{
  "priority": 1
}

# Actualizar a urgente
PUT http://localhost:5000/api/tasks/1
{
  "priority": 4
}
```

---

## 📊 Datos de Prueba Sugeridos

### **Usuarios Simulados**
```
Profesor 1: ID=1, Email=profesor@universidad.edu
Estudiante 1: ID=3, Email=estudiante1@universidad.edu
Estudiante 2: ID=4, Email=estudiante2@universidad.edu
```

### **Cursos Simulados**
```
Curso 1: ID=1, Nombre=Ingeniería de Software II
Curso 2: ID=2, Nombre=Bases de Datos Avanzadas
```

### **Tareas de Ejemplo**
```
1. Investigación académica (Prioridad: Media, Due: +2 semanas)
2. Proyecto final (Prioridad: Alta, Due: +1 mes)
3. Bug fix urgente (Prioridad: Urgente, Due: +1 día)
4. Lectura opcional (Prioridad: Baja, Sin fecha)
```

---

## 💡 Tips de Prueba

1. **Siempre guarda el token JWT** en una variable para reutilizarlo
2. **Prueba primero el endpoint de salud**: `GET /api/home`
3. **Verifica los datos en la BD** después de cada operación importante
4. **Usa Swagger UI** para explorar todos los endpoints disponibles
5. **Prueba casos límite**: tareas vencidas, calificaciones fuera de rango, etc.

---

**Última actualización:** Noviembre 2024
