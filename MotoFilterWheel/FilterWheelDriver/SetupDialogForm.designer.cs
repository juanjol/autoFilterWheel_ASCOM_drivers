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
            this.label1 = new System.Windows.Forms.Label();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.labelCompilationDate = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfiguration = new System.Windows.Forms.TabPage();
            this.tabPageMotorConfig = new System.Windows.Forms.TabPage();
            this.tabPageManualControl = new System.Windows.Forms.TabPage();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.textBoxLog = new System.Windows.Forms.TextBox();
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
            this.tabPageCalibration = new System.Windows.Forms.TabPage();
            this.groupBoxCalibration = new System.Windows.Forms.GroupBox();
            this.btnFinishCalibration = new System.Windows.Forms.Button();
            this.btnCalibrationBackward = new System.Windows.Forms.Button();
            this.btnCalibrationForward = new System.Windows.Forms.Button();
            this.btnStartCalibration = new System.Windows.Forms.Button();
            this.groupBoxBacklashCalibration = new System.Windows.Forms.GroupBox();
            this.btnStartBacklash = new System.Windows.Forms.Button();
            this.btnBacklashStep = new System.Windows.Forms.Button();
            this.btnBacklashMark = new System.Windows.Forms.Button();
            this.btnFinishBacklash = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageConfiguration.SuspendLayout();
            this.tabPageCalibration.SuspendLayout();
            this.groupBoxCalibration.SuspendLayout();
            this.groupBoxBacklashCalibration.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(341, 350);
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
            this.cmdCancel.Location = new System.Drawing.Point(406, 350);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 24);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "autoFilterWheel";
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = ((System.Drawing.Image)(resources.GetObject("picASCOM.Image")));
            this.picASCOM.Location = new System.Drawing.Point(292, 9);
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
            this.label2.Location = new System.Drawing.Point(13, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Comm Port";
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(77, 87);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(104, 21);
            this.comboBoxComPort.TabIndex = 7;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(190, 87);
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
            this.btnDisconnect.Location = new System.Drawing.Point(260, 87);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(80, 23);
            this.btnDisconnect.TabIndex = 7;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.BtnDisconnect_Click);
            // 
            // btnSet
            // 
            this.btnSet.Enabled = false;
            this.btnSet.Location = new System.Drawing.Point(338, 325);
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
            this.labelCompilationDate.Location = new System.Drawing.Point(12, 35);
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
            this.tabControl.Location = new System.Drawing.Point(13, 118);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(450, 200);
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
            this.tabPageConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfiguration.Name = "tabPageConfiguration";
            this.tabPageConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfiguration.Size = new System.Drawing.Size(317, 144);
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
            // tabPageMotorConfig
            //
            this.tabPageMotorConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageMotorConfig.Name = "tabPageMotorConfig";
            this.tabPageMotorConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMotorConfig.Size = new System.Drawing.Size(442, 174);
            this.tabPageMotorConfig.TabIndex = 2;
            this.tabPageMotorConfig.Text = "Motor Configuration";
            this.tabPageMotorConfig.UseVisualStyleBackColor = true;
            //
            // tabPageManualControl
            //
            this.tabPageManualControl.Location = new System.Drawing.Point(4, 22);
            this.tabPageManualControl.Name = "tabPageManualControl";
            this.tabPageManualControl.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageManualControl.Size = new System.Drawing.Size(442, 174);
            this.tabPageManualControl.TabIndex = 3;
            this.tabPageManualControl.Text = "Manual Control";
            this.tabPageManualControl.UseVisualStyleBackColor = true;
            //
            // tabPageCalibration
            // 
            this.tabPageCalibration.Controls.Add(this.groupBoxBacklashCalibration);
            this.tabPageCalibration.Controls.Add(this.groupBoxCalibration);
            this.tabPageCalibration.Location = new System.Drawing.Point(4, 22);
            this.tabPageCalibration.Name = "tabPageCalibration";
            this.tabPageCalibration.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCalibration.Size = new System.Drawing.Size(317, 150);
            this.tabPageCalibration.TabIndex = 1;
            this.tabPageCalibration.Text = "Calibration";
            this.tabPageCalibration.UseVisualStyleBackColor = true;
            //
            // tabPageLog
            //
            this.tabPageLog.Controls.Add(this.textBoxLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLog.Size = new System.Drawing.Size(317, 144);
            this.tabPageLog.TabIndex = 2;
            this.tabPageLog.Text = "Communication Log";
            this.tabPageLog.UseVisualStyleBackColor = true;
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
            this.textBoxLog.Size = new System.Drawing.Size(305, 132);
            this.textBoxLog.TabIndex = 0;
            this.textBoxLog.WordWrap = false;
            // 
            // groupBoxCalibration
            // 
            this.groupBoxCalibration.Controls.Add(this.btnFinishCalibration);
            this.groupBoxCalibration.Controls.Add(this.btnCalibrationBackward);
            this.groupBoxCalibration.Controls.Add(this.btnCalibrationForward);
            this.groupBoxCalibration.Controls.Add(this.btnStartCalibration);
            this.groupBoxCalibration.Location = new System.Drawing.Point(10, 10);
            this.groupBoxCalibration.Name = "groupBoxCalibration";
            this.groupBoxCalibration.Size = new System.Drawing.Size(300, 60);
            this.groupBoxCalibration.TabIndex = 30;
            this.groupBoxCalibration.TabStop = false;
            this.groupBoxCalibration.Text = "Revolution Calibration";
            //
            // groupBoxBacklashCalibration
            //
            this.groupBoxBacklashCalibration.Controls.Add(this.btnFinishBacklash);
            this.groupBoxBacklashCalibration.Controls.Add(this.btnBacklashMark);
            this.groupBoxBacklashCalibration.Controls.Add(this.btnBacklashStep);
            this.groupBoxBacklashCalibration.Controls.Add(this.btnStartBacklash);
            this.groupBoxBacklashCalibration.Location = new System.Drawing.Point(10, 80);
            this.groupBoxBacklashCalibration.Name = "groupBoxBacklashCalibration";
            this.groupBoxBacklashCalibration.Size = new System.Drawing.Size(300, 60);
            this.groupBoxBacklashCalibration.TabIndex = 31;
            this.groupBoxBacklashCalibration.TabStop = false;
            this.groupBoxBacklashCalibration.Text = "Backlash Calibration";
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
            // btnFinishBacklash
            //
            this.btnFinishBacklash.Enabled = false;
            this.btnFinishBacklash.Location = new System.Drawing.Point(200, 20);
            this.btnFinishBacklash.Name = "btnFinishBacklash";
            this.btnFinishBacklash.Size = new System.Drawing.Size(60, 25);
            this.btnFinishBacklash.TabIndex = 38;
            this.btnFinishBacklash.Text = "Finish";
            this.btnFinishBacklash.UseVisualStyleBackColor = true;
            this.btnFinishBacklash.Click += new System.EventHandler(this.BtnFinishBacklash_Click);
            // 
            // btnFinishCalibration
            // 
            this.btnFinishCalibration.Enabled = false;
            this.btnFinishCalibration.Location = new System.Drawing.Point(200, 20);
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
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 420);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.labelCompilationDate);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
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
            this.tabPageCalibration.ResumeLayout(false);
            this.groupBoxCalibration.ResumeLayout(false);
            this.groupBoxBacklashCalibration.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Label labelCompilationDate;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfiguration;
        private System.Windows.Forms.TabPage tabPageMotorConfig;
        private System.Windows.Forms.TabPage tabPageManualControl;
        private System.Windows.Forms.TabPage tabPageCalibration;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.GroupBox groupBoxCalibration;
        private System.Windows.Forms.Button btnStartCalibration;
        private System.Windows.Forms.Button btnCalibrationForward;
        private System.Windows.Forms.Button btnCalibrationBackward;
        private System.Windows.Forms.Button btnFinishCalibration;
        private System.Windows.Forms.GroupBox groupBoxBacklashCalibration;
        private System.Windows.Forms.Button btnStartBacklash;
        private System.Windows.Forms.Button btnBacklashStep;
        private System.Windows.Forms.Button btnBacklashMark;
        private System.Windows.Forms.Button btnFinishBacklash;
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