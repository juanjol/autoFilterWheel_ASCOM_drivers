using System;
using System.Threading;
using ASCOM.Utilities;
using System.Collections.Generic;

namespace ASCOM.autoFilterWheel.FilterWheel
{
    /// <summary>
    /// Complete device configuration data from GETCONFIG command
    /// </summary>
    internal class DeviceConfig
    {
        // Filter configuration
        public int FilterCount { get; set; }
        public string[] FilterNames { get; set; }

        // Motor configuration
        public int MotorSpeed { get; set; }
        public int MotorMaxSpeed { get; set; }
        public int MotorAccel { get; set; }
        public int MotorDisableDelay { get; set; }
        public int MotorStepsPerRev { get; set; }
        public bool MotorInverted { get; set; }
        public bool EncoderInverted { get; set; }

        // Display configuration
        public string DisplaySize { get; set; }
        public bool DisplayRotation180 { get; set; }
        public bool DisplayEnabled { get; set; }
        public int DisplayBrightness { get; set; }
        public int DisplayMode { get; set; }
        public int DisplayPowerMode { get; set; }
        public int DisplayTimeout { get; set; }

        // Status
        public int StatusPosition { get; set; }
        public bool StatusMoving { get; set; }
        public bool StatusCalibrated { get; set; }
        public float StatusAngle { get; set; }

        public DeviceConfig()
        {
            FilterNames = new string[SerialCommands.MAX_FILTER_COUNT];
        }
    }

    /// <summary>
    /// Helper class to manage serial communication with the ESP32-C3 Filter Wheel Controller
    /// </summary>
    internal class SerialCommunication : IDisposable
    {
        private Serial serialPort;
        private readonly TraceLogger tl;
        private bool isConnected = false;
        private readonly object lockObject = new object();

        public SerialCommunication(TraceLogger traceLogger)
        {
            tl = traceLogger;
            serialPort = new Serial();
        }

        /// <summary>
        /// Gets the connection status
        /// </summary>
        public bool IsConnected
        {
            get
            {
                lock (lockObject)
                {
                    return isConnected && serialPort.Connected;
                }
            }
        }

