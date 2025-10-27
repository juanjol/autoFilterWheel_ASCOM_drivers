using System;
using System.Collections.Generic;
using System.Linq;

namespace ASCOM.autoFilterWheel.FilterWheel
{
    /// <summary>
    /// Provides help information and autocomplete support for serial commands
    /// </summary>
    internal static class CommandHelp
    {
        /// <summary>
        /// Command information for help and autocomplete
        /// </summary>
        public class CommandInfo
        {
            public string Command { get; set; }
            public string Description { get; set; }
            public string Format { get; set; }
            public string Example { get; set; }
            public string Response { get; set; }

            public override string ToString()
            {
                return $"{Command} - {Description}";
            }

            public string GetDetailedHelp()
            {
                return $"Command: {Command}\n" +
                       $"Description: {Description}\n" +
                       $"Format: {Format}\n" +
                       $"Example: {Example}\n" +
                       $"Response: {Response}";
            }
        }

        /// <summary>
        /// Dictionary of all available commands with their help information
        /// </summary>
        private static readonly Dictionary<string, CommandInfo> commands = new Dictionary<string, CommandInfo>(StringComparer.OrdinalIgnoreCase)
        {
            // Position Commands
            { "GP", new CommandInfo { Command = "GP", Description = "Get current filter position", Format = "GP", Example = "GP", Response = "P1, P2, P3, etc." } },
            { "MP", new CommandInfo { Command = "MP", Description = "Move to position", Format = "MP[1-9]", Example = "MP3", Response = "M3" } },
            { "SP", new CommandInfo { Command = "SP", Description = "Set current position", Format = "SP[1-9]", Example = "SP1", Response = "S1" } },

            // System Commands
            { "CAL", new CommandInfo { Command = "CAL", Description = "Calibrate home position", Format = "CAL", Example = "CAL", Response = "CALIBRATED" } },
            { "STATUS", new CommandInfo { Command = "STATUS", Description = "Get system status", Format = "STATUS", Example = "STATUS", Response = "STATUS:POS=3,MOVING=NO,CAL=YES,ANGLE=144.0,ERROR=0.5" } },
            { "VER", new CommandInfo { Command = "VER", Description = "Get firmware version", Format = "VER", Example = "VER", Response = "VERSION:2.1.0" } },
            { "ID", new CommandInfo { Command = "ID", Description = "Get device identification", Format = "ID", Example = "ID", Response = "DEVICE_ID:ESP32_FILTER_WHEEL-v2.1" } },
            { "STOP", new CommandInfo { Command = "STOP", Description = "Emergency stop movement", Format = "STOP", Example = "STOP", Response = "STOPPED" } },

            // Configuration Commands
            { "GETCONFIG", new CommandInfo { Command = "GETCONFIG", Description = "Get complete device configuration (filters, motor, display, status)", Format = "GETCONFIG", Example = "GETCONFIG", Response = "Multi-line: FILTER_COUNT:5, FILTER_NAME:1:Luminance, MOTOR_SPEED:4000, DISPLAY_BRIGHTNESS:128, STATUS_POSITION:1, ... CONFIG_END" } },

            // Filter Information Commands
            { "GF", new CommandInfo { Command = "GF", Description = "Get number of filters", Format = "GF", Example = "GF", Response = "F5" } },
            { "FC", new CommandInfo { Command = "FC", Description = "Set filter count", Format = "FC[3-9]", Example = "FC5", Response = "FC5" } },
            { "GN", new CommandInfo { Command = "GN", Description = "Get filter name", Format = "GN[1-9]", Example = "GN2", Response = "N2:Red" } },
            { "SN", new CommandInfo { Command = "SN", Description = "Set filter name", Format = "SN[1-9]:name", Example = "SN1:Luminance", Response = "SN1:Luminance" } },

            // Manual Control Commands
            { "SF", new CommandInfo { Command = "SF", Description = "Step forward", Format = "SF[steps]", Example = "SF100", Response = "SF100" } },
            { "SB", new CommandInfo { Command = "SB", Description = "Step backward", Format = "SB[steps]", Example = "SB50", Response = "SB50" } },

            // Motor Control Commands
            { "ME", new CommandInfo { Command = "ME", Description = "Enable motor power", Format = "ME", Example = "ME", Response = "MOTOR_ENABLED" } },
            { "MD", new CommandInfo { Command = "MD", Description = "Disable motor power", Format = "MD", Example = "MD", Response = "MOTOR_DISABLED" } },

            // Motor Configuration Commands
            { "GMC", new CommandInfo { Command = "GMC", Description = "Get motor configuration", Format = "GMC", Example = "GMC", Response = "MOTOR_CONFIG:SPEED=4000,MAX_SPEED=5000,..." } },
            { "MS", new CommandInfo { Command = "MS", Description = "Set motor speed", Format = "MS[speed]", Example = "MS4000", Response = "MS4000" } },
            { "MXS", new CommandInfo { Command = "MXS", Description = "Set max motor speed", Format = "MXS[speed]", Example = "MXS5000", Response = "MXS5000" } },
            { "MA", new CommandInfo { Command = "MA", Description = "Set motor acceleration", Format = "MA[accel]", Example = "MA100000", Response = "MA100000" } },
            { "MDD", new CommandInfo { Command = "MDD", Description = "Set motor disable delay (ms)", Format = "MDD[delay]", Example = "MDD1000", Response = "MDD1000" } },
            { "RMC", new CommandInfo { Command = "RMC", Description = "Reset motor configuration to defaults", Format = "RMC", Example = "RMC", Response = "MOTOR_CONFIG_RESET" } },

            // Direction Inversion Commands
            { "MINV0", new CommandInfo { Command = "MINV0", Description = "Set motor direction normal", Format = "MINV0", Example = "MINV0", Response = "MINV:Normal" } },
            { "MINV1", new CommandInfo { Command = "MINV1", Description = "Set motor direction inverted", Format = "MINV1", Example = "MINV1", Response = "MINV:Inverted" } },
            { "GMINV", new CommandInfo { Command = "GMINV", Description = "Get motor inversion status", Format = "GMINV", Example = "GMINV", Response = "GMINV:0 (Normal) or GMINV:1 (Inverted)" } },
            { "ENCINV0", new CommandInfo { Command = "ENCINV0", Description = "Set encoder direction normal", Format = "ENCINV0", Example = "ENCINV0", Response = "ENCINV:Normal" } },
            { "ENCINV1", new CommandInfo { Command = "ENCINV1", Description = "Set encoder direction inverted", Format = "ENCINV1", Example = "ENCINV1", Response = "ENCINV:Inverted" } },
            { "GENCINV", new CommandInfo { Command = "GENCINV", Description = "Get encoder inversion status", Format = "GENCINV", Example = "GENCINV", Response = "GENCINV:0 (Normal) or GENCINV:1 (Inverted)" } },

            // Display Commands
            { "ROTATE0", new CommandInfo { Command = "ROTATE0", Description = "Display rotation normal", Format = "ROTATE0", Example = "ROTATE0", Response = "ROTATE0" } },
            { "ROTATE1", new CommandInfo { Command = "ROTATE1", Description = "Display rotation 180°", Format = "ROTATE1", Example = "ROTATE1", Response = "ROTATE1" } },
            { "DISPMODE0", new CommandInfo { Command = "DISPMODE0", Description = "Set display to minimal mode", Format = "DISPMODE0", Example = "DISPMODE0", Response = "DISPMODE0:Minimal" } },
            { "DISPMODE1", new CommandInfo { Command = "DISPMODE1", Description = "Set display to detailed mode", Format = "DISPMODE1", Example = "DISPMODE1", Response = "DISPMODE1:Detailed" } },
            { "BRIGHT", new CommandInfo { Command = "BRIGHT", Description = "Set display brightness", Format = "BRIGHT[0-255]", Example = "BRIGHT128", Response = "BRIGHT:128" } },
            { "DISPON", new CommandInfo { Command = "DISPON", Description = "Turn display on", Format = "DISPON", Example = "DISPON", Response = "DISPON:OK" } },
            { "DISPOFF", new CommandInfo { Command = "DISPOFF", Description = "Turn display off", Format = "DISPOFF", Example = "DISPOFF", Response = "DISPOFF:OK" } },
            { "DISPPOWER", new CommandInfo { Command = "DISPPOWER", Description = "Set display power mode", Format = "DISPPOWER[0|1|2]", Example = "DISPPOWER0 (Auto), DISPPOWER1 (AlwaysOn), DISPPOWER2 (AlwaysOff)", Response = "DISPPOWER:0:Auto" } },
            { "DISPTIMEOUT", new CommandInfo { Command = "DISPTIMEOUT", Description = "Set display auto-off timeout", Format = "DISPTIMEOUT[seconds]", Example = "DISPTIMEOUT30", Response = "DISPTIMEOUT:30:Seconds" } },
            { "DISPLAY", new CommandInfo { Command = "DISPLAY", Description = "Get display information", Format = "DISPLAY", Example = "DISPLAY", Response = "DISPLAY:Size=128x64,Rotation=Normal,..." } },

            // Encoder Commands
            { "ENCSTATUS", new CommandInfo { Command = "ENCSTATUS", Description = "Get encoder status", Format = "ENCSTATUS", Example = "ENCSTATUS", Response = "ENCSTATUS:Angle=144.5,Expected=144.0,Error=0.5,..." } },
            { "ENCDIR", new CommandInfo { Command = "ENCDIR", Description = "Get rotation direction", Format = "ENCDIR", Example = "ENCDIR", Response = "ENCDIR:CW or ENCDIR:CCW or ENCDIR:STOPPED" } },
            { "ENCRAW", new CommandInfo { Command = "ENCRAW", Description = "Get raw encoder data", Format = "ENCRAW", Example = "ENCRAW", Response = "ENCRAW:RawCounts=12345,RawAngle=144.5,..." } },

            // Guided Calibration Commands
            { "CALSTART", new CommandInfo { Command = "CALSTART", Description = "Start guided calibration", Format = "CALSTART", Example = "CALSTART", Response = "CALSTART:OK" } },
            { "CALCFM", new CommandInfo { Command = "CALCFM", Description = "Confirm guided calibration", Format = "CALCFM", Example = "CALCFM", Response = "CALCFM:Complete" } },

            // Custom Angles Calibration Commands
            { "CALWIZ", new CommandInfo { Command = "CALWIZ", Description = "Start calibration wizard", Format = "CALWIZ", Example = "CALWIZ", Response = "CALWIZ:Wizard started. Move to position 1..." } },
            { "CALWIZSET", new CommandInfo { Command = "CALWIZSET", Description = "Set position in calibration wizard", Format = "CALWIZSET[1-9]", Example = "CALWIZSET1", Response = "CALWIZSET1:OK (0.00°)" } },
            { "CALWIZFIN", new CommandInfo { Command = "CALWIZFIN", Description = "Finish calibration wizard", Format = "CALWIZFIN", Example = "CALWIZFIN", Response = "CALWIZFIN:Calibration complete! 5 positions saved." } },
            { "SETANG", new CommandInfo { Command = "SETANG", Description = "Set custom angle for position", Format = "SETANG[1-9]:angle", Example = "SETANG2:72.5", Response = "SETANG:Position 2 set to 72.5°" } },
            { "GETANG", new CommandInfo { Command = "GETANG", Description = "Get custom angle(s)", Format = "GETANG or GETANG[1-9]", Example = "GETANG1 or GETANG", Response = "GETANG1:0.00° (custom)" } },
            { "CLEARANG", new CommandInfo { Command = "CLEARANG", Description = "Clear all custom angles", Format = "CLEARANG", Example = "CLEARANG", Response = "CLEARANG:All custom angles cleared. Using default..." } },

            // Utility Commands
            { "HELP", new CommandInfo { Command = "HELP", Description = "Show firmware help", Format = "HELP", Example = "HELP", Response = "(Firmware help text)" } },
            { "TESTMOTOR", new CommandInfo { Command = "TESTMOTOR", Description = "Test motor directly", Format = "TESTMOTOR", Example = "TESTMOTOR", Response = "TESTMOTOR:Running test..." } },
        };

