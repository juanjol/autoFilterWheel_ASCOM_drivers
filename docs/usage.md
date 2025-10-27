# User Guide

## First Time Setup

### 1. Open Driver Setup

From your astronomy software (N.I.N.A., SGP, etc.):

1. Open Equipment settings
2. Select "Filter Wheel" or "Filter Changer"
3. Choose "ASCOM.autoFilterWheel.FilterWheel" from the list
4. Click "Setup" or "Properties"

### 2. Connect to Device

1. **Select COM Port**: Choose the port where your ESP32 is connected
   - Or click "Connect" to auto-detect the device

2. **Verify Connection**: Status bar should show "Connected to COMx"

3. **Check Current Status**: The driver will automatically read:
   - Filter count
   - Filter names
   - Current position
   - Motor configuration

### 3. Configure Filters

In the "Configuration" tab:

1. **Set Filter Count** (3-9):
   - Select the number of filters in your wheel

2. **Name Your Filters**:
   - Enter descriptive names (e.g., "Luminance", "Red", "H-Alpha")
   - Names can include spaces and special characters
   - Maximum 15 characters per name

3. **Save Configuration**:
   - Click "Save Configuration"
   - Configuration is stored in the ESP32 EEPROM

## Basic Operation

### Changing Filters

From your astronomy software:
- Select the desired filter by name
- The filter wheel will rotate to the correct position
- Wait for movement to complete before taking images

### Manual Position Change

In the driver setup dialog:
1. Select filter from dropdown
2. Click "Move to Position"
3. Wait for movement to complete

### Home Calibration

To set the current position as home (Position 1):
1. Manually rotate wheel to desired home position
2. In driver setup, select "Position 1"
3. Click appropriate calibration button

## Motor Configuration

### Basic Settings

**Motor Speed**: Normal rotation speed (steps/second)
- Default: 4000
- Range: 50 - 50,000
- Higher = faster, but may lose steps

**Max Speed**: Maximum speed during acceleration
- Default: 5000
- Range: 100 - 100,000
- Should be ≥ Motor Speed

**Acceleration**: Rate of speed change (steps/second²)
- Default: 100,000
- Range: 50 - 1,000,000
- Higher = quicker start/stop

**Disable Delay**: Time before motor power-off (milliseconds)
- Default: 1000
- Range: 500 - 10,000
- Longer = better holding, more power consumption

### Advanced Settings

**Motor Direction Inversion**: Reverses motor direction
- Use if wheel rotates backwards

**Encoder Direction Inversion**: Reverses encoder counting direction
- Use if position reading is backwards

**Steps Per Revolution**: Total steps for 360° rotation
- Default: 35,500 (for 28BYJ-48)
- Adjust based on gear ratio

## Display Configuration

### Display Modes

**Minimal Mode**: Large number display
- Shows current position prominently
- Best for at-a-glance status

**Detailed Mode**: Full information display
- Position, filter name, angle, calibration status
- More information, smaller text

### Display Settings

**Brightness**: 0-255
- Adjust for ambient light conditions
- Lower = less power, longer display life

**Rotation**: Normal or Inverted (180°)
- Adjust for mounting orientation

**Power Mode**:
- **Auto**: Display turns off after timeout
- **Always On**: Display never turns off
- **Always Off**: Display always off

**Timeout**: Auto-off delay (seconds)
- Only applies in Auto mode
- 0 = never turns off

## Custom Angles (Advanced)

For non-equally-spaced filters:

### Starting Calibration Wizard

1. Go to "Custom Angles" tab
2. Click "Start Calibration Wizard"
3. Follow on-screen instructions

### Calibration Steps

1. **Home Position**: Rotate to first filter, click "Set Position 1"
2. **Next Positions**: Rotate to each filter, click corresponding "Set Position" button
3. **Finish**: Click "Finish Calibration"

### Manual Angle Entry

If you know the exact angles:
1. Enter angle in degrees for each position
2. Click "Apply"
3. Test by moving to each position

## Tips and Best Practices

### Before Imaging Session

1. Connect and verify communication
2. Test movement to all filter positions
3. Verify filter names match your imaging plan
4. Check display is functioning

### During Session

- Let filter wheel complete movement before imaging
- Monitor for any unusual sounds or vibrations
- Keep USB cable secure to prevent disconnection

### Maintenance

- Periodically clean encoder and magnet
- Check for loose connections
- Verify calibration if positions drift
- Keep firmware updated

## Status Indicators

### Status Bar Messages

- **"Ready"**: Driver loaded, not connected
- **"Connecting..."**: Attempting connection
- **"Connected to COMx"**: Successfully connected
- **"Reading configuration..."**: Loading device settings
- **"Connection failed"**: Unable to connect

### Device Status

- **Position**: Current filter number (1-9)
- **Moving**: Yes/No
- **Calibrated**: Yes/No
- **Angle**: Current encoder angle (0-360°)

## Keyboard Shortcuts

In the Communication Log tab:
- **F1**: Show command help
- **Enter**: Send manual command
- **Up/Down**: Command history

## Safety Features

### Emergency Stop

- Large red "EMERGENCY STOP" button in Manual Control tab
- Immediately stops all motor movement
- Use in case of mechanical jam or other emergency

### Automatic Timeout

- If movement takes too long, operation times out
- Prevents infinite loops
- Timeout: 2 minutes for movement commands

### Error Handling

- Invalid positions are rejected
- Serial communication errors are logged
- Device automatically reconnects after USB reset
