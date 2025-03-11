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
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.displayViewInteract1 = new VisionTools.DisplayViewInteract();
            this.displayViewInteract2 = new VisionTools.DisplayViewInteract();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(547, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(539, 73);
            this.panel2.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(539, 73);
            this.label2.TabIndex = 0;
            this.label2.Text = "MONITOR JOB 2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(540, 73);
            this.panel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(540, 73);
            this.label1.TabIndex = 0;
            this.label1.Text = "MONITOR JOB 1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // displayViewInteract1
            // 
            this.displayViewInteract1.Auto_Fit = false;
            this.displayViewInteract1.AutoScroll = true;
            this.displayViewInteract1.Coordinate = ECoordinate.Pixel;
            this.displayViewInteract1.CurrentObject = null;
            this.displayViewInteract1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.displayViewInteract1.IsDisplayCenterLine = false;
            this.displayViewInteract1.IsDisplayCoordinate = false;
            this.displayViewInteract1.IsMeasurementMode = false;
            this.displayViewInteract1.Location = new System.Drawing.Point(2, 115);
            this.displayViewInteract1.Margin = new System.Windows.Forms.Padding(2);
            this.displayViewInteract1.Name = "displayViewInteract1";
            this.displayViewInteract1.Size = new System.Drawing.Size(541, 550);
            this.displayViewInteract1.TabIndex = 5;
            this.displayViewInteract1.ToastVisible = true;
            this.displayViewInteract1.ToolName = null;
            this.displayViewInteract1.ZoomVisible = false;
            // 
            // displayViewInteract2
            // 
            this.displayViewInteract2.Auto_Fit = false;
            this.displayViewInteract2.AutoScroll = true;
            this.displayViewInteract2.Coordinate = ECoordinate.Pixel;
            this.displayViewInteract2.CurrentObject = null;
            this.displayViewInteract2.Cursor = System.Windows.Forms.Cursors.Cross;
            this.displayViewInteract2.IsDisplayCenterLine = false;
            this.displayViewInteract2.IsDisplayCoordinate = false;
            this.displayViewInteract2.IsMeasurementMode = false;
            this.displayViewInteract2.Location = new System.Drawing.Point(547, 115);
            this.displayViewInteract2.Margin = new System.Windows.Forms.Padding(2);
            this.displayViewInteract2.Name = "displayViewInteract2";
            this.displayViewInteract2.Size = new System.Drawing.Size(543, 550);
            this.displayViewInteract2.TabIndex = 6;
            this.displayViewInteract2.ToastVisible = true;
            this.displayViewInteract2.ToolName = null;
            this.displayViewInteract2.ZoomVisible = false;
            // 
            // PageVision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.displayViewInteract1);
            this.Controls.Add(this.displayViewInteract2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PageVision";
            this.Size = new System.Drawing.Size(1092, 667);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion
        public VisionTools.DisplayViewInteract displayViewInteract1;
        public VisionTools.DisplayViewInteract displayViewInteract2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
    }
}
