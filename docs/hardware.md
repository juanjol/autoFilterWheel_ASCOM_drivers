# Hardware Setup

## Bill of Materials

| Component | Specification | Quantity |
|-----------|--------------|----------|
| Microcontroller | ESP32-C3 (ESP32-C3-DevKitM-1 or similar) | 1 |
| Stepper Motor | 28BYJ-48 with ULN2003 driver (5V) | 1 |
| Encoder | AS5600 magnetic encoder | 1 |
| Magnet | Diametric N52 magnet (6mm diameter) | 1 |
| Display | SSD1306 OLED 128x64 (I2C) | 1 |
| USB Cable | USB-C cable (for ESP32-C3) | 1 |
| Power Supply | 5V DC adapter (1A minimum) | 1 |

## Wiring Diagram

### ESP32-C3 Pin Connections

```
ESP32-C3 Pinout:

Motor (ULN2003):
  GPIO 0  → IN1
  GPIO 1  → IN2
  GPIO 2  → IN3
  GPIO 3  → IN4
  5V      → VCC
  GND     → GND

Encoder (AS5600):
  GPIO 6  → SDA
  GPIO 7  → SCL
  3.3V    → VCC
  GND     → GND

Display (SSD1306):
  GPIO 6  → SDA (shared with encoder)
  GPIO 7  → SCL (shared with encoder)
  3.3V    → VCC
  GND     → GND

Power:
  5V      → Motor power
  3.3V    → Encoder and display power
  GND     → Common ground
```

### Connection Diagram

```
┌─────────────────────────────────────────────────────┐
│                    ESP32-C3                         │
│                                                     │
│  GPIO0─────────┐                                    │
│  GPIO1─────────┤                                    │
│  GPIO2─────────┤ Motor Control                      │
│  GPIO3─────────┤                                    │
│                └────────► ULN2003 ──► 28BYJ-48     │
│                                                     │
│  GPIO6 (SDA)───┬────► AS5600 Encoder               │
│  GPIO7 (SCL)───┤                                    │
│                └────► SSD1306 Display               │
│                                                     │
│  USB-C ◄──────────────► Computer                   │
└─────────────────────────────────────────────────────┘
```

## Assembly Instructions

### 1. Encoder Setup

1. Mount the AS5600 encoder PCB near the rotation axis
2. Attach the diametric magnet to the rotating shaft
3. Ensure 1-3mm gap between magnet and encoder
4. Magnet should be centered over the AS5600 chip

!!! warning "Magnet Alignment"
    The magnet must be diametrically magnetized (poles on opposite edges, not top/bottom).
    Proper alignment is critical for accurate position sensing.

### 2. Motor Connection

1. Connect the 28BYJ-48 motor to the ULN2003 driver board
2. Wire the ULN2003 driver to ESP32-C3 GPIO pins
3. Connect 5V power to the ULN2003 board
4. Ensure common ground connection

### 3. Display Connection

1. Connect SSD1306 OLED display to I2C pins (shared with encoder)
2. Use 3.3V power for the display
3. Ensure correct SDA/SCL connections

### 4. Power Supply

- Motor requires 5V @ 500mA minimum
- ESP32-C3 can be powered via USB or external 5V
- Use quality power supply to avoid motor noise affecting encoder

### 5. Mechanical Assembly

1. Mount motor to filter wheel mechanism
2. Ensure encoder is fixed and magnet rotates with the filter wheel
3. Calibrate home position using the driver software

## USB Connection

1. Connect ESP32-C3 to computer via USB-C cable
2. Windows will install CH340 or CP2102 USB-to-serial driver automatically
3. Check Device Manager for the assigned COM port number
4. Note this COM port for driver configuration

## Testing Hardware

Before using the ASCOM driver, test the hardware:

1. Power on the system
2. Display should show the ESP32 Filter Wheel logo
3. Motor should be disabled initially (no holding torque)
4. Encoder should be reading position (visible on display)

## Troubleshooting Hardware

### Display not working
- Check I2C connections (SDA/SCL)
- Verify 3.3V power supply
- Ensure I2C address is 0x3C (default for SSD1306)

### Encoder not detecting position
- Check magnet alignment (1-3mm gap)
- Verify magnet is diametric (not axial)
- Confirm I2C connection
- Check AS5600 status LED (if present)

### Motor not moving
- Verify 5V power supply
- Check all 4 motor control pins
- Ensure ULN2003 board is powered
- Test motor separately with simple firmware

### USB not recognized
- Install ESP32-C3 USB driver
- Try different USB cable
- Check USB port functionality
- Verify ESP32-C3 is powered
