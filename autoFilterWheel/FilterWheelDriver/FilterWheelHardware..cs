//
// ASCOM FilterWheel hardware class for autoFilterWheel
//
// Description:	 Hardware interface for ESP32-C3 based filter wheel
//
// Implements:	ASCOM FilterWheel interface version: IFilterWheelV3
// Author:		Juanjo López
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ASCOM;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using System.Threading;

namespace ASCOM.autoFilterWheel.FilterWheel
{
    // Hardware communication layer for ESP32-C3 filter wheel

    /// <summary>
    /// ASCOM FilterWheel hardware class for autoFilterWheel.
    /// </summary>
    [HardwareClass()] // Class attribute flag this as a device hardware class that needs to be disposed by the local server when it exits.
    internal static class FilterWheelHardware
    {
        // Constants used for Profile persistence
        internal const string comPortProfileName = "COM Port";
        internal const string comPortDefault = "COM1";
        internal const string traceStateProfileName = "Trace Level";
        internal const string traceStateDefault = "true";
        internal const string filterCountProfileName = "Filter Count";
        internal const string filterCountDefault = "5";
        internal const string filterNamesProfileName = "Filter Names";
        internal const string filterNamesDefault = "Luminance,Red,Green,Blue,H-Alpha,Filter6,Filter7,Filter8";

        private static string DriverProgId = ""; // ASCOM DeviceID (COM ProgID) for this driver, the value is set by the driver's class initialiser.
        private static string DriverDescription = ""; // The value is set by the driver's class initialiser.
        internal static string comPort; // COM port name (if required)
        internal static int filterCount; // Number of filters configured (3-9)
        internal static string[] filterNames; // Array of filter names
        private static bool connectedState; // Local server's connected state
        private static bool runOnce = false; // Flag to enable "one-off" activities only to run once.
        internal static Util utilities; // ASCOM Utilities object for use as required
        internal static AstroUtils astroUtilities; // ASCOM AstroUtilities object for use as required
        internal static TraceLogger tl; // Local server's trace logger object for diagnostic log with information that you specify
        private static SerialCommunication serialComm; // Serial communication handler
        private static readonly object lockObject = new object(); // Thread safety lock

