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

        // Backlash calibration state
        private bool backlashCalibrationActive = false;
        private bool backlashForwardDirection = true;

        public SetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            tl = tlDriver;

            // Initialize serial communication for testing
            serialComm = new SerialCommunication(tl);

            // Initialize steps comboboxes with default value
            comboBoxSteps.SelectedIndex = 1; // Default to "3" steps
            comboBoxBacklashSteps.SelectedIndex = 1; // Default to "3" steps

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
                TextBox[] textBoxes = { textBoxFilter1, textBoxFilter2, textBoxFilter3, textBoxFilter4, textBoxFilter5, textBoxFilter6, textBoxFilter7, textBoxFilter8 };

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

        private void InitUI()
        {

            // set the list of COM ports to those that are currently available
            comboBoxComPort.Items.Clear(); // Clear any existing entries
            using (Serial serial = new Serial()) // User the Se5rial component to get an extended list of COM ports
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
            TextBox[] textBoxes = { textBoxFilter1, textBoxFilter2, textBoxFilter3, textBoxFilter4, textBoxFilter5, textBoxFilter6, textBoxFilter7, textBoxFilter8 };
            Label[] labels = { labelFilter1, labelFilter2, labelFilter3, labelFilter4, labelFilter5, labelFilter6, labelFilter7, labelFilter8 };

            // Show/hide controls based on filter count
            for (int i = 0; i < 8; i++)
            {
                bool visible = i < FilterWheelHardware.filterCount;
                textBoxes[i].Visible = visible;
                labels[i].Visible = visible;
            }
        }

        private void UpdateConnectionButtons(bool connected)
        {
            btnConnect.Enabled = !connected && comboBoxComPort.SelectedItem != null && comboBoxComPort.SelectedItem.ToString() != NO_PORTS_MESSAGE;
            btnDisconnect.Enabled = connected;
            btnSetFilterConfig.Enabled = connected;
            comboBoxComPort.Enabled = !connected;

            // Enable calibration buttons only when connected
            btnStartCalibration.Enabled = connected;
            btnCalibrationForward.Enabled = false; // Only enabled during calibration
            btnCalibrationBackward.Enabled = false; // Only enabled during calibration
            btnFinishCalibration.Enabled = false; // Only enabled during calibration

            // Enable backlash calibration buttons only when connected
            btnStartBacklash.Enabled = connected;
            btnBacklashStep.Enabled = false; // Only enabled during backlash calibration
            btnBacklashMark.Enabled = false; // Only enabled during backlash calibration
            btnFinishBacklash.Enabled = false; // Only enabled during backlash calibration

            // Enable motor configuration buttons only when connected
            btnGetMotorConfig.Enabled = connected;
            btnSetMotorConfig.Enabled = connected;
            btnResetMotorConfig.Enabled = connected;
            btnGetDirectionConfig.Enabled = connected;
            btnSetDirectionConfig.Enabled = connected;
            btnGetStepsPerRev.Enabled = connected;
            btnSetStepsPerRev.Enabled = connected;

            // Enable manual control buttons only when connected
            btnStepForward.Enabled = connected;
            btnStepBackward.Enabled = connected;
            btnStepTo.Enabled = connected;
            btnGetStepPosition.Enabled = connected;
            btnEnableMotor.Enabled = connected;
            btnDisableMotor.Enabled = connected;
            btnGetStatus.Enabled = connected;
            btnGetVersion.Enabled = connected;
            btnGetDeviceId.Enabled = connected;
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
                TextBox[] textBoxes = { textBoxFilter1, textBoxFilter2, textBoxFilter3, textBoxFilter4, textBoxFilter5, textBoxFilter6, textBoxFilter7, textBoxFilter8 };

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
                        string prefix = $"N{i}:";
                        if (nameResponse.StartsWith(prefix))
                        {
                            retrievedNames[i - 1] = nameResponse.Substring(prefix.Length);
                            AddToLog("INFO", $"Filter {i}: {retrievedNames[i - 1]}");
                        }
                        else
                        {
                            retrievedNames[i - 1] = $"Filter{i}";
                            AddToLog("WARN", $"Invalid response for filter {i}, using default name");
                        }
                    }
                    catch (Exception ex)
                    {
                        tl.LogMessage("RetrieveFilterConfiguration", $"Error getting name for position {i}: {ex.Message}");
                        AddToLog("ERR", $"Error getting filter {i} name: {ex.Message}");
                        retrievedNames[i - 1] = $"Filter{i}";
                    }
                }

                // Update UI with retrieved names
                FilterWheelHardware.filterNames = new string[8];
                TextBox[] textBoxes = { textBoxFilter1, textBoxFilter2, textBoxFilter3, textBoxFilter4, textBoxFilter5, textBoxFilter6, textBoxFilter7, textBoxFilter8 };

                for (int i = 0; i < 8; i++)
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


        private void BtnStartCalibration_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnStartCalibration_Click", "Starting revolution calibration");
                AddToLog("INFO", "Starting revolution calibration...");

                string response = SendCommandWithLog("REVCAL");

                // Update button states for calibration mode
                btnStartCalibration.Enabled = false;
                btnCalibrationForward.Enabled = true;
                btnCalibrationBackward.Enabled = true;
                btnFinishCalibration.Enabled = true;

                // Update status
                labelRevCalStatus.Text = "Status: Calibrating - Use Forward/Back buttons to adjust";

                tl.LogMessage("BtnStartCalibration_Click", "Revolution calibration started successfully");
                AddToLog("INFO", "Revolution calibration started!");
                MessageBox.Show("Revolution calibration started!\nUse Forward/Back buttons to adjust the wheel position.\nClick Finish when the wheel completes exactly one revolution.", "Calibration Started", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnStartCalibration_Click", $"Error starting calibration: {ex.Message}");
                AddToLog("ERR", $"Calibration start failed: {ex.Message}");
                MessageBox.Show($"Error starting calibration: {ex.Message}", "Calibration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCalibrationForward_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
                return;

            try
            {
                string steps = comboBoxSteps.SelectedItem?.ToString() ?? "3";
                string response = SendCommandWithLog($"RCP{steps}");
                tl.LogMessage("BtnCalibrationForward_Click", $"Added {steps} steps to calibration");
                AddToLog("INFO", $"Added {steps} steps forward");
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnCalibrationForward_Click", $"Error during forward adjustment: {ex.Message}");
                AddToLog("ERR", $"Forward adjustment failed: {ex.Message}");
                MessageBox.Show($"Error during forward adjustment: {ex.Message}", "Calibration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCalibrationBackward_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
                return;

            try
            {
                string steps = comboBoxSteps.SelectedItem?.ToString() ?? "3";
                string response = SendCommandWithLog($"RCM{steps}");
                tl.LogMessage("BtnCalibrationBackward_Click", $"Subtracted {steps} steps from calibration");
                AddToLog("INFO", $"Subtracted {steps} steps backward");
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnCalibrationBackward_Click", $"Error during backward adjustment: {ex.Message}");
                AddToLog("ERR", $"Backward adjustment failed: {ex.Message}");
                MessageBox.Show($"Error during backward adjustment: {ex.Message}", "Calibration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFinishCalibration_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
                return;

            try
            {
                tl.LogMessage("BtnFinishCalibration_Click", "Finishing revolution calibration");

                DialogResult result = MessageBox.Show("Are you sure the filter wheel has completed exactly one full revolution?\n\nThis will save the calibrated steps per revolution to the device.",
                                                    "Confirm Calibration", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string response = SendCommandWithLog("RCFIN");

                    // Reset button states
                    btnStartCalibration.Enabled = true;
                    btnCalibrationForward.Enabled = false;
                    btnCalibrationBackward.Enabled = false;
                    btnFinishCalibration.Enabled = false;

                    // Update status
                    labelRevCalStatus.Text = "Status: Complete - Revolution calibration saved";

                    tl.LogMessage("BtnFinishCalibration_Click", "Revolution calibration finished successfully");
                    AddToLog("INFO", "Revolution calibration completed!");
                    MessageBox.Show("Revolution calibration completed and saved!\nThe filter wheel is now calibrated for precise positioning.", "Calibration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnFinishCalibration_Click", $"Error finishing calibration: {ex.Message}");
                MessageBox.Show($"Error finishing calibration: {ex.Message}", "Calibration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Reset button states on error
                btnStartCalibration.Enabled = true;
                btnCalibrationForward.Enabled = false;
                btnCalibrationBackward.Enabled = false;
                btnFinishCalibration.Enabled = false;
                labelRevCalStatus.Text = "Status: Error - Calibration failed";
            }
        }

        private void BtnStartBacklash_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnStartBacklash_Click", "Starting backlash calibration");
                AddToLog("INFO", "Starting backlash calibration...");

                string response = SendCommandWithLog("BLCAL");

                if (response.Contains("ERROR:ENCODER_REQUIRED"))
                {
                    AddToLog("ERR", "Encoder required for backlash calibration");
                    labelBacklashStatus.Text = "Status: Error - AS5600 encoder required";
                    MessageBox.Show("Backlash calibration requires an encoder (AS5600). Please ensure it's connected and detected.", "Encoder Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update button states for backlash calibration mode
                backlashCalibrationActive = true;
                backlashForwardDirection = true;
                btnStartBacklash.Enabled = false;
                btnBacklashStep.Enabled = true;
                btnBacklashMark.Enabled = true;
                btnFinishBacklash.Enabled = false;

                // Disable revolution calibration during backlash calibration
                btnStartCalibration.Enabled = false;

                // Update status
                labelBacklashStatus.Text = "Status: Calibrating forward direction - Use Step and Mark";

                tl.LogMessage("BtnStartBacklash_Click", "Backlash calibration started successfully");
                AddToLog("INFO", "Backlash calibration started - testing forward direction");
                MessageBox.Show("Backlash calibration started!\n\n1. Use 'Step' to move in small increments\n2. Watch the encoder reading\n3. Click 'Mark' when you detect movement\n4. Process will repeat for reverse direction", "Backlash Calibration Started", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnStartBacklash_Click", $"Error starting backlash calibration: {ex.Message}");
                AddToLog("ERR", $"Backlash calibration start failed: {ex.Message}");
                MessageBox.Show($"Error starting backlash calibration: {ex.Message}", "Calibration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBacklashStep_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected || !backlashCalibrationActive)
                return;

            try
            {
                string steps = comboBoxBacklashSteps.SelectedItem?.ToString() ?? "3";
                string response = SendCommandWithLog($"BLS{steps}");
                string direction = backlashForwardDirection ? "forward" : "backward";
                tl.LogMessage("BtnBacklashStep_Click", $"Backlash test step ({steps} steps) in {direction} direction");
                AddToLog("INFO", $"Test step ({steps} steps, {direction} direction)");
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnBacklashStep_Click", $"Error during backlash step: {ex.Message}");
                AddToLog("ERR", $"Backlash step failed: {ex.Message}");
                MessageBox.Show($"Error during backlash step: {ex.Message}", "Calibration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBacklashMark_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected || !backlashCalibrationActive)
                return;

            try
            {
                string response = SendCommandWithLog("BLM");
                tl.LogMessage("BtnBacklashMark_Click", "Backlash movement marked");

                if (response.StartsWith("BLM_FORWARD:"))
                {
                    backlashForwardDirection = false;
                    labelBacklashStatus.Text = "Status: Calibrating backward direction - Use Step and Mark";
                    AddToLog("INFO", "Forward backlash measured - now testing backward direction");
                    MessageBox.Show("Forward direction complete!\n\nNow testing backward direction:\n1. Use 'Step' to move in reverse\n2. Click 'Mark' when you detect movement", "Testing Backward Direction", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (response.StartsWith("BLM_BACKWARD:"))
                {
                    AddToLog("INFO", "Backward backlash measured - calibration ready to finish");
                    btnFinishBacklash.Enabled = true;
                    btnBacklashStep.Enabled = false;
                    btnBacklashMark.Enabled = false;
                    labelBacklashStatus.Text = "Status: Ready to finish - Both directions calibrated";
                    MessageBox.Show("Both directions measured!\n\nClick 'Finish' to save the backlash calibration.", "Ready to Finish", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnBacklashMark_Click", $"Error marking backlash: {ex.Message}");
                AddToLog("ERR", $"Backlash mark failed: {ex.Message}");
                MessageBox.Show($"Error marking backlash: {ex.Message}", "Calibration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnFinishBacklash_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected || !backlashCalibrationActive)
                return;

            try
            {
                tl.LogMessage("BtnFinishBacklash_Click", "Finishing backlash calibration");

                DialogResult result = MessageBox.Show("Save the backlash calibration?\n\nThis will apply the measured backlash compensation to all future movements.",
                                                    "Confirm Backlash Calibration", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string response = SendCommandWithLog("BLFIN");

                    // Reset button states
                    backlashCalibrationActive = false;
                    btnStartBacklash.Enabled = true;
                    btnBacklashStep.Enabled = false;
                    btnBacklashMark.Enabled = false;
                    btnFinishBacklash.Enabled = false;

                    // Re-enable other functions
                    btnStartCalibration.Enabled = true;

                    // Update status
                    labelBacklashStatus.Text = "Status: Complete - Backlash calibration saved";

                    tl.LogMessage("BtnFinishBacklash_Click", "Backlash calibration finished successfully");
                    AddToLog("INFO", "Backlash calibration completed and saved!");
                    MessageBox.Show("Backlash calibration completed and saved!\n\nThe system will now compensate for backlash in all movements.", "Calibration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnFinishBacklash_Click", $"Error finishing backlash calibration: {ex.Message}");
                AddToLog("ERR", $"Backlash calibration finish failed: {ex.Message}");
                MessageBox.Show($"Error finishing backlash calibration: {ex.Message}", "Calibration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Reset button states on error
                backlashCalibrationActive = false;
                btnStartBacklash.Enabled = true;
                btnBacklashStep.Enabled = false;
                btnBacklashMark.Enabled = false;
                btnFinishBacklash.Enabled = false;
                btnStartCalibration.Enabled = true;
                labelBacklashStatus.Text = "Status: Error - Backlash calibration failed";
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

        // Motor Configuration Event Handlers
        private void BtnGetMotorConfig_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnGetMotorConfig_Click", "Getting motor configuration");
                AddToLog("INFO", "Getting motor configuration...");

                string response = SendCommandWithLog("GMC");

                // Parse motor configuration response
                // Expected format: MOTOR_CONFIG:SPEED=1000,MAX_SPEED=2000,ACCELERATION=800,DISABLE_DELAY=1000
                if (response.StartsWith("MOTOR_CONFIG:"))
                {
                    string configStr = response.Substring("MOTOR_CONFIG:".Length);
                    string[] configParams = configStr.Split(',');

                    foreach (string param in configParams)
                    {
                        string[] keyValue = param.Split('=');
                        if (keyValue.Length == 2)
                        {
                            string key = keyValue[0].Trim();
                            if (int.TryParse(keyValue[1].Trim(), out int value))
                            {
                                switch (key)
                                {
                                    case "SPEED":
                                        numericMotorSpeed.Value = Math.Max(numericMotorSpeed.Minimum, Math.Min(numericMotorSpeed.Maximum, value));
                                        break;
                                    case "MAX_SPEED":
                                        numericMaxSpeed.Value = Math.Max(numericMaxSpeed.Minimum, Math.Min(numericMaxSpeed.Maximum, value));
                                        break;
                                    case "ACCELERATION":
                                        numericAcceleration.Value = Math.Max(numericAcceleration.Minimum, Math.Min(numericAcceleration.Maximum, value));
                                        break;
                                    case "DISABLE_DELAY":
                                        numericDisableDelay.Value = Math.Max(numericDisableDelay.Minimum, Math.Min(numericDisableDelay.Maximum, value));
                                        break;
                                    case "STEPS_PER_REV":
                                        numericStepsPerRev.Value = Math.Max(numericStepsPerRev.Minimum, Math.Min(numericStepsPerRev.Maximum, value));
                                        break;
                                }
                            }
                        }
                    }

                    AddToLog("INFO", "Motor configuration retrieved successfully");
                    MessageBox.Show("Motor configuration retrieved successfully!", "Configuration Retrieved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    AddToLog("WARN", $"Unexpected motor config response: {response}");
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnGetMotorConfig_Click", $"Error getting motor configuration: {ex.Message}");
                AddToLog("ERR", $"Get motor config failed: {ex.Message}");
                MessageBox.Show($"Error getting motor configuration: {ex.Message}", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSetMotorConfig_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnSetMotorConfig_Click", "Setting motor configuration");
                AddToLog("INFO", "Setting motor configuration...");

                // Send motor speed
                SendCommandWithLog($"MS{numericMotorSpeed.Value}");

                // Send maximum speed
                SendCommandWithLog($"MXS{numericMaxSpeed.Value}");

                // Send acceleration
                SendCommandWithLog($"MA{numericAcceleration.Value}");

                // Send disable delay
                SendCommandWithLog($"MDD{numericDisableDelay.Value}");

                // Send steps per revolution
                SendCommandWithLog($"SPR{numericStepsPerRev.Value}");

                AddToLog("INFO", "Motor configuration sent successfully");
                MessageBox.Show("Motor configuration sent successfully!\nAll settings have been saved to the device.", "Configuration Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnSetMotorConfig_Click", $"Error setting motor configuration: {ex.Message}");
                AddToLog("ERR", $"Set motor config failed: {ex.Message}");
                MessageBox.Show($"Error setting motor configuration: {ex.Message}", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnResetMotorConfig_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DialogResult result = MessageBox.Show("Reset motor configuration to default values?\n\nThis will restore factory settings for speed, acceleration, and timing parameters.",
                                                    "Confirm Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    tl.LogMessage("BtnResetMotorConfig_Click", "Resetting motor configuration");
                    AddToLog("INFO", "Resetting motor configuration to defaults...");

                    string response = SendCommandWithLog("RMC");

                    // Reset UI controls to defaults
                    numericMotorSpeed.Value = 1000;
                    numericMaxSpeed.Value = 2000;
                    numericAcceleration.Value = 500;
                    numericDisableDelay.Value = 1000;
                    numericStepsPerRev.Value = 2048;

                    AddToLog("INFO", "Motor configuration reset to defaults");
                    MessageBox.Show("Motor configuration reset to default values!", "Configuration Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnResetMotorConfig_Click", $"Error resetting motor configuration: {ex.Message}");
                AddToLog("ERR", $"Reset motor config failed: {ex.Message}");
                MessageBox.Show($"Error resetting motor configuration: {ex.Message}", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGetDirectionConfig_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnGetDirectionConfig_Click", "Getting direction configuration");
                AddToLog("INFO", "Getting direction configuration...");

                string response = SendCommandWithLog("GDC");

                // Parse direction configuration response
                // Expected format: DIRECTION_CONFIG:MODE=0,REVERSE=1
                if (response.StartsWith("DIRECTION_CONFIG:"))
                {
                    string configStr = response.Substring("DIRECTION_CONFIG:".Length);
                    string[] configParams = configStr.Split(',');

                    foreach (string param in configParams)
                    {
                        string[] keyValue = param.Split('=');
                        if (keyValue.Length == 2)
                        {
                            string key = keyValue[0].Trim();
                            if (int.TryParse(keyValue[1].Trim(), out int value))
                            {
                                switch (key)
                                {
                                    case "MODE":
                                        radioUnidirectional.Checked = (value == 0);
                                        radioBidirectional.Checked = (value == 1);
                                        break;
                                    case "REVERSE":
                                        checkBoxReverse.Checked = (value == 1);
                                        break;
                                }
                            }
                        }
                    }

                    AddToLog("INFO", "Direction configuration retrieved successfully");
                    MessageBox.Show("Direction configuration retrieved successfully!", "Configuration Retrieved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    AddToLog("WARN", $"Unexpected direction config response: {response}");
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnGetDirectionConfig_Click", $"Error getting direction configuration: {ex.Message}");
                AddToLog("ERR", $"Get direction config failed: {ex.Message}");
                MessageBox.Show($"Error getting direction configuration: {ex.Message}", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSetDirectionConfig_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnSetDirectionConfig_Click", "Setting direction configuration");
                AddToLog("INFO", "Setting direction configuration...");

                // Send direction mode
                int directionMode = radioBidirectional.Checked ? 1 : 0;
                SendCommandWithLog($"MDM{directionMode}");

                // Send reverse setting
                int reverse = checkBoxReverse.Checked ? 1 : 0;
                SendCommandWithLog($"MRV{reverse}");

                AddToLog("INFO", "Direction configuration sent successfully");
                MessageBox.Show("Direction configuration sent successfully!\nDirection settings have been saved to the device.", "Configuration Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnSetDirectionConfig_Click", $"Error setting direction configuration: {ex.Message}");
                AddToLog("ERR", $"Set direction config failed: {ex.Message}");
                MessageBox.Show($"Error setting direction configuration: {ex.Message}", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Manual Control Event Handlers
        private void BtnStepForward_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int steps = (int)numericStepAmount.Value;
                tl.LogMessage("BtnStepForward_Click", $"Stepping forward {steps} steps");
                string response = SendCommandWithLog($"SF{steps}");
                AddToLog("INFO", $"Stepped forward {steps} steps");
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnStepForward_Click", $"Error stepping forward: {ex.Message}");
                AddToLog("ERR", $"Step forward failed: {ex.Message}");
                MessageBox.Show($"Error stepping forward: {ex.Message}", "Step Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStepBackward_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int steps = (int)numericStepAmount.Value;
                tl.LogMessage("BtnStepBackward_Click", $"Stepping backward {steps} steps");
                string response = SendCommandWithLog($"SB{steps}");
                AddToLog("INFO", $"Stepped backward {steps} steps");
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnStepBackward_Click", $"Error stepping backward: {ex.Message}");
                AddToLog("ERR", $"Step backward failed: {ex.Message}");
                MessageBox.Show($"Error stepping backward: {ex.Message}", "Step Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStepTo_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int position = (int)numericAbsoluteStep.Value;
                tl.LogMessage("BtnStepToPosition_Click", $"Moving to absolute step position {position}");
                string response = SendCommandWithLog($"ST{position}");
                AddToLog("INFO", $"Moved to absolute step position {position}");
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnStepToPosition_Click", $"Error moving to position: {ex.Message}");
                AddToLog("ERR", $"Move to position failed: {ex.Message}");
                MessageBox.Show($"Error moving to position: {ex.Message}", "Position Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGetStepPosition_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnGetStepPosition_Click", "Getting current step position");
                string response = SendCommandWithLog("GST");

                // Parse step position response
                // Expected format: STEP:1024
                if (response.StartsWith("STEP:"))
                {
                    string stepStr = response.Substring("STEP:".Length);
                    if (int.TryParse(stepStr, out int stepPosition))
                    {
                        labelCurrentStep.Text = $"Current: {stepPosition}";
                        AddToLog("INFO", $"Current step position: {stepPosition}");
                    }
                }
                else
                {
                    AddToLog("WARN", $"Unexpected step position response: {response}");
                }
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnGetStepPosition_Click", $"Error getting step position: {ex.Message}");
                AddToLog("ERR", $"Get step position failed: {ex.Message}");
                MessageBox.Show($"Error getting step position: {ex.Message}", "Position Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEnableMotor_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnEnableMotor_Click", "Enabling motor power");
                string response = SendCommandWithLog("ME");
                AddToLog("INFO", "Motor power enabled");
                MessageBox.Show("Motor power enabled!", "Motor Enabled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnEnableMotor_Click", $"Error enabling motor: {ex.Message}");
                AddToLog("ERR", $"Enable motor failed: {ex.Message}");
                MessageBox.Show($"Error enabling motor: {ex.Message}", "Motor Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDisableMotor_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnDisableMotor_Click", "Disabling motor power");
                string response = SendCommandWithLog("MD");
                AddToLog("INFO", "Motor power disabled");
                MessageBox.Show("Motor power disabled!", "Motor Disabled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnDisableMotor_Click", $"Error disabling motor: {ex.Message}");
                AddToLog("ERR", $"Disable motor failed: {ex.Message}");
                MessageBox.Show($"Error disabling motor: {ex.Message}", "Motor Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGetStatus_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnGetStatus_Click", "Getting system status");
                string response = SendCommandWithLog("STATUS");
                AddToLog("INFO", "System status retrieved");
                MessageBox.Show($"System Status:\n\n{response}", "System Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnGetStatus_Click", $"Error getting status: {ex.Message}");
                AddToLog("ERR", $"Get status failed: {ex.Message}");
                MessageBox.Show($"Error getting system status: {ex.Message}", "Status Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGetVersion_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnGetVersion_Click", "Getting firmware version");
                string response = SendCommandWithLog("VER");
                AddToLog("INFO", "Firmware version retrieved");

                string version = response;
                if (response.StartsWith("VERSION:"))
                {
                    version = response.Substring("VERSION:".Length);
                }

                MessageBox.Show($"Firmware Version: {version}", "Firmware Version", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnGetVersion_Click", $"Error getting version: {ex.Message}");
                AddToLog("ERR", $"Get version failed: {ex.Message}");
                MessageBox.Show($"Error getting firmware version: {ex.Message}", "Version Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGetDeviceId_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnGetDeviceId_Click", "Getting device ID");
                string response = SendCommandWithLog("ID");
                AddToLog("INFO", "Device ID retrieved");

                string deviceId = response;
                if (response.StartsWith("DEVICE_ID:"))
                {
                    deviceId = response.Substring("DEVICE_ID:".Length);
                }

                MessageBox.Show($"Device ID: {deviceId}", "Device ID", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnGetDeviceId_Click", $"Error getting device ID: {ex.Message}");
                AddToLog("ERR", $"Get device ID failed: {ex.Message}");
                MessageBox.Show($"Error getting device ID: {ex.Message}", "Device ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEmergencyStop_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnEmergencyStop_Click", "Emergency stop triggered");
                string response = SendCommandWithLog("STOP");
                AddToLog("WARN", "EMERGENCY STOP - All movement halted");
                MessageBox.Show("Emergency stop activated!\nAll motor movement has been halted.", "Emergency Stop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnEmergencyStop_Click", $"Error during emergency stop: {ex.Message}");
                AddToLog("ERR", $"Emergency stop failed: {ex.Message}");
                MessageBox.Show($"Error during emergency stop: {ex.Message}", "Stop Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Get steps per revolution from device
        /// </summary>
        private void BtnGetStepsPerRev_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnGetStepsPerRev_Click", "Getting steps per revolution");
                AddToLog("INFO", "Getting steps per revolution...");

                int stepsPerRev = serialComm.GetStepsPerRevolution();
                numericStepsPerRev.Value = Math.Max(numericStepsPerRev.Minimum, Math.Min(numericStepsPerRev.Maximum, stepsPerRev));

                AddToLog("INFO", $"Steps per revolution: {stepsPerRev}");
                MessageBox.Show($"Steps per revolution: {stepsPerRev}", "Steps Per Revolution", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnGetStepsPerRev_Click", $"Error getting steps per revolution: {ex.Message}");
                AddToLog("ERR", $"Get steps per revolution failed: {ex.Message}");
                MessageBox.Show($"Error getting steps per revolution: {ex.Message}", "Steps Per Revolution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Set steps per revolution to device
        /// </summary>
        private void BtnSetStepsPerRev_Click(object sender, EventArgs e)
        {
            if (!serialComm.IsConnected)
            {
                MessageBox.Show("Please connect to the device first.", "Not Connected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                tl.LogMessage("BtnSetStepsPerRev_Click", "Setting steps per revolution");
                AddToLog("INFO", "Setting steps per revolution...");

                int stepsPerRev = (int)numericStepsPerRev.Value;
                serialComm.SetStepsPerRevolution(stepsPerRev);

                AddToLog("INFO", $"Steps per revolution set to: {stepsPerRev}");
                MessageBox.Show($"Steps per revolution set to: {stepsPerRev}", "Steps Per Revolution Set", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tl.LogMessage("BtnSetStepsPerRev_Click", $"Error setting steps per revolution: {ex.Message}");
                AddToLog("ERR", $"Set steps per revolution failed: {ex.Message}");
                MessageBox.Show($"Error setting steps per revolution: {ex.Message}", "Steps Per Revolution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}