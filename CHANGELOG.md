# Changelog

All notable changes to the autoFilterWheel ASCOM Driver will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Initial release of autoFilterWheel ASCOM driver
- Full ASCOM 6.0 FilterWheel interface implementation
- Serial communication with ESP32-C3 controller
- Comprehensive setup dialog with:
  - Connection management
  - Filter configuration (3-8 positions)
  - Custom filter naming
  - Motor configuration settings
  - Real-time device monitoring
- Advanced motor control features:
  - Configurable speed (50-3000 steps/sec)
  - Configurable acceleration
  - Adjustable steps per revolution
  - Bidirectional/unidirectional operation modes
  - Direction reversal option
- Calibration features:
  - Revolution calibration for precise positioning
  - Backlash measurement and compensation
- Manual control capabilities:
  - Step forward/backward by specified amounts
  - Direct position movement
  - Emergency stop function
- Robust error handling and logging
- Installer with automatic COM registration
- GitHub Actions workflow for automated builds

### Technical Details
- Platform: .NET Framework 4.7.2
- ASCOM Platform: 6.0+
- Communication: Serial (115200 baud)
- Supported OS: Windows 7/8/10/11

## Version History

### [1.0.0] - TBD
- First stable release