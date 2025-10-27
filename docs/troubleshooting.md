# Troubleshooting

## Connection Issues

### Driver Cannot Find Device

**Symptoms:**
- "Device not found" error
- Auto-detect fails
- Manual COM port selection fails

**Solutions:**

1. **Check USB Connection:**
   - Verify cable is plugged in securely
   - Try different USB port
   - Try different USB cable
   - Check if ESP32 power LED is on

2. **Verify COM Port:**
   - Open Device Manager
   - Check "Ports (COM & LPT)"
   - Look for "USB-SERIAL" device
   - Note COM port number

3. **Driver Installation:**
   - Windows should auto-install CH340/CP2102 driver
   - If not, download from ESP32 manufacturer
   - Restart computer after driver install

4. **Device Reset:**
   - Unplug ESP32
   - Wait 5 seconds
   - Plug back in
   - Try connecting again

### Connection Drops During Use

**Symptoms:**
- "Lost connection" errors
- Device disconnects randomly
- Intermittent communication

**Solutions:**

1. **USB Power:**
   - Use powered USB hub if needed
   - Check motor power supply (5V, 1A minimum)
   - Avoid USB extension cables

2. **Interference:**
   - Route USB cable away from motor wires
   - Add ferrite beads to USB cable
   - Ensure motor ground is connected

3. **COM Port Conflicts:**
   - Close other serial port software
   - Check Device Manager for yellow exclamation marks
   - Update USB driver

### Slow Connection or Timeouts

**Symptoms:**
- Takes long time to connect
- "Timeout retrieving configuration"
- Connection succeeds but freezes

**Solutions:**

1. Driver has 3 second timeout for EEPROM reads
2. Old firmware may be slower - update if available
3. If persistent, try "legacy mode" in future driver versions

## Movement Issues

### Wheel Not Moving

**Symptoms:**
- Command accepted but no movement
- Motor doesn't make any sound
- Position doesn't change

**Solutions:**

1. **Check Motor Power:**
   - Verify 5V power supply connected
   - Check ULN2003 board LED
   - Measure voltage at motor connector

2. **Check Connections:**
   - Verify all 4 motor wires connected
   - Check GPIO pin connections
   - Ensure motor not mechanically stuck

3. **Test Emergency Stop:**
   - Click "Emergency Stop" to reset
   - Try manual movement command

4. **Firmware Issue:**
   - Check serial log for errors
   - Reflash ESP32 firmware if needed

### Wheel Moves to Wrong Position

**Symptoms:**
- Selects Position 3, goes to Position 4
- Positions are offset
- Inconsistent positioning

**Solutions:**

1. **Calibration:**
   - Perform home calibration
   - Set Position 1 at known location
   - Verify encoder reading

2. **Direction Inversion:**
   - Check "Motor Inverted" setting
   - Check "Encoder Inverted" setting
   - May need to invert one or both

3. **Custom Angles:**
   - If using custom angles, verify they're correct
   - Try clearing custom angles
   - Recalibrate if needed

4. **Steps Per Revolution:**
   - Verify correct value for your motor/gearing
   - Test 360° rotation
   - Adjust if doesn't return to same position

### Wheel Moves Too Fast/Slow

**Symptoms:**
- Movement is too fast and skips
- Movement is too slow
- Vibration during movement

**Solutions:**

1. **Adjust Motor Speed:**
   - Lower speed if skipping steps
   - Increase if too slow
   - Typical range: 3000-5000 steps/s

2. **Adjust Acceleration:**
   - Lower if wheel shakes at start/stop
   - Increase for quicker response
   - Typical: 100,000-150,000 steps/s²

3. **Check Load:**
   - Ensure filters aren't too heavy
   - Check for mechanical friction
   - Verify smooth rotation by hand

### Lost Steps / Position Drift

**Symptoms:**
- After many movements, position is off
- Encoder angle doesn't match expected
- Wheel slowly drifts over time

**Solutions:**

1. **Reduce Speed:**
   - Motor may be missing steps at high speed
   - Lower Motor Speed setting
   - Increase acceleration time

2. **Check Mechanical:**
   - Look for binding or friction
   - Ensure smooth rotation
   - Check filter wheel balance

3. **Encoder Feedback:**
   - Encoder provides position correction
   - Check encoder readings in Status
   - Verify magnet is secure and aligned

4. **Recalibrate:**
   - Perform home position calibration
   - Reset custom angles if used

## Display Issues

### Display Not Working

**Symptoms:**
- Display is blank
- Display never turns on
- No content visible

**Solutions:**

1. **Check Display Power:**
   - Verify 3.3V at display VCC pin
   - Check ground connection

2. **Check I2C Connection:**
   - Verify SDA/SCL pins connected
   - Check for loose wires
   - Swap SDA/SCL if image is corrupted

3. **Display Settings:**
   - Check if "Display Enabled" is checked
   - Try changing brightness to max (255)
   - Check power mode (set to "Always On" for testing)

4. **I2C Address:**
   - Most SSD1306 displays use 0x3C
   - Some use 0x3D
   - May need firmware modification

### Display Shows Wrong Information

**Symptoms:**
- Position number is incorrect
- Filter name doesn't match
- Angle reading is wrong

**Solutions:**

1. **Check Configuration:**
   - Verify filter count is correct
   - Check filter names are saved
   - Verify position calibration

2. **Display Rotation:**
   - Check if display is inverted
   - Adjust rotation setting (0°/180°)

3. **Encoder Reading:**
   - Check encoder status in driver
   - Verify magnet alignment
   - Test encoder separately

