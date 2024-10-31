namespace Demo
{
    partial class UserAccountManager
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView_AccountInfo = new System.Windows.Forms.DataGridView();
            this.btn_AccountDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AccountInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_AccountInfo
            // 
            this.dataGridView_AccountInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_AccountInfo.Location = new System.Drawing.Point(16, 15);
            this.dataGridView_AccountInfo.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView_AccountInfo.Name = "dataGridView_AccountInfo";
            this.dataGridView_AccountInfo.RowTemplate.Height = 33;
            this.dataGridView_AccountInfo.Size = new System.Drawing.Size(390, 246);
            this.dataGridView_AccountInfo.TabIndex = 0;
            this.dataGridView_AccountInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_AccountInfo_CellClick);
            // 
            // btn_AccountDelete
            // 
            this.btn_AccountDelete.Location = new System.Drawing.Point(422, 114);
            this.btn_AccountDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btn_AccountDelete.Name = "btn_AccountDelete";
            this.btn_AccountDelete.Size = new System.Drawing.Size(93, 53);
            this.btn_AccountDelete.TabIndex = 1;
            this.btn_AccountDelete.Text = "Delete Account";
            this.btn_AccountDelete.UseVisualStyleBackColor = true;
            this.btn_AccountDelete.Click += new System.EventHandler(this.btn_AccountDelete_Click_1);
            // 
            // UserAccountManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_AccountDelete);
            this.Controls.Add(this.dataGridView_AccountInfo);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UserAccountManager";
            this.Size = new System.Drawing.Size(526, 274);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_AccountInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_AccountInfo;
        private System.Windows.Forms.Button btn_AccountDelete;
    }
}
