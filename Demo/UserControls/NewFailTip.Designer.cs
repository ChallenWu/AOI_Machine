namespace Demo.UserControls
{
    partial class NewFailTip
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rBox_ErrorDetail = new System.Windows.Forms.RichTextBox();
            this.cbox_ErrorMsgList = new System.Windows.Forms.ComboBox();
            this.txt_Code = new System.Windows.Forms.TextBox();
            this.txt_StartTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ErrorDetail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_ErrorMsg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAffirm = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRetry = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rBox_ErrorDetail);
            this.groupBox1.Controls.Add(this.cbox_ErrorMsgList);
            this.groupBox1.Location = new System.Drawing.Point(4, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 178);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // rBox_ErrorDetail
            // 
            this.rBox_ErrorDetail.Location = new System.Drawing.Point(11, 47);
            this.rBox_ErrorDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rBox_ErrorDetail.Name = "rBox_ErrorDetail";
            this.rBox_ErrorDetail.Size = new System.Drawing.Size(300, 125);
            this.rBox_ErrorDetail.TabIndex = 4;
            this.rBox_ErrorDetail.Text = "";
            // 
            // cbox_ErrorMsgList
            // 
            this.cbox_ErrorMsgList.FormattingEnabled = true;
            this.cbox_ErrorMsgList.Location = new System.Drawing.Point(11, 22);
            this.cbox_ErrorMsgList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cbox_ErrorMsgList.Name = "cbox_ErrorMsgList";
            this.cbox_ErrorMsgList.Size = new System.Drawing.Size(300, 21);
            this.cbox_ErrorMsgList.TabIndex = 3;
            // 
            // txt_Code
            // 
            this.txt_Code.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Code.Location = new System.Drawing.Point(117, 48);
            this.txt_Code.Name = "txt_Code";
            this.txt_Code.Size = new System.Drawing.Size(198, 21);
            this.txt_Code.TabIndex = 10;
            // 
            // txt_StartTime
            // 
            this.txt_StartTime.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_StartTime.Location = new System.Drawing.Point(117, 20);
            this.txt_StartTime.Name = "txt_StartTime";
            this.txt_StartTime.Size = new System.Drawing.Size(198, 21);
            this.txt_StartTime.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Error Code:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Start Time:";
            // 
            // txt_ErrorDetail
            // 
            this.txt_ErrorDetail.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ErrorDetail.Location = new System.Drawing.Point(117, 105);
            this.txt_ErrorDetail.Name = "txt_ErrorDetail";
            this.txt_ErrorDetail.Size = new System.Drawing.Size(198, 21);
            this.txt_ErrorDetail.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(12, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Error Detail:";
            // 
            // txt_ErrorMsg
            // 
            this.txt_ErrorMsg.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ErrorMsg.Location = new System.Drawing.Point(117, 77);
            this.txt_ErrorMsg.Name = "txt_ErrorMsg";
            this.txt_ErrorMsg.Size = new System.Drawing.Size(198, 21);
            this.txt_ErrorMsg.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(12, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 17);
            this.label4.TabIndex = 16;
            this.label4.Text = "Error Msg:";
            // 
            // btnAffirm
            // 
            this.btnAffirm.Location = new System.Drawing.Point(250, 319);
            this.btnAffirm.Name = "btnAffirm";
            this.btnAffirm.Size = new System.Drawing.Size(75, 48);
            this.btnAffirm.TabIndex = 13;
            this.btnAffirm.Text = "Xác nhận";
            this.btnAffirm.UseVisualStyleBackColor = true;
            this.btnAffirm.Click += new System.EventHandler(this.btnAffirm_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(4, 319);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 48);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Tắt còi";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(125, 319);
            this.btnRetry.Name = "btnRetry";
            this.btnRetry.Size = new System.Drawing.Size(75, 48);
            this.btnRetry.TabIndex = 15;
            this.btnRetry.Text = "Thử lại";
            this.btnRetry.UseVisualStyleBackColor = true;
            this.btnRetry.Click += new System.EventHandler(this.btnRetry_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1500;
            // 
            // NewFailTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.IndianRed;
            this.ClientSize = new System.Drawing.Size(330, 376);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_Code);
            this.Controls.Add(this.txt_StartTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_ErrorDetail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_ErrorMsg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnAffirm);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRetry);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "NewFailTip";
            this.Text = "NewFailTip";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rBox_ErrorDetail;
        private System.Windows.Forms.ComboBox cbox_ErrorMsgList;
        private System.Windows.Forms.TextBox txt_Code;
        private System.Windows.Forms.TextBox txt_StartTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ErrorDetail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_ErrorMsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAffirm;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRetry;
        private System.Windows.Forms.Timer timer1;
    }
}