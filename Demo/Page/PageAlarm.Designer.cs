namespace Demo.Page
{
    partial class PageAlarm
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
            this.alarmLogShow1 = new BoTech.AlarmLogShow();
            this.alarm_Duration1 = new BoTech.Alarm_Duration();
            this.dT_Statiistic1 = new BoTech.DT_Statiistic();
            this.dTP_StartTime = new System.Windows.Forms.DateTimePicker();
            this.dTP_EndTime = new System.Windows.Forms.DateTimePicker();
            this.btn_Query = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.TriggerAlarm = new System.Windows.Forms.Button();
            this.cb_Alarm = new System.Windows.Forms.ComboBox();
            this.btnCloseAlarm = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusRefreshTimer
            // 
            this.StatusRefreshTimer.Interval = 3000;
            this.StatusRefreshTimer.Tick += new System.EventHandler(this.StatusRefreshTimer_Tick);
            // 
            // alarmLogShow1
            // 
            this.alarmLogShow1.AutoSize = true;
            this.alarmLogShow1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.alarmLogShow1.DataSourceTable = null;
            this.alarmLogShow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alarmLogShow1.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alarmLogShow1.Location = new System.Drawing.Point(4, 370);
            this.alarmLogShow1.Margin = new System.Windows.Forms.Padding(4);
            this.alarmLogShow1.Name = "alarmLogShow1";
            this.alarmLogShow1.ShowColumn = null;
            this.alarmLogShow1.Size = new System.Drawing.Size(1084, 293);
            this.alarmLogShow1.TabIndex = 15;
            // 
            // alarm_Duration1
            // 
            this.alarm_Duration1.AD_Label = null;
            this.alarm_Duration1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.alarm_Duration1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alarm_Duration1.ColumnColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(127)))), ((int)(((byte)(126)))));
            this.alarm_Duration1.DataSourceTable = null;
            this.alarm_Duration1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alarm_Duration1.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alarm_Duration1.Location = new System.Drawing.Point(547, 4);
            this.alarm_Duration1.Margin = new System.Windows.Forms.Padding(4);
            this.alarm_Duration1.Name = "alarm_Duration1";
            this.alarm_Duration1.Size = new System.Drawing.Size(535, 319);
            this.alarm_Duration1.TabIndex = 14;
            // 
            // dT_Statiistic1
            // 
            this.dT_Statiistic1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.dT_Statiistic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dT_Statiistic1.DataSourceTable = null;
            this.dT_Statiistic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dT_Statiistic1.DT_Label = null;
            this.dT_Statiistic1.ErrorMessage = null;
            this.dT_Statiistic1.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dT_Statiistic1.Location = new System.Drawing.Point(4, 4);
            this.dT_Statiistic1.Margin = new System.Windows.Forms.Padding(4);
            this.dT_Statiistic1.Name = "dT_Statiistic1";
            this.dT_Statiistic1.PieStyle = BoTech.PieStyles.Time;
            this.dT_Statiistic1.Size = new System.Drawing.Size(535, 319);
            this.dT_Statiistic1.TabIndex = 13;
            this.dT_Statiistic1.Top10 = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(121)))), ((int)(((byte)(128))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(151)))), ((int)(((byte)(193))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(172)))), ((int)(((byte)(122))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(195)))), ((int)(((byte)(92))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(150)))), ((int)(((byte)(91))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(94)))), ((int)(((byte)(105))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(114)))), ((int)(((byte)(187))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(170)))), ((int)(((byte)(233))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(217)))), ((int)(((byte)(56))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(72)))), ((int)(((byte)(134)))))};
            // 
            // dTP_StartTime
            // 
            this.dTP_StartTime.CustomFormat = "yyyy/MM/dd-HH:mm:ss";
            this.dTP_StartTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dTP_StartTime.Font = new System.Drawing.Font("SimSun", 10F);
            this.dTP_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTP_StartTime.Location = new System.Drawing.Point(93, 3);
            this.dTP_StartTime.Name = "dTP_StartTime";
            this.dTP_StartTime.ShowUpDown = true;
            this.dTP_StartTime.Size = new System.Drawing.Size(182, 23);
            this.dTP_StartTime.TabIndex = 18;
            // 
            // dTP_EndTime
            // 
            this.dTP_EndTime.CustomFormat = "yyyy/MM/dd-HH:mm:ss";
            this.dTP_EndTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dTP_EndTime.Font = new System.Drawing.Font("SimSun", 10F);
            this.dTP_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTP_EndTime.Location = new System.Drawing.Point(357, 3);
            this.dTP_EndTime.Name = "dTP_EndTime";
            this.dTP_EndTime.ShowUpDown = true;
            this.dTP_EndTime.Size = new System.Drawing.Size(193, 23);
            this.dTP_EndTime.TabIndex = 19;
            this.dTP_EndTime.ValueChanged += new System.EventHandler(this.dTP_EndTime_ValueChanged);
            // 
            // btn_Query
            // 
            this.btn_Query.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Query.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Query.Location = new System.Drawing.Point(556, 3);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(71, 21);
            this.btn_Query.TabIndex = 23;
            this.btn_Query.Text = "Query";
            this.btn_Query.UseVisualStyleBackColor = true;
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(281, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 27);
            this.label2.TabIndex = 16;
            this.label2.Text = "End Time:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Save
            // 
            this.btn_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Save.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Save.Location = new System.Drawing.Point(633, 3);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(70, 21);
            this.btn_Save.TabIndex = 24;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click_1);
            // 
            // TriggerAlarm
            // 
            this.TriggerAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TriggerAlarm.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TriggerAlarm.Location = new System.Drawing.Point(848, 3);
            this.TriggerAlarm.Name = "TriggerAlarm";
            this.TriggerAlarm.Size = new System.Drawing.Size(112, 21);
            this.TriggerAlarm.TabIndex = 21;
            this.TriggerAlarm.Text = "TriggerAlarm";
            this.TriggerAlarm.UseVisualStyleBackColor = true;
            this.TriggerAlarm.Click += new System.EventHandler(this.TriggerAlarm_Click);
            // 
            // cb_Alarm
            // 
            this.cb_Alarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cb_Alarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cb_Alarm.FormattingEnabled = true;
            this.cb_Alarm.Location = new System.Drawing.Point(709, 3);
            this.cb_Alarm.Name = "cb_Alarm";
            this.cb_Alarm.Size = new System.Drawing.Size(133, 24);
            this.cb_Alarm.TabIndex = 22;
            // 
            // btnCloseAlarm
            // 
            this.btnCloseAlarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCloseAlarm.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCloseAlarm.Location = new System.Drawing.Point(966, 3);
            this.btnCloseAlarm.Name = "btnCloseAlarm";
            this.btnCloseAlarm.Size = new System.Drawing.Size(117, 21);
            this.btnCloseAlarm.TabIndex = 20;
            this.btnCloseAlarm.Text = "Close All";
            this.btnCloseAlarm.UseVisualStyleBackColor = true;
            this.btnCloseAlarm.Click += new System.EventHandler(this.btnCloseAlarm_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("SimSun", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 27);
            this.label1.TabIndex = 17;
            this.label1.Text = "Start Time:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.alarmLogShow1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1092, 667);
            this.tableLayoutPanel1.TabIndex = 26;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.dT_Statiistic1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.alarm_Duration1, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 36);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1086, 327);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 10;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel3.Controls.Add(this.btnCloseAlarm, 9, 0);
            this.tableLayoutPanel3.Controls.Add(this.dTP_StartTime, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.TriggerAlarm, 8, 0);
            this.tableLayoutPanel3.Controls.Add(this.dTP_EndTime, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.cb_Alarm, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_Query, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_Save, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1086, 27);
            this.tableLayoutPanel3.TabIndex = 26;
            // 
            // PageAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PageAlarm";
            this.Size = new System.Drawing.Size(1092, 667);
            this.Load += new System.EventHandler(this.PageAlarm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private BoTech.AlarmLogShow alarmLogShow1;
        private BoTech.Alarm_Duration alarm_Duration1;
        private BoTech.DT_Statiistic dT_Statiistic1;
        private System.Windows.Forms.Timer StatusRefreshTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCloseAlarm;
        private System.Windows.Forms.ComboBox cb_Alarm;
        private System.Windows.Forms.Button TriggerAlarm;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Query;
        private System.Windows.Forms.DateTimePicker dTP_EndTime;
        private System.Windows.Forms.DateTimePicker dTP_StartTime;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}
