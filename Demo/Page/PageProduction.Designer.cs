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
            this.stn1 = new Demo.UserControls.STN();
            this.SuspendLayout();
            // 
            // criticalPameterDashBoard1
            // 
            this.criticalPameterDashBoard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.criticalPameterDashBoard1.DataSourceTable = null;
            this.criticalPameterDashBoard1.Location = new System.Drawing.Point(749, 249);
            this.criticalPameterDashBoard1.Name = "criticalPameterDashBoard1";
            this.criticalPameterDashBoard1.ShowColumn = null;
            this.criticalPameterDashBoard1.Size = new System.Drawing.Size(340, 193);
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
            this.machineErrorStatistics1.Location = new System.Drawing.Point(0, 343);
            this.machineErrorStatistics1.Name = "machineErrorStatistics1";
            this.machineErrorStatistics1.Size = new System.Drawing.Size(743, 321);
            this.machineErrorStatistics1.TabIndex = 8;
            this.machineErrorStatistics1.TrackEndTime = new System.DateTime(2024, 8, 10, 14, 46, 22, 0);
            this.machineErrorStatistics1.TrackStartTime = new System.DateTime(2024, 8, 3, 14, 46, 22, 0);
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
            // stn1
            // 
            this.stn1.HIVEConnected = false;
            this.stn1.Location = new System.Drawing.Point(749, 448);
            this.stn1.Main_SW_Path = null;
            this.stn1.MESConnected = false;
            this.stn1.MS_Hash = null;
            this.stn1.Name = "stn1";
            this.stn1.NewBackColor = System.Drawing.Color.LightGray;
            this.stn1.PDCAConnected = false;
            this.stn1.SiteName = null;
            this.stn1.Size = new System.Drawing.Size(338, 216);
            this.stn1.STNName = "";
            this.stn1.SW_Version = "";
            this.stn1.TabIndex = 12;
            this.stn1.Vender = null;
            // 
            // PageProduction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.stn1);
            this.Controls.Add(this.iO_Summary);
            this.Controls.Add(this.criticalPameterDashBoard1);
            this.Controls.Add(this.machineStateChanges1);
            this.Controls.Add(this.machineErrorStatistics1);
            this.Name = "PageProduction";
            this.Size = new System.Drawing.Size(1092, 667);
            this.ResumeLayout(false);

        }

        #endregion
        private UserControls.MachineErrorStatistics machineErrorStatistics1;
        private UserControls.MachineStateChanges machineStateChanges1;
        private UserControls.CriticalPameterDashBoard criticalPameterDashBoard1;
        private System.Windows.Forms.Timer StatusRefreshTimer;
        private IO_Summary iO_Summary;
        private UserControls.STN stn1;
    }
}
