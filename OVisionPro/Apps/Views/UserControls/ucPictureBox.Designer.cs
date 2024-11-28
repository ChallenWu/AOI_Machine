
namespace OVisionPro.Apps.Views.UserControls
{
    partial class ucPictureBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPictureBox));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCreateROI = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteROI = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pictureDebug = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDebug)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCreateROI,
            this.btnDeleteROI,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 392);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(664, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCreateROI
            // 
            this.btnCreateROI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCreateROI.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateROI.Image")));
            this.btnCreateROI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreateROI.Name = "btnCreateROI";
            this.btnCreateROI.Size = new System.Drawing.Size(23, 22);
            this.btnCreateROI.Text = "toolStripButton4";
            this.btnCreateROI.Click += new System.EventHandler(this.btnCreateROI_Click);
            // 
            // btnDeleteROI
            // 
            this.btnDeleteROI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteROI.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteROI.Image")));
            this.btnDeleteROI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteROI.Name = "btnDeleteROI";
            this.btnDeleteROI.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteROI.Text = "toolStripButton3";
            this.btnDeleteROI.Click += new System.EventHandler(this.btnDeleteROI_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // pictureDebug
            // 
            this.pictureDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureDebug.Image = ((System.Drawing.Image)(resources.GetObject("pictureDebug.Image")));
            this.pictureDebug.Location = new System.Drawing.Point(0, 0);
            this.pictureDebug.Name = "pictureDebug";
            this.pictureDebug.Size = new System.Drawing.Size(664, 392);
            this.pictureDebug.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureDebug.TabIndex = 1;
            this.pictureDebug.TabStop = false;
            this.pictureDebug.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureDebug_Paint);
            this.pictureDebug.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureDebug_MouseDown);
            this.pictureDebug.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureDebug_MouseMove);
            this.pictureDebug.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureDebug_MouseUp);
            // 
            // ucPictureBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureDebug);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ucPictureBox";
            this.Size = new System.Drawing.Size(664, 417);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDebug)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnDeleteROI;
        private System.Windows.Forms.ToolStripButton btnCreateROI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.PictureBox pictureDebug;
    }
}
