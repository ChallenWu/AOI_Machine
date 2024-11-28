
namespace OVisionPro
{
    partial class DialogSettingWidget
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSettingWidget));
            this.tabHome = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.customPropertyGrid1 = new OVisionPro.CustomPropertyGrid();
            this.pictureMainCCD = new System.Windows.Forms.PictureBox();
            this.tabBZ = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnCreateROI = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteROI = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel3 = new System.Windows.Forms.Panel();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lblCbbFuncID = new System.Windows.Forms.ToolStripLabel();
            this.cbbFuncID = new System.Windows.Forms.ToolStripComboBox();
            this.btnSaveCVConfig = new System.Windows.Forms.ToolStripButton();
            this.btnStartDebug = new System.Windows.Forms.ToolStripButton();
            this.txtDragTxtBox = new OVisionPro.DragDropTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbbModelTasks = new System.Windows.Forms.ToolStripComboBox();
            this.btnLoadConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.tabHome.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMainCCD)).BeginInit();
            this.tabBZ.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabHome
            // 
            this.tabHome.Controls.Add(this.tabPage1);
            this.tabHome.Controls.Add(this.tabBZ);
            this.tabHome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabHome.Location = new System.Drawing.Point(0, 0);
            this.tabHome.Name = "tabHome";
            this.tabHome.SelectedIndex = 0;
            this.tabHome.Size = new System.Drawing.Size(959, 526);
            this.tabHome.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(951, 500);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "HOME";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.customPropertyGrid1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureMainCCD);
            this.splitContainer1.Size = new System.Drawing.Size(945, 494);
            this.splitContainer1.SplitterDistance = 314;
            this.splitContainer1.TabIndex = 0;
            // 
            // customPropertyGrid1
            // 
            this.customPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customPropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.customPropertyGrid1.Name = "customPropertyGrid1";
            this.customPropertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.customPropertyGrid1.Size = new System.Drawing.Size(314, 494);
            this.customPropertyGrid1.TabIndex = 0;
            // 
            // pictureMainCCD
            // 
            this.pictureMainCCD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureMainCCD.Image = ((System.Drawing.Image)(resources.GetObject("pictureMainCCD.Image")));
            this.pictureMainCCD.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureMainCCD.InitialImage")));
            this.pictureMainCCD.Location = new System.Drawing.Point(0, 0);
            this.pictureMainCCD.Margin = new System.Windows.Forms.Padding(2);
            this.pictureMainCCD.Name = "pictureMainCCD";
            this.pictureMainCCD.Size = new System.Drawing.Size(627, 494);
            this.pictureMainCCD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureMainCCD.TabIndex = 1;
            this.pictureMainCCD.TabStop = false;
            // 
            // tabBZ
            // 
            this.tabBZ.Controls.Add(this.panel1);
            this.tabBZ.Location = new System.Drawing.Point(4, 22);
            this.tabBZ.Name = "tabBZ";
            this.tabBZ.Padding = new System.Windows.Forms.Padding(3);
            this.tabBZ.Size = new System.Drawing.Size(951, 500);
            this.tabBZ.TabIndex = 1;
            this.tabBZ.Text = "BZ9200";
            this.tabBZ.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(945, 494);
            this.panel1.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Controls.Add(this.toolStrip2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(314, 33);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(631, 461);
            this.panel5.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(627, 432);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseEventArgs);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCreateROI,
            this.btnDeleteROI,
            this.toolStripSeparator1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 432);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(627, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnCreateROI
            // 
            this.btnCreateROI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCreateROI.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateROI.Image")));
            this.btnCreateROI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreateROI.Name = "btnCreateROI";
            this.btnCreateROI.Size = new System.Drawing.Size(23, 22);
            this.btnCreateROI.Text = "toolStripButton1";
            // 
            // btnDeleteROI
            // 
            this.btnDeleteROI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteROI.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteROI.Image")));
            this.btnDeleteROI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteROI.Name = "btnDeleteROI";
            this.btnDeleteROI.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteROI.Text = "toolStripButton2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.propertyGrid1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(314, 461);
            this.panel3.TabIndex = 1;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 38);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(314, 423);
            this.propertyGrid1.TabIndex = 3;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.checkBox3);
            this.panel4.Controls.Add(this.checkBox2);
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(314, 38);
            this.panel4.TabIndex = 0;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox3.Location = new System.Drawing.Point(160, 0);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(80, 34);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox2.Location = new System.Drawing.Point(80, 0);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(80, 34);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(80, 34);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(945, 33);
            this.panel2.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCbbFuncID,
            this.cbbFuncID,
            this.btnSaveCVConfig,
            this.btnStartDebug,
            this.txtDragTxtBox,
            this.toolStripLabel1,
            this.cbbModelTasks,
            this.btnLoadConfig,
            this.toolStripComboBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(945, 33);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lblCbbFuncID
            // 
            this.lblCbbFuncID.Name = "lblCbbFuncID";
            this.lblCbbFuncID.Size = new System.Drawing.Size(34, 30);
            this.lblCbbFuncID.Text = "Tools";
            // 
            // cbbFuncID
            // 
            this.cbbFuncID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbFuncID.Name = "cbbFuncID";
            this.cbbFuncID.Size = new System.Drawing.Size(121, 33);
            this.cbbFuncID.SelectedIndexChanged += new System.EventHandler(this.cbbFuncID_SelectedIndexChanged);
            // 
            // btnSaveCVConfig
            // 
            this.btnSaveCVConfig.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSaveCVConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveCVConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveCVConfig.Image")));
            this.btnSaveCVConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveCVConfig.Name = "btnSaveCVConfig";
            this.btnSaveCVConfig.Size = new System.Drawing.Size(23, 30);
            this.btnSaveCVConfig.Text = "toolStripButton3";
            this.btnSaveCVConfig.Click += new System.EventHandler(this.btnSaveCVConfig_Click);
            // 
            // btnStartDebug
            // 
            this.btnStartDebug.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnStartDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStartDebug.Image = ((System.Drawing.Image)(resources.GetObject("btnStartDebug.Image")));
            this.btnStartDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartDebug.Name = "btnStartDebug";
            this.btnStartDebug.Size = new System.Drawing.Size(23, 30);
            this.btnStartDebug.Text = "toolStripButton1";
            this.btnStartDebug.Click += new System.EventHandler(this.btnStartDebug_Click);
            // 
            // txtDragTxtBox
            // 
            this.txtDragTxtBox.AllowedExtensions = new string[0];
            this.txtDragTxtBox.AutoSize = false;
            this.txtDragTxtBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDragTxtBox.Name = "txtDragTxtBox";
            this.txtDragTxtBox.ReadOnly = true;
            this.txtDragTxtBox.Size = new System.Drawing.Size(350, 33);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(77, 30);
            this.toolStripLabel1.Text = "Modek Tasks:";
            // 
            // cbbModelTasks
            // 
            this.cbbModelTasks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbModelTasks.Name = "cbbModelTasks";
            this.cbbModelTasks.Size = new System.Drawing.Size(121, 33);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnLoadConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoadConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadConfig.Image")));
            this.btnLoadConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(23, 30);
            this.btnLoadConfig.Text = "toolStripButton1";
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 33);
            // 
            // DialogSettingWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 526);
            this.Controls.Add(this.tabHome);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DialogSettingWidget";
            this.Text = "DialogSettingWidget";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DialogSettingWidget_FormClosed_1);
            this.Resize += new System.EventHandler(this.DialogSettingWidget_Resize);
            this.tabHome.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureMainCCD)).EndInit();
            this.tabBZ.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabHome;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabBZ;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureMainCCD;
        private CustomPropertyGrid customPropertyGrid1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnCreateROI;
        private System.Windows.Forms.ToolStripButton btnDeleteROI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripLabel lblCbbFuncID;
        private System.Windows.Forms.ToolStripComboBox cbbFuncID;
        private System.Windows.Forms.ToolStripButton btnStartDebug;
        private System.Windows.Forms.ToolStripButton btnSaveCVConfig;
        private DragDropTextBox txtDragTxtBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbbModelTasks;
        private System.Windows.Forms.ToolStripButton btnLoadConfig;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
    }
}