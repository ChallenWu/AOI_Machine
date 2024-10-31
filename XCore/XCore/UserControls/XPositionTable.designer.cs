namespace XCore
{
    partial class XPositionTable
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Btn_Add = new System.Windows.Forms.ToolStripButton();
            this.Btn_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_Teach = new System.Windows.Forms.ToolStripButton();
            this.btn_CancelTeach = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_Go = new System.Windows.Forms.ToolStripButton();
            this.Combo_Vel = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.Btn_Save = new System.Windows.Forms.ToolStripButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_Add,
            this.Btn_Delete,
            this.toolStripSeparator1,
            this.Btn_Teach,
            this.btn_CancelTeach,
            this.toolStripSeparator4,
            this.Btn_Go,
            this.Combo_Vel,
            this.toolStripLabel1,
            this.Btn_Save});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(491, 37);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Btn_Add
            // 
            this.Btn_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_Add.Image = global::XCore.Properties.Resources._add;
            this.Btn_Add.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(34, 34);
            this.Btn_Add.Text = "添加点";
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // Btn_Delete
            // 
            this.Btn_Delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_Delete.Image = global::XCore.Properties.Resources._minus;
            this.Btn_Delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Delete.Name = "Btn_Delete";
            this.Btn_Delete.Size = new System.Drawing.Size(34, 34);
            this.Btn_Delete.Text = "删除选中点";
            this.Btn_Delete.Click += new System.EventHandler(this.Btn_Delete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 37);
            // 
            // Btn_Teach
            // 
            this.Btn_Teach.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_Teach.Image = global::XCore.Properties.Resources._teach;
            this.Btn_Teach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Teach.Name = "Btn_Teach";
            this.Btn_Teach.Size = new System.Drawing.Size(34, 34);
            this.Btn_Teach.Text = "示教";
            this.Btn_Teach.Click += new System.EventHandler(this.Btn_Teach_Click);
            // 
            // btn_CancelTeach
            // 
            this.btn_CancelTeach.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_CancelTeach.Image = global::XCore.Properties.Resources._rotate_antiClock;
            this.btn_CancelTeach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_CancelTeach.Name = "btn_CancelTeach";
            this.btn_CancelTeach.Size = new System.Drawing.Size(34, 34);
            this.btn_CancelTeach.Text = "撤销";
            this.btn_CancelTeach.Click += new System.EventHandler(this.btn_CancelTeach_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 37);
            // 
            // Btn_Go
            // 
            this.Btn_Go.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_Go.Image = global::XCore.Properties.Resources._start;
            this.Btn_Go.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Go.Name = "Btn_Go";
            this.Btn_Go.Size = new System.Drawing.Size(34, 34);
            this.Btn_Go.Text = "再现";
            this.Btn_Go.Click += new System.EventHandler(this.Btn_Go_Click);
            // 
            // Combo_Vel
            // 
            this.Combo_Vel.Name = "Combo_Vel";
            this.Combo_Vel.Size = new System.Drawing.Size(100, 37);
            this.Combo_Vel.SelectedIndexChanged += new System.EventHandler(this.Combo_Vel_SelectedIndexChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(41, 34);
            this.toolStripLabel1.Text = "mm/s";
            // 
            // Btn_Save
            // 
            this.Btn_Save.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Btn_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Btn_Save.Image = global::XCore.Properties.Resources._ok;
            this.Btn_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Save.Name = "Btn_Save";
            this.Btn_Save.Size = new System.Drawing.Size(34, 34);
            this.Btn_Save.Text = "保存";
            this.Btn_Save.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dataGridView1.Size = new System.Drawing.Size(488, 168);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 208);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(491, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // XPositionTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "XPositionTable";
            this.Size = new System.Drawing.Size(491, 230);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Btn_Add;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton Btn_Teach;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Btn_Delete;
        private System.Windows.Forms.ToolStripButton Btn_Go;
        private System.Windows.Forms.ToolStripComboBox Combo_Vel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton Btn_Save;
        private System.Windows.Forms.ToolStripButton btn_CancelTeach;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
