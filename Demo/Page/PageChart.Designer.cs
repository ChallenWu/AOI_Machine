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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_CT = new System.Windows.Forms.Label();
            this.lbl_UPH = new System.Windows.Forms.Label();
            this.lbl_PF = new System.Windows.Forms.Label();
            this.lbl_Yield = new System.Windows.Forms.Label();
            this.Lbl_IO = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Pa_Unit = new System.Windows.Forms.Panel();
            this.Lbl_Unit = new System.Windows.Forms.Label();
            this.dataEachUnit1 = new Demo.UserControls.DataEachUnit();
            this.StatusRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.btnExport = new System.Windows.Forms.Button();
            this.chartIO_Update1 = new Demo.UserControls.ChartIO_Update();
            this.chkRealTimeChart = new System.Windows.Forms.CheckBox();
            this.btn_Query = new System.Windows.Forms.Button();
            this.cbModel = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.Pa_Unit.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.lbl_CT);
            this.panel1.Controls.Add(this.lbl_UPH);
            this.panel1.Controls.Add(this.lbl_PF);
            this.panel1.Controls.Add(this.lbl_Yield);
            this.panel1.Controls.Add(this.Lbl_IO);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Pa_Unit);
            this.panel1.Location = new System.Drawing.Point(880, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 579);
            this.panel1.TabIndex = 5;
            // 
            // lbl_CT
            // 
            this.lbl_CT.AutoSize = true;
            this.lbl_CT.Location = new System.Drawing.Point(118, 333);
            this.lbl_CT.Name = "lbl_CT";
            this.lbl_CT.Size = new System.Drawing.Size(13, 13);
            this.lbl_CT.TabIndex = 6;
            this.lbl_CT.Text = "0";
            this.lbl_CT.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_UPH
            // 
            this.lbl_UPH.AutoSize = true;
            this.lbl_UPH.Location = new System.Drawing.Point(118, 291);
            this.lbl_UPH.Name = "lbl_UPH";
            this.lbl_UPH.Size = new System.Drawing.Size(13, 13);
            this.lbl_UPH.TabIndex = 6;
            this.lbl_UPH.Text = "0";
            this.lbl_UPH.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_PF
            // 
            this.lbl_PF.AutoSize = true;
            this.lbl_PF.Location = new System.Drawing.Point(115, 249);
            this.lbl_PF.Name = "lbl_PF";
            this.lbl_PF.Size = new System.Drawing.Size(24, 13);
            this.lbl_PF.TabIndex = 6;
            this.lbl_PF.Text = "0/0";
            this.lbl_PF.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbl_Yield
            // 
            this.lbl_Yield.AutoSize = true;
            this.lbl_Yield.Location = new System.Drawing.Point(115, 204);
            this.lbl_Yield.Name = "lbl_Yield";
            this.lbl_Yield.Size = new System.Drawing.Size(13, 13);
            this.lbl_Yield.TabIndex = 6;
            this.lbl_Yield.Text = "0";
            this.lbl_Yield.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Lbl_IO
            // 
            this.Lbl_IO.AutoSize = true;
            this.Lbl_IO.Location = new System.Drawing.Point(118, 163);
            this.Lbl_IO.Name = "Lbl_IO";
            this.Lbl_IO.Size = new System.Drawing.Size(24, 13);
            this.Lbl_IO.TabIndex = 6;
            this.Lbl_IO.Text = "0/0";
            this.Lbl_IO.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 333);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(24, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "CT:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 291);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "UPH:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Pass/Fail:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Yield:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Input/Output:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(49, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "I/O Summary";
            // 
            // Pa_Unit
            // 
            this.Pa_Unit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(249)))), ((int)(((byte)(0)))));
            this.Pa_Unit.Controls.Add(this.Lbl_Unit);
            this.Pa_Unit.Location = new System.Drawing.Point(12, 16);
            this.Pa_Unit.Name = "Pa_Unit";
            this.Pa_Unit.Size = new System.Drawing.Size(179, 86);
            this.Pa_Unit.TabIndex = 4;
            // 
            // Lbl_Unit
            // 
            this.Lbl_Unit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Lbl_Unit.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbl_Unit.Location = new System.Drawing.Point(0, 0);
            this.Lbl_Unit.Name = "Lbl_Unit";
            this.Lbl_Unit.Size = new System.Drawing.Size(179, 86);
            this.Lbl_Unit.TabIndex = 0;
            this.Lbl_Unit.Text = "FVMDXXXXXXX";
            this.Lbl_Unit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataEachUnit1
            // 
            this.dataEachUnit1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataEachUnit1.Location = new System.Drawing.Point(2, 467);
            this.dataEachUnit1.Name = "dataEachUnit1";
            this.dataEachUnit1.ShowColumn = null;
            this.dataEachUnit1.Size = new System.Drawing.Size(871, 192);
            this.dataEachUnit1.TabIndex = 6;
            // 
            // StatusRefreshTimer
            // 
            this.StatusRefreshTimer.Interval = 3000;
            this.StatusRefreshTimer.Tick += new System.EventHandler(this.StatusRefreshTimer_Tick);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(917, 611);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(124, 37);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click_1);
            // 
            // chartIO_Update1
            // 
            this.chartIO_Update1.DoubleBarCurrentTime = new System.DateTime(2024, 11, 11, 15, 32, 29, 208);
            this.chartIO_Update1.IsSelectDay = true;
            this.chartIO_Update1.IsSelectIO = true;
            this.chartIO_Update1.IsSelectTime = false;
            this.chartIO_Update1.Location = new System.Drawing.Point(3, 3);
            this.chartIO_Update1.Name = "chartIO_Update1";
            this.chartIO_Update1.RealTimeTracking = false;
            this.chartIO_Update1.Size = new System.Drawing.Size(871, 463);
            this.chartIO_Update1.TabIndex = 9;
            this.chartIO_Update1.TrackEndTime = new System.DateTime(2024, 8, 9, 10, 20, 52, 0);
            this.chartIO_Update1.TrackStartTime = new System.DateTime(2024, 8, 9, 10, 21, 0, 0);
            // 
            // chkRealTimeChart
            // 
            this.chkRealTimeChart.AutoSize = true;
            this.chkRealTimeChart.Checked = true;
            this.chkRealTimeChart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRealTimeChart.Location = new System.Drawing.Point(391, 12);
            this.chkRealTimeChart.Name = "chkRealTimeChart";
            this.chkRealTimeChart.Size = new System.Drawing.Size(67, 17);
            this.chkRealTimeChart.TabIndex = 10;
            this.chkRealTimeChart.Text = "Realtime";
            this.chkRealTimeChart.UseVisualStyleBackColor = true;
            // 
            // btn_Query
            // 
            this.btn_Query.Location = new System.Drawing.Point(460, 7);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(57, 24);
            this.btn_Query.TabIndex = 24;
            this.btn_Query.Text = "Query";
            this.btn_Query.UseVisualStyleBackColor = true;
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // cbModel
            // 
            this.cbModel.FormattingEnabled = true;
            this.cbModel.Items.AddRange(new object[] {
            "All Model"});
            this.cbModel.Location = new System.Drawing.Point(253, 10);
            this.cbModel.Name = "cbModel";
            this.cbModel.Size = new System.Drawing.Size(121, 21);
            this.cbModel.TabIndex = 25;
            // 
            // PageChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbModel);
            this.Controls.Add(this.btn_Query);
            this.Controls.Add(this.chkRealTimeChart);
            this.Controls.Add(this.chartIO_Update1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dataEachUnit1);
            this.Controls.Add(this.panel1);
            this.Name = "PageChart";
            this.Size = new System.Drawing.Size(1092, 667);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Pa_Unit.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_CT;
        private System.Windows.Forms.Label lbl_UPH;
        private System.Windows.Forms.Label lbl_PF;
        private System.Windows.Forms.Label lbl_Yield;
        private System.Windows.Forms.Label Lbl_IO;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Pa_Unit;
        private System.Windows.Forms.Label Lbl_Unit;
        private UserControls.DataEachUnit dataEachUnit1;
        private System.Windows.Forms.Timer StatusRefreshTimer;
        private System.Windows.Forms.Button btnExport;
        private UserControls.ChartIO_Update chartIO_Update1;
        private System.Windows.Forms.CheckBox chkRealTimeChart;
        private System.Windows.Forms.Button btn_Query;
        private System.Windows.Forms.ComboBox cbModel;
    }
}
