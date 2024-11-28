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
            this.label2 = new System.Windows.Forms.Label();
            this.dTP_StartTime = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Query = new System.Windows.Forms.Button();
            this.cb_Alarm = new System.Windows.Forms.ComboBox();
            this.btnCloseAlarm = new System.Windows.Forms.Button();
            this.TriggerAlarm = new System.Windows.Forms.Button();
            this.dTP_EndTime = new System.Windows.Forms.DateTimePicker();
            this.alarmLogShow1 = new BoTech.AlarmLogShow();
            this.alarm_Duration1 = new BoTech.Alarm_Duration();
            this.dT_Statiistic1 = new BoTech.DT_Statiistic();
            this.xStationStateBar1 = new XCore.XStationStateBar();
            this.SuspendLayout();
            // 
            // StatusRefreshTimer
            // 
            this.StatusRefreshTimer.Interval = 3000;
            this.StatusRefreshTimer.Tick += new System.EventHandler(this.StatusRefreshTimer_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(297, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "End Time:";
            // 
            // dTP_StartTime
            // 
            this.dTP_StartTime.CustomFormat = "yyyy/MM/dd-HH:mm:ss";
            this.dTP_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTP_StartTime.Location = new System.Drawing.Point(138, 6);
            this.dTP_StartTime.Name = "dTP_StartTime";
            this.dTP_StartTime.ShowUpDown = true;
            this.dTP_StartTime.Size = new System.Drawing.Size(152, 20);
            this.dTP_StartTime.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(36, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Start Time:";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(619, 5);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(57, 25);
            this.btn_Save.TabIndex = 24;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click_1);
            // 
            // btn_Query
            // 
            this.btn_Query.Location = new System.Drawing.Point(555, 5);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(57, 25);
            this.btn_Query.TabIndex = 23;
            this.btn_Query.Text = "Query";
            this.btn_Query.UseVisualStyleBackColor = true;
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // cb_Alarm
            // 
            this.cb_Alarm.FormattingEnabled = true;
            this.cb_Alarm.Location = new System.Drawing.Point(735, 8);
            this.cb_Alarm.Name = "cb_Alarm";
            this.cb_Alarm.Size = new System.Drawing.Size(135, 21);
            this.cb_Alarm.TabIndex = 22;
            // 
            // btnCloseAlarm
            // 
            this.btnCloseAlarm.Location = new System.Drawing.Point(967, 6);
            this.btnCloseAlarm.Name = "btnCloseAlarm";
            this.btnCloseAlarm.Size = new System.Drawing.Size(64, 25);
            this.btnCloseAlarm.TabIndex = 20;
            this.btnCloseAlarm.Text = "CloseAll";
            this.btnCloseAlarm.UseVisualStyleBackColor = true;
            this.btnCloseAlarm.Click += new System.EventHandler(this.btnCloseAlarm_Click);
            // 
            // TriggerAlarm
            // 
            this.TriggerAlarm.Location = new System.Drawing.Point(875, 6);
            this.TriggerAlarm.Name = "TriggerAlarm";
            this.TriggerAlarm.Size = new System.Drawing.Size(89, 25);
            this.TriggerAlarm.TabIndex = 21;
            this.TriggerAlarm.Text = "TriggerAlarm";
            this.TriggerAlarm.UseVisualStyleBackColor = true;
            this.TriggerAlarm.Click += new System.EventHandler(this.TriggerAlarm_Click);
            // 
            // dTP_EndTime
            // 
            this.dTP_EndTime.CustomFormat = "yyyy/MM/dd-HH:mm:ss";
            this.dTP_EndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTP_EndTime.Location = new System.Drawing.Point(399, 6);
            this.dTP_EndTime.Name = "dTP_EndTime";
            this.dTP_EndTime.ShowUpDown = true;
            this.dTP_EndTime.Size = new System.Drawing.Size(152, 20);
            this.dTP_EndTime.TabIndex = 19;
            // 
            // alarmLogShow1
            // 
            this.alarmLogShow1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.alarmLogShow1.DataSourceTable = null;
            this.alarmLogShow1.Location = new System.Drawing.Point(4, 368);
            this.alarmLogShow1.Margin = new System.Windows.Forms.Padding(4);
            this.alarmLogShow1.Name = "alarmLogShow1";
            this.alarmLogShow1.ShowColumn = null;
            this.alarmLogShow1.Size = new System.Drawing.Size(1084, 295);
            this.alarmLogShow1.TabIndex = 15;
            // 
            // alarm_Duration1
            // 
            this.alarm_Duration1.AD_Label = null;
            this.alarm_Duration1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.alarm_Duration1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alarm_Duration1.ColumnColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(127)))), ((int)(((byte)(126)))));
            this.alarm_Duration1.DataSourceTable = null;
            this.alarm_Duration1.Location = new System.Drawing.Point(555, 33);
            this.alarm_Duration1.Margin = new System.Windows.Forms.Padding(4);
            this.alarm_Duration1.Name = "alarm_Duration1";
            this.alarm_Duration1.Size = new System.Drawing.Size(533, 331);
            this.alarm_Duration1.TabIndex = 14;
            // 
            // dT_Statiistic1
            // 
            this.dT_Statiistic1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
            this.dT_Statiistic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dT_Statiistic1.DataSourceTable = null;
            this.dT_Statiistic1.DT_Label = null;
            this.dT_Statiistic1.ErrorMessage = null;
            this.dT_Statiistic1.Location = new System.Drawing.Point(4, 33);
            this.dT_Statiistic1.Margin = new System.Windows.Forms.Padding(4);
            this.dT_Statiistic1.Name = "dT_Statiistic1";
            this.dT_Statiistic1.PieStyle = BoTech.PieStyles.Time;
            this.dT_Statiistic1.Size = new System.Drawing.Size(547, 331);
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
            // xStationStateBar1
            // 
            this.xStationStateBar1.Location = new System.Drawing.Point(903, 327);
            this.xStationStateBar1.Name = "xStationStateBar1";
            this.xStationStateBar1.Size = new System.Drawing.Size(185, 34);
            this.xStationStateBar1.StationId = 4;
            this.xStationStateBar1.TabIndex = 25;
            // 
            // PageAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xStationStateBar1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dTP_StartTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.alarmLogShow1);
            this.Controls.Add(this.alarm_Duration1);
            this.Controls.Add(this.dT_Statiistic1);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_Query);
            this.Controls.Add(this.cb_Alarm);
            this.Controls.Add(this.btnCloseAlarm);
            this.Controls.Add(this.TriggerAlarm);
            this.Controls.Add(this.dTP_EndTime);
            this.Name = "PageAlarm";
            this.Size = new System.Drawing.Size(1092, 667);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dTP_StartTime;
        private System.Windows.Forms.Label label1;
        private BoTech.AlarmLogShow alarmLogShow1;
        private BoTech.Alarm_Duration alarm_Duration1;
        private BoTech.DT_Statiistic dT_Statiistic1;
        private System.Windows.Forms.Timer StatusRefreshTimer;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Query;
        private System.Windows.Forms.ComboBox cb_Alarm;
        private System.Windows.Forms.Button btnCloseAlarm;
        private System.Windows.Forms.Button TriggerAlarm;
        private System.Windows.Forms.DateTimePicker dTP_EndTime;
        private XCore.XStationStateBar xStationStateBar1;
    }
}
