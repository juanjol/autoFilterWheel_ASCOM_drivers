@echo off
echo ===============================================
echo   ESP32 Filter Wheel - Serial Connection Test
echo ===============================================
echo.

echo This script will test if COM15 is accessible.
echo.

REM Test if COM15 exists using mode command
echo Testing COM15 accessibility...
mode COM15 baud=115200 parity=n data=8 stop=1 >nul 2>&1

if %errorLevel% equ 0 (
    echo SUCCESS: COM15 is accessible and can be configured.
    echo.
    echo The port exists and is not blocked by Windows.
    echo The issue might be:
    echo 1. Another program is using the port
    echo 2. ASCOM Serial class configuration issue
    echo 3. Permission or driver issue
) else (
    echo FAILED: COM15 is not accessible.
    echo.
    echo Possible causes:
    echo 1. ESP32 not connected or not powered
    echo 2. Wrong COM port number
    echo 3. USB cable issue
    echo 4. Windows driver not installed
    echo.
    echo Check Device Manager:
    echo - Ports (COM & LPT) section
    echo - Look for "USB Serial Port" or "CP210x" or "CH340"
    echo - Note the actual COM port number
)

echo.
echo Checking what devices are using COM ports...
echo.

REM List all serial devices
wmic path win32_pnpentity where "caption like '%(COM%%'" get caption,name,deviceid /format:table

echo.
echo ===============================================
echo.
pause