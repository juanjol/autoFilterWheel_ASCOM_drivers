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
        public const string CMD_STEP_TO = "ST";           // Step to absolute position (ST1024) -> Returns: ST[n]
        public const string CMD_GET_STEP = "GST";         // Get current step position -> Returns: STEP:[n]

        // Motor Control Commands
        public const string CMD_MOTOR_ENABLE = "ME";       // Enable motor power -> Returns: MOTOR_ENABLED
        public const string CMD_MOTOR_DISABLE = "MD";      // Disable motor power -> Returns: MOTOR_DISABLED

        // Revolution Calibration Commands
        public const string CMD_START_REV_CAL = "REVCAL";  // Start full revolution calibration
        public const string CMD_REV_CAL_ADJUST_PLUS = "RCP"; // Add steps during revolution calibration (RCP10)
        public const string CMD_REV_CAL_ADJUST_MINUS = "RCM"; // Subtract steps during revolution calibration (RCM5)
        public const string CMD_FINISH_REV_CAL = "RCFIN";  // Finish revolution calibration and save result

        // Response prefixes
        public const string RESP_POSITION = "P";
        public const string RESP_MOVED = "M";
        public const string RESP_SET = "S";
        public const string RESP_FILTERS = "F";
        public const string RESP_NAME = "N";
        public const string RESP_STATUS = "STATUS:";
        public const string RESP_VERSION = "VERSION:";
        public const string RESP_DEVICE_ID = "DEVICE_ID:";
        public const string RESP_STEP = "STEP:";
        public const string RESP_NAMES = "NAMES:";

        // Response values
        public const string RESP_CALIBRATED = "CALIBRATED";
        public const string RESP_STOPPED = "STOPPED";
        public const string RESP_MOTOR_ENABLED = "MOTOR_ENABLED";
        public const string RESP_MOTOR_DISABLED = "MOTOR_DISABLED";

        // Error responses
        public const string ERROR_PREFIX = "ERROR:";
        public const string ERROR_INVALID_POSITION = "ERROR:INVALID_POSITION";
        public const string ERROR_INVALID_FILTER = "ERROR:INVALID_FILTER";
        public const string ERROR_INVALID_STEP_COUNT = "ERROR:INVALID_STEP_COUNT";
        public const string ERROR_UNKNOWN_COMMAND = "ERROR:UNKNOWN_COMMAND";
        public const string ERROR_BUFFER_OVERFLOW = "ERROR:BUFFER_OVERFLOW";

        // Communication parameters
        public const int BAUD_RATE = 115200;
        public const int COMMAND_TIMEOUT_MS = 1000;
        public const int MOVEMENT_TIMEOUT_MS = 10000;
        public const int MAX_COMMAND_LENGTH = 50;

        // Filter wheel specifications (from Arduino config)
        public const int NUM_FILTERS = 5;
        public const int STEPS_PER_REVOLUTION = 2048;
        public const int STEPS_PER_FILTER = STEPS_PER_REVOLUTION / NUM_FILTERS;

        // Expected device identification
        public const string EXPECTED_DEVICE_ID = "ESP32FW-5POS-V1.0";

        /// <summary>
        /// Formats a command with the proper prefix and terminator
        /// </summary>
        public static string FormatCommand(string command)
        {
            return COMMAND_PREFIX + command + "\r";
        }

        /// <summary>
        /// Formats a move position command
        /// </summary>
        public static string FormatMoveCommand(int position)
        {
            if (position < 1 || position > NUM_FILTERS)
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be between 1 and {NUM_FILTERS}");

            return FormatCommand(CMD_MOVE_POSITION + position);
        }

        /// <summary>
        /// Formats a set position command
        /// </summary>
        public static string FormatSetCommand(int position)
        {
            if (position < 1 || position > NUM_FILTERS)
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be between 1 and {NUM_FILTERS}");

            return FormatCommand(CMD_SET_POSITION + position);
        }

        /// <summary>
        /// Formats a get filter name command
        /// </summary>
        public static string FormatGetNameCommand(int position)
        {
            if (position < 1 || position > NUM_FILTERS)
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be between 1 and {NUM_FILTERS}");

            return FormatCommand(CMD_GET_FILTER_NAME + position);
        }

        /// <summary>
        /// Formats a set filter name command
        /// </summary>
        public static string FormatSetNameCommand(int position, string filterName)
        {
            if (position < 1 || position > 8) // Support up to 8 filters
                throw new ArgumentOutOfRangeException(nameof(position), $"Position must be between 1 and 8");

            if (string.IsNullOrEmpty(filterName))
                throw new ArgumentException("Filter name cannot be null or empty", nameof(filterName));

            // Limit name length to match Arduino MAX_FILTER_NAME_LENGTH
            if (filterName.Length > 15)
                filterName = filterName.Substring(0, 15);

            return FormatCommand(CMD_SET_FILTER_NAME + position + ":" + filterName);
        }

        /// <summary>
        /// Formats a set filter count command
        /// </summary>
        public static string FormatSetFilterCountCommand(int filterCount)
        {
            if (filterCount < 3 || filterCount > 8) // Match Arduino MIN/MAX_FILTER_COUNT
                throw new ArgumentOutOfRangeException(nameof(filterCount), $"Filter count must be between 3 and 8");

            return FormatCommand(CMD_SET_FILTER_COUNT + filterCount);
        }

        /// <summary>
        /// Parses a position response (P1, P2, etc.)
        /// </summary>
        public static int ParsePositionResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
                throw new ArgumentException("Response is null or empty");

            response = response.Trim();

            if (response.StartsWith(RESP_POSITION) && response.Length > 1)
            {
                if (int.TryParse(response.Substring(1), out int position))
                {
                    if (position >= 1 && position <= NUM_FILTERS)
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