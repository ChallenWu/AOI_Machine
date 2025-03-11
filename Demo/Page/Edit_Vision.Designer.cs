namespace Demo.Page
{
    partial class Edit_Vision
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
            this.processCreatorUI1 = new VisionTools.ProcessCreatorUI();
            this.SuspendLayout();
            // 
            // processCreatorUI1
            // 
            this.processCreatorUI1.BackColor = System.Drawing.Color.Transparent;
            this.processCreatorUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processCreatorUI1.Location = new System.Drawing.Point(0, 0);
            this.processCreatorUI1.Margin = new System.Windows.Forms.Padding(4);
            this.processCreatorUI1.Name = "processCreatorUI1";
            this.processCreatorUI1.Size = new System.Drawing.Size(1111, 702);
            this.processCreatorUI1.TabIndex = 0;
            // 
            // Edit_Vision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 702);
            this.Controls.Add(this.processCreatorUI1);
            this.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Edit_Vision";
            this.Text = "Edit_Vision";
            this.ResumeLayout(false);

        }

        #endregion

        internal VisionTools.ProcessCreatorUI processCreatorUI1;
    }
}