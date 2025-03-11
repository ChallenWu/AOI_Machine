namespace Demo.UserControls
{
    partial class ChartIO_Update
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
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dateTimePicker1_2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1_1 = new System.Windows.Forms.DateTimePicker();
            this.btn_Hour = new System.Windows.Forms.Button();
            this.btn_Day = new System.Windows.Forms.Button();
            this.btn_Tossing = new System.Windows.Forms.Button();
            this.btn_IO = new System.Windows.Forms.Button();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1_2
            // 
            this.dateTimePicker1_2.CalendarFont = new System.Drawing.Font("SimSun", 8F);
            this.dateTimePicker1_2.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dateTimePicker1_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePicker1_2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1_2.Location = new System.Drawing.Point(715, 3);
            this.dateTimePicker1_2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dateTimePicker1_2.Name = "dateTimePicker1_2";
            this.dateTimePicker1_2.ShowUpDown = true;
            this.dateTimePicker1_2.Size = new System.Drawing.Size(148, 20);
            this.dateTimePicker1_2.TabIndex = 19;
            this.dateTimePicker1_2.Value = new System.DateTime(2024, 8, 9, 10, 20, 52, 0);
            // 
            // dateTimePicker1_1
            // 
            this.dateTimePicker1_1.CalendarFont = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker1_1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            this.dateTimePicker1_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePicker1_1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1_1.Location = new System.Drawing.Point(569, 2);
            this.dateTimePicker1_1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1_1.Name = "dateTimePicker1_1";
            this.dateTimePicker1_1.ShowUpDown = true;
            this.dateTimePicker1_1.Size = new System.Drawing.Size(142, 20);
            this.dateTimePicker1_1.TabIndex = 18;
            this.dateTimePicker1_1.Value = new System.DateTime(2024, 8, 9, 10, 21, 0, 0);
            // 
            // btn_Hour
            // 
            this.btn_Hour.AutoSize = true;
            this.btn_Hour.BackColor = System.Drawing.Color.LightGray;
            this.btn_Hour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Hour.Location = new System.Drawing.Point(263, 3);
            this.btn_Hour.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Hour.Name = "btn_Hour";
            this.btn_Hour.Size = new System.Drawing.Size(83, 23);
            this.btn_Hour.TabIndex = 17;
            this.btn_Hour.Text = "Hour";
            this.btn_Hour.UseVisualStyleBackColor = false;
            this.btn_Hour.Click += new System.EventHandler(this.btn_Hour_Click);
            // 
            // btn_Day
            // 
            this.btn_Day.AutoSize = true;
            this.btn_Day.BackColor = System.Drawing.Color.White;
            this.btn_Day.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Day.Location = new System.Drawing.Point(176, 3);
            this.btn_Day.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Day.Name = "btn_Day";
            this.btn_Day.Size = new System.Drawing.Size(83, 23);
            this.btn_Day.TabIndex = 16;
            this.btn_Day.Text = "Day";
            this.btn_Day.UseVisualStyleBackColor = false;
            this.btn_Day.Click += new System.EventHandler(this.btn_Day_Click);
            // 
            // btn_Tossing
            // 
            this.btn_Tossing.AutoSize = true;
            this.btn_Tossing.BackColor = System.Drawing.Color.LightGray;
            this.btn_Tossing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Tossing.Location = new System.Drawing.Point(89, 3);
            this.btn_Tossing.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Tossing.Name = "btn_Tossing";
            this.btn_Tossing.Size = new System.Drawing.Size(83, 23);
            this.btn_Tossing.TabIndex = 15;
            this.btn_Tossing.Text = "Chart";
            this.btn_Tossing.UseVisualStyleBackColor = false;
            this.btn_Tossing.Click += new System.EventHandler(this.btn_Tossing_Click);
            // 
            // btn_IO
            // 
            this.btn_IO.AutoSize = true;
            this.btn_IO.BackColor = System.Drawing.Color.White;
            this.btn_IO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_IO.Location = new System.Drawing.Point(2, 3);
            this.btn_IO.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_IO.Name = "btn_IO";
            this.btn_IO.Size = new System.Drawing.Size(83, 23);
            this.btn_IO.TabIndex = 14;
            this.btn_IO.Text = "Line";
            this.btn_IO.UseVisualStyleBackColor = false;
            this.btn_IO.Click += new System.EventHandler(this.btn_IO_Click);
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelAutoFitMinFontSize = 5;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.BackColor = System.Drawing.Color.LightGray;
            chartArea1.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea1);
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.BackColor = System.Drawing.Color.LightGray;
            legend1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            legend1.TitleBackColor = System.Drawing.Color.Black;
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(2, 252);
            this.chart2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chart2.Name = "chart2";
            this.chart2.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Chartreuse,
        System.Drawing.Color.OrangeRed,
        System.Drawing.Color.DeepSkyBlue,
        System.Drawing.Color.Gray};
            series1.ChartArea = "ChartArea1";
            series1.CustomProperties = "StackedGroupName=Group1";
            series1.IsValueShownAsLabel = true;
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "OK";
            series1.SmartLabelStyle.MovingDirection = ((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles)((((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Top | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.TopRight) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomLeft) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomRight)));
            series2.ChartArea = "ChartArea1";
            series2.CustomProperties = "StackedGroupName=Group1";
            series2.IsValueShownAsLabel = true;
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend1";
            series2.Name = "NG";
            series2.SmartLabelStyle.MovingDirection = ((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles)((((System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Top | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.TopRight) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomLeft) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.BottomRight)));
            this.chart2.Series.Add(series1);
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(867, 208);
            this.chart2.TabIndex = 13;
            this.chart2.Text = "chart2";
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.Interval = 1D;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelAutoFitMinFontSize = 5;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            chartArea2.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY.MajorTickMark.Enabled = false;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.BackColor = System.Drawing.Color.LightGray;
            chartArea2.Name = "ChartArea1";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 85F;
            chartArea2.Position.Width = 94F;
            chartArea2.Position.X = 3F;
            chartArea2.Position.Y = 15F;
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.BackColor = System.Drawing.Color.LightGray;
            legend2.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            legend2.DockedToChartArea = "ChartArea1";
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Name = "Legend1";
            legend2.Position.Auto = false;
            legend2.Position.Height = 11.64659F;
            legend2.Position.Width = 26.8797F;
            legend2.Position.X = 14F;
            legend2.Position.Y = 4F;
            legend2.TitleBackColor = System.Drawing.Color.Black;
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(2, 38);
            this.chart1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.IsValueShownAsLabel = true;
            series3.Legend = "Legend1";
            series3.MarkerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            series3.MarkerColor = System.Drawing.Color.White;
            series3.MarkerSize = 7;
            series3.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series3.Name = "DS Output_CT";
            series4.BorderColor = System.Drawing.Color.White;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Gray;
            series4.IsValueShownAsLabel = true;
            series4.Legend = "Legend1";
            series4.MarkerBorderColor = System.Drawing.Color.Gray;
            series4.MarkerColor = System.Drawing.Color.White;
            series4.MarkerSize = 7;
            series4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series4.Name = "NS Output_CT";
            this.chart1.Series.Add(series3);
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(867, 208);
            this.chart1.TabIndex = 12;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_DoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.chart2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.chart1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(871, 463);
            this.tableLayoutPanel1.TabIndex = 24;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 7;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.16949F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.16949F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.16949F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.16949F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.42373F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.94915F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.94915F));
            this.tableLayoutPanel4.Controls.Add(this.btn_IO, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Tossing, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Day, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.dateTimePicker1_2, 6, 0);
            this.tableLayoutPanel4.Controls.Add(this.btn_Hour, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.dateTimePicker1_1, 5, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(865, 29);
            this.tableLayoutPanel4.TabIndex = 15;
            // 
            // ChartIO_Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ChartIO_Update";
            this.Size = new System.Drawing.Size(871, 463);
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePicker1_2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1_1;
        private System.Windows.Forms.Button btn_Hour;
        private System.Windows.Forms.Button btn_Day;
        private System.Windows.Forms.Button btn_Tossing;
        private System.Windows.Forms.Button btn_IO;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}
