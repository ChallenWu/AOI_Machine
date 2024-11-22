namespace Demo.Page
{
    partial class PageVision
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
            this.ucHomeVision1 = new OVisionPro.ucHomeVision();
            this.SuspendLayout();
            // 
            // ucHomeVision1
            // 
            this.ucHomeVision1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucHomeVision1.Location = new System.Drawing.Point(0, 0);
            this.ucHomeVision1.Name = "ucHomeVision1";
            this.ucHomeVision1.Size = new System.Drawing.Size(1092, 667);
            this.ucHomeVision1.TabIndex = 0;
            // 
            // PageVision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucHomeVision1);
            this.Name = "PageVision";
            this.Size = new System.Drawing.Size(1092, 667);
            this.ResumeLayout(false);

        }

        #endregion

        private OVisionPro.ucHomeVision ucHomeVision1;
    }
}
