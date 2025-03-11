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
            this.xSettingGrid_ICT = new XCore.XSettingGrid();
            this.xSettingGrid_SettingOption = new XCore.XSettingGrid();
            this.xSettingGrid1 = new XCore.XSettingGrid();
            this.SuspendLayout();
            // 
            // xSettingGrid_ICT
            // 
            this.xSettingGrid_ICT.Id = 20;
            this.xSettingGrid_ICT.Location = new System.Drawing.Point(399, 3);
            this.xSettingGrid_ICT.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.xSettingGrid_ICT.Name = "xSettingGrid_ICT";
            this.xSettingGrid_ICT.Size = new System.Drawing.Size(347, 669);
            this.xSettingGrid_ICT.TabIndex = 8;
            // 
            // xSettingGrid_SettingOption
            // 
            this.xSettingGrid_SettingOption.Id = 1;
            this.xSettingGrid_SettingOption.Location = new System.Drawing.Point(18, 3);
            this.xSettingGrid_SettingOption.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.xSettingGrid_SettingOption.Name = "xSettingGrid_SettingOption";
            this.xSettingGrid_SettingOption.Size = new System.Drawing.Size(322, 669);
            this.xSettingGrid_SettingOption.TabIndex = 7;
            // 
            // xSettingGrid1
            // 
            this.xSettingGrid1.Id = 20;
            this.xSettingGrid1.Location = new System.Drawing.Point(804, 3);
            this.xSettingGrid1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.xSettingGrid1.Name = "xSettingGrid1";
            this.xSettingGrid1.Size = new System.Drawing.Size(340, 669);
            this.xSettingGrid1.TabIndex = 9;
            // 
            // PageSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xSettingGrid1);
            this.Controls.Add(this.xSettingGrid_SettingOption);
            this.Controls.Add(this.xSettingGrid_ICT);
            this.Name = "PageSetting";
            this.Size = new System.Drawing.Size(1168, 675);
            this.Load += new System.EventHandler(this.PageSetting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal XCore.XSettingGrid xSettingGrid_ICT;
        internal XCore.XSettingGrid xSettingGrid_SettingOption;
        internal XCore.XSettingGrid xSettingGrid1;
    }
}
