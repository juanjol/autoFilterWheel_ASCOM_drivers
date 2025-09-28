# autoFilterWheel Installer Build Instructions

## Prerequisites

### 1. Install Inno Setup
Download and install Inno Setup (free):
- **Download from:** https://jrsoftware.org/isdl.php
- **Version:** Inno Setup 6.x or later recommended
- **Installation:** Use default settings, install to Program Files

### 2. Verify Project Build
Ensure the project builds successfully in Release mode:
```batch
"C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" MotoFilterWheel.sln -p:Configuration=Release
```

## Building the Installer

### Option 1: Automated Build (Recommended)
1. **Run the batch script:**
   ```batch
   Build_Installer.bat
   ```

   This script will:
   - Check for Inno Setup installation
   - Build the project in Release mode
   - Compile the installer
   - Report success/failure

### Option 2: Manual Build
1. **Open Inno Setup Compiler**
2. **Open script:** `AutoFilterWheel_Setup.iss`
3. **Compile:** Build → Compile (or press F9)

## Output

The installer will be created as:
```
Installer/autoFilterWheel_Setup_v6.6.0.exe
```

## Installer Features

### What the installer does:
- ✅ Checks for ASCOM Platform 6 installation
- ✅ Installs driver files to `Program Files\ASCOM\FilterWheel`
- ✅ Registers the driver with Windows COM system
- ✅ Registers the driver with ASCOM Chooser
- ✅ Creates Start Menu shortcuts
- ✅ Provides clean uninstallation

### Installation Requirements:
- **ASCOM Platform 6.0+** (installer will warn if missing)
- **Windows 7/8/10/11**
- **.NET Framework 4.7.2+**
- **Administrator privileges** (required for COM registration)

## Distribution

The generated `.exe` file is completely self-contained and can be distributed to end users. Users simply need to:

1. **Download** the installer
2. **Run as Administrator** (right-click → "Run as administrator")
3. **Follow the installation wizard**
4. **Test** using ASCOM Chooser

## Testing the Installer

### Before distribution:
1. **Test on clean VM** or different computer
2. **Verify driver appears** in ASCOM FilterWheel Chooser
3. **Test connection** to hardware
4. **Test uninstallation** process

### Verification steps:
```batch
# Check COM registration
reg query "HKLM\SOFTWARE\Classes\ASCOM.autoFilterWheel.FilterWheel"

# Check ASCOM registration
reg query "HKLM\SOFTWARE\ASCOM\FilterWheel Drivers\ASCOM.autoFilterWheel.FilterWheel"
```

## Troubleshooting

### Common Issues:

**"Inno Setup not found"**
- Install Inno Setup from the official website
- Restart command prompt after installation

**"Build failed"**
- Ensure Visual Studio 2022 is installed
- Check that all source files are present
- Verify .NET Framework 4.7.2 is installed

**"Access denied during compilation"**
- Run command prompt as Administrator
- Ensure antivirus is not blocking file creation

**"Driver not appearing in ASCOM Chooser"**
- Ensure installer was run as Administrator
- Check Windows Event Log for COM registration errors
- Manually register: `ASCOM.autoFilterWheel.exe /regserver`

## Customization

### To customize the installer:
1. **Edit `AutoFilterWheel_Setup.iss`**
2. **Update these values:**
   - `MyAppPublisher` - Your name/company
   - `MyAppURL` - Your website/GitHub
   - `AppId` - Generate new GUID if creating derivative
3. **Recompile**

### Generate new GUID:
- Visual Studio: Tools → Create GUID
- PowerShell: `[System.Guid]::NewGuid()`
- Online: https://www.guidgenerator.com/

---

*For technical support, visit: [GitHub Repository](https://github.com/yourusername/autoFilterWheel)*