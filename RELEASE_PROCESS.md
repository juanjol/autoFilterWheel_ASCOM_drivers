# autoFilterWheel Release Process

This document describes how to create releases for the autoFilterWheel ASCOM driver.

## Automatic Installer Generation

The project is configured to automatically build and distribute installers through GitHub Actions.

### How it works

1. **Build and Test**: Every push to `main` or `develop` branches triggers a build test to ensure the code compiles correctly.

2. **Release Creation**: When you create a new release on GitHub, the system automatically:
   - Builds the solution in Release configuration
   - Generates an Inno Setup installer
   - Attaches the installer to the release

### Creating a Release

1. **Prepare the code**:
   - Ensure all changes are committed and pushed to `main` branch
   - Update version numbers in `AssemblyInfo.cs` if needed
   - Test the build locally

2. **Create a GitHub Release**:
   - Go to the [Releases page](../../releases) on GitHub
   - Click "Create a new release"
   - Create a new tag (e.g., `v1.0.0`, `v1.1.0`)
   - Set the target branch to `main`
   - Fill in release title and description
   - Click "Publish release"

3. **Automatic Process**:
   - GitHub Actions will automatically trigger
   - The system will build the solution
   - Generate `autoFilterWheel Setup.exe` installer
   - Attach the installer to the release

### Manual Build Trigger

You can also manually trigger the installer build without creating a release:

1. Go to the [Actions tab](../../actions)
2. Select "Build Release Installer"
3. Click "Run workflow"
4. The installer will be available as a downloadable artifact

### Files Structure

```
autoFilterWheel/
├── autoFilterWheel Setup.iss       # Inno Setup script
├── bin/Release/
│   └── ASCOM.autoFilterWheel.exe   # Built driver executable
├── ReadMe.htm                       # Driver documentation
└── .github/workflows/
    ├── release-installer.yml        # Release automation
    └── build-test.yml               # Build testing
```

### Installer Features

The generated installer includes:
- ✅ ASCOM driver registration
- ✅ Automatic uninstall of previous versions
- ✅ ASCOM Platform version validation (6.2+)
- ✅ DCOM configuration for TheSky compatibility
- ✅ Driver documentation (ReadMe.htm)

### Troubleshooting

**Build fails**: Check that all dependencies are correctly referenced in the project file.

**Installer not generated**: Verify that the Inno Setup script paths are correct and the build output exists.

**Registration issues**: Ensure the CLSID and ProgID in the installer match those in the driver code.

### Version Information

- **Driver CLSID**: `e9752f76-629c-4e84-a248-3262f3da0e1d`
- **ProgID**: `ASCOM.autoFilterWheel.FilterWheel`
- **Display Name**: `autoFilterWheel`
- **Minimum ASCOM Platform**: 6.2