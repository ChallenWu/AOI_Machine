namespace Demo.Page
{
    partial class PageSetting
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Setting = new System.Windows.Forms.TabPage();
            this.xSettingGrid_SettingOption = new XCore.XSettingGrid();
            this.xSettingGrid_ICT = new XCore.XSettingGrid();
            this.tabControl1.SuspendLayout();
            this.Setting.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Setting);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1089, 661);
            this.tabControl1.TabIndex = 0;
            // 
            // Setting
            // 
            this.Setting.BackColor = System.Drawing.Color.White;
            this.Setting.Controls.Add(this.xSettingGrid_SettingOption);
            this.Setting.Controls.Add(this.xSettingGrid_ICT);
            this.Setting.Location = new System.Drawing.Point(4, 24);
            this.Setting.Name = "Setting";
            this.Setting.Padding = new System.Windows.Forms.Padding(3);
            this.Setting.Size = new System.Drawing.Size(1081, 633);
            this.Setting.TabIndex = 0;
            this.Setting.Text = "Parameter";
            // 
            // xSettingGrid_SettingOption
            // 
            this.xSettingGrid_SettingOption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xSettingGrid_SettingOption.Id = 1;
            this.xSettingGrid_SettingOption.Location = new System.Drawing.Point(7, 3);
            this.xSettingGrid_SettingOption.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.xSettingGrid_SettingOption.Name = "xSettingGrid_SettingOption";
            this.xSettingGrid_SettingOption.Size = new System.Drawing.Size(344, 627);
            this.xSettingGrid_SettingOption.TabIndex = 7;
            // 
            // xSettingGrid_ICT
            // 
            this.xSettingGrid_ICT.BackColor = System.Drawing.Color.Transparent;
            this.xSettingGrid_ICT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xSettingGrid_ICT.Id = 20;
            this.xSettingGrid_ICT.Location = new System.Drawing.Point(378, 3);
            this.xSettingGrid_ICT.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.xSettingGrid_ICT.Name = "xSettingGrid_ICT";
            this.xSettingGrid_ICT.Size = new System.Drawing.Size(344, 627);
            this.xSettingGrid_ICT.TabIndex = 6;
            // 
            // PageSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "PageSetting";
            this.Size = new System.Drawing.Size(1092, 667);
            this.Load += new System.EventHandler(this.PageSetting_Load);
            this.tabControl1.ResumeLayout(false);
            this.Setting.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Setting;
        internal XCore.XSettingGrid xSettingGrid_ICT;
        internal XCore.XSettingGrid xSettingGrid_SettingOption;
    }
}
