namespace PasteLabelMachine.Controls
{
    partial class ViewEditCCD
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_RunTool = new System.Windows.Forms.ToolStripButton();
            this.btn_EditTool = new System.Windows.Forms.ToolStripButton();
            this.btn_EditWithCurentImage = new System.Windows.Forms.ToolStripButton();
            this.cb_Tool = new System.Windows.Forms.ToolStripComboBox();
            this.displayViewInteract1 = new VisionTools.DisplayViewInteract();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(510, 466);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vision Tools";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.displayViewInteract1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 19);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.424072F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.57593F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(510, 447);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_RunTool,
            this.btn_EditTool,
            this.btn_EditWithCurentImage,
            this.cb_Tool});
            this.toolStrip1.Location = new System.Drawing.Point(1, 1);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(508, 32);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_RunTool
            // 
            this.btn_RunTool.AutoSize = false;
            this.btn_RunTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_RunTool.Image = global::Demo.Properties.Resources._start;
            this.btn_RunTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_RunTool.Name = "btn_RunTool";
            this.btn_RunTool.Size = new System.Drawing.Size(60, 60);
            this.btn_RunTool.Text = "Run Tool";
            this.btn_RunTool.Click += new System.EventHandler(this.btn_RunTool_Click);
            // 
            // btn_EditTool
            // 
            this.btn_EditTool.AutoSize = false;
            this.btn_EditTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_EditTool.Image = global::Demo.Properties.Resources.setting;
            this.btn_EditTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_EditTool.Name = "btn_EditTool";
            this.btn_EditTool.Size = new System.Drawing.Size(60, 60);
            this.btn_EditTool.Text = "Edit Tool";
            this.btn_EditTool.Click += new System.EventHandler(this.btn_EditTool_Click);
            // 
            // btn_EditWithCurentImage
            // 
            this.btn_EditWithCurentImage.AutoSize = false;
            this.btn_EditWithCurentImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_EditWithCurentImage.Image = global::Demo.Properties.Resources.vision;
            this.btn_EditWithCurentImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_EditWithCurentImage.Name = "btn_EditWithCurentImage";
            this.btn_EditWithCurentImage.Size = new System.Drawing.Size(60, 60);
            this.btn_EditWithCurentImage.Text = "Set Current Image for Current Tool";
            this.btn_EditWithCurentImage.Click += new System.EventHandler(this.btn_EditWithCurentImage_Click);
            // 
            // cb_Tool
            // 
            this.cb_Tool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Tool.Name = "cb_Tool";
            this.cb_Tool.Size = new System.Drawing.Size(200, 32);
            this.cb_Tool.SelectedIndexChanged += new System.EventHandler(this.cb_Tool_SelectedIndexChanged);
            // 
            // displayViewInteract1
            // 
            this.displayViewInteract1.Auto_Fit = false;
            this.displayViewInteract1.AutoScroll = true;
            this.displayViewInteract1.Coordinate = ECoordinate.Pixel;
            this.displayViewInteract1.CurrentObject = null;
            this.displayViewInteract1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.displayViewInteract1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayViewInteract1.IsDisplayCenterLine = false;
            this.displayViewInteract1.IsDisplayCoordinate = false;
            this.displayViewInteract1.IsMeasurementMode = false;
            this.displayViewInteract1.Location = new System.Drawing.Point(1, 34);
            this.displayViewInteract1.Margin = new System.Windows.Forms.Padding(0);
            this.displayViewInteract1.Name = "displayViewInteract1";
            this.displayViewInteract1.Size = new System.Drawing.Size(508, 412);
            this.displayViewInteract1.TabIndex = 1;
            this.displayViewInteract1.ToastVisible = true;
            this.displayViewInteract1.ToolName = null;
            this.displayViewInteract1.ZoomVisible = false;
            // 
            // ViewEditCCD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ViewEditCCD";
            this.Size = new System.Drawing.Size(510, 466);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_RunTool;
        private System.Windows.Forms.ToolStripButton btn_EditTool;
        internal System.Windows.Forms.ToolStripComboBox cb_Tool;
        internal VisionTools.DisplayViewInteract displayViewInteract1;
        private System.Windows.Forms.ToolStripButton btn_EditWithCurentImage;
    }
}
