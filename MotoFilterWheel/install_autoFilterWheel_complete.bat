@echo off
REM Script completo para instalar ASCOM autoFilterWheel Driver
echo ======================================
echo ASCOM autoFilterWheel Driver - Instalacion Completa
echo ======================================

REM Verificar si estamos ejecutando como administrador
net session >nul 2>&1
if %errorlevel% neq 0 (
    echo.
    echo ERROR: Este script necesita ejecutarse como administrador.
    echo Por favor:
    echo 1. Haz clic derecho en este archivo
    echo 2. Selecciona "Ejecutar como administrador"
    echo.
    pause
    exit /b 1
)

echo Paso 1: Compilando el proyecto...
echo.

REM Compilar el proyecto usando MSBuild
set MSBUILD_PATH=""
for %%i in ("C:\Program Files (x86)\Microsoft Visual Studio\2019\*\MSBuild\Current\Bin\MSBuild.exe") do if exist "%%i" set MSBUILD_PATH="%%i"
for %%i in ("C:\Program Files (x86)\Microsoft Visual Studio\2022\*\MSBuild\Current\Bin\MSBuild.exe") do if exist "%%i" set MSBUILD_PATH="%%i"
for %%i in ("C:\Program Files\Microsoft Visual Studio\2022\*\MSBuild\Current\Bin\MSBuild.exe") do if exist "%%i" set MSBUILD_PATH="%%i"

if %MSBUILD_PATH%=="" (
    echo No se encontró MSBuild. Intentando con dotnet build...
    dotnet build "%~dp0MotoFilterWheel.csproj" --configuration Release
) else (
    echo Usando MSBuild: %MSBUILD_PATH%
    %MSBUILD_PATH% "%~dp0MotoFilterWheel.csproj" /p:Configuration=Release
)

if %errorlevel% neq 0 (
    echo.
    echo ERROR: Fallo la compilacion del proyecto.
    echo Por favor, compila manualmente en Visual Studio primero.
    pause
    exit /b 1
)

echo.
echo Paso 2: Desregistrando driver antiguo (si existe)...

REM Desregistrar driver antiguo
if exist "%~dp0bin\Release\ASCOM.juanjolMotoFilterWheel.exe" (
    echo Desregistrando driver antiguo...
    "%~dp0bin\Release\ASCOM.juanjolMotoFilterWheel.exe" /unregserver 2>nul
)

REM Limpiar registros antiguos
reg delete "HKEY_CLASSES_ROOT\ASCOM.juanjolMotoFilterWheel.FilterWheel" /f 2>nul
reg delete "HKEY_CLASSES_ROOT\CLSID\{e9752f76-629c-4e84-a248-3262f3da0e1d}" /f 2>nul

echo.
echo Paso 3: Registrando nuevo driver autoFilterWheel...

REM Verificar que existe el nuevo driver
if not exist "%~dp0bin\Release\ASCOM.autoFilterWheel.exe" (
    echo ERROR: No se encontró ASCOM.autoFilterWheel.exe
    echo El archivo debería estar en: %~dp0bin\Release\
    pause
    exit /b 1
)

REM Registrar el nuevo driver
echo Registrando ASCOM.autoFilterWheel.exe...
"%~dp0bin\Release\ASCOM.autoFilterWheel.exe" /regserver

if %errorlevel% neq 0 (
    echo ERROR: Fallo al registrar el nuevo driver COM
    echo Verifica que el archivo se compiló correctamente.
    pause
    exit /b 1
)

echo.
echo ======================================
echo ¡INSTALACION COMPLETADA EXITOSAMENTE!
echo ======================================
echo.
echo El driver ASCOM autoFilterWheel ha sido instalado.
echo.
echo Deberías poder encontrarlo en ASCOM Chooser como:
echo "ASCOM FilterWheel Driver for autoFilterWheel"
echo.
echo Archivo del driver: %~dp0bin\Release\ASCOM.autoFilterWheel.exe
echo ProgID: ASCOM.autoFilterWheel.FilterWheel
echo.
echo Puedes probarlo ahora en NINA u otra aplicación ASCOM.
echo ======================================

pause