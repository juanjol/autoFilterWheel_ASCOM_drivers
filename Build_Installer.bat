@echo off
echo ========================================
echo  autoFilterWheel Installer Builder
echo ========================================
echo.

:: Check if Inno Setup is installed
set "ISCC_PATH="
if exist "C:\Program Files\Inno Setup 6\ISCC.exe" (
    set "ISCC_PATH=C:\Program Files\Inno Setup 6\ISCC.exe"
) else if exist "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" (
    set "ISCC_PATH=C:\Program Files (x86)\Inno Setup 6\ISCC.exe"
) else if exist "C:\Program Files\Inno Setup 5\ISCC.exe" (
    set "ISCC_PATH=C:\Program Files\Inno Setup 5\ISCC.exe"
) else if exist "C:\Program Files (x86)\Inno Setup 5\ISCC.exe" (
    set "ISCC_PATH=C:\Program Files (x86)\Inno Setup 5\ISCC.exe"
)

if "%ISCC_PATH%"=="" (
    echo ERROR: Inno Setup not found!
    echo.
    echo Please install Inno Setup from:
    echo https://jrsoftware.org/isdl.php
    echo.
    echo After installation, run this script again.
    pause
    exit /b 1
)

echo Found Inno Setup at: %ISCC_PATH%
echo.

:: Create Installer directory if it doesn't exist
if not exist "Installer" mkdir Installer

:: Build the project in Release mode first
echo Building project in Release mode...
"%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" MotoFilterWheel.sln -p:Configuration=Release
if errorlevel 1 (
    echo ERROR: Build failed!
    pause
    exit /b 1
)

echo Build successful!
echo.

:: Compile the installer
echo Compiling installer...
"%ISCC_PATH%" AutoFilterWheel_Setup.iss
if errorlevel 1 (
    echo ERROR: Installer compilation failed!
    pause
    exit /b 1
)

echo.
echo ========================================
echo  INSTALLER BUILD COMPLETED SUCCESSFULLY!
echo ========================================
echo.
echo The installer has been created in the Installer folder:
dir /b Installer\*.exe
echo.
echo You can now distribute this installer to install the
echo autoFilterWheel ASCOM driver on other computers.
echo.
pause