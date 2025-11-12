# Script simplificado para ejecutar migraciones
# Ejecutar desde: D:\Ing. Software II\Proyecto IS\TaskService

Write-Host "========================================" -ForegroundColor Cyan
Write-Host " TaskService - Configuración de BD" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Navegar al directorio del proyecto API
$projectPath = "D:\Ing. Software II\Proyecto IS\TaskService\src\TaskService.API"
Write-Host "Navegando a: $projectPath" -ForegroundColor Yellow
Set-Location $projectPath
Write-Host ""

# Verificar dotnet-ef
Write-Host "Verificando dotnet-ef..." -ForegroundColor Cyan
$efVersion = dotnet ef --version 2>&1
if ($LASTEXITCODE -eq 0) {
    Write-Host "✓ dotnet-ef encontrado: $efVersion" -ForegroundColor Green
} else {
    Write-Host "✗ dotnet-ef no encontrado. Instalando..." -ForegroundColor Yellow
    dotnet tool install --global dotnet-ef
    Write-Host "✓ dotnet-ef instalado" -ForegroundColor Green
}
Write-Host ""

# Mostrar la cadena de conexión (sin password completo)
Write-Host "Cadena de conexión:" -ForegroundColor Cyan
Write-Host "  Servidor: controldb.cpc2m0c022b3.us-east-2.rds.amazonaws.com" -ForegroundColor White
Write-Host "  Base de datos: UserServiceDB_Dev" -ForegroundColor White
Write-Host "  Usuario: admin" -ForegroundColor White
Write-Host ""

# Crear migración
Write-Host "========================================" -ForegroundColor Cyan
Write-Host " Creando migración..." -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

dotnet ef migrations add InitialCreate --project ..\TaskService.Infrastructure --startup-project . --verbose

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✓ Migración creada exitosamente" -ForegroundColor Green
    Write-Host ""
    
    # Aplicar migración
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host " Aplicando migración a la base de datos..." -ForegroundColor Cyan
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host ""
    
    dotnet ef database update --project ..\TaskService.Infrastructure --startup-project . --verbose
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host ""
        Write-Host "========================================" -ForegroundColor Green
        Write-Host " ✓ ¡ÉXITO!" -ForegroundColor Green
        Write-Host "========================================" -ForegroundColor Green
        Write-Host ""
        Write-Host "Tablas creadas en UserServiceDB_Dev:" -ForegroundColor Cyan
        Write-Host "  • Tasks" -ForegroundColor White
        Write-Host "  • TaskSubmissions" -ForegroundColor White
        Write-Host ""
        Write-Host "Siguiente paso: Ejecutar la aplicación" -ForegroundColor Yellow
        Write-Host "  dotnet run --project src\TaskService.API" -ForegroundColor White
        Write-Host ""
    } else {
        Write-Host ""
        Write-Host "✗ Error al aplicar la migración" -ForegroundColor Red
        Write-Host "Verifica la conexión a la base de datos" -ForegroundColor Yellow
    }
} else {
    Write-Host ""
    Write-Host "✗ Error al crear la migración" -ForegroundColor Red
    Write-Host "Verifica que el proyecto compile correctamente" -ForegroundColor Yellow
}

Write-Host ""
Write-Host "Presiona cualquier tecla para continuar..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