        /// <summary>
        /// Initializes a new instance of the device Hardware class.
        /// </summary>
        static FilterWheelHardware()
        {
            try
            {
                // Create the hardware trace logger in the static initialiser.
                // All other initialisation should go in the InitialiseHardware method.
                tl = new TraceLogger("", "autoFilterWheel.Hardware");

                // DriverProgId has to be set here because it used by ReadProfile to get the TraceState flag.
                DriverProgId = FilterWheel.DriverProgId; // Get this device's ProgID so that it can be used to read the Profile configuration values

                // ReadProfile has to go here before anything is written to the log because it loads the TraceLogger enable / disable state.
                ReadProfile(); // Read device configuration from the ASCOM Profile store, including the trace state

                LogMessage("FilterWheelHardware", $"Static initialiser completed.");
            }
            catch (Exception ex)
            {
                try { LogMessage("FilterWheelHardware", $"Initialisation exception: {ex}"); } catch { }
                MessageBox.Show($"{ex.Message}", "Exception creating ASCOM.autoFilterWheel.FilterWheel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <summary>
        /// Place device initialisation code here
        /// </summary>
        /// <remarks>Called every time a new instance of the driver is created.</remarks>
        internal static void InitialiseHardware()
        {
            // This method will be called every time a new ASCOM client loads your driver
            LogMessage("InitialiseHardware", $"Start.");

            // Make sure that "one off" activities are only undertaken once
            if (runOnce == false)
            {
                LogMessage("InitialiseHardware", $"Starting one-off initialisation.");

                DriverDescription = FilterWheel.DriverDescription; // Get this device's Chooser description

                LogMessage("InitialiseHardware", $"ProgID: {DriverProgId}, Description: {DriverDescription}");

                connectedState = false; // Initialise connected to false
                utilities = new Util(); //Initialise ASCOM Utilities object
                astroUtilities = new AstroUtils(); // Initialise ASCOM Astronomy Utilities object

                LogMessage("InitialiseHardware", "Completed basic initialisation");

                // Add your own "one off" device initialisation here e.g. validating existence of hardware and setting up communications

                // Initialize serial communication handler
                serialComm = new SerialCommunication(tl);

                LogMessage("InitialiseHardware", $"One-off initialisation complete.");
                runOnce = true; // Set the flag to ensure that this code is not run again
            }
        }

        // PUBLIC COM INTERFACE IFilterWheelV3 IMPLEMENTATION

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialogue form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public static void SetupDialog()
        {
            // Don't permit the setup dialogue if already connected
            if (IsConnected)
                MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm(tl))
            {
                var result = F.ShowDialog();
                if (result == DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        /// <summary>Returns the list of custom action names supported by this driver.</summary>
        /// <value>An ArrayList of strings (SafeArray collection) containing the names of supported actions.</value>
        public static ArrayList SupportedActions
        {
            get
            {
                LogMessage("SupportedActions Get", "Returning empty ArrayList");
                return new ArrayList();
            }
        }

        /// <summary>Invokes the specified device-specific custom action.</summary>
        /// <param name="ActionName">A well known name agreed by interested parties that represents the action to be carried out.</param>
        /// <param name="ActionParameters">List of required parameters or an <see cref="String.Empty">Empty String</see> if none are required.</param>
        /// <returns>A string response. The meaning of returned strings is set by the driver author.
        /// <para>Suppose filter wheels start to appear with automatic wheel changers; new actions could be <c>QueryWheels</c> and <c>SelectWheel</c>. The former returning a formatted list
        /// of wheel names and the second taking a wheel name and making the change, returning appropriate values to indicate success or failure.</para>
        /// </returns>
        public static string Action(string actionName, string actionParameters)
        {
            LogMessage("Action", $"Action {actionName}, parameters {actionParameters} is not implemented");
            throw new ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        /// <summary>
        /// Transmits an arbitrary string to the device and does not wait for a response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="Command">The literal command string to be transmitted.</param>
        /// <param name="Raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        public static void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");

            // Send command to the Arduino without waiting for response
            lock (lockObject)
            {
                try
                {
                    if (!raw)
                    {
                        serialComm.SendCommandBlind(command);
                    }
                    else
                    {
                        // For raw commands, send directly without formatting
                        serialComm.SendCommandBlind(command);
                    }
                }
                catch (Exception ex)
                {
                    LogMessage("CommandBlind", $"Error: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Transmits an arbitrary string to the device and waits for a boolean response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="Command">The literal command string to be transmitted.</param>
        /// <param name="Raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        /// <returns>
        /// Returns the interpreted boolean response received from the device.
        /// </returns>
        public static bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");

            // Send command to the Arduino and interpret boolean response
            lock (lockObject)
            {
                try
                {
                    string response = serialComm.SendCommand(command);
                    // Interpret response as boolean (true if successful/non-error)
                    return !SerialCommands.IsErrorResponse(response);
                }
                catch (Exception ex)
                {
                    LogMessage("CommandBool", $"Error: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Transmits an arbitrary string to the device and waits for a string response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="Command">The literal command string to be transmitted.</param>
        /// <param name="Raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        /// <returns>
        /// Returns the string response received from the device.
        /// </returns>
        public static string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");

            // Send command to the Arduino and wait for response
            lock (lockObject)
            {
                try
                {
                    if (!raw)
                    {
                        // Command is already formatted, just send it
                        return serialComm.SendCommand(command);
                    }
                    else
                    {
                        // Raw command - send as is without formatting
                        serialComm.SendCommandBlind(command);
                        return "OK";
                    }
                }
                catch (Exception ex)
                {
                    LogMessage("CommandString", $"Error: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Deterministically release both managed and unmanaged resources that are used by this class.
        /// </summary>
        /// <remarks>
        /// Releases managed and unmanaged resources used by this hardware class.
        /// 
        /// Do not call this method from the Dispose method in your driver class.
        ///
        /// This is because this hardware class is decorated with the <see cref="HardwareClassAttribute"/> attribute and this Dispose() method will be called 
        /// automatically by the  local server executable when it is irretrievably shutting down. This gives you the opportunity to release managed and unmanaged 
        /// resources in a timely fashion and avoid any time delay between local server close down and garbage collection by the .NET runtime.
        ///
        /// For the same reason, do not call the SharedResources.Dispose() method from this method. Any resources used in the static shared resources class
        /// itself should be released in the SharedResources.Dispose() method as usual. The SharedResources.Dispose() method will be called automatically 
        /// by the local server just before it shuts down.
        /// 
        /// </remarks>
        public static void Dispose()
        {
            try { LogMessage("Dispose", $"Disposing of assets and closing down."); } catch { }

            try
            {
                // Disconnect and dispose of serial communication
                if (serialComm != null)
                {
                    serialComm.Dispose();
                    serialComm = null;
                }
            }
            catch { }

            try
            {
                // Clean up the trace logger and utility objects
                tl.Enabled = false;
                tl.Dispose();
                tl = null;
            }
            catch { }

            try
            {
                utilities.Dispose();
                utilities = null;
            }
            catch { }

            try
            {
                astroUtilities.Dispose();
                astroUtilities = null;
            }
            catch { }
        }

        /// <summary>
        /// Set True to connect to the device hardware. Set False to disconnect from the device hardware.
        /// You can also read the property to check whether it is connected. This reports the current hardware state.
        /// </summary>
        /// <value><c>true</c> if connected to the hardware; otherwise, <c>false</c>.</value>
        public static bool Connected
        {
            get
            {
                LogMessage("Connected", $"Get {IsConnected}");
                return IsConnected;
            }
            set
            {
                LogMessage("Connected", $"Set {value}");
                if (value == IsConnected)
                    return;

                if (value)
                {
                    LogMessage("Connected Set", $"Connecting to port {comPort}");

                    lock (lockObject)
                    {
                        try
                        {
                            serialComm.Connect(comPort);
                            connectedState = true;
                            LogMessage("Connected Set", "Successfully connected to filter wheel");
                        }
                        catch (Exception ex)
                        {
                            connectedState = false;
                            LogMessage("Connected Set", $"Connection failed: {ex.Message}");
                            throw new DriverException($"Failed to connect to filter wheel on {comPort}: {ex.Message}", ex);
                        }
                    }
                }
                else
                {
                    LogMessage("Connected Set", $"Disconnecting from port {comPort}");

                    lock (lockObject)
                    {
                        try
                        {
                            serialComm.Disconnect();
                        }
                        catch (Exception ex)
                        {
                            LogMessage("Connected Set", $"Error during disconnect: {ex.Message}");
                        }
                        finally
                        {
                            connectedState = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns a description of the device, such as manufacturer and model number. Any ASCII characters may be used.
        /// </summary>
        /// <value>The description.</value>
        public static string Description
        {
            get
            {
                string description = "ESP32-C3 Motorized Filter Wheel Controller";
                LogMessage("Description Get", description);
                return description;
            }
        }

        /// <summary>
        /// Descriptive and version information about this ASCOM driver.
        /// </summary>
        public static string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = $"ASCOM FilterWheel driver for ESP32-C3 controller. Version: {version.Major}.{version.Minor}. Supports 5-position filter wheel with serial communication. Compatible with device ID: {SerialCommands.EXPECTED_DEVICE_ID}";
                LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        /// <summary>
        /// A string containing only the major and minor version of the driver formatted as 'm.n'.
        /// </summary>
        public static string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = $"{version.Major}.{version.Minor}";
                LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        /// <summary>
        /// The interface version number that this device supports.
        /// </summary>
        public static short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                LogMessage("InterfaceVersion Get", "3");
                return Convert.ToInt16("3");
            }
        }

        /// <summary>
        /// The short name of the driver, for display purposes
        /// </summary>
        public static string Name
        {
            get
            {
                string name = "ESP32 Filter Wheel";
                LogMessage("Name Get", name);
                return name;
            }
        }

        /// <summary>
        /// Returns the device's operational state
        /// </summary>
        public static IStateValueCollection DeviceState
        {
            get
            {
                lock (lockObject)
                {
                    try
                    {
                        // Create a simple ArrayList to hold state values
                        var stateValues = new ArrayList();

                        // Add basic state information
                        if (IsConnected)
                        {
                            stateValues.Add(new KeyValuePair<string, object>("Connected", true));
                            stateValues.Add(new KeyValuePair<string, object>("Position", fwPosition));

                            // Add additional state if available
                            if (serialComm != null)
                            {
                                try
                                {
                                    string status = serialComm.SendCommand("STATUS");
                                    stateValues.Add(new KeyValuePair<string, object>("DeviceStatus", status));
                                }
                                catch
                                {
                                    stateValues.Add(new KeyValuePair<string, object>("DeviceStatus", "Unknown"));
                                }
                            }
                        }
                        else
                        {
                            stateValues.Add(new KeyValuePair<string, object>("Connected", false));
                        }

                        LogMessage("DeviceState Get", $"Returning {stateValues.Count} state values");
                        return new SimpleStateValueCollection(stateValues);
                    }
                    catch (Exception ex)
                    {
                        LogMessage("DeviceState Get", $"Error: {ex.Message}");
                        // Return minimal state collection on error
                        var errorStates = new ArrayList();
                        errorStates.Add(new KeyValuePair<string, object>("Connected", IsConnected));
                        return new SimpleStateValueCollection(errorStates);
                    }
                }
            }
        }

        #endregion

        #region IFilerWheel Implementation
        private static int[] fwOffsets = new int[5] { 0, 0, 0, 0, 0 }; //class level variable to hold focus offsets for 5 filters
        private static string[] fwNames = null; // Filter names will be retrieved from Arduino
        private static short fwPosition = 0; // class level variable to retain the current filter wheel position

        /// <summary>
        /// Focus offset of each filter in the wheel
        /// </summary>
        internal static int[] FocusOffsets
        {
            get
            {
                foreach (int fwOffset in fwOffsets) // Write filter offsets to the log
                {
                    LogMessage("FocusOffsets Get", fwOffset.ToString());
                }

                return fwOffsets;
            }
        }

        /// <summary>
        /// Name of each filter in the wheel
        /// </summary>
        internal static string[] Names
        {
            get
            {
                lock (lockObject)
                {
                    try
                    {
                        // Get filter names from Arduino if not already cached or if reconnected
                        if (fwNames == null && IsConnected)
                        {
                            fwNames = serialComm.GetAllFilterNames();
                        }

                        // If still null or not connected, return default names
                        if (fwNames == null)
                        {
                            fwNames = new string[] { "Filter 1", "Filter 2", "Filter 3", "Filter 4", "Filter 5" };
                        }

                        foreach (string fwName in fwNames) // Write filter names to the log
                        {
                            LogMessage("Names Get", fwName);
                        }

                        return fwNames;
                    }
                    catch (Exception ex)
                    {
                        LogMessage("Names Get", $"Error getting filter names: {ex.Message}");
                        // Return default names on error
                        return new string[] { "Filter 1", "Filter 2", "Filter 3", "Filter 4", "Filter 5" };
                    }
                }
            }
        }

        /// <summary>
        /// Sets or returns the current filter wheel position
        /// </summary>
        internal static short Position
        {
            get
            {
                lock (lockObject)
                {
                    try
                    {
                        if (IsConnected)
                        {
                            // Get current position from Arduino (returns 1-5, but ASCOM expects 0-4)
                            int arduinoPosition = serialComm.GetPosition();
                            fwPosition = (short)(arduinoPosition - 1);
                        }
                        LogMessage("Position Get", fwPosition.ToString());
                        return fwPosition;
                    }
                    catch (Exception ex)
                    {
                        LogMessage("Position Get", $"Error getting position: {ex.Message}");
                        return fwPosition; // Return last known position
                    }
                }
            }
            set
            {
                lock (lockObject)
                {
                    LogMessage("Position Set", value.ToString());

                    // ASCOM uses 0-based indexing, Arduino uses 1-based
                    if ((value < 0) | (value > SerialCommands.NUM_FILTERS - 1))
                    {
                        LogMessage("", "Throwing InvalidValueException - Position: " + value.ToString() + ", Range: 0 to " + (SerialCommands.NUM_FILTERS - 1).ToString());
                        throw new InvalidValueException("Position", value.ToString(), "0 to " + (SerialCommands.NUM_FILTERS - 1).ToString());
                    }

                    try
                    {
                        if (IsConnected)
                        {
                            // Convert from ASCOM 0-based to Arduino 1-based position
                            int arduinoPosition = value + 1;
                            serialComm.MoveToPosition(arduinoPosition);

                            // Wait for movement to complete
                            if (!serialComm.WaitForMovementComplete(SerialCommands.MOVEMENT_TIMEOUT_MS))
                            {
                                throw new DriverException("Filter wheel movement timeout");
                            }

                            fwPosition = value;
                        }
                        else
                        {
                            fwPosition = value;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogMessage("Position Set", $"Error setting position: {ex.Message}");
                        throw new DriverException($"Failed to move filter wheel to position {value}: {ex.Message}", ex);
                    }
                }
            }
        }

        #endregion

        #region Private properties and methods
        // Useful methods that can be used as required to help with driver development

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private static bool IsConnected
        {
            get
            {
                // Return the hardware connection state (serial communication status is managed through Connect/Disconnect)
                return connectedState;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private static void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new NotConnectedException(message);
            }
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal static void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "FilterWheel";
                tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, traceStateProfileName, string.Empty, traceStateDefault));
                comPort = driverProfile.GetValue(DriverProgId, comPortProfileName, string.Empty, comPortDefault);
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal static void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "FilterWheel";
                driverProfile.WriteValue(DriverProgId, traceStateProfileName, tl.Enabled.ToString());
                driverProfile.WriteValue(DriverProgId, comPortProfileName, comPort.ToString());
            }
        }

        /// <summary>
        /// Log helper function that takes identifier and message strings
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        internal static void LogMessage(string identifier, string message)
        {
            tl.LogMessageCrLf(identifier, message);
        }

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal static void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            LogMessage(identifier, msg);
        }
        #endregion
    }

    /// <summary>
    /// Simple implementation of IStateValueCollection for ASCOM compatibility
    /// </summary>
    internal class SimpleStateValueCollection : IStateValueCollection, IEnumerable<IStateValue>
    {
        private readonly ArrayList stateValues;

        public SimpleStateValueCollection(ArrayList values)
        {
            stateValues = values ?? new ArrayList();
        }

        public int Count
        {
            get { return stateValues.Count; }
        }

        public IStateValue this[int index]
        {
            get
            {
                if (index < 0 || index >= stateValues.Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                var kvp = (KeyValuePair<string, object>)stateValues[index];
                return new SimpleStateValue(kvp.Key, kvp.Value);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (KeyValuePair<string, object> kvp in stateValues)
            {
                yield return new SimpleStateValue(kvp.Key, kvp.Value);
            }
        }

        IEnumerator<IStateValue> IEnumerable<IStateValue>.GetEnumerator()
        {
            foreach (KeyValuePair<string, object> kvp in stateValues)
            {
                yield return new SimpleStateValue(kvp.Key, kvp.Value);
            }
        }
    }

    /// <summary>
    /// Simple implementation of IStateValue for ASCOM compatibility
    /// </summary>
    internal class SimpleStateValue : IStateValue
    {
        public string Name { get; private set; }
        public object Value { get; private set; }

        public SimpleStateValue(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}

