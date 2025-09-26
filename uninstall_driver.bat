@echo off
echo Uninstalling ASCOM Filter Wheel Driver for ESP32-C3...
echo.

REM Check if running as administrator
net session >nul 2>&1
if %errorLevel% == 0 (
    echo Running as Administrator - OK
) else (
    echo ERROR: This script must be run as Administrator!
    echo Right-click and select "Run as Administrator"
    pause
    exit /b 1
)

echo Unregistering ASCOM driver...

REM Change to the output directory
cd "MotoFilterWheel\bin\Release"

REM Check if the executable exists
if not exist "ASCOM.juanjolMotoFilterWheel.exe" (
    echo WARNING: ASCOM.juanjolMotoFilterWheel.exe not found!
    echo Driver may not be installed or files were moved.
)

REM Unregister the driver
echo Unregistering COM server...
"ASCOM.juanjolMotoFilterWheel.exe" /unregserver

if %errorLevel__ == 0 (
    echo.
    echo ======================================
    echo SUCCESS! Driver unregistered successfully.
    echo The driver has been removed from the system.
    echo ======================================
) else (
    echo Note: Unregistration completed (errors may be normal if driver wasn't registered)
)

echo.
pause