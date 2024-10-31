namespace Demo.Page
{
    partial class PageLogin
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_User = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_Password = new System.Windows.Forms.TextBox();
            this.button_Login = new System.Windows.Forms.Button();
            this.btn_Register = new System.Windows.Forms.Button();
            this.button_LogOut = new System.Windows.Forms.Button();
            this.button_Exit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.roundPanel1 = new Demo.UserControls.RoundPanel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.switchButtonGRR = new Demo.SwitchButton();
            this.switchButton_CPKGRR = new Demo.SwitchButton();
            this.switchButton_Engineering = new Demo.SwitchButton();
            this.switchButton_Production = new Demo.SwitchButton();
            this.userAccountManager1 = new Demo.UserAccountManager();
            this.roundPanel2 = new Demo.UserControls.RoundPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(35, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name";
            // 
            // comboBox_User
            // 
            this.comboBox_User.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_User.FormattingEnabled = true;
            this.comboBox_User.Location = new System.Drawing.Point(40, 139);
            this.comboBox_User.Name = "comboBox_User";
            this.comboBox_User.Size = new System.Drawing.Size(386, 28);
            this.comboBox_User.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(35, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // textBox_Password
            // 
            this.textBox_Password.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Password.Location = new System.Drawing.Point(40, 206);
            this.textBox_Password.Name = "textBox_Password";
            this.textBox_Password.Size = new System.Drawing.Size(386, 26);
            this.textBox_Password.TabIndex = 3;
            // 
            // button_Login
            // 
            this.button_Login.Location = new System.Drawing.Point(40, 259);
            this.button_Login.Name = "button_Login";
            this.button_Login.Size = new System.Drawing.Size(114, 62);
            this.button_Login.TabIndex = 4;
            this.button_Login.Text = "Login";
            this.button_Login.UseVisualStyleBackColor = true;
            this.button_Login.Click += new System.EventHandler(this.button_Login_Click);
            // 
            // btn_Register
            // 
            this.btn_Register.Location = new System.Drawing.Point(176, 259);
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.Size = new System.Drawing.Size(114, 62);
            this.btn_Register.TabIndex = 5;
            this.btn_Register.Text = "Register";
            this.btn_Register.UseVisualStyleBackColor = true;
            this.btn_Register.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // button_LogOut
            // 
            this.button_LogOut.Location = new System.Drawing.Point(312, 259);
            this.button_LogOut.Name = "button_LogOut";
            this.button_LogOut.Size = new System.Drawing.Size(114, 62);
            this.button_LogOut.TabIndex = 6;
            this.button_LogOut.Text = "LogOut";
            this.button_LogOut.UseVisualStyleBackColor = true;
            this.button_LogOut.Click += new System.EventHandler(this.button_LogOut_Click);
            // 
            // button_Exit
            // 
            this.button_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Exit.Location = new System.Drawing.Point(960, 602);
            this.button_Exit.Name = "button_Exit";
            this.button_Exit.Size = new System.Drawing.Size(129, 62);
            this.button_Exit.TabIndex = 7;
            this.button_Exit.Text = "Exit";
            this.button_Exit.UseVisualStyleBackColor = true;
            this.button_Exit.Click += new System.EventHandler(this.button_Exit_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.roundPanel1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox_User);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_Password);
            this.panel1.Controls.Add(this.button_LogOut);
            this.panel1.Controls.Add(this.button_Login);
            this.panel1.Controls.Add(this.btn_Register);
            this.panel1.Location = new System.Drawing.Point(608, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 339);
            this.panel1.TabIndex = 10;
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.roundPanel1.BackgroundImage = global::Demo.Properties.Resources.user;
            this.roundPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.roundPanel1.Location = new System.Drawing.Point(28, 18);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Size = new System.Drawing.Size(75, 75);
            this.roundPanel1.TabIndex = 7;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // switchButtonGRR
            // 
            this.switchButtonGRR.Location = new System.Drawing.Point(162, 166);
            this.switchButtonGRR.Name = "switchButtonGRR";
            this.switchButtonGRR.Size = new System.Drawing.Size(210, 58);
            this.switchButtonGRR.STS = false;
            this.switchButtonGRR.TabIndex = 12;
            // 
            // switchButton_CPKGRR
            // 
            this.switchButton_CPKGRR.Location = new System.Drawing.Point(162, 240);
            this.switchButton_CPKGRR.Name = "switchButton_CPKGRR";
            this.switchButton_CPKGRR.Size = new System.Drawing.Size(210, 58);
            this.switchButton_CPKGRR.STS = false;
            this.switchButton_CPKGRR.TabIndex = 11;
            // 
            // switchButton_Engineering
            // 
            this.switchButton_Engineering.Location = new System.Drawing.Point(162, 92);
            this.switchButton_Engineering.Name = "switchButton_Engineering";
            this.switchButton_Engineering.Size = new System.Drawing.Size(210, 58);
            this.switchButton_Engineering.STS = false;
            this.switchButton_Engineering.TabIndex = 9;
            // 
            // switchButton_Production
            // 
            this.switchButton_Production.Location = new System.Drawing.Point(162, 18);
            this.switchButton_Production.Name = "switchButton_Production";
            this.switchButton_Production.Size = new System.Drawing.Size(210, 58);
            this.switchButton_Production.STS = false;
            this.switchButton_Production.TabIndex = 8;
            // 
            // userAccountManager1
            // 
            this.userAccountManager1.Location = new System.Drawing.Point(36, 347);
            this.userAccountManager1.Margin = new System.Windows.Forms.Padding(2);
            this.userAccountManager1.Name = "userAccountManager1";
            this.userAccountManager1.Size = new System.Drawing.Size(526, 274);
            this.userAccountManager1.TabIndex = 13;
            // 
            // roundPanel2
            // 
            this.roundPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.roundPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.roundPanel2.Location = new System.Drawing.Point(608, 357);
            this.roundPanel2.Name = "roundPanel2";
            this.roundPanel2.Size = new System.Drawing.Size(484, 310);
            this.roundPanel2.TabIndex = 14;
            // 
            // PageLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.userAccountManager1);
            this.Controls.Add(this.switchButtonGRR);
            this.Controls.Add(this.switchButton_CPKGRR);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.switchButton_Engineering);
            this.Controls.Add(this.switchButton_Production);
            this.Controls.Add(this.button_Exit);
            this.Controls.Add(this.roundPanel2);
            this.Name = "PageLogin";
            this.Size = new System.Drawing.Size(1092, 667);
            this.Load += new System.EventHandler(this.PageLogin_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_User;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Password;
        private System.Windows.Forms.Button button_Login;
        private System.Windows.Forms.Button btn_Register;
        private System.Windows.Forms.Button button_LogOut;
        private System.Windows.Forms.Button button_Exit;
        private Demo.SwitchButton switchButton_Production;
        private Demo.SwitchButton switchButton_Engineering;
        private System.Windows.Forms.Panel panel1;
        private Demo.SwitchButton switchButton_CPKGRR;
        private Demo.SwitchButton switchButtonGRR;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private UserAccountManager userAccountManager1;
        private UserControls.RoundPanel roundPanel1;
        private UserControls.RoundPanel roundPanel2;
    }
}