        /// <summary>
        /// Get all command names for autocomplete
        /// </summary>
        public static string[] GetAllCommandNames()
        {
            return commands.Keys.OrderBy(k => k).ToArray();
        }

        /// <summary>
        /// Get command info for a specific command
        /// </summary>
        public static CommandInfo GetCommandInfo(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return null;

            // Try exact match first
            if (commands.TryGetValue(command.Trim(), out CommandInfo info))
                return info;

            // Try matching command prefix (e.g., "MP3" matches "MP")
            foreach (var kvp in commands)
            {
                if (command.StartsWith(kvp.Key, StringComparison.OrdinalIgnoreCase))
                    return kvp.Value;
            }

            return null;
        }

        /// <summary>
        /// Search commands by partial match
        /// </summary>
        public static CommandInfo[] SearchCommands(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
                return commands.Values.OrderBy(c => c.Command).ToArray();

            searchText = searchText.Trim().ToUpperInvariant();

            return commands.Values
                .Where(c => c.Command.Contains(searchText) ||
                           c.Description.ToUpperInvariant().Contains(searchText))
                .OrderBy(c => c.Command)
                .ToArray();
        }

        /// <summary>
        /// Get autocomplete suggestions for partial input
        /// </summary>
        public static string[] GetAutocompleteSuggestions(string partialCommand)
        {
            if (string.IsNullOrWhiteSpace(partialCommand))
                return GetAllCommandNames();

            partialCommand = partialCommand.Trim().ToUpperInvariant();

            return commands.Keys
                .Where(k => k.StartsWith(partialCommand, StringComparison.OrdinalIgnoreCase))
                .OrderBy(k => k)
                .ToArray();
        }

        /// <summary>
        /// Validate if a command exists
        /// </summary>
        public static bool IsValidCommand(string command)
        {
            return GetCommandInfo(command) != null;
        }
    }
}
