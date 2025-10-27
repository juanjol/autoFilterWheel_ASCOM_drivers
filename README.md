# autoFilterWheel ASCOM Driver

ASCOM driver for ESP32-C3 based motorized filter wheel controller.

![Version](https://img.shields.io/badge/version-2.0.0-blue)
![Platform](https://img.shields.io/badge/platform-Windows-blue)
![ASCOM](https://img.shields.io/badge/ASCOM-6.6+-green)
![License](https://img.shields.io/badge/license-MIT-green)

## Features

- **3-9 Filter Positions**: Flexible configuration for different wheel sizes
- **Custom Filter Names**: Easy identification in imaging software
- **Custom Angles**: Support for non-equally-spaced positions
- **Position Feedback**: AS5600 magnetic encoder for accurate positioning
- **OLED Display**: Real-time status display on device
- **Motor Configuration**: Adjustable speed, acceleration, and more
- **Fast Setup**: GETCONFIG command loads all settings in one request
- **Auto-detection**: Automatically finds device on available COM ports

## Hardware

- ESP32-C3 microcontroller
- 28BYJ-48 stepper motor with ULN2003 driver
- AS5600 magnetic encoder
- SSD1306 OLED display (128x64)
- USB connection

## Installation

1. Download the latest installer from [Releases](https://github.com/juanjol/autoFilterWheel_ASCOM_drivers/releases)
2. Install ASCOM Platform 6.6+ if not already installed
3. Run `autoFilterWheelSetup.exe` as administrator
4. Connect your ESP32-C3 filter wheel via USB
5. Configure in your astronomy software

## Quick Start

1. Open your astronomy software (N.I.N.A., SGP, MaxIm DL, etc.)
2. Select "ASCOM.autoFilterWheel.FilterWheel" as your filter wheel
3. Click "Setup" and connect to the device
4. Configure filter names and settings
5. Start imaging!

## Documentation

Full documentation is available at the [project documentation site](https://juanjol.github.io/autoFilterWheel_ASCOM_drivers/).

- [Installation Guide](https://juanjol.github.io/autoFilterWheel_ASCOM_drivers/installation/)
- [Hardware Setup](https://juanjol.github.io/autoFilterWheel_ASCOM_drivers/hardware/)
- [User Guide](https://juanjol.github.io/autoFilterWheel_ASCOM_drivers/usage/)
- [Configuration](https://juanjol.github.io/autoFilterWheel_ASCOM_drivers/configuration/)
- [Troubleshooting](https://juanjol.github.io/autoFilterWheel_ASCOM_drivers/troubleshooting/)

## Compatible Software

Works with any ASCOM-compatible astronomy software:

- N.I.N.A. (Nighttime Imaging 'N' Astronomy)
- Sequence Generator Pro
- MaxIm DL
- TheSkyX
- SharpCap
- AstroPhotography Tool (APT)
- And many more...

## Requirements

### Software
- Windows 10/11
- ASCOM Platform 6.6 or later
- .NET Framework 4.8 or later

### Hardware
- ESP32-C3 with programmed firmware
- USB cable (USB-C)
- 5V power supply (1A minimum)

## What's New in v2.0.0

- **GETCONFIG Command**: Faster connection and configuration loading
- **Improved Timeouts**: Better handling of EEPROM operations
- **Status Bar**: Real-time connection status display
- **Event Handler Protection**: Prevents unwanted command sends during config load
- **Increased Motor Limits**: Support for higher speeds (up to 100,000 steps/s)
- **Complete Documentation**: Comprehensive MkDocs-based documentation
- **Bug Fixes**: Various improvements and fixes

## Building from Source

Requirements:
- Visual Studio 2019 or later
- ASCOM Platform Developer Components
- Inno Setup (for installer)

Steps:
```bash
git clone https://github.com/juanjol/autoFilterWheel_ASCOM_drivers.git
cd autoFilterWheel_ASCOM_drivers
# Open autoFilterWheel.sln in Visual Studio
# Build solution (Release configuration)
# Installer will be generated automatically
```

## Serial Protocol

The driver communicates with the ESP32 at 115200 baud using a text-based protocol:

```
Commands: #COMMAND\n
Examples:
  #GP\n          → Get position → P1
  #MP3\n         → Move to position 3 → M3
  #GETCONFIG\n   → Get all config → Multi-line response
```

Full protocol documentation in the [firmware repository](https://github.com/juanjol/ESP32_FilterWheel_Firmware).

## Troubleshooting

### Device not found
- Check USB connection
- Verify COM port in Device Manager
- Try auto-detection by clicking "Connect"

### Connection timeout
- Ensure ESP32 is powered
- Check USB cable quality
- Update ESP32 firmware

### Motor not moving
- Verify 5V power supply
- Check motor connections
- Test with firmware diagnostic mode

See [full troubleshooting guide](https://juanjol.github.io/autoFilterWheel_ASCOM_drivers/troubleshooting/) for more help.

## License

MIT License - see LICENSE file for details.

## Author

Juanjo López

## Repositories

- **Driver**: [autoFilterWheel_ASCOM_drivers](https://github.com/juanjol/autoFilterWheel_ASCOM_drivers)
- **Firmware**: [ESP32_FilterWheel_Firmware](https://github.com/juanjol/ESP32_FilterWheel_Firmware)

## Support

For issues, questions, or feature requests, please use the [GitHub Issues](https://github.com/juanjol/autoFilterWheel_ASCOM_drivers/issues) page.
