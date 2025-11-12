@echo off
echo ========================================
echo  TaskService - Configuracion de BD
echo ========================================
echo.

cd /d "D:\Ing. Software II\Proyecto IS\TaskService\src\TaskService.API"
echo Directorio: %cd%
echo.

echo Verificando dotnet-ef...
dotnet ef --version >nul 2>&1
if %errorlevel% neq 0 (
    echo Instalando dotnet-ef...
    dotnet tool install --global dotnet-ef
)
echo.

echo ========================================
echo  Creando migracion...
echo ========================================
echo.

dotnet ef migrations add InitialCreate --project ..\TaskService.Infrastructure --startup-project .

if %errorlevel% equ 0 (
    echo.
    echo Migracion creada exitosamente!
    echo.
    echo ========================================
    echo  Aplicando migracion a la BD...
    echo ========================================
    echo.
    
    dotnet ef database update --project ..\TaskService.Infrastructure --startup-project .
    
    if %errorlevel% equ 0 (
        echo.
        echo ========================================
        echo  EXITO!
        echo ========================================
        echo.
        echo Tablas creadas en UserServiceDB_Dev:
        echo   - Tasks
        echo   - TaskSubmissions
        echo.
    ) else (
        echo.
        echo Error al aplicar la migracion
        echo Verifica la conexion a la base de datos
    )
) else (
    echo.
    echo Error al crear la migracion
)

echo.
pause
