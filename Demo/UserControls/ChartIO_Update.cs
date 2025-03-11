using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Demo.UserControls
{
    public partial class ChartIO_Update : UserControl
    {

        public ChartIO_Update()
        {
            InitializeComponent();
        }


        #region 属性 

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


        #region Chart1
        private DateTime _BarEndTime = DateTime.Now;
        [Description("Newest Time")]
        public DateTime DoubleBarCurrentTime 
        {
            get
            {
                return _BarEndTime;
            }
            set
            {
                _BarEndTime = value;
            }
        }
        [Description("Slider1 StartTime")]
        public DateTime TrackStartTime
        {
            get
            {
                if (!RealTimeTracking)
                {
                    return dateTimePicker1_1.Value;
                }

                dateTimePicker1_1.Value = DateTime.Now.AddDays(-6);
                return dateTimePicker1_1.Value;

            }
            set
            {
                if(dateTimePicker1_1.InvokeRequired)
                {
                    dateTimePicker1_1.Invoke( new Action(() => 
                        {
                            if (dateTimePicker1_1.Value < DoubleBarCurrentTime.AddDays(-6) || dateTimePicker1_1.Value > TrackEndTime)
                            {
                                dateTimePicker1_1.Value = value;
                            }
                            else
                            {
                                dateTimePicker1_1.Value = value;
                            }
                        }));
                }
                else
                if (dateTimePicker1_1.Value < DoubleBarCurrentTime.AddDays(-6) || dateTimePicker1_1.Value > TrackEndTime)
                {
                    dateTimePicker1_1.Value = value;
                }
                else
                {
                    dateTimePicker1_1.Value = value;
                }
            }
        }
        [Description("Slider2 Endtime")]
        public DateTime TrackEndTime
        {
            get
            {
                if (!RealTimeTracking)
                {
                    return dateTimePicker1_2.Value;
                }

                dateTimePicker1_2.Value = DateTime.Now;
                return dateTimePicker1_2.Value;
            }
            set
            {
                if (dateTimePicker1_2.InvokeRequired)
                {
                    dateTimePicker1_2.Invoke(new Action(() =>
                        {

                            if (dateTimePicker1_2.Value < dateTimePicker1_1.Value || dateTimePicker1_2.Value > DoubleBarCurrentTime)
                            {
                                dateTimePicker1_2.Value = value;
                            }
                            else
                            {
                                dateTimePicker1_2.Value = value;
                            }
                        }));
                }
                else
                {
                    if (dateTimePicker1_2.Value < dateTimePicker1_1.Value || dateTimePicker1_2.Value > DoubleBarCurrentTime)
                    {
                        dateTimePicker1_2.Value = value;
                    }
                    else
                    {
                        dateTimePicker1_2.Value = value;
                    }
                }
            }
        }

        #endregion

        public bool RealTimeTracking { get; set; }

        private void ChartLine_Update_Load(object sender, EventArgs e)
        {
            DateTime dTime = DateTime.Now;
            DoubleBarCurrentTime = dTime;
        }


        public void Refresh(Dictionary<string, double>[] dicCT, Dictionary<string, int>[] dicCount)
        {
            if(InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    InternalRefreshChart(dicCount, dicCT);
                }
                ));
            }
            else
                InternalRefreshChart(dicCount, dicCT);
        }



        public void Clear()
        {
            if(chart1.InvokeRequired)
            {
                chart1.Invoke(new Action(() =>
                {
                    chart1.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                }));
            }
            else
            {
                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
            }

            if (chart2.InvokeRequired)
            {
                chart2.Invoke(new Action(() =>
                {
                    chart2.Series[0].Points.Clear();
                    chart2.Series[1].Points.Clear();
                }));
            }
            else
            {
                chart2.Series[0].Points.Clear();
                chart2.Series[1].Points.Clear();
            }
        }


        /*Được sử dụng cho số liệu thống kê số lượng DS/NS trong Bảng 2;*/
        private void InternalRefreshChart(Dictionary<string, int>[] dic, Dictionary<string, double>[] dicCT)
        {
            //Biểu đồ Yield
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();   
            for (int i = 0; i < dic.Length;i++)
            {
                foreach (KeyValuePair<string, int> kvp in dic[i])
                {
                    chart2.Series[i].Points.AddXY(kvp.Key, kvp.Value);
                }
            }    

            /***********************************************/
            //Biểu đồ CT
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

        #region Chức năng chọn khung thời gian
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
        private const string DateTimeFormate = "MM/dd/yyyy HH:mm:ss";//Time format；


        private void chart1_DoubleClick(object sender, EventArgs e)
        {
            IsSelectTime = false;
        }
        #endregion
    }
}
