using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ASCOM.Utilities;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace ASCOM.autoFilterWheel.FilterWheel
{
    [ComVisible(false)] // Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        const string NO_PORTS_MESSAGE = "No COM ports found";
        TraceLogger tl; // Holder for a reference to the driver's trace logger
        SerialCommunication serialComm; // Serial communication for testing connection

        // Communication logging
        private Queue<string> logBuffer = new Queue<string>();
        private const int MAX_LOG_LINES = 1000;

        // Custom Angles calibration control
        private CustomAnglesControl customAnglesControl;

        // Flag to prevent event handlers from sending commands while loading configuration
        private bool isLoadingConfiguration = false;

        public SetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            tl = tlDriver;

            // Initialize serial communication for testing
            serialComm = new SerialCommunication(tl);

            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void CmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Disconnect if connected
            if (serialComm != null && serialComm.IsConnected)
            {
                serialComm.Disconnect();
            }

            // Place any validation constraint checks here and update the state variables with results from the dialogue

            // Update the COM port variable if one has been selected
            if (comboBoxComPort.SelectedItem is null) // No COM port selected
            {
                tl.LogMessage("Setup OK", $"New configuration values - COM Port: Not selected");
            }
            else if (comboBoxComPort.SelectedItem.ToString() == NO_PORTS_MESSAGE)
            {
                tl.LogMessage("Setup OK", $"New configuration values - NO COM ports detected on this PC.");
            }
            else // A valid COM port has been selected
            {
                FilterWheelHardware.comPort = (string)comboBoxComPort.SelectedItem;
                tl.LogMessage("Setup OK", $"New configuration values - COM Port: {comboBoxComPort.SelectedItem}");
            }

            // Update filter count and names
            if (comboBoxFilterCount.SelectedItem != null)
            {
                FilterWheelHardware.filterCount = int.Parse(comboBoxFilterCount.SelectedItem.ToString());
                tl.LogMessage("Setup OK", $"New configuration values - Filter Count: {FilterWheelHardware.filterCount}");

                // Update filter names array
                FilterWheelHardware.filterNames = new string[8]; // Always allocate 8 slots
                TextBox[] textBoxes = { textBoxFilter1, textBoxFilter2, textBoxFilter3, textBoxFilter4, textBoxFilter5, textBoxFilter6, textBoxFilter7, textBoxFilter8, textBoxFilter9 };

                for (int i = 0; i < 8; i++)
                {
                    if (i < FilterWheelHardware.filterCount && !string.IsNullOrWhiteSpace(textBoxes[i].Text))
                    {
                        FilterWheelHardware.filterNames[i] = SanitizeFilterName(textBoxes[i].Text.Trim());
                    }
                    else
                    {
                        FilterWheelHardware.filterNames[i] = $"Filter{i + 1}";
                    }
                }

                // Create comma-separated string for profile storage
                string filterNamesString = string.Join(",", FilterWheelHardware.filterNames);
                tl.LogMessage("Setup OK", $"New configuration values - Filter Names: {filterNamesString}");
            }
        }

        private void CmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            // Disconnect if connected
            if (serialComm != null && serialComm.IsConnected)
            {
                serialComm.Disconnect();
            }

            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("https://ascom-standards.org/");
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void RefreshComPortList()
        {
            // set the list of COM ports to those that are currently available
            comboBoxComPort.Items.Clear(); // Clear any existing entries
            using (Serial serial = new Serial()) // User the Serial component to get an extended list of COM ports
            {
                comboBoxComPort.Items.AddRange(serial.AvailableCOMPorts);
            }

            // If no ports are found include a message to this effect
            if (comboBoxComPort.Items.Count == 0)
            {
                comboBoxComPort.Items.Add(NO_PORTS_MESSAGE);
                comboBoxComPort.SelectedItem = NO_PORTS_MESSAGE;
            }

            // select the current port if possible
            if (comboBoxComPort.Items.Contains(FilterWheelHardware.comPort))
            {
                comboBoxComPort.SelectedItem = FilterWheelHardware.comPort;
            }
        }

        private void InitUI()
        {
            RefreshComPortList();

            // Add event handler for COM port selection change
            comboBoxComPort.SelectedIndexChanged += ComboBoxComPort_SelectedIndexChanged;

            // Initialize filter configuration
            comboBoxFilterCount.SelectedItem = FilterWheelHardware.filterCount.ToString();

            // Initialize filter names from the static array or defaults
            TextBox[] textBoxes = { textBoxFilter1, textBoxFilter2, textBoxFilter3, textBoxFilter4, textBoxFilter5, textBoxFilter6, textBoxFilter7, textBoxFilter8 };
            string[] defaultNames = FilterWheelHardware.filterNamesDefault.Split(',');

            for (int i = 0; i < 8; i++)
            {
                if (FilterWheelHardware.filterNames != null && i < FilterWheelHardware.filterNames.Length && !string.IsNullOrEmpty(FilterWheelHardware.filterNames[i]))
                {
                    textBoxes[i].Text = FilterWheelHardware.filterNames[i];
                }
                else if (i < defaultNames.Length)
                {
                    textBoxes[i].Text = defaultNames[i];
                }
                else
                {
                    textBoxes[i].Text = $"Filter{i + 1}";
                }
            }

            // Update visibility based on filter count
            UpdateFilterVisibility();

            // Initialize button states
            UpdateConnectionButtons(false);

            // Set compilation date
            labelCompilationDate.Text = $"Built: {GetCompilationDate():yyyy-MM-dd HH:mm}";

            // Initialize Custom Angles control
            InitializeCustomAnglesTab();

            // Set version in About tab
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            labelVersion.Text = $"Version: {version.Major}.{version.Minor}.{version.Build}";

            // Initialize status bar
            UpdateStatus("Ready");

            tl.LogMessage("InitUI", $"Set UI controls to COM Port: {comboBoxComPort.SelectedItem}, Filter Count: {FilterWheelHardware.filterCount}");
        }

        private void InitializeCustomAnglesTab()
        {
            try
            {
                tl.LogMessage("InitializeCustomAnglesTab", "Starting initialization...");

                // Create and configure the custom angles control
                customAnglesControl = new CustomAnglesControl();
                tl.LogMessage("InitializeCustomAnglesTab", "CustomAnglesControl created");

                customAnglesControl.Dock = System.Windows.Forms.DockStyle.Fill;
                tl.LogMessage("InitializeCustomAnglesTab", "Properties set");

                // Add to the custom angles tab page
                tabPageCustomAngles.Controls.Add(customAnglesControl);
                tl.LogMessage("InitializeCustomAnglesTab", $"Control added to tab. Tab has {tabPageCustomAngles.Controls.Count} controls");

                // Initialize with default values (will be updated when connected)
                customAnglesControl.lblFilterCountValue.Text = FilterWheelHardware.filterCount.ToString();

                tl.LogMessage("InitializeCustomAnglesTab", "Custom Angles tab initialized successfully");
            }
            catch (Exception ex)
            {
                tl.LogMessage("InitializeCustomAnglesTab", $"Error initializing Custom Angles tab: {ex.Message}");
                tl.LogMessage("InitializeCustomAnglesTab", $"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error al inicializar Custom Angles tab: {ex.Message}\n\nStack: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            // Bring the setup dialogue to the front of the screen
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            else
            {
                TopMost = true;
                Focus();
                BringToFront();
                TopMost = false;
            }
        }

        private void ComboBoxFilterCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update filter count and refresh visibility
            if (comboBoxFilterCount.SelectedItem != null)
            {
                FilterWheelHardware.filterCount = int.Parse(comboBoxFilterCount.SelectedItem.ToString());
                UpdateFilterVisibility();
            }
        }

        private void UpdateFilterVisibility()
        {
            // Arrays for easy access to controls
            TextBox[] textBoxes = { textBoxFilter1, textBoxFilter2, textBoxFilter3, textBoxFilter4, textBoxFilter5, textBoxFilter6, textBoxFilter7, textBoxFilter8, textBoxFilter9 };
            Label[] labels = { labelFilter1, labelFilter2, labelFilter3, labelFilter4, labelFilter5, labelFilter6, labelFilter7, labelFilter8, labelFilter9 };

            // Show/hide controls based on filter count (max 9)
            for (int i = 0; i < 9; i++)
            {
                bool visible = i < FilterWheelHardware.filterCount;
                textBoxes[i].Visible = visible;
                labels[i].Visible = visible;
            }

            // Update filter selection combobox
            PopulateFilterComboBox();
        }

        private void PopulateFilterComboBox()
        {
            comboBoxSelectFilter.Items.Clear();

            TextBox[] textBoxes = { textBoxFilter1, textBoxFilter2, textBoxFilter3, textBoxFilter4, textBoxFilter5, textBoxFilter6, textBoxFilter7, textBoxFilter8 };

            for (int i = 0; i < FilterWheelHardware.filterCount; i++)
            {
                string filterName = textBoxes[i].Text.Trim();
                if (string.IsNullOrWhiteSpace(filterName))
                {
                    filterName = $"Filter{i + 1}";
                }
                comboBoxSelectFilter.Items.Add($"{i + 1}: {filterName}");
            }

            // Select first item by default if available
            if (comboBoxSelectFilter.Items.Count > 0)
            {
                comboBoxSelectFilter.SelectedIndex = 0;
            }
        }

        private void UpdateConnectionButtons(bool connected)
        {
            btnConnect.Enabled = !connected && comboBoxComPort.SelectedItem != null && comboBoxComPort.SelectedItem.ToString() != NO_PORTS_MESSAGE;
            btnDisconnect.Enabled = connected;
            btnSet.Enabled = connected;
            btnReloadFilterNames.Enabled = connected;
            comboBoxComPort.Enabled = !connected;

            // Enable filter selection only when connected
            btnSelectFilter.Enabled = connected;
            btnStopMovement.Enabled = connected;

            // Enable motor configuration buttons when connected
            btnLoadMotorConfig.Enabled = connected;
            btnSetMotorConfig.Enabled = connected;
            btnResetMotorConfig.Enabled = connected;

            // Enable display configuration buttons when connected
            btnLoadDisplayConfig.Enabled = connected;
            btnApplyDisplayConfig.Enabled = connected;
            btnSetDisplayRotation.Enabled = connected;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            string portName = null;
            bool manualConnectionFailed = false;

            // Clear log when connecting
            ClearLog();
            UpdateStatus("Connecting...");

            // If no port selected or invalid, try auto-detection immediately
            if (comboBoxComPort.SelectedItem == null || comboBoxComPort.SelectedItem.ToString() == NO_PORTS_MESSAGE)
            {
                tl.LogMessage("BtnConnect_Click", "No port selected, attempting auto-detection");
                AddToLog("INFO", "No port selected, searching for device...");
                UpdateStatus("Searching for device...");

                portName = AutoDetectPort();

                if (portName == null)
                {
                    UpdateStatus("Device not found");
                    MessageBox.Show("Could not find the filter wheel on any available COM port.\n\nPlease make sure:\n- The device is connected\n- Drivers are installed\n- The device is powered on", "Device Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Update combo box selection
                comboBoxComPort.SelectedItem = portName;
                AddToLog("INFO", $"Device found on {portName}!");
            }
            else
            {
                // Port is selected, try to connect to it first
                portName = comboBoxComPort.SelectedItem.ToString();
            }

            try
            {
                tl.LogMessage("BtnConnect_Click", $"Attempting to connect to {portName}");
                AddToLog("INFO", $"Connecting to {portName}...");
                UpdateStatus($"Connecting to {portName}...");

                serialComm.Connect(portName);

                if (serialComm.IsConnected)
                {
                    tl.LogMessage("BtnConnect_Click", "Connected successfully, retrieving filter configuration");
                    AddToLog("INFO", "Connected successfully!");
                    UpdateStatus("Reading filter configuration...");

                    // Retrieve all configuration from device using GETCONFIG
                    // This includes: filter count, filter names, motor config, display config, and current status
                    RetrieveFilterConfiguration();

                    // Initialize custom angles control with connection
                    if (customAnglesControl != null)
                    {
                        customAnglesControl.Initialize(serialComm, tl, FilterWheelHardware.filterCount, SendCommandWithLog);
                        customAnglesControl.UpdateConnectionState(true);
                    }

                    UpdateConnectionButtons(true);
                    UpdateStatus($"Connected to {portName}");
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnConnect_Click", $"Connection to {portName} failed: {ex.Message}");
                AddToLog("ERR", $"Connection to {portName} failed: {ex.Message}");
                UpdateStatus($"Connection failed");

                // If manual connection failed, try auto-detection as fallback
                if (!manualConnectionFailed && comboBoxComPort.SelectedItem != null &&
                    comboBoxComPort.SelectedItem.ToString() != NO_PORTS_MESSAGE)
                {
                    manualConnectionFailed = true;
                    tl.LogMessage("BtnConnect_Click", "Manual connection failed, attempting auto-detection...");
                    AddToLog("WARN", "Manual connection failed, searching for device on all ports...");
                    UpdateStatus("Searching for device...");

                    string detectedPort = AutoDetectPort();

                    if (detectedPort != null && detectedPort != portName)
                    {
                        // Found device on a different port, try connecting
                        AddToLog("INFO", $"Device found on {detectedPort}, attempting connection...");
                        UpdateStatus($"Connecting to {detectedPort}...");
                        comboBoxComPort.SelectedItem = detectedPort;

                        try
                        {
                            serialComm.Connect(detectedPort);

                            if (serialComm.IsConnected)
                            {
                                tl.LogMessage("BtnConnect_Click", $"Auto-detection successful! Connected to {detectedPort}");
                                AddToLog("INFO", $"Connected successfully to {detectedPort}!");
                                UpdateStatus("Reading filter configuration...");

                                // Retrieve all configuration from device using GETCONFIG
                                RetrieveFilterConfiguration();

                                // Initialize custom angles control
                                if (customAnglesControl != null)
                                {
                                    customAnglesControl.Initialize(serialComm, tl, FilterWheelHardware.filterCount, SendCommandWithLog);
                                    customAnglesControl.UpdateConnectionState(true);
                                }

                                UpdateConnectionButtons(true);
                                UpdateStatus($"Connected to {detectedPort}");
                                return; // Success!
                            }
                        }
                        catch (Exception autoEx)
                        {
                            tl.LogMessage("BtnConnect_Click", $"Auto-detection connection failed: {autoEx.Message}");
                            AddToLog("ERR", $"Auto-detection connection failed: {autoEx.Message}");
                            UpdateStatus("Connection failed");
                        }
                    }
                }

                // All connection attempts failed
                UpdateStatus("Connection failed");
                MessageBox.Show($"Failed to connect to {portName}.\n\nError: {ex.Message}\n\nPlease verify:\n- The device is connected\n- The correct COM port is selected\n- No other application is using the port", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateConnectionButtons(false);
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Update custom angles connection state
                if (customAnglesControl != null)
                {
                    customAnglesControl.UpdateConnectionState(false);
                }

                if (serialComm != null && serialComm.IsConnected)
                {
                    serialComm.Disconnect();
                    tl.LogMessage("BtnDisconnect_Click", "Disconnected successfully");
                }
                UpdateConnectionButtons(false);
                UpdateStatus("Disconnected");
                MessageBox.Show("Disconnected successfully.", "Disconnected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnDisconnect_Click", $"Disconnect error: {ex.Message}");
                UpdateStatus("Disconnect error");
                MessageBox.Show($"Error during disconnect: {ex.Message}", "Disconnect Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UpdateConnectionButtons(false);
            }
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnSet_Click", "Sending filter configuration to device");
                AddToLog("INFO", "Starting configuration save...");

                // Send filter count first - correct format: FC3, FC4, FC5, etc.
                int filterCount = int.Parse(comboBoxFilterCount.SelectedItem.ToString());
                tl.LogMessage("BtnSet_Click", $"Sending filter count: FC{filterCount}");
                string fcResponse = SendCommandWithLog($"FC{filterCount}");
                tl.LogMessage("BtnSet_Click", $"Filter count response: {fcResponse}");

                // Get filter names from textboxes
                TextBox[] textBoxes = { textBoxFilter1, textBoxFilter2, textBoxFilter3, textBoxFilter4, textBoxFilter5, textBoxFilter6, textBoxFilter7, textBoxFilter8, textBoxFilter9 };

                // Send filter names one by one - correct format: SN1:Luminance, SN2:Red, etc.
                for (int i = 0; i < filterCount; i++)
                {
                    string filterName = textBoxes[i].Text.Trim();
                    if (string.IsNullOrWhiteSpace(filterName))
                        filterName = $"Filter{i + 1}";

                    // Sanitize filter name (remove problematic characters, limit length)
                    filterName = SanitizeFilterName(filterName);

                    string snCommand = $"SN{i + 1}:{filterName}";
                    tl.LogMessage("BtnSet_Click", $"Sending filter name: {snCommand}");
                    string snResponse = SendCommandWithLog(snCommand);
                    tl.LogMessage("BtnSet_Click", $"Filter name response: {snResponse}");
                    // Add delay between consecutive EEPROM writes
                    System.Threading.Thread.Sleep(100);
                }

                tl.LogMessage("BtnSet_Click", "Filter configuration sent successfully");
                AddToLog("INFO", "Configuration saved successfully!");
                MessageBox.Show($"Filter configuration sent successfully!\nFilter count: {filterCount}\nAll filter names updated.", "Configuration Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnSet_Click", $"Error sending configuration: {ex.Message}");
                AddToLog("ERR", $"Configuration save failed: {ex.Message}");
                MessageBox.Show($"Error sending configuration: {ex.Message}", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RetrieveFilterConfiguration()
        {
            try
            {
                AddToLog("INFO", "Retrieving device configuration...");
                UpdateStatus("Reading configuration...");

                // Use new GETCONFIG command to get all configuration at once
                DeviceConfig config = serialComm.GetDeviceConfig();

                // Apply configuration to UI
                ApplyDeviceConfigToUI(config);

                tl.LogMessage("RetrieveFilterConfiguration", "Configuration retrieved successfully using GETCONFIG");
                AddToLog("INFO", "Configuration retrieved successfully!");
            }
            catch (Exception ex)
            {
                tl.LogMessage("RetrieveFilterConfiguration", $"Error retrieving configuration: {ex.Message}");
                AddToLog("ERR", $"Error retrieving configuration: {ex.Message}");

                // Fallback to old method if GETCONFIG fails
                tl.LogMessage("RetrieveFilterConfiguration", "Falling back to individual commands");
                AddToLog("WARN", "Using fallback method...");
                RetrieveFilterConfigurationLegacy();
            }
        }

        /// <summary>
        /// Apply device configuration to UI controls
        /// </summary>
        private void ApplyDeviceConfigToUI(DeviceConfig config)
        {
            // Set flag to prevent event handlers from sending commands
            isLoadingConfiguration = true;

            try
            {
                // Filter configuration
                FilterWheelHardware.filterCount = config.FilterCount;
                comboBoxFilterCount.SelectedItem = config.FilterCount.ToString();
                tl.LogMessage("ApplyDeviceConfigToUI", $"Filter count: {config.FilterCount}");
                AddToLog("INFO", $"Device has {config.FilterCount} filters");

                // Update filter names
                FilterWheelHardware.filterNames = new string[9];
                TextBox[] textBoxes = { textBoxFilter1, textBoxFilter2, textBoxFilter3, textBoxFilter4, textBoxFilter5, textBoxFilter6, textBoxFilter7, textBoxFilter8, textBoxFilter9 };

                for (int i = 0; i < 9; i++)
                {
                    if (i < config.FilterCount && !string.IsNullOrEmpty(config.FilterNames[i]))
                    {
                        FilterWheelHardware.filterNames[i] = config.FilterNames[i];
                        textBoxes[i].Text = config.FilterNames[i];
                        AddToLog("INFO", $"Filter {i + 1}: {config.FilterNames[i]}");
                    }
                    else
                    {
                        FilterWheelHardware.filterNames[i] = $"Filter{i + 1}";
                        textBoxes[i].Text = $"Filter{i + 1}";
                    }
                }

                UpdateFilterVisibility();

                // Motor configuration
                numericMotorSpeed.Value = Math.Min(numericMotorSpeed.Maximum, Math.Max(numericMotorSpeed.Minimum, config.MotorSpeed));
                numericMaxSpeed.Value = Math.Min(numericMaxSpeed.Maximum, Math.Max(numericMaxSpeed.Minimum, config.MotorMaxSpeed));
                numericAcceleration.Value = Math.Min(numericAcceleration.Maximum, Math.Max(numericAcceleration.Minimum, config.MotorAccel));
                numericDisableDelay.Value = Math.Min(numericDisableDelay.Maximum, Math.Max(numericDisableDelay.Minimum, config.MotorDisableDelay));
                numericStepsPerRev.Value = Math.Min(numericStepsPerRev.Maximum, Math.Max(numericStepsPerRev.Minimum, config.MotorStepsPerRev));
                chkMotorInverted.Checked = config.MotorInverted;
                chkEncoderInverted.Checked = config.EncoderInverted;

                tl.LogMessage("ApplyDeviceConfigToUI", "Motor configuration applied");

                // Display configuration
                radioDisplayInverted.Checked = config.DisplayRotation180;
                radioDisplayNormal.Checked = !config.DisplayRotation180;
                chkDisplayEnabled.Checked = config.DisplayEnabled;
                trackBarBrightness.Value = Math.Min(255, Math.Max(0, config.DisplayBrightness));
                labelBrightnessValue.Text = config.DisplayBrightness.ToString();

                radioDisplayDetailed.Checked = config.DisplayMode == 1;
                radioDisplayMinimal.Checked = config.DisplayMode == 0;

                switch (config.DisplayPowerMode)
                {
                    case 0:
                        radioPowerAuto.Checked = true;
                        break;
                    case 1:
                        radioPowerAlwaysOn.Checked = true;
                        break;
                    case 2:
                        radioPowerAlwaysOff.Checked = true;
                        break;
                }

                numericDisplayTimeout.Value = Math.Min(65535, Math.Max(0, config.DisplayTimeout));

                tl.LogMessage("ApplyDeviceConfigToUI", "Display configuration applied");

                // Status information
                tl.LogMessage("ApplyDeviceConfigToUI", $"Current position: {config.StatusPosition}, Moving: {config.StatusMoving}, Calibrated: {config.StatusCalibrated}, Angle: {config.StatusAngle}");

                // Set current position in combo box
                if (config.StatusPosition >= 1 && config.StatusPosition <= config.FilterCount)
                {
                    comboBoxSelectFilter.SelectedIndex = config.StatusPosition - 1;
                    AddToLog("INFO", $"Filter wheel is at position {config.StatusPosition}");
                }
            }
            finally
            {
                // Clear flag to allow event handlers to work normally
                isLoadingConfiguration = false;
            }
        }

        /// <summary>
        /// Legacy method - retrieve configuration using individual commands
        /// </summary>
        private void RetrieveFilterConfigurationLegacy()
        {
            try
            {
                // Get filter count from device (GF command)
                string response = SendCommandWithLog("GF");
                if (response.StartsWith("F"))
                {
                    int deviceFilterCount = int.Parse(response.Substring(1));
                    if (deviceFilterCount >= 3 && deviceFilterCount <= 8)
                    {
                        FilterWheelHardware.filterCount = deviceFilterCount;
                        comboBoxFilterCount.SelectedItem = deviceFilterCount.ToString();
                        tl.LogMessage("RetrieveFilterConfigurationLegacy", $"Retrieved filter count: {deviceFilterCount}");
                        AddToLog("INFO", $"Device has {deviceFilterCount} filters");
                    }
                }

                // Get filter names from device
                string[] retrievedNames = new string[FilterWheelHardware.filterCount];
                for (int i = 1; i <= FilterWheelHardware.filterCount; i++)
                {
                    try
                    {
                        string nameResponse = SendCommandWithLog($"GN{i}");
                        string prefix = $"N{i}:";
                        if (nameResponse.StartsWith(prefix))
                        {
                            retrievedNames[i - 1] = nameResponse.Substring(prefix.Length);
                            AddToLog("INFO", $"Filter {i}: {retrievedNames[i - 1]}");
                        }
                        else
                        {
                            retrievedNames[i - 1] = $"Filter{i}";
                        }
                        System.Threading.Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                        tl.LogMessage("RetrieveFilterConfigurationLegacy", $"Error getting name for position {i}: {ex.Message}");
                        retrievedNames[i - 1] = $"Filter{i}";
                    }
                }

                // Update UI
                FilterWheelHardware.filterNames = new string[9];
                TextBox[] textBoxes = { textBoxFilter1, textBoxFilter2, textBoxFilter3, textBoxFilter4, textBoxFilter5, textBoxFilter6, textBoxFilter7, textBoxFilter8, textBoxFilter9 };

                for (int i = 0; i < 9; i++)
                {
                    if (i < retrievedNames.Length)
                    {
                        FilterWheelHardware.filterNames[i] = retrievedNames[i];
                        textBoxes[i].Text = retrievedNames[i];
                    }
                    else
                    {
                        FilterWheelHardware.filterNames[i] = $"Filter{i + 1}";
                        textBoxes[i].Text = $"Filter{i + 1}";
                    }
                }

                UpdateFilterVisibility();
                AddToLog("INFO", "Configuration retrieved (legacy method)");
            }
            catch (Exception ex)
            {
                tl.LogMessage("RetrieveFilterConfigurationLegacy", $"Error: {ex.Message}");
                AddToLog("ERR", $"Error: {ex.Message}");
            }
        }

        private void ComboBoxComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update connect button state when port selection changes
            UpdateConnectionButtons(serialComm != null && serialComm.IsConnected);
        }

        private void BtnRefreshPorts_Click(object sender, EventArgs e)
        {
            tl.LogMessage("BtnRefreshPorts_Click", "Refreshing COM port list");
            RefreshComPortList();
        }

        private void BtnSetPort_Click(object sender, EventArgs e)
        {
            if (comboBoxComPort.SelectedItem == null || comboBoxComPort.SelectedItem.ToString() == NO_PORTS_MESSAGE)
            {
                MessageBox.Show("Please select a valid COM port.", "No Port Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string selectedPort = comboBoxComPort.SelectedItem.ToString();
                FilterWheelHardware.comPort = selectedPort;

                tl.LogMessage("BtnSetPort_Click", $"Port set to {selectedPort}, closing dialog");

                // Close the dialog
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnSetPort_Click", $"Error: {ex.Message}");
                MessageBox.Show($"Error setting port: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReloadFilterNames_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnReloadFilterNames_Click", "Reloading filter names from device");
                AddToLog("INFO", "Reloading filter names from device...");

                RetrieveFilterConfiguration();

                MessageBox.Show("Filter names reloaded successfully!", "Reload Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnReloadFilterNames_Click", $"Error reloading filter names: {ex.Message}");
                AddToLog("ERR", $"Error reloading filter names: {ex.Message}");
                MessageBox.Show($"Error reloading filter names: {ex.Message}", "Reload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSelectFilter_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxSelectFilter.SelectedItem == null)
            {
                MessageBox.Show("Please select a filter position.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Extract position number from "1: Luminance" format
                string selected = comboBoxSelectFilter.SelectedItem.ToString();
                int colonIndex = selected.IndexOf(':');
                if (colonIndex > 0)
                {
                    string positionStr = selected.Substring(0, colonIndex).Trim();
                    int position = int.Parse(positionStr);

                    string command = $"MP{position}";
                    tl.LogMessage("BtnSelectFilter_Click", $"Moving to filter position {position}");
                    AddToLog("INFO", $"Moving to filter position {position}...");

                    // Use extended timeout for movement command (can take up to 10 seconds)
                    string response = SendCommandWithLog(command, SerialCommands.MOVEMENT_TIMEOUT_MS);

                    tl.LogMessage("BtnSelectFilter_Click", $"Response: {response}");
                    AddToLog("INFO", $"Moved to filter position {position}");
                    MessageBox.Show($"Filter wheel moved to position {position}!", "Movement Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnSelectFilter_Click", $"Error: {ex.Message}");
                AddToLog("ERR", $"Filter selection failed: {ex.Message}");
                MessageBox.Show($"Error selecting filter: {ex.Message}", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStopMovement_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Not connected to the device.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnStopMovement_Click", "Sending emergency stop command");
                AddToLog("WARN", "Emergency stop requested!");

                string response = SendCommandWithLog("STOP");

                tl.LogMessage("BtnStopMovement_Click", $"Response: {response}");
                AddToLog("WARN", "Movement stopped!");
                MessageBox.Show("Filter wheel movement stopped!", "Emergency Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnStopMovement_Click", $"Error: {ex.Message}");
                AddToLog("ERR", $"Stop command failed: {ex.Message}");
                MessageBox.Show($"Error stopping movement: {ex.Message}", "Stop Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gets the compilation date of the assembly
        /// </summary>
        private DateTime GetCompilationDate()
        {
            try
            {
                // Get the assembly file path
                string assemblyPath = Assembly.GetExecutingAssembly().Location;

                // Get the last write time (compilation date)
                return File.GetLastWriteTime(assemblyPath);
            }
            catch (Exception ex)
            {
                tl?.LogMessage("GetCompilationDate", $"Error getting compilation date: {ex.Message}");
                // Return a default date if we can't get the actual compilation date
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Adds a message to the communication log with automatic scroll to bottom
        /// </summary>
        private void AddToLog(string direction, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, string>(AddToLog), direction, message);
                return;
            }

            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            string logEntry = $"{timestamp} {direction}: {message}";

            // Add to buffer
            logBuffer.Enqueue(logEntry);

            // Remove old entries if buffer is too large
            while (logBuffer.Count > MAX_LOG_LINES)
            {
                logBuffer.Dequeue();
            }

            // Update textbox
            if (textBoxLog != null)
            {
                // Update text
                textBoxLog.Text = string.Join(Environment.NewLine, logBuffer);

                // Force scroll to bottom using Win32 message (works even when control is not focused)
                Win32ScrollToBottom(textBoxLog);
            }
        }

        /// <summary>
        /// Forces a TextBox to scroll to the bottom using Win32 API
        /// This works reliably even when the control is not focused
        /// </summary>
        private void Win32ScrollToBottom(TextBox textBox)
        {
            const int WM_VSCROLL = 0x115;
            const int SB_BOTTOM = 7;

            // Send scroll to bottom message
            SendMessage(textBox.Handle, WM_VSCROLL, (IntPtr)SB_BOTTOM, IntPtr.Zero);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Clear the communication log
        /// </summary>
        private void ClearLog()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ClearLog));
                return;
            }

            logBuffer.Clear();
            if (textBoxLog != null)
            {
                textBoxLog.Clear();
            }
        }

        /// <summary>
        /// Update status bar message
        /// </summary>
        private void UpdateStatus(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateStatus), message);
                return;
            }

            if (toolStripStatusLabel != null)
            {
                toolStripStatusLabel.Text = message;
                statusStrip.Refresh();
            }
        }

        /// <summary>
        /// Send command with logging
        /// </summary>
        private string SendCommandWithLog(string command)
        {
            return SendCommandWithLog(command, SerialCommands.COMMAND_TIMEOUT_MS);
        }

        /// <summary>
        /// Send command with logging and custom timeout
        /// </summary>
        private string SendCommandWithLog(string command, int timeoutMs)
        {
            AddToLog("TX", command);
            try
            {
                string response = serialComm.SendCommand(command, timeoutMs);
                AddToLog("RX", response);
                return response;
            }
            catch (Exception ex)
            {
                AddToLog("ERR", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Open Driver Repository link
        /// </summary>
        private void LinkLabelDriverRepo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/juanjol/autoFilterWheel_ASCOM_drivers");
            }
            catch (Exception ex)
            {
                tl.LogMessage("LinkLabelDriverRepo_LinkClicked", $"Error opening link: {ex.Message}");
                MessageBox.Show($"Could not open link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Open Firmware Repository link
        /// </summary>
        private void LinkLabelFirmwareRepo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/juanjol/autoFilterWheel");
            }
            catch (Exception ex)
            {
                tl.LogMessage("LinkLabelFirmwareRepo_LinkClicked", $"Error opening link: {ex.Message}");
                MessageBox.Show($"Could not open link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Open GitHub profile link
        /// </summary>
        private void LinkLabelGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/juanjol");
            }
            catch (Exception ex)
            {
                tl.LogMessage("LinkLabelGitHub_LinkClicked", $"Error opening link: {ex.Message}");
                MessageBox.Show($"Could not open link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validates and sanitizes filter name to ensure serial protocol compatibility
        /// Excludes only: : (separator), \n, \r, # (command prefix)
        /// </summary>
        private string SanitizeFilterName(string filterName)
        {
            if (string.IsNullOrEmpty(filterName))
                return filterName;

            // Remove problematic characters that would break serial protocol
            string sanitized = filterName.Replace(":", "")
                                         .Replace("\n", "")
                                         .Replace("\r", "")
                                         .Replace("#", "");

            // Trim to maximum length
            if (sanitized.Length > SerialCommands.MAX_FILTER_NAME_LENGTH)
                sanitized = sanitized.Substring(0, SerialCommands.MAX_FILTER_NAME_LENGTH);

            return sanitized;
        }

        /// <summary>
        /// Auto-detect the COM port where the filter wheel is connected
        /// </summary>
        private string AutoDetectPort()
        {
            // Use the static method from SerialCommunication for consistency
            return SerialCommunication.AutoDetectPort(tl);
        }

        /// <summary>
        /// Load motor configuration from device (without UI messages)
        /// </summary>
        private void LoadMotorConfigFromDevice()
        {
            if (!serialComm.IsConnected)
                return;

            // Send GMC command to get motor configuration
            // Response format: MOTOR_CONFIG:SPEED=4000,MAX_SPEED=5000,ACCEL=100000,DISABLE_DELAY=1000,STEPS_PER_REV=35500,MOTOR_INV=0,ENC_INV=0
            string response = SendCommandWithLog("GMC");

            if (response.StartsWith("MOTOR_CONFIG:"))
            {
                string configData = response.Substring("MOTOR_CONFIG:".Length);
                string[] pairs = configData.Split(',');

                foreach (string pair in pairs)
                {
                    string[] keyValue = pair.Split('=');
                    if (keyValue.Length == 2)
                    {
                        string key = keyValue[0].Trim();
                        string value = keyValue[1].Trim();

                        switch (key)
                        {
                            case "SPEED":
                                if (int.TryParse(value, out int speed))
                                    numericMotorSpeed.Value = Math.Min(numericMotorSpeed.Maximum, Math.Max(numericMotorSpeed.Minimum, speed));
                                break;
                            case "MAX_SPEED":
                                if (int.TryParse(value, out int maxSpeed))
                                    numericMaxSpeed.Value = Math.Min(numericMaxSpeed.Maximum, Math.Max(numericMaxSpeed.Minimum, maxSpeed));
                                break;
                            case "ACCEL":
                                if (int.TryParse(value, out int accel))
                                    numericAcceleration.Value = Math.Min(numericAcceleration.Maximum, Math.Max(numericAcceleration.Minimum, accel));
                                break;
                            case "DISABLE_DELAY":
                                if (int.TryParse(value, out int disableDelay))
                                    numericDisableDelay.Value = Math.Min(numericDisableDelay.Maximum, Math.Max(numericDisableDelay.Minimum, disableDelay));
                                break;
                            case "STEPS_PER_REV":
                                if (int.TryParse(value, out int stepsPerRev))
                                    numericStepsPerRev.Value = Math.Min(numericStepsPerRev.Maximum, Math.Max(numericStepsPerRev.Minimum, stepsPerRev));
                                break;
                            case "MOTOR_INV":
                                chkMotorInverted.Checked = value == "1";
                                break;
                            case "ENC_INV":
                                chkEncoderInverted.Checked = value == "1";
                                break;
                        }
                    }
                }
            }
            else
            {
                throw new InvalidOperationException($"Unexpected response: {response}");
            }
        }

        /// <summary>
        /// Load motor configuration from the filter wheel
        /// </summary>
        private void BtnLoadMotorConfig_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnLoadMotorConfig_Click", "Loading motor configuration from device");
                AddToLog("INFO", "Loading motor configuration...");

                LoadMotorConfigFromDevice();

                tl.LogMessage("BtnLoadMotorConfig_Click", "Motor configuration loaded successfully");
                AddToLog("INFO", "Motor configuration loaded successfully!");
                MessageBox.Show("Motor configuration loaded from device!", "Load Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnLoadMotorConfig_Click", $"Error loading motor configuration: {ex.Message}");
                AddToLog("ERR", $"Error loading motor configuration: {ex.Message}");
                MessageBox.Show($"Error loading motor configuration: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Apply motor configuration to the filter wheel
        /// </summary>
        private void BtnSetMotorConfig_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnSetMotorConfig_Click", "Sending motor configuration to device");
                AddToLog("INFO", "Applying motor configuration...");

                // Send individual motor configuration commands
                SendCommandWithLog($"MS{numericMotorSpeed.Value}");
                SendCommandWithLog($"MXS{numericMaxSpeed.Value}");
                SendCommandWithLog($"MA{numericAcceleration.Value}");
                SendCommandWithLog($"MDD{numericDisableDelay.Value}");

                // Send motor inversion command
                SendCommandWithLog(chkMotorInverted.Checked ? "MINV1" : "MINV0");

                // Send encoder inversion command
                SendCommandWithLog(chkEncoderInverted.Checked ? "ENCINV1" : "ENCINV0");

                tl.LogMessage("BtnSetMotorConfig_Click", "Motor configuration applied successfully");
                AddToLog("INFO", "Motor configuration applied successfully!");
                MessageBox.Show("Motor configuration applied to device!", "Apply Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnSetMotorConfig_Click", $"Error applying motor configuration: {ex.Message}");
                AddToLog("ERR", $"Error applying motor configuration: {ex.Message}");
                MessageBox.Show($"Error applying motor configuration: {ex.Message}", "Apply Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Reset motor configuration to defaults
        /// </summary>
        private void BtnResetMotorConfig_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to reset motor configuration to defaults?", "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            try
            {
                tl.LogMessage("BtnResetMotorConfig_Click", "Resetting motor configuration to defaults");
                AddToLog("WARN", "Resetting motor configuration to defaults...");

                // Send RMC command to reset motor configuration
                string response = SendCommandWithLog("RMC");

                if (response == "MOTOR_CONFIG_RESET")
                {
                    // Reload configuration from device
                    BtnLoadMotorConfig_Click(sender, e);

                    tl.LogMessage("BtnResetMotorConfig_Click", "Motor configuration reset successfully");
                    AddToLog("INFO", "Motor configuration reset to defaults!");
                    MessageBox.Show("Motor configuration reset to defaults!", "Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    throw new InvalidOperationException($"Unexpected response: {response}");
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnResetMotorConfig_Click", $"Error resetting motor configuration: {ex.Message}");
                AddToLog("ERR", $"Error resetting motor configuration: {ex.Message}");
                MessageBox.Show($"Error resetting motor configuration: {ex.Message}", "Reset Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Update brightness label during scroll (visual feedback only)
        /// </summary>
        private void TrackBarBrightness_Scroll(object sender, EventArgs e)
        {
            labelBrightnessValue.Text = trackBarBrightness.Value.ToString();
        }

        /// <summary>
        /// Send brightness to device when user releases mouse (after scroll ends)
        /// </summary>
        private void TrackBarBrightness_MouseUp(object sender, MouseEventArgs e)
        {
            // Don't send commands while loading configuration
            if (isLoadingConfiguration)
                return;

            // Send brightness command to device after scroll ends
            if (serialComm != null && serialComm.IsConnected)
            {
                try
                {
                    string command = $"BRIGHT{trackBarBrightness.Value}";
                    tl.LogMessage("TrackBarBrightness_MouseUp", $"Sending brightness: {trackBarBrightness.Value}");
                    SendCommandWithLog(command);
                }
                catch (Exception ex)
                {
                    tl.LogMessage("TrackBarBrightness_MouseUp", $"Error sending brightness: {ex.Message}");
                    // No mostramos MessageBox para no interrumpir la experiencia del usuario
                }
            }
        }

        /// <summary>
        /// Load display configuration from device (without UI messages)
        /// </summary>
        private void LoadDisplayConfigFromDevice()
        {
            if (!serialComm.IsConnected)
                return;

            // Send DISPLAY command to get display configuration
            // Response format: DISPLAY:Size=128x64,Rotation=Normal|180°,Enabled=Yes|No,Update=100ms,Brightness=128
            string response = SendCommandWithLog("DISPLAY");

            if (response.StartsWith("DISPLAY:"))
            {
                string configData = response.Substring("DISPLAY:".Length);
                string[] pairs = configData.Split(',');

                foreach (string pair in pairs)
                {
                    string[] keyValue = pair.Split('=');
                    if (keyValue.Length == 2)
                    {
                        string key = keyValue[0].Trim();
                        string value = keyValue[1].Trim();

                        switch (key)
                        {
                            case "Rotation":
                                radioDisplayInverted.Checked = value.Contains("180");
                                radioDisplayNormal.Checked = !value.Contains("180");
                                break;
                            case "Enabled":
                                chkDisplayEnabled.Checked = value.Equals("Yes", StringComparison.OrdinalIgnoreCase);
                                break;
                            case "Brightness":
                                if (int.TryParse(value, out int brightness))
                                {
                                    trackBarBrightness.Value = Math.Min(255, Math.Max(0, brightness));
                                }
                                break;
                        }
                    }
                }

                // Get display timeout setting
                try
                {
                    string timeoutResponse = SendCommandWithLog("DISPTIMEOUT");
                    // Response format: DISPTIMEOUT:[seconds]:[Seconds|Never]
                    if (timeoutResponse.StartsWith("DISPTIMEOUT:"))
                    {
                        string timeoutData = timeoutResponse.Substring("DISPTIMEOUT:".Length);
                        string[] timeoutParts = timeoutData.Split(':');
                        if (timeoutParts.Length >= 1 && int.TryParse(timeoutParts[0], out int timeout))
                        {
                            numericDisplayTimeout.Value = Math.Min(65535, Math.Max(0, timeout));
                        }
                    }
                }
                catch (Exception ex)
                {
                    tl.LogMessage("LoadDisplayConfigFromDevice", $"Could not get display timeout: {ex.Message}");
                }
            }
            else
            {
                throw new InvalidOperationException($"Unexpected response: {response}");
            }
        }

        /// <summary>
        /// Load display configuration from the filter wheel
        /// </summary>
        private void BtnLoadDisplayConfig_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnLoadDisplayConfig_Click", "Loading display configuration from device");
                AddToLog("INFO", "Loading display configuration...");

                LoadDisplayConfigFromDevice();

                tl.LogMessage("BtnLoadDisplayConfig_Click", "Display configuration loaded successfully");
                AddToLog("INFO", "Display configuration loaded successfully!");
                MessageBox.Show("Display configuration loaded from device!", "Load Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnLoadDisplayConfig_Click", $"Error loading display configuration: {ex.Message}");
                AddToLog("ERR", $"Error loading display configuration: {ex.Message}");
                MessageBox.Show($"Error loading display configuration: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handle display rotation change in real-time
        /// </summary>
        private void RadioDisplayRotation_CheckedChanged(object sender, EventArgs e)
        {
            // Don't send commands while loading configuration
            if (isLoadingConfiguration)
                return;

            if (serialComm != null && serialComm.IsConnected && ((RadioButton)sender).Checked)
            {
                try
                {
                    string command = $"ROTATE{(radioDisplayInverted.Checked ? "1" : "0")}";
                    tl.LogMessage("RadioDisplayRotation_CheckedChanged", $"Sending rotation command: {command}");
                    SendCommandWithLog(command);
                }
                catch (Exception ex)
                {
                    tl.LogMessage("RadioDisplayRotation_CheckedChanged", $"Error sending rotation: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Handle display mode change in real-time
        /// </summary>
        private void RadioDisplayMode_CheckedChanged(object sender, EventArgs e)
        {
            // Don't send commands while loading configuration
            if (isLoadingConfiguration)
                return;

            if (serialComm != null && serialComm.IsConnected && ((RadioButton)sender).Checked)
            {
                try
                {
                    string command = $"DISPMODE{(radioDisplayDetailed.Checked ? "1" : "0")}";
                    tl.LogMessage("RadioDisplayMode_CheckedChanged", $"Sending display mode command: {command}");
                    SendCommandWithLog(command);
                }
                catch (Exception ex)
                {
                    tl.LogMessage("RadioDisplayMode_CheckedChanged", $"Error sending display mode: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Handle power mode change in real-time
        /// </summary>
        private void RadioPowerMode_CheckedChanged(object sender, EventArgs e)
        {
            // Don't send commands while loading configuration
            if (isLoadingConfiguration)
                return;

            if (serialComm != null && serialComm.IsConnected && ((RadioButton)sender).Checked)
            {
                try
                {
                    int powerMode = radioPowerAuto.Checked ? 0 : (radioPowerAlwaysOn.Checked ? 1 : 2);
                    string command = $"DISPPOWER{powerMode}";
                    tl.LogMessage("RadioPowerMode_CheckedChanged", $"Sending power mode command: {command}");
                    SendCommandWithLog(command);
                }
                catch (Exception ex)
                {
                    tl.LogMessage("RadioPowerMode_CheckedChanged", $"Error sending power mode: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Handle display enabled change in real-time
        /// </summary>
        private void ChkDisplayEnabled_CheckedChanged(object sender, EventArgs e)
        {
            // Don't send commands while loading configuration
            if (isLoadingConfiguration)
                return;

            if (serialComm != null && serialComm.IsConnected)
            {
                try
                {
                    string command = chkDisplayEnabled.Checked ? "DISPON" : "DISPOFF";
                    tl.LogMessage("ChkDisplayEnabled_CheckedChanged", $"Sending display enabled command: {command}");
                    SendCommandWithLog(command);
                }
                catch (Exception ex)
                {
                    tl.LogMessage("ChkDisplayEnabled_CheckedChanged", $"Error sending display enabled: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Handle display timeout change in real-time
        /// </summary>
        private void NumericDisplayTimeout_ValueChanged(object sender, EventArgs e)
        {
            // Don't send commands while loading configuration
            if (isLoadingConfiguration)
                return;

            if (serialComm != null && serialComm.IsConnected)
            {
                try
                {
                    string command = $"DISPTIMEOUT{numericDisplayTimeout.Value}";
                    tl.LogMessage("NumericDisplayTimeout_ValueChanged", $"Sending display timeout command: {command}");
                    SendCommandWithLog(command);
                }
                catch (Exception ex)
                {
                    tl.LogMessage("NumericDisplayTimeout_ValueChanged", $"Error sending display timeout: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Apply display configuration to the filter wheel
        /// </summary>
        private void BtnApplyDisplayConfig_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnApplyDisplayConfig_Click", "Sending display configuration to device");
                AddToLog("INFO", "Applying display configuration...");

                // Send display rotation command
                SendCommandWithLog($"ROTATE{(radioDisplayInverted.Checked ? "1" : "0")}");

                // Send display mode command
                SendCommandWithLog($"DISPMODE{(radioDisplayDetailed.Checked ? "1" : "0")}");

                // Send brightness command
                SendCommandWithLog($"BRIGHT{trackBarBrightness.Value}");

                // Send display power mode command
                int powerMode = radioPowerAuto.Checked ? 0 : (radioPowerAlwaysOn.Checked ? 1 : 2);
                SendCommandWithLog($"DISPPOWER{powerMode}");

                // Send display on/off command
                SendCommandWithLog(chkDisplayEnabled.Checked ? "DISPON" : "DISPOFF");

                // Send display auto-off timeout (only relevant when in Auto power mode)
                SendCommandWithLog($"DISPTIMEOUT{numericDisplayTimeout.Value}");

                tl.LogMessage("BtnApplyDisplayConfig_Click", "Display configuration applied successfully");
                AddToLog("INFO", "Display configuration applied successfully!");
                MessageBox.Show("Display configuration applied to device!", "Apply Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnApplyDisplayConfig_Click", $"Error applying display configuration: {ex.Message}");
                AddToLog("ERR", $"Error applying display configuration: {ex.Message}");
                MessageBox.Show($"Error applying display configuration: {ex.Message}", "Apply Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Manual Command Section

        /// <summary>
        /// Handle text change to update autocomplete and tooltip
        /// </summary>
        private void TextBoxManualCommand_TextChanged(object sender, EventArgs e)
        {
            if (textBoxManualCommand == null)
                return;

            // Get command info for tooltip
            string currentText = textBoxManualCommand.Text.Trim().ToUpperInvariant();
            if (!string.IsNullOrWhiteSpace(currentText))
            {
                var commandInfo = CommandHelp.GetCommandInfo(currentText);
                if (commandInfo != null)
                {
                    toolTipCommand.SetToolTip(textBoxManualCommand, commandInfo.GetDetailedHelp());
                }
                else
                {
                    toolTipCommand.SetToolTip(textBoxManualCommand, "Type a command or press F1 for help");
                }
            }
        }

        /// <summary>
        /// Handle key down events in manual command textbox
        /// </summary>
        private void TextBoxManualCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                SendManualCommand();
            }
            else if (e.KeyCode == Keys.F1)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                ShowCommandHelp();
            }
            else if (e.Control && e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                ShowAutocompletePopup();
            }
        }

        /// <summary>
        /// Handle send command button click
        /// </summary>
        private void BtnSendCommand_Click(object sender, EventArgs e)
        {
            SendManualCommand();
        }

        /// <summary>
        /// Handle help button click
        /// </summary>
        private void BtnCommandHelp_Click(object sender, EventArgs e)
        {
            ShowCommandHelp();
        }

        /// <summary>
        /// Send manual command to device
        /// </summary>
        private void SendManualCommand()
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string command = textBoxManualCommand.Text.Trim();
            if (string.IsNullOrWhiteSpace(command))
            {
                MessageBox.Show("Please enter a command.", "No Command", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("SendManualCommand", $"Sending manual command: {command}");

                // Determine timeout based on command type
                int timeout = SerialCommands.COMMAND_TIMEOUT_MS;
                string upperCommand = command.ToUpperInvariant();

                // Use extended timeout for movement commands
                if (upperCommand.StartsWith("MP") || upperCommand.StartsWith("CAL") || upperCommand.StartsWith("CALWIZ"))
                {
                    timeout = SerialCommands.MOVEMENT_TIMEOUT_MS;
                }

                string response = SendCommandWithLog(command, timeout);

                // Clear the textbox after successful send
                textBoxManualCommand.Clear();
                textBoxManualCommand.Focus();
            }
            catch (Exception ex)
            {
                tl.LogMessage("SendManualCommand", $"Error sending manual command: {ex.Message}");
                MessageBox.Show($"Error sending command: {ex.Message}", "Command Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Show autocomplete popup with available commands
        /// </summary>
        private void ShowAutocompletePopup()
        {
            string currentText = textBoxManualCommand.Text.Trim();
            string[] suggestions = CommandHelp.GetAutocompleteSuggestions(currentText);

            if (suggestions.Length > 0)
            {
                // Create a simple popup with suggestions
                string suggestionsList = string.Join("\n", suggestions.Take(10));
                MessageBox.Show($"Available commands starting with '{currentText}':\n\n{suggestionsList}\n\n{(suggestions.Length > 10 ? $"... and {suggestions.Length - 10} more" : "")}",
                    "Autocomplete Suggestions", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Show command help dialog
        /// </summary>
        private void ShowCommandHelp()
        {
            try
            {
                CommandHelpForm helpForm = new CommandHelpForm(textBoxManualCommand.Text.Trim());
                if (helpForm.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(helpForm.SelectedCommand))
                {
                    textBoxManualCommand.Text = helpForm.SelectedCommand;
                    textBoxManualCommand.Focus();
                    textBoxManualCommand.SelectionStart = textBoxManualCommand.Text.Length;
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("ShowCommandHelp", $"Error showing command help: {ex.Message}");
                MessageBox.Show($"Error showing help: {ex.Message}", "Help Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

    }
}