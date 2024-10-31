using BoTech;
using Demo.UserControls;

//using Demo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Page
{
    public partial class PageChart : UserControlBase
    {
        private static PageChart instance;
        public static PageChart Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new PageChart();
                return instance;
            }
        }
        public PageChart()
        {
            InitializeComponent();

            //绑定点击事件 
            //BindColtrolsClick.BindControlClick(this);
            chartIO_Update1.UpdateChart += RefreshChart_IO_Hour;
            //chartIO_Update1.UpdateChart2 += RefreshChart_IO_HourCount_Func;

        }
        private DataTable Unit_Daily;
        private DataTable ChartLine8H;
        private DataTable ChartZhu7D;
        private DataTable ChartEachUnit;
        private void DataPage_Load(object sender, EventArgs e)
        {
            //FormScaleFunc(4);
        }

        public override void StartRefreshPage()
        {
            this.StatusRefreshTimer.Start();
        }

        public override void StopRefreshPage()
        {
            this.StatusRefreshTimer.Stop();
        }

        private void StatusRefreshTimer_Tick(object sender, EventArgs e)
        {
            this.StatusRefreshTimer.Stop();

            RefreshDEU(DateTime.Now.AddHours(-48), DateTime.Now);

            RefreshUI();
            if (chartIO_Update1.IsSelectTime)
            {

            }
            else
            {
                this.StatusRefreshTimer.Start();
            }

        }





        private void RefreshUI()
        {
            try
            {

                DateTime dTime = DateTime.Now;
                /*数据表中显示48小时数据；*/
                if (chartIO_Update1.IsSelectIO && chartIO_Update1.IsSelectDay)
                {

                    chartIO_Update1.DoubleBarCurrentTime = dTime;
                    chartIO_Update1.DoubleBarCurrentTime2 = dTime;
                    chartIO_Update1.TrackStartTime = dTime.AddDays(-6);
                    chartIO_Update1.TrackStartTime2 = dTime.AddDays(-6);
                    chartIO_Update1.TrackEndTime = dTime;
                    chartIO_Update1.TrackEndTime2 = dTime;
                    chartIO_Update1.Clear();
                    RefreshChart_IO_Day();
                }
                else if (chartIO_Update1.IsSelectIO && chartIO_Update1.IsSelectDay == false)
                {

                    chartIO_Update1.DoubleBarCurrentTime = dTime;
                    chartIO_Update1.DoubleBarCurrentTime2 = dTime;

                    chartIO_Update1.Clear();
                    RefreshChart_IO_Hour();
                }
                else if (chartIO_Update1.IsSelectIO == false && chartIO_Update1.IsSelectDay)
                { }
                else if (chartIO_Update1.IsSelectIO == false && chartIO_Update1.IsSelectDay == false)
                { }




            }
            catch
            { }

        }







        private void btnExport_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (ChartLine8H != null)
                ds.Tables.Add(ChartLine8H.Copy());
            if (ChartZhu7D != null)
                ds.Tables.Add(ChartZhu7D.Copy());
            if (ChartEachUnit != null)
                ds.Tables.Add(ChartEachUnit.Copy());



            string filePath = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "请选择保存路径";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                filePath = fbd.SelectedPath;
            }
            else
            {
                return;
            }
            filePath += @"\Data" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            //EPPlusHelper.DataSetToExcel(ds, filePath);
        }



        #region Unit && Daily
        public void Async_UnitDaily(IO_Summary ios)
        {
            try
            {
                if (!this.IsHandleCreated)
                {
                    this.CreateControl();
                    //return;
                }
                if(IsHandleCreated)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this.Lbl_Unit.Text = ios.SN;
                        this.Lbl_IO.Text = ios.Input_Output;
                        this.lbl_Yield.Text = ios.Yield;
                        this.lbl_PF.Text = ios.Pass_Fail;
                        this.lbl_UPH.Text = ios.UPH;
                        this.lbl_CT.Text = ios.CT;
                        this.Pa_Unit.BackColor = ios.UnitStatus == true ? Color.Green : Color.Red;
                    }));
                }
            }
            catch
            {

            }


        }
        #endregion


        #region Chart Each Unit
        private void RefreshDEU(DateTime start, DateTime end)
        {

            try
            {
                DataTable dt = DataServerManager.Instance.SelectDurationUnit(start, end);


                dt.Columns[0].ColumnName = "Data";
                DataTable dtt = new DataTable();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    DataColumn dc = new DataColumn(dt.Columns[i].ColumnName, typeof(System.String));
                    dtt.Columns.Add(dc);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtt.Rows.Add(dtt.NewRow());
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        //dtt.Rows[i][j] =  Convert.ToString(dt.Rows[i][j]) ;
                        if (j == 0 && Convert.ToString(dt.Rows[i][j]).Length > 5)
                            dtt.Rows[i][j] = ((DateTime)dt.Rows[i][j]).ToString("yyyy-MM-dd");
                        else if (j == 4 && Convert.ToString(dt.Rows[i][j]).Length > 5)
                            dtt.Rows[i][j] = ((DateTime)dt.Rows[i][j]).ToString("HH:mm:ss.ff");
                        else if (j == 5 && Convert.ToString(dt.Rows[i][j]).Length > 5)
                            dtt.Rows[i][j] = ((DateTime)dt.Rows[i][j]).ToString("HH:mm:ss.ff");
                        else
                            dtt.Rows[i][j] = dt.Rows[i][j];

                    }
                }


                ChartEachUnit = dtt;
                ChartEachUnit.TableName = "Chart Each Unit";
                this.dataEachUnit1.UpdateData(dtt);
            }
            catch (Exception ex)
            {

            }

        }


        #endregion


        #region 图标显示 chart；


        private void RefreshChart_IO_Day()
        {
            /*表2，按天；显示CT;*/
            DataTable dt = DataServerManager.Instance.Select7DavgCT(DateTime.Now);
            //DataTable dt = DataServerManager.Instance.SelectTest(DateTime.Now);
            Dictionary<string, double>[] dicCT = new Dictionary<string, double>[] { new Dictionary<string, double>(), new Dictionary<string, double>() };
            for (int i = 0; i < 7; i++)
            {
                string key;
                key = DateTime.Now.AddDays(i - 6).ToString("yyyy/MM/dd");
                string strTemp = dt.Rows[6 - i][0].ToString();
                if (strTemp == "" || strTemp == null)
                {
                    strTemp = "0";
                }

                double value = Convert.ToDouble(strTemp);
                dicCT[0].Add(key, value);



                string strTemp2 = dt.Rows[13 - i][0].ToString();
                if (strTemp2 == "" || strTemp2 == null)
                {
                    strTemp2 = "0";
                }
                double value2 = Convert.ToDouble(strTemp2);
                dicCT[1].Add(key, value2);
            }



            /*表2，按天；显示产量*/
            DataTable dt2 = DataServerManager.Instance.Count7DayYield(DateTime.Now);
            Dictionary<string, int>[] dicCount = new Dictionary<string, int>[] { new Dictionary<string, int>(), new Dictionary<string, int>() };
            for (int i = 0; i < 7; i++)
            {
                string key2 = dt2.Rows[6 - i][1].ToString();
                if (key2 == "" || key2 == null)
                {
                    key2 = DateTime.Now.AddDays(i - 6).ToString("yyyy/MM/dd");

                }
                else
                {
                    key2 = Convert.ToDateTime(dt2.Rows[6 - i][1]).ToString("yyyy/MM/dd");
                }

                int value = Convert.ToInt32(dt2.Rows[6 - i][0]);
                dicCount[0].Add(key2, value);

                int value2 = Convert.ToInt32(dt2.Rows[13 - i][0]);
                dicCount[1].Add(key2, value2);
            }



            ChartZhu7D = dt;
            ChartZhu7D.TableName = "7Day";
            chartIO_Update1.Refresh(dicCT, dicCount);
        }


        private void RefreshChart_IO_Hour()
        {

            try
            {
                DateTime chart1TimeStart = chartIO_Update1.TrackStartTime;
                DateTime chart2TimeStart = chartIO_Update1.TrackStartTime2;

                DateTime dtTemp1 = chart1TimeStart.AddHours(24);
                DateTime dtTemp2 = chart2TimeStart.AddHours(24);
                //chartIO_Update1.TrackEndTime = dtTemp1;
                //chartIO_Update1.TrackEndTime2 = dtTemp2;

                #region /*根据24-1点白夜班CT整理数据*/
                DataTable dtCT = DataServerManager.Instance.Select24HDavgCT(chart1TimeStart);
                Dictionary<int, double>[] dicIntCTTemp = new Dictionary<int, double>[] { new Dictionary<int, double>(), new Dictionary<int, double>() };
                Dictionary<int, double>[] dicIntCTRslt = new Dictionary<int, double>[] { new Dictionary<int, double>(), new Dictionary<int, double>() };
                Dictionary<string, double>[] dicStrCTRslt = new Dictionary<string, double>[] { new Dictionary<string, double>(), new Dictionary<string, double>() };


                string[] sTimes = new string[12];
                for (int i = 0; i < 24; i++)
                {

                    string key = "";
                    double value;

                    key = dtCT.Rows[i][1].ToString();
                    if (i < 12)
                    {
                        sTimes[i] = key;
                    }

                    int iTemp = Convert.ToInt32(key);
                    if (dtCT.Rows[i][0] == null || dtCT.Rows[i][0].ToString() == "")
                    {
                        value = 0;
                    }
                    else
                    {
                        value = Convert.ToDouble(dtCT.Rows[i][0]);
                    }

                    if (iTemp >= 8 && iTemp < 20)
                    {
                        dicIntCTTemp[0].Add(iTemp, value);
                    }
                    else
                    {
                        dicIntCTTemp[1].Add(iTemp, value);
                    }

                }


                DateTime dTime;
                for (int i = 0; i < 12; i++)
                {
                    string key, keyT;
                    key = sTimes[i];
                    keyT = Convert.ToInt32(key).ToString("00");

                    string strKey;
                    strKey = HourConvert2(key.ToString(), chart1TimeStart);
                    dTime = DateTime.ParseExact(keyT, "HH", System.Globalization.CultureInfo.CurrentCulture);
                    int iTemp = dTime.AddHours(12).Hour;
                    int iTempKey = 0;
                    double dTempValue = 0;
                    foreach (var item in dicIntCTTemp[0])
                    {
                        if (item.Key == Convert.ToInt32(key) || item.Key == iTemp)
                        {
                            iTempKey = item.Key;
                            dTempValue = item.Value;
                            break;
                        }
                    }
                    dicStrCTRslt[0].Add(strKey, dTempValue);


                    int iTempKey2 = 0;
                    double dTempValue2 = 0;
                    foreach (var item in dicIntCTTemp[1])
                    {
                        if (item.Key == Convert.ToInt32(key) || item.Key == iTemp)
                        {
                            iTempKey2 = item.Key;
                            dTempValue2 = item.Value;
                            break;
                        }
                    }
                    dicStrCTRslt[1].Add(strKey, dTempValue2);

                }





                #endregion


                #region 白夜班按小时统计；
                DataTable dtCount = DataServerManager.Instance.Count24HourYield(chart2TimeStart);
                Dictionary<int, int>[] dicCount = new Dictionary<int, int>[] { new Dictionary<int, int>(), new Dictionary<int, int>() };
                Dictionary<int, int>[] dicCountTemp = new Dictionary<int, int>[] { new Dictionary<int, int>(), new Dictionary<int, int>() };
                Dictionary<string, int>[] dicCountRslt = new Dictionary<string, int>[] { new Dictionary<string, int>(), new Dictionary<string, int>() };

                string[] sDsNs = new string[12];
                for (int i = 0; i < 24; i++)
                {
                    string key = "";
                    int value;

                    key = dtCount.Rows[i][1].ToString();
                    if (i < 12)
                    {
                        sDsNs[i] = key;
                    }

                    int iTemp = Convert.ToInt32(key);
                    if (dtCount.Rows[i][0] == null || dtCount.Rows[i][0].ToString() == "")
                    {
                        value = 0;
                    }
                    else
                    {
                        value = Convert.ToInt32(dtCount.Rows[i][0].ToString().Trim());
                    }

                    if (iTemp >= 8 && iTemp < 20)
                    {
                        dicCount[0].Add(iTemp, value);
                    }
                    else
                    {
                        dicCount[1].Add(iTemp, value);
                    }
                }



                DateTime dTime2;
                for (int i = 0; i < 12; i++)
                {
                    string key, keyT;
                    key = sTimes[i];
                    keyT = Convert.ToInt32(key).ToString("00");

                    string strKey;
                    strKey = HourConvert2(key.ToString(), chart2TimeStart);


                    dTime2 = DateTime.ParseExact(keyT, "HH", System.Globalization.CultureInfo.CurrentCulture);
                    int iTemp = dTime2.AddHours(12).Hour;


                    int iTempKey = 0;
                    int dTempValue = 0;
                    foreach (var item in dicCount[0])
                    {
                        if (item.Key == Convert.ToInt32(key) || item.Key == iTemp)
                        {
                            iTempKey = item.Key;
                            dTempValue = item.Value;
                            break;
                        }
                    }

                    dicCountRslt[0].Add(strKey, dTempValue);


                    int iTempKey2 = 0;
                    int dTempValue2 = 0;
                    foreach (var item in dicCount[1])
                    {
                        if (item.Key == Convert.ToInt32(key) || item.Key == iTemp)
                        {
                            iTempKey2 = item.Key;
                            dTempValue2 = item.Value;
                            break;
                        }
                    }

                    dicCountRslt[1].Add(strKey, dTempValue2);

                }




                #endregion

                ChartLine8H = dtCount;
                ChartLine8H.TableName = "24 Hour";


                chartIO_Update1.Refresh(dicStrCTRslt, dicCountRslt);
            }
            catch (Exception ex)
            { }
        }






        private void RefreshChart_IO_HourCT_Func(DateTime chart1TimeStart)
        {
            try
            {

                chartIO_Update1.TrackEndTime = chart1TimeStart.AddHours(24);
                #region /*根据24-1点白夜班CT整理数据*/
                DataTable dtCT = DataServerManager.Instance.Select24HDavgCT(chart1TimeStart);
                Dictionary<int, double>[] dicIntCTTemp = new Dictionary<int, double>[] { new Dictionary<int, double>(), new Dictionary<int, double>() };
                Dictionary<int, double>[] dicIntCTRslt = new Dictionary<int, double>[] { new Dictionary<int, double>(), new Dictionary<int, double>() };
                Dictionary<string, double>[] dicStrCTRslt = new Dictionary<string, double>[] { new Dictionary<string, double>(), new Dictionary<string, double>() };


                string[] sTimes = new string[12];
                for (int i = 0; i < 24; i++)
                {

                    string key = "";
                    double value;

                    key = dtCT.Rows[i][1].ToString();
                    if (i < 12)
                    {
                        sTimes[i] = key;
                    }

                    int iTemp = Convert.ToInt32(key);
                    if (dtCT.Rows[i][0] == null || dtCT.Rows[i][0].ToString() == "")
                    {
                        value = 0;
                    }
                    else
                    {
                        value = Convert.ToDouble(dtCT.Rows[i][0]);
                    }

                    if (iTemp >= 8 && iTemp < 20)
                    {
                        dicIntCTTemp[0].Add(iTemp, value);
                    }
                    else
                    {
                        dicIntCTTemp[1].Add(iTemp, value);
                    }

                }


                DateTime dTime;
                for (int i = 0; i < 12; i++)
                {
                    string key, keyT;
                    key = sTimes[i];
                    keyT = Convert.ToInt32(key).ToString("00");

                    string strKey;
                    strKey = HourConvert2(key.ToString(), chart1TimeStart);
                    dTime = DateTime.ParseExact(keyT, "HH", System.Globalization.CultureInfo.CurrentCulture);
                    int iTemp = dTime.AddHours(12).Hour;
                    int iTempKey = 0;
                    double dTempValue = 0;
                    foreach (var item in dicIntCTTemp[0])
                    {
                        if (item.Key == Convert.ToInt32(key) || item.Key == iTemp)
                        {
                            iTempKey = item.Key;
                            dTempValue = item.Value;
                            break;
                        }
                    }
                    dicStrCTRslt[0].Add(strKey, dTempValue);


                    int iTempKey2 = 0;
                    double dTempValue2 = 0;
                    foreach (var item in dicIntCTTemp[1])
                    {
                        if (item.Key == Convert.ToInt32(key) || item.Key == iTemp)
                        {
                            iTempKey2 = item.Key;
                            dTempValue2 = item.Value;
                            break;
                        }
                    }
                    dicStrCTRslt[1].Add(strKey, dTempValue2);

                }





                #endregion



                ChartLine8H = dtCT;
                ChartLine8H.TableName = "24 Hour";


                chartIO_Update1.Refresh(dicStrCTRslt);
            }
            catch (Exception ex)
            { }
        }


        private void RefreshChart_IO_HourCount_Func(DateTime chart2TimeStart)
        {
            try
            {
                string[] sTimes = new string[12];
                chartIO_Update1.TrackEndTime = chart2TimeStart.AddHours(24);

                #region 白夜班按小时统计；
                DataTable dtCount = DataServerManager.Instance.Count24HourYield(chart2TimeStart);
                Dictionary<int, int>[] dicCount = new Dictionary<int, int>[] { new Dictionary<int, int>(), new Dictionary<int, int>() };
                Dictionary<int, int>[] dicCountTemp = new Dictionary<int, int>[] { new Dictionary<int, int>(), new Dictionary<int, int>() };
                Dictionary<string, int>[] dicCountRslt = new Dictionary<string, int>[] { new Dictionary<string, int>(), new Dictionary<string, int>() };

                string[] sDsNs = new string[12];
                for (int i = 0; i < 24; i++)
                {
                    string key = "";
                    int value;

                    key = dtCount.Rows[i][1].ToString();
                    if (i < 12)
                    {
                        sDsNs[i] = key;
                    }

                    int iTemp = Convert.ToInt32(key);
                    if (dtCount.Rows[i][0] == null || dtCount.Rows[i][0].ToString() == "")
                    {
                        value = 0;
                    }
                    else
                    {
                        value = Convert.ToInt32(dtCount.Rows[i][0].ToString().Trim());
                    }

                    if (iTemp >= 8 && iTemp < 20)
                    {
                        dicCount[0].Add(iTemp, value);
                    }
                    else
                    {
                        dicCount[1].Add(iTemp, value);
                    }
                }



                DateTime dTime2;
                for (int i = 0; i < 12; i++)
                {
                    string key, keyT;
                    key = sTimes[i];
                    keyT = Convert.ToInt32(key).ToString("00");

                    string strKey;
                    strKey = HourConvert2(key.ToString(), chart2TimeStart);


                    dTime2 = DateTime.ParseExact(keyT, "HH", System.Globalization.CultureInfo.CurrentCulture);
                    int iTemp = dTime2.AddHours(12).Hour;


                    int iTempKey = 0;
                    int dTempValue = 0;
                    foreach (var item in dicCount[0])
                    {
                        if (item.Key == Convert.ToInt32(key) || item.Key == iTemp)
                        {
                            iTempKey = item.Key;
                            dTempValue = item.Value;
                            break;
                        }
                    }

                    dicCountRslt[0].Add(strKey, dTempValue);


                    int iTempKey2 = 0;
                    int dTempValue2 = 0;
                    foreach (var item in dicCount[1])
                    {
                        if (item.Key == Convert.ToInt32(key) || item.Key == iTemp)
                        {
                            iTempKey2 = item.Key;
                            dTempValue2 = item.Value;
                            break;
                        }
                    }

                    dicCountRslt[1].Add(strKey, dTempValue2);

                }




                #endregion


                ChartLine8H = dtCount;
                ChartLine8H.TableName = "24 Hour";
                chartIO_Update1.Refresh(dicCountRslt);
            }
            catch (Exception ex)
            { }
        }


        private string HourConvert2(string hour, DateTime start)
        {
            string key = "";
            switch (hour)
            {
                case "0":
                    key = "24:00(12:00)";
                    break;
                case "1":
                    key = "01:00(13:00)";
                    break;
                case "2":
                    key = "02:00(14:00)";
                    break;
                case "3":
                    key = "03:00(15:00)";
                    break;
                case "4":
                    key = "04:00(16:00)";
                    break;
                case "5":
                    key = "05:00(17:00)";
                    break;
                case "6":
                    key = "06:00(18:00)";
                    break;
                case "7":
                    key = "07:00(19:00)";
                    break;
                case "8":
                    key = "08:00(20:00)";
                    break;
                case "9":
                    key = "09:00(21:00)";
                    break;
                case "10":
                    key = "10:00(22:00)";
                    break;
                case "11":
                    key = "11:00(23:00)";
                    break;
                case "12":
                    key = "12:00(00:00)";
                    break;
                case "13":
                    key = "13:00(01:00)";
                    break;
                case "14":
                    key = "14:00(02:00)";
                    break;
                case "15":
                    key = "15:00(03:00)";
                    break;
                case "16":
                    key = "16:00(04:00)";
                    break;
                case "17":
                    key = "17:00(05:00)";
                    break;
                case "18":
                    key = "18:00(06:00)";
                    break;
                case "19":
                    key = "19:00(07:00)";
                    break;
                case "20":
                    key = "20:00(08:00)";
                    break;
                case "21":
                    key = "21:00(09:00)";
                    break;
                case "22":
                    key = "22:00(10:00)";
                    break;
                case "23":
                    key = "23:00(11:00)";
                    break;
                case "24":
                    key = "24:00(12:00)";
                    break;


            }

            return key + start.ToString("yyyy/MM/dd");
        }
        #endregion
    }
}
