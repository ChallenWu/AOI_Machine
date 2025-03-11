using Demo.Page;
using Demo.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCore;
using HB_IWatch;
using Demo;
using System.Diagnostics;
using static Demo.Globals;
using Demo.Device;
using BoTech;
using Demo.Task;
using Models;
using System.Threading;
using System.Text.RegularExpressions;
using Sunny.UI;
using AutoStudio.Forms.GUI_Forms;
using static Demo.Form1;

namespace Demo
{
    public partial class Form1 : Form
    {
        private Dictionary<newMenuButton, Control> pageMap = new Dictionary<newMenuButton, Control>();
        private Stopwatch SW_Mouse = new Stopwatch();
        private int MouseX = 0;
        private int MouseY = 0;
        public static List<string> lstModelNumber = new List<string>();
        Rectangle OriginFormSize;

        private static Form1 _instance;
        public static Form1 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Form1();
                return _instance;
            }
        }
        public Form1()
        {

            //Lấy dữ liệu model sử dụng
            LoadModel.StartUp();
            
            //var mainPage = PageEngineering.Instance;
            //Cài đặt đa ngôn ngữ
            MultiLanguage.ReadMultiLangFile("Language.xlsx");
            MultiLanguage.LoadFile("Language.xlsx");
            MultiLanguage.RegisterForms(this);



            OriginFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            this.MinimumSize = this.Size;

            Globals.BindDevice();

            //Globals.AddProcessCreatorUI();

            //Tạo đường dẫn các file
            Globals.CreateAllDirectory();

            //Init AxisCard
            //Globals.InitDevice();

            XController.Instance.Start();   // Run Event Server

            XAlarmReporter.Instance.Start();  // Start Alarm System

            System.Threading.Thread.Sleep(50);

            XMachine.Instance.Start(); // Machine IO System Start

            CsvServer.Instance.Start();       // Excel 引擎啟動
                                              // 
                                              // Connect PLC


            // Connect external device
            ExternalDevice();


            InitializeComponent();
            InitPages();
            
            this.StartPosition = FormStartPosition.CenterScreen;
            //Khởi tạo các task
            InitTask();
            //Set run mode
            this.lbEQMStatus.Text = MultiLanguage.GetMessage(Globals.RUNMODE.ToString());
            this.pnEQMStatus.BackColor = MyColor.Green;
            //Load dữ liệu từ SQLite
            DataServerManager.Instance.Load();

            //PageLogin.Instance.UpdateLabelRequested += (user, level) =>
            //{
            //    lblUser.Text = "User: " + user;
            //    lblLevel.Text = "Level: " + level;
            //};

            PageEngineering.Instance.AddLogRichtextBox("Initilize software complete",Color.Green);

            this.lbModelRun.Text = LoadModel.appSettings.currentModel;

            PLCStatus = new Thread(new ThreadStart(AsyncPlc));
            isRunning = true;
            PLCStatus.IsBackground = true;
            PLCStatus.Start();
            
            //Đặt window thành maximized
            //this.WindowState = FormWindowState.Maximized;
            //this.MinimizeBox = false;
            //this.MaximizeBox = false;
            startTime = DateTime.Now;
        }
        Thread PLCStatus;
        bool isRunning;
        private void InitTask()
        {
            XTaskManager.Instance.Initialize();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Globals.SetStateTop += Globals_SetStateTop;
            XCore.XTask.OnPauseActive += this.OnPauseActive;
            XCore.XTask.OnStopActive += this.OnStopRunning;

            Globals.OnPauseActive += this.OnPauseActive;
            Globals.OnStopActive += this.OnStopRunning;
            timer1.Start();
        }
        private void MainDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
            ExitTask();
            Process.GetCurrentProcess().Kill();
        }

        private void KillTaskProcess(string sFileName)
        {
            try
            {
                // Get all processes running on the local computer.
                Process[] localAll = Process.GetProcesses();

                //Start: find, list and kll Process  keyword code
                foreach (Process i in localAll)
                {
                    System.Text.RegularExpressions.Regex r;
                    System.Text.RegularExpressions.Match m;

                    // save the input string
                    string strEscape;

                    strEscape = Regex.Escape(sFileName);

                    r = new Regex(@strEscape,
                        RegexOptions.IgnoreCase | RegexOptions.Compiled);

                    m = r.Match(i.ProcessName.ToString());

                    if (m.Success)
                    {
                        Process[] killProcess = Process.GetProcessesByName(i.ProcessName.ToString());
                        killProcess[0].Kill();
                        m.NextMatch();
                    }
                }
                //End: find, list and kll Process  keyword code
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("KillTaskProcess Exception: " + ex.Message);
            }
        }

        TimeSpan runTime;
        DateTime startTime;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to exit the program?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            {
                if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                //DateTime dtt = DateTime.Now.AddSeconds(-1);
                //DataTable temDt = DataServerManager.Instance.SelectLastMachineState();
                //HiveMessage hm = new HiveMessage() { HappenTime = DateTime.Now };

                //if (temDt.Rows.Count <= 0)
                //{

                //}
                //else
                //{
                //    DateTime dtt = (DateTime)temDt.Rows[0][1];
                //    hm.PreviousState = (int)temDt.Rows[0][3];
                //    hm.MachineState = (int)temDt.Rows[0][3];
                //    hm.TimeDuration = (long)((hm.HappenTime - dtt).TotalSeconds);
                //}
                //DataServerManager.Instance.InsertMachineState(hm);

                HBMachine.Instance.UploadMachineStateMessage(MachineSts.Idle);
                HBMachine.Instance.CancelAlarmForm();
                isRunning = false;

                Globals.DisconectCamera();

                Thread.Sleep(300);
                Environment.Exit(0);

                //KillTaskProcess("Demo");
                ExitTask();
                Process.GetCurrentProcess().Kill();
            }

        }
        private void ExitTask()
        {
            XTaskManager.Instance.Exit();
        }
        private void InitPages()
        {
            this.menuButton_Start.SetBackColor(MyColor.Green, MyColor.None);
            this.menuButton_Pause.SetBackColor(MyColor.Blue, MyColor.None);
            this.menuButton_Stop.SetBackColor(MyColor.Red, MyColor.None);

            pageMap.Add(menuButton_Login, PageLogin.Instance);
            pageMap.Add(menuButton_Alarm, PageAlarm.Instance);
            pageMap.Add(menuButton_Chart, PageChart.Instance);
            pageMap.Add(menuButton_Home, PageProduction.Instance);
            pageMap.Add(menuButton_Vison, PageVision.Instance);
            pageMap.Add(menuButton_Setting, PageSetting.Instance);

            //PageEngineering = new PageEngineering();

            foreach (KeyValuePair<newMenuButton, Control> kvp in pageMap)
            {

                kvp.Value.Location = new Point(0, 0);
                kvp.Key.OnClicked += Key_OnClicked;
            }
            menuButton_Home.Selected = true;



            this.Shown += (s, e) =>
            {
                ((UserControlBase)pageMap[menuButton_Home]).Dock = DockStyle.Fill;
                this.pageContainer.Controls.Clear();
                this.pageContainer.Controls.Add(pageMap[menuButton_Home]);
            };

            var page = (PageLogin)pageMap[menuButton_Login];
            page.ONShowPage += ShowModePage;
        }
        private void Key_OnClicked(object sender, EventArgs e)
        {
            newMenuButton menuClicked = sender as newMenuButton;

            ((UserControlBase)pageMap[menuClicked]).Dock = DockStyle.Fill;

            //Tạo sự kiện load init mỗi trang
            foreach (var item in pageMap)
            {
                ((UserControlBase)item.Value).StopRefreshPage();
            }
            ((UserControlBase)pageMap[menuClicked]).StartRefreshPage();


            if (menuClicked.Name == "menuButton_Setting")
            {
                if (UserAccountControl.currentAccount.UserPermission == Privilige.Operator)
                {
                    menuClicked.Selected = false;
                    return;
                }
            }
            if (menuClicked.Name == menuButton_Home.Name)
            {
                ((PageLogin)pageMap[menuButton_Login]).NoneUserPrivilige();
                UserAccountControl.currentAccount = UserAccountControl.Operator;
                //lblUser.Text = "User: None";
                //lblLevel.Text = "Level: Operator";
            }

            foreach (KeyValuePair<newMenuButton, Control> kvp in pageMap)
            {
                if (kvp.Key.Name == menuClicked.Name)
                {
                    kvp.Key.Selected = true;
                    var map = pageMap[kvp.Key];                  


                    this.pageContainer.Controls.Clear();
                    this.pageContainer.Controls.Add(pageMap[kvp.Key]);
                }
                else
                {
                    kvp.Key.Selected = false;
                }
            }

        }
        private void ShowModePage(ModeType mode)
        {
            switch (mode)
            {
                case ModeType.Engineering:
                    if (XMachine.Instance.MachineMode == MachineModeType.CPK || XMachine.Instance.MachineMode == MachineModeType.GRR)
                    {
                        if (MessageBox.Show("The current mode is CPK/GRR. If you enter Enginnering, you will automatically exit CPK/GRR mode.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    //Globals.isEnableCPK = false;
                    this.pageContainer.Controls.Clear();
                    this.pageContainer.Controls.Add(PageEngineering.Instance);                    

                    break;
                case ModeType.Production:
                    this.pageContainer.Controls.Clear();
                    this.pageContainer.Controls.Add(pageMap[menuButton_Home]);
                    
                    //Globals.isEnableCPK = false;
                    ((PageLogin)pageMap[menuButton_Login]).NoneUserPrivilige();
                    UserAccountControl.currentAccount = UserAccountControl.Operator;
                    break;
                case ModeType.CPK:
                case ModeType.GRR:
                    if (MessageBox.Show("Confirm using CPK/GRR mode", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        return;
                    }
                    this.pageContainer.Controls.Clear();
                    //if (Globals.SettingOption.A是否开启PDCA == false)
                    //    MessageBox.Show("PDCA chưa được kích hoạt", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
            XMachine.Instance.MachineMode = (MachineModeType)mode;
        }
        private void menu_Click(object sender, EventArgs e)
        {
            newMenuButton menuClicked = sender as newMenuButton;

            ((UserControlBase)pageMap[menuClicked]).Dock = DockStyle.Fill;

            //Tạo sự kiện load init mỗi trang
            foreach (var item in pageMap)
            {
                ((UserControlBase)item.Value).StopRefreshPage();
            }
            ((UserControlBase)pageMap[menuClicked]).StartRefreshPage();


            if (menuClicked.Name == "menuButton_Setting")
            {
                if (UserAccountControl.currentAccount.UserPermission == Privilige.Operator)
                {
                    menuClicked.Selected = false;
                    return;
                }
            }
            if (menuClicked.Name == menuButton_Home.Name)
            {
                ((PageLogin)pageMap[menuButton_Login]).NoneUserPrivilige();
                UserAccountControl.currentAccount = UserAccountControl.Operator;
                //lblUser.Text = $"User: {UserAccountControl.currentAccount.Name}";
                //lblLevel.Text = $"Level: {UserAccountControl.currentAccount.UserPermission}";
            }

            foreach (KeyValuePair<newMenuButton, Control> kvp in pageMap)
            {
                if (kvp.Key.Name == menuClicked.Name)
                {

                    kvp.Key.Selected = true;
                    this.pageContainer.Controls.Clear();
                    this.pageContainer.Controls.Add(pageMap[kvp.Key]);
                }
                else
                {
                    kvp.Key.Selected = false;
                }
            }
            //switchButton_MachineInfo.STS = false;
        }
        private bool CheckSafeDoor()
        {
            bool safeDoorSts = true;
            foreach (KeyValuePair<XDi, bool> kvp in XMachine.Instance.signalDoor)
            {
                if (XMachine.Instance.DictDoorDiCheck.Keys.Contains(kvp.Key) && !XMachine.Instance.DictDoorDiCheck[kvp.Key])
                {
                    continue;
                }
                DISTSTYPE diKeySts = DISTSTYPE.LOW;
                kvp.Key.GetDi(ref diKeySts);

                if (diKeySts == DISTSTYPE.LOW)
                {
                    //if (!Globals.SettingParameter.Safedoor && (!kvp.Value))
                    //    continue;
                    //else
                    //{
                    //    safeDoorSts = false;
                    //    break;
                    //}
                }
            }
            return safeDoorSts;
        }
        public static void OnparameterChanged(object sender, EventArgs e)
        {
            parameterControl();
        }
        public static bool parameterControl()
        {
            //Sử dụng hệ thống MES để kiểm soát parameter của thiết bị
            //if (!Globals.SettingOption.是否开启参数管控)
            //    return true;
            //try
            //{
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //    return false;
            //}
            return true;
        }
        #region Button Control
        private void menuButton_Start_Click(object sender, EventArgs e)
        {
            if (!CheckSafeDoor())
            {
                MessageBox.Show("Safety door is opened. Please close the door and try it again.");
                return;
            }
            try
            {
                if (!CsvServer.Instance.Server.IsAlive)
                {
                    BzMessagebox.Show("Server CSV has a problem. Cant save the CSV file \r\n Please restart the software.");
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            //Đặt lại trạng thái của các button Pause và Stop
            this.menuButton_Pause.IsSelect(false);
            this.menuButton_Stop.IsSelect(false);
            //Kiểm tra trạng thái của các TaskId
            var st1 = XStationManager.Instance.FindStationById((int)StationId.Scanner).State;
            //Thiết bị đã reset xong

            if (st1 == XStationState.WAITRUN)
            {
                #region Chế độ tự động
                //if (!parameterControl())
                //{
                //    this.menuButton_Start.IsSelect(false);
                //    this.menuButton_Pause.IsSelect(false);
                //    this.menuButton_Stop.IsSelect(false);
                //    return;
                //}
                PageEngineering.Instance.AddLogRichtextBox("Start Autorun mode",Color.Green);
                if (BzMessagebox.Show("Are you want AutoRun? \r\nRun mode：" + Globals.RUNMODE.ToString(), "Tips", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if(Globals.SettingParameter.Async_HMI)
                    {
                        SLMP.Instance.ReadBit(DevideCode.M, 13, out bool initOK);
                        if(initOK)
                        {
                            //Write Bit Run to Plc
                            //manual mode
                            SLMP.Instance.WriteBit(DevideCode.L, 6, true);

                            SLMP.Instance.WriteBit(DevideCode.L, 1, true);
                            Thread.Sleep(50);
                            SLMP.Instance.WriteBit(DevideCode.L, 1, false);
                        }

                        else
                        {
                            BzMessagebox.Show("Initilize PLC is not complete.");
                        }
                    }    
                    else
                     StartProgram();
                }
                else
                {
                    this.menuButton_Start.IsSelect(false);
                    this.menuButton_Pause.IsSelect(false);
                    this.menuButton_Stop.IsSelect(false);
                }
                #endregion
            }

            
            else if (st1 == XStationState.PAUSE)
            {
                PageEngineering.Instance.AddLogRichtextBox("The device change to the pause station", Color.Green);

                if (Globals.SettingParameter.Async_HMI)
                {
                    SLMP.Instance.WriteBit(DevideCode.L, 1, true);
                    Thread.Sleep(50);
                    SLMP.Instance.WriteBit(DevideCode.L, 1, false);
                }    
                else
                 ContinueProgram();
            }
            else if (st1 == XStationState.WAITRESET || st1 == XStationState.STOP || st1 == XStationState.RESETING)
            {
                if (BzMessagebox.Show(MultiLanguage.GetMessage("Confirm to start reset?"), MultiLanguage.GetMessage("Confirm"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    PageEngineering.Instance.AddLogRichtextBox("Start reset program process", Color.Green);

                    if (Globals.SettingParameter.Async_HMI)
                    {
                        //Kiểm tra xem PLC đã init hay chưa
                        SLMP.Instance.ReadBit(DevideCode.M, 31, out bool value);
                        if (value == false)
                        {
                            //Chuyền về trạng thái Manual
                            SLMP.Instance.WriteBit(DevideCode.L, 6, false);
                            Thread.Sleep(50);
                            //Gửi bit reset xuống plc
                            SLMP.Instance.WriteBit(DevideCode.L, 8, true);
                            Thread.Sleep(10);
                            SLMP.Instance.WriteBit(DevideCode.L, 8, false);
                            //InitilizeProgram();
                        }
                        else
                        {                            
                            InitilizeProgram();
                        }
                    } 
                    else
                    {
                        InitilizeProgram();
                    }    
                }
            }
        }
        private void menuButton_Stop_Click(object sender, EventArgs e)
        {
            if (BzMessagebox.Show(MultiLanguage.GetMessage("Confirm that the machine is stopped?"), "Tips",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                WriteMainLog("Stop, switchs to Idle status");
                PageEngineering.Instance.AddLogRichtextBox("Stop Equipment", Color.Green);
                if (Globals.SettingParameter.Async_HMI)
                {
                    StopProgram();
                    SLMP.Instance.WriteBit(DevideCode.L, 2, true);
                    Thread.Sleep(50);
                    SLMP.Instance.WriteBit(DevideCode.L, 2, false);
                }    
                else
                {
                    StopProgram();
                }    
            }
        }
        private void menuButton_Pause_Click(object sender, EventArgs e)
        {
            PageEngineering.Instance.AddLogRichtextBox("Pause the equipment", Color.Blue);
            Globals.WriteMainLog("Pause the equipment");
            if (Globals.SettingParameter.Async_HMI)
            {
                SLMP.Instance.WriteBit(DevideCode.L, 3, true);
                Thread.Sleep(10);
                SLMP.Instance.WriteBit(DevideCode.L, 3, false);
            }    
            else
             PauseProgram();
        }
        #endregion
        #region HMI Control
        public void PauseProgram()
        {
            if (!CheckSafeDoor())
            {
                MessageBox.Show("Cửa an toàn đã được mở. Vui lòng đóng cửa an toàn và thử lại.");
                Globals.WriteMainLog("Cửa an toàn đã được mở. Vui lòng đóng cửa an toàn và thử lại.");
                return;
            }

            var st1 = XStationManager.Instance.FindStationById((int)StationId.Scanner).State;
            if (st1 != XStationState.STOP)
            {
                HBMachine.Instance.UploadMachineStateMessage(MachineSts.Idle);
                XStationManager.Instance.Pause();
                this.menuButton_Start.IsSelect(false);
                Globals.WriteMainLog("Equipment switchs to the Pause state");
                this.menuButton_Stop.IsSelect(false);
            }
            else
            {
                this.menuButton_Pause.IsSelect(false);
            }
        }
        private void ContinueProgram()
        {
            HBMachine.Instance.UploadMachineStateMessage(MachineSts.Running);
            XStationManager.Instance.Continue();
            this.menuButton_Start.IsSelect(true);            
        }
        public void StopProgram()
        {
            if (HBMachine.Instance.IsMachineRunning())
            {
                XStationManager.Instance.Stop();
                System.Threading.Thread.Sleep(1000);
                if (Globals.OnWaitTaskStop != null)
                    Globals.OnWaitTaskStop();
            }
            else
            {
                XStationManager.Instance.Stop();
            }
            this.menuButton_Start.IsSelect(false);
            this.menuButton_Pause.IsSelect(false);
            this.menuButton_Stop.IsSelect(true);

            HBMachine.Instance.SetMachineStatus(MachineSts.Idle);
            HBMachine.Instance.UploadMachineStateMessage(MachineSts.Idle);
            Globals.WriteMainLog("Equipment switches to the Stop status");
            PageEngineering.Instance.AddLogRichtextBox("Equipment switches to the Stop status", Color.Red);
        }
        public void StartProgram()
        {
            //Update trạng thái thiết bị 
            HBMachine.Instance.UploadMachineStateMessage(MachineSts.Running);
            //Ghi log
            Globals.WriteMainLog("The machine starts" + Globals.RUNMODE + "mode");
            var runMode = StationRunMode.AutoRun;
            //Chọn chế chạy của thiết bị
            if (Globals.RUNMODE == MachineRunMode.NormalRun ||
                Globals.RUNMODE == MachineRunMode.Entire_Machine_Dry_Run)
            {
                XStationManager.Instance.FindStationById((int)StationId.Scanner).Start(runMode);
            }
            else if (Globals.RUNMODE == MachineRunMode.Conveyor_Dry_Run)
            {
                XStationManager.Instance.FindStationById((int)StationId.Scanner).Start(runMode);
            }
            else if (Globals.RUNMODE == MachineRunMode.DryRun)
            {
                XStationManager.Instance.FindStationById((int)StationId.Scanner).Start(runMode);
            }
            else
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("The currently selected operation mode is wrong"),
                    MultiLanguage.GetMessage("Mode Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            HBMachine.Instance.SetMachineStatus(MachineSts.Running);
            this.menuButton_Start.IsSelect(true);
            this.menuButton_Pause.IsSelect(false);
            this.menuButton_Stop.IsSelect(false);
        }
        private void InitilizeProgram()
        {
            ETask.homeDoneTaskNum = 0;
            XStationManager.Instance.FindStationById((int)StationId.Scanner).Stop();
            XStationManager.Instance.FindStationById((int)StationId.Scanner).Reset();
            Globals.WriteMainLog("Reset button is pressed");
            HBMachine.Instance.UploadMachineStateMessage(MachineSts.Idle);
        }
        #endregion
        private void Globals_SetStateTop(object sender, EventArgs e)
        {
            try
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.lbEQMStatus.Text = MultiLanguage.GetMessage(Globals.RUNMODE.ToString());
                    this.lbModelRun.Text = LoadModel.appSettings.currentModel;
                    if (Globals.RUNMODE == MachineRunMode.NormalRun)
                    {
                        this.pnEQMStatus.BackColor = Color.Lime;
                    }
                    else
                    {
                        this.pnEQMStatus.BackColor = MyColor.Red;
                    }
                }));
            }
            catch
            {

            }
        }
        private void OnPauseActive(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(OnPauseActive), new object[] { sender, e });
                return;
            }

            XStationManager.Instance.Pause();
            this.menuButton_Start.IsSelect(false);
            this.menuButton_Pause.IsSelect(true);
            this.menuButton_Stop.IsSelect(false);

            Globals.WriteMainLog("The equipment swiches to the Pause status");
        }
        private void OnStopRunning(object sender, EventArgs e)
        {
            XStationManager.Instance.Stop();

            this.menuButton_Start.IsSelect(false);
            this.menuButton_Pause.IsSelect(false);
            this.menuButton_Stop.IsSelect(true);

            HBMachine.Instance.SetMachineStatus(HB_IWatch.MachineSts.Idle);
            Globals.WriteMainLog("The equipment swiches to the Stop status");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Form1.ShowTestdata showTest = delegate ()
            {
                Form1.Instance.tss_curTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                runTime = DateTime.Now - startTime;
                Form1.Instance.tss_RunTime.Text = "Running time: " + runTime.Hours.ToString("00") + ":" + runTime.Minutes.ToString("00") + ":" + runTime.Seconds.ToString("00");
                tss_permissionInfo.Text = "Current User: " + UserAccountControl.currentAccount.Name;
            };
            statusStrip1.Invoke(showTest);
            Application.DoEvents();

        }


        private void ExternalDevice()
        {


            ////Connect the left,right light
            if(Globals.SettingParameter.Light_Connect)
            {
                Mid_Light_TCP.Instance.Connect();
                if (!Mid_Light_TCP.Instance.Connected)
                    BzMessagebox.Show(MultiLanguage.GetMessage("Left/Right light controller connection failed!", "\r\n",
                                                                "Please check the network connection and restart the program"));

                //Connect the middle light lamp
                LR_Light.Instance.Connect();
                if (!LR_Light.Instance.IsConnected)
                    BzMessagebox.Show(MultiLanguage.GetMessage("Middle light controller connection failed!", "\r\n",
                                                                "Please check the network connection and restart the program"));
            }
            

            //Connect Scanner using TCP method
            if (Globals.SettingParameter.Scanner_Using)
            {
                Scanner_TCP.Instance.Connect(); 
                if (!Scanner_TCP.Instance.Connected)
                    BzMessagebox.Show(MultiLanguage.GetMessage("Scanner connection failed!", "\r\n",
                                                                "Please check the network connection and restart the program"));
            }

            if (Globals.SettingParameter.Connect_AOI)
            {
                if (Globals.ConnectCamera() == false)
                {
                    BzMessagebox.Show(MultiLanguage.GetMessage("Connect camera fail!", "\r\n",
                                                               "Please check the network connection and restart the program"));
                }
            }

            if (Globals.SettingParameter.PLC_Connect)
            {
                if (SLMP.Instance.Open() < 0)
                {
                    BzMessagebox.Show(MultiLanguage.GetMessage("PLC connection failed!", "\r\n",
                                                               "Please check the network connection and restart the program"));
                }
                else
                {
                    SLMP.Instance.WriteBit(DevideCode.M, 4010, true);
                }
            }

            // Run thread to check PLC status
            Thread pingPlc = new Thread(() =>
            {
                while (true)
                {
                    if (!Globals.SettingParameter.Ping_PLC)
                    {
                        continue;
                    }
                    SLMP.Instance.WriteBit(DevideCode.M, 4011, true);
                    Thread.Sleep(4000);
                PingPlc:
                    SLMP.Instance.ReadBit(DevideCode.M, 4011, out bool vlue);
                    if (vlue)
                    {
                        BzMessagebox.Show(MultiLanguage.GetMessage("Disconect Ping PLC", "\r\n",
                                                                   "Please check the network connection and restart the program"));
                        Thread.Sleep(100);
                        goto PingPlc;
                    }
                }
            });
            pingPlc.IsBackground = true;
            pingPlc.Start();
        }
        private void AsyncPlc()
        {
            while (isRunning)
            {
                try
                {
                    var st1 = XStationManager.Instance.FindStationById((int)StationId.Scanner).State;
                    if (st1 == XStationState.PAUSE)
                    {
                        this.menuButton_Start.IsSelect(false);
                        this.menuButton_Pause.IsSelect(true);
                        this.menuButton_Stop.IsSelect(false);
                    }
                    if (st1 == XStationState.RUNNING)
                    {
                        this.menuButton_Start.IsSelect(true);
                        this.menuButton_Pause.IsSelect(false);
                        this.menuButton_Stop.IsSelect(false);
                    }
                    if (st1 == XStationState.WAITRUN)
                    {
                        this.menuButton_Start.IsSelect(false);
                        this.menuButton_Pause.IsSelect(true);
                        this.menuButton_Stop.IsSelect(false);
                    }
                    if (st1 == XStationState.STOP)
                    {
                        this.menuButton_Start.IsSelect(false);
                        this.menuButton_Pause.IsSelect(false);
                        this.menuButton_Stop.IsSelect(true);
                    }

                    //Đồng bộ function theo plc
                    var actionPlc = false;
                    // Reset/Start chương trình : PLC >> PC
                    var PLC_Start = 4000;
                    var PLC_Stop = 4001;
                    var PLC_Pause = 4002;
                    var PLC_Init = 4003;

                    //Alarm form
                    var PLC_ClearErr = 4004;
                    var PLC_Retry = 4005;
                    var PLC_Bypass = 4006;
                    PLC_ClearErr = 801;
                    //Bật chức năng đồng bộ thao tác HMI lên giao diện C#
                    if (Globals.SettingParameter.Async_HMI)
                    {
                        //Start PLC >> PC
                        SLMP.Instance.ReadBit(DevideCode.M, PLC_Start, out actionPlc);
                        if (actionPlc)
                        {
                            SLMP.Instance.WriteBit(DevideCode.M, PLC_Start, false);
                            if (st1 == XStationState.WAITRUN || st1 == XStationState.PAUSE)
                            {
                                StartProgram();
                            }
                        }
                        //Stop PLC >> PC
                        SLMP.Instance.ReadBit(DevideCode.M, PLC_Stop, out actionPlc);
                        if (actionPlc)
                        {
                            SLMP.Instance.WriteBit(DevideCode.M, PLC_Stop, false);
                            StopProgram();
                        }
                        //Pause PLC >> PC
                        SLMP.Instance.ReadBit(DevideCode.M, PLC_Pause, out actionPlc);
                        if (actionPlc)
                        {
                            SLMP.Instance.WriteBit(DevideCode.M, PLC_Pause, false);
                            if (st1 != XStationState.PAUSE)
                                PauseProgram();

                        }
                        SLMP.Instance.ReadBit(DevideCode.M, PLC_Init, out actionPlc);
                        //Reset PLC >> PC
                        if (actionPlc)
                        {                           
                            SLMP.Instance.WriteBit(DevideCode.M, PLC_Init, false);
                            if (st1 == XStationState.WAITRESET || st1 == XStationState.STOP || st1 == XStationState.RESETING)
                                InitilizeProgram();
                        }


                        // Clear Alarm PLC >> PC. Put NG. Ngoai cung phai
                        SLMP.Instance.ReadBit(DevideCode.M, PLC_ClearErr, out actionPlc);
                        if (actionPlc)
                        {
                            SLMP.Instance.WriteBit(DevideCode.M, PLC_ClearErr, false);
                            WriteMainLog("User press Clear Error button");
                            HBMachine.Instance.CancelAlarmForm();
                        }
                        //PLC >> PC Retry: Ngoai cung trai
                        SLMP.Instance.ReadBit(DevideCode.M, PLC_Retry, out actionPlc);
                        if (actionPlc)
                        {
                            SLMP.Instance.WriteBit(DevideCode.M, PLC_Retry, false);
                            HBMachine.Instance.RetryAlarmForm();
                            WriteMainLog("User press RETRY button");
                        }
                        //PLC >> PC Bypass. Nam vi tri giua
                        SLMP.Instance.ReadBit(DevideCode.M, PLC_Bypass, out actionPlc);
                        if (actionPlc)
                        {
                            SLMP.Instance.WriteBit(DevideCode.M, PLC_Bypass, false);
                            HBMachine.Instance.IgnoreAlarmForm();
                            WriteMainLog("User press IGNORE button");
                        }
                    }   
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    throw (new Exception("MainDlg Trigger Exception: " + ex.Message));
                }
            }
        }

        public delegate void ShowTestdata();

       
    }
}
