using System;

namespace ASCOM.autoFilterWheel.FilterWheel
{
    partial class SetupDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Clean up serial connection
                if (serialComm != null)
                {
                    try
                    {
                        if (serialComm.IsConnected)
                            serialComm.Disconnect();
                        serialComm.Dispose();
                    }
                    catch (Exception ex)
                    {
                        // Use try-catch to avoid exceptions during disposal
                        System.Diagnostics.Debug.WriteLine($"Error disposing serial connection: {ex.Message}");
                    }
                    serialComm = null;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDialogForm));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.labelCompilationDate = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfiguration = new System.Windows.Forms.TabPage();
            this.labelFilterCount = new System.Windows.Forms.Label();
            this.comboBoxFilterCount = new System.Windows.Forms.ComboBox();
            this.labelFilterNames = new System.Windows.Forms.Label();
            this.labelFilter1 = new System.Windows.Forms.Label();
            this.textBoxFilter1 = new System.Windows.Forms.TextBox();
            this.labelFilter2 = new System.Windows.Forms.Label();
            this.textBoxFilter2 = new System.Windows.Forms.TextBox();
            this.labelFilter3 = new System.Windows.Forms.Label();
            this.textBoxFilter3 = new System.Windows.Forms.TextBox();
            this.labelFilter4 = new System.Windows.Forms.Label();
            this.textBoxFilter4 = new System.Windows.Forms.TextBox();
            this.labelFilter5 = new System.Windows.Forms.Label();
            this.textBoxFilter5 = new System.Windows.Forms.TextBox();
            this.labelFilter6 = new System.Windows.Forms.Label();
            this.textBoxFilter6 = new System.Windows.Forms.TextBox();
            this.labelFilter7 = new System.Windows.Forms.Label();
            this.textBoxFilter7 = new System.Windows.Forms.TextBox();
            this.labelFilter8 = new System.Windows.Forms.Label();
            this.textBoxFilter8 = new System.Windows.Forms.TextBox();
            this.btnSetFilterConfig = new System.Windows.Forms.Button();
            this.tabPageMotorConfig = new System.Windows.Forms.TabPage();
            this.groupBoxDirection = new System.Windows.Forms.GroupBox();
            this.btnSetDirectionConfig = new System.Windows.Forms.Button();
            this.btnGetDirectionConfig = new System.Windows.Forms.Button();
            this.checkBoxReverse = new System.Windows.Forms.CheckBox();
            this.radioBidirectional = new System.Windows.Forms.RadioButton();
            this.radioUnidirectional = new System.Windows.Forms.RadioButton();
            this.groupBoxMotorSettings = new System.Windows.Forms.GroupBox();
            this.btnResetMotorConfig = new System.Windows.Forms.Button();
            this.btnSetMotorConfig = new System.Windows.Forms.Button();
            this.btnGetMotorConfig = new System.Windows.Forms.Button();
            this.numericDisableDelay = new System.Windows.Forms.NumericUpDown();
            this.labelDisableDelay = new System.Windows.Forms.Label();
            this.numericAcceleration = new System.Windows.Forms.NumericUpDown();
            this.labelAcceleration = new System.Windows.Forms.Label();
            this.numericMaxSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelMaxSpeed = new System.Windows.Forms.Label();
            this.numericMotorSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelMotorSpeed = new System.Windows.Forms.Label();
            this.numericStepsPerRev = new System.Windows.Forms.NumericUpDown();
            this.labelStepsPerRev = new System.Windows.Forms.Label();
            this.btnGetStepsPerRev = new System.Windows.Forms.Button();
            this.btnSetStepsPerRev = new System.Windows.Forms.Button();
            this.tabPageCalibration = new System.Windows.Forms.TabPage();
            this.groupBoxBacklashCalibration = new System.Windows.Forms.GroupBox();
            this.labelBacklashStatus = new System.Windows.Forms.Label();
            this.btnFinishBacklash = new System.Windows.Forms.Button();
            this.btnBacklashMark = new System.Windows.Forms.Button();
            this.btnBacklashStep = new System.Windows.Forms.Button();
            this.btnStartBacklash = new System.Windows.Forms.Button();
            this.comboBoxBacklashSteps = new System.Windows.Forms.ComboBox();
            this.labelBacklashSteps = new System.Windows.Forms.Label();
            this.groupBoxCalibration = new System.Windows.Forms.GroupBox();
            this.labelRevCalStatus = new System.Windows.Forms.Label();
            this.btnFinishCalibration = new System.Windows.Forms.Button();
            this.btnCalibrationBackward = new System.Windows.Forms.Button();
            this.btnCalibrationForward = new System.Windows.Forms.Button();
            this.btnStartCalibration = new System.Windows.Forms.Button();
            this.comboBoxSteps = new System.Windows.Forms.ComboBox();
            this.labelSteps = new System.Windows.Forms.Label();
            this.tabPageManualControl = new System.Windows.Forms.TabPage();
            this.groupBoxSystemInfo = new System.Windows.Forms.GroupBox();
            this.textBoxSystemInfo = new System.Windows.Forms.TextBox();
            this.btnGetDeviceId = new System.Windows.Forms.Button();
            this.btnGetVersion = new System.Windows.Forms.Button();
            this.btnGetStatus = new System.Windows.Forms.Button();
            this.groupBoxMotorPower = new System.Windows.Forms.GroupBox();
            this.textBoxCurrentStep = new System.Windows.Forms.TextBox();
            this.labelCurrentStep = new System.Windows.Forms.Label();
            this.btnDisableMotor = new System.Windows.Forms.Button();
            this.btnEnableMotor = new System.Windows.Forms.Button();
            this.groupBoxStepping = new System.Windows.Forms.GroupBox();
            this.btnGetStepPosition = new System.Windows.Forms.Button();
            this.btnStepTo = new System.Windows.Forms.Button();
            this.numericAbsoluteStep = new System.Windows.Forms.NumericUpDown();
            this.labelAbsoluteStep = new System.Windows.Forms.Label();
            this.btnStepBackward = new System.Windows.Forms.Button();
            this.btnStepForward = new System.Windows.Forms.Button();
            this.numericStepAmount = new System.Windows.Forms.NumericUpDown();
            this.labelStepAmount = new System.Windows.Forms.Label();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageConfiguration.SuspendLayout();
            this.tabPageMotorConfig.SuspendLayout();
            this.groupBoxDirection.SuspendLayout();
            this.groupBoxMotorSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDisableDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAcceleration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMotorSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericStepsPerRev)).BeginInit();
            this.tabPageCalibration.SuspendLayout();
            this.groupBoxBacklashCalibration.SuspendLayout();
            this.groupBoxCalibration.SuspendLayout();
            this.tabPageManualControl.SuspendLayout();
            this.groupBoxSystemInfo.SuspendLayout();
            this.groupBoxMotorPower.SuspendLayout();
            this.groupBoxStepping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericAbsoluteStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericStepAmount)).BeginInit();
            this.tabPageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(418, 384);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(59, 24);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.CmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(483, 384);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 24);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = ((System.Drawing.Image)(resources.GetObject("picASCOM.Image")));
            this.picASCOM.InitialImage = ((System.Drawing.Image)(resources.GetObject("picASCOM.InitialImage")));
            this.picASCOM.Location = new System.Drawing.Point(486, 9);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Comm Port";
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(77, 42);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(104, 21);
            this.comboBoxComPort.TabIndex = 7;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(190, 42);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(60, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(260, 42);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(80, 23);
            this.btnDisconnect.TabIndex = 7;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
            // 
            // labelCompilationDate
            // 
            this.labelCompilationDate.AutoSize = true;
            this.labelCompilationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompilationDate.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelCompilationDate.Location = new System.Drawing.Point(14, 391);
            this.labelCompilationDate.Name = "labelCompilationDate";
            this.labelCompilationDate.Size = new System.Drawing.Size(148, 13);
            this.labelCompilationDate.TabIndex = 29;
            this.labelCompilationDate.Text = "Built: [Date will be set in code]";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfiguration);
            this.tabControl.Controls.Add(this.tabPageMotorConfig);
            this.tabControl.Controls.Add(this.tabPageCalibration);
            this.tabControl.Controls.Add(this.tabPageManualControl);
            this.tabControl.Controls.Add(this.tabPageLog);
            this.tabControl.Location = new System.Drawing.Point(13, 87);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(525, 257);
            this.tabControl.TabIndex = 30;
            // 
            // tabPageConfiguration
            // 
            this.tabPageConfiguration.Controls.Add(this.labelFilterCount);
            this.tabPageConfiguration.Controls.Add(this.comboBoxFilterCount);
            this.tabPageConfiguration.Controls.Add(this.labelFilterNames);
            this.tabPageConfiguration.Controls.Add(this.labelFilter1);
            this.tabPageConfiguration.Controls.Add(this.textBoxFilter1);
            this.tabPageConfiguration.Controls.Add(this.labelFilter2);
            this.tabPageConfiguration.Controls.Add(this.textBoxFilter2);
            this.tabPageConfiguration.Controls.Add(this.labelFilter3);
            this.tabPageConfiguration.Controls.Add(this.textBoxFilter3);
            this.tabPageConfiguration.Controls.Add(this.labelFilter4);
            this.tabPageConfiguration.Controls.Add(this.textBoxFilter4);
            this.tabPageConfiguration.Controls.Add(this.labelFilter5);
            this.tabPageConfiguration.Controls.Add(this.textBoxFilter5);
            this.tabPageConfiguration.Controls.Add(this.labelFilter6);
            this.tabPageConfiguration.Controls.Add(this.textBoxFilter6);
            this.tabPageConfiguration.Controls.Add(this.labelFilter7);
            this.tabPageConfiguration.Controls.Add(this.textBoxFilter7);
            this.tabPageConfiguration.Controls.Add(this.labelFilter8);
            this.tabPageConfiguration.Controls.Add(this.textBoxFilter8);
            this.tabPageConfiguration.Controls.Add(this.btnSetFilterConfig);
            this.tabPageConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfiguration.Name = "tabPageConfiguration";
            this.tabPageConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfiguration.Size = new System.Drawing.Size(517, 231);
            this.tabPageConfiguration.TabIndex = 0;
            this.tabPageConfiguration.Text = "Filter Configuration";
            this.tabPageConfiguration.UseVisualStyleBackColor = true;
            // 
            // labelFilterCount
            // 
            this.labelFilterCount.AutoSize = true;
            this.labelFilterCount.Location = new System.Drawing.Point(10, 10);
            this.labelFilterCount.Name = "labelFilterCount";
            this.labelFilterCount.Size = new System.Drawing.Size(60, 13);
            this.labelFilterCount.TabIndex = 8;
            this.labelFilterCount.Text = "Filter Count";
            // 
            // comboBoxFilterCount
            // 
            this.comboBoxFilterCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterCount.FormattingEnabled = true;
            this.comboBoxFilterCount.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.comboBoxFilterCount.Location = new System.Drawing.Point(80, 7);
            this.comboBoxFilterCount.Name = "comboBoxFilterCount";
            this.comboBoxFilterCount.Size = new System.Drawing.Size(60, 21);
            this.comboBoxFilterCount.TabIndex = 10;
            this.comboBoxFilterCount.SelectedIndexChanged += new System.EventHandler(this.ComboBoxFilterCount_SelectedIndexChanged);
            // 
            // labelFilterNames
            // 
            this.labelFilterNames.AutoSize = true;
            this.labelFilterNames.Location = new System.Drawing.Point(10, 40);
            this.labelFilterNames.Name = "labelFilterNames";
            this.labelFilterNames.Size = new System.Drawing.Size(65, 13);
            this.labelFilterNames.TabIndex = 11;
            this.labelFilterNames.Text = "Filter Names";
            // 
            // labelFilter1
            // 
            this.labelFilter1.AutoSize = true;
            this.labelFilter1.Location = new System.Drawing.Point(10, 70);
            this.labelFilter1.Name = "labelFilter1";
            this.labelFilter1.Size = new System.Drawing.Size(13, 13);
            this.labelFilter1.TabIndex = 11;
            this.labelFilter1.Text = "1";
            // 
            // textBoxFilter1
            // 
            this.textBoxFilter1.Location = new System.Drawing.Point(29, 67);
            this.textBoxFilter1.MaxLength = 15;
            this.textBoxFilter1.Name = "textBoxFilter1";
            this.textBoxFilter1.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter1.TabIndex = 12;
            // 
            // labelFilter2
            // 
            this.labelFilter2.AutoSize = true;
            this.labelFilter2.Location = new System.Drawing.Point(170, 70);
            this.labelFilter2.Name = "labelFilter2";
            this.labelFilter2.Size = new System.Drawing.Size(13, 13);
            this.labelFilter2.TabIndex = 13;
            this.labelFilter2.Text = "2";
            // 
            // textBoxFilter2
            // 
            this.textBoxFilter2.Location = new System.Drawing.Point(189, 67);
            this.textBoxFilter2.MaxLength = 15;
            this.textBoxFilter2.Name = "textBoxFilter2";
            this.textBoxFilter2.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter2.TabIndex = 14;
            // 
            // labelFilter3
            // 
            this.labelFilter3.AutoSize = true;
            this.labelFilter3.Location = new System.Drawing.Point(10, 96);
            this.labelFilter3.Name = "labelFilter3";
            this.labelFilter3.Size = new System.Drawing.Size(13, 13);
            this.labelFilter3.TabIndex = 15;
            this.labelFilter3.Text = "3";
            // 
            // textBoxFilter3
            // 
            this.textBoxFilter3.Location = new System.Drawing.Point(29, 93);
            this.textBoxFilter3.MaxLength = 15;
            this.textBoxFilter3.Name = "textBoxFilter3";
            this.textBoxFilter3.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter3.TabIndex = 16;
            // 
            // labelFilter4
            // 
            this.labelFilter4.AutoSize = true;
            this.labelFilter4.Location = new System.Drawing.Point(170, 96);
            this.labelFilter4.Name = "labelFilter4";
            this.labelFilter4.Size = new System.Drawing.Size(13, 13);
            this.labelFilter4.TabIndex = 17;
            this.labelFilter4.Text = "4";
            // 
            // textBoxFilter4
            // 
            this.textBoxFilter4.Location = new System.Drawing.Point(189, 93);
            this.textBoxFilter4.MaxLength = 15;
            this.textBoxFilter4.Name = "textBoxFilter4";
            this.textBoxFilter4.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter4.TabIndex = 18;
            // 
            // labelFilter5
            // 
            this.labelFilter5.AutoSize = true;
            this.labelFilter5.Location = new System.Drawing.Point(10, 122);
            this.labelFilter5.Name = "labelFilter5";
            this.labelFilter5.Size = new System.Drawing.Size(13, 13);
            this.labelFilter5.TabIndex = 19;
            this.labelFilter5.Text = "5";
            // 
            // textBoxFilter5
            // 
            this.textBoxFilter5.Location = new System.Drawing.Point(29, 119);
            this.textBoxFilter5.MaxLength = 15;
            this.textBoxFilter5.Name = "textBoxFilter5";
            this.textBoxFilter5.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter5.TabIndex = 20;
            // 
            // labelFilter6
            // 
            this.labelFilter6.AutoSize = true;
            this.labelFilter6.Location = new System.Drawing.Point(170, 122);
            this.labelFilter6.Name = "labelFilter6";
            this.labelFilter6.Size = new System.Drawing.Size(13, 13);
            this.labelFilter6.TabIndex = 21;
            this.labelFilter6.Text = "6";
            // 
            // textBoxFilter6
            // 
            this.textBoxFilter6.Location = new System.Drawing.Point(189, 119);
            this.textBoxFilter6.MaxLength = 15;
            this.textBoxFilter6.Name = "textBoxFilter6";
            this.textBoxFilter6.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter6.TabIndex = 22;
            // 
            // labelFilter7
            // 
            this.labelFilter7.AutoSize = true;
            this.labelFilter7.Location = new System.Drawing.Point(10, 148);
            this.labelFilter7.Name = "labelFilter7";
            this.labelFilter7.Size = new System.Drawing.Size(13, 13);
            this.labelFilter7.TabIndex = 23;
            this.labelFilter7.Text = "7";
            // 
            // textBoxFilter7
            // 
            this.textBoxFilter7.Location = new System.Drawing.Point(29, 145);
            this.textBoxFilter7.MaxLength = 15;
            this.textBoxFilter7.Name = "textBoxFilter7";
            this.textBoxFilter7.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter7.TabIndex = 24;
            // 
            // labelFilter8
            // 
            this.labelFilter8.AutoSize = true;
            this.labelFilter8.Location = new System.Drawing.Point(170, 148);
            this.labelFilter8.Name = "labelFilter8";
            this.labelFilter8.Size = new System.Drawing.Size(13, 13);
            this.labelFilter8.TabIndex = 25;
            this.labelFilter8.Text = "8";
            // 
            // textBoxFilter8
            // 
            this.textBoxFilter8.Location = new System.Drawing.Point(189, 145);
            this.textBoxFilter8.MaxLength = 15;
            this.textBoxFilter8.Name = "textBoxFilter8";
            this.textBoxFilter8.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter8.TabIndex = 26;
            // 
            // btnSetFilterConfig
            // 
            this.btnSetFilterConfig.Enabled = false;
            this.btnSetFilterConfig.Location = new System.Drawing.Point(340, 145);
            this.btnSetFilterConfig.Name = "btnSetFilterConfig";
            this.btnSetFilterConfig.Size = new System.Drawing.Size(120, 25);
            this.btnSetFilterConfig.TabIndex = 27;
            this.btnSetFilterConfig.Text = "Save Filter Config";
            this.btnSetFilterConfig.UseVisualStyleBackColor = true;
            this.btnSetFilterConfig.Click += new System.EventHandler(this.BtnSet_Click);
            // 
            // tabPageMotorConfig
            // 
            this.tabPageMotorConfig.Controls.Add(this.groupBoxDirection);
            this.tabPageMotorConfig.Controls.Add(this.groupBoxMotorSettings);
            this.tabPageMotorConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageMotorConfig.Name = "tabPageMotorConfig";
            this.tabPageMotorConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMotorConfig.Size = new System.Drawing.Size(517, 231);
            this.tabPageMotorConfig.TabIndex = 1;
            this.tabPageMotorConfig.Text = "Motor Configuration";
            this.tabPageMotorConfig.UseVisualStyleBackColor = true;
            // 
            // groupBoxDirection
            // 
            this.groupBoxDirection.Controls.Add(this.btnSetDirectionConfig);
            this.groupBoxDirection.Controls.Add(this.btnGetDirectionConfig);
            this.groupBoxDirection.Controls.Add(this.checkBoxReverse);
            this.groupBoxDirection.Controls.Add(this.radioBidirectional);
            this.groupBoxDirection.Controls.Add(this.radioUnidirectional);
            this.groupBoxDirection.Location = new System.Drawing.Point(10, 140);
            this.groupBoxDirection.Name = "groupBoxDirection";
            this.groupBoxDirection.Size = new System.Drawing.Size(495, 46);
            this.groupBoxDirection.TabIndex = 1;
            this.groupBoxDirection.TabStop = false;
            this.groupBoxDirection.Text = "Direction Configuration";
            // 
            // btnSetDirectionConfig
            // 
            this.btnSetDirectionConfig.Enabled = false;
            this.btnSetDirectionConfig.Location = new System.Drawing.Point(400, 15);
            this.btnSetDirectionConfig.Name = "btnSetDirectionConfig";
            this.btnSetDirectionConfig.Size = new System.Drawing.Size(40, 23);
            this.btnSetDirectionConfig.TabIndex = 4;
            this.btnSetDirectionConfig.Text = "Set";
            this.btnSetDirectionConfig.UseVisualStyleBackColor = true;
            this.btnSetDirectionConfig.Click += new System.EventHandler(this.BtnSetDirectionConfig_Click);
            // 
            // btnGetDirectionConfig
            // 
            this.btnGetDirectionConfig.Enabled = false;
            this.btnGetDirectionConfig.Location = new System.Drawing.Point(350, 15);
            this.btnGetDirectionConfig.Name = "btnGetDirectionConfig";
            this.btnGetDirectionConfig.Size = new System.Drawing.Size(40, 23);
            this.btnGetDirectionConfig.TabIndex = 3;
            this.btnGetDirectionConfig.Text = "Get";
            this.btnGetDirectionConfig.UseVisualStyleBackColor = true;
            this.btnGetDirectionConfig.Click += new System.EventHandler(this.BtnGetDirectionConfig_Click);
            // 
            // checkBoxReverse
            // 
            this.checkBoxReverse.AutoSize = true;
            this.checkBoxReverse.Location = new System.Drawing.Point(210, 16);
            this.checkBoxReverse.Name = "checkBoxReverse";
            this.checkBoxReverse.Size = new System.Drawing.Size(66, 17);
            this.checkBoxReverse.TabIndex = 2;
            this.checkBoxReverse.Text = "Reverse";
            this.checkBoxReverse.UseVisualStyleBackColor = true;
            // 
            // radioBidirectional
            // 
            this.radioBidirectional.AutoSize = true;
            this.radioBidirectional.Location = new System.Drawing.Point(110, 15);
            this.radioBidirectional.Name = "radioBidirectional";
            this.radioBidirectional.Size = new System.Drawing.Size(82, 17);
            this.radioBidirectional.TabIndex = 1;
            this.radioBidirectional.Text = "Bidirectional";
            this.radioBidirectional.UseVisualStyleBackColor = true;
            // 
            // radioUnidirectional
            // 
            this.radioUnidirectional.AutoSize = true;
            this.radioUnidirectional.Checked = true;
            this.radioUnidirectional.Location = new System.Drawing.Point(10, 15);
            this.radioUnidirectional.Name = "radioUnidirectional";
            this.radioUnidirectional.Size = new System.Drawing.Size(89, 17);
            this.radioUnidirectional.TabIndex = 0;
            this.radioUnidirectional.TabStop = true;
            this.radioUnidirectional.Text = "Unidirectional";
            this.radioUnidirectional.UseVisualStyleBackColor = true;
            // 
            // groupBoxMotorSettings
            // 
            this.groupBoxMotorSettings.Controls.Add(this.btnSetStepsPerRev);
            this.groupBoxMotorSettings.Controls.Add(this.btnGetStepsPerRev);
            this.groupBoxMotorSettings.Controls.Add(this.btnResetMotorConfig);
            this.groupBoxMotorSettings.Controls.Add(this.btnSetMotorConfig);
            this.groupBoxMotorSettings.Controls.Add(this.btnGetMotorConfig);
            this.groupBoxMotorSettings.Controls.Add(this.numericDisableDelay);
            this.groupBoxMotorSettings.Controls.Add(this.labelDisableDelay);
            this.groupBoxMotorSettings.Controls.Add(this.numericAcceleration);
            this.groupBoxMotorSettings.Controls.Add(this.labelAcceleration);
            this.groupBoxMotorSettings.Controls.Add(this.numericMaxSpeed);
            this.groupBoxMotorSettings.Controls.Add(this.labelMaxSpeed);
            this.groupBoxMotorSettings.Controls.Add(this.numericMotorSpeed);
            this.groupBoxMotorSettings.Controls.Add(this.labelMotorSpeed);
            this.groupBoxMotorSettings.Controls.Add(this.numericStepsPerRev);
            this.groupBoxMotorSettings.Controls.Add(this.labelStepsPerRev);
            this.groupBoxMotorSettings.Location = new System.Drawing.Point(10, 10);
            this.groupBoxMotorSettings.Name = "groupBoxMotorSettings";
            this.groupBoxMotorSettings.Size = new System.Drawing.Size(495, 120);
            this.groupBoxMotorSettings.TabIndex = 0;
            this.groupBoxMotorSettings.TabStop = false;
            this.groupBoxMotorSettings.Text = "Motor Settings";
            // 
            // btnResetMotorConfig
            // 
            this.btnResetMotorConfig.Enabled = false;
            this.btnResetMotorConfig.Location = new System.Drawing.Point(180, 85);
            this.btnResetMotorConfig.Name = "btnResetMotorConfig";
            this.btnResetMotorConfig.Size = new System.Drawing.Size(75, 25);
            this.btnResetMotorConfig.TabIndex = 10;
            this.btnResetMotorConfig.Text = "Reset";
            this.btnResetMotorConfig.UseVisualStyleBackColor = true;
            this.btnResetMotorConfig.Click += new System.EventHandler(this.BtnResetMotorConfig_Click);
            // 
            // btnSetMotorConfig
            // 
            this.btnSetMotorConfig.Enabled = false;
            this.btnSetMotorConfig.Location = new System.Drawing.Point(95, 85);
            this.btnSetMotorConfig.Name = "btnSetMotorConfig";
            this.btnSetMotorConfig.Size = new System.Drawing.Size(75, 25);
            this.btnSetMotorConfig.TabIndex = 9;
            this.btnSetMotorConfig.Text = "Apply";
            this.btnSetMotorConfig.UseVisualStyleBackColor = true;
            this.btnSetMotorConfig.Click += new System.EventHandler(this.BtnSetMotorConfig_Click);
            // 
            // btnGetMotorConfig
            // 
            this.btnGetMotorConfig.Enabled = false;
            this.btnGetMotorConfig.Location = new System.Drawing.Point(10, 85);
            this.btnGetMotorConfig.Name = "btnGetMotorConfig";
            this.btnGetMotorConfig.Size = new System.Drawing.Size(75, 25);
            this.btnGetMotorConfig.TabIndex = 8;
            this.btnGetMotorConfig.Text = "Get Config";
            this.btnGetMotorConfig.UseVisualStyleBackColor = true;
            this.btnGetMotorConfig.Click += new System.EventHandler(this.BtnGetMotorConfig_Click);
            // 
            // numericDisableDelay
            // 
            this.numericDisableDelay.Location = new System.Drawing.Point(270, 53);
            this.numericDisableDelay.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericDisableDelay.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericDisableDelay.Name = "numericDisableDelay";
            this.numericDisableDelay.Size = new System.Drawing.Size(80, 20);
            this.numericDisableDelay.TabIndex = 7;
            this.numericDisableDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // labelDisableDelay
            // 
            this.labelDisableDelay.AutoSize = true;
            this.labelDisableDelay.Location = new System.Drawing.Point(180, 55);
            this.labelDisableDelay.Name = "labelDisableDelay";
            this.labelDisableDelay.Size = new System.Drawing.Size(97, 13);
            this.labelDisableDelay.TabIndex = 6;
            this.labelDisableDelay.Text = "Disable Delay (ms):";
            // 
            // numericAcceleration
            // 
            this.numericAcceleration.Location = new System.Drawing.Point(80, 53);
            this.numericAcceleration.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericAcceleration.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericAcceleration.Name = "numericAcceleration";
            this.numericAcceleration.Size = new System.Drawing.Size(80, 20);
            this.numericAcceleration.TabIndex = 5;
            this.numericAcceleration.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // labelAcceleration
            // 
            this.labelAcceleration.AutoSize = true;
            this.labelAcceleration.Location = new System.Drawing.Point(10, 55);
            this.labelAcceleration.Name = "labelAcceleration";
            this.labelAcceleration.Size = new System.Drawing.Size(64, 13);
            this.labelAcceleration.TabIndex = 4;
            this.labelAcceleration.Text = "Accel (s/s²):";
            // 
            // numericMaxSpeed
            // 
            this.numericMaxSpeed.Location = new System.Drawing.Point(270, 23);
            this.numericMaxSpeed.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericMaxSpeed.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericMaxSpeed.Name = "numericMaxSpeed";
            this.numericMaxSpeed.Size = new System.Drawing.Size(80, 20);
            this.numericMaxSpeed.TabIndex = 3;
            this.numericMaxSpeed.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // labelMaxSpeed
            // 
            this.labelMaxSpeed.AutoSize = true;
            this.labelMaxSpeed.Location = new System.Drawing.Point(180, 25);
            this.labelMaxSpeed.Name = "labelMaxSpeed";
            this.labelMaxSpeed.Size = new System.Drawing.Size(88, 13);
            this.labelMaxSpeed.TabIndex = 2;
            this.labelMaxSpeed.Text = "Max Speed (s/s):";
            // 
            // numericMotorSpeed
            // 
            this.numericMotorSpeed.Location = new System.Drawing.Point(80, 23);
            this.numericMotorSpeed.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numericMotorSpeed.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericMotorSpeed.Name = "numericMotorSpeed";
            this.numericMotorSpeed.Size = new System.Drawing.Size(80, 20);
            this.numericMotorSpeed.TabIndex = 1;
            this.numericMotorSpeed.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // labelMotorSpeed
            // 
            this.labelMotorSpeed.AutoSize = true;
            this.labelMotorSpeed.Location = new System.Drawing.Point(10, 25);
            this.labelMotorSpeed.Name = "labelMotorSpeed";
            this.labelMotorSpeed.Size = new System.Drawing.Size(65, 13);
            this.labelMotorSpeed.TabIndex = 0;
            this.labelMotorSpeed.Text = "Speed (s/s):";
            //
            // numericStepsPerRev
            //
            this.numericStepsPerRev.Location = new System.Drawing.Point(330, 23);
            this.numericStepsPerRev.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.numericStepsPerRev.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericStepsPerRev.Name = "numericStepsPerRev";
            this.numericStepsPerRev.Size = new System.Drawing.Size(80, 20);
            this.numericStepsPerRev.TabIndex = 9;
            this.numericStepsPerRev.Value = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            //
            // labelStepsPerRev
            //
            this.labelStepsPerRev.AutoSize = true;
            this.labelStepsPerRev.Location = new System.Drawing.Point(250, 25);
            this.labelStepsPerRev.Name = "labelStepsPerRev";
            this.labelStepsPerRev.Size = new System.Drawing.Size(74, 13);
            this.labelStepsPerRev.TabIndex = 8;
            this.labelStepsPerRev.Text = "Steps/Rev:";
            //
            // btnGetStepsPerRev
            //
            this.btnGetStepsPerRev.Enabled = false;
            this.btnGetStepsPerRev.Location = new System.Drawing.Point(415, 20);
            this.btnGetStepsPerRev.Name = "btnGetStepsPerRev";
            this.btnGetStepsPerRev.Size = new System.Drawing.Size(35, 23);
            this.btnGetStepsPerRev.TabIndex = 11;
            this.btnGetStepsPerRev.Text = "Get";
            this.btnGetStepsPerRev.UseVisualStyleBackColor = true;
            this.btnGetStepsPerRev.Click += new System.EventHandler(this.BtnGetStepsPerRev_Click);
            //
            // btnSetStepsPerRev
            //
            this.btnSetStepsPerRev.Enabled = false;
            this.btnSetStepsPerRev.Location = new System.Drawing.Point(451, 20);
            this.btnSetStepsPerRev.Name = "btnSetStepsPerRev";
            this.btnSetStepsPerRev.Size = new System.Drawing.Size(35, 23);
            this.btnSetStepsPerRev.TabIndex = 12;
            this.btnSetStepsPerRev.Text = "Set";
            this.btnSetStepsPerRev.UseVisualStyleBackColor = true;
            this.btnSetStepsPerRev.Click += new System.EventHandler(this.BtnSetStepsPerRev_Click);
            //
            // tabPageCalibration
            // 
            this.tabPageCalibration.Controls.Add(this.groupBoxBacklashCalibration);
            this.tabPageCalibration.Controls.Add(this.groupBoxCalibration);
            this.tabPageCalibration.Location = new System.Drawing.Point(4, 22);
            this.tabPageCalibration.Name = "tabPageCalibration";
            this.tabPageCalibration.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCalibration.Size = new System.Drawing.Size(517, 231);
            this.tabPageCalibration.TabIndex = 1;
            this.tabPageCalibration.Text = "Calibration";
            this.tabPageCalibration.UseVisualStyleBackColor = true;
            // 
            // groupBoxBacklashCalibration
            // 
            this.groupBoxBacklashCalibration.Controls.Add(this.labelBacklashStatus);
            this.groupBoxBacklashCalibration.Controls.Add(this.btnFinishBacklash);
            this.groupBoxBacklashCalibration.Controls.Add(this.btnBacklashMark);
            this.groupBoxBacklashCalibration.Controls.Add(this.btnBacklashStep);
            this.groupBoxBacklashCalibration.Controls.Add(this.btnStartBacklash);
            this.groupBoxBacklashCalibration.Controls.Add(this.comboBoxBacklashSteps);
            this.groupBoxBacklashCalibration.Controls.Add(this.labelBacklashSteps);
            this.groupBoxBacklashCalibration.Location = new System.Drawing.Point(10, 100);
            this.groupBoxBacklashCalibration.Name = "groupBoxBacklashCalibration";
            this.groupBoxBacklashCalibration.Size = new System.Drawing.Size(387, 80);
            this.groupBoxBacklashCalibration.TabIndex = 31;
            this.groupBoxBacklashCalibration.TabStop = false;
            this.groupBoxBacklashCalibration.Text = "Backlash Calibration";
            // 
            // labelBacklashStatus
            // 
            this.labelBacklashStatus.AutoSize = true;
            this.labelBacklashStatus.Location = new System.Drawing.Point(10, 55);
            this.labelBacklashStatus.Name = "labelBacklashStatus";
            this.labelBacklashStatus.Size = new System.Drawing.Size(208, 13);
            this.labelBacklashStatus.TabIndex = 36;
            this.labelBacklashStatus.Text = "Status: Ready - Requires AS5600 encoder";
            // 
            // btnFinishBacklash
            // 
            this.btnFinishBacklash.Enabled = false;
            this.btnFinishBacklash.Location = new System.Drawing.Point(302, 22);
            this.btnFinishBacklash.Name = "btnFinishBacklash";
            this.btnFinishBacklash.Size = new System.Drawing.Size(60, 25);
            this.btnFinishBacklash.TabIndex = 38;
            this.btnFinishBacklash.Text = "Finish";
            this.btnFinishBacklash.UseVisualStyleBackColor = true;
            this.btnFinishBacklash.Click += new System.EventHandler(this.BtnFinishBacklash_Click);
            // 
            // btnBacklashMark
            // 
            this.btnBacklashMark.Enabled = false;
            this.btnBacklashMark.Location = new System.Drawing.Point(140, 20);
            this.btnBacklashMark.Name = "btnBacklashMark";
            this.btnBacklashMark.Size = new System.Drawing.Size(50, 25);
            this.btnBacklashMark.TabIndex = 37;
            this.btnBacklashMark.Text = "Mark";
            this.btnBacklashMark.UseVisualStyleBackColor = true;
            this.btnBacklashMark.Click += new System.EventHandler(this.BtnBacklashMark_Click);
            // 
            // btnBacklashStep
            // 
            this.btnBacklashStep.Enabled = false;
            this.btnBacklashStep.Location = new System.Drawing.Point(80, 20);
            this.btnBacklashStep.Name = "btnBacklashStep";
            this.btnBacklashStep.Size = new System.Drawing.Size(50, 25);
            this.btnBacklashStep.TabIndex = 36;
            this.btnBacklashStep.Text = "Step";
            this.btnBacklashStep.UseVisualStyleBackColor = true;
            this.btnBacklashStep.Click += new System.EventHandler(this.BtnBacklashStep_Click);
            // 
            // btnStartBacklash
            // 
            this.btnStartBacklash.Enabled = false;
            this.btnStartBacklash.Location = new System.Drawing.Point(10, 20);
            this.btnStartBacklash.Name = "btnStartBacklash";
            this.btnStartBacklash.Size = new System.Drawing.Size(60, 25);
            this.btnStartBacklash.TabIndex = 35;
            this.btnStartBacklash.Text = "Start";
            this.btnStartBacklash.UseVisualStyleBackColor = true;
            this.btnStartBacklash.Click += new System.EventHandler(this.BtnStartBacklash_Click);
            // 
            // comboBoxBacklashSteps
            // 
            this.comboBoxBacklashSteps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBacklashSteps.FormattingEnabled = true;
            this.comboBoxBacklashSteps.Items.AddRange(new object[] {
            "1",
            "3",
            "5",
            "10",
            "20",
            "50",
            "100"});
            this.comboBoxBacklashSteps.Location = new System.Drawing.Point(243, 22);
            this.comboBoxBacklashSteps.Name = "comboBoxBacklashSteps";
            this.comboBoxBacklashSteps.Size = new System.Drawing.Size(50, 21);
            this.comboBoxBacklashSteps.TabIndex = 39;
            // 
            // labelBacklashSteps
            // 
            this.labelBacklashSteps.AutoSize = true;
            this.labelBacklashSteps.Location = new System.Drawing.Point(200, 25);
            this.labelBacklashSteps.Name = "labelBacklashSteps";
            this.labelBacklashSteps.Size = new System.Drawing.Size(37, 13);
            this.labelBacklashSteps.TabIndex = 38;
            this.labelBacklashSteps.Text = "Steps:";
            // 
            // groupBoxCalibration
            // 
            this.groupBoxCalibration.Controls.Add(this.labelRevCalStatus);
            this.groupBoxCalibration.Controls.Add(this.btnFinishCalibration);
            this.groupBoxCalibration.Controls.Add(this.btnCalibrationBackward);
            this.groupBoxCalibration.Controls.Add(this.btnCalibrationForward);
            this.groupBoxCalibration.Controls.Add(this.btnStartCalibration);
            this.groupBoxCalibration.Controls.Add(this.comboBoxSteps);
            this.groupBoxCalibration.Controls.Add(this.labelSteps);
            this.groupBoxCalibration.Location = new System.Drawing.Point(10, 10);
            this.groupBoxCalibration.Name = "groupBoxCalibration";
            this.groupBoxCalibration.Size = new System.Drawing.Size(387, 80);
            this.groupBoxCalibration.TabIndex = 30;
            this.groupBoxCalibration.TabStop = false;
            this.groupBoxCalibration.Text = "Revolution Calibration";
            // 
            // labelRevCalStatus
            // 
            this.labelRevCalStatus.AutoSize = true;
            this.labelRevCalStatus.Location = new System.Drawing.Point(10, 55);
            this.labelRevCalStatus.Name = "labelRevCalStatus";
            this.labelRevCalStatus.Size = new System.Drawing.Size(223, 13);
            this.labelRevCalStatus.TabIndex = 35;
            this.labelRevCalStatus.Text = "Status: Ready - Click Start to begin calibration";
            // 
            // btnFinishCalibration
            // 
            this.btnFinishCalibration.Enabled = false;
            this.btnFinishCalibration.Location = new System.Drawing.Point(302, 22);
            this.btnFinishCalibration.Name = "btnFinishCalibration";
            this.btnFinishCalibration.Size = new System.Drawing.Size(60, 25);
            this.btnFinishCalibration.TabIndex = 34;
            this.btnFinishCalibration.Text = "Finish";
            this.btnFinishCalibration.UseVisualStyleBackColor = true;
            this.btnFinishCalibration.Click += new System.EventHandler(this.BtnFinishCalibration_Click);
            // 
            // btnCalibrationBackward
            // 
            this.btnCalibrationBackward.Enabled = false;
            this.btnCalibrationBackward.Location = new System.Drawing.Point(140, 20);
            this.btnCalibrationBackward.Name = "btnCalibrationBackward";
            this.btnCalibrationBackward.Size = new System.Drawing.Size(50, 25);
            this.btnCalibrationBackward.TabIndex = 33;
            this.btnCalibrationBackward.Text = "Back";
            this.btnCalibrationBackward.UseVisualStyleBackColor = true;
            this.btnCalibrationBackward.Click += new System.EventHandler(this.BtnCalibrationBackward_Click);
            // 
            // btnCalibrationForward
            // 
            this.btnCalibrationForward.Enabled = false;
            this.btnCalibrationForward.Location = new System.Drawing.Point(80, 20);
            this.btnCalibrationForward.Name = "btnCalibrationForward";
            this.btnCalibrationForward.Size = new System.Drawing.Size(50, 25);
            this.btnCalibrationForward.TabIndex = 32;
            this.btnCalibrationForward.Text = "Fwd";
            this.btnCalibrationForward.UseVisualStyleBackColor = true;
            this.btnCalibrationForward.Click += new System.EventHandler(this.BtnCalibrationForward_Click);
            // 
            // btnStartCalibration
            // 
            this.btnStartCalibration.Enabled = false;
            this.btnStartCalibration.Location = new System.Drawing.Point(10, 20);
            this.btnStartCalibration.Name = "btnStartCalibration";
            this.btnStartCalibration.Size = new System.Drawing.Size(60, 25);
            this.btnStartCalibration.TabIndex = 31;
            this.btnStartCalibration.Text = "Start";
            this.btnStartCalibration.UseVisualStyleBackColor = true;
            this.btnStartCalibration.Click += new System.EventHandler(this.BtnStartCalibration_Click);
            // 
            // comboBoxSteps
            // 
            this.comboBoxSteps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSteps.FormattingEnabled = true;
            this.comboBoxSteps.Items.AddRange(new object[] {
            "1",
            "3",
            "5",
            "10",
            "20",
            "50",
            "100"});
            this.comboBoxSteps.Location = new System.Drawing.Point(243, 22);
            this.comboBoxSteps.Name = "comboBoxSteps";
            this.comboBoxSteps.Size = new System.Drawing.Size(50, 21);
            this.comboBoxSteps.TabIndex = 36;
            // 
            // labelSteps
            // 
            this.labelSteps.AutoSize = true;
            this.labelSteps.Location = new System.Drawing.Point(200, 25);
            this.labelSteps.Name = "labelSteps";
            this.labelSteps.Size = new System.Drawing.Size(37, 13);
            this.labelSteps.TabIndex = 35;
            this.labelSteps.Text = "Steps:";
            // 
            // tabPageManualControl
            // 
            this.tabPageManualControl.Controls.Add(this.groupBoxSystemInfo);
            this.tabPageManualControl.Controls.Add(this.groupBoxMotorPower);
            this.tabPageManualControl.Controls.Add(this.groupBoxStepping);
            this.tabPageManualControl.Location = new System.Drawing.Point(4, 22);
            this.tabPageManualControl.Name = "tabPageManualControl";
            this.tabPageManualControl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageManualControl.Size = new System.Drawing.Size(517, 231);
            this.tabPageManualControl.TabIndex = 3;
            this.tabPageManualControl.Text = "Manual Control";
            this.tabPageManualControl.UseVisualStyleBackColor = true;
            // 
            // groupBoxSystemInfo
            // 
            this.groupBoxSystemInfo.Controls.Add(this.textBoxSystemInfo);
            this.groupBoxSystemInfo.Controls.Add(this.btnGetDeviceId);
            this.groupBoxSystemInfo.Controls.Add(this.btnGetVersion);
            this.groupBoxSystemInfo.Controls.Add(this.btnGetStatus);
            this.groupBoxSystemInfo.Location = new System.Drawing.Point(10, 120);
            this.groupBoxSystemInfo.Name = "groupBoxSystemInfo";
            this.groupBoxSystemInfo.Size = new System.Drawing.Size(420, 45);
            this.groupBoxSystemInfo.TabIndex = 2;
            this.groupBoxSystemInfo.TabStop = false;
            this.groupBoxSystemInfo.Text = "System Information";
            // 
            // textBoxSystemInfo
            // 
            this.textBoxSystemInfo.Location = new System.Drawing.Point(190, 17);
            this.textBoxSystemInfo.Name = "textBoxSystemInfo";
            this.textBoxSystemInfo.ReadOnly = true;
            this.textBoxSystemInfo.Size = new System.Drawing.Size(220, 20);
            this.textBoxSystemInfo.TabIndex = 3;
            this.textBoxSystemInfo.Text = "Click buttons to get system info";
            // 
            // btnGetDeviceId
            // 
            this.btnGetDeviceId.Enabled = false;
            this.btnGetDeviceId.Location = new System.Drawing.Point(130, 15);
            this.btnGetDeviceId.Name = "btnGetDeviceId";
            this.btnGetDeviceId.Size = new System.Drawing.Size(50, 25);
            this.btnGetDeviceId.TabIndex = 2;
            this.btnGetDeviceId.Text = "Device";
            this.btnGetDeviceId.UseVisualStyleBackColor = true;
            this.btnGetDeviceId.Click += new System.EventHandler(this.BtnGetDeviceId_Click);
            // 
            // btnGetVersion
            // 
            this.btnGetVersion.Enabled = false;
            this.btnGetVersion.Location = new System.Drawing.Point(70, 15);
            this.btnGetVersion.Name = "btnGetVersion";
            this.btnGetVersion.Size = new System.Drawing.Size(50, 25);
            this.btnGetVersion.TabIndex = 1;
            this.btnGetVersion.Text = "Version";
            this.btnGetVersion.UseVisualStyleBackColor = true;
            this.btnGetVersion.Click += new System.EventHandler(this.BtnGetVersion_Click);
            // 
            // btnGetStatus
            // 
            this.btnGetStatus.Enabled = false;
            this.btnGetStatus.Location = new System.Drawing.Point(10, 15);
            this.btnGetStatus.Name = "btnGetStatus";
            this.btnGetStatus.Size = new System.Drawing.Size(50, 25);
            this.btnGetStatus.TabIndex = 0;
            this.btnGetStatus.Text = "Status";
            this.btnGetStatus.UseVisualStyleBackColor = true;
            this.btnGetStatus.Click += new System.EventHandler(this.BtnGetStatus_Click);
            // 
            // groupBoxMotorPower
            // 
            this.groupBoxMotorPower.Controls.Add(this.textBoxCurrentStep);
            this.groupBoxMotorPower.Controls.Add(this.labelCurrentStep);
            this.groupBoxMotorPower.Controls.Add(this.btnDisableMotor);
            this.groupBoxMotorPower.Controls.Add(this.btnEnableMotor);
            this.groupBoxMotorPower.Location = new System.Drawing.Point(220, 10);
            this.groupBoxMotorPower.Name = "groupBoxMotorPower";
            this.groupBoxMotorPower.Size = new System.Drawing.Size(210, 100);
            this.groupBoxMotorPower.TabIndex = 1;
            this.groupBoxMotorPower.TabStop = false;
            this.groupBoxMotorPower.Text = "Motor Power Control";
            // 
            // textBoxCurrentStep
            // 
            this.textBoxCurrentStep.Location = new System.Drawing.Point(85, 57);
            this.textBoxCurrentStep.Name = "textBoxCurrentStep";
            this.textBoxCurrentStep.ReadOnly = true;
            this.textBoxCurrentStep.Size = new System.Drawing.Size(80, 20);
            this.textBoxCurrentStep.TabIndex = 3;
            this.textBoxCurrentStep.Text = "0";
            // 
            // labelCurrentStep
            // 
            this.labelCurrentStep.AutoSize = true;
            this.labelCurrentStep.Location = new System.Drawing.Point(10, 60);
            this.labelCurrentStep.Name = "labelCurrentStep";
            this.labelCurrentStep.Size = new System.Drawing.Size(69, 13);
            this.labelCurrentStep.TabIndex = 2;
            this.labelCurrentStep.Text = "Current Step:";
            // 
            // btnDisableMotor
            // 
            this.btnDisableMotor.Enabled = false;
            this.btnDisableMotor.Location = new System.Drawing.Point(80, 25);
            this.btnDisableMotor.Name = "btnDisableMotor";
            this.btnDisableMotor.Size = new System.Drawing.Size(60, 25);
            this.btnDisableMotor.TabIndex = 1;
            this.btnDisableMotor.Text = "Disable";
            this.btnDisableMotor.UseVisualStyleBackColor = true;
            this.btnDisableMotor.Click += new System.EventHandler(this.BtnDisableMotor_Click);
            // 
            // btnEnableMotor
            // 
            this.btnEnableMotor.Enabled = false;
            this.btnEnableMotor.Location = new System.Drawing.Point(10, 25);
            this.btnEnableMotor.Name = "btnEnableMotor";
            this.btnEnableMotor.Size = new System.Drawing.Size(60, 25);
            this.btnEnableMotor.TabIndex = 0;
            this.btnEnableMotor.Text = "Enable";
            this.btnEnableMotor.UseVisualStyleBackColor = true;
            this.btnEnableMotor.Click += new System.EventHandler(this.BtnEnableMotor_Click);
            // 
            // groupBoxStepping
            // 
            this.groupBoxStepping.Controls.Add(this.btnGetStepPosition);
            this.groupBoxStepping.Controls.Add(this.btnStepTo);
            this.groupBoxStepping.Controls.Add(this.numericAbsoluteStep);
            this.groupBoxStepping.Controls.Add(this.labelAbsoluteStep);
            this.groupBoxStepping.Controls.Add(this.btnStepBackward);
            this.groupBoxStepping.Controls.Add(this.btnStepForward);
            this.groupBoxStepping.Controls.Add(this.numericStepAmount);
            this.groupBoxStepping.Controls.Add(this.labelStepAmount);
            this.groupBoxStepping.Location = new System.Drawing.Point(10, 10);
            this.groupBoxStepping.Name = "groupBoxStepping";
            this.groupBoxStepping.Size = new System.Drawing.Size(200, 100);
            this.groupBoxStepping.TabIndex = 0;
            this.groupBoxStepping.TabStop = false;
            this.groupBoxStepping.Text = "Manual Stepping";
            // 
            // btnGetStepPosition
            // 
            this.btnGetStepPosition.Enabled = false;
            this.btnGetStepPosition.Location = new System.Drawing.Point(80, 75);
            this.btnGetStepPosition.Name = "btnGetStepPosition";
            this.btnGetStepPosition.Size = new System.Drawing.Size(80, 20);
            this.btnGetStepPosition.TabIndex = 7;
            this.btnGetStepPosition.Text = "Get Position";
            this.btnGetStepPosition.UseVisualStyleBackColor = true;
            this.btnGetStepPosition.Click += new System.EventHandler(this.BtnGetStepPosition_Click);
            // 
            // btnStepTo
            // 
            this.btnStepTo.Enabled = false;
            this.btnStepTo.Location = new System.Drawing.Point(10, 75);
            this.btnStepTo.Name = "btnStepTo";
            this.btnStepTo.Size = new System.Drawing.Size(60, 20);
            this.btnStepTo.TabIndex = 6;
            this.btnStepTo.Text = "Go";
            this.btnStepTo.UseVisualStyleBackColor = true;
            this.btnStepTo.Click += new System.EventHandler(this.BtnStepTo_Click);
            // 
            // numericAbsoluteStep
            // 
            this.numericAbsoluteStep.Location = new System.Drawing.Point(80, 53);
            this.numericAbsoluteStep.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericAbsoluteStep.Minimum = new decimal(new int[] {
            -10000,
            0,
            0,
            -2147483648});
            this.numericAbsoluteStep.Name = "numericAbsoluteStep";
            this.numericAbsoluteStep.Size = new System.Drawing.Size(80, 20);
            this.numericAbsoluteStep.TabIndex = 5;
            // 
            // labelAbsoluteStep
            // 
            this.labelAbsoluteStep.AutoSize = true;
            this.labelAbsoluteStep.Location = new System.Drawing.Point(10, 55);
            this.labelAbsoluteStep.Name = "labelAbsoluteStep";
            this.labelAbsoluteStep.Size = new System.Drawing.Size(59, 13);
            this.labelAbsoluteStep.TabIndex = 4;
            this.labelAbsoluteStep.Text = "Go to step:";
            // 
            // btnStepBackward
            // 
            this.btnStepBackward.Enabled = false;
            this.btnStepBackward.Location = new System.Drawing.Point(160, 21);
            this.btnStepBackward.Name = "btnStepBackward";
            this.btnStepBackward.Size = new System.Drawing.Size(30, 25);
            this.btnStepBackward.TabIndex = 3;
            this.btnStepBackward.Text = "←";
            this.btnStepBackward.UseVisualStyleBackColor = true;
            this.btnStepBackward.Click += new System.EventHandler(this.BtnStepBackward_Click);
            // 
            // btnStepForward
            // 
            this.btnStepForward.Enabled = false;
            this.btnStepForward.Location = new System.Drawing.Point(125, 21);
            this.btnStepForward.Name = "btnStepForward";
            this.btnStepForward.Size = new System.Drawing.Size(30, 25);
            this.btnStepForward.TabIndex = 2;
            this.btnStepForward.Text = "→";
            this.btnStepForward.UseVisualStyleBackColor = true;
            this.btnStepForward.Click += new System.EventHandler(this.BtnStepForward_Click);
            // 
            // numericStepAmount
            // 
            this.numericStepAmount.Location = new System.Drawing.Point(55, 23);
            this.numericStepAmount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericStepAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericStepAmount.Name = "numericStepAmount";
            this.numericStepAmount.Size = new System.Drawing.Size(60, 20);
            this.numericStepAmount.TabIndex = 1;
            this.numericStepAmount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelStepAmount
            // 
            this.labelStepAmount.AutoSize = true;
            this.labelStepAmount.Location = new System.Drawing.Point(10, 25);
            this.labelStepAmount.Name = "labelStepAmount";
            this.labelStepAmount.Size = new System.Drawing.Size(37, 13);
            this.labelStepAmount.TabIndex = 0;
            this.labelStepAmount.Text = "Steps:";
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.textBoxLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLog.Size = new System.Drawing.Size(517, 231);
            this.tabPageLog.TabIndex = 2;
            this.tabPageLog.Text = "Communication Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = System.Drawing.Color.Black;
            this.textBoxLog.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLog.ForeColor = System.Drawing.Color.LimeGreen;
            this.textBoxLog.Location = new System.Drawing.Point(0, -1);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(517, 236);
            this.textBoxLog.TabIndex = 0;
            this.textBoxLog.WordWrap = false;
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 420);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.labelCompilationDate);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Awesome Filter Wheel  Setup";
            this.Load += new System.EventHandler(this.SetupDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageConfiguration.ResumeLayout(false);
            this.tabPageConfiguration.PerformLayout();
            this.tabPageMotorConfig.ResumeLayout(false);
            this.groupBoxDirection.ResumeLayout(false);
            this.groupBoxDirection.PerformLayout();
            this.groupBoxMotorSettings.ResumeLayout(false);
            this.groupBoxMotorSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDisableDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAcceleration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMotorSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericStepsPerRev)).EndInit();
            this.tabPageCalibration.ResumeLayout(false);
            this.groupBoxBacklashCalibration.ResumeLayout(false);
            this.groupBoxBacklashCalibration.PerformLayout();
            this.groupBoxCalibration.ResumeLayout(false);
            this.groupBoxCalibration.PerformLayout();
            this.tabPageManualControl.ResumeLayout(false);
            this.groupBoxSystemInfo.ResumeLayout(false);
            this.groupBoxSystemInfo.PerformLayout();
            this.groupBoxMotorPower.ResumeLayout(false);
            this.groupBoxMotorPower.PerformLayout();
            this.groupBoxStepping.ResumeLayout(false);
            this.groupBoxStepping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericAbsoluteStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericStepAmount)).EndInit();
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Label labelCompilationDate;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfiguration;
        private System.Windows.Forms.TabPage tabPageMotorConfig;
        private System.Windows.Forms.TabPage tabPageCalibration;
        private System.Windows.Forms.TabPage tabPageManualControl;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.GroupBox groupBoxCalibration;
        private System.Windows.Forms.Button btnStartCalibration;
        private System.Windows.Forms.Button btnCalibrationForward;
        private System.Windows.Forms.Button btnCalibrationBackward;
        private System.Windows.Forms.Button btnFinishCalibration;
        private System.Windows.Forms.ComboBox comboBoxSteps;
        private System.Windows.Forms.Label labelSteps;
        private System.Windows.Forms.GroupBox groupBoxBacklashCalibration;
        private System.Windows.Forms.Button btnStartBacklash;
        private System.Windows.Forms.Button btnBacklashStep;
        private System.Windows.Forms.Button btnBacklashMark;
        private System.Windows.Forms.Button btnFinishBacklash;
        private System.Windows.Forms.ComboBox comboBoxBacklashSteps;
        private System.Windows.Forms.Label labelBacklashSteps;
        private System.Windows.Forms.Label labelRevCalStatus;
        private System.Windows.Forms.Label labelBacklashStatus;
        private System.Windows.Forms.GroupBox groupBoxMotorSettings;
        private System.Windows.Forms.Label labelMotorSpeed;
        private System.Windows.Forms.NumericUpDown numericMotorSpeed;
        private System.Windows.Forms.Label labelMaxSpeed;
        private System.Windows.Forms.NumericUpDown numericMaxSpeed;
        private System.Windows.Forms.Label labelAcceleration;
        private System.Windows.Forms.NumericUpDown numericAcceleration;
        private System.Windows.Forms.Label labelDisableDelay;
        private System.Windows.Forms.NumericUpDown numericDisableDelay;
        private System.Windows.Forms.Button btnGetMotorConfig;
        private System.Windows.Forms.Button btnSetMotorConfig;
        private System.Windows.Forms.Button btnResetMotorConfig;
        private System.Windows.Forms.NumericUpDown numericStepsPerRev;
        private System.Windows.Forms.Label labelStepsPerRev;
        private System.Windows.Forms.Button btnGetStepsPerRev;
        private System.Windows.Forms.Button btnSetStepsPerRev;
        private System.Windows.Forms.GroupBox groupBoxDirection;
        private System.Windows.Forms.RadioButton radioUnidirectional;
        private System.Windows.Forms.RadioButton radioBidirectional;
        private System.Windows.Forms.CheckBox checkBoxReverse;
        private System.Windows.Forms.Button btnGetDirectionConfig;
        private System.Windows.Forms.Button btnSetDirectionConfig;
        private System.Windows.Forms.GroupBox groupBoxStepping;
        private System.Windows.Forms.Label labelStepAmount;
        private System.Windows.Forms.NumericUpDown numericStepAmount;
        private System.Windows.Forms.Button btnStepForward;
        private System.Windows.Forms.Button btnStepBackward;
        private System.Windows.Forms.Label labelAbsoluteStep;
        private System.Windows.Forms.NumericUpDown numericAbsoluteStep;
        private System.Windows.Forms.Button btnStepTo;
        private System.Windows.Forms.Button btnGetStepPosition;
        private System.Windows.Forms.GroupBox groupBoxMotorPower;
        private System.Windows.Forms.Button btnEnableMotor;
        private System.Windows.Forms.Button btnDisableMotor;
        private System.Windows.Forms.Label labelCurrentStep;
        private System.Windows.Forms.TextBox textBoxCurrentStep;
        private System.Windows.Forms.GroupBox groupBoxSystemInfo;
        private System.Windows.Forms.Button btnGetStatus;
        private System.Windows.Forms.Button btnGetVersion;
        private System.Windows.Forms.Button btnGetDeviceId;
        private System.Windows.Forms.TextBox textBoxSystemInfo;
        private System.Windows.Forms.Label labelFilterCount;
        private System.Windows.Forms.ComboBox comboBoxFilterCount;
        private System.Windows.Forms.Label labelFilterNames;
        private System.Windows.Forms.TextBox textBoxFilter1;
        private System.Windows.Forms.TextBox textBoxFilter2;
        private System.Windows.Forms.TextBox textBoxFilter3;
        private System.Windows.Forms.TextBox textBoxFilter4;
        private System.Windows.Forms.TextBox textBoxFilter5;
        private System.Windows.Forms.TextBox textBoxFilter6;
        private System.Windows.Forms.TextBox textBoxFilter7;
        private System.Windows.Forms.TextBox textBoxFilter8;
        private System.Windows.Forms.Button btnSetFilterConfig;
        private System.Windows.Forms.Label labelFilter1;
        private System.Windows.Forms.Label labelFilter2;
        private System.Windows.Forms.Label labelFilter3;
        private System.Windows.Forms.Label labelFilter4;
        private System.Windows.Forms.Label labelFilter5;
        private System.Windows.Forms.Label labelFilter6;
        private System.Windows.Forms.Label labelFilter7;
        private System.Windows.Forms.Label labelFilter8;
    }
}