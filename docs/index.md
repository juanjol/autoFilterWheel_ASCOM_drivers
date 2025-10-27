# autoFilterWheel ASCOM Driver

ASCOM driver for ESP32-C3 based motorized filter wheel controller.

## Overview

This driver allows control of a custom-built filter wheel using the ASCOM standard interface, compatible with popular astrophotography software like N.I.N.A., Sequence Generator Pro, MaxIm DL, and others.

## Features

- **3-9 Filter Support**: Configure between 3 and 9 filter positions
- **Custom Filter Names**: Name your filters for easy identification
- **Custom Angles**: Support for non-equally-spaced filter positions
- **Real-time Position Feedback**: Magnetic encoder provides accurate position tracking
- **OLED Display**: Shows current position and filter name on the device
- **Motor Configuration**: Adjustable speed, acceleration, and other motor parameters
- **Serial Communication**: USB connection via ESP32-C3 (115200 baud)

## Hardware Requirements

- ESP32-C3 microcontroller
- Stepper motor (28BYJ-48 or similar)
- AS5600 magnetic encoder
- 128x64 OLED display (SSD1306)
- USB cable for connection

## Software Requirements

- Windows 10/11
- ASCOM Platform 6.6 or later
- .NET Framework 4.8 or later

## Quick Start

1. [Install the driver](installation.md)
2. [Connect and configure your hardware](hardware.md)
3. [Configure the driver](configuration.md)
4. Start using it with your astrophotography software

## System Architecture

```
┌─────────────────────┐
│  Astro Software     │
│  (N.I.N.A., SGP)    │
└──────────┬──────────┘
           │ ASCOM Interface
┌──────────▼──────────┐
│  ASCOM Driver       │
│  (This Software)    │
└──────────┬──────────┘
           │ Serial/USB
┌──────────▼──────────┐
│  ESP32-C3           │
│  Filter Wheel       │
│  Controller         │
└─────────────────────┘
```

## Support

For issues, questions, or contributions, visit the [GitHub repository](https://github.com/juanjol/autoFilterWheel_ASCOM_drivers).