## Encoder Issues

### No Encoder Reading

**Symptoms:**
- Angle always shows 0.00°
- Position detection doesn't work
- "Encoder error" in logs

**Solutions:**

1. **Check Magnet:**
   - Verify magnet is present
   - Check 1-3mm gap from AS5600
   - Ensure diametric magnetization (not axial)

2. **Check Connections:**
   - Verify I2C SDA/SCL connected
   - Check 3.3V power
   - Ensure ground connected

3. **Test AS5600:**
   - AS5600 should have status LED
   - Use I2C scanner to verify address (0x36)
   - May need separate testing firmware

### Erratic Encoder Readings

**Symptoms:**
- Angle jumps around wildly
- Inconsistent readings
- Position detection unreliable

**Solutions:**

1. **Magnet Alignment:**
   - Check magnet is centered over AS5600
   - Verify proper gap distance
   - Ensure magnet doesn't wobble

2. **Magnetic Interference:**
   - Keep magnet away from motor
   - Shield from external magnetic fields
   - Check for ferromagnetic materials nearby

3. **Electrical Noise:**
   - Add capacitor to AS5600 power (0.1µF)
   - Route I2C wires away from motor
   - Check ground connections

## Software Integration Issues

### ASCOM Software Can't Find Driver

**Symptoms:**
- Driver doesn't appear in filter wheel list
- "Not registered" error
- ASCOM Chooser shows empty

**Solutions:**

1. **Re-register Driver:**
   - Run installer again as administrator
   - Check ASCOM Diagnostics
   - Restart ASCOM software

2. **ASCOM Platform:**
   - Verify ASCOM Platform 6.6+ installed
   - Update ASCOM Platform if needed
   - Restart computer after install

3. **32/64-bit Mismatch:**
   - Check if astronomy software is 32 or 64-bit
   - Driver should work with both
   - May need to reinstall

### Driver Crashes or Hangs

**Symptoms:**
- Driver setup window freezes
- Application not responding
- Must force-close

**Solutions:**

1. **Close Other Software:**
   - Only one application can use driver at once
   - Close ASCOM Diagnostics
   - Close other astronomy software

2. **Reset Connection:**
   - Unplug USB cable
   - Close driver setup
   - Plug USB back in
   - Reopen setup

3. **Check Logs:**
   - Look in ASCOM Diagnostics logs
   - Check Windows Event Viewer
   - Report bug with log files

### Position Not Updating in Software

**Symptoms:**
- Wheel moves but software doesn't see it
- Position shows "Unknown"
- Software times out waiting for movement

**Solutions:**

1. **Wait for Completion:**
   - Some software polls position frequently
   - Movement must fully complete first
   - Check "Moving" status

2. **ASCOM Polling:**
   - Software may be polling too aggressively
   - Check software settings for filter wheel polling interval
   - Increase polling interval if needed

## Performance Issues

### Slow Filter Changes

**Symptoms:**
- Filter changes take a long time
- Much slower than expected
- Timeout warnings

**Solutions:**

1. **Increase Motor Speed:**
   - Current speed may be too conservative
   - Try increasing to 5000-8000 steps/s
   - Monitor for lost steps

2. **Optimize Path:**
   - Driver calculates shortest rotation path
   - For 5-filter wheel: max 2 positions
   - If taking longer, check encoder feedback

3. **Check Mechanical:**
   - Ensure no binding
   - Lubricate if needed
   - Check for obstructions

### Position Accuracy Issues

**Symptoms:**
- Filter slightly off-center
- Images show filter edge
- Inconsistent positioning

**Solutions:**

1. **Disable Delay:**
   - Increase motor disable delay
   - Gives more time for settling
   - Try 2000-5000ms

2. **Encoder Feedback:**
   - Driver uses encoder for accuracy
   - Check encoder readings
   - May need to adjust expected angles

3. **Mechanical Tolerance:**
   - Some play is normal in stepper motors
   - Use encoder compensation
   - Consider mechanical improvements

## Error Messages

### "Timeout retrieving configuration"

**Cause:** Device took too long to respond to GETCONFIG command

**Solutions:**
- Check USB connection
- Increase timeout in driver (if configurable)
- Try reconnecting
- Update firmware if available

### "Invalid position response"

**Cause:** Device sent malformed position data

**Solutions:**
- Check for serial line noise
- Verify firmware version compatibility
- Try power cycling device

### "Serial port access denied"

**Cause:** Another application is using the COM port

**Solutions:**
- Close other serial terminal software
- Close other ASCOM applications
- Restart computer if persistent

### "Device ID mismatch"

**Cause:** Connected device is not an autoFilterWheel

**Solutions:**
- Verify correct device is connected
- Check firmware is programmed correctly
- Update firmware to compatible version

## Getting Help

If problems persist:

1. **Collect Information:**
   - Driver version (visible in About tab)
   - Firmware version (shown on device display or in driver)
   - Windows version
   - ASCOM Platform version
   - Exact error message
   - Steps to reproduce

2. **Check Logs:**
   - ASCOM Diagnostics logs
   - Driver Communication Log tab
   - Windows Event Viewer

3. **Report Issue:**
   - GitHub Issues: https://github.com/juanjol/autoFilterWheel_ASCOM_drivers/issues
   - Include all information from step 1
   - Attach log files if possible
   - Screenshots are helpful

4. **Hardware Testing:**
   - Test with simple firmware first
   - Verify each component separately
   - Document what works and what doesn't
