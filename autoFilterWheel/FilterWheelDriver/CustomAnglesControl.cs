using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ASCOM.Utilities;

namespace ASCOM.autoFilterWheel.FilterWheel
{
    /// <summary>
    /// UserControl for custom angles calibration
    /// Implements direct calibration (no wizard) according to ASCOM_CALIBRATION_TAB_SPEC.md
    /// </summary>
    public partial class CustomAnglesControl : UserControl
    {
        #region Private Fields

        private SerialCommunication serialComm;
        private TraceLogger tl;
        private int filterCount = 0;
        private Dictionary<int, PositionCalibrationData> positionData = new Dictionary<int, PositionCalibrationData>();
        private Func<string, int, string> sendCommandWithLog;

        #endregion

        #region Constructor

        public CustomAnglesControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize with serial communication and logger
        /// </summary>
        internal void Initialize(SerialCommunication serial, TraceLogger logger, int filters, Func<string, int, string> sendCommandFunc)
        {
            serialComm = serial;
            tl = logger;
            filterCount = filters;
            sendCommandWithLog = sendCommandFunc;

            // Update system info display
            lblFilterCountValue.Text = filterCount.ToString();

            InitializePositionsGrid();
            RefreshAllAngles();
            UpdateButtonStates();
        }

        #endregion

        #region Initialization

        private void InitializePositionsGrid()
        {
            dgvPositions.Rows.Clear();
            dgvPositions.AllowUserToAddRows = false;
            dgvPositions.ReadOnly = false;
            dgvPositions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPositions.MultiSelect = false;

            // Populate grid based on filter count
            for (int i = 1; i <= filterCount; i++)
            {
                int rowIndex = dgvPositions.Rows.Add();
                DataGridViewRow row = dgvPositions.Rows[rowIndex];

                row.Cells["colPosition"].Value = i;
                row.Cells["colFilterName"].Value = GetFilterName(i);
                row.Cells["colAngle"].Value = "---";
                row.Cells["colCalibrated"].Value = false;

                // Set button text
                DataGridViewButtonCell btnCell = row.Cells["colAction"] as DataGridViewButtonCell;
                if (btnCell != null)
                {
                    btnCell.Value = "Calibrate";
                }

                // Initialize position data
                positionData[i] = new PositionCalibrationData
                {
                    Position = i,
                    FilterName = GetFilterName(i),
                    Angle = 0.0f,
                    IsCustom = false,
                    IsCalibrated = false
                };
            }

            UpdateProgress();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update connection state
        /// </summary>
        public void UpdateConnectionState(bool connected)
        {
            if (connected)
            {
                lblEncoderAngleValue.Text = "Ready";
                lblEncoderAngleValue.ForeColor = Color.Green;
                UpdateCurrentPosition();
            }
            else
            {
                lblEncoderAngleValue.Text = "---";
                lblEncoderAngleValue.ForeColor = Color.Gray;
                lblCurrentPosValue.Text = "---";
            }

            UpdateButtonStates();
        }

        #endregion

        #region Button Events

        private void BtnClearCalibration_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to clear ALL custom calibration?\n\n" +
                "This action will:\n" +
                "• Delete all calibrated angles\n" +
                "• Return to uniform distribution\n" +
                "• CANNOT be undone\n\n" +
                "Do you want to continue?",
                "Confirm Clear Calibration",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    string response = SendCommand(SerialCommands.CMD_CLEAR_ANGLES);
                    tl?.LogMessage("CustomAngles", "Custom calibration cleared");
                    RefreshAllAngles();

                    MessageBox.Show(
                        "Calibration cleared successfully.\nThe system has returned to uniform distribution.",
                        "Calibration Cleared",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error clearing calibration: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tl?.LogMessage("CustomAngles", $"Error: {ex.Message}");
                }
            }
        }

        private void BtnRefreshAngles_Click(object sender, EventArgs e)
        {
            RefreshAllAngles();
            UpdateCurrentPosition();
        }

        #endregion

        #region Movement Button Events

