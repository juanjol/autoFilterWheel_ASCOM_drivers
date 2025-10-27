# Configuration

## Filter Configuration

### Filter Count

The number of filter positions in your wheel (3-9).

**To change filter count:**
1. Open driver setup
2. Connect to device
3. Select new filter count from dropdown
4. Click "Save Configuration"

**Notes:**
- Changing filter count resets position angles to equally-spaced
- Use Custom Angles for non-equal spacing
- Position 1 is always the home/reference position

### Filter Names

Descriptive names for each filter position.

**Naming guidelines:**
- Maximum 15 characters
- Can include spaces and special characters
- Avoid: `:` `#` (breaks serial protocol)
- Be descriptive: "H-Alpha 7nm" not "HA"

**Common naming conventions:**
- Broadband: "Luminance", "Red", "Green", "Blue"
- Narrowband: "H-Alpha", "OIII", "SII"
- Special: "Clear", "UV-IR Cut", "Dark"

### Saving Configuration

Click "Save Configuration" to write to ESP32 EEPROM:
- Filter count
- All filter names
- Current configuration

**Recovery:** Settings persist after power cycle.

## Motor Configuration

### Speed Settings

**Motor Speed** (steps/second):
```
Slow:    1000-2000   (gentle, quiet)
Normal:  3000-5000   (balanced)
Fast:    5000-10000  (quick, may vibrate)
```

**Max Speed** (steps/second):
- Should be ≥ Motor Speed
- Used during acceleration phase
- Higher values = shorter acceleration distance

**Acceleration** (steps/second²):
```
Gentle:  50,000-75,000   (smooth start/stop)
Normal:  100,000-150,000 (standard)
Aggressive: 200,000+     (quick response)
```

### Motor Control

**Disable Delay** (milliseconds):
- Time motor stays powered after movement
- Longer = better position holding
- Shorter = less heat, less power

**Recommended values:**
```
Always Holding:  5000-10000 ms
Normal:          1000-2000 ms
Minimal Hold:    500-1000 ms
```

### Direction Inversion

**Motor Inverted:**
- Check if wheel rotates backwards from expected
- Fixes: Position 1→2 should be clockwise (when viewed from motor side)

**Encoder Inverted:**
- Check if angle increases in wrong direction
- Fixes: Clockwise rotation should increase angle

### Advanced: Steps Per Revolution

Total motor steps for 360° wheel rotation.

**Calculation:**
```
Steps = Motor Steps × Gear Ratio
```

**Example (28BYJ-48):**
- Motor: 2048 steps/rev (in half-step mode)
- Gear ratio: 17.34:1 (approx)
- Total: 2048 × 17.34 ≈ 35,500 steps
```

**Adjustment:**
1. Set known value (e.g., 35,500)
2. Move to Position 1
3. Manually rotate 360°
4. Check if returns to Position 1
5. Adjust if needed

## Display Configuration

### Display Mode

**Minimal (0):**
```
┌──────────────┐
│              │
│      3       │  ← Large position number
│   Filter 3   │  ← Filter name
│              │
└──────────────┘
```

**Detailed (1):**
```
┌──────────────┐
│Pos:3  CAL:✓ │  ← Status indicators
│Filter 3      │  ← Name
│Angle: 144.5° │  ← Encoder
│Moving: No    │  ← State
└──────────────┘
```

### Brightness

Value: 0-255
- 0: Display off
- 128: Medium (default)
- 255: Maximum brightness

**Recommendations:**
- Daytime: 200-255
- Night observing: 50-100
- Remote observatory: 128 (default)

### Power Management

**Auto Mode:**
- Display on during movement
- Auto-off after timeout period
- Wakes on any activity

**Always On:**
- Display never turns off
- Higher power consumption
- Best for monitoring

**Always Off:**
- Display completely disabled
- Minimum power consumption
- Useful for power-sensitive setups

**Timeout Setting:**
- Time before auto-off (seconds)
- Only applies in Auto mode
- 0 = never auto-off
- Recommended: 30-60 seconds

### Display Rotation

**Normal (0°):**
- Default orientation
- Logo right-side up when viewed from front

**Inverted (180°):**
- Upside-down from normal
- Use when display is mounted inverted

## Custom Angles Configuration

For non-equally-spaced filter positions.

### When to Use

- Filters are not evenly spaced mechanically
- Custom wheel design
- Obstruction prevents equal spacing
- Fine-tuning position accuracy

### Calibration Methods

**Method 1: Wizard (Recommended)**
1. Start Calibration Wizard
2. Rotate to each position manually
3. Click "Set Position" for each
4. Finish wizard

**Method 2: Manual Entry**
1. Measure angles with protractor/encoder
2. Enter exact angles for each position
3. Apply settings
4. Test movement

### Angle Guidelines

- Position 1 should be 0° (reference)
- Angles must increase clockwise
- Each position must have unique angle
- Range: 0-359.99°

**Example 5-filter wheel:**
```
Position 1:   0.00°
Position 2:  68.50°
Position 3: 145.20°
Position 4: 215.80°
Position 5: 290.10°
```

### Testing Custom Angles

After setting:
1. Move to Position 1 → verify
2. Move to each position in sequence
3. Return to Position 1 → should match exactly
4. Adjust if positions are off

### Clearing Custom Angles

Returns to equally-spaced positions:
1. Click "Clear Custom Angles"
2. Confirm action
3. Positions reset to equal spacing

## COM Port Selection

### Manual Selection
1. Open Device Manager
2. Expand "Ports (COM & LPT)"
3. Find "USB-SERIAL CH340" or similar
4. Note COM port number
5. Select in driver setup

### Auto-Detection
1. Click "Connect" without selecting port
2. Driver scans all COM ports
3. Identifies ESP32 filter wheel
4. Connects automatically

**Notes:**
- Auto-detect may take 5-10 seconds
- COM port may change if device reconnects
- Save COM port in astronomy software profile

## Persistence

### Settings Saved in ESP32 EEPROM
- Filter count
- Filter names (all)
- Motor configuration (speed, acceleration, etc.)
- Motor direction inversions
- Display configuration
- Custom angles (if set)

### Settings Saved in ASCOM Profile
- Last used COM port
- Driver preferences

**Backup:** EEPROM settings persist even if driver is uninstalled or ASCOM profile is reset.

## Import/Export

Currently not supported. To backup configuration:
1. Take photo/screenshot of settings
2. Document custom angles if used
3. Note motor configuration values

Future versions may include export/import functionality.

## Factory Reset

To reset ESP32 to defaults:
1. Use firmware-specific reset procedure
2. Or: reflash firmware
3. Driver will detect new/reset device
4. Reconfigure from scratch

**Default values:**
- Filter count: 5
- Names: "Filter1", "Filter2", etc.
- Motor speed: 4000
- Max speed: 5000
- Acceleration: 100,000
- Display: On, normal rotation, auto mode
