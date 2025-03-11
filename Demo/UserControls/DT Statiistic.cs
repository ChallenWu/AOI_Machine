using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoTech
{
    public partial class DT_Statiistic : UserControl
    {
        public DT_Statiistic()
        {
            InitializeComponent();
            Top10_DefaultColor();
            this.BackColor = Color.FromArgb(211, 211, 211);
            Chart_DT.BackColor = Color.Transparent;
            Chart_DT.ChartAreas[0].BackColor = Color.Transparent;
        }
        private Color[] _top10;
        private DataTable _sourceData;
        private PieStyles _pieStyle;
        private string _errorMessage;
        private DataGridView dataGridView1;
        private Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_DT;
        private TableLayoutPanel tableLayoutPanel1;
        private string _dtLabel; 
        [Description("Control refresh complete event")]
        public event Action<DataTable> Refreshed_event; 
        #region 属性   
        //[Category("GUI属性")]
        //[Browsable(true)]
        //[DefaultValue("")]
        [Description("Set the Top10 color array")]
        public Color[] Top10
        {
            get
            {
                return _top10;
            } 
            set
            {
                if(value!=null&& value.Length>=10)
                    _top10 = value;
            }
        }
        //[Category("GUI属性")]
        //[Browsable(true)]
        //[DefaultValue("")]
        [Description("Setting up the data source")]
        public DataTable DataSourceTable
        {
            get
            {
                return _sourceData;
            }
            set
            {
                _sourceData = value;
            }
        }
        //[Category("GUI属性")]
        //[Browsable(true)]
        //[DefaultValue("")]
        [Description("Set up pie chart format")]
        public PieStyles PieStyle
        {
            get
            {
                return _pieStyle;
            }
            set
            {
                _pieStyle = value;
            }
        }
        //[Category("GUI属性")]
        //[Browsable(true)]
        //[DefaultValue("")]
        [Description("Internal error message")]

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
            }
        }
        //[Category("GUI属性")]
        //[Browsable(true)]
        //[DefaultValue("")]
        [Description("Control tag name")]
        public string DT_Label
        {
            get
            {
                return _dtLabel;
            }
            set
            {
                if(value!=null&& value.Length>5)
                {
                    _dtLabel = value;
                    this.label1.Text = value;
                }       
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 无源刷新控件表格、扇图
        /// </summary>
        /// <param name="pieStyle">扇图数据源</param>
        public void Refresh(PieStyles pieStyle= PieStyles.Time)
        {
            InternalRefreshChartAndDGV(DataSourceTable, pieStyle);
        }
        /// <summary>
        ///  有源刷新控件表格、扇图
        /// </summary>
        /// <param name="SourceData">数据表格</param>
        /// <param name="pieStyle">扇图数据源</param>
        public void Refresh(DataTable SourceData,PieStyles pieStyle= PieStyles.Time)
        {
            DataSourceTable = SourceData;
            InternalRefreshChartAndDGV(SourceData, pieStyle); 
        }
        private void InternalRefreshChartAndDGV(DataTable SourceData, PieStyles pieStyle = PieStyles.Time)
        {
            double allTime = 0;
            int allCount = 0;
            try
            {
                
                DataTable Tem = new DataTable();     //数据转换数据表
                #region DataGrideView
                DataView dv = SourceData.DefaultView;
                if(pieStyle== PieStyles.Time)
                {
                    dv.Sort = SourceData.Columns[1].Caption;
                }
                else
                {
                    dv.Sort = SourceData.Columns[2].Caption;
                }
                SourceData = dv.ToTable(); 
                DataColumn color = new DataColumn(" ",typeof(System.String));
                Tem.Columns.Add(color);

                for (int i = 0; i < SourceData.Columns.Count; i++)
                {
                    DataColumn dc1 = new DataColumn(SourceData.Columns[i].Caption,SourceData.Columns[i].DataType);
                    Tem.Columns.Add(dc1);
                } 

                //Tem.Columns.AddRange(new DataColumn[] { SourceData.Columns[0], SourceData.Columns[1], SourceData.Columns[2] });
                for (int i = 0; i < SourceData.Rows.Count; i++)
                {
                    allCount += Convert.ToInt32( SourceData.Rows[SourceData.Rows.Count - 1 - i][2]);
                    allTime += (double)SourceData.Rows[SourceData.Rows.Count - 1 - i][1];
                    Tem.Rows.Add(Tem.NewRow());
                    Tem.Rows[i][0] = null;
                    Tem.Rows[i][1] = SourceData.Rows[SourceData.Rows.Count-1-i][0];
                    Tem.Rows[i][2] = SourceData.Rows[SourceData.Rows.Count-1-i][1];
                    Tem.Rows[i][3] = SourceData.Rows[SourceData.Rows.Count-1 - i][2];
                }
                Tem.Columns[1].ColumnName = "Error";
                Tem.Columns[2].ColumnName = "TotalTime (Min)";
                Tem.Columns[3].ColumnName = "Count Total";

                dataGridView1.DataSource = Tem;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToOrderColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.ScrollBars = ScrollBars.None;
                dataGridView1.Enabled = false;
                //dataGridView1.Columns[0].Width = 50;  固定列宽
                dataGridView1.AutoSizeColumnsMode =  DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns[0].FillWeight = 10; //百分比列宽
                dataGridView1.Columns[1].FillWeight = 40; 
                dataGridView1.Columns[2].FillWeight = 30;
                dataGridView1.Columns[3].FillWeight = 20;
                //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedHeaders;
                int sourceRows = Tem.Rows.Count;
                sourceRows = sourceRows > _top10.Length ? _top10.Length : sourceRows;
                for (int i = 0; i < sourceRows; i++)
                {
                    //dataGridView1.RowHeadersDefaultCellStyle.BackColor = _top10[i];
                    dataGridView1.Rows[i].Cells[0].Style.BackColor = _top10[i];
                }
                
                dataGridView1.ClearSelection();

                #endregion

                #region ChartPie
                Chart_DT.Series[0].Points.Clear();
                Chart_DT.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
                Chart_DT.Series[0].IsVisibleInLegend = false;
                //Chart_DT.Series[0].IsValueShownAsLabel= true;
                Chart_DT.BackColor = Color.Transparent;
                
                for (int i = 0; i < Tem.Rows.Count; i++)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint dp = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                    if (pieStyle == PieStyles.Time)
                    {
                        double temD = Convert.ToDouble(Tem.Rows[i][2]);
                        dp.YValues =new double[] { temD };
                        dp.Label ="" +((allTime == 0) ? "0%" : (temD / allTime*100).ToString("f0") + "%")+"";
                        
                    }
                    else
                    {
                        double temI = Convert.ToDouble(Tem.Rows[i][3]);
                        dp.YValues = new double[] { temI };
                        dp.Label = (allCount == 0) ? "0%" : (temI / allCount*100).ToString("f0") + "%";
                        
                    }         
                    dp.Color = _top10[i];
                    //Chart_DT.Series[0].LabelAngle = 30;
                    //Chart_DT.Series[0].Label.PadRight = 30;
                    Chart_DT.Series[0].Points.Add(dp);
                }

                //Chart_DT.Series[0]["PieLabelStyle"] = "Outside";
                Chart_DT.Series[0]["PieStartAngle"] = "270";  //设置起始角度
                Chart_DT.Series[0].LabelForeColor = Color.White;
                Chart_DT.Series[0].Font = new System.Drawing.Font("Times New Roman", 16f);
                Chart_DT.BackColor = Color.Transparent;
                Chart_DT.ChartAreas[0].BackColor = Color.Transparent;
                #endregion



                if (Refreshed_event != null)  //刷新完成事件
                    Refreshed_event(SourceData);
                ErrorMessage = "";
            }
            catch (Exception e)
            {
                ErrorMessage = e.ToString();
            }
        }
        /// <summary>
        /// 返回控件当前数据源
        /// </summary>
        /// <returns></returns>
        public DataTable GetData()
        {
            return this._sourceData;
        }
        #endregion
        
        private void Top10_DefaultColor()
        {
            _top10 = new Color[10];
            _top10[0] = Color.FromArgb(111, 121, 128);
            _top10[1] = Color.FromArgb(84,151,193);
            _top10[2] = Color.FromArgb(83,172,122);
            _top10[3] = Color.FromArgb(248,195,92);
            _top10[4] = Color.FromArgb(243,150,91);
            _top10[5] = Color.FromArgb(228,94,105);   
            _top10[6] = Color.FromArgb(125,114,187);
            _top10[7] = Color.FromArgb(76,170,233);
            _top10[8] = Color.FromArgb(102,217,56);
            _top10[9] = Color.FromArgb(194,72,134);
            
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.Chart_DT = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_DT)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 42);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(244, 280);
            this.dataGridView1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 39);
            this.label1.TabIndex = 4;
            this.label1.Text = "Downtime Statistic(Top10)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Chart_DT
            // 
            chartArea1.Name = "ChartArea1";
            this.Chart_DT.ChartAreas.Add(chartArea1);
            this.Chart_DT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Chart_DT.Location = new System.Drawing.Point(253, 42);
            this.Chart_DT.Name = "Chart_DT";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.IsVisibleInLegend = false;
            series1.Name = "Series1";
            this.Chart_DT.Series.Add(series1);
            this.Chart_DT.Size = new System.Drawing.Size(244, 280);
            this.Chart_DT.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Chart_DT, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 325);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // DT_Statiistic
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DT_Statiistic";
            this.Size = new System.Drawing.Size(500, 325);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_DT)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }
    }
    public enum PieStyles
    {
        Time,
        Count,
    }
}
