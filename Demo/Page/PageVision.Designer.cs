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
            this.ucVisionPage1 = new OVisionPro.ucVisionPage();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ucVisionPage1
            // 
            this.ucVisionPage1.Location = new System.Drawing.Point(0, 0);
            this.ucVisionPage1.Margin = new System.Windows.Forms.Padding(2);
            this.ucVisionPage1.Name = "ucVisionPage1";
            this.ucVisionPage1.Size = new System.Drawing.Size(1092, 580);
            this.ucVisionPage1.TabIndex = 0;
            this.ucVisionPage1.TaskId = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(934, 585);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 41);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PageVision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ucVisionPage1);
            this.Name = "PageVision";
            this.Size = new System.Drawing.Size(1092, 667);
            this.ResumeLayout(false);

        }


        #endregion

        private OVisionPro.ucVisionPage ucVisionPage1;
        private System.Windows.Forms.Button button1;
    }
}
