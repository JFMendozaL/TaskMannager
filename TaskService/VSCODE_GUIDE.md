# 🎯 Guía Visual - VS Code

## 📂 Paso 1: Abrir el Proyecto

1. Abre **Visual Studio Code**
2. Presiona `Ctrl + K` luego `Ctrl + O`
3. Navega a: `D:\Ing. Software II\Proyecto IS\TaskService`
4. Click en **"Seleccionar carpeta"**

---

## 🖥️ Paso 2: Abrir Terminal

### Opción A: Atajo de Teclado
Presiona: `Ctrl + Ñ` o `Ctrl + '`

### Opción B: Menú
1. Click en **"View"** (Vista) en el menú superior
2. Click en **"Terminal"**

O también:
1. Click en **"Terminal"** en el menú superior
2. Click en **"New Terminal"**

---

## ⚙️ Paso 3: Verificar el Tipo de Terminal

En la esquina superior derecha de la terminal, verás el tipo:
- 🔵 **PowerShell** (Recomendado)
- 🟢 **CMD**
- 🟠 **Git Bash**

Para cambiar el tipo de terminal:
1. Click en la **flecha hacia abajo** al lado del **"+"**
2. Selecciona el tipo que prefieras

---

## 🚀 Paso 4: Ejecutar la Migración

### Si estás en PowerShell (🔵):
```powershell
.\run-migration.ps1
```

### Si estás en CMD (🟢):
```cmd
run-migration.bat
```

### Si estás en Git Bash (🟠):
```bash
powershell -File run-migration.ps1
```

---

## ❗ Si Aparece Error de Permisos (PowerShell)

Si ves: `"no se puede cargar porque la ejecución de scripts está deshabilitada"`

**Solución:**
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser -Force
```

Luego vuelve a ejecutar:
```powershell
.\run-migration.ps1
```

---

## 🎨 Paso 5: ALTERNATIVA - Usar Tasks (Más Elegante)

He creado tareas personalizadas para VS Code:

### Ejecutar una Tarea:
1. Presiona `Ctrl + Shift + P`
2. Escribe: `Tasks: Run Task`
3. Selecciona: **"🚀 Aplicar Migraciones"**

### Lista de Tareas Disponibles:
- 🗄️ **Configurar Base de Datos** - Crea las migraciones
- 🚀 **Aplicar Migraciones** - Aplica cambios a la BD
- 🏃 **Ejecutar Aplicación** - Corre el proyecto
- 📦 **Restaurar Paquetes** - Restaura dependencias
- 🧹 **Limpiar Proyecto** - Limpia archivos compilados
- 🔨 **Compilar Proyecto** - Compila el código

---

## 🐛 Paso 6: Ejecutar y Debuggear

### Para Ejecutar (Sin Debug):
1. Presiona `Ctrl + F5`
2. O click en **"Run"** → **"Run Without Debugging"**

### Para Debuggear:
1. Presiona `F5`
2. O click en **"Run"** → **"Start Debugging"**
3. Se abrirá automáticamente el navegador en Swagger

---

## ✅ Verificar que Todo Funcionó

Deberías ver en la terminal:

```
✓ Migración creada exitosamente
✓ ÉXITO!
Tablas creadas en UserServiceDB_Dev:
  • Tasks
  • TaskSubmissions
```

---

## 📋 Comandos Manuales Alternativos

Si prefieres ejecutar paso a paso:

### 1. Navegar al directorio API:
```powershell
cd src\TaskService.API
```

### 2. Crear migración:
```powershell
dotnet ef migrations add InitialCreate --project ..\TaskService.Infrastructure --startup-project .
```

### 3. Aplicar a la BD:
```powershell
dotnet ef database update --project ..\TaskService.Infrastructure --startup-project .
```

### 4. Ejecutar la aplicación:
```powershell
dotnet run
```

---

## 🎯 Atajos de Teclado Útiles

| Atajo | Acción |
|-------|--------|
| `Ctrl + Ñ` | Abrir/Cerrar Terminal |
| `Ctrl + Shift + P` | Paleta de Comandos |
| `Ctrl + Shift + B` | Ejecutar Tarea de Build |
| `F5` | Iniciar Debug |
| `Ctrl + F5` | Ejecutar sin Debug |
| `Ctrl + C` | Detener proceso en terminal |

---

## 🔧 Tips Adicionales

### Ver Archivos de Migración Creados:
En el explorador de VS Code:
```
src/
└── TaskService.Infrastructure/
    └── Migrations/
        ├── 20241111XXXXXX_InitialCreate.cs
        ├── 20241111XXXXXX_InitialCreate.Designer.cs
        └── TaskDbContextModelSnapshot.cs
```

### Múltiples Terminales:
- Click en el **"+"** para abrir una nueva terminal
- Usa `Ctrl + Tab` para cambiar entre terminales

### Ver Output de Entity Framework:
1. Click en **"View"** → **"Output"**
2. En el dropdown, selecciona **"Entity Framework Core"**

---

## 🎬 Resumen Visual

```
┌─────────────────────────────────────────┐
│  Visual Studio Code                     │
├─────────────────────────────────────────┤
│  📁 EXPLORER        🔍 SEARCH           │
│  └── TaskService/                       │
│      ├── src/                           │
│      ├── run-migration.ps1  ← Script    │
│      └── README.md                      │
├─────────────────────────────────────────┤
│  📝 [Editor de Código]                  │
│                                         │
├─────────────────────────────────────────┤
│  💻 TERMINAL                            │
│  PS D:\...\TaskService> .\run-migration.ps1
│  ✓ Migración creada exitosamente       │
│  ✓ ÉXITO!                               │
└─────────────────────────────────────────┘
```

---

**🎉 ¡Listo! Ahora ejecuta el script siguiendo cualquiera de los métodos de arriba.**
