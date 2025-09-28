# autoFilterWheel ASCOM Driver

[![Build Status](https://github.com/juanjol/autoFilterWheel_ASCOM_drivers/actions/workflows/build-installer.yml/badge.svg)](https://github.com/juanjol/autoFilterWheel_ASCOM_drivers/actions)
[![ASCOM Platform](https://img.shields.io/badge/ASCOM-6.0+-blue.svg)](https://ascom-standards.org/)
[![License](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE)

ASCOM driver for ESP32-C3 based automatic filter wheel controller with advanced motor control capabilities.

## Features

- **ASCOM 6.0+ Compatible**: Full compliance with ASCOM FilterWheel interface standards
- **Advanced Motor Control**:
  - Configurable speed, acceleration, and steps per revolution
  - Bidirectional and unidirectional operation modes
  - Revolution and backlash calibration
- **Multi-Filter Support**: Supports 3-8 filter positions
- **User-Friendly Interface**: Comprehensive setup dialog with real-time device monitoring
- **Robust Communication**: Reliable serial protocol with error handling

## Installation

### Requirements

- Windows 7/8/10/11
- .NET Framework 4.7.2 or higher
- ASCOM Platform 6.0 or higher
- Compatible ESP32-C3 filter wheel hardware

### Quick Install

1. Download the latest installer from [Releases](https://github.com/juanjol/autoFilterWheel_ASCOM_drivers/releases)
2. Ensure ASCOM Platform 6.0+ is installed
3. Right-click the installer and select "Run as Administrator"
4. Follow the installation wizard
5. The driver will appear in ASCOM FilterWheel Chooser as "autoFilterWheel"

## Building from Source

### Prerequisites

- Visual Studio 2022 or later
- ASCOM Platform 6 Developer Components
- Inno Setup 6 (for installer creation)

### Build Steps

1. Clone the repository:
```bash
git clone https://github.com/juanjol/autoFilterWheel_ASCOM_drivers.git
cd autoFilterWheel_ASCOM_drivers
```

2. Open `MotoFilterWheel.sln` in Visual Studio

3. Build in Release mode:
   - Build → Build Solution
   - Or use MSBuild: `msbuild MotoFilterWheel.sln /p:Configuration=Release`

4. Create installer (optional):
   - Open `AutoFilterWheel_Setup.iss` in Inno Setup Compiler
   - Press F9 to compile

## Usage

### Initial Setup

1. Connect your ESP32-C3 filter wheel to a USB port
2. Open your astronomy software (e.g., NINA, SGP, APT)
3. Select "autoFilterWheel" from the ASCOM FilterWheel Chooser
4. Click "Properties" to open the setup dialog
5. Select the correct COM port and click "Connect"
6. Configure filter names and motor settings as needed

### Motor Configuration

The driver supports advanced motor configuration:

- **Speed**: Motor rotation speed (50-3000 steps/sec)
- **Acceleration**: Motor acceleration rate
- **Steps/Revolution**: Total steps for one complete rotation
- **Direction Mode**: Bidirectional or unidirectional operation
- **Reverse**: Option to reverse motor direction

### Calibration

Two calibration modes are available:

1. **Revolution Calibration**: Precisely calibrate steps per full revolution
2. **Backlash Calibration**: Measure and compensate for mechanical backlash

## Development

### Project Structure

```
MotoFilterWheel/
├── FilterWheelDriver/       # Main driver implementation
│   ├── FilterWheelDriver.cs # ASCOM interface implementation
│   ├── SerialCommunication.cs # Serial port handling
│   ├── SerialCommands.cs    # Protocol definitions
│   └── SetupDialogForm.cs   # Configuration UI
├── Properties/              # Assembly information
└── LocalServer.cs          # COM server implementation
```

### Serial Protocol

The driver communicates with the ESP32-C3 using a simple text-based protocol:

- Commands: `#COMMAND\r`
- Responses: `RESPONSE\r\n`
- See `SerialCommands.cs` for complete protocol documentation

### Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## CI/CD

This project uses GitHub Actions for automated builds:

- **Automatic Builds**: Every push to `main` or `develop`
- **Release Creation**: Tagged commits automatically create releases with installers
- **Artifact Storage**: Development builds are stored as artifacts

To create a new release:
```bash
git tag -a v1.0.0 -m "Release version 1.0.0"
git push origin v1.0.0
```

## Troubleshooting

### Driver doesn't appear in ASCOM Chooser
- Ensure installation was run as Administrator
- Try manual registration: `ASCOM.autoFilterWheel.exe /regserver`

### Cannot connect to device
- Verify correct COM port selection
- Check USB cable and connections
- Ensure no other software is using the COM port

### Motor not moving correctly
- Perform revolution calibration
- Check motor configuration settings
- Verify power supply to motor

## Support

- **Issues**: [GitHub Issues](https://github.com/juanjol/autoFilterWheel_ASCOM_drivers/issues)
- **ASCOM Forum**: [ASCOM Talk](https://ascomtalk.groups.io/)
- **Documentation**: [ASCOM Standards](https://ascom-standards.org/)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- ASCOM Initiative for the platform and standards
- ESP32-C3 community for hardware support
- Contributors and testers

---

**Note**: This driver requires compatible ESP32-C3 filter wheel hardware. Ensure your hardware firmware supports the communication protocol defined in this driver.