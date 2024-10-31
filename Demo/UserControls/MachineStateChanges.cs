using NPOI.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.UserControls
{
    public partial class MachineStateChanges : UserControl
    {
        #region 变量；

        private string _errorMessage;
        private DataTable _dataSource;
        [Description("控件刷新完事件")]
        public Action<DataTable> Refreshed_event;

        private Dictionary<string, double[]> _dicBarData = new Dictionary<string, double[]>();
        private const int DaysNum = 7; //保存7天的数据；
        private const int OneDayMinute = 1440;//每天总时间数；
        private Color ControlColor = Color.FromArgb(211, 211, 211);//控件背景色；
        #endregion


        #region 属性；
        /// <summary>
        /// 设备状态数据表；
        /// </summary>
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

        #endregion



        public MachineStateChanges()
        {
            InitializeComponent();

            this.chart1.BackColor = ControlColor;
            this.chart1.ChartAreas[0].BackColor = ControlColor;
            ChartSet();
        }




        #region 方法：

        /// <summary>
        /// chart 坐标轴设置；
        /// </summary>
        private void ChartSet()
        {

            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "0%";
            chart1.Series[0].Color = HiveColor.HiveColorRunning_Green;
            chart1.Series[1].Color = HiveColor.HiveColorIdle_Blue;
            chart1.Series[2].Color = HiveColor.HiveColorEngineering_Purple;
            chart1.Series[3].Color = HiveColor.HiveColorPlannedDT_Orange;
            chart1.Series[4].Color = HiveColor.HiveColorDowntime_Red;

            chart1.Series[0].Name = "Running";
            chart1.Series[1].Name = "Idle";
            chart1.Series[2].Name = "Engineering";
            chart1.Series[3].Name = "PlannedDT";
            chart1.Series[4].Name = "Down";

            //chart1.Series[0].LabelFormat = "0%";
            //chart1.Series[1].LabelFormat = "0%";
            //chart1.Series[2].LabelFormat = "0%";
            //chart1.Series[3].LabelFormat = "0%";
            //chart1.Series[4].LabelFormat = "0%";

            //chart1.Series[0].Label = "#VAL{P}";
            //chart1.Series[1].Label = "#VAL{P}";
            //chart1.Series[2].Label = "#VAL{P}";
            //chart1.Series[3].Label = "#VAL{P}";
            //chart1.Series[4].Label = "#VAL{P}";



            chart1.Legends.Add("legend1");
            chart1.Legends[0].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            chart1.Legends[0].BackColor = Color.Transparent;

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


        private void InternalRefreshChart(DataTable SourceData)
        {
            //chart1.Series.Clear();
            _dicBarData.Clear();
            double[] dTemp = new double[5] { 0, 0, 0, 0, 0 };
            int rowNum = DataSourceTable.Rows.Count;
            try
            {

                #region 读表处理；
                /*没有行数的情况；*/
                if (DataSourceTable.Rows.Count == 0)
                {
                    ///假如一行数据都没有为空；那就空数据；
                    for (int i = 0; i < DaysNum; i++)
                    {
                        _dicBarData.Add(DateTime.Now.AddDays(1 - DaysNum + i).ToString(), dTemp);
                    }
                    return;
                }

                /*************************************************/
                /*有行数的情况；行数不一定够7行即7天，数据补充；*/
                string keyDate = DataSourceTable.Rows[rowNum - 1][0].ToString();//得到最新日期；
                DateTime dtTemp;
                dtTemp = Convert.ToDateTime(keyDate);//转换成dateTime格式；
                string sTemp;
                for (int i = 0; i < DaysNum - rowNum; i++)
                {
                    sTemp = dtTemp.AddDays(-rowNum + i - 1).ToString("yyyy/MM/dd");
                    _dicBarData.Add(sTemp, dTemp);
                }


                /****************************************************/
                for (int i = 0; i < rowNum; i++)
                {
                    string key;
                    key = ((DateTime)DataSourceTable.Rows[i][0]).ToString("yyyy/MM/dd");//日期；
                    //第1/2/3/4/5列分别代表running/idle/Eng/PlannedDown/Down;
                    double[] db1 = new double[5];
                    for (int j = 1; j < DataSourceTable.Columns.Count; j++)
                    {
                        double d1 = 0.0;
                        double d2 = 0.0;
                        d1 = (double)DataSourceTable.Rows[i][j];

                        d2 = Math.Round(d1 / 1440, 3);


                        if (d2 > 1)
                        { d2 = 0; }

                        if (d2 < 0)
                        { d2 = 0; }


                        db1[j - 1] = d2;
                    }



                    DateTime tem = (DateTime)(DataSourceTable.Rows[i][0]);

                    ///What to do if the percentage of idle time is still greater than 1?
                    for (int j = 0; j < 5; j++)
                    {
                        if (db1[j] == 1)
                        {
                            db1[j] = 2 - db1[0] - db1[1] - db1[2] - db1[3] - db1[4];
                            break;
                        }
                    }


                    double dTemp2 = db1[0] + db1[2] + db1[3] + db1[4];
                    //double dTemp2 = db1[1];
                    if (dTemp2 > 1)
                    {
                        int tt = 0;
                        double tt1 = 0;
                        for (int k = 0; k < db1.Length; k++)
                        {
                            if (tt1 < db1[k])
                            {
                                tt1 = db1[k];
                                tt = k;
                            }
                        }
                        db1[tt] = 1;
                        for (int l = 0; l < db1.Length; l++)
                        {
                            if (l != tt)
                            {
                                db1[tt] -= db1[l];
                            }
                        }

                        //db1 = dTemp;//dTemp这里的每个值都为0；

                    }
                    else if ((DateTime.Now - tem).Days != 0)
                    {
                        db1[1] = 1 - dTemp2;
                    }





                    /////其余时间算作idle时间；这是idle时间占比；
                    //if (i < rowNum - 1)
                    //{
                    //    db1[1] = Math.Round(1 - (db1[0] + db1[2] + db1[3] + db1[4]), 3);
                    //}


                    _dicBarData.Add(key, db1);
                }
                #endregion


                #region 图表显示
                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();
                chart1.Series[2].Points.Clear();
                chart1.Series[3].Points.Clear();
                chart1.Series[4].Points.Clear();

                foreach (KeyValuePair<string, double[]> kvp in _dicBarData)
                {
                    chart1.Series[0].Points.AddXY(kvp.Key, kvp.Value[0]);
                    chart1.Series[1].Points.AddXY(kvp.Key, kvp.Value[1]);
                    chart1.Series[2].Points.AddXY(kvp.Key, kvp.Value[2]);
                    chart1.Series[3].Points.AddXY(kvp.Key, kvp.Value[3]);
                    chart1.Series[4].Points.AddXY(kvp.Key, kvp.Value[4]);
                }
                #endregion


                if (Refreshed_event != null)
                    Refreshed_event(_dataSource);
                _errorMessage = "";
            }
            catch (Exception ex)
            {
                _errorMessage = ex.ToString();
            }
        }













        #endregion

        private void chart1_MouseHover(object sender, EventArgs e)
        {
           

        }

        private void chart1_MouseHover(object sender, MouseEventArgs e)
        {
           
            try
            {
                if (DataSourceTable != null)
                {
                    for (int i = 0; i < DataSourceTable.Rows.Count; i++)
                    {
                        DateTime dtime;
                        string strValue = "";
                        for (int j = 0; j < DataSourceTable.Columns.Count; j++)
                        {
                            switch (j)
                            {
                                case 0:
                                    
                                    strValue += DataSourceTable.Rows[i][j].ToString();
                                    dtime = DateTime.Parse(strValue);
                                    strValue = dtime.ToString("MM/dd/yyyy") + "\r\n";
                                    break;
                                case 1:
                                    strValue += "Running: " + String.Format("{0:F2}", DataSourceTable.Rows[i][j]) + "\r\n";
                                    break;
                                case 2:
                                    strValue += "Idle: " + String.Format("{0:F2}", DataSourceTable.Rows[i][j]) + "\r\n";
                                    break;
                                case 3:
                                    strValue += "Engineering: " + String.Format("{0:F2}", DataSourceTable.Rows[i][j]) + "\r\n";
                                    break;
                                case 4:
                                    strValue += "Planned_downtime: " + String.Format("{0:F2}",DataSourceTable.Rows[i][j])+ "\r\n";
                                    break;
                                case 5:
                                    strValue += "Downtime: " + String.Format("{0:F2}", DataSourceTable.Rows[i][j]) + "\r\n";
                                    break;
                            }
                        }

                        this.chart1.Series[0].Points[i].ToolTip = strValue;
                        this.chart1.Series[1].Points[i].ToolTip = strValue;
                        this.chart1.Series[2].Points[i].ToolTip = strValue;                        
                        this.chart1.Series[3].Points[i].ToolTip = strValue;
                        this.chart1.Series[4].Points[i].ToolTip = strValue;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
    public enum ErrorMessageList
    {
        无,
        hive运行时间数据表未赋值,
        Hive运行时间数据表读报错,
        Hive运行时间数据表行数少于1,
        Hive运行时间数据表列数少于1,

    }

    public class HiveColor
    {

        public static Color HiveColorDisabled = Color.LightGray;
        /// <summary>
        /// running，绿色
        /// </summary>
        public static Color HiveColorRunning_Green = Color.FromArgb(211, 235, 115);
        /// <summary>
        /// idle,蓝色
        /// </summary>
        public static Color HiveColorIdle_Blue = Color.FromArgb(235, 255, 254);
        /// <summary>
        /// engineering,紫色
        /// </summary>
        public static Color HiveColorEngineering_Purple = Color.FromArgb(204, 171, 216);
        /// <summary>
        /// planned dt, 橘色
        /// </summary>
        public static Color HiveColorPlannedDT_Orange = Color.FromArgb(255, 215, 212);
        /// <summary>
        /// downtime,宕机
        /// </summary>
        public static Color HiveColorDowntime_Red = Color.FromArgb(235, 115, 115);

    }
}
