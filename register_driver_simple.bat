@echo off
echo ===============================================
echo   ASCOM ESP32 Filter Wheel Driver - Install
echo ===============================================
echo.

REM Check if running as administrator
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo ERROR: Must run as Administrator!
    echo.
    echo Right-click this file and select "Run as Administrator"
    echo.
    pause
    exit /b 1
)

echo Running as Administrator - OK
echo.

REM Go directly to the Release directory
echo Navigating to driver location...
cd /d "C:\Users\juanjo\source\repos\MotoFilterWheel\MotoFilterWheel\bin\Release"

if %errorLevel% neq 0 (
    echo ERROR: Could not navigate to Release directory
    pause
    exit /b 1
)

echo Current directory: %CD%
echo.

REM Check if executable exists
if not exist "ASCOM.juanjolMotoFilterWheel.exe" (
    echo ERROR: ASCOM.juanjolMotoFilterWheel.exe not found!
    echo.
    echo Expected location: %CD%\ASCOM.juanjolMotoFilterWheel.exe
    echo.
    echo Please make sure the project is compiled:
    echo 1. Open Visual Studio
    echo 2. Open MotoFilterWheel.sln
    echo 3. Build ^> Rebuild Solution
    echo.
    pause
    exit /b 1
)

echo Driver executable found: ASCOM.juanjolMotoFilterWheel.exe
echo File size:
dir "ASCOM.juanjolMotoFilterWheel.exe" | find "ASCOM.juanjolMotoFilterWheel.exe"
echo.

REM Register the driver
echo Registering ASCOM driver...
"ASCOM.juanjolMotoFilterWheel.exe" /regserver

if %errorLevel% equ 0 (
    echo.
    echo ============================================
    echo SUCCESS! Driver registered successfully!
    echo ============================================
    echo.
    echo The driver is now available in:
    echo - NINA: Equipment ^> Filter Wheel ^> Choose Device
    echo - ASCOM Device Hub: Filter Wheel ^> Choose
    echo - Other ASCOM applications
    echo.
    echo Look for: "ASCOM FilterWheel Driver for juanjolMotoFilterWheel"
    echo Display name: "ESP32 Filter Wheel"
    echo.
    echo Next steps:
    echo 1. Open NINA or ASCOM Device Hub
    echo 2. Go to Filter Wheel section
    echo 3. Choose Device ^> Select the ESP32 Filter Wheel driver
    echo 4. Configure the COM port where your ESP32 is connected
    echo 5. Connect and test
    echo.
    echo ============================================
) else (
    echo.
    echo ERROR: Registration failed!
    echo Error code: %errorLevel%
    echo.
    echo Possible causes:
    echo - Not running as Administrator
    echo - ASCOM Platform not installed
    echo - Antivirus blocking the registration
    echo.
    echo Please try:
    echo 1. Make sure ASCOM Platform 6.0+ is installed
    echo 2. Temporarily disable antivirus
    echo 3. Run this script as Administrator
    echo.
)

echo.
pause