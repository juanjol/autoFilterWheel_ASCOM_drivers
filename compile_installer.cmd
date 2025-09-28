@echo off
echo Compiling autoFilterWheel installer...
"C:\Program Files\Inno Setup 6\ISCC.exe" "AutoFilterWheel_Setup.iss"
echo Compilation finished.
if exist "%USERPROFILE%\Desktop\autoFilterWheel_Setup_v6.6.0.exe" (
    echo SUCCESS: Installer created on desktop!
    dir "%USERPROFILE%\Desktop\autoFilterWheel_Setup_v*.exe"
) else (
    echo ERROR: Installer not found on desktop.
)
pause