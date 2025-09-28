using System;
using System.Threading;
using ASCOM.Utilities;

namespace ASCOM.autoFilterWheel.FilterWheel
{
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

                    // Wait a bit for Arduino to initialize after connection
                    Thread.Sleep(1500);

                    // Test basic connectivity first (skip device ID check for now)
                    try
                    {
                        // Try a simple command to verify communication
                        serialPort.ClearBuffers();
                        serialPort.Transmit("#VER\r");

                        // Wait for response with shorter timeout for initial test
                        string response = "";
                        int attempts = 0;
                        while (attempts < 50 && !response.Contains("VERSION") && !response.Contains("ERROR"))
                        {
                            try
                            {
                                string partial = serialPort.ReceiveCounted(1);
                                if (!string.IsNullOrEmpty(partial))
                                {
                                    response += partial;
                                }
                            }
                            catch (TimeoutException)
                            {
                                // Continue trying
                            }
                            attempts++;
                            Thread.Sleep(50);
                        }

                        response = response.Trim('\r', '\n', ' ');
                        tl.LogMessage("SerialCommunication.Connect", $"Initial response: '{response}'");

                        if (response.Contains("VERSION"))
                        {
                            tl.LogMessage("SerialCommunication.Connect", "Communication established successfully");
                            isConnected = true;
                        }
                        else
                        {
                            tl.LogMessage("SerialCommunication.Connect", $"No valid response received. Got: '{response}'");
                            isConnected = true; // Allow connection anyway for now
                        }
                    }
                    catch (Exception comEx)
                    {
                        tl.LogMessage("SerialCommunication.Connect", $"Communication test failed: {comEx.Message}");
                        throw new InvalidOperationException($"Failed to communicate with device: {comEx.Message}", comEx);
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
            lock (lockObject)
            {
                if (!IsConnected)
                    throw new NotConnectedException("Serial port is not connected");

                try
                {
                    string formattedCommand = SerialCommands.FormatCommand(command);
                    tl.LogMessage("SerialCommunication.SendCommand", $"Sending: {formattedCommand.TrimEnd('\r', '\n')}");

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
        public void MoveToPosition(int position)
        {
            if (position < 1 || position > SerialCommands.MAX_FILTERS)
                throw new InvalidValueException($"Position must be between 1 and {SerialCommands.MAX_FILTERS}");

            string command = SerialCommands.CMD_MOVE_POSITION + position;
            string response = SendCommand(command);

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
        /// Get filter name for a position
        /// </summary>
        public string GetFilterName(int position)
        {
            if (position < 1 || position > SerialCommands.MAX_FILTERS)
                throw new InvalidValueException($"Position must be between 1 and {SerialCommands.MAX_FILTERS}");

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
        public string[] GetAllFilterNames()
        {
            // First get the actual filter count from device
            int filterCount = GetFilterCountFromDevice();
            string[] names = new string[filterCount];

            for (int i = 1; i <= filterCount; i++)
            {
                try
                {
                    names[i - 1] = GetFilterName(i);
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

            if (filterCount < 3 || filterCount > 8)
                throw new InvalidValueException("Filter count must be between 3 and 8");

            tl.LogMessage("SerialCommunication.SendAllFilterNames", $"Sending {filterCount} filter names to device");

            for (int i = 0; i < filterCount; i++)
            {
                try
                {
                    string name = filterNames[i];
                    if (string.IsNullOrWhiteSpace(name))
                        name = $"Filter{i + 1}";

                    SetFilterName(i + 1, name);
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
        /// Get filter count from device
        /// </summary>
        public int GetFilterCountFromDevice()
        {
            string response = SendCommand(SerialCommands.CMD_GET_FILTERS);

            // Parse response format: F[n]
            if (response.StartsWith("F") && response.Length >= 2)
            {
                if (int.TryParse(response.Substring(1), out int count))
                {
                    return count;
                }
            }

            throw new InvalidOperationException($"Invalid filter count response: {response}");
        }

        /// <summary>
        /// Set steps per revolution
        /// </summary>
        public void SetStepsPerRevolution(int steps)
        {
            if (steps < SerialCommands.MIN_STEPS_PER_REV || steps > SerialCommands.MAX_STEPS_PER_REV)
                throw new InvalidValueException($"Steps per revolution must be between {SerialCommands.MIN_STEPS_PER_REV} and {SerialCommands.MAX_STEPS_PER_REV}");

            string command = SerialCommands.CMD_SET_STEPS_PER_REV + steps;
            string response = SendCommand(command);

            // Verify the response
            if (!response.StartsWith("SPR" + steps))
            {
                throw new InvalidOperationException($"Unexpected response to set steps per revolution command: {response}");
            }
        }

        /// <summary>
        /// Get steps per revolution
        /// </summary>
        public int GetStepsPerRevolution()
        {
            string response = SendCommand(SerialCommands.CMD_GET_STEPS_PER_REV);

            // Parse response format: SPR:xxxx
            if (response.StartsWith("SPR:"))
            {
                string stepsStr = response.Substring(4);
                if (int.TryParse(stepsStr, out int steps))
                {
                    return steps;
                }
            }

            throw new FormatException($"Invalid steps per revolution response: {response}");
        }

        /// <summary>
        /// Get complete motor configuration
        /// </summary>
        public MotorConfiguration GetMotorConfiguration()
        {
            string response = SendCommand(SerialCommands.CMD_GET_MOTOR_CONFIG);
            return ParseMotorConfigResponse(response);
        }

        /// <summary>
        /// Parse motor configuration response
        /// </summary>
        private MotorConfiguration ParseMotorConfigResponse(string response)
        {
            var config = new MotorConfiguration();

            if (!response.StartsWith("MOTOR_CONFIG:"))
            {
                throw new FormatException($"Invalid motor config response: {response}");
            }

            string configData = response.Substring(13); // Remove "MOTOR_CONFIG:"
            string[] parts = configData.Split(',');

            foreach (string part in parts)
            {
                string[] keyValue = part.Split('=');
                if (keyValue.Length == 2)
                {
                    string key = keyValue[0].Trim();
                    string value = keyValue[1].Trim();

                    switch (key)
                    {
                        case "SPEED":
                            if (int.TryParse(value, out int speed))
                                config.Speed = speed;
                            break;
                        case "MAX_SPEED":
                            if (int.TryParse(value, out int maxSpeed))
                                config.MaxSpeed = maxSpeed;
                            break;
                        case "ACCEL":
                            if (int.TryParse(value, out int accel))
                                config.Acceleration = accel;
                            break;
                        case "DISABLE_DELAY":
                            if (int.TryParse(value, out int delay))
                                config.DisableDelay = delay;
                            break;
                        case "STEPS_PER_REV":
                            if (int.TryParse(value, out int stepsPerRev))
                                config.StepsPerRevolution = stepsPerRev;
                            break;
                    }
                }
            }

            return config;
        }

        /// <summary>
        /// Read a response from the serial port
        /// </summary>
        private string ReadResponse()
        {
            try
            {
                string response = serialPort.ReceiveCounted(1);
                string fullResponse = response;

                // Keep reading until we get a newline or timeout
                while (!fullResponse.Contains("\n") && !fullResponse.Contains("\r"))
                {
                    response = serialPort.ReceiveCounted(1);
                    if (!string.IsNullOrEmpty(response))
                    {
                        fullResponse += response;
                    }

                    // Prevent infinite loop
                    if (fullResponse.Length > SerialCommands.MAX_COMMAND_LENGTH)
                    {
                        break;
                    }
                }

                return fullResponse.Trim('\r', '\n', ' ');
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
    }
}