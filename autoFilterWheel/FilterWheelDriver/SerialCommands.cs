using System;

namespace ASCOM.autoFilterWheel.FilterWheel
{
    /// <summary>
    /// Serial communication protocol constants for ESP32-C3 Filter Wheel Controller
    /// </summary>
    internal static class SerialCommands
    {
        // Command prefix
        public const string COMMAND_PREFIX = "#";

        // Position Commands
        public const string CMD_GET_POSITION = "GP";       // Get current position -> Returns: P[1-5]
        public const string CMD_MOVE_POSITION = "MP";      // Move to position (MP1, MP2, etc.) -> Returns: M[1-5]
        public const string CMD_SET_POSITION = "SP";       // Set current position (SP1, SP2, etc.) -> Returns: S[1-5]

        // System Commands
        public const string CMD_CALIBRATE = "CAL";         // Calibrate home position -> Returns: CALIBRATED
        public const string CMD_STATUS = "STATUS";         // Get system status -> Returns: STATUS:POS=n,MOVING=YES/NO,CAL=YES/NO,ANGLE=x.x,ERROR=n
        public const string CMD_VERSION = "VER";           // Get firmware version -> Returns: VERSION:x.x.x
        public const string CMD_DEVICE_ID = "ID";          // Get device identification -> Returns: DEVICE_ID:ESP32FW-5POS-V1.0
        public const string CMD_STOP = "STOP";            // Emergency stop -> Returns: STOPPED

        // Filter Information Commands
        public const string CMD_GET_FILTERS = "GF";        // Get number of filters -> Returns: F[n]
        public const string CMD_SET_FILTER_COUNT = "FC";   // Set filter count (FC3, FC4, etc.) -> Returns: FC[n]
        public const string CMD_GET_FILTER_NAME = "GN";    // Get filter name (GN1, GN2, etc.) -> Returns: N[n]:name
        public const string CMD_SET_FILTER_NAME = "SN";    // Set filter name (SN1:Luminance, SN2:Red, etc.) -> Returns: SN[n]:name

        // Manual Control Commands
        public const string CMD_STEP_FORWARD = "SF";       // Step forward X steps (SF100) -> Returns: SF[n]
        public const string CMD_STEP_BACKWARD = "SB";      // Step backward X steps (SB50) -> Returns: SB[n]

        // Motor Control Commands
        public const string CMD_MOTOR_ENABLE = "ME";       // Enable motor power -> Returns: MOTOR_ENABLED
        public const string CMD_MOTOR_DISABLE = "MD";      // Disable motor power -> Returns: MOTOR_DISABLED

        // Motor Configuration Commands
        public const string CMD_GET_MOTOR_CONFIG = "GMC";  // Get motor configuration -> Returns: MOTOR_CONFIG:SPEED=n,MAX_SPEED=n,ACCEL=n,DISABLE_DELAY=n
        public const string CMD_SET_MOTOR_SPEED = "MS";    // Set motor speed (MS1000) -> Returns: MS[n]
        public const string CMD_SET_MAX_SPEED = "MXS";     // Set max motor speed (MXS2000) -> Returns: MXS[n]
        public const string CMD_SET_ACCELERATION = "MA";   // Set motor acceleration (MA500) -> Returns: MA[n]
        public const string CMD_SET_DISABLE_DELAY = "MDD"; // Set motor disable delay (MDD1000) -> Returns: MDD[n]
        public const string CMD_RESET_MOTOR_CONFIG = "RMC"; // Reset motor configuration -> Returns: MOTOR_CONFIG_RESET

        // Display Commands
        public const string CMD_ROTATE_DISPLAY = "ROTATE"; // Rotate display 180° (ROTATE0/ROTATE1) -> Returns: ROTATE[0/1]
        public const string CMD_GET_DISPLAY_INFO = "DISPLAY"; // Get display information -> Returns: DISPLAY:Size=128x64,Rotation=...

        // Encoder Commands
        public const string CMD_GET_ENCODER_STATUS = "ENCSTATUS"; // Get encoder status -> Returns: ENCSTATUS:Angle=x.x,Expected=x.x,Error=x.x,...
        public const string CMD_GET_ROTATION_DIR = "ENCDIR";      // Get rotation direction -> Returns: ENCDIR:CW/CCW/STOPPED
        public const string CMD_GET_ENCODER_RAW = "ENCRAW";       // Get raw encoder data -> Returns: ENCRAW:RawCounts=n,RawAngle=x.x,...

        // Guided Calibration Commands
        public const string CMD_START_GUIDED_CAL = "CALSTART";    // Start guided calibration -> Returns: CALSTART:OK
        public const string CMD_CONFIRM_GUIDED_CAL = "CALCFM";    // Confirm guided calibration -> Returns: CALCFM:Complete

        // Utility Commands
        public const string CMD_HELP = "HELP";             // Show help -> Returns: (help text)
        public const string CMD_TEST_MOTOR = "TESTMOTOR";  // Test motor directly -> Returns: TESTMOTOR:Running...

        // Response prefixes
        public const string RESP_POSITION = "P";
        public const string RESP_MOVED = "M";
        public const string RESP_SET = "S";
        public const string RESP_FILTERS = "F";
        public const string RESP_NAME = "N";
        public const string RESP_STATUS = "STATUS:";
        public const string RESP_VERSION = "VERSION:";
        public const string RESP_DEVICE_ID = "DEVICE_ID:";
        public const string RESP_NAMES = "NAMES:";
        public const string RESP_MOTOR_CONFIG = "MOTOR_CONFIG:";
        public const string RESP_DISPLAY = "DISPLAY:";
        public const string RESP_ENCSTATUS = "ENCSTATUS:";
        public const string RESP_ENCDIR = "ENCDIR:";
        public const string RESP_ENCRAW = "ENCRAW:";

