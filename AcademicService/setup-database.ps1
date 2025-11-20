#!/usr/bin/env pwsh
# Script de configuración para AcademicService

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  AcademicService - Setup Database" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Verificar que estamos en el directorio correcto
if (-not (Test-Path "AcademicService.sln")) {
    Write-Host "Error: Debes ejecutar este script desde la carpeta raíz de AcademicService" -ForegroundColor Red
    exit 1
}

Write-Host "1. Verificando herramientas..." -ForegroundColor Yellow
dotnet --version
if ($LASTEXITCODE -ne 0) {
    Write-Host "Error: .NET SDK no está instalado" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "2. Instalando/Actualizando EF Core Tools..." -ForegroundColor Yellow
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

Write-Host ""
Write-Host "3. Restaurando paquetes..." -ForegroundColor Yellow
Set-Location "src\AcademicService.Infrastructure"
dotnet restore

Write-Host ""
Write-Host "4. Verificando migraciones existentes..." -ForegroundColor Yellow
$migrations = dotnet ef migrations list --startup-project ..\AcademicService.API 2>&1
if ($migrations -like "*No migrations were found*") {
    Write-Host "   Creando migración inicial..." -ForegroundColor Cyan
    dotnet ef migrations add InitialCreate --startup-project ..\AcademicService.API
} else {
    Write-Host "   Migraciones encontradas:" -ForegroundColor Green
    Write-Host $migrations
}

Write-Host ""
Write-Host "5. Aplicando migraciones a la base de datos..." -ForegroundColor Yellow
dotnet ef database update --startup-project ..\AcademicService.API

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "  ✓ Base de datos configurada!" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "Siguiente paso:" -ForegroundColor Cyan
    Write-Host "  cd src\AcademicService.API" -ForegroundColor White
    Write-Host "  dotnet run" -ForegroundColor White
    Write-Host ""
    Write-Host "Luego abre: https://localhost:5001" -ForegroundColor Cyan
} else {
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Red
    Write-Host "  ✗ Error en la configuración" -ForegroundColor Red
    Write-Host "========================================" -ForegroundColor Red
    Write-Host ""
    Write-Host "Revisa:" -ForegroundColor Yellow
    Write-Host "  1. SQL Server está corriendo" -ForegroundColor White
    Write-Host "  2. Cadena de conexión en appsettings.json" -ForegroundColor White
}

Set-Location ..\..
