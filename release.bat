@echo off
echo.
echo =====================================
echo   autoFilterWheel Release Tool
echo =====================================
echo.

:: Check if gh CLI is installed
gh --version >nul 2>&1
if errorlevel 1 (
    echo ERROR: GitHub CLI not found!
    echo        Install it from: https://cli.github.com/
    pause
    exit /b 1
)

:: Get version input
set /p version="Enter version (e.g., 1.0.1): "
if "%version%"=="" (
    echo ERROR: Version is required!
    pause
    exit /b 1
)

echo.
echo Building project...
echo.

:: Build the solution
msbuild autoFilterWheel.sln /p:Configuration=Release /p:Platform="Any CPU"
if errorlevel 1 (
    echo.
    echo ERROR: Build failed!
    echo        Fix the build errors and try again.
    pause
    exit /b 1
)

:: Check if the exe was created
if not exist "autoFilterWheel\bin\Release\ASCOM.autoFilterWheel.exe" (
    echo.
    echo ERROR: Driver executable not found!
    echo        Expected: autoFilterWheel\bin\Release\ASCOM.autoFilterWheel.exe
    pause
    exit /b 1
)

echo.
echo Build successful!
echo.

:: Get file size for confirmation
for %%F in ("autoFilterWheel\bin\Release\ASCOM.autoFilterWheel.exe") do (
    echo Driver size: %%~zF bytes
)

echo.
echo Creating GitHub release v%version%...
echo.

:: Create the release
gh release create v%version% --title "Release v%version%" --notes "## Release v%version%

**autoFilterWheel ASCOM Driver Release**

### What's Included
- ASCOM.autoFilterWheel.exe - Driver executable
- autoFilterWheelSetup.exe - Complete installer (generated automatically)

### Installation
Download and run autoFilterWheelSetup.exe for automatic installation and ASCOM registration.

### Requirements
- ASCOM Platform 6.2 or later
- Windows 7 SP1 or later
- .NET Framework 4.7.2

---
*Installer generated automatically by GitHub Actions*"

if errorlevel 1 (
    echo.
    echo ERROR: Failed to create release!
    echo        Make sure you're logged in: gh auth login
    pause
    exit /b 1
)

echo.
echo Release created successfully!
echo.
echo Uploading driver executable...
echo.

:: Upload the driver
gh release upload v%version% "autoFilterWheel\bin\Release\ASCOM.autoFilterWheel.exe"
if errorlevel 1 (
    echo.
    echo ERROR: Failed to upload driver!
    pause
    exit /b 1
)

echo.
echo Driver uploaded successfully!
echo.
echo GitHub Actions will now:
echo   1. Detect the uploaded driver
echo   2. Generate the installer automatically
echo   3. Add it to the same release
echo.
echo Release complete! Check: https://github.com/%GITHUB_REPOSITORY%/releases/tag/v%version%
echo.
echo Press any key to exit...
pause >nul