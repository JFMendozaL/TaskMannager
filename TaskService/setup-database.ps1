# Script para configurar la base de datos de TaskService
# PowerShell Script

Write-Host "🚀 Iniciando configuración de base de datos para TaskService..." -ForegroundColor Cyan
Write-Host ""

# Navegar al directorio del proyecto API
$projectPath = "D:\Ing. Software II\Proyecto IS\TaskService\src\TaskService.API"
Set-Location $projectPath
Write-Host "📂 Directorio actual: $projectPath" -ForegroundColor Yellow
Write-Host ""

# Verificar si dotnet-ef está instalado
Write-Host "🔍 Verificando herramientas de Entity Framework..." -ForegroundColor Cyan
$efInstalled = dotnet tool list -g | Select-String "dotnet-ef"

if (-not $efInstalled) {
    Write-Host "⚙️  Instalando dotnet-ef..." -ForegroundColor Yellow
    dotnet tool install --global dotnet-ef
    Write-Host "✅ dotnet-ef instalado correctamente" -ForegroundColor Green
    Write-Host ""
} else {
    Write-Host "✅ dotnet-ef ya está instalado" -ForegroundColor Green
    Write-Host ""
}

# Eliminar migraciones anteriores (si existen)
$migrationsPath = "..\TaskService.Infrastructure\Migrations"
if (Test-Path $migrationsPath) {
    Write-Host "🗑️  Eliminando migraciones anteriores..." -ForegroundColor Yellow
    Remove-Item -Path $migrationsPath -Recurse -Force
    Write-Host "✅ Migraciones anteriores eliminadas" -ForegroundColor Green
    Write-Host ""
}

# Crear nueva migración
Write-Host "📝 Creando migración inicial..." -ForegroundColor Cyan
dotnet ef migrations add InitialCreate --project ..\TaskService.Infrastructure --startup-project . --verbose

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Migración creada correctamente" -ForegroundColor Green
    Write-Host ""
} else {
    Write-Host "❌ Error al crear la migración" -ForegroundColor Red
    Write-Host "Por favor revisa los errores anteriores" -ForegroundColor Red
    exit 1
}

# Aplicar migración a la base de datos
Write-Host "🔄 Aplicando migración a la base de datos..." -ForegroundColor Cyan
Write-Host "⏳ Esto puede tomar unos momentos..." -ForegroundColor Yellow
Write-Host ""

dotnet ef database update --project ..\TaskService.Infrastructure --startup-project . --verbose

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✅ ¡Base de datos configurada exitosamente!" -ForegroundColor Green
    Write-Host ""
    Write-Host "📊 Tablas creadas:" -ForegroundColor Cyan
    Write-Host "  - Tasks" -ForegroundColor White
    Write-Host "  - TaskSubmissions" -ForegroundColor White
    Write-Host ""
    Write-Host "🎉 ¡Todo listo! Puedes ejecutar tu aplicación." -ForegroundColor Green
} else {
    Write-Host ""
    Write-Host "❌ Error al aplicar la migración" -ForegroundColor Red
    Write-Host "Verifica:" -ForegroundColor Yellow
    Write-Host "  1. La conexión a la base de datos en appsettings.json" -ForegroundColor Yellow
    Write-Host "  2. Que el servidor SQL esté accesible" -ForegroundColor Yellow
    Write-Host "  3. Las credenciales de conexión" -ForegroundColor Yellow
    exit 1
}

Write-Host ""
Write-Host "💡 Comandos útiles:" -ForegroundColor Cyan
Write-Host "  - Ver migraciones: dotnet ef migrations list --project ..\TaskService.Infrastructure --startup-project ." -ForegroundColor White
Write-Host "  - Revertir: dotnet ef database update 0 --project ..\TaskService.Infrastructure --startup-project ." -ForegroundColor White
Write-Host "  - Nueva migración: dotnet ef migrations add [Nombre] --project ..\TaskService.Infrastructure --startup-project ." -ForegroundColor White
