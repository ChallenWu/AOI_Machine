
namespace XCore.UserControls
{
    partial class XPositionModelsTable
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.Btn_Add = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteModel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefreshModel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnModelSave = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewModels = new System.Windows.Forms.DataGridView();
            this.dataGridViewPossitions = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cbbPositionModel = new System.Windows.Forms.ToolStripComboBox();
            this.btnAddPosModel = new System.Windows.Forms.ToolStripButton();
            this.btnDeletePosModel = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPosModelSave = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPossitions)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.Btn_Add,
            this.btnDeleteModel,
            this.toolStripSeparator1,
            this.btnRefreshModel,
            this.toolStripSeparator4,
            this.btnModelSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(572, 45);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(56, 42);
            this.toolStripLabel3.Text = "MODELS:";
            // 
            // Btn_Add
            // 
            this.Btn_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_Add.Image = global::XCore.Properties.Resources._add;
            this.Btn_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(34, 42);
            this.Btn_Add.Text = "添加点";
            this.Btn_Add.Click += new System.EventHandler(this.btnAddModel);
            // 
            // btnDeleteModel
            // 
            this.btnDeleteModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteModel.Image = global::XCore.Properties.Resources._minus;
            this.btnDeleteModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteModel.Name = "btnDeleteModel";
            this.btnDeleteModel.Size = new System.Drawing.Size(34, 42);
            this.btnDeleteModel.Text = "删除选中点";
            this.btnDeleteModel.Click += new System.EventHandler(this.btnDeleteModelClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 45);
            // 
            // btnRefreshModel
            // 
            this.btnRefreshModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshModel.Image = global::XCore.Properties.Resources._rotate_antiClock;
            this.btnRefreshModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshModel.Name = "btnRefreshModel";
            this.btnRefreshModel.Size = new System.Drawing.Size(34, 42);
            this.btnRefreshModel.Text = "撤销";
            this.btnRefreshModel.Click += new System.EventHandler(this.btnRefreshModelClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 45);
            // 
            // btnModelSave
            // 
            this.btnModelSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnModelSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnModelSave.Image = global::XCore.Properties.Resources._ok;
            this.btnModelSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModelSave.Name = "btnModelSave";
            this.btnModelSave.Size = new System.Drawing.Size(34, 42);
            this.btnModelSave.Text = "保存";
            this.btnModelSave.Click += new System.EventHandler(this.btnModelSaveClick);
            // 
            // dataGridViewModels
            // 
            this.dataGridViewModels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewModels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewModels.Location = new System.Drawing.Point(3, 48);
            this.dataGridViewModels.Name = "dataGridViewModels";
            this.dataGridViewModels.RowHeadersWidth = 51;
            this.dataGridViewModels.RowTemplate.Height = 23;
            this.dataGridViewModels.Size = new System.Drawing.Size(566, 202);
            this.dataGridViewModels.TabIndex = 2;
            this.dataGridViewModels.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewModels_RowHeaderMouseClick);
            // 
            // dataGridViewPossitions
            // 
            this.dataGridViewPossitions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPossitions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPossitions.Location = new System.Drawing.Point(3, 298);
            this.dataGridViewPossitions.Name = "dataGridViewPossitions";
            this.dataGridViewPossitions.RowHeadersWidth = 51;
            this.dataGridViewPossitions.RowTemplate.Height = 23;
            this.dataGridViewPossitions.Size = new System.Drawing.Size(566, 197);
            this.dataGridViewPossitions.TabIndex = 3;
            this.dataGridViewPossitions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPossitions_CellClick);
            this.dataGridViewPossitions.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewPossitions_RowHeaderMouseClick);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.cbbPositionModel,
            this.btnAddPosModel,
            this.btnDeletePosModel,
            this.toolStripButton1,
            this.toolStripSeparator2,
            this.btnPosModelSave});
            this.toolStrip2.Location = new System.Drawing.Point(0, 253);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(572, 42);
            this.toolStrip2.TabIndex = 4;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(75, 39);
            this.toolStripLabel2.Text = "Select Model";
            // 
            // cbbPositionModel
            // 
            this.cbbPositionModel.Name = "cbbPositionModel";
            this.cbbPositionModel.Size = new System.Drawing.Size(100, 42);
            this.cbbPositionModel.SelectedIndexChanged += new System.EventHandler(this.cbbPositionModel_SelectedIndexChanged);
            // 
            // btnAddPosModel
            // 
            this.btnAddPosModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddPosModel.Image = global::XCore.Properties.Resources._add;
            this.btnAddPosModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddPosModel.Name = "btnAddPosModel";
            this.btnAddPosModel.Size = new System.Drawing.Size(34, 39);
            this.btnAddPosModel.Text = "添加点";
            this.btnAddPosModel.Click += new System.EventHandler(this.btnAddPosModelClick);
            // 
            // btnDeletePosModel
            // 
            this.btnDeletePosModel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeletePosModel.Image = global::XCore.Properties.Resources._minus;
            this.btnDeletePosModel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeletePosModel.Name = "btnDeletePosModel";
            this.btnDeletePosModel.Size = new System.Drawing.Size(34, 39);
            this.btnDeletePosModel.Text = "删除选中点";
            this.btnDeletePosModel.Click += new System.EventHandler(this.btnDeletePosModelClick);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::XCore.Properties.Resources._start;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(34, 39);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 42);
            // 
            // btnPosModelSave
            // 
            this.btnPosModelSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnPosModelSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPosModelSave.Image = global::XCore.Properties.Resources._ok;
            this.btnPosModelSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPosModelSave.Name = "btnPosModelSave";
            this.btnPosModelSave.Size = new System.Drawing.Size(34, 39);
            this.btnPosModelSave.Text = "保存";
            this.btnPosModelSave.Click += new System.EventHandler(this.btnPosModelSaveClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewPossitions, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridViewModels, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.8744F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.1256F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 202F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(572, 498);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // XPositionModelsTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "XPositionModelsTable";
            this.Size = new System.Drawing.Size(572, 498);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPossitions)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Btn_Add;
        private System.Windows.Forms.ToolStripButton btnDeleteModel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnRefreshModel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnModelSave;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.DataGridView dataGridViewModels;
        private System.Windows.Forms.DataGridView dataGridViewPossitions;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cbbPositionModel;
        private System.Windows.Forms.ToolStripButton btnAddPosModel;
        private System.Windows.Forms.ToolStripButton btnDeletePosModel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnPosModelSave;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
