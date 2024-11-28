
namespace OVisionPro
{
    partial class VisionUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisionUI));
            this.tabVSBZ0010 = new System.Windows.Forms.TabPage();
            this.pnAdjustMain = new System.Windows.Forms.Panel();
            this.tabVSHome = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.pictureMainCCD = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.timerStreamCCD = new System.Windows.Forms.Timer(this.components);
            this.ucPropertyGridBlockBasic1 = new OVisionPro.ucPropertyGridBlockBasic();
            this.tabVSBZ0010.SuspendLayout();
            this.pnAdjustMain.SuspendLayout();
            this.tabVSHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMainCCD)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabVSBZ0010
            // 
            this.tabVSBZ0010.Controls.Add(this.pnAdjustMain);
            this.tabVSBZ0010.Location = new System.Drawing.Point(4, 22);
            this.tabVSBZ0010.Margin = new System.Windows.Forms.Padding(2);
            this.tabVSBZ0010.Name = "tabVSBZ0010";
            this.tabVSBZ0010.Size = new System.Drawing.Size(908, 520);
            this.tabVSBZ0010.TabIndex = 2;
            this.tabVSBZ0010.Text = "BZ0010";
            this.tabVSBZ0010.UseVisualStyleBackColor = true;
            // 
            // pnAdjustMain
            // 
            this.pnAdjustMain.Controls.Add(this.ucPropertyGridBlockBasic1);
            this.pnAdjustMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnAdjustMain.Location = new System.Drawing.Point(0, 0);
            this.pnAdjustMain.Name = "pnAdjustMain";
            this.pnAdjustMain.Size = new System.Drawing.Size(908, 520);
            this.pnAdjustMain.TabIndex = 3;
            // 
            // tabVSHome
            // 
            this.tabVSHome.Controls.Add(this.splitContainer1);
            this.tabVSHome.Location = new System.Drawing.Point(4, 22);
            this.tabVSHome.Margin = new System.Windows.Forms.Padding(2);
            this.tabVSHome.Name = "tabVSHome";
            this.tabVSHome.Padding = new System.Windows.Forms.Padding(2);
            this.tabVSHome.Size = new System.Drawing.Size(908, 520);
            this.tabVSHome.TabIndex = 0;
            this.tabVSHome.Text = "HOME";
            this.tabVSHome.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(2, 2);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.propertyGrid1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureMainCCD);
            this.splitContainer1.Size = new System.Drawing.Size(904, 516);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Margin = new System.Windows.Forms.Padding(2);
            this.propertyGrid1.MaximumSize = new System.Drawing.Size(300, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(300, 516);
            this.propertyGrid1.TabIndex = 0;
            // 
            // pictureMainCCD
            // 
            this.pictureMainCCD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureMainCCD.Image = ((System.Drawing.Image)(resources.GetObject("pictureMainCCD.Image")));
            this.pictureMainCCD.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureMainCCD.InitialImage")));
            this.pictureMainCCD.Location = new System.Drawing.Point(0, 0);
            this.pictureMainCCD.Margin = new System.Windows.Forms.Padding(2);
            this.pictureMainCCD.Name = "pictureMainCCD";
            this.pictureMainCCD.Size = new System.Drawing.Size(601, 516);
            this.pictureMainCCD.TabIndex = 0;
            this.pictureMainCCD.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabVSHome);
            this.tabControl1.Controls.Add(this.tabVSBZ0010);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(916, 546);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // timerStreamCCD
            // 
            this.timerStreamCCD.Interval = 33;
            this.timerStreamCCD.Tick += new System.EventHandler(this.timerStreamCCD_Tick);
            // 
            // ucPropertyGridBlockBasic1
            // 
            this.ucPropertyGridBlockBasic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPropertyGridBlockBasic1.Location = new System.Drawing.Point(0, 0);
            this.ucPropertyGridBlockBasic1.Name = "ucPropertyGridBlockBasic1";
            this.ucPropertyGridBlockBasic1.Size = new System.Drawing.Size(908, 520);
            this.ucPropertyGridBlockBasic1.TabIndex = 0;
            this.ucPropertyGridBlockBasic1.TaskId = 1;
            // 
            // VisionUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "VisionUI";
            this.Size = new System.Drawing.Size(916, 546);
            this.tabVSBZ0010.ResumeLayout(false);
            this.pnAdjustMain.ResumeLayout(false);
            this.tabVSHome.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureMainCCD)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabVSBZ0010;
        private System.Windows.Forms.TabPage tabVSHome;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.PictureBox pictureMainCCD;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel pnAdjustMain;
        private System.Windows.Forms.Timer timerStreamCCD;
        private ucPropertyGridBlockBasic ucPropertyGridBlockBasic1;
    }
}
