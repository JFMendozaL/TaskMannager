@echo off
echo ============================================
echo  Configuracion de Base de Datos - TaskService
echo ============================================
echo.

cd /d "D:\Ing. Software II\Proyecto IS\TaskService\src\TaskService.API"
echo Directorio actual: %cd%
echo.

echo Verificando herramientas de Entity Framework...
dotnet tool list -g | findstr "dotnet-ef" >nul
if %errorlevel% neq 0 (
    echo Instalando dotnet-ef...
    dotnet tool install --global dotnet-ef
    echo.
)

echo.
echo Creando migracion inicial...
dotnet ef migrations add InitialCreate --project ..\TaskService.Infrastructure --startup-project . --verbose

if %errorlevel% neq 0 (
    echo.
    echo ERROR: No se pudo crear la migracion
    pause
    exit /b 1
)

echo.
echo Aplicando migracion a la base de datos...
echo Esto puede tomar unos momentos...
echo.

dotnet ef database update --project ..\TaskService.Infrastructure --startup-project . --verbose

if %errorlevel% equ 0 (
    echo.
    echo ============================================
    echo  BASE DE DATOS CONFIGURADA EXITOSAMENTE!
    echo ============================================
    echo.
    echo Tablas creadas:
    echo   - Tasks
    echo   - TaskSubmissions
    echo.
    echo Todo listo! Puedes ejecutar tu aplicacion.
) else (
    echo.
    echo ERROR: No se pudo aplicar la migracion
    echo.
    echo Verifica:
    echo   1. La conexion en appsettings.json
    echo   2. Que el servidor SQL este accesible
    echo   3. Las credenciales de conexion
)

echo.
pause
