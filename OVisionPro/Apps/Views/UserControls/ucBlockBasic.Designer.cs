
namespace OVisionPro.apps.Views.UserControls
{
    partial class ucBlockBasic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucBlockBasic));
            this.label1 = new System.Windows.Forms.Label();
            this.pnHeaderBlock = new System.Windows.Forms.Panel();
            this.lblL = new System.Windows.Forms.Label();
            this.lblR = new System.Windows.Forms.Label();
            this.pnHeaderBlock.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "BlockName_1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnHeaderBlock
            // 
            this.pnHeaderBlock.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnHeaderBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnHeaderBlock.Controls.Add(this.lblL);
            this.pnHeaderBlock.Controls.Add(this.lblR);
            this.pnHeaderBlock.Location = new System.Drawing.Point(3, 39);
            this.pnHeaderBlock.Name = "pnHeaderBlock";
            this.pnHeaderBlock.Size = new System.Drawing.Size(208, 36);
            this.pnHeaderBlock.TabIndex = 1;
            this.pnHeaderBlock.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DraggablePanel_MouseDown);
            this.pnHeaderBlock.MouseEnter += new System.EventHandler(this.pnHeaderBlock_MouseEnter);
            this.pnHeaderBlock.MouseLeave += new System.EventHandler(this.pnHeaderBlock_MouseLeave);
            this.pnHeaderBlock.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DraggablePanel_MouseMove);
            this.pnHeaderBlock.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DraggablePanel_MouseUp);
            // 
            // lblL
            // 
            this.lblL.AutoSize = true;
            this.lblL.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblL.Image = ((System.Drawing.Image)(resources.GetObject("lblL.Image")));
            this.lblL.Location = new System.Drawing.Point(3, 8);
            this.lblL.Name = "lblL";
            this.lblL.Size = new System.Drawing.Size(16, 17);
            this.lblL.TabIndex = 3;
            this.lblL.Text = "  ";
            this.lblL.Click += new System.EventHandler(this.lblL_Click);
            this.lblL.MouseEnter += new System.EventHandler(this.lblL_MouseEnter);
            this.lblL.MouseLeave += new System.EventHandler(this.lblL_MouseLeave);
            // 
            // lblR
            // 
            this.lblR.AutoSize = true;
            this.lblR.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblR.Image = ((System.Drawing.Image)(resources.GetObject("lblR.Image")));
            this.lblR.Location = new System.Drawing.Point(187, 8);
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(16, 17);
            this.lblR.TabIndex = 2;
            this.lblR.Text = "  ";
            this.lblR.Click += new System.EventHandler(this.lblR_Click);
            this.lblR.MouseEnter += new System.EventHandler(this.lblR_MouseEnter);
            this.lblR.MouseLeave += new System.EventHandler(this.lblR_MouseLeave);
            // 
            // ucBlockBasic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnHeaderBlock);
            this.Controls.Add(this.label1);
            this.Name = "ucBlockBasic";
            this.Size = new System.Drawing.Size(216, 258);
            this.pnHeaderBlock.ResumeLayout(false);
            this.pnHeaderBlock.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnHeaderBlock;
        private System.Windows.Forms.Label lblR;
        private System.Windows.Forms.Label lblL;
    }
}