        /// <summary>
        /// Connect to the serial port
        /// </summary>
        public void Connect(string portName)
        {
            lock (lockObject)
            {
                try
                {
                    if (isConnected)
                    {
                        tl.LogMessage("SerialCommunication.Connect", "Already connected");
                        return;
                    }

                    tl.LogMessage("SerialCommunication.Connect", $"Connecting to {portName}");

                    // Configure serial port parameters
                    serialPort.PortName = portName;
                    serialPort.Speed = SerialSpeed.ps115200;
                    serialPort.DataBits = 8;
                    serialPort.Parity = SerialParity.None;
                    serialPort.StopBits = SerialStopBits.One;
                    serialPort.Handshake = SerialHandshake.None;
                    serialPort.DTREnable = true;
                    serialPort.RTSEnable = true;
                    serialPort.ReceiveTimeout = SerialCommands.COMMAND_TIMEOUT_MS;
                    serialPort.ReceiveTimeoutMs = SerialCommands.COMMAND_TIMEOUT_MS;

                    tl.LogMessage("SerialCommunication.Connect", $"Port configuration: {portName}, 115200, 8N1");

                    // Attempt to open the serial port
                    try
                    {
                        serialPort.Connected = true;
                        tl.LogMessage("SerialCommunication.Connect", "Serial port opened successfully");
                    }
                    catch (Exception portEx)
                    {
                        tl.LogMessage("SerialCommunication.Connect", $"Failed to open serial port: {portEx.Message}");

                        // Provide specific error information
                        if (portEx.Message.Contains("Access is denied"))
                        {
                            throw new InvalidOperationException($"Port {portName} is already in use by another application. Please close any terminal programs or other ASCOM drivers using this port.", portEx);
                        }
                        else if (portEx.Message.Contains("does not exist"))
                        {
                            throw new InvalidOperationException($"Port {portName} does not exist. Please verify the ESP32 is connected and check Device Manager for the correct COM port.", portEx);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Cannot open port {portName}: {portEx.Message}", portEx);
                        }
                    }

                    // Clear any existing data in the buffer
                    serialPort.ClearBuffers();

                    // Wait briefly for ESP32 to be ready (ESP32-C3 is much faster than Arduino)
                    Thread.Sleep(250);

                    // Quick connectivity test - try to get filter count
                    try
                    {
                        serialPort.ClearBuffers();

                        // Test with a simple quick command
                        string testCommand = SerialCommands.FormatCommand(SerialCommands.CMD_GET_FILTERS);
                        serialPort.Transmit(testCommand);

                        // Try to read response quickly (max 500ms)
                        string response = "";
                        int maxWait = 10; // 10 * 50ms = 500ms max
                        int attempts = 0;

                        while (attempts < maxWait && !response.Contains("\n"))
                        {
                            try
                            {
                                string chunk = serialPort.ReceiveCounted(1);
                                if (!string.IsNullOrEmpty(chunk))
                                {
                                    response += chunk;
                                    // If we got a complete response, exit early
                                    if (response.Contains("\n") || response.Contains("\r"))
                                        break;
                                }
                            }
                            catch (TimeoutException)
                            {
                                // Continue
                            }
                            attempts++;
                            Thread.Sleep(50);
                        }

                        response = response.Trim('\r', '\n', ' ');
                        tl.LogMessage("SerialCommunication.Connect", $"Quick test response: '{response}'");

                        if (response.StartsWith("F") || response.Contains("ERROR"))
                        {
                            tl.LogMessage("SerialCommunication.Connect", "Device responded - connection successful");
                            isConnected = true;
                        }
                        else
                        {
                            // Even if no response, mark as connected - we'll find out on first real command
                            tl.LogMessage("SerialCommunication.Connect", "No response to test command, assuming connected");
                            isConnected = true;
                        }
                    }
                    catch (Exception comEx)
                    {
                        // Don't fail connection if test fails - mark as connected anyway
                        tl.LogMessage("SerialCommunication.Connect", $"Quick test failed, assuming connected anyway: {comEx.Message}");
                        isConnected = true;
                    }
                }
                catch (Exception ex)
                {
                    tl.LogMessage("SerialCommunication.Connect", $"Connection failed: {ex.Message}");
                    Disconnect();
                    throw;
                }
            }
        }

