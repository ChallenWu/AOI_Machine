namespace Demo.Page
{
    partial class PageEngineering
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
            this.comboBox_Mode = new System.Windows.Forms.ComboBox();
            this.lbTime = new System.Windows.Forms.Label();
            this.lbPN = new System.Windows.Forms.Label();
            this.Label_NG_OK = new System.Windows.Forms.Label();
            this.Cycle = new System.Windows.Forms.TextBox();
            this.Product_Num = new System.Windows.Forms.TextBox();
            this.Label43 = new System.Windows.Forms.Label();
            this.Label44 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.roundPanel4 = new Demo.UserControls.RoundPanel();
            this.roundPanel3 = new Demo.UserControls.RoundPanel();
            this.netWorkWatcher1 = new Demo.NetWorkWatcher();
            this.switchButton1 = new Demo.SwitchButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.xStationStateBar1 = new XCore.XStationStateBar();
            this.xTaskStepBar1 = new XCore.XTaskStepBar();
            this.pnStatus = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbPcRun = new System.Windows.Forms.Label();
            this.lbSoftwareRun = new System.Windows.Forms.Label();
            this.chkMES = new System.Windows.Forms.CheckBox();
            this.chkSN = new System.Windows.Forms.CheckBox();
            this.chkPN = new System.Windows.Forms.CheckBox();
            this.chkImage2 = new System.Windows.Forms.CheckBox();
            this.chkImageAOI = new System.Windows.Forms.CheckBox();
            this.chkImage1 = new System.Windows.Forms.CheckBox();
            this.chkStation = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.pnStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Mode
            // 
            this.comboBox_Mode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Mode.FormattingEnabled = true;
            this.comboBox_Mode.Location = new System.Drawing.Point(657, 14);
            this.comboBox_Mode.Name = "comboBox_Mode";
            this.comboBox_Mode.Size = new System.Drawing.Size(237, 24);
            this.comboBox_Mode.TabIndex = 4;
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.BackColor = System.Drawing.Color.Lime;
            this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold);
            this.lbTime.ForeColor = System.Drawing.Color.Black;
            this.lbTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbTime.Location = new System.Drawing.Point(303, 131);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(63, 13);
            this.lbTime.TabIndex = 98;
            this.lbTime.Text = "20220211";
            // 
            // lbPN
            // 
            this.lbPN.BackColor = System.Drawing.Color.Lime;
            this.lbPN.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbPN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbPN.Location = new System.Drawing.Point(0, 0);
            this.lbPN.Name = "lbPN";
            this.lbPN.Size = new System.Drawing.Size(434, 162);
            this.lbPN.TabIndex = 97;
            this.lbPN.Text = "------";
            this.lbPN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_NG_OK
            // 
            this.Label_NG_OK.BackColor = System.Drawing.Color.Lime;
            this.Label_NG_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 42F, System.Drawing.FontStyle.Bold);
            this.Label_NG_OK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label_NG_OK.Location = new System.Drawing.Point(811, 60);
            this.Label_NG_OK.Name = "Label_NG_OK";
            this.Label_NG_OK.Size = new System.Drawing.Size(121, 64);
            this.Label_NG_OK.TabIndex = 96;
            this.Label_NG_OK.Text = "OK";
            this.Label_NG_OK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cycle
            // 
            this.Cycle.Enabled = false;
            this.Cycle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.Cycle.Location = new System.Drawing.Point(767, 278);
            this.Cycle.Multiline = true;
            this.Cycle.Name = "Cycle";
            this.Cycle.Size = new System.Drawing.Size(77, 23);
            this.Cycle.TabIndex = 104;
            this.Cycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Product_Num
            // 
            this.Product_Num.Enabled = false;
            this.Product_Num.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F);
            this.Product_Num.Location = new System.Drawing.Point(767, 235);
            this.Product_Num.Multiline = true;
            this.Product_Num.Name = "Product_Num";
            this.Product_Num.Size = new System.Drawing.Size(77, 23);
            this.Product_Num.TabIndex = 102;
            this.Product_Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Label43
            // 
            this.Label43.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.Label43.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Label43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label43.Location = new System.Drawing.Point(675, 237);
            this.Label43.Name = "Label43";
            this.Label43.Size = new System.Drawing.Size(79, 21);
            this.Label43.TabIndex = 103;
            this.Label43.Text = "Yield ";
            this.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label44
            // 
            this.Label44.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.Label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Label44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label44.Location = new System.Drawing.Point(675, 278);
            this.Label44.Name = "Label44";
            this.Label44.Size = new System.Drawing.Size(79, 21);
            this.Label44.TabIndex = 105;
            this.Label44.Text = "Cycle(s)";
            this.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // roundPanel4
            // 
            this.roundPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.roundPanel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.roundPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.roundPanel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.roundPanel4.Location = new System.Drawing.Point(654, 220);
            this.roundPanel4.Name = "roundPanel4";
            this.roundPanel4.Size = new System.Drawing.Size(216, 221);
            this.roundPanel4.TabIndex = 16;
            // 
            // roundPanel3
            // 
            this.roundPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.roundPanel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.roundPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.roundPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundPanel3.Location = new System.Drawing.Point(872, 220);
            this.roundPanel3.Name = "roundPanel3";
            this.roundPanel3.Size = new System.Drawing.Size(217, 220);
            this.roundPanel3.TabIndex = 15;
            // 
            // netWorkWatcher1
            // 
            this.netWorkWatcher1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.netWorkWatcher1.Location = new System.Drawing.Point(654, 446);
            this.netWorkWatcher1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.netWorkWatcher1.Name = "netWorkWatcher1";
            this.netWorkWatcher1.Size = new System.Drawing.Size(435, 167);
            this.netWorkWatcher1.TabIndex = 13;
            // 
            // switchButton1
            // 
            this.switchButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.switchButton1.Location = new System.Drawing.Point(915, 3);
            this.switchButton1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.switchButton1.Name = "switchButton1";
            this.switchButton1.Size = new System.Drawing.Size(174, 46);
            this.switchButton1.STS = false;
            this.switchButton1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.xStationStateBar1);
            this.panel1.Controls.Add(this.xTaskStepBar1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 610);
            this.panel1.TabIndex = 112;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.richTextBox1.Location = new System.Drawing.Point(3, 79);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(636, 528);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Main Log";
            // 
            // xStationStateBar1
            // 
            this.xStationStateBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.xStationStateBar1.Location = new System.Drawing.Point(16, 11);
            this.xStationStateBar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.xStationStateBar1.Name = "xStationStateBar1";
            this.xStationStateBar1.Size = new System.Drawing.Size(292, 30);
            this.xStationStateBar1.StationId = 4;
            this.xStationStateBar1.TabIndex = 2;
            // 
            // xTaskStepBar1
            // 
            this.xTaskStepBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xTaskStepBar1.Location = new System.Drawing.Point(335, 11);
            this.xTaskStepBar1.Margin = new System.Windows.Forms.Padding(4);
            this.xTaskStepBar1.Name = "xTaskStepBar1";
            this.xTaskStepBar1.Size = new System.Drawing.Size(292, 30);
            this.xTaskStepBar1.TabIndex = 3;
            this.xTaskStepBar1.TaskId = 70;
            // 
            // pnStatus
            // 
            this.pnStatus.BackColor = System.Drawing.Color.Lime;
            this.pnStatus.Controls.Add(this.lbTime);
            this.pnStatus.Controls.Add(this.lbPN);
            this.pnStatus.Location = new System.Drawing.Point(655, 52);
            this.pnStatus.Name = "pnStatus";
            this.pnStatus.Size = new System.Drawing.Size(434, 162);
            this.pnStatus.TabIndex = 120;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(675, 322);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 21);
            this.label2.TabIndex = 121;
            this.label2.Text = "PC";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(675, 368);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 21);
            this.label3.TabIndex = 122;
            this.label3.Text = "RUNTIME";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbPcRun
            // 
            this.lbPcRun.AutoSize = true;
            this.lbPcRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.lbPcRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbPcRun.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbPcRun.Location = new System.Drawing.Point(767, 328);
            this.lbPcRun.Name = "lbPcRun";
            this.lbPcRun.Size = new System.Drawing.Size(72, 15);
            this.lbPcRun.TabIndex = 123;
            this.lbPcRun.Text = "00:00:00:00";
            this.lbPcRun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbSoftwareRun
            // 
            this.lbSoftwareRun.AutoSize = true;
            this.lbSoftwareRun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.lbSoftwareRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbSoftwareRun.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbSoftwareRun.Location = new System.Drawing.Point(766, 371);
            this.lbSoftwareRun.Name = "lbSoftwareRun";
            this.lbSoftwareRun.Size = new System.Drawing.Size(72, 15);
            this.lbSoftwareRun.TabIndex = 124;
            this.lbSoftwareRun.Text = "00:00:00:00";
            this.lbSoftwareRun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkMES
            // 
            this.chkMES.AutoSize = true;
            this.chkMES.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.chkMES.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkMES.Location = new System.Drawing.Point(976, 337);
            this.chkMES.Name = "chkMES";
            this.chkMES.Size = new System.Drawing.Size(96, 19);
            this.chkMES.TabIndex = 125;
            this.chkMES.Text = "Upload MES";
            this.chkMES.UseVisualStyleBackColor = false;
            this.chkMES.Visible = false;
            this.chkMES.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chkSN
            // 
            this.chkSN.AutoSize = true;
            this.chkSN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.chkSN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkSN.Location = new System.Drawing.Point(976, 287);
            this.chkSN.Name = "chkSN";
            this.chkSN.Size = new System.Drawing.Size(43, 19);
            this.chkSN.TabIndex = 126;
            this.chkSN.Text = "SN";
            this.chkSN.UseVisualStyleBackColor = false;
            this.chkSN.Visible = false;
            this.chkSN.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // chkPN
            // 
            this.chkPN.AutoSize = true;
            this.chkPN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.chkPN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkPN.Location = new System.Drawing.Point(891, 337);
            this.chkPN.Name = "chkPN";
            this.chkPN.Size = new System.Drawing.Size(46, 19);
            this.chkPN.TabIndex = 127;
            this.chkPN.Text = "P/N";
            this.chkPN.UseVisualStyleBackColor = false;
            this.chkPN.Visible = false;
            this.chkPN.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // chkImage2
            // 
            this.chkImage2.AutoSize = true;
            this.chkImage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.chkImage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkImage2.Location = new System.Drawing.Point(891, 287);
            this.chkImage2.Name = "chkImage2";
            this.chkImage2.Size = new System.Drawing.Size(60, 19);
            this.chkImage2.TabIndex = 128;
            this.chkImage2.Text = "Tool 2";
            this.chkImage2.UseVisualStyleBackColor = false;
            this.chkImage2.Visible = false;
            this.chkImage2.CheckedChanged += new System.EventHandler(this.chkImage2_CheckedChanged);
            // 
            // chkImageAOI
            // 
            this.chkImageAOI.AutoSize = true;
            this.chkImageAOI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.chkImageAOI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkImageAOI.Location = new System.Drawing.Point(976, 237);
            this.chkImageAOI.Name = "chkImageAOI";
            this.chkImageAOI.Size = new System.Drawing.Size(83, 19);
            this.chkImageAOI.TabIndex = 129;
            this.chkImageAOI.Text = "Result AOI";
            this.chkImageAOI.UseVisualStyleBackColor = false;
            this.chkImageAOI.Visible = false;
            this.chkImageAOI.CheckedChanged += new System.EventHandler(this.chkImageProcess_CheckedChanged);
            // 
            // chkImage1
            // 
            this.chkImage1.AutoSize = true;
            this.chkImage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.chkImage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkImage1.Location = new System.Drawing.Point(891, 237);
            this.chkImage1.Name = "chkImage1";
            this.chkImage1.Size = new System.Drawing.Size(60, 19);
            this.chkImage1.TabIndex = 130;
            this.chkImage1.Text = "Tool 1";
            this.chkImage1.UseVisualStyleBackColor = false;
            this.chkImage1.Visible = false;
            this.chkImage1.CheckedChanged += new System.EventHandler(this.chkImage1_CheckedChanged);
            // 
            // chkStation
            // 
            this.chkStation.AutoSize = true;
            this.chkStation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.chkStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.chkStation.Location = new System.Drawing.Point(891, 387);
            this.chkStation.Name = "chkStation";
            this.chkStation.Size = new System.Drawing.Size(90, 19);
            this.chkStation.TabIndex = 131;
            this.chkStation.Text = "Check MES";
            this.chkStation.UseVisualStyleBackColor = false;
            this.chkStation.Visible = false;
            this.chkStation.CheckedChanged += new System.EventHandler(this.chkStation_CheckedChanged);
            // 
            // PageEngineering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.chkStation);
            this.Controls.Add(this.chkImage1);
            this.Controls.Add(this.chkImageAOI);
            this.Controls.Add(this.chkImage2);
            this.Controls.Add(this.chkPN);
            this.Controls.Add(this.chkSN);
            this.Controls.Add(this.chkMES);
            this.Controls.Add(this.lbSoftwareRun);
            this.Controls.Add(this.lbPcRun);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Cycle);
            this.Controls.Add(this.Product_Num);
            this.Controls.Add(this.Label43);
            this.Controls.Add(this.Label44);
            this.Controls.Add(this.Label_NG_OK);
            this.Controls.Add(this.roundPanel4);
            this.Controls.Add(this.roundPanel3);
            this.Controls.Add(this.netWorkWatcher1);
            this.Controls.Add(this.comboBox_Mode);
            this.Controls.Add(this.switchButton1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnStatus);
            this.Font = new System.Drawing.Font("SimSun", 9F);
            this.Name = "PageEngineering";
            this.Size = new System.Drawing.Size(1092, 616);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnStatus.ResumeLayout(false);
            this.pnStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private SwitchButton switchButton1;
        private XCore.XStationStateBar xStationStateBar1;
        private XCore.XTaskStepBar xTaskStepBar1;
        private System.Windows.Forms.ComboBox comboBox_Mode;
        private NetWorkWatcher netWorkWatcher1;
        private UserControls.RoundPanel roundPanel3;
        private UserControls.RoundPanel roundPanel4;
        internal System.Windows.Forms.Label lbTime;
        internal System.Windows.Forms.Label lbPN;
        internal System.Windows.Forms.Label Label_NG_OK;
        internal System.Windows.Forms.TextBox Cycle;
        internal System.Windows.Forms.TextBox Product_Num;
        internal System.Windows.Forms.Label Label43;
        internal System.Windows.Forms.Label Label44;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnStatus;
        private System.Windows.Forms.RichTextBox richTextBox1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label lbPcRun;
        internal System.Windows.Forms.Label lbSoftwareRun;
        private System.Windows.Forms.CheckBox chkMES;
        private System.Windows.Forms.CheckBox chkSN;
        private System.Windows.Forms.CheckBox chkPN;
        private System.Windows.Forms.CheckBox chkImage2;
        private System.Windows.Forms.CheckBox chkImageAOI;
        private System.Windows.Forms.CheckBox chkImage1;
        private System.Windows.Forms.CheckBox chkStation;
    }
}
