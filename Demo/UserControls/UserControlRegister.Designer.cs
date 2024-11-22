namespace Demo
{
    partial class UserAccountRegister
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Account = new System.Windows.Forms.TextBox();
            this.txt_Name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_PassWord1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_PassWord2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Register = new System.Windows.Forms.Button();
            this.errorProvider_DiffPassWord = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_DiffPassWord)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Account";
            // 
            // txt_Account
            // 
            this.txt_Account.Location = new System.Drawing.Point(33, 60);
            this.txt_Account.Name = "txt_Account";
            this.txt_Account.Size = new System.Drawing.Size(206, 20);
            this.txt_Account.TabIndex = 1;
            // 
            // txt_Name
            // 
            this.txt_Name.Location = new System.Drawing.Point(33, 123);
            this.txt_Name.Name = "txt_Name";
            this.txt_Name.Size = new System.Drawing.Size(206, 20);
            this.txt_Name.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Name";
            // 
            // txt_PassWord1
            // 
            this.txt_PassWord1.Location = new System.Drawing.Point(33, 194);
            this.txt_PassWord1.Name = "txt_PassWord1";
            this.txt_PassWord1.Size = new System.Drawing.Size(206, 20);
            this.txt_PassWord1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password";
            // 
            // txt_PassWord2
            // 
            this.txt_PassWord2.Location = new System.Drawing.Point(33, 268);
            this.txt_PassWord2.Name = "txt_PassWord2";
            this.txt_PassWord2.Size = new System.Drawing.Size(206, 20);
            this.txt_PassWord2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Re-enter Password";
            // 
            // btn_Register
            // 
            this.btn_Register.Location = new System.Drawing.Point(78, 321);
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.Size = new System.Drawing.Size(112, 43);
            this.btn_Register.TabIndex = 8;
            this.btn_Register.Text = "Register";
            this.btn_Register.UseVisualStyleBackColor = true;
            this.btn_Register.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // errorProvider_DiffPassWord
            // 
            this.errorProvider_DiffPassWord.ContainerControl = this;
            // 
            // UserAccountRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 398);
            this.Controls.Add(this.btn_Register);
            this.Controls.Add(this.txt_PassWord2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_PassWord1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Account);
            this.Controls.Add(this.label1);
            this.Name = "UserAccountRegister";
            this.Text = "UserControlRegister";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider_DiffPassWord)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_Account;
        private System.Windows.Forms.TextBox txt_Name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_PassWord1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_PassWord2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Register;
        private System.Windows.Forms.ErrorProvider errorProvider_DiffPassWord;
    }
}