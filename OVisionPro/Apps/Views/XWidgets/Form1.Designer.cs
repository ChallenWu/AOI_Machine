
namespace OVisionPro.Apps.Views.XWidgets
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnCreateROI = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteROI = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1057, 582);
            this.panel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
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
            this.toolStrip1.Size = new System.Drawing.Size(1057, 33);
            this.toolStrip1.TabIndex = 1;
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
            // 
            // txtDragTxtBox
            // 
            this.txtDragTxtBox.AllowedExtensions = new string[0];
            this.txtDragTxtBox.AutoSize = false;
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
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 33);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.propertyGrid1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(309, 549);
            this.panel2.TabIndex = 2;
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
            this.panel4.Size = new System.Drawing.Size(309, 38);
            this.panel4.TabIndex = 1;
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
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 38);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(309, 511);
            this.propertyGrid1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.toolStrip2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(309, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(748, 549);
            this.panel3.TabIndex = 3;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCreateROI,
            this.btnDeleteROI,
            this.toolStripSeparator1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 524);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(748, 25);
            this.toolStrip2.TabIndex = 1;
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
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(748, 524);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 582);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel lblCbbFuncID;
        private System.Windows.Forms.ToolStripComboBox cbbFuncID;
        private System.Windows.Forms.ToolStripButton btnSaveCVConfig;
        private System.Windows.Forms.ToolStripButton btnStartDebug;
        private DragDropTextBox txtDragTxtBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbbModelTasks;
        private System.Windows.Forms.ToolStripButton btnLoadConfig;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnCreateROI;
        private System.Windows.Forms.ToolStripButton btnDeleteROI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}