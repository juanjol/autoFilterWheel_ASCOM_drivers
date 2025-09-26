@echo off
REM Script para registrar el driver ASCOM autoFilterWheel
echo Installing ASCOM autoFilterWheel Driver...

REM Verificar si estamos ejecutando como administrador
net session >nul 2>&1
if %errorlevel% neq 0 (
    echo Este script necesita ejecutarse como administrador.
    echo Por favor, haz clic derecho en el archivo y selecciona "Ejecutar como administrador"
    pause
    exit /b 1
)

REM Buscar el archivo ejecutable del driver
set DRIVER_PATH=""
if exist "%~dp0bin\Release\ASCOM.autoFilterWheel.exe" (
    set DRIVER_PATH="%~dp0bin\Release\ASCOM.autoFilterWheel.exe"
    echo Encontrado driver en Release: %DRIVER_PATH%
) else if exist "%~dp0bin\Debug\ASCOM.autoFilterWheel.exe" (
    set DRIVER_PATH="%~dp0bin\Debug\ASCOM.autoFilterWheel.exe"
    echo Encontrado driver en Debug: %DRIVER_PATH%
) else (
    echo ERROR: No se encontró ASCOM.autoFilterWheel.exe
    echo Por favor, compila el proyecto primero.
    pause
    exit /b 1
)

REM Registrar el driver COM
echo Registrando componentes COM...
%DRIVER_PATH% /regserver
if %errorlevel% neq 0 (
    echo ERROR: Fallo al registrar el driver COM
    pause
    exit /b 1
)

echo.
echo ======================================
echo ASCOM autoFilterWheel Driver instalado correctamente!
echo.
echo El driver debería aparecer ahora como:
echo "ASCOM FilterWheel Driver for autoFilterWheel"
echo en el ASCOM Chooser de NINA y otras aplicaciones.
echo ======================================
echo.

pause