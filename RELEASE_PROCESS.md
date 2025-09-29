# autoFilterWheel Release Process

This document describes how to create releases for the autoFilterWheel ASCOM driver.

## Automated Installer Generation

This project uses an **upload driver + auto-generate installer** approach that automatically creates the setup from your compiled driver.

### How It Works

✅ **Build locally** with full ASCOM Platform access
✅ **Upload only the driver executable** to GitHub releases
✅ **Automatic installer generation** via GitHub Actions
✅ **Clean repository** - no binary files committed to git

### Step-by-Step Process

#### 1. Prepare for Release
```bash
# Ensure you're on the main branch with latest changes
git checkout main
git pull origin main

# Update version numbers if needed in:
# - autoFilterWheel/Properties/AssemblyInfo.cs
# - autoFilterWheel Setup.iss (AppVersion)
```

#### 2. Build Locally
```bash
# Build the solution in Release mode
msbuild autoFilterWheel.sln /p:Configuration=Release /p:Platform="Any CPU"

# Verify the build output exists
# Should create: autoFilterWheel/bin/Release/ASCOM.autoFilterWheel.exe
```

#### 3. Create GitHub Release
```bash
# Create and push a tag
git tag v1.0.1
git push origin v1.0.1

# Or create release directly on GitHub web interface
```

#### 4. Upload Driver Executable
- Go to the release page on GitHub
- Click "Edit release"
- Drag and drop **only** `ASCOM.autoFilterWheel.exe` to attach it as a release asset
- Click "Publish release"

#### 5. Automatic Installer Generation
- GitHub Actions will automatically detect the uploaded driver
- It downloads the driver executable from the release
- Generates the installer using Inno Setup
- Uploads `autoFilterWheelSetup.exe` back to the same release
- Adds a note about the generated installer to the release description

### What Gets Generated

The automated process creates:
- ✅ `autoFilterWheelSetup.exe` - Complete installer with ASCOM registration
- ✅ Automatic uninstall of previous versions
- ✅ ASCOM Platform version validation (6.2+)
- ✅ DCOM configuration for TheSky compatibility
- ✅ Driver documentation (ReadMe.htm)

### Troubleshooting

#### Build Fails Locally
- Ensure ASCOM Platform 6.6+ is installed on your machine
- Check that all NuGet packages are restored
- Verify .NET Framework 4.7.2 is available
- Run `msbuild autoFilterWheel.sln /p:Configuration=Release` to see detailed errors

#### GitHub Actions Fails
- **Driver not found**: Make sure you uploaded `ASCOM.autoFilterWheel.exe` (exact filename)
- **Installer generation fails**: Check the Actions log for Inno Setup errors
- **Upload fails**: Verify the workflow has proper GitHub token permissions

#### Wrong File Uploaded
If you uploaded the wrong file:
1. Go to the release page
2. Delete the incorrect asset
3. Upload the correct `ASCOM.autoFilterWheel.exe` file
4. The workflow will automatically retry

#### Multiple Drivers
- Only upload `ASCOM.autoFilterWheel.exe` - don't upload dependencies
- The installer script handles all required files and registration

### Version Information

- **Driver CLSID**: `e9752f76-629c-4e84-a248-3262f3da0e1d`
- **ProgID**: `ASCOM.autoFilterWheel.FilterWheel`
- **Display Name**: `autoFilterWheel`
- **Minimum ASCOM Platform**: 6.2