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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDialogForm));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.btnRefreshPorts = new System.Windows.Forms.Button();
            this.btnSetPort = new System.Windows.Forms.Button();
            this.labelOr = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.labelCompilationDate = new System.Windows.Forms.Label();
            this.labelSelectFilter = new System.Windows.Forms.Label();
            this.comboBoxSelectFilter = new System.Windows.Forms.ComboBox();
            this.btnSelectFilter = new System.Windows.Forms.Button();
            this.btnStopMovement = new System.Windows.Forms.Button();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfiguration = new System.Windows.Forms.TabPage();
            this.btnReloadFilterNames = new System.Windows.Forms.Button();
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
            this.labelFilter9 = new System.Windows.Forms.Label();
            this.textBoxFilter9 = new System.Windows.Forms.TextBox();
            this.tabPageMotorConfig = new System.Windows.Forms.TabPage();
            this.groupBoxMotorSettings = new System.Windows.Forms.GroupBox();
            this.chkMotorInverted = new System.Windows.Forms.CheckBox();
            this.chkEncoderInverted = new System.Windows.Forms.CheckBox();
            this.btnLoadMotorConfig = new System.Windows.Forms.Button();
            this.btnSetStepsPerRev = new System.Windows.Forms.Button();
            this.numericStepsPerRev = new System.Windows.Forms.NumericUpDown();
            this.btnResetMotorConfig = new System.Windows.Forms.Button();
            this.btnSetMotorConfig = new System.Windows.Forms.Button();
            this.numericDisableDelay = new System.Windows.Forms.NumericUpDown();
            this.labelDisableDelay = new System.Windows.Forms.Label();
            this.numericAcceleration = new System.Windows.Forms.NumericUpDown();
            this.labelAcceleration = new System.Windows.Forms.Label();
            this.numericMaxSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelMaxSpeed = new System.Windows.Forms.Label();
            this.numericMotorSpeed = new System.Windows.Forms.NumericUpDown();
            this.labelMotorSpeed = new System.Windows.Forms.Label();
            this.labelStepsPerRev = new System.Windows.Forms.Label();
            this.tabPageCustomAngles = new System.Windows.Forms.TabPage();
            this.tabPageDisplay = new System.Windows.Forms.TabPage();
            this.btnApplyDisplayConfig = new System.Windows.Forms.Button();
            this.btnLoadDisplayConfig = new System.Windows.Forms.Button();
            this.chkDisplayEnabled = new System.Windows.Forms.CheckBox();
            this.groupBoxPowerMode = new System.Windows.Forms.GroupBox();
            this.labelDisplayTimeout = new System.Windows.Forms.Label();
            this.numericDisplayTimeout = new System.Windows.Forms.NumericUpDown();
            this.radioPowerAlwaysOff = new System.Windows.Forms.RadioButton();
            this.radioPowerAlwaysOn = new System.Windows.Forms.RadioButton();
            this.radioPowerAuto = new System.Windows.Forms.RadioButton();
            this.groupBoxBrightness = new System.Windows.Forms.GroupBox();
            this.labelBrightnessValue = new System.Windows.Forms.Label();
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.groupBoxDisplayMode = new System.Windows.Forms.GroupBox();
            this.radioDisplayDetailed = new System.Windows.Forms.RadioButton();
            this.radioDisplayMinimal = new System.Windows.Forms.RadioButton();
            this.groupBoxDisplayRotation = new System.Windows.Forms.GroupBox();
            this.btnSetDisplayRotation = new System.Windows.Forms.Button();
            this.radioDisplayInverted = new System.Windows.Forms.RadioButton();
            this.radioDisplayNormal = new System.Windows.Forms.RadioButton();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.btnCommandHelp = new System.Windows.Forms.Button();
            this.btnSendCommand = new System.Windows.Forms.Button();
            this.textBoxManualCommand = new System.Windows.Forms.TextBox();
            this.labelManualCommand = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.linkLabelGitHub = new System.Windows.Forms.LinkLabel();
            this.linkLabelFirmwareRepo = new System.Windows.Forms.LinkLabel();
            this.linkLabelDriverRepo = new System.Windows.Forms.LinkLabel();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelAboutDescription = new System.Windows.Forms.Label();
            this.labelAboutTitle = new System.Windows.Forms.Label();
            this.toolTipCommand = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPageManualControl = new System.Windows.Forms.TabPage();
            this.groupBoxStepping = new System.Windows.Forms.GroupBox();
            this.btnGetCurrentStep = new System.Windows.Forms.Button();
            this.labelCurrentStep = new System.Windows.Forms.Label();
            this.btnStepTo = new System.Windows.Forms.Button();
            this.btnStepBackward = new System.Windows.Forms.Button();
            this.btnStepForward = new System.Windows.Forms.Button();
            this.numericStepAmount = new System.Windows.Forms.NumericUpDown();
            this.labelStepAmount = new System.Windows.Forms.Label();
            this.btnEmergencyStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageConfiguration.SuspendLayout();
            this.tabPageMotorConfig.SuspendLayout();
            this.groupBoxMotorSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStepsPerRev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDisableDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAcceleration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMotorSpeed)).BeginInit();
            this.tabPageDisplay.SuspendLayout();
            this.groupBoxPowerMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDisplayTimeout)).BeginInit();
            this.groupBoxBrightness.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            this.groupBoxDisplayMode.SuspendLayout();
            this.groupBoxDisplayRotation.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabPageManualControl.SuspendLayout();
            this.groupBoxStepping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStepAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(512, 460);
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
            this.cmdCancel.Location = new System.Drawing.Point(447, 460);
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
            this.picASCOM.Location = new System.Drawing.Point(529, 9);
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
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Comm Port";
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(76, 9);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(80, 21);
            this.comboBoxComPort.TabIndex = 7;
            // 
            // btnRefreshPorts
            // 
            this.btnRefreshPorts.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshPorts.Location = new System.Drawing.Point(162, 8);
            this.btnRefreshPorts.Margin = new System.Windows.Forms.Padding(0);
            this.btnRefreshPorts.Name = "btnRefreshPorts";
            this.btnRefreshPorts.Size = new System.Drawing.Size(25, 24);
            this.btnRefreshPorts.TabIndex = 8;
            this.btnRefreshPorts.Text = "🔄";
            this.btnRefreshPorts.UseVisualStyleBackColor = true;
            this.btnRefreshPorts.Click += new System.EventHandler(this.BtnRefreshPorts_Click);
            // 
            // btnSetPort
            // 
            this.btnSetPort.Location = new System.Drawing.Point(193, 9);
            this.btnSetPort.Name = "btnSetPort";
            this.btnSetPort.Size = new System.Drawing.Size(90, 23);
            this.btnSetPort.TabIndex = 9;
            this.btnSetPort.Text = "Set port && close";
            this.btnSetPort.UseVisualStyleBackColor = true;
            this.btnSetPort.Click += new System.EventHandler(this.BtnSetPort_Click);
            // 
            // labelOr
            // 
            this.labelOr.AutoSize = true;
            this.labelOr.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelOr.Location = new System.Drawing.Point(289, 14);
            this.labelOr.Name = "labelOr";
            this.labelOr.Size = new System.Drawing.Size(28, 13);
            this.labelOr.TabIndex = 10;
            this.labelOr.Text = "- or -";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(324, 9);
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
            this.btnDisconnect.Location = new System.Drawing.Point(390, 9);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(80, 23);
            this.btnDisconnect.TabIndex = 11;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
            // 
            // btnSet
            // 
            this.btnSet.Enabled = false;
            this.btnSet.Location = new System.Drawing.Point(430, 193);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(120, 25);
            this.btnSet.TabIndex = 8;
            this.btnSet.Text = "Save Configuration";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.BtnSet_Click);
            // 
            // labelCompilationDate
            // 
            this.labelCompilationDate.AutoSize = true;
            this.labelCompilationDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCompilationDate.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelCompilationDate.Location = new System.Drawing.Point(12, 467);
            this.labelCompilationDate.Name = "labelCompilationDate";
            this.labelCompilationDate.Size = new System.Drawing.Size(148, 13);
            this.labelCompilationDate.TabIndex = 29;
            this.labelCompilationDate.Text = "Built: [Date will be set in code]";
            // 
            // labelSelectFilter
            // 
            this.labelSelectFilter.AutoSize = true;
            this.labelSelectFilter.Location = new System.Drawing.Point(12, 52);
            this.labelSelectFilter.Name = "labelSelectFilter";
            this.labelSelectFilter.Size = new System.Drawing.Size(65, 13);
            this.labelSelectFilter.TabIndex = 40;
            this.labelSelectFilter.Text = "Select Filter:";
            // 
            // comboBoxSelectFilter
            // 
            this.comboBoxSelectFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectFilter.FormattingEnabled = true;
            this.comboBoxSelectFilter.Location = new System.Drawing.Point(87, 49);
            this.comboBoxSelectFilter.Name = "comboBoxSelectFilter";
            this.comboBoxSelectFilter.Size = new System.Drawing.Size(150, 21);
            this.comboBoxSelectFilter.TabIndex = 41;
            // 
            // btnSelectFilter
            // 
            this.btnSelectFilter.Enabled = false;
            this.btnSelectFilter.Location = new System.Drawing.Point(249, 47);
            this.btnSelectFilter.Name = "btnSelectFilter";
            this.btnSelectFilter.Size = new System.Drawing.Size(90, 25);
            this.btnSelectFilter.TabIndex = 42;
            this.btnSelectFilter.Text = "Select Filter";
            this.btnSelectFilter.UseVisualStyleBackColor = true;
            this.btnSelectFilter.Click += new System.EventHandler(this.BtnSelectFilter_Click);
            // 
            // btnStopMovement
            // 
            this.btnStopMovement.BackColor = System.Drawing.Color.Red;
            this.btnStopMovement.Enabled = false;
            this.btnStopMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopMovement.ForeColor = System.Drawing.Color.White;
            this.btnStopMovement.Location = new System.Drawing.Point(345, 47);
            this.btnStopMovement.Name = "btnStopMovement";
            this.btnStopMovement.Size = new System.Drawing.Size(60, 25);
            this.btnStopMovement.TabIndex = 43;
            this.btnStopMovement.Text = "STOP!";
            this.btnStopMovement.UseVisualStyleBackColor = false;
            this.btnStopMovement.Click += new System.EventHandler(this.BtnStopMovement_Click);
            // 
            // panelSeparator
            // 
            this.panelSeparator.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelSeparator.Location = new System.Drawing.Point(11, 77);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(564, 1);
            this.panelSeparator.TabIndex = 44;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfiguration);
            this.tabControl.Controls.Add(this.tabPageMotorConfig);
            this.tabControl.Controls.Add(this.tabPageCustomAngles);
            this.tabControl.Controls.Add(this.tabPageDisplay);
            this.tabControl.Controls.Add(this.tabPageLog);
            this.tabControl.Controls.Add(this.tabPageAbout);
            this.tabControl.Location = new System.Drawing.Point(11, 82);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(564, 371);
            this.tabControl.TabIndex = 30;
            // 
            // tabPageConfiguration
            // 
            this.tabPageConfiguration.Controls.Add(this.btnReloadFilterNames);
            this.tabPageConfiguration.Controls.Add(this.labelFilterCount);
            this.tabPageConfiguration.Controls.Add(this.comboBoxFilterCount);
            this.tabPageConfiguration.Controls.Add(this.btnSet);
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
            this.tabPageConfiguration.Controls.Add(this.labelFilter9);
            this.tabPageConfiguration.Controls.Add(this.textBoxFilter9);
            this.tabPageConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfiguration.Name = "tabPageConfiguration";
            this.tabPageConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfiguration.Size = new System.Drawing.Size(556, 345);
            this.tabPageConfiguration.TabIndex = 0;
            this.tabPageConfiguration.Text = "Filter Configuration";
            this.tabPageConfiguration.UseVisualStyleBackColor = true;
            // 
            // btnReloadFilterNames
            // 
            this.btnReloadFilterNames.Enabled = false;
            this.btnReloadFilterNames.Location = new System.Drawing.Point(280, 193);
            this.btnReloadFilterNames.Name = "btnReloadFilterNames";
            this.btnReloadFilterNames.Size = new System.Drawing.Size(140, 25);
            this.btnReloadFilterNames.TabIndex = 7;
            this.btnReloadFilterNames.Text = "Reload Filter Names";
            this.btnReloadFilterNames.UseVisualStyleBackColor = true;
            this.btnReloadFilterNames.Click += new System.EventHandler(this.BtnReloadFilterNames_Click);
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
            this.labelFilter2.Location = new System.Drawing.Point(10, 96);
            this.labelFilter2.Name = "labelFilter2";
            this.labelFilter2.Size = new System.Drawing.Size(13, 13);
            this.labelFilter2.TabIndex = 13;
            this.labelFilter2.Text = "2";
            // 
            // textBoxFilter2
            // 
            this.textBoxFilter2.Location = new System.Drawing.Point(29, 93);
            this.textBoxFilter2.MaxLength = 15;
            this.textBoxFilter2.Name = "textBoxFilter2";
            this.textBoxFilter2.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter2.TabIndex = 14;
            // 
            // labelFilter3
            // 
            this.labelFilter3.AutoSize = true;
            this.labelFilter3.Location = new System.Drawing.Point(10, 122);
            this.labelFilter3.Name = "labelFilter3";
            this.labelFilter3.Size = new System.Drawing.Size(13, 13);
            this.labelFilter3.TabIndex = 15;
            this.labelFilter3.Text = "3";
            // 
            // textBoxFilter3
            // 
            this.textBoxFilter3.Location = new System.Drawing.Point(29, 119);
            this.textBoxFilter3.MaxLength = 15;
            this.textBoxFilter3.Name = "textBoxFilter3";
            this.textBoxFilter3.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter3.TabIndex = 16;
            // 
            // labelFilter4
            // 
            this.labelFilter4.AutoSize = true;
            this.labelFilter4.Location = new System.Drawing.Point(200, 70);
            this.labelFilter4.Name = "labelFilter4";
            this.labelFilter4.Size = new System.Drawing.Size(13, 13);
            this.labelFilter4.TabIndex = 17;
            this.labelFilter4.Text = "4";
            // 
            // textBoxFilter4
            // 
            this.textBoxFilter4.Location = new System.Drawing.Point(219, 67);
            this.textBoxFilter4.MaxLength = 15;
            this.textBoxFilter4.Name = "textBoxFilter4";
            this.textBoxFilter4.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter4.TabIndex = 18;
            // 
            // labelFilter5
            // 
            this.labelFilter5.AutoSize = true;
            this.labelFilter5.Location = new System.Drawing.Point(200, 96);
            this.labelFilter5.Name = "labelFilter5";
            this.labelFilter5.Size = new System.Drawing.Size(13, 13);
            this.labelFilter5.TabIndex = 19;
            this.labelFilter5.Text = "5";
            // 
            // textBoxFilter5
            // 
            this.textBoxFilter5.Location = new System.Drawing.Point(219, 93);
            this.textBoxFilter5.MaxLength = 15;
            this.textBoxFilter5.Name = "textBoxFilter5";
            this.textBoxFilter5.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter5.TabIndex = 20;
            // 
            // labelFilter6
            // 
            this.labelFilter6.AutoSize = true;
            this.labelFilter6.Location = new System.Drawing.Point(200, 122);
            this.labelFilter6.Name = "labelFilter6";
            this.labelFilter6.Size = new System.Drawing.Size(13, 13);
            this.labelFilter6.TabIndex = 21;
            this.labelFilter6.Text = "6";
            // 
            // textBoxFilter6
            // 
            this.textBoxFilter6.Location = new System.Drawing.Point(219, 119);
            this.textBoxFilter6.MaxLength = 15;
            this.textBoxFilter6.Name = "textBoxFilter6";
            this.textBoxFilter6.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter6.TabIndex = 22;
            // 
            // labelFilter7
            // 
            this.labelFilter7.AutoSize = true;
            this.labelFilter7.Location = new System.Drawing.Point(390, 70);
            this.labelFilter7.Name = "labelFilter7";
            this.labelFilter7.Size = new System.Drawing.Size(13, 13);
            this.labelFilter7.TabIndex = 23;
            this.labelFilter7.Text = "7";
            // 
            // textBoxFilter7
            // 
            this.textBoxFilter7.Location = new System.Drawing.Point(409, 67);
            this.textBoxFilter7.MaxLength = 15;
            this.textBoxFilter7.Name = "textBoxFilter7";
            this.textBoxFilter7.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter7.TabIndex = 24;
            // 
            // labelFilter8
            // 
            this.labelFilter8.AutoSize = true;
            this.labelFilter8.Location = new System.Drawing.Point(390, 96);
            this.labelFilter8.Name = "labelFilter8";
            this.labelFilter8.Size = new System.Drawing.Size(13, 13);
            this.labelFilter8.TabIndex = 25;
            this.labelFilter8.Text = "8";
            // 
            // textBoxFilter8
            // 
            this.textBoxFilter8.Location = new System.Drawing.Point(409, 93);
            this.textBoxFilter8.MaxLength = 15;
            this.textBoxFilter8.Name = "textBoxFilter8";
            this.textBoxFilter8.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter8.TabIndex = 26;
            // 
            // labelFilter9
            // 
            this.labelFilter9.AutoSize = true;
            this.labelFilter9.Location = new System.Drawing.Point(390, 122);
            this.labelFilter9.Name = "labelFilter9";
            this.labelFilter9.Size = new System.Drawing.Size(13, 13);
            this.labelFilter9.TabIndex = 27;
            this.labelFilter9.Text = "9";
            // 
            // textBoxFilter9
            // 
            this.textBoxFilter9.Location = new System.Drawing.Point(409, 119);
            this.textBoxFilter9.MaxLength = 15;
            this.textBoxFilter9.Name = "textBoxFilter9";
            this.textBoxFilter9.Size = new System.Drawing.Size(120, 20);
            this.textBoxFilter9.TabIndex = 28;
            // 
            // tabPageMotorConfig
            // 
            this.tabPageMotorConfig.Controls.Add(this.groupBoxMotorSettings);
            this.tabPageMotorConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageMotorConfig.Name = "tabPageMotorConfig";
            this.tabPageMotorConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMotorConfig.Size = new System.Drawing.Size(556, 345);
            this.tabPageMotorConfig.TabIndex = 2;
            this.tabPageMotorConfig.Text = "Motor Configuration";
            this.tabPageMotorConfig.UseVisualStyleBackColor = true;
            // 
            // groupBoxMotorSettings
            // 
            this.groupBoxMotorSettings.Controls.Add(this.chkMotorInverted);
            this.groupBoxMotorSettings.Controls.Add(this.chkEncoderInverted);
            this.groupBoxMotorSettings.Controls.Add(this.btnLoadMotorConfig);
            this.groupBoxMotorSettings.Controls.Add(this.btnSetStepsPerRev);
            this.groupBoxMotorSettings.Controls.Add(this.numericStepsPerRev);
            this.groupBoxMotorSettings.Controls.Add(this.btnResetMotorConfig);
            this.groupBoxMotorSettings.Controls.Add(this.btnSetMotorConfig);
            this.groupBoxMotorSettings.Controls.Add(this.numericDisableDelay);
            this.groupBoxMotorSettings.Controls.Add(this.labelDisableDelay);
            this.groupBoxMotorSettings.Controls.Add(this.numericAcceleration);
            this.groupBoxMotorSettings.Controls.Add(this.labelAcceleration);
            this.groupBoxMotorSettings.Controls.Add(this.numericMaxSpeed);
            this.groupBoxMotorSettings.Controls.Add(this.labelMaxSpeed);
            this.groupBoxMotorSettings.Controls.Add(this.numericMotorSpeed);
            this.groupBoxMotorSettings.Controls.Add(this.labelMotorSpeed);
            this.groupBoxMotorSettings.Controls.Add(this.labelStepsPerRev);
            this.groupBoxMotorSettings.Location = new System.Drawing.Point(10, 10);
            this.groupBoxMotorSettings.Name = "groupBoxMotorSettings";
            this.groupBoxMotorSettings.Size = new System.Drawing.Size(540, 170);
            this.groupBoxMotorSettings.TabIndex = 0;
            this.groupBoxMotorSettings.TabStop = false;
            this.groupBoxMotorSettings.Text = "Motor Settings";
            // 
            // chkMotorInverted
            // 
            this.chkMotorInverted.AutoSize = true;
            this.chkMotorInverted.Location = new System.Drawing.Point(20, 85);
            this.chkMotorInverted.Name = "chkMotorInverted";
            this.chkMotorInverted.Size = new System.Drawing.Size(140, 17);
            this.chkMotorInverted.TabIndex = 13;
            this.chkMotorInverted.Text = "Motor Direction Inverted";
            this.chkMotorInverted.UseVisualStyleBackColor = true;
            // 
            // chkEncoderInverted
            // 
            this.chkEncoderInverted.AutoSize = true;
            this.chkEncoderInverted.Location = new System.Drawing.Point(190, 85);
            this.chkEncoderInverted.Name = "chkEncoderInverted";
            this.chkEncoderInverted.Size = new System.Drawing.Size(153, 17);
            this.chkEncoderInverted.TabIndex = 14;
            this.chkEncoderInverted.Text = "Encoder Direction Inverted";
            this.chkEncoderInverted.UseVisualStyleBackColor = true;
            // 
            // btnLoadMotorConfig
            // 
            this.btnLoadMotorConfig.Enabled = false;
            this.btnLoadMotorConfig.Location = new System.Drawing.Point(90, 130);
            this.btnLoadMotorConfig.Name = "btnLoadMotorConfig";
            this.btnLoadMotorConfig.Size = new System.Drawing.Size(75, 25);
            this.btnLoadMotorConfig.TabIndex = 15;
            this.btnLoadMotorConfig.Text = "Load";
            this.btnLoadMotorConfig.UseVisualStyleBackColor = true;
            this.btnLoadMotorConfig.Click += new System.EventHandler(this.BtnLoadMotorConfig_Click);
            // 
            // btnSetStepsPerRev
            // 
            this.btnSetStepsPerRev.Enabled = false;
            this.btnSetStepsPerRev.Location = new System.Drawing.Point(455, 50);
            this.btnSetStepsPerRev.Name = "btnSetStepsPerRev";
            this.btnSetStepsPerRev.Size = new System.Drawing.Size(35, 23);
            this.btnSetStepsPerRev.TabIndex = 12;
            this.btnSetStepsPerRev.Text = "Set";
            this.btnSetStepsPerRev.UseVisualStyleBackColor = true;
            // 
            // numericStepsPerRev
            // 
            this.numericStepsPerRev.Location = new System.Drawing.Point(433, 23);
            this.numericStepsPerRev.Maximum = new decimal(new int[] {
            100000,
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
            35500,
            0,
            0,
            0});
            // 
            // btnResetMotorConfig
            // 
            this.btnResetMotorConfig.Enabled = false;
            this.btnResetMotorConfig.Location = new System.Drawing.Point(265, 130);
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
            this.btnSetMotorConfig.Location = new System.Drawing.Point(180, 130);
            this.btnSetMotorConfig.Name = "btnSetMotorConfig";
            this.btnSetMotorConfig.Size = new System.Drawing.Size(75, 25);
            this.btnSetMotorConfig.TabIndex = 9;
            this.btnSetMotorConfig.Text = "Apply";
            this.btnSetMotorConfig.UseVisualStyleBackColor = true;
            this.btnSetMotorConfig.Click += new System.EventHandler(this.BtnSetMotorConfig_Click);
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
            this.labelDisableDelay.Location = new System.Drawing.Point(190, 55);
            this.labelDisableDelay.Name = "labelDisableDelay";
            this.labelDisableDelay.Size = new System.Drawing.Size(75, 13);
            this.labelDisableDelay.TabIndex = 6;
            this.labelDisableDelay.Text = "Disable Delay:";
            // 
            // numericAcceleration
            // 
            this.numericAcceleration.Location = new System.Drawing.Point(100, 53);
            this.numericAcceleration.Maximum = new decimal(new int[] {
            1000000,
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
            500,
            0,
            0,
            0});
            // 
            // labelAcceleration
            // 
            this.labelAcceleration.AutoSize = true;
            this.labelAcceleration.Location = new System.Drawing.Point(20, 55);
            this.labelAcceleration.Name = "labelAcceleration";
            this.labelAcceleration.Size = new System.Drawing.Size(69, 13);
            this.labelAcceleration.TabIndex = 4;
            this.labelAcceleration.Text = "Acceleration:";
            // 
            // numericMaxSpeed
            // 
            this.numericMaxSpeed.Location = new System.Drawing.Point(270, 23);
            this.numericMaxSpeed.Maximum = new decimal(new int[] {
            100000,
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
            this.labelMaxSpeed.Location = new System.Drawing.Point(190, 25);
            this.labelMaxSpeed.Name = "labelMaxSpeed";
            this.labelMaxSpeed.Size = new System.Drawing.Size(64, 13);
            this.labelMaxSpeed.TabIndex = 2;
            this.labelMaxSpeed.Text = "Max Speed:";
            // 
            // numericMotorSpeed
            // 
            this.numericMotorSpeed.Location = new System.Drawing.Point(100, 23);
            this.numericMotorSpeed.Maximum = new decimal(new int[] {
            50000,
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
            this.labelMotorSpeed.Location = new System.Drawing.Point(20, 25);
            this.labelMotorSpeed.Name = "labelMotorSpeed";
            this.labelMotorSpeed.Size = new System.Drawing.Size(41, 13);
            this.labelMotorSpeed.TabIndex = 0;
            this.labelMotorSpeed.Text = "Speed:";
            // 
            // labelStepsPerRev
            // 
            this.labelStepsPerRev.AutoSize = true;
            this.labelStepsPerRev.Location = new System.Drawing.Point(365, 25);
            this.labelStepsPerRev.Name = "labelStepsPerRev";
            this.labelStepsPerRev.Size = new System.Drawing.Size(62, 13);
            this.labelStepsPerRev.TabIndex = 8;
            this.labelStepsPerRev.Text = "Steps/Rev:";
            // 
            // tabPageCustomAngles
            // 
            this.tabPageCustomAngles.Location = new System.Drawing.Point(4, 22);
            this.tabPageCustomAngles.Name = "tabPageCustomAngles";
            this.tabPageCustomAngles.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCustomAngles.Size = new System.Drawing.Size(556, 345);
            this.tabPageCustomAngles.TabIndex = 6;
            this.tabPageCustomAngles.Text = "Calibration";
            this.tabPageCustomAngles.UseVisualStyleBackColor = true;
            // 
            // tabPageDisplay
            // 
            this.tabPageDisplay.Controls.Add(this.btnApplyDisplayConfig);
            this.tabPageDisplay.Controls.Add(this.btnLoadDisplayConfig);
            this.tabPageDisplay.Controls.Add(this.chkDisplayEnabled);
            this.tabPageDisplay.Controls.Add(this.groupBoxPowerMode);
            this.tabPageDisplay.Controls.Add(this.groupBoxBrightness);
            this.tabPageDisplay.Controls.Add(this.groupBoxDisplayMode);
            this.tabPageDisplay.Controls.Add(this.groupBoxDisplayRotation);
            this.tabPageDisplay.Location = new System.Drawing.Point(4, 22);
            this.tabPageDisplay.Name = "tabPageDisplay";
            this.tabPageDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDisplay.Size = new System.Drawing.Size(556, 345);
            this.tabPageDisplay.TabIndex = 5;
            this.tabPageDisplay.Text = "Display Configuration";
            this.tabPageDisplay.UseVisualStyleBackColor = true;
            // 
            // btnApplyDisplayConfig
            // 
            this.btnApplyDisplayConfig.Enabled = false;
            this.btnApplyDisplayConfig.Location = new System.Drawing.Point(470, 216);
            this.btnApplyDisplayConfig.Name = "btnApplyDisplayConfig";
            this.btnApplyDisplayConfig.Size = new System.Drawing.Size(75, 25);
            this.btnApplyDisplayConfig.TabIndex = 6;
            this.btnApplyDisplayConfig.Text = "Apply";
            this.btnApplyDisplayConfig.UseVisualStyleBackColor = true;
            this.btnApplyDisplayConfig.Click += new System.EventHandler(this.BtnApplyDisplayConfig_Click);
            // 
            // btnLoadDisplayConfig
            // 
            this.btnLoadDisplayConfig.Enabled = false;
            this.btnLoadDisplayConfig.Location = new System.Drawing.Point(380, 216);
            this.btnLoadDisplayConfig.Name = "btnLoadDisplayConfig";
            this.btnLoadDisplayConfig.Size = new System.Drawing.Size(75, 25);
            this.btnLoadDisplayConfig.TabIndex = 5;
            this.btnLoadDisplayConfig.Text = "Load";
            this.btnLoadDisplayConfig.UseVisualStyleBackColor = true;
            this.btnLoadDisplayConfig.Click += new System.EventHandler(this.BtnLoadDisplayConfig_Click);
            // 
            // chkDisplayEnabled
            // 
            this.chkDisplayEnabled.AutoSize = true;
            this.chkDisplayEnabled.Checked = true;
            this.chkDisplayEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisplayEnabled.Location = new System.Drawing.Point(10, 216);
            this.chkDisplayEnabled.Name = "chkDisplayEnabled";
            this.chkDisplayEnabled.Size = new System.Drawing.Size(102, 17);
            this.chkDisplayEnabled.TabIndex = 4;
            this.chkDisplayEnabled.Text = "Display Enabled";
            this.chkDisplayEnabled.UseVisualStyleBackColor = true;
            this.chkDisplayEnabled.CheckedChanged += new System.EventHandler(this.ChkDisplayEnabled_CheckedChanged);
            // 
            // groupBoxPowerMode
            // 
            this.groupBoxPowerMode.Controls.Add(this.labelDisplayTimeout);
            this.groupBoxPowerMode.Controls.Add(this.numericDisplayTimeout);
            this.groupBoxPowerMode.Controls.Add(this.radioPowerAlwaysOff);
            this.groupBoxPowerMode.Controls.Add(this.radioPowerAlwaysOn);
            this.groupBoxPowerMode.Controls.Add(this.radioPowerAuto);
            this.groupBoxPowerMode.Location = new System.Drawing.Point(10, 150);
            this.groupBoxPowerMode.Name = "groupBoxPowerMode";
            this.groupBoxPowerMode.Size = new System.Drawing.Size(540, 60);
            this.groupBoxPowerMode.TabIndex = 3;
            this.groupBoxPowerMode.TabStop = false;
            this.groupBoxPowerMode.Text = "Power Mode";
            // 
            // labelDisplayTimeout
            // 
            this.labelDisplayTimeout.AutoSize = true;
            this.labelDisplayTimeout.Location = new System.Drawing.Point(340, 25);
            this.labelDisplayTimeout.Name = "labelDisplayTimeout";
            this.labelDisplayTimeout.Size = new System.Drawing.Size(83, 13);
            this.labelDisplayTimeout.TabIndex = 4;
            this.labelDisplayTimeout.Text = "Auto timeout (s):";
            // 
            // numericDisplayTimeout
            // 
            this.numericDisplayTimeout.Location = new System.Drawing.Point(435, 23);
            this.numericDisplayTimeout.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericDisplayTimeout.Name = "numericDisplayTimeout";
            this.numericDisplayTimeout.Size = new System.Drawing.Size(80, 20);
            this.numericDisplayTimeout.TabIndex = 3;
            this.numericDisplayTimeout.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericDisplayTimeout.ValueChanged += new System.EventHandler(this.NumericDisplayTimeout_ValueChanged);
            // 
            // radioPowerAlwaysOff
            // 
            this.radioPowerAlwaysOff.AutoSize = true;
            this.radioPowerAlwaysOff.Location = new System.Drawing.Point(240, 25);
            this.radioPowerAlwaysOff.Name = "radioPowerAlwaysOff";
            this.radioPowerAlwaysOff.Size = new System.Drawing.Size(75, 17);
            this.radioPowerAlwaysOff.TabIndex = 2;
            this.radioPowerAlwaysOff.Text = "Always Off";
            this.radioPowerAlwaysOff.UseVisualStyleBackColor = true;
            this.radioPowerAlwaysOff.CheckedChanged += new System.EventHandler(this.RadioPowerMode_CheckedChanged);
            // 
            // radioPowerAlwaysOn
            // 
            this.radioPowerAlwaysOn.AutoSize = true;
            this.radioPowerAlwaysOn.Location = new System.Drawing.Point(130, 25);
            this.radioPowerAlwaysOn.Name = "radioPowerAlwaysOn";
            this.radioPowerAlwaysOn.Size = new System.Drawing.Size(75, 17);
            this.radioPowerAlwaysOn.TabIndex = 1;
            this.radioPowerAlwaysOn.Text = "Always On";
            this.radioPowerAlwaysOn.UseVisualStyleBackColor = true;
            this.radioPowerAlwaysOn.CheckedChanged += new System.EventHandler(this.RadioPowerMode_CheckedChanged);
            // 
            // radioPowerAuto
            // 
            this.radioPowerAuto.AutoSize = true;
            this.radioPowerAuto.Checked = true;
            this.radioPowerAuto.Location = new System.Drawing.Point(10, 25);
            this.radioPowerAuto.Name = "radioPowerAuto";
            this.radioPowerAuto.Size = new System.Drawing.Size(47, 17);
            this.radioPowerAuto.TabIndex = 0;
            this.radioPowerAuto.TabStop = true;
            this.radioPowerAuto.Text = "Auto";
            this.radioPowerAuto.UseVisualStyleBackColor = true;
            this.radioPowerAuto.CheckedChanged += new System.EventHandler(this.RadioPowerMode_CheckedChanged);
            // 
            // groupBoxBrightness
            // 
            this.groupBoxBrightness.Controls.Add(this.labelBrightnessValue);
            this.groupBoxBrightness.Controls.Add(this.trackBarBrightness);
            this.groupBoxBrightness.Location = new System.Drawing.Point(270, 80);
            this.groupBoxBrightness.Name = "groupBoxBrightness";
            this.groupBoxBrightness.Size = new System.Drawing.Size(280, 60);
            this.groupBoxBrightness.TabIndex = 2;
            this.groupBoxBrightness.TabStop = false;
            this.groupBoxBrightness.Text = "Brightness";
            // 
            // labelBrightnessValue
            // 
            this.labelBrightnessValue.AutoSize = true;
            this.labelBrightnessValue.Location = new System.Drawing.Point(240, 28);
            this.labelBrightnessValue.Name = "labelBrightnessValue";
            this.labelBrightnessValue.Size = new System.Drawing.Size(25, 13);
            this.labelBrightnessValue.TabIndex = 1;
            this.labelBrightnessValue.Text = "128";
            // 
            // trackBarBrightness
            // 
            this.trackBarBrightness.Location = new System.Drawing.Point(10, 20);
            this.trackBarBrightness.Maximum = 255;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(220, 45);
            this.trackBarBrightness.TabIndex = 0;
            this.trackBarBrightness.TickFrequency = 25;
            this.trackBarBrightness.Value = 128;
            this.trackBarBrightness.Scroll += new System.EventHandler(this.TrackBarBrightness_Scroll);
            this.trackBarBrightness.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TrackBarBrightness_MouseUp);
            // 
            // groupBoxDisplayMode
            // 
            this.groupBoxDisplayMode.Controls.Add(this.radioDisplayDetailed);
            this.groupBoxDisplayMode.Controls.Add(this.radioDisplayMinimal);
            this.groupBoxDisplayMode.Location = new System.Drawing.Point(10, 80);
            this.groupBoxDisplayMode.Name = "groupBoxDisplayMode";
            this.groupBoxDisplayMode.Size = new System.Drawing.Size(250, 60);
            this.groupBoxDisplayMode.TabIndex = 1;
            this.groupBoxDisplayMode.TabStop = false;
            this.groupBoxDisplayMode.Text = "Display Mode";
            // 
            // radioDisplayDetailed
            // 
            this.radioDisplayDetailed.AutoSize = true;
            this.radioDisplayDetailed.Checked = true;
            this.radioDisplayDetailed.Location = new System.Drawing.Point(80, 25);
            this.radioDisplayDetailed.Name = "radioDisplayDetailed";
            this.radioDisplayDetailed.Size = new System.Drawing.Size(64, 17);
            this.radioDisplayDetailed.TabIndex = 1;
            this.radioDisplayDetailed.TabStop = true;
            this.radioDisplayDetailed.Text = "Detailed";
            this.radioDisplayDetailed.UseVisualStyleBackColor = true;
            this.radioDisplayDetailed.CheckedChanged += new System.EventHandler(this.RadioDisplayMode_CheckedChanged);
            // 
            // radioDisplayMinimal
            // 
            this.radioDisplayMinimal.AutoSize = true;
            this.radioDisplayMinimal.Location = new System.Drawing.Point(10, 25);
            this.radioDisplayMinimal.Name = "radioDisplayMinimal";
            this.radioDisplayMinimal.Size = new System.Drawing.Size(60, 17);
            this.radioDisplayMinimal.TabIndex = 0;
            this.radioDisplayMinimal.Text = "Minimal";
            this.radioDisplayMinimal.UseVisualStyleBackColor = true;
            this.radioDisplayMinimal.CheckedChanged += new System.EventHandler(this.RadioDisplayMode_CheckedChanged);
            // 
            // groupBoxDisplayRotation
            // 
            this.groupBoxDisplayRotation.Controls.Add(this.btnSetDisplayRotation);
            this.groupBoxDisplayRotation.Controls.Add(this.radioDisplayInverted);
            this.groupBoxDisplayRotation.Controls.Add(this.radioDisplayNormal);
            this.groupBoxDisplayRotation.Location = new System.Drawing.Point(10, 10);
            this.groupBoxDisplayRotation.Name = "groupBoxDisplayRotation";
            this.groupBoxDisplayRotation.Size = new System.Drawing.Size(540, 60);
            this.groupBoxDisplayRotation.TabIndex = 0;
            this.groupBoxDisplayRotation.TabStop = false;
            this.groupBoxDisplayRotation.Text = "Display Rotation";
            // 
            // btnSetDisplayRotation
            // 
            this.btnSetDisplayRotation.Enabled = false;
            this.btnSetDisplayRotation.Location = new System.Drawing.Point(200, 20);
            this.btnSetDisplayRotation.Name = "btnSetDisplayRotation";
            this.btnSetDisplayRotation.Size = new System.Drawing.Size(75, 25);
            this.btnSetDisplayRotation.TabIndex = 3;
            this.btnSetDisplayRotation.Text = "Apply";
            this.btnSetDisplayRotation.UseVisualStyleBackColor = true;
            // 
            // radioDisplayInverted
            // 
            this.radioDisplayInverted.AutoSize = true;
            this.radioDisplayInverted.Location = new System.Drawing.Point(80, 25);
            this.radioDisplayInverted.Name = "radioDisplayInverted";
            this.radioDisplayInverted.Size = new System.Drawing.Size(95, 17);
            this.radioDisplayInverted.TabIndex = 1;
            this.radioDisplayInverted.Text = "Inverted (180°)";
            this.radioDisplayInverted.UseVisualStyleBackColor = true;
            this.radioDisplayInverted.CheckedChanged += new System.EventHandler(this.RadioDisplayRotation_CheckedChanged);
            // 
            // radioDisplayNormal
            // 
            this.radioDisplayNormal.AutoSize = true;
            this.radioDisplayNormal.Checked = true;
            this.radioDisplayNormal.Location = new System.Drawing.Point(10, 25);
            this.radioDisplayNormal.Name = "radioDisplayNormal";
            this.radioDisplayNormal.Size = new System.Drawing.Size(58, 17);
            this.radioDisplayNormal.TabIndex = 0;
            this.radioDisplayNormal.TabStop = true;
            this.radioDisplayNormal.Text = "Normal";
            this.radioDisplayNormal.UseVisualStyleBackColor = true;
            this.radioDisplayNormal.CheckedChanged += new System.EventHandler(this.RadioDisplayRotation_CheckedChanged);
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.btnCommandHelp);
            this.tabPageLog.Controls.Add(this.btnSendCommand);
            this.tabPageLog.Controls.Add(this.textBoxManualCommand);
            this.tabPageLog.Controls.Add(this.labelManualCommand);
            this.tabPageLog.Controls.Add(this.textBoxLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLog.Size = new System.Drawing.Size(556, 345);
            this.tabPageLog.TabIndex = 2;
            this.tabPageLog.Text = "Communication Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // btnCommandHelp
            // 
            this.btnCommandHelp.Location = new System.Drawing.Point(473, 312);
            this.btnCommandHelp.Name = "btnCommandHelp";
            this.btnCommandHelp.Size = new System.Drawing.Size(80, 24);
            this.btnCommandHelp.TabIndex = 4;
            this.btnCommandHelp.Text = "Help (F1)";
            this.btnCommandHelp.UseVisualStyleBackColor = true;
            this.btnCommandHelp.Click += new System.EventHandler(this.BtnCommandHelp_Click);
            // 
            // btnSendCommand
            // 
            this.btnSendCommand.Location = new System.Drawing.Point(392, 312);
            this.btnSendCommand.Name = "btnSendCommand";
            this.btnSendCommand.Size = new System.Drawing.Size(75, 24);
            this.btnSendCommand.TabIndex = 3;
            this.btnSendCommand.Text = "Send";
            this.btnSendCommand.UseVisualStyleBackColor = true;
            this.btnSendCommand.Click += new System.EventHandler(this.BtnSendCommand_Click);
            // 
            // textBoxManualCommand
            // 
            this.textBoxManualCommand.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxManualCommand.Location = new System.Drawing.Point(6, 314);
            this.textBoxManualCommand.Name = "textBoxManualCommand";
            this.textBoxManualCommand.Size = new System.Drawing.Size(380, 22);
            this.textBoxManualCommand.TabIndex = 2;
            this.textBoxManualCommand.TextChanged += new System.EventHandler(this.TextBoxManualCommand_TextChanged);
            this.textBoxManualCommand.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxManualCommand_KeyDown);
            // 
            // labelManualCommand
            // 
            this.labelManualCommand.AutoSize = true;
            this.labelManualCommand.Location = new System.Drawing.Point(6, 295);
            this.labelManualCommand.Name = "labelManualCommand";
            this.labelManualCommand.Size = new System.Drawing.Size(95, 13);
            this.labelManualCommand.TabIndex = 1;
            this.labelManualCommand.Text = "Manual Command:";
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = System.Drawing.Color.Black;
            this.textBoxLog.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLog.ForeColor = System.Drawing.Color.LimeGreen;
            this.textBoxLog.Location = new System.Drawing.Point(6, 6);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(547, 280);
            this.textBoxLog.TabIndex = 0;
            this.textBoxLog.WordWrap = false;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.linkLabelGitHub);
            this.tabPageAbout.Controls.Add(this.linkLabelFirmwareRepo);
            this.tabPageAbout.Controls.Add(this.linkLabelDriverRepo);
            this.tabPageAbout.Controls.Add(this.labelAuthor);
            this.tabPageAbout.Controls.Add(this.labelVersion);
            this.tabPageAbout.Controls.Add(this.labelAboutDescription);
            this.tabPageAbout.Controls.Add(this.labelAboutTitle);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAbout.Size = new System.Drawing.Size(556, 345);
            this.tabPageAbout.TabIndex = 7;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // linkLabelGitHub
            // 
            this.linkLabelGitHub.AutoSize = true;
            this.linkLabelGitHub.Location = new System.Drawing.Point(20, 195);
            this.linkLabelGitHub.Name = "linkLabelGitHub";
            this.linkLabelGitHub.Size = new System.Drawing.Size(179, 13);
            this.linkLabelGitHub.TabIndex = 6;
            this.linkLabelGitHub.TabStop = true;
            this.linkLabelGitHub.Text = "My Github profile: github.com/juanjol";
            this.linkLabelGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelGitHub_LinkClicked);
            // 
            // linkLabelFirmwareRepo
            // 
            this.linkLabelFirmwareRepo.AutoSize = true;
            this.linkLabelFirmwareRepo.Location = new System.Drawing.Point(20, 175);
            this.linkLabelFirmwareRepo.Name = "linkLabelFirmwareRepo";
            this.linkLabelFirmwareRepo.Size = new System.Drawing.Size(102, 13);
            this.linkLabelFirmwareRepo.TabIndex = 5;
            this.linkLabelFirmwareRepo.TabStop = true;
            this.linkLabelFirmwareRepo.Text = "Firmware Repository";
            this.linkLabelFirmwareRepo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelFirmwareRepo_LinkClicked);
            // 
            // linkLabelDriverRepo
            // 
            this.linkLabelDriverRepo.AutoSize = true;
            this.linkLabelDriverRepo.Location = new System.Drawing.Point(20, 155);
            this.linkLabelDriverRepo.Name = "linkLabelDriverRepo";
            this.linkLabelDriverRepo.Size = new System.Drawing.Size(88, 13);
            this.linkLabelDriverRepo.TabIndex = 4;
            this.linkLabelDriverRepo.TabStop = true;
            this.linkLabelDriverRepo.Text = "Driver Repository";
            this.linkLabelDriverRepo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabelDriverRepo_LinkClicked);
            // 
            // labelAuthor
            // 
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(20, 115);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(107, 13);
            this.labelAuthor.TabIndex = 3;
            this.labelAuthor.Text = "Author: Juanjo López";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(20, 95);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(88, 13);
            this.labelVersion.TabIndex = 2;
            this.labelVersion.Text = "Version: [version]";
            // 
            // labelAboutDescription
            // 
            this.labelAboutDescription.AutoSize = true;
            this.labelAboutDescription.Location = new System.Drawing.Point(20, 55);
            this.labelAboutDescription.MaximumSize = new System.Drawing.Size(500, 0);
            this.labelAboutDescription.Name = "labelAboutDescription";
            this.labelAboutDescription.Size = new System.Drawing.Size(484, 26);
            this.labelAboutDescription.TabIndex = 1;
            this.labelAboutDescription.Text = "ASCOM FilterWheel driver for ESP32-C3 based motorized filter wheel controller. Su" +
    "pports 3-9 position filter wheels with magnetic encoder feedback and custom angl" +
    "e calibration.";
            // 
            // labelAboutTitle
            // 
            this.labelAboutTitle.AutoSize = true;
            this.labelAboutTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAboutTitle.Location = new System.Drawing.Point(20, 20);
            this.labelAboutTitle.Name = "labelAboutTitle";
            this.labelAboutTitle.Size = new System.Drawing.Size(271, 24);
            this.labelAboutTitle.TabIndex = 0;
            this.labelAboutTitle.Text = "autoFilterWheel (ESP32-C3)";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 494);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(589, 22);
            this.statusStrip.TabIndex = 30;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // tabPageManualControl
            // 
            this.tabPageManualControl.Controls.Add(this.groupBoxStepping);
            this.tabPageManualControl.Controls.Add(this.btnEmergencyStop);
            this.tabPageManualControl.Location = new System.Drawing.Point(4, 22);
            this.tabPageManualControl.Name = "tabPageManualControl";
            this.tabPageManualControl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageManualControl.Size = new System.Drawing.Size(556, 224);
            this.tabPageManualControl.TabIndex = 3;
            this.tabPageManualControl.Text = "Manual Control";
            this.tabPageManualControl.UseVisualStyleBackColor = true;
            // 
            // groupBoxStepping
            // 
            this.groupBoxStepping.Controls.Add(this.btnGetCurrentStep);
            this.groupBoxStepping.Controls.Add(this.labelCurrentStep);
            this.groupBoxStepping.Controls.Add(this.btnStepTo);
            this.groupBoxStepping.Controls.Add(this.btnStepBackward);
            this.groupBoxStepping.Controls.Add(this.btnStepForward);
            this.groupBoxStepping.Controls.Add(this.numericStepAmount);
            this.groupBoxStepping.Controls.Add(this.labelStepAmount);
            this.groupBoxStepping.Location = new System.Drawing.Point(10, 10);
            this.groupBoxStepping.Name = "groupBoxStepping";
            this.groupBoxStepping.Size = new System.Drawing.Size(400, 120);
            this.groupBoxStepping.TabIndex = 0;
            this.groupBoxStepping.TabStop = false;
            this.groupBoxStepping.Text = "Manual Stepping";
            // 
            // btnGetCurrentStep
            // 
            this.btnGetCurrentStep.Enabled = false;
            this.btnGetCurrentStep.Location = new System.Drawing.Point(300, 20);
            this.btnGetCurrentStep.Name = "btnGetCurrentStep";
            this.btnGetCurrentStep.Size = new System.Drawing.Size(80, 25);
            this.btnGetCurrentStep.TabIndex = 6;
            this.btnGetCurrentStep.Text = "Get Current";
            this.btnGetCurrentStep.UseVisualStyleBackColor = true;
            // 
            // labelCurrentStep
            // 
            this.labelCurrentStep.AutoSize = true;
            this.labelCurrentStep.Location = new System.Drawing.Point(200, 25);
            this.labelCurrentStep.Name = "labelCurrentStep";
            this.labelCurrentStep.Size = new System.Drawing.Size(78, 13);
            this.labelCurrentStep.TabIndex = 5;
            this.labelCurrentStep.Text = "Current Step: 0";
            // 
            // btnStepTo
            // 
            this.btnStepTo.Enabled = false;
            this.btnStepTo.Location = new System.Drawing.Point(225, 55);
            this.btnStepTo.Name = "btnStepTo";
            this.btnStepTo.Size = new System.Drawing.Size(89, 25);
            this.btnStepTo.TabIndex = 4;
            this.btnStepTo.Text = "Step To";
            this.btnStepTo.UseVisualStyleBackColor = true;
            // 
            // btnStepBackward
            // 
            this.btnStepBackward.Enabled = false;
            this.btnStepBackward.Location = new System.Drawing.Point(110, 55);
            this.btnStepBackward.Name = "btnStepBackward";
            this.btnStepBackward.Size = new System.Drawing.Size(100, 25);
            this.btnStepBackward.TabIndex = 3;
            this.btnStepBackward.Text = "Step Backward";
            this.btnStepBackward.UseVisualStyleBackColor = true;
            // 
            // btnStepForward
            // 
            this.btnStepForward.Enabled = false;
            this.btnStepForward.Location = new System.Drawing.Point(20, 55);
            this.btnStepForward.Name = "btnStepForward";
            this.btnStepForward.Size = new System.Drawing.Size(80, 25);
            this.btnStepForward.TabIndex = 2;
            this.btnStepForward.Text = "Step Forward";
            this.btnStepForward.UseVisualStyleBackColor = true;
            // 
            // numericStepAmount
            // 
            this.numericStepAmount.Location = new System.Drawing.Point(65, 23);
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
            this.numericStepAmount.Size = new System.Drawing.Size(80, 20);
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
            this.labelStepAmount.Location = new System.Drawing.Point(20, 25);
            this.labelStepAmount.Name = "labelStepAmount";
            this.labelStepAmount.Size = new System.Drawing.Size(37, 13);
            this.labelStepAmount.TabIndex = 0;
            this.labelStepAmount.Text = "Steps:";
            // 
            // btnEmergencyStop
            // 
            this.btnEmergencyStop.BackColor = System.Drawing.Color.Red;
            this.btnEmergencyStop.Enabled = false;
            this.btnEmergencyStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmergencyStop.ForeColor = System.Drawing.Color.White;
            this.btnEmergencyStop.Location = new System.Drawing.Point(439, 18);
            this.btnEmergencyStop.Name = "btnEmergencyStop";
            this.btnEmergencyStop.Size = new System.Drawing.Size(100, 51);
            this.btnEmergencyStop.TabIndex = 1;
            this.btnEmergencyStop.Text = "EMERGENCY STOP";
            this.btnEmergencyStop.UseVisualStyleBackColor = false;
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 516);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelSeparator);
            this.Controls.Add(this.btnStopMovement);
            this.Controls.Add(this.btnSelectFilter);
            this.Controls.Add(this.comboBoxSelectFilter);
            this.Controls.Add(this.labelSelectFilter);
            this.Controls.Add(this.labelCompilationDate);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.labelOr);
            this.Controls.Add(this.btnRefreshPorts);
            this.Controls.Add(this.btnSetPort);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "autoFilterWheel Setup";
            this.Load += new System.EventHandler(this.SetupDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageConfiguration.ResumeLayout(false);
            this.tabPageConfiguration.PerformLayout();
            this.tabPageMotorConfig.ResumeLayout(false);
            this.groupBoxMotorSettings.ResumeLayout(false);
            this.groupBoxMotorSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStepsPerRev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericDisableDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAcceleration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMaxSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericMotorSpeed)).EndInit();
            this.tabPageDisplay.ResumeLayout(false);
            this.tabPageDisplay.PerformLayout();
            this.groupBoxPowerMode.ResumeLayout(false);
            this.groupBoxPowerMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericDisplayTimeout)).EndInit();
            this.groupBoxBrightness.ResumeLayout(false);
            this.groupBoxBrightness.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
            this.groupBoxDisplayMode.ResumeLayout(false);
            this.groupBoxDisplayMode.PerformLayout();
            this.groupBoxDisplayRotation.ResumeLayout(false);
            this.groupBoxDisplayRotation.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabPageManualControl.ResumeLayout(false);
            this.groupBoxStepping.ResumeLayout(false);
            this.groupBoxStepping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStepAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.Button btnRefreshPorts;
        private System.Windows.Forms.Button btnSetPort;
        private System.Windows.Forms.Label labelOr;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnReloadFilterNames;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Label labelCompilationDate;
        private System.Windows.Forms.Label labelSelectFilter;
        private System.Windows.Forms.ComboBox comboBoxSelectFilter;
        private System.Windows.Forms.Button btnSelectFilter;
        private System.Windows.Forms.Button btnStopMovement;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfiguration;
        private System.Windows.Forms.TabPage tabPageMotorConfig;
        private System.Windows.Forms.TabPage tabPageManualControl;
        private System.Windows.Forms.TabPage tabPageCustomAngles;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.Label labelAboutTitle;
        private System.Windows.Forms.Label labelAboutDescription;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.LinkLabel linkLabelDriverRepo;
        private System.Windows.Forms.LinkLabel linkLabelFirmwareRepo;
        private System.Windows.Forms.LinkLabel linkLabelGitHub;
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
        private System.Windows.Forms.TextBox textBoxFilter9;
        private System.Windows.Forms.Label labelFilter1;
        private System.Windows.Forms.Label labelFilter2;
        private System.Windows.Forms.Label labelFilter3;
        private System.Windows.Forms.Label labelFilter4;
        private System.Windows.Forms.Label labelFilter5;
        private System.Windows.Forms.Label labelFilter6;
        private System.Windows.Forms.Label labelFilter7;
        private System.Windows.Forms.Label labelFilter8;
        private System.Windows.Forms.Label labelFilter9;
        private System.Windows.Forms.GroupBox groupBoxMotorSettings;
        private System.Windows.Forms.CheckBox chkMotorInverted;
        private System.Windows.Forms.CheckBox chkEncoderInverted;
        private System.Windows.Forms.Button btnLoadMotorConfig;
        private System.Windows.Forms.Button btnSetStepsPerRev;
        private System.Windows.Forms.Button btnResetMotorConfig;
        private System.Windows.Forms.Button btnSetMotorConfig;
        private System.Windows.Forms.NumericUpDown numericDisableDelay;
        private System.Windows.Forms.Label labelDisableDelay;
        private System.Windows.Forms.NumericUpDown numericAcceleration;
        private System.Windows.Forms.Label labelAcceleration;
        private System.Windows.Forms.NumericUpDown numericMaxSpeed;
        private System.Windows.Forms.Label labelMaxSpeed;
        private System.Windows.Forms.NumericUpDown numericMotorSpeed;
        private System.Windows.Forms.Label labelMotorSpeed;
        private System.Windows.Forms.NumericUpDown numericStepsPerRev;
        private System.Windows.Forms.Label labelStepsPerRev;
        private System.Windows.Forms.GroupBox groupBoxStepping;
        private System.Windows.Forms.Label labelStepAmount;
        private System.Windows.Forms.NumericUpDown numericStepAmount;
        private System.Windows.Forms.Button btnStepForward;
        private System.Windows.Forms.Button btnStepBackward;
        private System.Windows.Forms.Button btnStepTo;
        private System.Windows.Forms.Label labelCurrentStep;
        private System.Windows.Forms.Button btnGetCurrentStep;
        private System.Windows.Forms.Button btnEmergencyStop;
        private System.Windows.Forms.TabPage tabPageDisplay;
        private System.Windows.Forms.GroupBox groupBoxDisplayRotation;
        private System.Windows.Forms.RadioButton radioDisplayNormal;
        private System.Windows.Forms.RadioButton radioDisplayInverted;
        private System.Windows.Forms.Button btnSetDisplayRotation;
        private System.Windows.Forms.GroupBox groupBoxDisplayMode;
        private System.Windows.Forms.RadioButton radioDisplayDetailed;
        private System.Windows.Forms.RadioButton radioDisplayMinimal;
        private System.Windows.Forms.GroupBox groupBoxBrightness;
        private System.Windows.Forms.Label labelBrightnessValue;
        private System.Windows.Forms.TrackBar trackBarBrightness;
        private System.Windows.Forms.GroupBox groupBoxPowerMode;
        private System.Windows.Forms.RadioButton radioPowerAlwaysOff;
        private System.Windows.Forms.RadioButton radioPowerAlwaysOn;
        private System.Windows.Forms.RadioButton radioPowerAuto;
        private System.Windows.Forms.CheckBox chkDisplayEnabled;
        private System.Windows.Forms.Button btnLoadDisplayConfig;
        private System.Windows.Forms.Button btnApplyDisplayConfig;
        private System.Windows.Forms.NumericUpDown numericDisplayTimeout;
        private System.Windows.Forms.Label labelDisplayTimeout;
        private System.Windows.Forms.Label labelManualCommand;
        private System.Windows.Forms.TextBox textBoxManualCommand;
        private System.Windows.Forms.Button btnSendCommand;
        private System.Windows.Forms.Button btnCommandHelp;
        private System.Windows.Forms.ToolTip toolTipCommand;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}