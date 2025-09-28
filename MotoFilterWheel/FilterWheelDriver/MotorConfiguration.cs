using System;

namespace ASCOM.autoFilterWheel.FilterWheel
{
    /// <summary>
    /// Motor configuration data structure
    /// </summary>
    public class MotorConfiguration
    {
        /// <summary>
        /// Motor speed
        /// </summary>
        public int Speed { get; set; } = SerialCommands.DEFAULT_MOTOR_SPEED;

        /// <summary>
        /// Maximum motor speed
        /// </summary>
        public int MaxSpeed { get; set; } = SerialCommands.DEFAULT_MAX_SPEED;

        /// <summary>
        /// Motor acceleration
        /// </summary>
        public int Acceleration { get; set; } = SerialCommands.DEFAULT_ACCELERATION;

        /// <summary>
        /// Motor disable delay in milliseconds
        /// </summary>
        public int DisableDelay { get; set; } = SerialCommands.DEFAULT_DISABLE_DELAY;

        /// <summary>
        /// Steps per revolution
        /// </summary>
        public int StepsPerRevolution { get; set; } = SerialCommands.DEFAULT_STEPS_PER_REV;

        /// <summary>
        /// Returns a string representation of the motor configuration
        /// </summary>
        public override string ToString()
        {
            return $"Speed: {Speed}, MaxSpeed: {MaxSpeed}, Acceleration: {Acceleration}, " +
                   $"DisableDelay: {DisableDelay}, StepsPerRevolution: {StepsPerRevolution}";
        }

        /// <summary>
        /// Validates the motor configuration values
        /// </summary>
        public bool IsValid()
        {
            return Speed >= SerialCommands.MIN_MOTOR_SPEED && Speed <= SerialCommands.MAX_MOTOR_SPEED &&
                   MaxSpeed >= SerialCommands.MIN_MAX_SPEED && MaxSpeed <= SerialCommands.MAX_MAX_SPEED &&
                   Acceleration >= SerialCommands.MIN_ACCELERATION && Acceleration <= SerialCommands.MAX_ACCELERATION &&
                   DisableDelay >= SerialCommands.MIN_DISABLE_DELAY && DisableDelay <= SerialCommands.MAX_DISABLE_DELAY &&
                   StepsPerRevolution >= SerialCommands.MIN_STEPS_PER_REV && StepsPerRevolution <= SerialCommands.MAX_STEPS_PER_REV;
        }
    }
}