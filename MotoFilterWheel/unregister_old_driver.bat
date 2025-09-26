@echo off
REM Script para desregistrar el driver ASCOM juanjolMotoFilterWheel (nombre antiguo)
echo Unregistering old ASCOM juanjolMotoFilterWheel Driver...

REM Verificar si estamos ejecutando como administrador
net session >nul 2>&1
if %errorlevel% neq 0 (
    echo Este script necesita ejecutarse como administrador.
    echo Por favor, haz clic derecho en el archivo y selecciona "Ejecutar como administrador"
    pause
    exit /b 1
)

REM Buscar el archivo ejecutable del driver antiguo
set OLD_DRIVER_PATH=""
if exist "%~dp0bin\Release\ASCOM.juanjolMotoFilterWheel.exe" (
    set OLD_DRIVER_PATH="%~dp0bin\Release\ASCOM.juanjolMotoFilterWheel.exe"
    echo Encontrado driver antiguo en Release: %OLD_DRIVER_PATH%
) else if exist "%~dp0bin\Debug\ASCOM.juanjolMotoFilterWheel.exe" (
    set OLD_DRIVER_PATH="%~dp0bin\Debug\ASCOM.juanjolMotoFilterWheel.exe"
    echo Encontrado driver antiguo en Debug: %OLD_DRIVER_PATH%
) else (
    echo No se encontró el archivo del driver antiguo.
    echo Intentando desregistrar usando regsvr32 directamente...
)

REM Desregistrar el driver COM antiguo si existe el archivo
if not %OLD_DRIVER_PATH%=="" (
    echo Desregistrando driver antiguo COM...
    %OLD_DRIVER_PATH% /unregserver
    if %errorlevel% neq 0 (
        echo ADVERTENCIA: Fallo al desregistrar el driver antiguo COM
    ) else (
        echo Driver antiguo COM desregistrado exitosamente.
    )
)

REM Limpiar registros COM manualmente si es necesario
echo Limpiando registros COM del driver antiguo...
reg delete "HKEY_CLASSES_ROOT\ASCOM.juanjolMotoFilterWheel.FilterWheel" /f 2>nul
reg delete "HKEY_CLASSES_ROOT\CLSID\{e9752f76-629c-4e84-a248-3262f3da0e1d}" /f 2>nul

echo.
echo ======================================
echo Driver antiguo desregistrado.
echo Ahora puedes registrar el nuevo driver con:
echo register_autoFilterWheel.bat
echo ======================================
echo.

pause