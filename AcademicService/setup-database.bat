@echo off
echo ================================
echo AcademicService - Database Setup
echo ================================
echo.

cd src\AcademicService.Infrastructure

echo Creating database migration...
dotnet ef migrations add InitialCreate --startup-project ../AcademicService.API
if errorlevel 1 (
    echo Error creating migration
    pause
    exit /b 1
)

echo.
echo Applying migration to database...
dotnet ef database update --startup-project ../AcademicService.API
if errorlevel 1 (
    echo Error applying migration
    pause
    exit /b 1
)

echo.
echo ================================
echo Database setup completed!
echo ================================
pause
