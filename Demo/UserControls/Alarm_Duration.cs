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
    public partial class Alarm_Duration : UserControl
    {
        public Alarm_Duration()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(211, 211, 211);
            this.Chart_Duration.ChartAreas[0].BackColor = Color.Transparent;
            this.Chart_Duration.BackColor = Color.Transparent;
            this._columnColor = Color.FromArgb(239, 127, 126);
        }
        private DataTable _dataSource;
        private Color _columnColor;
        private string _errorMessage;
        private Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_Duration;
        private TableLayoutPanel tableLayoutPanel1;
        private string _adLabel;
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
                return _dataSource;
            }
            set
            {
                _dataSource = value;
            }
        }
        //[Category("GUI属性")]
        //[Browsable(true)]
        //[DefaultValue("")]
        [Description("条形图条颜色")] 
        public Color ColumnColor
        {
            get
            {
                return _columnColor;
            }
            set
            {
                _columnColor = value;
            }
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
        //[Category("GUI属性")]
        //[Browsable(true)]
        //[DefaultValue("")]
        [Description("控件标签名")]
        public string AD_Label
        {
            get
            {
                return _adLabel;
            }
            set
            {
                if(value != null && value.Length>5)
                {
                    _adLabel = value;
                    this.label1.Text = value;
                }
            }
        }
        #endregion  
        #region 方法
        /// <summary>
        /// 无源刷新
        /// </summary>
        public void Refresh()
        {
            InternalRefreshChart(_dataSource);
        }
        /// <summary>
        /// 有源刷新
        /// </summary>
        /// <param name="SourceData">数据源</param>
        public void Refresh(DataTable SourceData)
        {
            this.DataSourceTable = SourceData;
            InternalRefreshChart(_dataSource);
        }
        private void InternalRefreshChart(DataTable SourceData)
        {
           try
            {
                DataTable Tem = new DataTable();
                DataColumn dc = new DataColumn("Alarm Duration", typeof(System.String));
                DataColumn dc1 = new DataColumn("Count Total", typeof(System.Int32));
                Tem.Columns.Add(dc);
                Tem.Columns.Add(dc1);
                string[] temStr = new string[] {"0-4","5-9","10-14","15-19","20-24","25+" };
                int[] temint = new int[6];
                for (int i = 0; i < SourceData.Rows.Count; i++)
                {
                    int tt = (int)SourceData.Rows[i][0];
                    if (tt >= 0 && tt < 5)
                        temint[0] += Convert.ToInt32( SourceData.Rows[i][1]);
                    else if(tt<10)
                        temint[1] += Convert.ToInt32(SourceData.Rows[i][1]);
                    else if (tt < 15)
                        temint[2] += Convert.ToInt32(SourceData.Rows[i][1]);
                    else if (tt < 20)
                        temint[3] += Convert.ToInt32(SourceData.Rows[i][1]);
                    else if (tt < 25)
                        temint[4] += Convert.ToInt32(SourceData.Rows[i][1]);
                    else 
                        temint[5] += Convert.ToInt32(SourceData.Rows[i][1]);
                }
                for (int i = 0; i < temint.Length; i++)
                {
                    Tem.Rows.Add(Tem.NewRow());
                    Tem.Rows[i][0] = temStr[i];
                    Tem.Rows[i][1] = temint[i];
                }



                #region Chart
                Chart_Duration.Series[0].IsVisibleInLegend = false;
                Chart_Duration.Series[0].Points.Clear();
                for (int i = 0; i < Tem.Rows.Count; i++)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint dp = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                  
                    double temD = (int)Tem.Rows[i][1];
                    dp.YValues = new double[] { temD };
                    dp.Label = ((int)Tem.Rows[i][1]).ToString();
                    //dp.XValue = (string)SourceData.Rows[i][0];
                    dp.AxisLabel = (string)Tem.Rows[i][0];
                    
                    dp.Color = _columnColor;
                    //Chart_DT.Series[0].LabelAngle = 30;
                    //Chart_DT.Series[0].Label.PadRight = 30;
                    Chart_Duration.Series[0].Points.Add(dp);
                }
                Chart_Duration.Series[0]["LabelStyle"] = "Bottom";
                //Chart_DT.Series[0]["PieLabelStyle"] = "Outside";
                //Chart_Duration.Series[0]["PieStartAngle"] = "270";  //设置起始角度
                Chart_Duration.Series[0].LabelForeColor = Color.White;
                Chart_Duration.Series[0].Font = new System.Drawing.Font("Times New Roman", 16f);
                
                //调整网格线





                #endregion
                if (Refreshed_event != null)
                    Refreshed_event(_dataSource);
                _errorMessage = "";
            }
            catch(Exception e)
            {
                _errorMessage = e.ToString();
            }
        }
        /// <summary>
        /// 返回当前图表数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetData()
        {
            return _dataSource;
        }
        #endregion

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.Chart_Duration = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Duration)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(494, 57);
            this.label1.TabIndex = 3;
            this.label1.Text = "Alarm Duration";
            // 
            // Chart_Duration
            // 
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.Chart_Duration.ChartAreas.Add(chartArea1);
            this.Chart_Duration.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.Chart_Duration.Legends.Add(legend1);
            this.Chart_Duration.Location = new System.Drawing.Point(3, 3);
            this.Chart_Duration.Name = "Chart_Duration";
            series1.ChartArea = "ChartArea1";
            series1.CustomProperties = "LabelStyle=BottomRight";
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 6;
            this.Chart_Duration.Series.Add(series1);
            this.Chart_Duration.Size = new System.Drawing.Size(494, 262);
            this.Chart_Duration.TabIndex = 2;
            this.Chart_Duration.Text = "chart1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Chart_Duration, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(500, 325);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // Alarm_Duration
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Alarm_Duration";
            this.Size = new System.Drawing.Size(500, 325);
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Duration)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
