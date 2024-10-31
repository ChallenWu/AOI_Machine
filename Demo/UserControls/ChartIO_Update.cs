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
    public partial class ChartIO_Update : UserControl
    {

        public ChartIO_Update()
        {
            InitializeComponent();
            //this.doubleTrackBar1.Value1Changed += DoubleTrackChange;
            //this.doubleTrackBar1.Value2Changed += DoubleTrackChange;
            //this.doubleTrackBar2.Value1Changed += DoubleTrackChange2;
            //this.doubleTrackBar2.Value2Changed += DoubleTrackChange2;

            this.doubleTrackBar1.UpdateDT += UpdateDTPick1;
            this.doubleTrackBar2.UpdateDT += UpdateDTPick2;
        }

        #region 事件定义

        [Description("BarValueChange事件")]
        public event Action<DateTime, DateTime> DoubleTrackValueChange;
        [Description("BarValueChange事件")]
        public event Action<DateTime, DateTime> DoubleTrackValueChange2;
        #endregion


        #region 属性

        //private DataTable _dataSource;
        //[Description("数据源")]
        //public DataTable DataSourceTable
        //{
        //    get
        //    {

        //        return _dataSource;
        //    }
        //    set
        //    {
        //        _dataSource = value;
        //    }

        //} 

        private bool _isSelectIO = true;
        public bool IsSelectIO
        {

            get { return _isSelectIO; }
            set
            {
                _isSelectIO = value;



            }
        }


        private bool _isSelectDay = true;
        public bool IsSelectDay
        {

            get { return _isSelectDay; }
            set
            {
                _isSelectDay = value;

            }

        }
        #endregion


        #region 滑动条1
        private DateTime _BarEndTime = DateTime.Now;
        [Description("最新时间")]
        public DateTime DoubleBarCurrentTime  //DoubleBarNewestTime
        {
            get
            {
                return _BarEndTime;
            }
            set
            {
                _BarEndTime = value;
                this.doubleTrackBar1.DataTableNewestTime = _BarEndTime.ToString("MM/dd/yyyy HH:mm:ss");
            }

        }
        [Description("Slider1 Start时间")]
        public DateTime TrackStartTime
        {
            //get
            //{
            //    return this.doubleTrackBar1.DateStartValue1;
            //}

            get
            {
                dateTimePicker1_1.Text = this.doubleTrackBar1.DateStartValue1.ToString(DateTimeFormate);
                return this.doubleTrackBar1.DateStartValue1;

            }
            set
            {
                if (dateTimePicker1_1.Value < DoubleBarCurrentTime.AddDays(-7) || dateTimePicker1_1.Value > TrackEndTime)
                {

                }
                else
                {
                    dateTimePicker1_1.Value = value;
                    this.doubleTrackBar1.DateStartValue1 = dateTimePicker1_1.Value;
                }
            }
        }
        [Description("Slider2 Endtime时间")]
        public DateTime TrackEndTime
        {
            //get
            //{
            //    return this.doubleTrackBar1.DateEndValue2;
            //}

            get
            {
                dateTimePicker1_2.Text = this.doubleTrackBar1.DateEndValue2.ToString(DateTimeFormate);
                return this.doubleTrackBar1.DateEndValue2;
            }
            set
            {

                if (dateTimePicker1_2.Value < dateTimePicker1_1.Value || dateTimePicker1_2.Value > DoubleBarCurrentTime)
                {
                    this.doubleTrackBar1.DateEndValue2 = DoubleBarCurrentTime;
                }
                else
                {
                    dateTimePicker1_2.Value = value;
                    this.doubleTrackBar1.DateEndValue2 = dateTimePicker1_2.Value;
                }


            }
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



        #region 滑动条2
        private DateTime _BarEndTime2;
        [Description("最新时间")]
        public DateTime DoubleBarCurrentTime2
        {
            get
            {
                return _BarEndTime2;
            }
            set
            {
                _BarEndTime2 = value;
                this.doubleTrackBar2.DataTableNewestTime = _BarEndTime2.ToString("MM/dd/yyyy HH:mm:ss");
            }

        }
        [Description("Slider1 Start时间")]
        public DateTime TrackStartTime2
        {
            //get
            //{
            //    return this.doubleTrackBar2.DateStartValue1;
            //}
            get
            {
                dateTimePicker2_1.Text = this.doubleTrackBar2.DateStartValue1.ToString(DateTimeFormate);
                return this.doubleTrackBar2.DateStartValue1;
            }
            set
            {
                if (dateTimePicker2_1.Value < DoubleBarCurrentTime2.AddDays(-7) || dateTimePicker2_1.Value > TrackEndTime2)
                { }
                else
                {
                    dateTimePicker2_1.Value = value;
                    this.doubleTrackBar2.DateStartValue1 = dateTimePicker2_1.Value;
                }
            }
        }
        [Description("Slider2 Endtime时间")]
        public DateTime TrackEndTime2
        {
            //get
            //{
            //    return this.doubleTrackBar2.DateEndValue2;
            //}

            get
            {
                dateTimePicker2_2.Text = this.doubleTrackBar2.DateEndValue2.ToString(DateTimeFormate);
                return this.doubleTrackBar2.DateEndValue2;
            }
            set
            {

                if (dateTimePicker2_2.Value < dateTimePicker2_1.Value || dateTimePicker2_2.Value > DoubleBarCurrentTime2)
                {
                    this.doubleTrackBar2.DateEndValue2 = DoubleBarCurrentTime;
                }
                else
                {
                    dateTimePicker2_2.Value = value;
                    this.doubleTrackBar2.DateEndValue2 = dateTimePicker2_2.Value;
                }


            }
        }

        [Description("Bar时间跨度，天")]
        public int CheckDays2
        {
            get
            {
                return this.doubleTrackBar2.CheckDays;
            }
            set
            {
                this.doubleTrackBar2.CheckDays = value;
            }
        }






        #endregion







        private void ChartLine_Update_Load(object sender, EventArgs e)
        {
            DateTime dTime = DateTime.Now;
            DoubleBarCurrentTime = dTime;
            DoubleBarCurrentTime2 = dTime;

        }


        public void Refresh(Dictionary<string, double>[] dicCT, Dictionary<string, int>[] dicCount)
        {
            InternalRefreshChart(dicCount, dicCT);
        }



        public void Clear()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
        }


        /*用于表2中的DS/NS数量统计；*/
        private void InternalRefreshChart(Dictionary<string, int>[] dic, Dictionary<string, double>[] dicCT)
        {

            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            foreach (KeyValuePair<string, int> kvp in dic[0])
            {
                chart2.Series[0].Points.AddXY(kvp.Key, kvp.Value);
            }
            foreach (KeyValuePair<string, int> kvp2 in dic[1])
            {
                chart2.Series[1].Points.AddXY(kvp2.Key, kvp2.Value);
            }

            /***********************************************/
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            foreach (KeyValuePair<string, double> kvp in dicCT[0])
            {
                chart1.Series[0].Points.AddXY(kvp.Key, kvp.Value);
            }
            foreach (KeyValuePair<string, double> kvp2 in dicCT[1])
            {
                chart1.Series[1].Points.AddXY(kvp2.Key, kvp2.Value);
            }



        }





        public void Refresh(Dictionary<string, double>[] dicCT)
        {
            InternalRefreshChart_CT(dicCT);
        }

        public void Refresh(Dictionary<string, int>[] dicCount)
        {
            InternalRefreshChart_Count(dicCount);
        }
        private void InternalRefreshChart_CT(Dictionary<string, double>[] dicCT)
        {
            /***********************************************/
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            foreach (KeyValuePair<string, double> kvp in dicCT[0])
            {
                chart1.Series[0].Points.AddXY(kvp.Key, kvp.Value);
            }
            foreach (KeyValuePair<string, double> kvp2 in dicCT[1])
            {
                chart1.Series[1].Points.AddXY(kvp2.Key, kvp2.Value);
            }
        }

        private void InternalRefreshChart_Count(Dictionary<string, int>[] dic)
        {
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            foreach (KeyValuePair<string, int> kvp in dic[0])
            {
                chart2.Series[0].Points.AddXY(kvp.Key, kvp.Value);
            }
            foreach (KeyValuePair<string, int> kvp2 in dic[1])
            {
                chart2.Series[1].Points.AddXY(kvp2.Key, kvp2.Value);
            }
        }

        private void DoubleTrackChange()
        {
            if (DoubleTrackValueChange != null)
                DoubleTrackValueChange(this.doubleTrackBar1.DateStartValue1, this.doubleTrackBar1.DateEndValue2);
        }

        private void DoubleTrackChange2()
        {
            if (DoubleTrackValueChange2 != null)
                DoubleTrackValueChange2(this.doubleTrackBar2.DateStartValue1, this.doubleTrackBar2.DateEndValue2);
        }





        private void SetChart1_Column()
        {
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;
            chart1.Series[1].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;




        }
        private void SetChart1_Line()
        {
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            chart1.Series[1].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;


            //chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0%";
            //chart1.Series[0].LabelFormat = "0%";
        }






        private void btn_IO_Click(object sender, EventArgs e)
        {
            _isSelectIO = true;
            btn_IO.BackColor = Color.White;
            btn_Tossing.BackColor = Color.LightGray;


            SetChart1_Line();
        }

        private void btn_Tossing_Click(object sender, EventArgs e)
        {
            _isSelectIO = false;
            btn_IO.BackColor = Color.LightGray;
            btn_Tossing.BackColor = Color.White;
            SetChart1_Column();
        }

        private void btn_Day_Click(object sender, EventArgs e)
        {
            _isSelectDay = true;
            btn_Day.BackColor = Color.White;
            btn_Hour.BackColor = Color.LightGray;
        }

        private void btn_Hour_Click(object sender, EventArgs e)
        {
            _isSelectDay = false;
            btn_Day.BackColor = Color.LightGray;
            btn_Hour.BackColor = Color.White;

        }


        #region 时间选择功能；




        private void dateTimePicker1_1_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtTemp = dateTimePicker1_1.Value;
            if (dtTemp <= doubleTrackBar1.DateEndValue2 && dtTemp >= DoubleBarCurrentTime.AddDays(-7.5))
            {
                doubleTrackBar1.DateStartValue1 = dateTimePicker1_1.Value;
                if (UpdateChart != null)
                {
                    UpdateChart();
                }
            }
        }

        private void dateTimePicker1_2_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtTemp = dateTimePicker1_2.Value;
            if (dtTemp <= DoubleBarCurrentTime && dtTemp >= doubleTrackBar1.DateStartValue1)
            {
                doubleTrackBar1.DateEndValue2 = dateTimePicker1_2.Value;

            }
        }

        private void dateTimePicker2_1_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtTemp = dateTimePicker2_1.Value;
            if (dtTemp <= doubleTrackBar2.DateEndValue2 && dtTemp >= DoubleBarCurrentTime.AddDays(-7.5))
            {
                doubleTrackBar2.DateStartValue1 = dateTimePicker2_1.Value;

            }
            if (UpdateChart != null)
            {
                UpdateChart();
            }
        }

        private void dateTimePicker2_2_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtTemp = dateTimePicker2_2.Value;
            if (dtTemp <= DoubleBarCurrentTime && dtTemp >= doubleTrackBar1.DateStartValue1)
            {
                doubleTrackBar2.DateEndValue2 = dateTimePicker2_2.Value;


            }
        }

        public bool IsSelectTime
        {
            get; set;
        }
        private void dateTimePicker1_1_Enter(object sender, EventArgs e)
        {
            IsSelectTime = true;
        }

        private void dateTimePicker2_1_Enter(object sender, EventArgs e)
        {
            IsSelectTime = true;
        }

        public event Action UpdateChart;
        public event Action<DateTime> UpdateChart2;
        public event Action<DateTime, DateTime> UpdateChart1Event;
        public event Action<DateTime, DateTime> UpdateChart2Event;
        private const string DateTimeFormate = "MM/dd/yyyy HH:mm:ss";//定义时间格式；
        public void UpdateDTPick1(DateTime dtStart, DateTime dtEnd)
        {
            dateTimePicker1_1.Text = dtStart.ToString(DateTimeFormate);
            dateTimePicker1_2.Text = dtEnd.ToString(DateTimeFormate);
        }
        public void UpdateDTPick2(DateTime dtStart, DateTime dtEnd)
        {
            dateTimePicker2_1.Text = dtStart.ToString(DateTimeFormate);
            dateTimePicker2_2.Text = dtEnd.ToString(DateTimeFormate);
        }


        private void chart1_DoubleClick(object sender, EventArgs e)
        {
            IsSelectTime = false;
        }



        #endregion

        private void doubleTrackBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
