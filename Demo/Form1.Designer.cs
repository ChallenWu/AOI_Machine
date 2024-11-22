namespace Demo
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pageContainer = new System.Windows.Forms.Panel();
            this.pnHeader = new System.Windows.Forms.Panel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnModel = new System.Windows.Forms.Panel();
            this.lbModelRun = new System.Windows.Forms.Label();
            this.menuButton_Start = new Demo.MenuButton();
            this.menuButton_Stop = new Demo.MenuButton();
            this.menuButton_Pause = new Demo.MenuButton();
            this.pnEQMStatus = new System.Windows.Forms.Panel();
            this.lbEQMStatus = new System.Windows.Forms.Label();
            this.pnUser = new System.Windows.Forms.Panel();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.menuButton_Home = new Demo.UserControls.newMenuButton();
            this.menuButton_Setting = new Demo.UserControls.newMenuButton();
            this.menuButton_Vison = new Demo.UserControls.newMenuButton();
            this.menuButton_Alarm = new Demo.UserControls.newMenuButton();
            this.menuButton_Chart = new Demo.UserControls.newMenuButton();
            this.menuButton_Login = new Demo.UserControls.newMenuButton();
            this.pnHeader.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnModel.SuspendLayout();
            this.pnEQMStatus.SuspendLayout();
            this.pnUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageContainer
            // 
            this.pageContainer.BackColor = System.Drawing.SystemColors.Control;
            this.pageContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pageContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageContainer.Location = new System.Drawing.Point(197, 3);
            this.pageContainer.Name = "pageContainer";
            this.pageContainer.Size = new System.Drawing.Size(1183, 680);
            this.pageContainer.TabIndex = 13;
            // 
            // pnHeader
            // 
            this.pnHeader.BackColor = System.Drawing.Color.LightGray;
            this.pnHeader.Controls.Add(this.tableLayoutPanel4);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnHeader.Location = new System.Drawing.Point(3, 3);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(1383, 94);
            this.pnHeader.TabIndex = 16;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 610F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1383, 94);
            this.tableLayoutPanel4.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(315, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(455, 94);
            this.label1.TabIndex = 17;
            this.label1.Text = "AOI Machine";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel5.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.pnUser, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(776, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(604, 88);
            this.tableLayoutPanel5.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pnModel);
            this.panel1.Controls.Add(this.menuButton_Start);
            this.panel1.Controls.Add(this.menuButton_Stop);
            this.panel1.Controls.Add(this.menuButton_Pause);
            this.panel1.Controls.Add(this.pnEQMStatus);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(452, 82);
            this.panel1.TabIndex = 18;
            // 
            // pnModel
            // 
            this.pnModel.BackColor = System.Drawing.Color.IndianRed;
            this.pnModel.Controls.Add(this.lbModelRun);
            this.pnModel.Location = new System.Drawing.Point(16, 48);
            this.pnModel.Name = "pnModel";
            this.pnModel.Size = new System.Drawing.Size(150, 31);
            this.pnModel.TabIndex = 17;
            // 
            // lbModelRun
            // 
            this.lbModelRun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbModelRun.Location = new System.Drawing.Point(0, 0);
            this.lbModelRun.Name = "lbModelRun";
            this.lbModelRun.Size = new System.Drawing.Size(150, 31);
            this.lbModelRun.TabIndex = 0;
            this.lbModelRun.Text = "Model";
            this.lbModelRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuButton_Start
            // 
            this.menuButton_Start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.menuButton_Start.BackgroundImage = global::Demo.Properties.Resources.start;
            this.menuButton_Start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.menuButton_Start.Location = new System.Drawing.Point(197, 3);
            this.menuButton_Start.MaximumSize = new System.Drawing.Size(67, 73);
            this.menuButton_Start.MinimumSize = new System.Drawing.Size(67, 73);
            this.menuButton_Start.Name = "menuButton_Start";
            this.menuButton_Start.Selected = false;
            this.menuButton_Start.Size = new System.Drawing.Size(67, 73);
            this.menuButton_Start.TabIndex = 6;
            this.menuButton_Start.Click += new System.EventHandler(this.menuButton_Start_Click);
            // 
            // menuButton_Stop
            // 
            this.menuButton_Stop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.menuButton_Stop.BackgroundImage = global::Demo.Properties.Resources.stop;
            this.menuButton_Stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.menuButton_Stop.Location = new System.Drawing.Point(366, 3);
            this.menuButton_Stop.MaximumSize = new System.Drawing.Size(67, 73);
            this.menuButton_Stop.MinimumSize = new System.Drawing.Size(67, 73);
            this.menuButton_Stop.Name = "menuButton_Stop";
            this.menuButton_Stop.Selected = false;
            this.menuButton_Stop.Size = new System.Drawing.Size(67, 73);
            this.menuButton_Stop.TabIndex = 8;
            this.menuButton_Stop.Click += new System.EventHandler(this.menuButton_Stop_Click);
            // 
            // menuButton_Pause
            // 
            this.menuButton_Pause.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(235)))));
            this.menuButton_Pause.BackgroundImage = global::Demo.Properties.Resources.pause;
            this.menuButton_Pause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.menuButton_Pause.Location = new System.Drawing.Point(279, 3);
            this.menuButton_Pause.MaximumSize = new System.Drawing.Size(67, 73);
            this.menuButton_Pause.MinimumSize = new System.Drawing.Size(67, 73);
            this.menuButton_Pause.Name = "menuButton_Pause";
            this.menuButton_Pause.Selected = false;
            this.menuButton_Pause.Size = new System.Drawing.Size(67, 73);
            this.menuButton_Pause.TabIndex = 7;
            this.menuButton_Pause.Click += new System.EventHandler(this.menuButton_Pause_Click);
            // 
            // pnEQMStatus
            // 
            this.pnEQMStatus.BackColor = System.Drawing.Color.LawnGreen;
            this.pnEQMStatus.Controls.Add(this.lbEQMStatus);
            this.pnEQMStatus.Location = new System.Drawing.Point(16, 4);
            this.pnEQMStatus.Name = "pnEQMStatus";
            this.pnEQMStatus.Size = new System.Drawing.Size(150, 37);
            this.pnEQMStatus.TabIndex = 16;
            // 
            // lbEQMStatus
            // 
            this.lbEQMStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEQMStatus.Location = new System.Drawing.Point(0, 0);
            this.lbEQMStatus.Name = "lbEQMStatus";
            this.lbEQMStatus.Size = new System.Drawing.Size(150, 37);
            this.lbEQMStatus.TabIndex = 0;
            this.lbEQMStatus.Text = "Mode: Normal Run";
            this.lbEQMStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnUser
            // 
            this.pnUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnUser.Controls.Add(this.lblLevel);
            this.pnUser.Controls.Add(this.lblUser);
            this.pnUser.Location = new System.Drawing.Point(461, 3);
            this.pnUser.Name = "pnUser";
            this.pnUser.Size = new System.Drawing.Size(140, 82);
            this.pnUser.TabIndex = 15;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(5, 42);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(39, 13);
            this.lblLevel.TabIndex = 1;
            this.lblLevel.Text = "Level: ";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(7, 17);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(75, 13);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "User: NoLogin";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Demo.Properties.Resources.images;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(306, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnHeader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1389, 792);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.09978F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.90022F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pageContainer, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 103);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 686F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1383, 686);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.menuButton_Home, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.menuButton_Setting, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.menuButton_Vison, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.menuButton_Alarm, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.menuButton_Chart, 0, 4);
            this.tableLayoutPanel3.Controls.Add(this.menuButton_Login, 0, 5);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 7;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(188, 680);
            this.tableLayoutPanel3.TabIndex = 13;
            // 
            // menuButton_Home
            // 
            this.menuButton_Home.BackColor = System.Drawing.Color.Silver;
            this.menuButton_Home.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButton_Home.ImageShow = global::Demo.Properties.Resources.home;
            this.menuButton_Home.Location = new System.Drawing.Point(3, 3);
            this.menuButton_Home.Name = "menuButton_Home";
            this.menuButton_Home.Selected = false;
            this.menuButton_Home.Size = new System.Drawing.Size(182, 62);
            this.menuButton_Home.TabIndex = 14;
            this.menuButton_Home.TextButtton = "Home";
            // 
            // menuButton_Setting
            // 
            this.menuButton_Setting.BackColor = System.Drawing.Color.Silver;
            this.menuButton_Setting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButton_Setting.ImageShow = global::Demo.Properties.Resources.setting;
            this.menuButton_Setting.Location = new System.Drawing.Point(3, 71);
            this.menuButton_Setting.Name = "menuButton_Setting";
            this.menuButton_Setting.Selected = false;
            this.menuButton_Setting.Size = new System.Drawing.Size(182, 62);
            this.menuButton_Setting.TabIndex = 15;
            this.menuButton_Setting.TextButtton = "Setting";
            // 
            // menuButton_Vison
            // 
            this.menuButton_Vison.BackColor = System.Drawing.Color.Silver;
            this.menuButton_Vison.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButton_Vison.ImageShow = global::Demo.Properties.Resources.vision;
            this.menuButton_Vison.Location = new System.Drawing.Point(3, 139);
            this.menuButton_Vison.Name = "menuButton_Vison";
            this.menuButton_Vison.Selected = false;
            this.menuButton_Vison.Size = new System.Drawing.Size(182, 62);
            this.menuButton_Vison.TabIndex = 16;
            this.menuButton_Vison.TextButtton = "Vision";
            // 
            // menuButton_Alarm
            // 
            this.menuButton_Alarm.BackColor = System.Drawing.Color.Silver;
            this.menuButton_Alarm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButton_Alarm.ImageShow = global::Demo.Properties.Resources.alarm;
            this.menuButton_Alarm.Location = new System.Drawing.Point(3, 207);
            this.menuButton_Alarm.Name = "menuButton_Alarm";
            this.menuButton_Alarm.Selected = false;
            this.menuButton_Alarm.Size = new System.Drawing.Size(182, 62);
            this.menuButton_Alarm.TabIndex = 17;
            this.menuButton_Alarm.TextButtton = "Alarm";
            // 
            // menuButton_Chart
            // 
            this.menuButton_Chart.BackColor = System.Drawing.Color.Silver;
            this.menuButton_Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButton_Chart.ImageShow = global::Demo.Properties.Resources.chart;
            this.menuButton_Chart.Location = new System.Drawing.Point(3, 275);
            this.menuButton_Chart.Name = "menuButton_Chart";
            this.menuButton_Chart.Selected = false;
            this.menuButton_Chart.Size = new System.Drawing.Size(182, 62);
            this.menuButton_Chart.TabIndex = 18;
            this.menuButton_Chart.TextButtton = "Chart";
            // 
            // menuButton_Login
            // 
            this.menuButton_Login.BackColor = System.Drawing.Color.Silver;
            this.menuButton_Login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuButton_Login.ImageShow = global::Demo.Properties.Resources.user;
            this.menuButton_Login.Location = new System.Drawing.Point(3, 343);
            this.menuButton_Login.Name = "menuButton_Login";
            this.menuButton_Login.Selected = false;
            this.menuButton_Login.Size = new System.Drawing.Size(182, 62);
            this.menuButton_Login.TabIndex = 13;
            this.menuButton_Login.TextButtton = "User Manager";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(1389, 792);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnHeader.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnModel.ResumeLayout(false);
            this.pnEQMStatus.ResumeLayout(false);
            this.pnUser.ResumeLayout(false);
            this.pnUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Demo.MenuButton menuButton_Start;
        private Demo.MenuButton menuButton_Pause;
        private Demo.MenuButton menuButton_Stop;
        private System.Windows.Forms.Panel pageContainer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnUser;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Panel pnHeader;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private UserControls.newMenuButton menuButton_Login;
        private UserControls.newMenuButton menuButton_Home;
        private UserControls.newMenuButton menuButton_Setting;
        private UserControls.newMenuButton menuButton_Vison;
        private UserControls.newMenuButton menuButton_Alarm;
        private UserControls.newMenuButton menuButton_Chart;
        private System.Windows.Forms.Panel pnEQMStatus;
        private System.Windows.Forms.Label lbEQMStatus;
        private System.Windows.Forms.Panel pnModel;
        private System.Windows.Forms.Label lbModelRun;
    }
}

