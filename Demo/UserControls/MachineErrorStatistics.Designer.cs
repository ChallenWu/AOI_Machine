namespace Demo.UserControls
{
    partial class MachineErrorStatistics
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.doubleTrackBar1 = new UCTest.DoubleTrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX.MajorTickMark.TickMarkStyle = System.Windows.Forms.DataVisualization.Charting.TickMarkStyle.None;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.BackColor = System.Drawing.Color.LightGray;
            chartArea1.CursorX.LineColor = System.Drawing.Color.Empty;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            series1.BorderColor = System.Drawing.Color.LightGray;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series1.Color = System.Drawing.Color.Tomato;
            series1.IsValueShownAsLabel = true;
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(737, 330);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            title1.Alignment = System.Drawing.ContentAlignment.TopLeft;
            title1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.Name = "Title1";
            title1.Text = "Machine Error Statistics(Top10)";
            this.chart1.Titles.Add(title1);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CalendarFont = new System.Drawing.Font("SimSun", 8F);
            this.dateTimePicker2.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(495, 3);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowUpDown = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(151, 20);
            this.dateTimePicker2.TabIndex = 8;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("SimSun", 8F);
            this.dateTimePicker1.CustomFormat = "MM/dd/yyyy HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(341, 3);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(151, 20);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // doubleTrackBar1
            // 
            this.doubleTrackBar1.AutoSize = false;
            this.doubleTrackBar1.BackColor = System.Drawing.Color.LightGray;
            this.doubleTrackBar1.CheckDays = 7;
            this.doubleTrackBar1.ControlHeight = 10;
            this.doubleTrackBar1.DataTableNewestTime = "11/13/2024 16:43:44";
            this.doubleTrackBar1.DateEndValue2 = new System.DateTime(2024, 11, 13, 16, 43, 44, 0);
            this.doubleTrackBar1.DateStartValue1 = new System.DateTime(2024, 11, 6, 16, 43, 44, 0);
            this.doubleTrackBar1.IsSlider2Enable = true;
            this.doubleTrackBar1.LabelPlaces = ((uint)(1u));
            this.doubleTrackBar1.Location = new System.Drawing.Point(341, 30);
            this.doubleTrackBar1.MinimumSize = new System.Drawing.Size(1, 10);
            this.doubleTrackBar1.Name = "doubleTrackBar1";
            this.doubleTrackBar1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.doubleTrackBar1.SelectTrackColor = System.Drawing.Color.DarkGray;
            this.doubleTrackBar1.Size = new System.Drawing.Size(305, 23);
            this.doubleTrackBar1.TabIndex = 9;
            this.doubleTrackBar1.Text = "doubleTrackBar1";
            this.doubleTrackBar1.TickColor = System.Drawing.Color.Black;
            this.doubleTrackBar1.TickCount = 5;
            this.doubleTrackBar1.TickLabelVisible = false;
            this.doubleTrackBar1.TrackBarFontSize = 8;
            this.doubleTrackBar1.TrackButtonClickColor = System.Drawing.Color.Green;
            this.doubleTrackBar1.TrackButtonColor1 = System.Drawing.Color.LightGray;
            this.doubleTrackBar1.TrackButtonColor2 = System.Drawing.Color.LightGray;
            this.doubleTrackBar1.TrackButtonSize = new System.Drawing.Size(12, 12);
            this.doubleTrackBar1.TrackColor = System.Drawing.Color.White;
            this.doubleTrackBar1.TrackSelectedMode = UCTest.DoubleTrackBar.emTrackBarSelectedMode.Inner;
            this.doubleTrackBar1.Value1 = 0D;
            this.doubleTrackBar1.Value2 = 10000D;
            // 
            // MachineErrorStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.doubleTrackBar1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.chart1);
            this.Name = "MachineErrorStatistics";
            this.Size = new System.Drawing.Size(737, 330);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private UCTest.DoubleTrackBar doubleTrackBar1;
    }
}
