namespace ASCOM.autoFilterWheel.FilterWheel
{
    partial class CustomAnglesControl
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxSystemInfo = new System.Windows.Forms.GroupBox();
            this.lblEncoderAngleValue = new System.Windows.Forms.Label();
            this.lblEncoderAngle = new System.Windows.Forms.Label();
            this.lblCurrentPosValue = new System.Windows.Forms.Label();
            this.lblCurrentPos = new System.Windows.Forms.Label();
            this.lblFilterCountValue = new System.Windows.Forms.Label();
            this.lblFilterCount = new System.Windows.Forms.Label();
            this.groupBoxMovement = new System.Windows.Forms.GroupBox();
            this.lblCustomSteps = new System.Windows.Forms.Label();
            this.nudCustomSteps = new System.Windows.Forms.NumericUpDown();
            this.btnCustomBackward = new System.Windows.Forms.Button();
            this.btnCustomForward = new System.Windows.Forms.Button();
            this.lblForward = new System.Windows.Forms.Label();
            this.btnFwd100 = new System.Windows.Forms.Button();
            this.btnFwd50 = new System.Windows.Forms.Button();
            this.btnFwd10 = new System.Windows.Forms.Button();
            this.btnFwd1 = new System.Windows.Forms.Button();
            this.lblBackward = new System.Windows.Forms.Label();
            this.btnBack1 = new System.Windows.Forms.Button();
            this.btnBack10 = new System.Windows.Forms.Button();
            this.btnBack50 = new System.Windows.Forms.Button();
            this.btnBack100 = new System.Windows.Forms.Button();
            this.groupBoxPositions = new System.Windows.Forms.GroupBox();
            this.dgvPositions = new System.Windows.Forms.DataGridView();
            this.colPosition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFilterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAngle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCalibrated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.btnRefreshAngles = new System.Windows.Forms.Button();
            this.btnClearCalibration = new System.Windows.Forms.Button();
            this.groupBoxSystemInfo.SuspendLayout();
            this.groupBoxMovement.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCustomSteps)).BeginInit();
            this.groupBoxPositions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPositions)).BeginInit();
            this.groupBoxActions.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBoxSystemInfo
            //
            this.groupBoxSystemInfo.Controls.Add(this.lblEncoderAngleValue);
            this.groupBoxSystemInfo.Controls.Add(this.lblEncoderAngle);
            this.groupBoxSystemInfo.Controls.Add(this.lblCurrentPosValue);
            this.groupBoxSystemInfo.Controls.Add(this.lblCurrentPos);
            this.groupBoxSystemInfo.Controls.Add(this.lblFilterCountValue);
            this.groupBoxSystemInfo.Controls.Add(this.lblFilterCount);
            this.groupBoxSystemInfo.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSystemInfo.Name = "groupBoxSystemInfo";
            this.groupBoxSystemInfo.Size = new System.Drawing.Size(540, 42);
            this.groupBoxSystemInfo.TabIndex = 0;
            this.groupBoxSystemInfo.TabStop = false;
            this.groupBoxSystemInfo.Text = "System Information";
            //
            // lblEncoderAngleValue
            //
            this.lblEncoderAngleValue.AutoSize = true;
            this.lblEncoderAngleValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncoderAngleValue.Location = new System.Drawing.Point(460, 20);
            this.lblEncoderAngleValue.Name = "lblEncoderAngleValue";
            this.lblEncoderAngleValue.Size = new System.Drawing.Size(27, 13);
            this.lblEncoderAngleValue.TabIndex = 5;
            this.lblEncoderAngleValue.Text = "---°";
            //
            // lblEncoderAngle
            //
            this.lblEncoderAngle.AutoSize = true;
            this.lblEncoderAngle.Location = new System.Drawing.Point(360, 20);
            this.lblEncoderAngle.Name = "lblEncoderAngle";
            this.lblEncoderAngle.Size = new System.Drawing.Size(79, 13);
            this.lblEncoderAngle.TabIndex = 4;
            this.lblEncoderAngle.Text = "Encoder angle:";
            //
            // lblCurrentPosValue
            //
            this.lblCurrentPosValue.AutoSize = true;
            this.lblCurrentPosValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentPosValue.Location = new System.Drawing.Point(250, 20);
            this.lblCurrentPosValue.Name = "lblCurrentPosValue";
            this.lblCurrentPosValue.Size = new System.Drawing.Size(19, 13);
            this.lblCurrentPosValue.TabIndex = 3;
            this.lblCurrentPosValue.Text = "---";
            //
            // lblCurrentPos
            //
            this.lblCurrentPos.AutoSize = true;
            this.lblCurrentPos.Location = new System.Drawing.Point(160, 20);
            this.lblCurrentPos.Name = "lblCurrentPos";
            this.lblCurrentPos.Size = new System.Drawing.Size(88, 13);
            this.lblCurrentPos.TabIndex = 2;
            this.lblCurrentPos.Text = "Current position:";
            //
            // lblFilterCountValue
            //
            this.lblFilterCountValue.AutoSize = true;
            this.lblFilterCountValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterCountValue.Location = new System.Drawing.Point(90, 20);
            this.lblFilterCountValue.Name = "lblFilterCountValue";
            this.lblFilterCountValue.Size = new System.Drawing.Size(14, 13);
            this.lblFilterCountValue.TabIndex = 1;
            this.lblFilterCountValue.Text = "0";
            //
            // lblFilterCount
            //
            this.lblFilterCount.AutoSize = true;
            this.lblFilterCount.Location = new System.Drawing.Point(10, 20);
            this.lblFilterCount.Name = "lblFilterCount";
            this.lblFilterCount.Size = new System.Drawing.Size(63, 13);
            this.lblFilterCount.TabIndex = 0;
            this.lblFilterCount.Text = "Filter count:";
            //
            // groupBoxMovement
            //
            this.groupBoxMovement.Controls.Add(this.lblCustomSteps);
            this.groupBoxMovement.Controls.Add(this.nudCustomSteps);
            this.groupBoxMovement.Controls.Add(this.btnCustomBackward);
            this.groupBoxMovement.Controls.Add(this.btnCustomForward);
            this.groupBoxMovement.Controls.Add(this.lblForward);
            this.groupBoxMovement.Controls.Add(this.btnFwd100);
            this.groupBoxMovement.Controls.Add(this.btnFwd50);
            this.groupBoxMovement.Controls.Add(this.btnFwd10);
            this.groupBoxMovement.Controls.Add(this.btnFwd1);
            this.groupBoxMovement.Controls.Add(this.lblBackward);
            this.groupBoxMovement.Controls.Add(this.btnBack1);
            this.groupBoxMovement.Controls.Add(this.btnBack10);
            this.groupBoxMovement.Controls.Add(this.btnBack50);
            this.groupBoxMovement.Controls.Add(this.btnBack100);
            this.groupBoxMovement.Location = new System.Drawing.Point(3, 47);
            this.groupBoxMovement.Name = "groupBoxMovement";
            this.groupBoxMovement.Size = new System.Drawing.Size(540, 65);
            this.groupBoxMovement.TabIndex = 1;
            this.groupBoxMovement.TabStop = false;
            this.groupBoxMovement.Text = "Manual Movement Control";
            //
            // lblCustomSteps
            //
            this.lblCustomSteps.AutoSize = true;
            this.lblCustomSteps.Location = new System.Drawing.Point(270, 48);
            this.lblCustomSteps.Name = "lblCustomSteps";
            this.lblCustomSteps.Size = new System.Drawing.Size(73, 13);
            this.lblCustomSteps.TabIndex = 13;
            this.lblCustomSteps.Text = "Custom steps:";
            //
            // nudCustomSteps
            //
            this.nudCustomSteps.Location = new System.Drawing.Point(350, 46);
            this.nudCustomSteps.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.nudCustomSteps.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCustomSteps.Name = "nudCustomSteps";
            this.nudCustomSteps.Size = new System.Drawing.Size(60, 20);
            this.nudCustomSteps.TabIndex = 12;
            this.nudCustomSteps.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            //
            // btnCustomBackward
            //
            this.btnCustomBackward.Location = new System.Drawing.Point(10, 43);
            this.btnCustomBackward.Name = "btnCustomBackward";
            this.btnCustomBackward.Size = new System.Drawing.Size(80, 25);
            this.btnCustomBackward.TabIndex = 11;
            this.btnCustomBackward.Text = "◀ Backward";
            this.btnCustomBackward.UseVisualStyleBackColor = true;
            this.btnCustomBackward.Click += new System.EventHandler(this.BtnCustomBackward_Click);
            //
            // btnCustomForward
            //
            this.btnCustomForward.Location = new System.Drawing.Point(452, 43);
            this.btnCustomForward.Name = "btnCustomForward";
            this.btnCustomForward.Size = new System.Drawing.Size(80, 25);
            this.btnCustomForward.TabIndex = 10;
            this.btnCustomForward.Text = "Forward ▶";
            this.btnCustomForward.UseVisualStyleBackColor = true;
            this.btnCustomForward.Click += new System.EventHandler(this.BtnCustomForward_Click);
            //
            // lblForward
            //
            this.lblForward.AutoSize = true;
            this.lblForward.Location = new System.Drawing.Point(270, 20);
            this.lblForward.Name = "lblForward";
            this.lblForward.Size = new System.Drawing.Size(48, 13);
            this.lblForward.TabIndex = 9;
            this.lblForward.Text = "Forward:";
            //
            // btnFwd100
            //
            this.btnFwd100.Location = new System.Drawing.Point(466, 15);
            this.btnFwd100.Name = "btnFwd100";
            this.btnFwd100.Size = new System.Drawing.Size(60, 23);
            this.btnFwd100.TabIndex = 8;
            this.btnFwd100.Text = "+100";
            this.btnFwd100.UseVisualStyleBackColor = true;
            this.btnFwd100.Click += new System.EventHandler(this.BtnFwd100_Click);
            //
            // btnFwd50
            //
            this.btnFwd50.Location = new System.Drawing.Point(400, 15);
            this.btnFwd50.Name = "btnFwd50";
            this.btnFwd50.Size = new System.Drawing.Size(60, 23);
            this.btnFwd50.TabIndex = 7;
            this.btnFwd50.Text = "+50";
            this.btnFwd50.UseVisualStyleBackColor = true;
            this.btnFwd50.Click += new System.EventHandler(this.BtnFwd50_Click);
            //
            // btnFwd10
            //
            this.btnFwd10.Location = new System.Drawing.Point(334, 15);
            this.btnFwd10.Name = "btnFwd10";
            this.btnFwd10.Size = new System.Drawing.Size(60, 23);
            this.btnFwd10.TabIndex = 6;
            this.btnFwd10.Text = "+10";
            this.btnFwd10.UseVisualStyleBackColor = true;
            this.btnFwd10.Click += new System.EventHandler(this.BtnFwd10_Click);
            //
            // btnFwd1
            //
            this.btnFwd1.Location = new System.Drawing.Point(268, 15);
            this.btnFwd1.Name = "btnFwd1";
            this.btnFwd1.Size = new System.Drawing.Size(60, 23);
            this.btnFwd1.TabIndex = 5;
            this.btnFwd1.Text = "+1";
            this.btnFwd1.UseVisualStyleBackColor = true;
            this.btnFwd1.Click += new System.EventHandler(this.BtnFwd1_Click);
            //
            // lblBackward
            //
            this.lblBackward.AutoSize = true;
            this.lblBackward.Location = new System.Drawing.Point(10, 20);
            this.lblBackward.Name = "lblBackward";
            this.lblBackward.Size = new System.Drawing.Size(59, 13);
            this.lblBackward.TabIndex = 4;
            this.lblBackward.Text = "Backward:";
            //
            // btnBack1
            //
            this.btnBack1.Location = new System.Drawing.Point(202, 15);
            this.btnBack1.Name = "btnBack1";
            this.btnBack1.Size = new System.Drawing.Size(60, 23);
            this.btnBack1.TabIndex = 3;
            this.btnBack1.Text = "-1";
            this.btnBack1.UseVisualStyleBackColor = true;
            this.btnBack1.Click += new System.EventHandler(this.BtnBack1_Click);
            //
            // btnBack10
            //
            this.btnBack10.Location = new System.Drawing.Point(136, 15);
            this.btnBack10.Name = "btnBack10";
            this.btnBack10.Size = new System.Drawing.Size(60, 23);
            this.btnBack10.TabIndex = 2;
            this.btnBack10.Text = "-10";
            this.btnBack10.UseVisualStyleBackColor = true;
            this.btnBack10.Click += new System.EventHandler(this.BtnBack10_Click);
            //
            // btnBack50
            //
            this.btnBack50.Location = new System.Drawing.Point(70, 15);
            this.btnBack50.Name = "btnBack50";
            this.btnBack50.Size = new System.Drawing.Size(60, 23);
            this.btnBack50.TabIndex = 1;
            this.btnBack50.Text = "-50";
            this.btnBack50.UseVisualStyleBackColor = true;
            this.btnBack50.Click += new System.EventHandler(this.BtnBack50_Click);
            //
            // btnBack100
            //
            this.btnBack100.Location = new System.Drawing.Point(4, 15);
            this.btnBack100.Name = "btnBack100";
            this.btnBack100.Size = new System.Drawing.Size(60, 23);
            this.btnBack100.TabIndex = 0;
            this.btnBack100.Text = "-100";
            this.btnBack100.UseVisualStyleBackColor = true;
            this.btnBack100.Click += new System.EventHandler(this.BtnBack100_Click);
            //
            // groupBoxPositions
            //
            this.groupBoxPositions.Controls.Add(this.dgvPositions);
            this.groupBoxPositions.Controls.Add(this.pbProgress);
            this.groupBoxPositions.Controls.Add(this.lblProgress);
            this.groupBoxPositions.Location = new System.Drawing.Point(3, 116);
            this.groupBoxPositions.Name = "groupBoxPositions";
            this.groupBoxPositions.Size = new System.Drawing.Size(340, 238);
            this.groupBoxPositions.TabIndex = 2;
            this.groupBoxPositions.TabStop = false;
            this.groupBoxPositions.Text = "Position Calibration";
            //
            // dgvPositions
            //
            this.dgvPositions.AllowUserToAddRows = false;
            this.dgvPositions.AllowUserToDeleteRows = false;
            this.dgvPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPositions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colPosition,
            this.colFilterName,
            this.colAngle,
            this.colCalibrated,
            this.colAction});
            this.dgvPositions.Location = new System.Drawing.Point(6, 19);
            this.dgvPositions.Name = "dgvPositions";
            this.dgvPositions.RowHeadersVisible = false;
            this.dgvPositions.Size = new System.Drawing.Size(328, 182);
            this.dgvPositions.TabIndex = 0;
            this.dgvPositions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPositions_CellContentClick);
            //
            // colPosition
            //
            this.colPosition.HeaderText = "Pos";
            this.colPosition.Name = "colPosition";
            this.colPosition.ReadOnly = true;
            this.colPosition.Width = 35;
            //
            // colFilterName
            //
            this.colFilterName.HeaderText = "Filter";
            this.colFilterName.Name = "colFilterName";
            this.colFilterName.ReadOnly = true;
            this.colFilterName.Width = 80;
            //
            // colAngle
            //
            this.colAngle.HeaderText = "Current Angle";
            this.colAngle.Name = "colAngle";
            this.colAngle.ReadOnly = true;
            this.colAngle.Width = 110;
            //
            // colCalibrated
            //
            this.colCalibrated.HeaderText = "Cal.";
            this.colCalibrated.Name = "colCalibrated";
            this.colCalibrated.ReadOnly = true;
            this.colCalibrated.Width = 35;
            //
            // colAction
            //
            this.colAction.HeaderText = "Action";
            this.colAction.Name = "colAction";
            this.colAction.Width = 70;
            //
            // pbProgress
            //
            this.pbProgress.Location = new System.Drawing.Point(4, 221);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(220, 16);
            this.pbProgress.TabIndex = 2;
            //
            // lblProgress
            //
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(230, 222);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(90, 13);
            this.lblProgress.TabIndex = 1;
            this.lblProgress.Text = "0/0 calibrated (0%)";
            //
            // groupBoxActions
            //
            this.groupBoxActions.Controls.Add(this.btnRefreshAngles);
            this.groupBoxActions.Controls.Add(this.btnClearCalibration);
            this.groupBoxActions.Location = new System.Drawing.Point(349, 116);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(194, 60);
            this.groupBoxActions.TabIndex = 3;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Text = "Global Actions";
            //
            // btnRefreshAngles
            //
            this.btnRefreshAngles.Location = new System.Drawing.Point(10, 32);
            this.btnRefreshAngles.Name = "btnRefreshAngles";
            this.btnRefreshAngles.Size = new System.Drawing.Size(175, 23);
            this.btnRefreshAngles.TabIndex = 1;
            this.btnRefreshAngles.Text = "Refresh Angles";
            this.btnRefreshAngles.UseVisualStyleBackColor = true;
            this.btnRefreshAngles.Click += new System.EventHandler(this.BtnRefreshAngles_Click);
            //
            // btnClearCalibration
            //
            this.btnClearCalibration.Location = new System.Drawing.Point(10, 11);
            this.btnClearCalibration.Name = "btnClearCalibration";
            this.btnClearCalibration.Size = new System.Drawing.Size(175, 23);
            this.btnClearCalibration.TabIndex = 0;
            this.btnClearCalibration.Text = "Clear Calibration";
            this.btnClearCalibration.UseVisualStyleBackColor = true;
            this.btnClearCalibration.Click += new System.EventHandler(this.BtnClearCalibration_Click);
            //
            // CustomAnglesControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxActions);
            this.Controls.Add(this.groupBoxPositions);
            this.Controls.Add(this.groupBoxMovement);
            this.Controls.Add(this.groupBoxSystemInfo);
            this.Name = "CustomAnglesControl";
            this.Size = new System.Drawing.Size(560, 357);
            this.Load += new System.EventHandler(this.CustomAnglesControl_Load);
            this.groupBoxSystemInfo.ResumeLayout(false);
            this.groupBoxSystemInfo.PerformLayout();
            this.groupBoxMovement.ResumeLayout(false);
            this.groupBoxMovement.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCustomSteps)).EndInit();
            this.groupBoxPositions.ResumeLayout(false);
            this.groupBoxPositions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPositions)).EndInit();
            this.groupBoxActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSystemInfo;
        internal System.Windows.Forms.Label lblFilterCountValue;
        private System.Windows.Forms.Label lblFilterCount;
        internal System.Windows.Forms.Label lblCurrentPosValue;
        private System.Windows.Forms.Label lblCurrentPos;
        internal System.Windows.Forms.Label lblEncoderAngleValue;
        private System.Windows.Forms.Label lblEncoderAngle;
        private System.Windows.Forms.GroupBox groupBoxMovement;
        private System.Windows.Forms.Button btnBack100;
        private System.Windows.Forms.Button btnBack50;
        private System.Windows.Forms.Button btnBack10;
        private System.Windows.Forms.Button btnBack1;
        private System.Windows.Forms.Label lblBackward;
        private System.Windows.Forms.Button btnFwd1;
        private System.Windows.Forms.Button btnFwd10;
        private System.Windows.Forms.Button btnFwd50;
        private System.Windows.Forms.Button btnFwd100;
        private System.Windows.Forms.Label lblForward;
        private System.Windows.Forms.Button btnCustomForward;
        private System.Windows.Forms.Button btnCustomBackward;
        private System.Windows.Forms.NumericUpDown nudCustomSteps;
        private System.Windows.Forms.Label lblCustomSteps;
        private System.Windows.Forms.GroupBox groupBoxPositions;
        private System.Windows.Forms.DataGridView dgvPositions;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.Button btnClearCalibration;
        private System.Windows.Forms.Button btnRefreshAngles;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPosition;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFilterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAngle;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCalibrated;
        private System.Windows.Forms.DataGridViewButtonColumn colAction;
    }
}