        // Response values
        public const string RESP_CALIBRATED = "CALIBRATED";
        public const string RESP_STOPPED = "STOPPED";
        public const string RESP_MOTOR_ENABLED = "MOTOR_ENABLED";
        public const string RESP_MOTOR_DISABLED = "MOTOR_DISABLED";
        public const string RESP_MOTOR_CONFIG_RESET = "MOTOR_CONFIG_RESET";

        // Error responses
        public const string ERROR_PREFIX = "ERROR:";
        public const string ERROR_INVALID_POSITION = "ERROR:INVALID_POSITION";
        public const string ERROR_INVALID_PARAMETER = "ERROR:INVALID_PARAMETER";
        public const string ERROR_INVALID_FORMAT = "ERROR:INVALID_FORMAT";
        public const string ERROR_SYSTEM_BUSY = "ERROR:SYSTEM_BUSY";
        public const string ERROR_UNKNOWN_COMMAND = "ERROR:UNKNOWN_COMMAND";

        // Communication parameters
        public const int BAUD_RATE = 115200;
        public const int COMMAND_TIMEOUT_MS = 1000;
        public const int MOVEMENT_TIMEOUT_MS = 10000;
        public const int MAX_COMMAND_LENGTH = 50;

        // Filter wheel specifications (from firmware config.h)
        public const int MIN_FILTER_COUNT = 3;
        public const int MAX_FILTER_COUNT = 9;  // Firmware v2 supports 3-9 filters
        public const int DEFAULT_FILTER_COUNT = 5;
        public const int STEPS_PER_REVOLUTION = 2048;  // 28BYJ-48 stepper motor
        public const int MAX_FILTER_NAME_LENGTH = 15;

        // Expected device identification (firmware v2)
        public const string EXPECTED_DEVICE_ID_PREFIX = "ESP32_FILTER_WHEEL";
        public const string FIRMWARE_VERSION_PREFIX = "VERSION:";

        /// <summary>
        /// Formats a command with the proper prefix and terminator
        /// Firmware expects: #COMMAND\n (LF line ending)
        /// </summary>
        public static string FormatCommand(string command)
        {
            return COMMAND_PREFIX + command + "\n";
        }

        /// <summary>
        /// Formats a move position command
        /// </summary>
        public static string FormatMoveCommand(int position, int maxFilters = MAX_FILTER_COUNT)
        {
            if (position < 1 || position > maxFilters)
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be between 1 and {maxFilters}");

            return FormatCommand(CMD_MOVE_POSITION + position);
        }

        /// <summary>
        /// Formats a set position command
        /// </summary>
        public static string FormatSetCommand(int position, int maxFilters = MAX_FILTER_COUNT)
        {
            if (position < 1 || position > maxFilters)
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be between 1 and {maxFilters}");

            return FormatCommand(CMD_SET_POSITION + position);
        }

        /// <summary>
        /// Formats a get filter name command
        /// </summary>
        public static string FormatGetNameCommand(int position, int maxFilters = MAX_FILTER_COUNT)
        {
            if (position < 1 || position > maxFilters)
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be between 1 and {maxFilters}");

            return FormatCommand(CMD_GET_FILTER_NAME + position);
        }

        /// <summary>
        /// Formats a set filter name command
        /// </summary>
        public static string FormatSetNameCommand(int position, string filterName)
        {
            if (position < 1 || position > MAX_FILTER_COUNT)
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be between 1 and {MAX_FILTER_COUNT}");

            if (string.IsNullOrEmpty(filterName))
                throw new ArgumentException("Filter name cannot be null or empty", nameof(filterName));

            // Limit name length to match firmware MAX_FILTER_NAME_LENGTH
            if (filterName.Length > MAX_FILTER_NAME_LENGTH)
                filterName = filterName.Substring(0, MAX_FILTER_NAME_LENGTH);

            return FormatCommand(CMD_SET_FILTER_NAME + position + ":" + filterName);
        }

        /// <summary>
        /// Formats a set filter count command
        /// </summary>
        public static string FormatSetFilterCountCommand(int filterCount)
        {
            if (filterCount < MIN_FILTER_COUNT || filterCount > MAX_FILTER_COUNT)
                throw new ArgumentOutOfRangeException(nameof(filterCount), $"Filter count must be between {MIN_FILTER_COUNT} and {MAX_FILTER_COUNT}");

            return FormatCommand(CMD_SET_FILTER_COUNT + filterCount);
        }

        /// <summary>
        /// Parses a position response (P1, P2, etc.)
        /// </summary>
        public static int ParsePositionResponse(string response, int maxFilters = MAX_FILTER_COUNT)
        {
            if (string.IsNullOrEmpty(response))
                throw new ArgumentException("Response is null or empty");

            response = response.Trim();

            if (response.StartsWith(RESP_POSITION) && response.Length > 1)
            {
                if (int.TryParse(response.Substring(1), out int position))
                {
                    if (position >= 1 && position <= maxFilters)
                        return position;
                }
            }

            throw new FormatException($"Invalid position response: {response}");
        }

        /// <summary>
        /// Checks if the response is an error
        /// </summary>
        public static bool IsErrorResponse(string response)
        {
            return !string.IsNullOrEmpty(response) && response.StartsWith(ERROR_PREFIX);
        }

        /// <summary>
        /// Extracts the error message from an error response
        /// </summary>
        public static string GetErrorMessage(string response)
        {
            if (IsErrorResponse(response))
            {
                return response.Substring(ERROR_PREFIX.Length);
            }
            return string.Empty;
        }
    }
}