        /// <summary>
        /// Disconnect from the serial port
        /// </summary>
        public void Disconnect()
        {
            lock (lockObject)
            {
                try
                {
                    if (serialPort != null && serialPort.Connected)
                    {
                        tl.LogMessage("SerialCommunication.Disconnect", "Disconnecting");
                        serialPort.ClearBuffers();
                        serialPort.Connected = false;
                    }
                    isConnected = false;
                }
                catch (Exception ex)
                {
                    tl.LogMessage("SerialCommunication.Disconnect", $"Error during disconnect: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Send a command and wait for response
        /// </summary>
        public string SendCommand(string command)
        {
            return SendCommand(command, SerialCommands.COMMAND_TIMEOUT_MS);
        }

        /// <summary>
        /// Send a command and wait for response with custom timeout
        /// </summary>
        public string SendCommand(string command, int timeoutMs)
        {
            lock (lockObject)
            {
                if (!IsConnected)
                    throw new NotConnectedException("Serial port is not connected");

                try
                {
                    string formattedCommand = SerialCommands.FormatCommand(command);
                    tl.LogMessage("SerialCommunication.SendCommand", $"Sending: {formattedCommand.TrimEnd('\r', '\n')} (timeout: {timeoutMs}ms)");

                    // Temporarily set the timeout for this command
                    int originalTimeout = serialPort.ReceiveTimeoutMs;
                    try
                    {
                        serialPort.ReceiveTimeoutMs = timeoutMs;

                        // Clear buffers before sending
                        serialPort.ClearBuffers();

                        // Send command
                        serialPort.Transmit(formattedCommand);

                        // Read response
                        string response = ReadResponse();
                        tl.LogMessage("SerialCommunication.SendCommand", $"Received: {response}");

                        // Check for errors
                        if (SerialCommands.IsErrorResponse(response))
                        {
                            string errorMsg = SerialCommands.GetErrorMessage(response);
                            throw new InvalidOperationException($"Device error: {errorMsg}");
                        }

                        return response;
                    }
                    finally
                    {
                        // Restore original timeout
                        serialPort.ReceiveTimeoutMs = originalTimeout;
                    }
                }
                catch (TimeoutException ex)
                {
                    tl.LogMessage("SerialCommunication.SendCommand", $"Timeout: {ex.Message}");
                    throw new TimeoutException($"No response from device for command: {command}", ex);
                }
                catch (Exception ex)
                {
                    tl.LogMessage("SerialCommunication.SendCommand", $"Error: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Send a command without waiting for response
        /// </summary>
        public void SendCommandBlind(string command)
        {
            lock (lockObject)
            {
                if (!IsConnected)
                    throw new NotConnectedException("Serial port is not connected");

                try
                {
                    string formattedCommand = SerialCommands.FormatCommand(command);
                    tl.LogMessage("SerialCommunication.SendCommandBlind", $"Sending: {formattedCommand.TrimEnd('\r', '\n')}");

                    serialPort.Transmit(formattedCommand);
                }
                catch (Exception ex)
                {
                    tl.LogMessage("SerialCommunication.SendCommandBlind", $"Error: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Get current position from the device
        /// </summary>
        public int GetPosition()
        {
            string response = SendCommand(SerialCommands.CMD_GET_POSITION);
            return SerialCommands.ParsePositionResponse(response);
        }

        /// <summary>
        /// Move to specified position
        /// </summary>
        public void MoveToPosition(int position, int maxFilters = SerialCommands.MAX_FILTER_COUNT)
        {
            if (position < 1 || position > maxFilters)
                throw new InvalidValueException($"Position must be between 1 and {maxFilters}");

            string command = SerialCommands.CMD_MOVE_POSITION + position;

            // MP command is blocking and can take up to 10 seconds for movement
            // Use extended timeout (30 seconds) to give sufficient margin for slow movements
            string response = SendCommand(command, 30000);

            // Verify the response
            if (!response.StartsWith(SerialCommands.RESP_MOVED + position))
            {
                throw new InvalidOperationException($"Unexpected response to move command: {response}");
            }
        }

        /// <summary>
        /// Wait for movement to complete
        /// </summary>
        public bool WaitForMovementComplete(int timeoutMs = 10000)
        {
            int elapsed = 0;
            const int pollInterval = 100;

            while (elapsed < timeoutMs)
            {
                try
                {
                    string status = SendCommand(SerialCommands.CMD_STATUS);
                    if (status.Contains("MOVING=NO"))
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    tl.LogMessage("SerialCommunication.WaitForMovementComplete", $"Error checking status: {ex.Message}");
                }

                Thread.Sleep(pollInterval);
                elapsed += pollInterval;
            }

            return false;
        }

        /// <summary>
        /// Get filter count from device
        /// </summary>
        public int GetFilterCount()
        {
            string response = SendCommand(SerialCommands.CMD_GET_FILTERS);

            // Parse response format: F[3-9]
            if (response.StartsWith(SerialCommands.RESP_FILTERS) && response.Length > 1)
            {
                if (int.TryParse(response.Substring(1), out int count))
                {
                    return count;
                }
            }

            throw new FormatException($"Invalid filter count response: {response}");
        }

        /// <summary>
        /// Get filter name for a position
        /// </summary>
        public string GetFilterName(int position, int maxFilters = SerialCommands.MAX_FILTER_COUNT)
        {
            if (position < 1 || position > maxFilters)
                throw new InvalidValueException($"Position must be between 1 and {maxFilters}");

            string command = SerialCommands.CMD_GET_FILTER_NAME + position;
            string response = SendCommand(command);

            // Parse response format: N[position]:name
            string prefix = SerialCommands.RESP_NAME + position + ":";
            if (response.StartsWith(prefix))
            {
                return response.Substring(prefix.Length);
            }

            throw new FormatException($"Invalid filter name response: {response}");
        }

        /// <summary>
        /// Get all filter names
        /// </summary>
        public string[] GetAllFilterNames(int filterCount = SerialCommands.DEFAULT_FILTER_COUNT)
        {
            if (filterCount < SerialCommands.MIN_FILTER_COUNT || filterCount > SerialCommands.MAX_FILTER_COUNT)
                filterCount = SerialCommands.DEFAULT_FILTER_COUNT;

            string[] names = new string[filterCount];

            for (int i = 1; i <= filterCount; i++)
            {
                try
                {
                    names[i - 1] = GetFilterName(i, filterCount);
                    // Add delay between consecutive EEPROM reads to avoid overwhelming the device
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    tl.LogMessage("SerialCommunication.GetAllFilterNames", $"Error getting name for position {i}: {ex.Message}");
                    names[i - 1] = $"Filter {i}";
                }
            }

            return names;
        }

        /// <summary>
        /// Get complete device configuration using GETCONFIG command
        /// </summary>
        public DeviceConfig GetDeviceConfig()
        {
            lock (lockObject)
            {
                if (!IsConnected)
                    throw new NotConnectedException("Serial port is not connected");

                try
                {
                    DeviceConfig config = new DeviceConfig();

                    tl.LogMessage("SerialCommunication.GetDeviceConfig", "Sending GETCONFIG command");

                    // Use extended timeout for this command (5 seconds)
                    string formattedCommand = SerialCommands.FormatCommand(SerialCommands.CMD_GET_CONFIG);

                    // Temporarily set timeout
                    int originalTimeout = serialPort.ReceiveTimeoutMs;
                    try
                    {
                        serialPort.ReceiveTimeoutMs = 5000;

                        // Clear buffers before sending
                        serialPort.ClearBuffers();

                        // Send command
                        serialPort.Transmit(formattedCommand);

                        // Read multi-line response until CONFIG_END
                        List<string> lines = new List<string>();
                        bool configComplete = false;
                        int maxLines = 50; // Safety limit
                        int lineCount = 0;

                        while (!configComplete && lineCount < maxLines)
                        {
                            string line = ReadResponse();

                            if (string.IsNullOrWhiteSpace(line))
                                continue;

                            tl.LogMessage("SerialCommunication.GetDeviceConfig", $"Received: {line}");

                            if (line == "CONFIG_END")
                            {
                                configComplete = true;
                                break;
                            }

                            lines.Add(line);
                            lineCount++;
                        }

                        if (!configComplete)
                        {
                            throw new InvalidOperationException("GETCONFIG response did not end with CONFIG_END");
                        }

                        // Parse all lines
                        foreach (string line in lines)
                        {
                            ParseConfigLine(line, config);
                        }

                        tl.LogMessage("SerialCommunication.GetDeviceConfig", $"Configuration received: {config.FilterCount} filters");

                        return config;
                    }
                    finally
                    {
                        // Restore original timeout
                        serialPort.ReceiveTimeoutMs = originalTimeout;
                    }
                }
                catch (TimeoutException ex)
                {
                    tl.LogMessage("SerialCommunication.GetDeviceConfig", $"Timeout: {ex.Message}");
                    throw new TimeoutException($"No response from device for GETCONFIG command", ex);
                }
                catch (Exception ex)
                {
                    tl.LogMessage("SerialCommunication.GetDeviceConfig", $"Error: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Parse a single line from GETCONFIG response
        /// </summary>
        private void ParseConfigLine(string line, DeviceConfig config)
        {
            if (string.IsNullOrWhiteSpace(line))
                return;

            int colonIndex = line.IndexOf(':');
            if (colonIndex < 0)
                return;

            string key = line.Substring(0, colonIndex);
            string value = line.Substring(colonIndex + 1);

            try
            {
                switch (key)
                {
                    // Filter configuration
                    case "FILTER_COUNT":
                        config.FilterCount = int.Parse(value);
                        break;

                    case "FILTER_NAME":
                        // Format: FILTER_NAME:1:Luminance
                        int secondColon = value.IndexOf(':');
                        if (secondColon > 0)
                        {
                            int position = int.Parse(value.Substring(0, secondColon));
                            string name = value.Substring(secondColon + 1);
                            if (position >= 1 && position <= SerialCommands.MAX_FILTER_COUNT)
                            {
                                config.FilterNames[position - 1] = name;
                            }
                        }
                        break;

                    // Motor configuration
                    case "MOTOR_SPEED":
                        config.MotorSpeed = int.Parse(value);
                        break;
                    case "MOTOR_MAX_SPEED":
                        config.MotorMaxSpeed = int.Parse(value);
                        break;
                    case "MOTOR_ACCEL":
                        config.MotorAccel = int.Parse(value);
                        break;
                    case "MOTOR_DISABLE_DELAY":
                        config.MotorDisableDelay = int.Parse(value);
                        break;
                    case "MOTOR_STEPS_PER_REV":
                        config.MotorStepsPerRev = int.Parse(value);
                        break;
                    case "MOTOR_INV":
                        config.MotorInverted = value == "1";
                        break;
                    case "ENC_INV":
                        config.EncoderInverted = value == "1";
                        break;

                    // Display configuration
                    case "DISPLAY_SIZE":
                        config.DisplaySize = value;
                        break;
                    case "DISPLAY_ROTATION":
                        config.DisplayRotation180 = value == "1";
                        break;
                    case "DISPLAY_ENABLED":
                        config.DisplayEnabled = value == "1";
                        break;
                    case "DISPLAY_BRIGHTNESS":
                        config.DisplayBrightness = int.Parse(value);
                        break;
                    case "DISPLAY_MODE":
                        config.DisplayMode = int.Parse(value);
                        break;
                    case "DISPLAY_POWER_MODE":
                        config.DisplayPowerMode = int.Parse(value);
                        break;
                    case "DISPLAY_TIMEOUT":
                        config.DisplayTimeout = int.Parse(value);
                        break;

                    // Status
                    case "STATUS_POSITION":
                        config.StatusPosition = int.Parse(value);
                        break;
                    case "STATUS_MOVING":
                        config.StatusMoving = value == "1";
                        break;
                    case "STATUS_CALIBRATED":
                        config.StatusCalibrated = value == "1";
                        break;
                    case "STATUS_ANGLE":
                        config.StatusAngle = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;

                    default:
                        tl.LogMessage("SerialCommunication.ParseConfigLine", $"Unknown key: {key}");
                        break;
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("SerialCommunication.ParseConfigLine", $"Error parsing {key}={value}: {ex.Message}");
            }
        }

        /// <summary>
        /// Set filter name for a specific position
        /// </summary>
        public void SetFilterName(int position, string filterName)
        {
            if (position < 1 || position > 8)
                throw new InvalidValueException($"Position must be between 1 and 8");

            if (string.IsNullOrEmpty(filterName))
                throw new InvalidValueException("Filter name cannot be null or empty");

            string response = SendCommand($"SN{position}:{filterName}");

            // Parse response format: SN[position]:name
            string expectedPrefix = SerialCommands.RESP_NAME + position + ":";
            if (!response.StartsWith("SN" + position + ":"))
            {
                throw new InvalidOperationException($"Unexpected response to set name command: {response}");
            }

            tl.LogMessage("SerialCommunication.SetFilterName", $"Successfully set filter {position} name to '{filterName}'");
        }

        /// <summary>
        /// Send all configured filter names to the device
        /// </summary>
        public void SendAllFilterNames(string[] filterNames, int filterCount)
        {
            if (filterNames == null)
                throw new ArgumentNullException(nameof(filterNames));

            if (filterCount < SerialCommands.MIN_FILTER_COUNT || filterCount > SerialCommands.MAX_FILTER_COUNT)
                throw new InvalidValueException($"Filter count must be between {SerialCommands.MIN_FILTER_COUNT} and {SerialCommands.MAX_FILTER_COUNT}");

            tl.LogMessage("SerialCommunication.SendAllFilterNames", $"Sending {filterCount} filter names to device");

            for (int i = 0; i < filterCount; i++)
            {
                try
                {
                    string name = filterNames[i];
                    if (string.IsNullOrWhiteSpace(name))
                        name = $"Filter{i + 1}";

                    SetFilterName(i + 1, name);
                    // Add delay between consecutive EEPROM writes to avoid overwhelming the device
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    tl.LogMessage("SerialCommunication.SendAllFilterNames", $"Error setting name for position {i + 1}: {ex.Message}");
                    // Continue with remaining names even if one fails
                }
            }

            tl.LogMessage("SerialCommunication.SendAllFilterNames", "Finished sending filter names");
        }

        /// <summary>
        /// Emergency stop
        /// </summary>
        public void EmergencyStop()
        {
            try
            {
                SendCommand(SerialCommands.CMD_STOP);
            }
            catch (Exception ex)
            {
                tl.LogMessage("SerialCommunication.EmergencyStop", $"Error during emergency stop: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Calibrate the filter wheel
        /// </summary>
        public void Calibrate()
        {
            string response = SendCommand(SerialCommands.CMD_CALIBRATE);
            if (response != SerialCommands.RESP_CALIBRATED)
            {
                throw new InvalidOperationException($"Calibration failed: {response}");
            }
        }

        /// <summary>
        /// Get device identification
        /// </summary>
        public string GetDeviceId()
        {
            string response = SendCommand(SerialCommands.CMD_DEVICE_ID);
            if (response.StartsWith(SerialCommands.RESP_DEVICE_ID))
            {
                return response.Substring(SerialCommands.RESP_DEVICE_ID.Length);
            }
            throw new FormatException($"Invalid device ID response: {response}");
        }

        /// <summary>
        /// Get firmware version
        /// </summary>
        public string GetVersion()
        {
            string response = SendCommand(SerialCommands.CMD_VERSION);
            if (response.StartsWith(SerialCommands.RESP_VERSION))
            {
                return response.Substring(SerialCommands.RESP_VERSION.Length);
            }
            throw new FormatException($"Invalid version response: {response}");
        }

        /// <summary>
        /// Read a response from the serial port
        /// </summary>
        private string ReadResponse()
        {
            try
            {
                string fullResponse = "";
                int consecutiveTimeouts = 0;
                const int maxConsecutiveTimeouts = 3;

                // Keep reading until we get a newline or multiple timeouts
                while (!fullResponse.Contains("\n") && !fullResponse.Contains("\r"))
                {
                    try
                    {
                        string chunk = serialPort.ReceiveCounted(1);
                        if (!string.IsNullOrEmpty(chunk))
                        {
                            fullResponse += chunk;
                            consecutiveTimeouts = 0; // Reset timeout counter on successful read
                        }
                    }
                    catch (TimeoutException)
                    {
                        // If we have some data and hit timeout, we're probably done
                        if (fullResponse.Length > 0)
                        {
                            consecutiveTimeouts++;
                            if (consecutiveTimeouts >= maxConsecutiveTimeouts)
                            {
                                tl.LogMessage("SerialCommunication.ReadResponse",
                                    $"Read timeout after {fullResponse.Length} chars, assuming complete");
                                break;
                            }
                        }
                        else
                        {
                            // No data yet, re-throw the timeout
                            throw;
                        }
                    }

                    // Prevent infinite loop with excessive data
                    if (fullResponse.Length > SerialCommands.MAX_COMMAND_LENGTH * 4)
                    {
                        tl.LogMessage("SerialCommunication.ReadResponse",
                            $"Response exceeded max length ({fullResponse.Length} chars), truncating");
                        break;
                    }
                }

                string trimmed = fullResponse.Trim('\r', '\n', ' ');
                tl.LogMessage("SerialCommunication.ReadResponse",
                    $"Read {fullResponse.Length} chars, trimmed to {trimmed.Length}: '{trimmed}'");
                return trimmed;
            }
            catch (Exception ex)
            {
                tl.LogMessage("SerialCommunication.ReadResponse", $"Error reading response: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Dispose of resources
        /// </summary>
        public void Dispose()
        {
            try
            {
                Disconnect();
                if (serialPort != null)
                {
                    serialPort.Dispose();
                    serialPort = null;
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("SerialCommunication.Dispose", $"Error during dispose: {ex.Message}");
            }
        }

        /// <summary>
        /// Auto-detect the COM port where the filter wheel is connected
        /// </summary>
        /// <param name="tl">TraceLogger for logging (optional)</param>
        /// <returns>The port name if found, null otherwise</returns>
        public static string AutoDetectPort(TraceLogger tl = null)
        {
            tl?.LogMessage("SerialCommunication.AutoDetectPort", "Starting auto-detection of filter wheel");

            string[] availablePorts;
            try
            {
                availablePorts = System.IO.Ports.SerialPort.GetPortNames();
            }
            catch (Exception ex)
            {
                tl?.LogMessage("SerialCommunication.AutoDetectPort", $"Error getting COM ports: {ex.Message}");
                return null;
            }

            if (availablePorts.Length == 0)
            {
                tl?.LogMessage("SerialCommunication.AutoDetectPort", "No COM ports available");
                return null;
            }

            tl?.LogMessage("SerialCommunication.AutoDetectPort", $"Found {availablePorts.Length} COM ports to test: {string.Join(", ", availablePorts)}");

            foreach (string port in availablePorts)
            {
                tl?.LogMessage("SerialCommunication.AutoDetectPort", $"Testing port {port}");

                // Create a temporary serial communication instance for testing
                SerialCommunication testComm = null;
                try
                {
                    testComm = new SerialCommunication(tl);

                    // Try to connect with a short timeout
                    testComm.Connect(port);

                    if (!testComm.IsConnected)
                    {
                        tl?.LogMessage("SerialCommunication.AutoDetectPort", $"Port {port} - connection failed");
                        continue;
                    }

                    // Send device ID command to verify it's our device
                    string response = testComm.SendCommand(SerialCommands.CMD_DEVICE_ID, 2000);

                    tl?.LogMessage("SerialCommunication.AutoDetectPort", $"Response from {port}: {response}");

                    // Check if response matches expected device ID
                    if (response.StartsWith(SerialCommands.RESP_DEVICE_ID) &&
                        response.Contains(SerialCommands.EXPECTED_DEVICE_ID_PREFIX))
                    {
                        tl?.LogMessage("SerialCommunication.AutoDetectPort", $"Filter wheel found on {port}!");
                        testComm.Disconnect();
                        testComm.Dispose();
                        return port;
                    }
                    else
                    {
                        tl?.LogMessage("SerialCommunication.AutoDetectPort", $"Port {port} - device ID mismatch");
                    }

                    // Not our device, disconnect and try next port
                    testComm.Disconnect();
                }
                catch (Exception ex)
                {
                    tl?.LogMessage("SerialCommunication.AutoDetectPort", $"Port {port} test failed: {ex.Message}");
                    try { testComm?.Disconnect(); } catch { }
                }
                finally
                {
                    try { testComm?.Dispose(); } catch { }
                }
            }

            tl?.LogMessage("SerialCommunication.AutoDetectPort", "Filter wheel not found on any port");
            return null;
        }
    }
}