namespace Demo.Page
{
    partial class PageProduction
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
            this.components = new System.ComponentModel.Container();
            this.criticalPameterDashBoard1 = new Demo.UserControls.CriticalPameterDashBoard();
            this.machineStateChanges1 = new Demo.UserControls.MachineStateChanges();
            this.machineErrorStatistics1 = new Demo.UserControls.MachineErrorStatistics();
            this.StatusRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.iO_Summary = new Demo.IO_Summary();
            this.lbl_Vender = new System.Windows.Forms.Label();
            this.lbl_SW = new System.Windows.Forms.Label();
            this.lbl_MSP = new System.Windows.Forms.Label();
            this.lbl_MH = new System.Windows.Forms.Label();
            this.lbl_Site = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_STN = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnCCDStatus = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.pnICWStatus = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.pnMESStatus = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnPLCStatus = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenReport = new System.Windows.Forms.Button();
            this.btnOpenImage = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnCCDStatus.SuspendLayout();
            this.pnICWStatus.SuspendLayout();
            this.pnMESStatus.SuspendLayout();
            this.pnPLCStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // criticalPameterDashBoard1
            // 
            this.criticalPameterDashBoard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.criticalPameterDashBoard1.DataSourceTable = null;
            this.criticalPameterDashBoard1.Location = new System.Drawing.Point(749, 249);
            this.criticalPameterDashBoard1.Name = "criticalPameterDashBoard1";
            this.criticalPameterDashBoard1.ShowColumn = null;
            this.criticalPameterDashBoard1.Size = new System.Drawing.Size(340, 187);
            this.criticalPameterDashBoard1.TabIndex = 10;
            // 
            // machineStateChanges1
            // 
            this.machineStateChanges1.DataSourceTable = null;
            this.machineStateChanges1.Location = new System.Drawing.Point(0, 0);
            this.machineStateChanges1.Name = "machineStateChanges1";
            this.machineStateChanges1.Size = new System.Drawing.Size(743, 334);
            this.machineStateChanges1.TabIndex = 9;
            // 
            // machineErrorStatistics1
            // 
            this.machineErrorStatistics1.CheckDays = 7;
            this.machineErrorStatistics1.DataSourceTable = null;
            this.machineErrorStatistics1.DoubleBarCurrentTime = new System.DateTime(2024, 8, 10, 14, 46, 22, 856);
            this.machineErrorStatistics1.IsSelectTime = false;
            this.machineErrorStatistics1.Location = new System.Drawing.Point(0, 340);
            this.machineErrorStatistics1.Name = "machineErrorStatistics1";
            this.machineErrorStatistics1.Size = new System.Drawing.Size(743, 321);
            this.machineErrorStatistics1.TabIndex = 8;
            this.machineErrorStatistics1.TrackEndTime = new System.DateTime(2024, 8, 10, 14, 46, 22, 0);
            this.machineErrorStatistics1.TrackStartTime = new System.DateTime(2024, 8, 10, 14, 46, 22, 0);
            // 
            // StatusRefreshTimer
            // 
            this.StatusRefreshTimer.Interval = 3000;
            this.StatusRefreshTimer.Tick += new System.EventHandler(this.StatusRefreshTimer_Tick);
            // 
            // iO_Summary
            // 
            this.iO_Summary.CT = "0";
            this.iO_Summary.Input_Output = "0/0";
            this.iO_Summary.Location = new System.Drawing.Point(749, 0);
            this.iO_Summary.Name = "iO_Summary";
            this.iO_Summary.NGColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(93)))), ((int)(((byte)(87)))));
            this.iO_Summary.OKColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(249)))), ((int)(((byte)(0)))));
            this.iO_Summary.Pass_Fail = "0/0";
            this.iO_Summary.Size = new System.Drawing.Size(343, 243);
            this.iO_Summary.SN = "FVMDXXXXXXX";
            this.iO_Summary.TabIndex = 11;
            this.iO_Summary.UnitStatus = true;
            this.iO_Summary.UPH = "0";
            this.iO_Summary.Yield = "0";
            // 
            // lbl_Vender
            // 
            this.lbl_Vender.AutoSize = true;
            this.lbl_Vender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Vender.Location = new System.Drawing.Point(172, 30);
            this.lbl_Vender.Name = "lbl_Vender";
            this.lbl_Vender.Size = new System.Drawing.Size(28, 13);
            this.lbl_Vender.TabIndex = 2;
            this.lbl_Vender.Text = "AAA";
            this.lbl_Vender.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_SW
            // 
            this.lbl_SW.AutoSize = true;
            this.lbl_SW.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SW.Location = new System.Drawing.Point(93, 50);
            this.lbl_SW.Name = "lbl_SW";
            this.lbl_SW.Size = new System.Drawing.Size(154, 13);
            this.lbl_SW.TabIndex = 2;
            this.lbl_SW.Text = "AAA_X.X.X.X_YYMMDD_POR";
            this.lbl_SW.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_MSP
            // 
            this.lbl_MSP.AutoSize = true;
            this.lbl_MSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MSP.Location = new System.Drawing.Point(107, 70);
            this.lbl_MSP.Name = "lbl_MSP";
            this.lbl_MSP.Size = new System.Drawing.Size(120, 13);
            this.lbl_MSP.TabIndex = 2;
            this.lbl_MSP.Text = "C:/path/path/name.exe";
            this.lbl_MSP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_MH
            // 
            this.lbl_MH.AutoSize = true;
            this.lbl_MH.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MH.Location = new System.Drawing.Point(72, 90);
            this.lbl_MH.Name = "lbl_MH";
            this.lbl_MH.Size = new System.Drawing.Size(193, 13);
            this.lbl_MH.TabIndex = 2;
            this.lbl_MH.Text = "84f684f85f12a6f8aas1f68f4as4aw54da";
            this.lbl_MH.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Site
            // 
            this.lbl_Site.AutoSize = true;
            this.lbl_Site.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Site.Location = new System.Drawing.Point(51, 30);
            this.lbl_Site.Name = "lbl_Site";
            this.lbl_Site.Size = new System.Drawing.Size(35, 13);
            this.lbl_Site.TabIndex = 2;
            this.lbl_Site.Text = "XXXX";
            this.lbl_Site.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(121, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Vendor:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "SW Version:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(15, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Main SW Path:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "MS_Hash:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Site:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_STN
            // 
            this.lbl_STN.AutoSize = true;
            this.lbl_STN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_STN.Location = new System.Drawing.Point(121, 6);
            this.lbl_STN.Name = "lbl_STN";
            this.lbl_STN.Size = new System.Drawing.Size(74, 16);
            this.lbl_STN.TabIndex = 0;
            this.lbl_STN.Text = "STN01 #1";
            this.lbl_STN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.pnCCDStatus);
            this.panel1.Controls.Add(this.pnICWStatus);
            this.panel1.Controls.Add(this.pnMESStatus);
            this.panel1.Controls.Add(this.pnPLCStatus);
            this.panel1.Controls.Add(this.lbl_Vender);
            this.panel1.Controls.Add(this.lbl_SW);
            this.panel1.Controls.Add(this.lbl_MSP);
            this.panel1.Controls.Add(this.lbl_MH);
            this.panel1.Controls.Add(this.lbl_Site);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lbl_STN);
            this.panel1.Location = new System.Drawing.Point(751, 442);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 171);
            this.panel1.TabIndex = 12;
            // 
            // pnCCDStatus
            // 
            this.pnCCDStatus.BackColor = System.Drawing.Color.IndianRed;
            this.pnCCDStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnCCDStatus.Controls.Add(this.label7);
            this.pnCCDStatus.Location = new System.Drawing.Point(260, 122);
            this.pnCCDStatus.Name = "pnCCDStatus";
            this.pnCCDStatus.Size = new System.Drawing.Size(64, 28);
            this.pnCCDStatus.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 26);
            this.label7.TabIndex = 0;
            this.label7.Text = "CCD";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnICWStatus
            // 
            this.pnICWStatus.BackColor = System.Drawing.Color.IndianRed;
            this.pnICWStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnICWStatus.Controls.Add(this.label5);
            this.pnICWStatus.Location = new System.Drawing.Point(174, 122);
            this.pnICWStatus.Name = "pnICWStatus";
            this.pnICWStatus.Size = new System.Drawing.Size(64, 28);
            this.pnICWStatus.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 26);
            this.label5.TabIndex = 0;
            this.label5.Text = "ICW";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnMESStatus
            // 
            this.pnMESStatus.BackColor = System.Drawing.Color.IndianRed;
            this.pnMESStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnMESStatus.Controls.Add(this.label3);
            this.pnMESStatus.Location = new System.Drawing.Point(93, 122);
            this.pnMESStatus.Name = "pnMESStatus";
            this.pnMESStatus.Size = new System.Drawing.Size(64, 28);
            this.pnMESStatus.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "MES";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnPLCStatus
            // 
            this.pnPLCStatus.BackColor = System.Drawing.Color.IndianRed;
            this.pnPLCStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnPLCStatus.Controls.Add(this.label1);
            this.pnPLCStatus.Location = new System.Drawing.Point(15, 122);
            this.pnPLCStatus.Name = "pnPLCStatus";
            this.pnPLCStatus.Size = new System.Drawing.Size(64, 28);
            this.pnPLCStatus.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "PLC";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOpenReport
            // 
            this.btnOpenReport.AutoSize = true;
            this.btnOpenReport.Location = new System.Drawing.Point(805, 624);
            this.btnOpenReport.Name = "btnOpenReport";
            this.btnOpenReport.Size = new System.Drawing.Size(84, 31);
            this.btnOpenReport.TabIndex = 13;
            this.btnOpenReport.Text = "Open Report";
            this.btnOpenReport.UseVisualStyleBackColor = true;
            this.btnOpenReport.Click += new System.EventHandler(this.btnOpenReport_Click);
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.AutoSize = true;
            this.btnOpenImage.Location = new System.Drawing.Point(935, 624);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(81, 31);
            this.btnOpenImage.TabIndex = 14;
            this.btnOpenImage.Text = "Open Image";
            this.btnOpenImage.UseVisualStyleBackColor = true;
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // PageProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.btnOpenImage);
            this.Controls.Add(this.btnOpenReport);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.iO_Summary);
            this.Controls.Add(this.criticalPameterDashBoard1);
            this.Controls.Add(this.machineStateChanges1);
            this.Controls.Add(this.machineErrorStatistics1);
            this.Name = "PageProduction";
            this.Size = new System.Drawing.Size(1092, 667);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnCCDStatus.ResumeLayout(false);
            this.pnICWStatus.ResumeLayout(false);
            this.pnMESStatus.ResumeLayout(false);
            this.pnPLCStatus.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private UserControls.MachineErrorStatistics machineErrorStatistics1;
        private UserControls.MachineStateChanges machineStateChanges1;
        private UserControls.CriticalPameterDashBoard criticalPameterDashBoard1;
        private System.Windows.Forms.Timer StatusRefreshTimer;
        private IO_Summary iO_Summary;
        private System.Windows.Forms.Label lbl_Vender;
        private System.Windows.Forms.Label lbl_SW;
        private System.Windows.Forms.Label lbl_MSP;
        private System.Windows.Forms.Label lbl_MH;
        private System.Windows.Forms.Label lbl_Site;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_STN;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOpenReport;
        private System.Windows.Forms.Button btnOpenImage;
        private System.Windows.Forms.Panel pnPLCStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnCCDStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnICWStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnMESStatus;
        private System.Windows.Forms.Label label3;
    }
}
