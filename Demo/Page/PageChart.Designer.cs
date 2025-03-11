namespace Demo.Page
{
    partial class PageChart
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
            this.StatusRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.btn_Query = new System.Windows.Forms.Button();
            this.cbModel = new System.Windows.Forms.ComboBox();
            this.chkShift = new System.Windows.Forms.CheckBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.chkRealTime = new System.Windows.Forms.CheckBox();
            this.dataEachUnit1 = new Demo.UserControls.DataEachUnit();
            this.chartIO_Update1 = new Demo.UserControls.ChartIO_Update();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusRefreshTimer
            // 
            this.StatusRefreshTimer.Interval = 15000;
            this.StatusRefreshTimer.Tick += new System.EventHandler(this.StatusRefreshTimer_Tick);
            // 
            // btn_Query
            // 
            this.btn_Query.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btn_Query.Location = new System.Drawing.Point(133, 3);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(76, 25);
            this.btn_Query.TabIndex = 24;
            this.btn_Query.Text = "Query";
            this.btn_Query.UseVisualStyleBackColor = true;
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // cbModel
            // 
            this.cbModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbModel.FormattingEnabled = true;
            this.cbModel.Items.AddRange(new object[] {
            "All Model"});
            this.cbModel.Location = new System.Drawing.Point(3, 4);
            this.cbModel.Name = "cbModel";
            this.cbModel.Size = new System.Drawing.Size(121, 24);
            this.cbModel.TabIndex = 25;
            // 
            // chkShift
            // 
            this.chkShift.AutoSize = true;
            this.chkShift.Checked = true;
            this.chkShift.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.chkShift.Location = new System.Drawing.Point(4, 73);
            this.chkShift.Name = "chkShift";
            this.chkShift.Size = new System.Drawing.Size(123, 22);
            this.chkShift.TabIndex = 26;
            this.chkShift.Text = "All Day or Shift";
            this.chkShift.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnExport.Location = new System.Drawing.Point(41, 110);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(124, 37);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click_1);
            // 
            // chkRealTime
            // 
            this.chkRealTime.AutoSize = true;
            this.chkRealTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.chkRealTime.Location = new System.Drawing.Point(4, 36);
            this.chkRealTime.Name = "chkRealTime";
            this.chkRealTime.Size = new System.Drawing.Size(85, 22);
            this.chkRealTime.TabIndex = 10;
            this.chkRealTime.Text = "Realtime";
            this.chkRealTime.UseVisualStyleBackColor = true;
            // 
            // dataEachUnit1
            // 
            this.dataEachUnit1.AutoSize = true;
            this.dataEachUnit1.BackColor = System.Drawing.SystemColors.Control;
            this.dataEachUnit1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataEachUnit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataEachUnit1.Location = new System.Drawing.Point(3, 465);
            this.dataEachUnit1.Name = "dataEachUnit1";
            this.dataEachUnit1.ShowColumn = null;
            this.dataEachUnit1.Size = new System.Drawing.Size(867, 199);
            this.dataEachUnit1.TabIndex = 6;
            // 
            // chartIO_Update1
            // 
            this.chartIO_Update1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chartIO_Update1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartIO_Update1.DoubleBarCurrentTime = new System.DateTime(2024, 11, 11, 15, 32, 29, 208);
            this.chartIO_Update1.IsSelectDay = true;
            this.chartIO_Update1.IsSelectIO = true;
            this.chartIO_Update1.IsSelectTime = false;
            this.chartIO_Update1.Location = new System.Drawing.Point(3, 3);
            this.chartIO_Update1.Name = "chartIO_Update1";
            this.chartIO_Update1.RealTimeTracking = false;
            this.chartIO_Update1.Size = new System.Drawing.Size(867, 456);
            this.chartIO_Update1.TabIndex = 9;
            this.chartIO_Update1.TrackEndTime = new System.DateTime(2024, 8, 9, 10, 20, 52, 0);
            this.chartIO_Update1.TrackStartTime = new System.DateTime(2024, 8, 9, 10, 21, 0, 0);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_Query);
            this.panel2.Controls.Add(this.chkShift);
            this.panel2.Controls.Add(this.cbModel);
            this.panel2.Controls.Add(this.btnExport);
            this.panel2.Controls.Add(this.chkRealTime);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(876, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(213, 456);
            this.panel2.TabIndex = 27;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.94505F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.05494F));
            this.tableLayoutPanel1.Controls.Add(this.chartIO_Update1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataEachUnit1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.26537F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.73463F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1092, 667);
            this.tableLayoutPanel1.TabIndex = 28;
            // 
            // PageChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PageChart";
            this.Size = new System.Drawing.Size(1092, 667);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private UserControls.DataEachUnit dataEachUnit1;
        private System.Windows.Forms.Timer StatusRefreshTimer;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.CheckBox chkRealTime;
        private System.Windows.Forms.CheckBox chkShift;
        internal UserControls.ChartIO_Update chartIO_Update1;
        private System.Windows.Forms.ComboBox cbModel;
        private System.Windows.Forms.Button btn_Query;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
