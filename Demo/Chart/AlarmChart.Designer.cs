namespace Demo.Chart
{
    partial class AlarmChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.RbtnCode = new System.Windows.Forms.RadioButton();
            this.RBtnCateg = new System.Windows.Forms.RadioButton();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.chart1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(275, 432);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.RbtnCode);
            this.panel1.Controls.Add(this.RBtnCateg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 360);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 69);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(101, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 41);
            this.button1.TabIndex = 23;
            this.button1.Text = "重置";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // RbtnCode
            // 
            this.RbtnCode.Appearance = System.Windows.Forms.Appearance.Button;
            this.RbtnCode.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RbtnCode.FlatAppearance.BorderSize = 0;
            this.RbtnCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RbtnCode.Location = new System.Drawing.Point(10, 5);
            this.RbtnCode.Name = "RbtnCode";
            this.RbtnCode.Size = new System.Drawing.Size(80, 59);
            this.RbtnCode.TabIndex = 21;
            this.RbtnCode.TabStop = true;
            this.RbtnCode.Text = "Code";
            this.RbtnCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RbtnCode.UseVisualStyleBackColor = false;
            // 
            // RBtnCateg
            // 
            this.RBtnCateg.Appearance = System.Windows.Forms.Appearance.Button;
            this.RBtnCateg.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.RBtnCateg.FlatAppearance.BorderSize = 0;
            this.RBtnCateg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RBtnCateg.Location = new System.Drawing.Point(173, 5);
            this.RBtnCateg.Name = "RBtnCateg";
            this.RBtnCateg.Size = new System.Drawing.Size(80, 59);
            this.RBtnCateg.TabIndex = 22;
            this.RBtnCateg.TabStop = true;
            this.RBtnCateg.Text = "Category";
            this.RBtnCateg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RBtnCateg.UseVisualStyleBackColor = false;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chart1.BorderlineColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            this.chart1.BorderlineWidth = 0;
            this.chart1.BorderSkin.SkinStyle = System.Windows.Forms.DataVisualization.Charting.BorderSkinStyle.Emboss;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("SimSun", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX2.MajorGrid.Enabled = false;
            chartArea1.AxisX2.MajorTickMark.Enabled = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("SimSun", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.AxisY2.MajorTickMark.Enabled = false;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.Name = "Area1";
            chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.IsSoftShadows = false;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Font = new System.Drawing.Font("SimSun", 8F, System.Drawing.FontStyle.Bold);
            legend1.IsEquallySpacedItems = true;
            legend1.IsTextAutoFit = false;
            legend1.Name = "Default";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            series1.ChartArea = "Area1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(65)))), ((int)(((byte)(140)))), ((int)(((byte)(240)))));
            series1.Font = new System.Drawing.Font("SimSun", 8.25F, System.Drawing.FontStyle.Bold);
            series1.Label = "#PERCENT{P1}";
            series1.Legend = "Default";
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "Series1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(269, 351);
            this.chart1.TabIndex = 3;
            // 
            // AlarmChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AlarmChart";
            this.Size = new System.Drawing.Size(275, 432);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton RbtnCode;
        private System.Windows.Forms.RadioButton RBtnCateg;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}
