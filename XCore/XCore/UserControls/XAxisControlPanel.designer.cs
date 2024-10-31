namespace XCore
{
    partial class XAxisControlPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XAxisControlPanel));
            this.Bar_Vel = new System.Windows.Forms.TrackBar();
            this.Comb_Distance = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Btn_Home = new System.Windows.Forms.Button();
            this.Btn_Forward = new System.Windows.Forms.Button();
            this.Btn_Back = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LB_Home = new System.Windows.Forms.ToolStripStatusLabel();
            this.LB_AxisNo = new System.Windows.Forms.ToolStripStatusLabel();
            this.LB_Pos = new System.Windows.Forms.ToolStripStatusLabel();
            this.PB_MEL = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PB_ORG = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.PB_PEL = new System.Windows.Forms.PictureBox();
            this.PB_ALM = new System.Windows.Forms.PictureBox();
            this.PB_ASTP = new System.Windows.Forms.PictureBox();
            this.PB_SVON = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Comb_AxisNo = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_ClearError = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.bt_JOG_N = new System.Windows.Forms.Button();
            this.bt_JOG_P = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_Acc = new System.Windows.Forms.TextBox();
            this.Btn_Stop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Bar_Vel)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MEL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_ORG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_PEL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_ALM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_ASTP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_SVON)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Bar_Vel
            // 
            resources.ApplyResources(this.Bar_Vel, "Bar_Vel");
            this.Bar_Vel.Name = "Bar_Vel";
            this.Bar_Vel.Scroll += new System.EventHandler(this.Bar_Vel_Scroll);
            // 
            // Comb_Distance
            // 
            resources.ApplyResources(this.Comb_Distance, "Comb_Distance");
            this.Comb_Distance.FormattingEnabled = true;
            this.Comb_Distance.Name = "Comb_Distance";
            this.Comb_Distance.SelectedIndexChanged += new System.EventHandler(this.Comb_Distance_SelectedIndexChanged);
            this.Comb_Distance.TextChanged += new System.EventHandler(this.Comb_Distance_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // Btn_Home
            // 
            resources.ApplyResources(this.Btn_Home, "Btn_Home");
            this.Btn_Home.Name = "Btn_Home";
            this.Btn_Home.UseVisualStyleBackColor = true;
            this.Btn_Home.Click += new System.EventHandler(this.Btn_Home_Click);
            // 
            // Btn_Forward
            // 
            resources.ApplyResources(this.Btn_Forward, "Btn_Forward");
            this.Btn_Forward.Image = global::XCore.Properties.Resources._add;
            this.Btn_Forward.Name = "Btn_Forward";
            this.Btn_Forward.UseVisualStyleBackColor = true;
            this.Btn_Forward.Click += new System.EventHandler(this.Btn_Forward_Click);
            // 
            // Btn_Back
            // 
            resources.ApplyResources(this.Btn_Back, "Btn_Back");
            this.Btn_Back.Image = global::XCore.Properties.Resources._minus;
            this.Btn_Back.Name = "Btn_Back";
            this.Btn_Back.UseVisualStyleBackColor = true;
            this.Btn_Back.Click += new System.EventHandler(this.Btn_Back_Click);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.LB_Home,
            this.LB_AxisNo,
            this.LB_Pos});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // LB_Home
            // 
            resources.ApplyResources(this.LB_Home, "LB_Home");
            this.LB_Home.Name = "LB_Home";
            // 
            // LB_AxisNo
            // 
            resources.ApplyResources(this.LB_AxisNo, "LB_AxisNo");
            this.LB_AxisNo.Name = "LB_AxisNo";
            // 
            // LB_Pos
            // 
            resources.ApplyResources(this.LB_Pos, "LB_Pos");
            this.LB_Pos.Name = "LB_Pos";
            // 
            // PB_MEL
            // 
            resources.ApplyResources(this.PB_MEL, "PB_MEL");
            this.PB_MEL.Name = "PB_MEL";
            this.PB_MEL.TabStop = false;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // PB_ORG
            // 
            resources.ApplyResources(this.PB_ORG, "PB_ORG");
            this.PB_ORG.Name = "PB_ORG";
            this.PB_ORG.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // PB_PEL
            // 
            resources.ApplyResources(this.PB_PEL, "PB_PEL");
            this.PB_PEL.Name = "PB_PEL";
            this.PB_PEL.TabStop = false;
            // 
            // PB_ALM
            // 
            resources.ApplyResources(this.PB_ALM, "PB_ALM");
            this.PB_ALM.Name = "PB_ALM";
            this.PB_ALM.TabStop = false;
            // 
            // PB_ASTP
            // 
            resources.ApplyResources(this.PB_ASTP, "PB_ASTP");
            this.PB_ASTP.Name = "PB_ASTP";
            this.PB_ASTP.TabStop = false;
            // 
            // PB_SVON
            // 
            resources.ApplyResources(this.PB_SVON, "PB_SVON");
            this.PB_SVON.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PB_SVON.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PB_SVON.Name = "PB_SVON";
            this.PB_SVON.TabStop = false;
            this.PB_SVON.Click += new System.EventHandler(this.PB_SVON_Click);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // Comb_AxisNo
            // 
            resources.ApplyResources(this.Comb_AxisNo, "Comb_AxisNo");
            this.Comb_AxisNo.FormattingEnabled = true;
            this.Comb_AxisNo.Name = "Comb_AxisNo";
            this.Comb_AxisNo.SelectedIndexChanged += new System.EventHandler(this.Comb_AxisNo_SelectedIndexChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.btn_ClearError);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.PB_SVON);
            this.panel1.Controls.Add(this.PB_ASTP);
            this.panel1.Controls.Add(this.PB_ALM);
            this.panel1.Controls.Add(this.PB_MEL);
            this.panel1.Controls.Add(this.PB_PEL);
            this.panel1.Controls.Add(this.PB_ORG);
            this.panel1.Controls.Add(this.bt_JOG_N);
            this.panel1.Controls.Add(this.bt_JOG_P);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.textBox_Acc);
            this.panel1.Controls.Add(this.Btn_Stop);
            this.panel1.Controls.Add(this.Btn_Home);
            this.panel1.Controls.Add(this.Comb_AxisNo);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.Comb_Distance);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.Btn_Back);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.Btn_Forward);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.Bar_Vel);
            this.panel1.Name = "panel1";
            // 
            // btn_ClearError
            // 
            resources.ApplyResources(this.btn_ClearError, "btn_ClearError");
            this.btn_ClearError.Name = "btn_ClearError";
            this.btn_ClearError.UseVisualStyleBackColor = true;
            this.btn_ClearError.Click += new System.EventHandler(this.btn_ClearError_Click);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // bt_JOG_N
            // 
            resources.ApplyResources(this.bt_JOG_N, "bt_JOG_N");
            this.bt_JOG_N.Image = global::XCore.Properties.Resources._minus;
            this.bt_JOG_N.Name = "bt_JOG_N";
            this.bt_JOG_N.UseVisualStyleBackColor = true;
            this.bt_JOG_N.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_JOG_N_MouseDown);
            this.bt_JOG_N.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_JOG_N_MouseUp);
            // 
            // bt_JOG_P
            // 
            resources.ApplyResources(this.bt_JOG_P, "bt_JOG_P");
            this.bt_JOG_P.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bt_JOG_P.Image = global::XCore.Properties.Resources._add;
            this.bt_JOG_P.Name = "bt_JOG_P";
            this.bt_JOG_P.UseVisualStyleBackColor = false;
            this.bt_JOG_P.MouseDown += new System.Windows.Forms.MouseEventHandler(this.bt_JOG_P_MouseDown);
            this.bt_JOG_P.MouseUp += new System.Windows.Forms.MouseEventHandler(this.bt_JOG_P_MouseUp);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // textBox_Acc
            // 
            resources.ApplyResources(this.textBox_Acc, "textBox_Acc");
            this.textBox_Acc.Name = "textBox_Acc";
            this.textBox_Acc.TextChanged += new System.EventHandler(this.textBox_Acc_TextChanged);
            // 
            // Btn_Stop
            // 
            resources.ApplyResources(this.Btn_Stop, "Btn_Stop");
            this.Btn_Stop.Image = global::XCore.Properties.Resources._wait;
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.UseVisualStyleBackColor = true;
            this.Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // XAxisControlPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "XAxisControlPanel";
            ((System.ComponentModel.ISupportInitialize)(this.Bar_Vel)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PB_MEL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_ORG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_PEL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_ALM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_ASTP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PB_SVON)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar Bar_Vel;
        private System.Windows.Forms.ComboBox Comb_Distance;
        private System.Windows.Forms.Button Btn_Back;
        private System.Windows.Forms.Button Btn_Forward;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Btn_Home;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LB_Home;
        private System.Windows.Forms.ToolStripStatusLabel LB_AxisNo;
        private System.Windows.Forms.ToolStripStatusLabel LB_Pos;
        private System.Windows.Forms.PictureBox PB_MEL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox PB_ORG;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox PB_PEL;
        private System.Windows.Forms.PictureBox PB_ALM;
        private System.Windows.Forms.PictureBox PB_ASTP;
        private System.Windows.Forms.PictureBox PB_SVON;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox Comb_AxisNo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Btn_Stop;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_Acc;
        private System.Windows.Forms.Button bt_JOG_N;
        private System.Windows.Forms.Button bt_JOG_P;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn_ClearError;
    }
}
