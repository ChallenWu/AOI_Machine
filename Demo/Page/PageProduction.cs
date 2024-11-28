using BoTech;
using Demo.UserControls;
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
using System.Net.NetworkInformation;
using Demo.Setting;

namespace Demo.Page
{
    public partial class PageProduction : UserControlBase
    {
        private static PageProduction instance;
        public static PageProduction Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new PageProduction();
                return instance;
            }
        }
        
        public PageProduction()
        {
            InitializeComponent();   
            //this.Dock = DockStyle.Fill;
            StartRefreshPage();
            IniSTN();
            IniIOSummary();
            IniDashboard();
        }
        #region STN Control 
        private void IniSTN()
        {
            LoadMachineMessage();
            
            Version av = new Version(Application.ProductVersion);
            string tt = av.Major.ToString();
            //this.stn1.SW_Version = AudioSystem.AudioMachineMessage.SW_version.Trim();
            //this.stn1.STNName = AudioSystem.AudioMachineMessage.Station + " " + AudioSystem.AudioMachineMessage.MachineNo + "#";

            //this.stn1.MS_Hash = AudioSystem.AudioMachineMessage.MS_Hash;
            //this.stn1.SiteName = AudioSystem.AudioMachineMessage.Site;
            //this.stn1.Vender = AudioSystem.AudioMachineMessage.Vendor;

            //Ouput đường dẫn file
            AudioSystem.InforMachine.Main_SW_Path = Application.StartupPath + @"\Demo.exe";
            string[] tem = AudioSystem.InforMachine.Main_SW_Path.Split('\\');

            //if (tem.Length > 3)
            //{
            //    this.stn1.Main_SW_Path = tem[0] + "\\" + tem[1] + "\\...\\" + tem[tem.Length - 1];
            //}
            //else
            //    this.stn1.Main_SW_Path = AudioSystem.AudioMachineMessage.Main_SW_Path;
        }

        private static void LoadMachineMessage()
        {
            AudioSystem.InforMachine.Main_SW_Path = Application.StartupPath + @"\Demo.exe";
            AudioSystem.InforMachine.MS_Hash = GetFileHash.SHA1(Application.StartupPath + @"\Demo.exe");
            try
            {
                    string visionFile = @"D:\CCD\BE010\CCDAlignMentSystem\CCDAlignMentSystem\bin\Debug\CCDAlignMentSystem.exe";
                    if (File.Exists(visionFile))
                    {
                        AudioSystem.InforMachine.VS_Hash = GetFileHash.SHA1(visionFile);
                    }
                    else
                    {
                        string visionTemp = Application.StartupPath + @"\CCDAlignMentSystem.exe";
                        AudioSystem.InforMachine.VS_Hash = GetFileHash.SHA1(visionTemp);
                    }
            }
            catch (Exception ex)
            {

            }

        }

        private void RefreshSTN()
        {
           IniSTN();


        //    if (AudioSystem.AudioMachineMessage.Station == AudioSystem.MachineName_BE010_1)
        //    {
        //        if (mFunction.TcpIP[(int)ParName.TCPIP.PDCA].Connected())
        //        {
        //            AudioSystem.isPDCAOk = true;
        //            this.stn1.PDCAConnected = true;
        //        }
        //        else
        //        {
        //            AudioSystem.isPDCAOk = false;
        //            this.stn1.PDCAConnected = false;
        //        }
        //    }
        //    else /*(TI040没有PDCA)*/
        //    {
        //        AudioSystem.isPDCAOk = true;
        //    }


        //    if (mFunction.TcpIP[(int)ParName.TCPIP.MES].Connected())
        //    {
        //        //AudioSystem.isMESOk = true;
        //        this.stn1.MESConnected = true;
        //    }
        //    else
        //    {
        //        //AudioSystem.isMESOk = false;
        //        this.stn1.MESConnected = false;
        //    }


        //    if (mFunction.TcpIP[(int)ParName.TCPIP.Hive].Connected())
        //    {
        //        this.stn1.HIVEConnected = true;
        //        AudioSystem.isHiveOk = true;
        //    }
        //    else
        //    {
        //        AudioSystem.isHiveOk = false;
        //        this.stn1.HIVEConnected = false;
        //    }
        }

        #endregion
        #region IO_Summary
        private void RefreshIO()
        {
           
        }

        string currentUCSN = "";
        //DateTime lastUCTime = DateTime.Now;
        public void Async_IO_Refresh(ProductMessage ucm, int unitIndex)
        {
            try
            {
                if (!IsHandleCreated)
                    this.CreateControl();

                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action(() =>
                    {

                        int OKunit = 0;
                        int NGunit = 0;
                        DateTime dtt;
                        //DateTime.TryParse(DateTime.Now.ToString("yyyy-MM-dd ") + "8:0:0", out dtt);
                        DateTime.TryParse(DateTime.Now.ToString("yyyy-MM-dd HH") + ":0:0", out dtt);//per hour
                        DataTable dt = DataServerManager.Instance.SelectUPHData(dtt, DateTime.Now);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][2] == "PASS")
                                OKunit += Convert.ToInt32(dt.Rows[i][1]);
                            else
                                NGunit += Convert.ToInt32(dt.Rows[i][1]);
                        }
                        this.iO_Summary.Input_Output = (OKunit + 1).ToString() + "/" + (OKunit + 1 + NGunit).ToString();
                        
                        if (OKunit == 0 && NGunit ==0)
                            {
                            this.iO_Summary.Yield = "0"+ "%";
                        }
                        else
                        {
                            this.iO_Summary.Yield = (OKunit * 100.0 / (OKunit + NGunit)).ToString("f2") + "%";
                        }    
                        OKunit = 0;
                        NGunit = 0;
                        DateTime.TryParse(DateTime.Now.ToString("yyyy-MM-dd HH") + ":0:0", out dtt);

                        dt = DataServerManager.Instance.SelectUPHData(dtt, DateTime.Now);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if ((string)dt.Rows[i][2] == "PASS")
                                OKunit = Convert.ToInt32(dt.Rows[i][1]);
                            else
                                NGunit = Convert.ToInt32(dt.Rows[i][1]);
                        }
                        
                        this.iO_Summary.CT = (ucm.Unit.CT).ToString("f2");

                        //if (currentUCSN != ucm.UC_SN)
                        //{
                        //    this.iO_Summary.CT = (ucm.Units[unitIndex].CT).ToString("f2");
                        //}

                        this.iO_Summary.Pass_Fail = (OKunit + 1).ToString() + "/" + NGunit.ToString();

                        this.iO_Summary.UPH = (OKunit + 1 + NGunit).ToString();

                        //this.iO_Summary.UPH = (3600/double.Parse(iO_Summary.CT)).ToString("f2");

                        this.iO_Summary.SN = ucm.Unit.UnitSN;
                        this.iO_Summary.UnitStatus = ucm.Unit.Pass == "PASS" ? true : false;
                        PageChart.Instance.Async_UnitDaily(this.iO_Summary);



                    }));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }



        private void IniIOSummary()
        {
            try
            {
                DateTime dtt;
                int OKunit = 0;
                int NGunit = 0;
                //DateTime dtStart = DsAndNsTimeSet.Instance.GetDayStartTime();
                //DataTable dt = DataServerManager.Instance.SelectUPHData(dtStart, DateTime.Now);

                DateTime.TryParse(DateTime.Now.ToString("yyyy-MM-dd HH") + ":0:0", out dtt);//request per hour

                DataTable dt = DataServerManager.Instance.SelectUPHData(dtt, DateTime.Now);
                //Select last product
                DataTable dt1 = DataServerManager.Instance.SelectLastProduct();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((string)dt.Rows[i][2] == "PASS")
                        OKunit += Convert.ToInt32(dt.Rows[i][1]);
                    else
                        NGunit += Convert.ToInt32(dt.Rows[i][1]);
                }
                //if ((OKunit + NGunit) == 0)
                //{ return; }
                this.iO_Summary.SN = dt1.Rows[0][6].ToString();


                this.iO_Summary.Input_Output = OKunit.ToString() + "/" + NGunit.ToString();
                this.iO_Summary.Pass_Fail = OKunit.ToString() + "/" + NGunit.ToString();
                this.iO_Summary.UPH = (OKunit + NGunit).ToString();

                if (OKunit == 0 && NGunit == 0)
                {
                    this.iO_Summary.Yield = "0" + "%";
                }
                else
                {
                    this.iO_Summary.Yield = (OKunit * 100.0 / (OKunit + NGunit)).ToString("f2") + "%";
                }

                PageChart.Instance.Async_UnitDaily(this.iO_Summary);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



        }
        #endregion

        #region  CriticalPameterDashBoard


        private void IniDashboard()
        {
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Key Name", typeof(System.String));
            DataColumn dc1 = new DataColumn("Value", typeof(System.Double));
            DataColumn dc2 = new DataColumn("LSL", typeof(System.Double));
            DataColumn dc3 = new DataColumn("USL", typeof(System.Double));
            dt.Columns.Add(dc);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);


            //if (mFunction.mParList[(int)ParName.ChkPar.开启显示重要参数].CheckSts)

            //Enable important parameter
            if (true)
            {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[0][0] = "Air Pressure";
                dt.Rows[0][1] = "0.6";
                dt.Rows[0][2] = "0.5";
                dt.Rows[0][3] = "0.7";
            }
            criticalPameterDashBoard1.Refresh(dt);
        }



        private void button1_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("Key Name", typeof(System.String));
            DataColumn dc1 = new DataColumn("Value", typeof(System.Double));
            DataColumn dc2 = new DataColumn("LSL", typeof(System.Double));
            DataColumn dc3 = new DataColumn("USL", typeof(System.Double));
            dt.Columns.Add(dc);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            Random rd = new Random();
            for (int i = 0; i < 10; i++)
            {
                dt.Rows.Add(dt.NewRow());

                dt.Rows[i][0] = "Name" + i.ToString();
                dt.Rows[i][1] = rd.Next(10, 100) * 1.0 / 10;
                dt.Rows[i][2] = 3.5;
                dt.Rows[i][3] = 7.5;
            }
            criticalPameterDashBoard1.Refresh(dt);
        }

        #endregion

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

            //刷新Machine State Changes
            RefreshMSC();
            this.machineErrorStatistics1.DoubleBarCurrentTime = DateTime.Now;
            RefreshMESS(this.machineErrorStatistics1.TrackStartTime, this.machineErrorStatistics1.TrackEndTime);
            //刷新STN
            RefreshSTN();
            //ParCheckItemLoad();
            

            if (machineErrorStatistics1.IsSelectTime == false)
            {
                this.StatusRefreshTimer.Start();
            }


            //Ping PLC
            if (PingIpOrDomainName("127.0.0.1"))
            {
                pnPLCStatus.BackColor = Color.Green;
            }
            else
            {
                pnPLCStatus.BackColor = Color.Red;
            }

            //Ping MES
            if (PingIpOrDomainName("127.0.0.1"))
            {
                pnMESStatus.BackColor = Color.Green;
            }
            else
            {
                pnMESStatus.BackColor = Color.Red;
            }
            //Ping ICW
            if (PingIpOrDomainName("127.0.0.1"))
            {
                pnICWStatus.BackColor = Color.Green;
            }
            else
            {
                pnICWStatus.BackColor = Color.Red;
            }
            //Ping CCD
            //if (PingIpOrDomainName(Globals.SettingICT.ScanLead_IP))
            //{
            //    pnCCDStatus.BackColor = Color.Green;
            //}
            //else
            //{
            //    pnCCDStatus.BackColor = Color.Red;
            //}
        }

        #region    MachineStateChange
        private void RefreshMSC()
        {
            //Get database data
            DateTime now = DateTime.Now;
            //DateTime now = new DateTime(2024, 1, 29);
            DateTime before = now.AddDays(-6).AddHours(-1);
            DataTable mscTable = DataServerManager.Instance.SelectMachineState(before, now);


            #region   //控件需要的数据表
            DataTable dt = new DataTable();
            DataColumn dc0 = new DataColumn("Date", typeof(System.DateTime));
            DataColumn dc1 = new DataColumn("Running", typeof(System.Double));
            DataColumn dc2 = new DataColumn("Idle", typeof(System.Double));
            DataColumn dc3 = new DataColumn("Engineering", typeof(System.Double));
            DataColumn dc4 = new DataColumn("PlannedDT", typeof(System.Double));
            DataColumn dc5 = new DataColumn("Downtime", typeof(System.Double));
            dt.Columns.Add(dc0);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            for (int i = 0; i < 7; i++)
            {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[i][0] = now.AddDays(i - 6).ToString("yyyy/MM/dd");
                dt.Rows[i][1] = 0;
                dt.Rows[i][2] = 0;
                dt.Rows[i][3] = 0;
                dt.Rows[i][4] = 0;
                dt.Rows[i][5] = 0;
            }
            #endregion

            #region chuyển đổi dữ liệu
            //Lấy dữ liệu từ bảng dt SQL ra



            for (int i = 0; i < mscTable.Rows.Count; i++)
            {
                DateTime rowTime = (DateTime)mscTable.Rows[i][0];
                int row = 6 - (now - rowTime).Days;
                int colum = (int)mscTable.Rows[i][1];
                if (row >= 0 && colum > 0)
                {
                    //dt.Rows[row][colum] = TimeSpan.FromTicks(Convert.ToInt64(mscTable.Rows[i][3])).TotalMinutes;
                    //edit
                    //Console.WriteLine(mscTable.Rows[i][3].ToString());
                    dt.Rows[row][colum] = TimeSpan.FromSeconds(Convert.ToInt64(mscTable.Rows[i][3])).TotalMinutes;

                }
            }
            #endregion

            //dataGridView1.DataSource = dt;

            //Refresh interface
            //Xử lý dữ liệu bảng dt trên khi import vào chart
            //TimeSpan date1 = now - now.Date;
            //double timeDiff = date1.TotalMinutes;
            //for (int i = 6; i >= 0; i--)
            //{
            //    for (int j = 1; j < 6; j++) //Tính giá trị các cột
            //    {
            //        if (Convert.ToDouble(dt.Rows[i][j]) > 1440)
            //        {
            //            dt.Rows[i - 1][j] = Convert.ToDouble(dt.Rows[i - 1][j]) + (Convert.ToDouble(dt.Rows[i][j]) - 1440);
            //            if (i == 6) // Ngày hiện tại
            //            {
            //                dt.Rows[i][j] = timeDiff;
            //            }
            //            else
            //                dt.Rows[i][j] = 1440;
            //        }
            //    }
            //}

            this.machineStateChanges1.Refresh(dt);

        }
        private void Tt2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataColumn dc0 = new DataColumn("Date", typeof(System.DateTime));
            DataColumn dc1 = new DataColumn("Running", typeof(System.Double));
            DataColumn dc2 = new DataColumn("Idle", typeof(System.Double));
            DataColumn dc3 = new DataColumn("Engineering", typeof(System.Double));
            DataColumn dc4 = new DataColumn("PlannedDT", typeof(System.Double));
            DataColumn dc5 = new DataColumn("Downtime", typeof(System.Double));
            dt.Columns.Add(dc0);
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            for (int i = 0; i < 7; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }

            dt.Rows[0][0] = DateTime.Now.AddDays(-6);
            dt.Rows[0][1] = 440;
            dt.Rows[0][2] = 100;
            dt.Rows[0][3] = 100;
            dt.Rows[0][4] = 100;
            dt.Rows[0][5] = 700;


            dt.Rows[1][0] = DateTime.Now.AddDays(-5);
            dt.Rows[1][1] = 240;
            dt.Rows[1][2] = 100;
            dt.Rows[1][3] = 200;
            dt.Rows[1][4] = 200;
            dt.Rows[1][5] = 600;

            dt.Rows[2][0] = DateTime.Now.AddDays(-4);
            dt.Rows[2][1] = 140;
            dt.Rows[2][2] = 100;
            dt.Rows[2][3] = 400;
            dt.Rows[2][4] = 300;
            dt.Rows[2][5] = 500;


            dt.Rows[3][0] = DateTime.Now.AddDays(-3);
            dt.Rows[3][1] = 440;
            dt.Rows[3][2] = 100;
            dt.Rows[3][3] = 100;
            dt.Rows[3][4] = 400;
            dt.Rows[3][5] = 400;

            dt.Rows[4][0] = DateTime.Now.AddDays(-2);
            dt.Rows[4][1] = 440;
            dt.Rows[4][2] = 100;
            dt.Rows[4][3] = 200;
            dt.Rows[4][4] = 300;
            dt.Rows[4][5] = 400;


            dt.Rows[5][0] = DateTime.Now.AddDays(-1);
            dt.Rows[5][1] = 440;
            dt.Rows[5][2] = 100;
            dt.Rows[5][3] = 300;
            dt.Rows[5][4] = 400;
            dt.Rows[5][5] = 200;


            dt.Rows[6][0] = DateTime.Now.AddDays(0);
            dt.Rows[6][1] = 440;
            dt.Rows[6][2] = 200;
            dt.Rows[6][3] = 200;
            dt.Rows[6][4] = 200;
            dt.Rows[6][5] = 100;


            //this.machineStateChanges1.DataSourceTable = dt;
            this.machineStateChanges1.Refresh(dt);



        }
        #endregion

        #region MachineErrorStatistics

        private void RefreshMESS(DateTime start, DateTime end)
        {
            //DateTime now = DateTime.Now;
            //DateTime before = now.AddDays(-7);  
            DataTable mesTable = DataServerManager.Instance.SelectDT_Statistic(start, end, false);

            #region 创建接口数据表格
            DataTable dt = new DataTable();

            DataColumn dc0 = new DataColumn("ErrorCode", typeof(System.String));
            DataColumn dc1 = new DataColumn("数量", typeof(System.Int32));
            dt.Columns.Add(dc0);
            dt.Columns.Add(dc1);
            #endregion

            #region 数据转换
            int allCount = mesTable.Rows.Count;
            allCount = allCount >= 10 ? 10 : allCount;

            for (int i = 0; i < allCount; i++)
            {
                dt.Rows.Add(dt.NewRow());
                dt.Rows[i][0] = mesTable.Rows[i][0];
                dt.Rows[i][1] = mesTable.Rows[i][2];
            }

            #region 补齐报警条数；
            dt = AlarmAdd(dt);

            #endregion

            this.machineErrorStatistics1.Refresh(dt);
            #endregion
        }

        private DataTable AlarmAdd(DataTable dt)
        {
            DataTable dtTemp = new DataTable();
            DataColumn dc_0 = new DataColumn("ErrorCode", typeof(System.String));
            DataColumn dc_1 = new DataColumn("Quantity", typeof(System.Int32));
            dtTemp.Columns.Add(dc_0);
            dtTemp.Columns.Add(dc_1);


            try
            {

                List<string> lTemp = new List<string>();
                List<string> lTemp2 = new List<string>();
                List<string> lTemp3 = new List<string>();
                if (dt.Rows.Count < AudioSystem.Errors.Length)
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0] == null)
                        {
                            dt.Rows[i][0] = "";
                        }

                        lTemp.Add(dt.Rows[i][0].ToString());
                    }


                    lTemp2 = AudioSystem.Errors.ToList();

                    for (int i = 0; i < lTemp2.Count; i++)
                    {
                        bool isHave = true;
                        for (int j = 0; j < lTemp.Count; j++)
                        {
                            isHave = true;
                            if (lTemp[j] == lTemp2[i])
                            {
                                continue;
                            }
                            else
                            {
                                isHave = false;
                            }
                        }

                        if (lTemp3.Count == 8 - lTemp.Count)
                        {
                            break;
                        }

                        if (isHave == false || lTemp.Count == 0)
                        {
                            lTemp3.Add(lTemp2[i]);
                        }
                    }



                    for (int i = 0; i < lTemp3.Count; i++)
                    {
                        DataRow dr;
                        dr = dtTemp.NewRow();
                        dr[0] = lTemp3[i];
                        dr[1] = 0;
                        dtTemp.Rows.Add(dr);

                    }



                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr;
                        dr = dtTemp.NewRow();
                        dr[0] = dt.Rows[i][0];
                        dr[1] = dt.Rows[i][1];
                        dtTemp.Rows.Add(dr);
                    }
                    dt.Rows.Clear();
                    dt = dtTemp;

                }



            }
            catch (Exception ex)
            {

            }
            return dt;
        }


        private void UpdateMachineErrorStatistics(DateTime start, DateTime end)
        {
            try
            {
                if (machineErrorStatistics1.IsSelectTime)
                {
                    DataTable mesTable = DataServerManager.Instance.SelectDT_Statistic(start, end, false);
                    #region 创建接口数据表格
                    DataTable dt = new DataTable();
                    DataColumn dc0 = new DataColumn("ErrorCode", typeof(System.String));
                    DataColumn dc1 = new DataColumn("数量", typeof(System.Int32));
                    dt.Columns.Add(dc0);
                    dt.Columns.Add(dc1);
                    #endregion

                    #region 数据转换
                    int allCount = mesTable.Rows.Count;
                    allCount = allCount >= 10 ? 10 : allCount;

                    for (int i = 0; i < allCount; i++)
                    {
                        dt.Rows.Add(dt.NewRow());
                        dt.Rows[i][0] = mesTable.Rows[i][0];
                        dt.Rows[i][1] = mesTable.Rows[i][2];
                    }

                    #region 补齐报警条数；
                    dt = AlarmAdd(dt);

                    #endregion

                    this.machineErrorStatistics1.Refresh(dt);
                    #endregion


                }
            }
            catch (Exception)
            {

            }

        }






        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataColumn dc0 = new DataColumn("ErrorCode", typeof(System.String));
            DataColumn dc1 = new DataColumn("数量", typeof(System.Int32));



            dt.Columns.Add(dc0);
            dt.Columns.Add(dc1);


            //前十位，报警数量最多的
            for (int i = 0; i < 10; i++)
            {
                dt.Rows.Add(dt.NewRow());
            }

            dt.Rows[0][0] = "MES Error";
            dt.Rows[0][1] = 40;



            dt.Rows[1][0] = "Safety Error";
            dt.Rows[1][1] = 60;


            dt.Rows[2][0] = "Scanning Error";
            dt.Rows[2][1] = 80;



            dt.Rows[3][0] = "Vision Error";
            dt.Rows[3][1] = 90;


            dt.Rows[4][0] = "Cylinder Error";
            dt.Rows[4][1] = 140;



            dt.Rows[5][0] = "Sensor Error";
            dt.Rows[5][1] = 640;



            dt.Rows[6][0] = "Motion Error";
            dt.Rows[6][1] = 740;



            dt.Rows[7][0] = "Vision Error";
            dt.Rows[7][1] = 840;


            dt.Rows[8][0] = "Cylinder Error";
            dt.Rows[8][1] = 1000;



            dt.Rows[9][0] = "Sensor Error";
            dt.Rows[9][1] = 1800;

            this.machineErrorStatistics1.DataSourceTable = dt;


            this.machineErrorStatistics1.Refresh(dt);
            //RefreshMESS();
        }

        #endregion

        #region FileHash
        public static class GetFileHash
        {
            public static string SHA1(string strFileName)
            {
                return HashFile(strFileName, "sha1");
            }

            private static string HashFile(string strFileName, string algname)
            {
                if (!System.IO.File.Exists(strFileName))
                    return string.Empty;
                System.IO.FileStream fs = new System.IO.FileStream(strFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] hashBytes = HashData(fs, algname);
                fs.Close();
                return ByteArrayToHexString(hashBytes);
            }

            private static byte[] HashData(System.IO.Stream stream, string algName)
            {
                System.Security.Cryptography.HashAlgorithm algorithm;
                if (string.Compare(algName, "sha1", true) == 0)
                {
                    algorithm = System.Security.Cryptography.SHA1.Create();
                }
                else
                {
                    throw new ArgumentException("algName不能为NUll");
                }
                return algorithm.ComputeHash(stream);
            }
            private static string ByteArrayToHexString(byte[] buf)
            {
                return BitConverter.ToString(buf).Replace("-", "");
            }
        }
        #endregion

        #region Ping Domain
        public static bool PingIpOrDomainName(string strIpOrDName)
        {
            try
            {
                Ping objPingSender = new Ping();

                PingOptions objPinOptions = new PingOptions();

                objPinOptions.DontFragment = true;

                string data = "";

                byte[] buffer = Encoding.UTF8.GetBytes(data);

                int intTimeout = 120;

                PingReply objPinReply = objPingSender.Send(strIpOrDName, intTimeout, buffer, objPinOptions);

                string strInfo = objPinReply.Status.ToString();

                if (strInfo == "Success")
                {
                    return true;
                }

                else
                {
                    return false;
                }

            }

            catch (Exception)
            {

                return false;

            }

        }
        #endregion

        #region Button
        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            try
            {
                var filePath = @"E:\VDB\VisionDB";
                if (Directory.Exists(filePath))
                {
                    System.Diagnostics.Process.Start(filePath);
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnOpenReport_Click(object sender, EventArgs e)
        {
            try
            {
                var filePath = "E:\\HB_Record\\Report";
                if (Directory.Exists(filePath))
                {
                    System.Diagnostics.Process.Start(filePath);
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
    }
}