        private void BtnFwd1_Click(object sender, EventArgs e) => ExecuteStepCommand($"{SerialCommands.CMD_STEP_FORWARD}1", "Forward 1 step");
        private void BtnFwd10_Click(object sender, EventArgs e) => ExecuteStepCommand($"{SerialCommands.CMD_STEP_FORWARD}10", "Forward 10 steps");
        private void BtnFwd50_Click(object sender, EventArgs e) => ExecuteStepCommand($"{SerialCommands.CMD_STEP_FORWARD}50", "Forward 50 steps");
        private void BtnFwd100_Click(object sender, EventArgs e) => ExecuteStepCommand($"{SerialCommands.CMD_STEP_FORWARD}100", "Forward 100 steps");

        private void BtnBack1_Click(object sender, EventArgs e) => ExecuteStepCommand($"{SerialCommands.CMD_STEP_BACKWARD}1", "Backward 1 step");
        private void BtnBack10_Click(object sender, EventArgs e) => ExecuteStepCommand($"{SerialCommands.CMD_STEP_BACKWARD}10", "Backward 10 steps");
        private void BtnBack50_Click(object sender, EventArgs e) => ExecuteStepCommand($"{SerialCommands.CMD_STEP_BACKWARD}50", "Backward 50 steps");
        private void BtnBack100_Click(object sender, EventArgs e) => ExecuteStepCommand($"{SerialCommands.CMD_STEP_BACKWARD}100", "Backward 100 steps");

        private void BtnCustomForward_Click(object sender, EventArgs e)
        {
            int steps = (int)nudCustomSteps.Value;
            ExecuteStepCommand($"{SerialCommands.CMD_STEP_FORWARD}{steps}", $"Forward {steps} steps");
        }

        private void BtnCustomBackward_Click(object sender, EventArgs e)
        {
            int steps = (int)nudCustomSteps.Value;
            ExecuteStepCommand($"{SerialCommands.CMD_STEP_BACKWARD}{steps}", $"Backward {steps} steps");
        }

        private void ExecuteStepCommand(string command, string description)
        {
            DisableAllButtons();
            try
            {
                string response = SendCommand(command, timeout: 10000);
                tl?.LogMessage("CustomAngles", $"{description}: {response}");

                // Update position after movement
                UpdateCurrentPosition();
            }
            catch (Exception ex)
            {
                tl?.LogMessage("CustomAngles", $"ERROR: {ex.Message}");
                MessageBox.Show($"Error moving motor: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnableAllButtons();
            }
        }

        #endregion

        #region DataGridView Events

        private void DgvPositions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Check if "Action" column button was clicked
            if (dgvPositions.Columns[e.ColumnIndex].Name == "colAction")
            {
                int position = e.RowIndex + 1;
                CalibratePosition(position);
            }
        }

