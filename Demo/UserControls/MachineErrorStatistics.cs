using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.UserControls
{
    public partial class MachineErrorStatistics : UserControl
    {
        #region 变量
        private Dictionary<string, int> _dicErrorData = new Dictionary<string, int>();
        private static List<ErrorStatistics> _listError = new List<ErrorStatistics>();
        private const int Top10Error = 10;
        private Color ControlColor = Color.FromArgb(211, 211, 211);//控件背景色；


        private DataTable _dataSource;
        private DateTime _BarEndTime;
        private string _errorMessage;

        [Description("控件刷新完事件")]
        public Action<DataTable> Refreshed_event;
        [Description("单击空白区域，跳转到ErrorTab界面去")]
        public event Action ClickToErrorTab_Event;
        [Description("BarValueChange事件")]
        public event Action<DateTime, DateTime> DoubleTrackValueChange;



        struct ErrorStatistics
        {
            public string ErrorCode;
            public int ErrorNum;
        }
        #endregion



        #region 属性；
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

        [Description("报错信息")]
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
        }

        [Description("最新时间")]
        public DateTime DoubleBarCurrentTime
        {
            get
            {
                if (_BarEndTime.ToString(DateTimeFormate).Contains("0001"))
                {
                    _BarEndTime = DateTime.Now;
                }
                return _BarEndTime;
            }
            set
            {
                _BarEndTime = value;
                this.doubleTrackBar1.DataTableNewestTime = _BarEndTime.ToString("MM/dd/yyyy HH:mm:ss");
            }

        }
        [Description("BarStart时间")]
        public DateTime TrackStartTime
        {
            get
            {
                dateTimePicker1.Text = this.doubleTrackBar1.DateStartValue1.ToString(DateTimeFormate);
                return this.doubleTrackBar1.DateStartValue1;

            }
            set
            {



                if (dateTimePicker1.Value < DoubleBarCurrentTime.AddDays(-7) || dateTimePicker1.Value > TrackEndTime)
                {

                }
                else
                {

                    this.doubleTrackBar1.DateStartValue1 = dateTimePicker1.Value;
                }



            }
        }
        [Description("BarEndtime时间")]
        public DateTime TrackEndTime
        {
            get
            {
                dateTimePicker2.Text = this.doubleTrackBar1.DateEndValue2.ToString(DateTimeFormate);
                return this.doubleTrackBar1.DateEndValue2;
            }
            set
            {

                if (dateTimePicker2.Value < dateTimePicker1.Value || dateTimePicker2.Value > DoubleBarCurrentTime)
                {

                }
                else
                {

                    this.doubleTrackBar1.DateEndValue2 = dateTimePicker2.Value;
                }


            }
        }

        public bool IsSelectTime
        {
            get; set;
        }




        [Description("Bar时间跨度，天")]
        public int CheckDays
        {
            get
            {
                return this.doubleTrackBar1.CheckDays;
            }
            set
            {
                this.doubleTrackBar1.CheckDays = value;
            }
        }
        #endregion


        public MachineErrorStatistics()
        {
            InitializeComponent();
            this.chart1.BackColor = ControlColor;
            this.chart1.ChartAreas[0].BackColor = ControlColor;
            this.doubleTrackBar1.Value1Changed += DoubleTrackChange;
            this.doubleTrackBar1.Value2Changed += DoubleTrackChange;

            this.doubleTrackBar1.UpdateDT += UpdateDTPick;


        }


        #region 方法
        private const string DateTimeFormate = "MM/dd/yyyy HH:mm:ss";//定义时间格式；
        public void UpdateDTPick(DateTime dtStart, DateTime dtEnd)
        {
            dateTimePicker1.Text = dtStart.ToString(DateTimeFormate);
            dateTimePicker2.Text = dtEnd.ToString(DateTimeFormate);
        }


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


        /// <summary>
        /// 内部刷新表格；
        /// </summary>
        /// <param name="SourceData"></param>
        private void InternalRefreshChart(DataTable SourceData)
        {
            chart1.Series[0].Points.Clear();
            ///如果键值对数量不为空，清空键值对；
            if (_listError.Count > 0)
            {
                _listError.Clear();
                //return;
            }

            if (SourceData == null || SourceData.Rows.Count == 0)
            {
                //数据表为空；
                return;
            }
            try
            {
                for (int i = 0; i < SourceData.Rows.Count; i++)
                {
                    ErrorStatistics es = new ErrorStatistics();
                    es.ErrorCode = SourceData.Rows[i][0].ToString();//日期
                    es.ErrorNum = (int)SourceData.Rows[i][1];//值；

                    _listError.Add(es);
                }



                for (int i = 0; i < Top10Error; i++)
                {
                    string key;
                    int value;
                    if (i < _listError.Count)
                    {
                        key = _listError[i].ErrorCode;
                        value = _listError[i].ErrorNum;
                        chart1.Series[0].Points.AddXY(key, value);
                    }

                }


                if (Refreshed_event != null)
                    Refreshed_event(_dataSource);
                _errorMessage = "";

            }
            catch (Exception ex)
            {
                _errorMessage = ex.ToString();
            }
        }




        private void SelectTime()
        {

        }



        /// <summary>
        /// 返回当前图表数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetData()
        {
            return _dataSource;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        private void chart1_DoubleClick(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, EventArgs.Empty);

            IsSelectTime = false;
        }
        private void DoubleTrackChange()
        {
            if (DoubleTrackValueChange != null)
                DoubleTrackValueChange(this.doubleTrackBar1.DateStartValue1, this.doubleTrackBar1.DateEndValue2);
        }







        #endregion


        public event Action<DateTime, DateTime> UpdateMachineErrorEvent;

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtTemp = dateTimePicker1.Value;
            if (dtTemp <= doubleTrackBar1.DateEndValue2 && dtTemp >= DoubleBarCurrentTime.AddDays(-7.5))
            {
                doubleTrackBar1.DateStartValue1 = dateTimePicker1.Value;
                if (UpdateMachineErrorEvent != null)
                {
                    UpdateMachineErrorEvent(doubleTrackBar1.DateStartValue1, doubleTrackBar1.DateEndValue2);
                }
            }

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtTemp = dateTimePicker2.Value;
            if (dtTemp <= DoubleBarCurrentTime && dtTemp >= doubleTrackBar1.DateStartValue1)
            {
                doubleTrackBar1.DateEndValue2 = dateTimePicker2.Value;
                if (UpdateMachineErrorEvent != null)
                {
                    UpdateMachineErrorEvent(doubleTrackBar1.DateStartValue1, doubleTrackBar1.DateEndValue2);
                }
            }
        }




        private void dateTimePicker2_Enter(object sender, EventArgs e)
        {
            IsSelectTime = true;
        }

    }
}
