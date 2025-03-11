
namespace OVisionPro.Apps.Views.XWidgets
{
    partial class DialogSettingWidgetLiveCCD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSettingWidgetLiveCCD));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCapture = new System.Windows.Forms.ToolStripButton();
            this.btnLive = new System.Windows.Forms.ToolStripButton();
            this.btnpause = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(545, 357);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(545, 332);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCapture,
            this.btnLive,
            this.btnpause});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(545, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCapture
            // 
            this.btnCapture.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCapture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCapture.Image = ((System.Drawing.Image)(resources.GetObject("btnCapture.Image")));
            this.btnCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(23, 22);
            this.btnCapture.Text = "toolStripButton2";
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnLive
            // 
            this.btnLive.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnLive.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLive.Image = ((System.Drawing.Image)(resources.GetObject("btnLive.Image")));
            this.btnLive.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(23, 22);
            this.btnLive.Text = "toolStripButton1";
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // btnpause
            // 
            this.btnpause.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnpause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnpause.Image = ((System.Drawing.Image)(resources.GetObject("btnpause.Image")));
            this.btnpause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnpause.Name = "btnpause";
            this.btnpause.Size = new System.Drawing.Size(23, 22);
            this.btnpause.Text = "toolStripButton1";
            this.btnpause.Click += new System.EventHandler(this.btnpause_Click);
            // 
            // DialogSettingWidgetLiveCCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 357);
            this.Controls.Add(this.panel1);
            this.Name = "DialogSettingWidgetLiveCCD";
            this.Text = "DialogSettingWidgetLiveCCD";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DialogSettingWidgetLiveCCD_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnCapture;
        private System.Windows.Forms.ToolStripButton btnLive;
        private System.Windows.Forms.ToolStripButton btnpause;
    }
}