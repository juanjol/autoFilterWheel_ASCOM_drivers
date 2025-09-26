@echo off
echo Installing ASCOM Filter Wheel Driver for ESP32-C3...
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

REM Get the current directory
set "SCRIPT_DIR=%~dp0"
echo Script directory: %SCRIPT_DIR%

REM Build the project first
echo Building the driver...
if exist "%SCRIPT_DIR%MotoFilterWheel.sln" (
    msbuild "%SCRIPT_DIR%MotoFilterWheel.sln" /p:Configuration=Release /p:Platform="Any CPU" /v:minimal
    if %errorLevel% neq 0 (
        echo ERROR: Build failed!
        pause
        exit /b 1
    )
    echo Build successful!
) else (
    echo WARNING: Solution file not found. Make sure the driver is already built.
)

echo.
echo Registering ASCOM driver...

REM Change to the output directory using full path
cd /d "%SCRIPT_DIR%MotoFilterWheel\bin\Release"

REM Check if the executable exists
if not exist "ASCOM.juanjolMotoFilterWheel.exe" (
    echo ERROR: ASCOM.juanjolMotoFilterWheel.exe not found!
    echo Make sure the project compiled successfully.
    pause
    exit /b 1
)

REM Register the driver
echo Registering COM server...
"ASCOM.juanjolMotoFilterWheel.exe" /regserver

if %errorLevel% == 0 (
    echo.
    echo ======================================
    echo SUCCESS! Driver registered successfully.
    echo.
    echo You can now use the driver in:
    echo - NINA
    echo - SGP (Sequence Generator Pro)
    echo - ASCOM Device Hub
    echo - Other ASCOM-compatible software
    echo.
    echo Look for: "ASCOM FilterWheel Driver for juanjolMotoFilterWheel"
    echo or "ESP32 Filter Wheel"
    echo ======================================
) else (
    echo ERROR: Registration failed!
    echo Make sure you're running as Administrator.
)

echo.
pause