        private void CalibratePosition(int position)
        {
            try
            {
                ValidateConnectionBeforeAction();
                ValidatePosition(position);

                // Get current encoder angle (read only when clicking)
                float currentAngle = GetCurrentEncoderAngle();

                // Update display
                lblEncoderAngleValue.Text = $"{currentAngle:F2}°";

                // Confirm with user
                DialogResult result = MessageBox.Show(
                    $"Calibrate position {position} with current encoder angle?\n\n" +
                    $"Filter: {GetFilterName(position)}\n" +
                    $"Current angle: {currentAngle:F2}°\n\n" +
                    $"The angle will be saved immediately to EEPROM.",
                    "Confirm Calibration",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result != DialogResult.Yes) return;

                // Send #SETANG command - saves immediately to EEPROM
                // Use InvariantCulture to ensure correct format (dot as decimal separator)
                string command = $"{SerialCommands.CMD_SET_ANGLE}{position}:{currentAngle.ToString("F2", CultureInfo.InvariantCulture)}";
                string response = SendCommand(command);

                // Update position data
                positionData[position].Angle = currentAngle;
                positionData[position].IsCustom = true;
                positionData[position].IsCalibrated = true;

                // Update UI
                UpdatePositionRow(position);
                UpdateProgress();

                tl?.LogMessage("CustomAngles", $"✓ Position {position} calibrated to {currentAngle:F2}°");

                // Check if all positions are calibrated
                if (AllPositionsCalibrated())
                {
                    MessageBox.Show(
                        "All positions have been calibrated!\n\n" +
                        "Custom angles are now active.",
                        "Calibration Complete",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calibrating position: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                tl?.LogMessage("CustomAngles", $"Error: {ex.Message}");
            }
        }

        #endregion

        #region Helper Methods

        private void UpdateCurrentPosition()
        {
            try
            {
                if (!IsConnected()) return;

                string posResponse = SendCommand(SerialCommands.CMD_GET_POSITION);
                if (posResponse.StartsWith("P"))
                {
                    int position = int.Parse(posResponse.Substring(1));
                    lblCurrentPosValue.Text = $"{position} ({GetFilterName(position)})";
                }
            }
            catch
            {
                // Silently fail
            }
        }

        private void RefreshAllAngles()
        {
            try
            {
                if (!IsConnected()) return;

                string response = SendCommand(SerialCommands.CMD_GET_ANGLE);

                if (response.Contains("No custom angles"))
                {
                    tl?.LogMessage("CustomAngles", "No custom angles configured");

                    // Set to uniform distribution
                    for (int i = 1; i <= filterCount; i++)
                    {
                        float defaultAngle = ((i - 1) * 360.0f) / filterCount;
                        positionData[i].Angle = defaultAngle;
                        positionData[i].IsCustom = false;
                        positionData[i].IsCalibrated = false;
                    }
                }
                else if (response.StartsWith("GETANG:"))
                {
                    // Parse: "GETANG:1=0.00°,2=68.50°,3=142.30°,4=218.75°,5=291.20°"
                    string anglesPart = response.Substring(7);
                    string[] pairs = anglesPart.Split(',');

                    foreach (string pair in pairs)
                    {
                        string[] parts = pair.Split('=');
                        if (parts.Length == 2)
                        {
                            int pos = int.Parse(parts[0]);
                            string angleStr = parts[1].TrimEnd('°');

                            // Use InvariantCulture to parse correctly (0.62 not 62)
                            float angle = float.Parse(angleStr, CultureInfo.InvariantCulture);

                            if (positionData.ContainsKey(pos))
                            {
                                positionData[pos].Angle = angle;
                                positionData[pos].IsCustom = true;
                                positionData[pos].IsCalibrated = true;
                            }
                        }
                    }

                    tl?.LogMessage("CustomAngles", "Custom angles loaded successfully");
                }

                RefreshPositionsDisplay();
            }
            catch (Exception ex)
            {
                tl?.LogMessage("CustomAngles", $"Error: {ex.Message}");
            }
        }

        private void RefreshPositionsDisplay()
        {
            for (int i = 1; i <= filterCount; i++)
            {
                UpdatePositionRow(i);
            }
            UpdateProgress();
        }

        private void UpdatePositionRow(int position)
        {
            if (position < 1 || position > dgvPositions.Rows.Count) return;

            int rowIndex = position - 1;
            DataGridViewRow row = dgvPositions.Rows[rowIndex];
            PositionCalibrationData data = positionData[position];

            row.Cells["colFilterName"].Value = data.FilterName;
            row.Cells["colAngle"].Value = $"{data.Angle:F2}° ({(data.IsCustom ? "custom" : "default")})";
            row.Cells["colCalibrated"].Value = data.IsCalibrated;

            // Update button text
            DataGridViewButtonCell btnCell = row.Cells["colAction"] as DataGridViewButtonCell;
            if (btnCell != null)
            {
                btnCell.Value = data.IsCalibrated ? "Recalibrate" : "Calibrate";
            }
        }

        private void UpdateProgress()
        {
            int calibratedCount = positionData.Values.Count(p => p.IsCalibrated);
            int totalCount = filterCount;
            int percentage = totalCount > 0 ? (int)((calibratedCount / (float)totalCount) * 100) : 0;

            pbProgress.Value = percentage;
            lblProgress.Text = $"{calibratedCount}/{totalCount} calibrated ({percentage}%)";
        }

        private void UpdateButtonStates()
        {
            bool connected = IsConnected();

            btnClearCalibration.Enabled = connected;
            btnRefreshAngles.Enabled = connected;

            // Movement buttons
            btnBack100.Enabled = connected;
            btnBack50.Enabled = connected;
            btnBack10.Enabled = connected;
            btnBack1.Enabled = connected;
            btnFwd1.Enabled = connected;
            btnFwd10.Enabled = connected;
            btnFwd50.Enabled = connected;
            btnFwd100.Enabled = connected;
            btnCustomBackward.Enabled = connected;
            btnCustomForward.Enabled = connected;

            // Grid - always enabled when connected (no wizard needed)
            dgvPositions.Enabled = connected;
        }

        private void DisableAllButtons()
        {
            btnClearCalibration.Enabled = false;
            btnRefreshAngles.Enabled = false;
            btnBack100.Enabled = false;
            btnBack50.Enabled = false;
            btnBack10.Enabled = false;
            btnBack1.Enabled = false;
            btnFwd1.Enabled = false;
            btnFwd10.Enabled = false;
            btnFwd50.Enabled = false;
            btnFwd100.Enabled = false;
            btnCustomBackward.Enabled = false;
            btnCustomForward.Enabled = false;
        }

        private void EnableAllButtons()
        {
            UpdateButtonStates();
        }

        private bool AllPositionsCalibrated()
        {
            return positionData.Values.All(p => p.IsCalibrated);
        }

        private float GetCurrentEncoderAngle()
        {
            string response = SendCommand(SerialCommands.CMD_GET_ENCODER_STATUS);

            if (response.Contains("Not connected"))
            {
                throw new InvalidOperationException("AS5600 encoder is not available");
            }

            return ParseEncoderAngle(response);
        }

        private float ParseEncoderAngle(string response)
        {
            // Parse: "ENCSTATUS:Angle=144.50,Expected=72.00,..."
            string data = response.Substring(10); // Skip "ENCSTATUS:"
            string[] pairs = data.Split(',');

            foreach (string pair in pairs)
            {
                string[] parts = pair.Split('=');
                if (parts.Length == 2 && parts[0] == "Angle")
                {
                    // Use InvariantCulture to parse correctly (0.62 not 62)
                    return float.Parse(parts[1], CultureInfo.InvariantCulture);
                }
            }

            return 0.0f;
        }

        #endregion

        #region Serial Communication

        private string SendCommand(string command, int timeout = 5000)
        {
            if (serialComm == null || !serialComm.IsConnected)
            {
                throw new InvalidOperationException("Serial port is not connected");
            }

            // Use the logging function if available, otherwise fall back to direct call
            if (sendCommandWithLog != null)
            {
                return sendCommandWithLog(command, timeout);
            }
            else
            {
                return serialComm.SendCommand(command, timeout);
            }
        }

        private bool IsConnected()
        {
            return serialComm != null && serialComm.IsConnected;
        }

        private string GetFilterName(int position)
        {
            try
            {
                if (FilterWheelHardware.filterNames != null &&
                    position >= 1 &&
                    position <= FilterWheelHardware.filterNames.Length)
                {
                    string name = FilterWheelHardware.filterNames[position - 1];
                    if (!string.IsNullOrEmpty(name))
                    {
                        return name;
                    }
                }
            }
            catch { }

            return $"Filter{position}";
        }

        #endregion

        #region Validation

        private void ValidateConnectionBeforeAction()
        {
            if (!IsConnected())
            {
                throw new InvalidOperationException(
                    "No connection to device.\n" +
                    "Check cable and serial connection."
                );
            }
        }

        private void ValidatePosition(int position)
        {
            if (position < 1 || position > filterCount)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(position),
                    $"Invalid position: {position}. Must be between 1 and {filterCount}."
                );
            }
        }

        #endregion

        #region Nested Classes

        private class PositionCalibrationData
        {
            public int Position { get; set; }
            public string FilterName { get; set; }
            public float Angle { get; set; }
            public bool IsCustom { get; set; }
            public bool IsCalibrated { get; set; }
        }

        #endregion

        private void CustomAnglesControl_Load(object sender, EventArgs e)
        {

        }
    }
}
