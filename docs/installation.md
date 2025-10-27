# Installation

## Prerequisites

Before installing the driver, ensure you have:

- Windows 10 or Windows 11
- ASCOM Platform 6.6 or later ([Download here](https://ascom-standards.org/Downloads/Index.htm))
- .NET Framework 4.8 or later (usually pre-installed on Windows 10/11)
- Administrator privileges

## Download

Download the latest release from the [Releases page](https://github.com/juanjol/autoFilterWheel_ASCOM_drivers/releases).

Choose the `autoFilterWheelSetup.exe` installer.

## Installation Steps

1. **Close all astronomy software** that might be using ASCOM drivers

2. **Run the installer** (`autoFilterWheelSetup.exe`)
   - Right-click and select "Run as administrator"
   - Follow the installation wizard
   - Accept the default installation location

3. **Complete the installation**
   - The installer will:
     - Copy driver files
     - Register the ASCOM driver
     - Create shortcuts (optional)

4. **Verify installation**
   - Open ASCOM Diagnostics (included with ASCOM Platform)
   - Look for "autoFilterWheel" in the FilterWheel devices list

## Post-Installation

After installation:

1. Connect your ESP32-C3 filter wheel via USB
2. Wait for Windows to recognize the device
3. Note the COM port number (you can find it in Device Manager under "Ports (COM & LPT)")

## Updating

To update to a newer version:

1. Download the new installer
2. Run the new installer - it will automatically replace the old version
3. No need to uninstall the previous version

## Uninstallation

To remove the driver:

1. Open Windows Settings → Apps
2. Find "ASCOM autoFilterWheel Driver"
3. Click Uninstall
4. Follow the uninstallation wizard

The uninstaller will:
- Remove all driver files
- Unregister the ASCOM driver
- Remove shortcuts

## Troubleshooting Installation

### "ASCOM Platform not found"
Install ASCOM Platform 6.6 or later before installing this driver.

### "Installation failed"
- Ensure you're running the installer as administrator
- Disable antivirus temporarily
- Check Windows Event Viewer for error details

### Driver not appearing in ASCOM
- Restart your computer
- Re-run the installer
- Check ASCOM Diagnostics for registration errors
