using BoTech;
using Demo.UserControls;
using HB_IWatch;
using System.Diagnostics;

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
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Models;
using System.Threading;

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
            //chkRealTime.CheckedChanged += ChkRealTimeChart_CheckedChanged;

            var a = ModelStore.GetModelInfoList();
            foreach (var i in ModelStore.GetModelInfoList())
            { 
                cbModel.Items.Add(i.Name);
            }    
            cbModel.SelectedIndex = 0;
        }

        private void ChkRealTimeChart_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRealTime.Checked)
            {
                this.StatusRefreshTimer.Start();
            }
            else
            {
                this.StatusRefreshTimer.Stop();
            }

        }

        private DataTable Unit_Daily;
        private DataTable ChartLine8H;
        private DataTable ChartZhu7D;
        private DataTable ChartEachUnit;

        /// <summary>
        /// Chart tracktime within 2 days
        /// </summary>
        public override void StartRefreshPage()
        {
            chartIO_Update1.TrackStartTime = DateTime.Now.AddHours(-48);
            chartIO_Update1.TrackEndTime = DateTime.Now;
            this.StatusRefreshTimer.Start();
        }

        public override void StopRefreshPage()
        {
            this.StatusRefreshTimer.Stop();
        }

        public void RefeshAllChart()
        {
            if (!chkRealTime.Checked)
            {
                return;
            }    
               

            if (chkRealTime.Checked)
            {
                RefreshDEU(DateTime.Now.AddHours(-48), DateTime.Now);
            }
            else
            {
                var startTime = chartIO_Update1.TrackStartTime;
                var endTime = chartIO_Update1.TrackEndTime;
                if ((endTime.Date - startTime.Date).Days > 30)
                {
                    startTime = endTime.AddDays(-30);
                }
                RefreshDEU(startTime, endTime);
            }
            RefreshUI();
        }
        private void StatusRefreshTimer_Tick(object sender, EventArgs e)
        {
            this.StatusRefreshTimer.Stop();

            RefeshAllChart();

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
                //Chọn line chart và thống kê theo ngày
                if (chkRealTime.Checked)
                {
                    chartIO_Update1.RealTimeTracking = true;
                }
                else
                {
                    chartIO_Update1.RealTimeTracking = false;
                }

                //Line Chart
                if (chartIO_Update1.IsSelectIO && chartIO_Update1.IsSelectDay)
                {
                    if (chkRealTime.Checked)
                    {
                        chartIO_Update1.DoubleBarCurrentTime = dTime;
                        chartIO_Update1.TrackStartTime = dTime.AddDays(-6);
                        chartIO_Update1.TrackEndTime = dTime;
                    }
                    chartIO_Update1.Clear();
                    RefreshChart_IO_Day();
                }
                //Thông kê theo giờ
                else if (chartIO_Update1.IsSelectIO && chartIO_Update1.IsSelectDay == false)
                {
                    if(chkRealTime.Checked)
                    {
                        chartIO_Update1.DoubleBarCurrentTime = dTime;                     
                    }

                    chartIO_Update1.Clear();
                    RefreshChart_IO_Hour();
                }
                //Bar Chart
                else if (chartIO_Update1.IsSelectIO == false && chartIO_Update1.IsSelectDay)
                {
                    if (chkRealTime.Checked)
                    {
                        //Thời gian check hiện tại
                        chartIO_Update1.DoubleBarCurrentTime = dTime;
                        //Thời gian bắt đầu của trackbar
                        chartIO_Update1.TrackStartTime = dTime.AddDays(-6);
                        //Thời gian kết thúc trackBar
                        chartIO_Update1.TrackEndTime = dTime;
                    }

                    chartIO_Update1.Clear();
                    RefreshChart_IO_Day();
                }
                //Bar chart
                else if (chartIO_Update1.IsSelectIO == false && chartIO_Update1.IsSelectDay == false)
                {
                    if (chkRealTime.Checked || QueryAnother)
                    {
                        chartIO_Update1.DoubleBarCurrentTime = dTime;
                    }
                    chartIO_Update1.Clear();
                    RefreshChart_IO_Hour();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

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
            fbd.Description = "Please select the save path";
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
                        //this.Lbl_Unit.Text = ios.SN;
                        //this.Lbl_IO.Text = ios.Input_Output;
                        //this.lbl_Yield.Text = ios.Yield;
                        //this.lbl_PF.Text = ios.Pass_Fail;
                        //this.lbl_UPH.Text = ios.UPH;
                        //this.lbl_CT.Text = ios.CT;
                        //this.Pa_Unit.BackColor = ios.UnitStatus == true ? Color.Lime : Color.Red;
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
            //Bảng data SQL truy xuất dữ liệu chi tiết của từng sản phẩm
            try
            {
                DataTable dt;
                if(QueryAnother == false)
                {
                    if (isQuery)
                    {
                        if (cbModel.SelectedIndex == 0)
                        {
                            dt = DataServerManager.Instance.MonitorMassProduct(start, end);
                        }

                        else
                        {
                            dt = DataServerManager.Instance.MonitorMassProduct(cbModel.SelectedItem.ToString(), start, end);
                        }
                    }
                    else
                    {
                        dt = DataServerManager.Instance.MonitorMassProduct(LoadModel.currentModel.modelName, start, end);
                    }
                }   
  
                else
                {
                    dt = DataServerManager.Instance.MonitorMassProduct(LoadModel.currentModel.modelName, start, end);
                }    



                dt.Columns[0].ColumnName = "HappenTime";
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
                Stopwatch sw = new Stopwatch();
                sw.Start();
                this.dataEachUnit1.UpdateData(dtt);
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        #endregion


        #region chart；        
        // Tính theo đơn vị ngày
        private void RefreshChart_IO_Day()
        {


            var startTime1 = chartIO_Update1.TrackStartTime.Date;
            var endTime1 = chartIO_Update1.TrackEndTime.Date;
            var Days1 = (endTime1 - startTime1).Days+1;
            if(Days1 >= 30)
            {
                Days1 = 30;
            }

            /*表2，按天；显示CT;*/
            #region Cycle Time 
            // cycle time
            /*Chọn 7 ngày gần nhất*/
            DataTable dt = DataServerManager.Instance.Select7DavgCT(DateTime.Now);
            //Chọn được nhiều ngày, tối đa là 1 tháng
            DataTable dt1 = DataServerManager.Instance.SelectMultiDayCT(startTime1, endTime1);

            Dictionary<string, double>[] dicCT = new Dictionary<string, double>[] { new Dictionary<string, double>(), new Dictionary<string, double>() };

            //Dùng chế độ check thời gian thực
            if (chkRealTime.Checked)
            {
                for (int i = 0; i < 7; i++)
                {
                    string key;
                    key = DateTime.Now.AddDays(i - 6).ToString("yyyy/MM/dd");
                    string strTemp = dt.Rows[6 - i][0].ToString();
                    if (strTemp == "" || strTemp == null)
                    {
                        strTemp = "0";
                    }

                    double value = Math.Round( Convert.ToDouble(strTemp),2);
                    dicCT[0].Add(key, value);

                    string strTemp2 = dt.Rows[13 - i][0].ToString();
                    if (strTemp2 == "" || strTemp2 == null)
                    {
                        strTemp2 = "0";
                    }
                    double value2 = Math.Round(Convert.ToDouble(strTemp2),2);
                    dicCT[1].Add(key, value2);
                }
            }
            //Ko dùng đến realtime checked
            else
            {
                for (int i = 0; i < Days1; i++)
                {
                    string key;
                    key = endTime1.AddDays(i - (Days1-1)).ToString("yyyy/MM/dd");
                    string strTemp = dt1.Rows[(Days1 - 1) - i][0].ToString();
                    if (strTemp == "" || strTemp == null)
                    {
                        strTemp = "0";
                    }

                    double value = Math.Round(Convert.ToDouble(strTemp),2);
                    dicCT[0].Add(key, value);

                    string strTemp2 = dt1.Rows[(Days1 - 1)*2 + 1 - i][0].ToString();
                    if (strTemp2 == "" || strTemp2 == null)
                    {
                        strTemp2 = "0";
                    }
                    double value2 = Math.Round(Convert.ToDouble(strTemp2),2);
                    dicCT[1].Add(key, value2);
                }
            }
            #endregion

            #region Thống kê sản lượng theo ngày

            //Thống kê theo 7 ngày gần nhất, chia theo ca ngày và ca đêm
            DataTable dt2;
            if (chkRealTime.Checked)
            {
                dt2 = DataServerManager.Instance.Count7DayShiftYield(DateTime.Now, 7);
            }
            else
                dt2 = DataServerManager.Instance.Count7DayShiftYield(endTime1, 7);



            //Lấy dữ liệu PASS/FAIL của 7 ngày gần nhất, không phân chia ca ngày ca đêm
            DataTable dt4 = DataServerManager.Instance.CountDayYield(DateTime.Now);
            

            //Lấy dữ liệu của pass fail nhiều ngày <Tối đa 30 ngày>
            DataTable dt3 = DataServerManager.Instance.CountMultiDayYield(startTime1, endTime1);

            ///Shift == false là không phân ca
            ///==true là phân ca ngày ca đêm
            var Shift = true;

            Dictionary<string, int>[] dicCount = new Dictionary<string, int>[] { new Dictionary<string, int>(), new Dictionary<string, int>(), new Dictionary<string, int>(), new Dictionary<string, int>() };
            if (chkRealTime.Checked)
            {
                if(chkShift.Checked == false)
                {
                    // PASS/FAIL chia theo từng ca: Lấy dữ liệu của 7 ngày gần nhất
                    for (int i = 0; i < 7; i++)
                    {
                        string key2 = dt2.Rows[12 - i * 2][1].ToString();
                        if (key2 == "" || key2 == null)
                        {
                            key2 = DateTime.Now.AddDays(i - 6).ToString("MM/dd");
                        }
                        else
                        {
                            key2 = Convert.ToDateTime(dt2.Rows[12 - i*2][1]).ToString("MM/dd");
                        }
                        //Ca ngày
                        //PASS
                        int value = Convert.ToInt32(dt2.Rows[12 - i*2][0]);
                        dicCount[0].Add(key2 + "\r\nDS", value);
                        //Fail
                        int value1 = Convert.ToInt32(dt2.Rows[13 - i*2][0]);
                        dicCount[1].Add(key2 + "\r\nDS", value1);

                        //Ca đêm
                        //PASS
                        int value2 = Convert.ToInt32(dt2.Rows[26 - i*2][0]);
                        dicCount[0].Add(key2 + "\r\nNS", value2);
                        //fail
                        int value3 = Convert.ToInt32(dt2.Rows[27 - i*2][0]);
                        dicCount[1].Add(key2 + "\r\nNS", value3);
                          
                    }
                }
                else
                {
                    //Only PassFail on 7 day
                    for (int i = 0; i < 7; i++)
                    {
                        string key2 = dt4.Rows[6 - i][1].ToString();
                        if (key2 == "" || key2 == null)
                        {
                            key2 = DateTime.Now.AddDays(i - 6).ToString("MM/dd");
                        }
                        else
                        {
                            key2 = Convert.ToDateTime(dt4.Rows[6- i][1]).ToString("MM/dd");
                        }
                        //key2 = key2 + "_DS";
                        //Pass
                        int value = Convert.ToInt32(dt4.Rows[6 - i][0]);
                        dicCount[0].Add(key2, value);
                        //Fail
                        int value1 = Convert.ToInt32(dt4.Rows[13 - i][0]);
                        dicCount[1].Add(key2, value1);
                    }
                }    
             
            }
            else
            {
                if (chkShift.Checked == false)
                {
                    // PASS/FAIL chia theo từng ca: Lấy dữ liệu của 7 ngày gần nhất
                    for (int i = 0; i < 7; i++)
                    {
                        string key2 = dt2.Rows[12 - i * 2][1].ToString();
                        if (key2 == "" || key2 == null)
                        {
                            key2 = DateTime.Now.AddDays(i - 6).ToString("MM/dd");
                        }
                        else
                        {
                            key2 = Convert.ToDateTime(dt2.Rows[12 - i * 2][1]).ToString("MM/dd");
                        }
                        //Ca ngày
                        //PASS
                        int value = Convert.ToInt32(dt2.Rows[12 - i * 2][0]);
                        dicCount[0].Add(key2 + "\r\nDS", value);
                        //Fail
                        int value1 = Convert.ToInt32(dt2.Rows[13 - i * 2][0]);
                        dicCount[1].Add(key2 + "\r\nDS", value1);

                        //Ca đêm
                        //PASS
                        int value2 = Convert.ToInt32(dt2.Rows[26 - i * 2][0]);
                        dicCount[0].Add(key2 + "\r\nNS", value2);
                        //fail
                        int value3 = Convert.ToInt32(dt2.Rows[27 - i * 2][0]);
                        dicCount[1].Add(key2 + "\r\nNS", value3);

                    }
                }
                else
                {
                    for (int i = 0; i < Days1; i++)
                    {
                        string key2 = dt3.Rows[Days1 - 1 - i][1].ToString();
                        if (key2 == "" || key2 == null)
                        {
                            key2 = endTime1.AddDays(i - (Days1 - 1)).ToString("yyyy/MM/dd");
                        }
                        else
                        {
                            key2 = Convert.ToDateTime(dt3.Rows[(Days1 - 1) - i][1]).ToString("yyyy/MM/dd");
                        }

                        int value = Convert.ToInt32(dt3.Rows[(Days1 - 1) - i][0]);
                        dicCount[0].Add(key2, value);

                        int value2 = Convert.ToInt32(dt3.Rows[((Days1 - 1) * 2) + 1 - i][0]);
                        dicCount[1].Add(key2, value2);
                    }
                }    

            }
            #endregion

            ChartZhu7D = dt;
            ChartZhu7D.TableName = "7Day";
            chartIO_Update1.Refresh(dicCT, dicCount);
        }

        //Theo giờ
        private void RefreshChart_IO_Hour()
        {

            try
            {
                DateTime chart1TimeStart = chartIO_Update1.TrackStartTime;
                DateTime chart2TimeStart = chartIO_Update1.TrackEndTime;

                DateTime dtTemp1 = chart1TimeStart.AddHours(-23);
                DateTime dtTemp2 = chart2TimeStart.AddHours(-23);

                #region Thông kê cycle time theo giờ

                //Check CycleTime 24h gần nhất
                string model = "";
                if(cbModel.InvokeRequired)
                {
                    cbModel.Invoke(new Action(() =>
                     {
                         model = cbModel.SelectedItem.ToString();
                     }));                    
                }
                else
                    model = cbModel.SelectedItem.ToString();


                DataTable dtCT = DataServerManager.Instance.Select24HDavgCT(model,dtTemp2);

                Dictionary<int, double>[] dicIntCTTemp = new Dictionary<int, double>[] { new Dictionary<int, double>(), new Dictionary<int, double>() };
                Dictionary<int, double>[] dicIntCTRslt = new Dictionary<int, double>[] { new Dictionary<int, double>(), new Dictionary<int, double>() };
                Dictionary<string, double>[] dicStrCTRslt = new Dictionary<string, double>[] { new Dictionary<string, double>(), new Dictionary<string, double>()};

                string[] sTimes = new string[24];
                for (int i = 0; i < 24; i++)
                {

                    string key = "";
                    double value;

                    key = dtCT.Rows[i][1].ToString();
                    sTimes[i] = key;

                    int iTemp = Convert.ToInt32(key);
                    if (dtCT.Rows[i][0] == null || dtCT.Rows[i][0].ToString() == "")
                    {
                        value = 0;
                    }
                    else
                    {
                        value = Math.Round(Convert.ToDouble(dtCT.Rows[i][0]),2);
                    }

                    dicIntCTTemp[0].Add(iTemp, value);
                }

                DateTime dTime;
                for (int i = 0; i < 24; i++)
                    {
                    string key, keyT;
                    key = sTimes[i]; 
                    keyT = Convert.ToInt32(key).ToString("00");

                    string strKey;
                    strKey = HourConvert2(key.ToString(), chart2TimeStart.AddDays(-1).AddHours(i));

                    dTime = DateTime.ParseExact(keyT, "HH", System.Globalization.CultureInfo.CurrentCulture);
                    int iTemp = dTime.AddHours(24).Hour;
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
                }
                #endregion

                #region Thống kê sản lượng ngày và đêm theo giờ；
                //24h thời gian gần nhất.
                DataTable dtCount = DataServerManager.Instance.Count24HourYield(dtTemp2, 1);

               //Dictionary<int, int>[] dicCount = new Dictionary<int, int>[] { new Dictionary<int, int>(), new Dictionary<int, int>() };
                Dictionary<string, int>[] dicCountRslt = new Dictionary<string, int>[] { new Dictionary<string, int>(), new Dictionary<string, int>() };

                //Check24h gần nhất
                for (int i = 0; i < 24; i++)
                {
                    string key2 = dtTemp2.AddHours(i).ToString("HH:00") +"\r\n" + dtTemp2.AddHours(i).ToString("dd-MM");

                    //Pass
                    int value = Convert.ToInt32(dtCount.Rows[i][0]);
                    dicCountRslt[0].Add(key2, value);
                    //Fail
                    int value1 = Convert.ToInt32(dtCount.Rows[i+24][0]);
                    dicCountRslt[1].Add(key2, value1);
                }
                #endregion
                ChartLine8H = dtCount;
                ChartLine8H.TableName = "24 Hour";


                chartIO_Update1.Refresh(dicStrCTRslt, dicCountRslt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }    

        private string HourConvert2(string hour, DateTime start)
        {
            string key = "";
            switch (hour)
            {
                case "0":
                    key = "00:00";
                    break;
                case "1":
                    key = "01:00";
                    break;
                case "2":
                    key = "02:00";
                    break;
                case "3":
                    key = "03:00";
                    break;
                case "4":
                    key = "04:00";
                    break;
                case "5":
                    key = "05:00";
                    break;
                case "6":
                    key = "06:00";
                    break;
                case "7":
                    key = "07:00";
                    break;
                case "8":
                    key = "08:00";
                    break;
                case "9":
                    key = "09:00";
                    break;
                case "10":
                    key = "10:00";
                    break;
                case "11":
                    key = "11:00";
                    break;
                case "12":
                    key = "12:00";
                    break;
                case "13":
                    key = "13:00";
                    break;
                case "14":
                    key = "14:00";
                    break;
                case "15":
                    key = "15:00";
                    break;
                case "16":
                    key = "16:00";
                    break;
                case "17":
                    key = "17:00";
                    break;
                case "18":
                    key = "18:00";
                    break;
                case "19":
                    key = "19:00";
                    break;
                case "20":
                    key = "20:00";
                    break;
                case "21":
                    key = "21:00";
                    break;
                case "22":
                    key = "22:00";
                    break;
                case "23":
                    key = "23:00";
                    break;
                case "24":
                    key = "24:00";
                    break;
            }
            return key + "\r\n" + start.ToString("dd/MM");

            //return key;
        }
        #endregion

        private void btnExport_Click_1(object sender, EventArgs e)
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
            fbd.Description = "Please select the save path";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                filePath = fbd.SelectedPath;
            }
            else
            {
                return;
            }
            filePath += @"\Data" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
            DataSetToExcel(ds, filePath);
        }
        public static bool DataSetToExcel(DataSet sourceSet, string filePath)
        {

            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
                using (ExcelPackage package = new ExcelPackage(new System.IO.FileInfo(filePath)))
                {
                    for (int k = 0; k < sourceSet.Tables.Count; k++)
                    {
                        string tableName = sourceSet.Tables[k].TableName;
                        if (tableName.Length <= 0)
                            tableName = k.ToString();
                        ExcelWorksheet ws = package.Workbook.Worksheets.Add(tableName);
                        for (int i = 0; i < sourceSet.Tables[k].Columns.Count; i++)
                        {


                            ws.Cells[1, i + 1].Value = sourceSet.Tables[k].Columns[i].ColumnName.ToString();
                            for (int j = 0; j < sourceSet.Tables[k].Rows.Count; j++)
                            {
                                ws.Cells[j + 2, i + 1].Value = sourceSet.Tables[k].Rows[j][i].ToString();
                            }
                        }
                        package.Save();
                    }
                }
                return true;
            }   
            catch (Exception e)
            {
                return false;
            }
        }
        bool isQuery = false;
        public void btn_Query_Click(object sender, EventArgs e)
        {
            try
            {
                if (isQuery == false)
                {
                    isQuery = true;
                    var startTime = chartIO_Update1.TrackStartTime;
                    var endTime = chartIO_Update1.TrackEndTime;

                    if (QueryAnother)
                    {
                        endTime = DateTime.Now;
                        startTime = DateTime.Now.AddDays(-7);
                        chartIO_Update1.TrackStartTime = startTime;
                        chartIO_Update1.TrackEndTime = endTime;
                    }

                    if ((endTime.Date - startTime.Date).Days > 30)
                    {
                        startTime = endTime.AddDays(-30);
                    }
                    //sql table
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    RefreshDEU(startTime, endTime);
                    //chart
                    RefreshUI();
                    sw.Stop();
                    Console.WriteLine(sw.ElapsedMilliseconds);
                    isQuery = false;
                    if (QueryAnother)
                    {
                        QueryAnother = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isQuery = false;
                MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.ToString());
            }


        }
        bool QueryAnother = false;
        public void QueryFromOtherForm()
        {
            QueryAnother = true;
            btn_Query_Click(null, null);
            HBMachine.Instance.UploadMachineStateMessage(MachineSts.Running);
            QueryAnother = false;
            isQuery = false;
        }
    }
}
