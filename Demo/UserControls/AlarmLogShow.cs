using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.UserControls;

namespace BoTech
{
    public partial class AlarmLogShow : UserControl
    {
        public AlarmLogShow()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(211, 211, 211);
            this.dataGridView1.BackgroundColor = Color.FromArgb(211, 211, 211);
        }
        private DataTable _dataSourceTable;
        private int[] _showColumn;
        private DataGridView dataGridView1;
        private string _errorMessage;
        [Description("控件刷新完事件")]
        public event Action<DataTable> Refreshed_event;

        #region 属性
        //[Category("GUI属性")]
        //[Browsable(true)]
        //[DefaultValue("")]
        [Description("数据源")]
        public DataTable DataSourceTable
        {
            get
            {
                return _dataSourceTable;
            }
            set
            {
                _dataSourceTable = value;
            }
        }
        //[Category("GUI属性")]
        //[Browsable(true)]
        //[DefaultValue("")]
        [Description("要显示的列")]
        public int[] ShowColumn
        {
            get { return _showColumn; }
            set { _showColumn = value; }
        }
        //[Category("GUI属性")]
        //[Browsable(true)]
        //[DefaultValue("")]
        [Description("报错信息")]
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 无源刷新
        /// </summary>
        public void Refresh()
        {
            InternalDGVRefresh(_dataSourceTable);
        }
        /// <summary>
        /// 有源刷新
        /// </summary>
        /// <param name="SourceData">数据源</param>
        public void Refresh(DataTable SourceData)
        {
            DataSourceTable = SourceData;
            InternalDGVRefresh(SourceData);
        }
        public DataTable GetData()
        {
            return _dataSourceTable;
        }
        private void InternalDGVRefresh(DataTable SourceData)
        {
            try
            {
                DataTable tem = new DataTable();
                if(_showColumn==null|| _showColumn.Length<4)
                {
                    tem = SourceData;
                }
                else
                {
                    for (int i = 0; i < _showColumn.Length; i++)
                    {
                        DataColumn dc1 = new DataColumn(SourceData.Columns[_showColumn[i]].Caption, SourceData.Columns[_showColumn[i]].DataType);
                        tem.Columns.Add(dc1);
                    }
                    for (int i = 0; i < SourceData.Rows.Count; i++)
                    {                      
                        tem.Rows.Add(tem.NewRow());
                        for (int j = 0; j < _showColumn.Length; j++)
                        {
                            tem.Rows[i][j] = SourceData.Rows[i][_showColumn[j]];    
                        }       
                    }
                }
                dataGridView1.DataSource = tem;
                
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToOrderColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                
                dataGridView1.ScrollBars = ScrollBars.Vertical;
                //dataGridView1.Enabled = false;

                dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(211, 211, 211);
                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(211, 211, 211);
                dataGridView1.EnableHeadersVisualStyles = false;
                ////dataGridView1.Columns[0].Width = 50;  固定列宽
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //dataGridView1.Columns[0].FillWeight = 10; //百分比列宽
                //dataGridView1.Columns[1].FillWeight = 40;
                //dataGridView1.Columns[2].FillWeight = 25;
                //dataGridView1.Columns[3].FillWeight = 25;

                dataGridView1.ClearSelection();

                _errorMessage = "";
                if (Refreshed_event != null)
                    Refreshed_event(SourceData);
            }
            catch (Exception e)
            {
                _errorMessage = e.ToString();
            }
        }
        #endregion

        #region 事件
        #endregion

        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(871, 303);
            this.dataGridView1.TabIndex = 1;
            // 
            // AlarmLogShow
            // 
            this.Controls.Add(this.dataGridView1);
            this.Name = "AlarmLogShow";
            this.Size = new System.Drawing.Size(871, 303);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
