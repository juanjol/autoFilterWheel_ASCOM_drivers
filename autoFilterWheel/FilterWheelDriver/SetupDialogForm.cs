using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ASCOM.Utilities;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

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
                        FilterWheelHardware.filterNames[i] = textBoxes[i].Text.Trim();
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

            // Set default values for calibration step comboboxes
            comboBoxBackwardSteps.SelectedItem = "10";
            comboBoxForwardSteps.SelectedItem = "10";

            // Set compilation date
            labelCompilationDate.Text = $"Built: {GetCompilationDate():yyyy-MM-dd HH:mm}";

            tl.LogMessage("InitUI", $"Set UI controls to COM Port: {comboBoxComPort.SelectedItem}, Filter Count: {FilterWheelHardware.filterCount}");
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

            // Enable calibration buttons only when connected
            btnMoveBackward.Enabled = connected;
            btnSetToPos1.Enabled = connected;
            btnMoveForward.Enabled = connected;

            // Enable filter selection only when connected
            btnSelectFilter.Enabled = connected;
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (comboBoxComPort.SelectedItem == null || comboBoxComPort.SelectedItem.ToString() == NO_PORTS_MESSAGE)
            {
                MessageBox.Show("Please select a valid COM port.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string portName = comboBoxComPort.SelectedItem.ToString();
                tl.LogMessage("BtnConnect_Click", $"Attempting to connect to {portName}");

                // Clear log when connecting
                ClearLog();
                AddToLog("INFO", "Connecting to device...");

                serialComm.Connect(portName);

                if (serialComm.IsConnected)
                {
                    tl.LogMessage("BtnConnect_Click", "Connected successfully, retrieving filter configuration");
                    AddToLog("INFO", "Connected successfully!");

                    // Retrieve filter count and names from device
                    RetrieveFilterConfiguration();

                    UpdateConnectionButtons(true);
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnConnect_Click", $"Connection failed: {ex.Message}");
                MessageBox.Show($"Failed to connect: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateConnectionButtons(false);
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialComm != null && serialComm.IsConnected)
                {
                    serialComm.Disconnect();
                    tl.LogMessage("BtnDisconnect_Click", "Disconnected successfully");
                }
                UpdateConnectionButtons(false);
                MessageBox.Show("Disconnected successfully.", "Disconnected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnDisconnect_Click", $"Disconnect error: {ex.Message}");
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

                    // Limit name length to match Arduino firmware (15 chars max)
                    if (filterName.Length > 15)
                        filterName = filterName.Substring(0, 15);

                    string snCommand = $"SN{i + 1}:{filterName}";
                    tl.LogMessage("BtnSet_Click", $"Sending filter name: {snCommand}");
                    string snResponse = SendCommandWithLog(snCommand);
                    tl.LogMessage("BtnSet_Click", $"Filter name response: {snResponse}");
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

                // Get filter count from device (GF command)
                string response = SendCommandWithLog("GF");
                if (response.StartsWith("F"))
                {
                    int deviceFilterCount = int.Parse(response.Substring(1));
                    if (deviceFilterCount >= 3 && deviceFilterCount <= 8)
                    {
                        FilterWheelHardware.filterCount = deviceFilterCount;
                        comboBoxFilterCount.SelectedItem = deviceFilterCount.ToString();
                        tl.LogMessage("RetrieveFilterConfiguration", $"Retrieved filter count: {deviceFilterCount}");
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
                        string prefix = $"N{i}:";  // Firmware returns "N1:Name" format (not GN)
                        if (nameResponse.StartsWith(prefix))
                        {
                            retrievedNames[i - 1] = nameResponse.Substring(prefix.Length);
                            AddToLog("INFO", $"Filter {i}: {retrievedNames[i - 1]}");
                        }
                        else
                        {
                            retrievedNames[i - 1] = $"Filter{i}";
                            AddToLog("WARN", $"Invalid response for filter {i}: '{nameResponse}', using default name");
                        }
                    }
                    catch (Exception ex)
                    {
                        tl.LogMessage("RetrieveFilterConfiguration", $"Error getting name for position {i}: {ex.Message}");
                        AddToLog("ERR", $"Error getting filter {i} name: {ex.Message}");
                        retrievedNames[i - 1] = $"Filter{i}";
                    }
                }

                // Update UI with retrieved names (max 9 filters)
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
                tl.LogMessage("RetrieveFilterConfiguration", "Filter configuration retrieved successfully");
                AddToLog("INFO", "Configuration retrieved successfully!");
            }
            catch (Exception ex)
            {
                tl.LogMessage("RetrieveFilterConfiguration", $"Error retrieving filter configuration: {ex.Message}");
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

                    string response = SendCommandWithLog(command);

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


        private void BtnMoveBackward_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxBackwardSteps.SelectedItem == null)
            {
                MessageBox.Show("Please select number of steps.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string steps = comboBoxBackwardSteps.SelectedItem.ToString();
                string command = $"SB{steps}";
                tl.LogMessage("BtnMoveBackward_Click", $"Sending command: {command}");
                AddToLog("INFO", $"Moving backward {steps} steps...");

                string response = SendCommandWithLog(command);

                tl.LogMessage("BtnMoveBackward_Click", $"Response: {response}");
                AddToLog("INFO", $"Moved backward {steps} steps");
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnMoveBackward_Click", $"Error: {ex.Message}");
                AddToLog("ERR", $"Move backward failed: {ex.Message}");
                MessageBox.Show($"Error moving backward: {ex.Message}", "Movement Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSetToPos1_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnSetToPos1_Click", "Sending CAL command");
                AddToLog("INFO", "Setting position to 1 (calibrating)...");

                string response = SendCommandWithLog("CAL");

                tl.LogMessage("BtnSetToPos1_Click", $"Response: {response}");
                AddToLog("INFO", "Position set to 1 - Calibration complete!");
                MessageBox.Show("Position set to 1!\n\nThe current position is now registered as filter position 1.", "Calibration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnSetToPos1_Click", $"Error: {ex.Message}");
                AddToLog("ERR", $"Calibration failed: {ex.Message}");
                MessageBox.Show($"Error during calibration: {ex.Message}", "Calibration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMoveForward_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBoxForwardSteps.SelectedItem == null)
            {
                MessageBox.Show("Please select number of steps.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string steps = comboBoxForwardSteps.SelectedItem.ToString();
                string command = $"SF{steps}";
                tl.LogMessage("BtnMoveForward_Click", $"Sending command: {command}");
                AddToLog("INFO", $"Moving forward {steps} steps...");

                string response = SendCommandWithLog(command);

                tl.LogMessage("BtnMoveForward_Click", $"Response: {response}");
                AddToLog("INFO", $"Moved forward {steps} steps");
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnMoveForward_Click", $"Error: {ex.Message}");
                AddToLog("ERR", $"Move forward failed: {ex.Message}");
                MessageBox.Show($"Error moving forward: {ex.Message}", "Movement Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Adds a message to the communication log
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
                textBoxLog.Text = string.Join(Environment.NewLine, logBuffer);
                // Scroll to bottom
                textBoxLog.SelectionStart = textBoxLog.Text.Length;
                textBoxLog.ScrollToCaret();
            }
        }

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
        /// Send command with logging
        /// </summary>
        private string SendCommandWithLog(string command)
        {
            AddToLog("TX", command);
            try
            {
                string response = serialComm.SendCommand(command);
                AddToLog("RX", response);
                return response;
            }
            catch (Exception ex)
            {
                AddToLog("ERR", ex.Message);
                throw;
            }
        }

    }
}