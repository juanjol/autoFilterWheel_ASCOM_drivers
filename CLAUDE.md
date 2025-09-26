# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview
This is an ASCOM Local Server driver for a motorized filter wheel device. The project creates a COM server executable that implements the ASCOM FilterWheel interface, allowing multiple astronomy software clients to control the filter wheel concurrently.

## Build Requirements
- **ASCOM Platform 6.0 or higher** must be installed (provides required ASCOM assemblies)
- **Visual Studio 2019 or later** with .NET Framework 4.7.2 support
- **MSBuild** (included with Visual Studio)

## Build Commands
```bash
# Build Debug configuration
msbuild MotoFilterWheel.sln /p:Configuration=Debug /p:Platform="Any CPU"

# Build Release configuration
msbuild MotoFilterWheel.sln /p:Configuration=Release /p:Platform="Any CPU"

# Clean solution
msbuild MotoFilterWheel.sln /t:Clean
```

Note: The project requires ASCOM libraries which are not available via NuGet and must be installed from the ASCOM Platform installer.

## Key Architecture Components

### Local Server Architecture
The driver uses a Local COM Server pattern where:
- `LocalServer.cs` manages the COM server lifecycle, handles client connections, and runs the Windows message loop
- Multiple clients can connect simultaneously through COM interfaces
- The server automatically starts when a client connects and shuts down when the last client disconnects
- Reference counting tracks active driver instances

### Driver Implementation Layers
1. **FilterWheelDriver.cs** - Presentation layer implementing the ASCOM IFilterWheelV3 interface. Handles COM visibility and client-specific state.
2. **FilterWheelHardware.cs** - Hardware abstraction layer (static class) that manages the actual device communication. Shared by all driver instances to ensure hardware serialization.
3. **SerialCommunication.cs** - Handles serial port communication with the ESP32-C3 controller, including command formatting and response parsing.
4. **SerialCommands.cs** - Defines all protocol constants and commands for communication with the Arduino/ESP32 controller.
5. **SetupDialogForm.cs** - Configuration UI for COM port selection and driver settings.

### Critical Implementation Notes
- The hardware class is static and marked with `[HardwareClass()]` to ensure proper disposal
- All hardware communication must be thread-safe as multiple clients may access concurrently
- The driver must handle asynchronous operations (e.g., report position while moving)
- COM port and trace settings are persisted in the ASCOM Profile store
- Serial communication: 115200 baud, 8N1, 1000ms timeout
- Position mapping: ASCOM uses 0-based indexing, Arduino uses 1-based indexing
- The driver supports 5 filter positions
- Device verification: Connection only succeeds if device ID matches `ESP32FW-5POS-V1.0`
- Connection sequence: Device ID verification → Version check → Ready for commands

## ASCOM-Specific Conventions
- Driver ProgID: `ASCOM.juanjolMotoFilterWheel.FilterWheel`
- Uses ASCOM.DeviceInterface for standard interface definitions
- Trace logging via ASCOM.Utilities.TraceLogger for diagnostics
- Settings stored using ASCOM Profile mechanism
- Must implement proper COM reference counting via ReferenceCountedObjectBase

## Development Notes
- Target Framework: .NET Framework 4.7.2
- Platform target: x86 (required for ASCOM compatibility)
- Strong-name signed with LocalServer.snk
- Assembly name: ASCOM.juanjolMotoFilterWheel
- Requires ASCOM Platform 6+ libraries