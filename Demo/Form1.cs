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
using System.Collections;
using System.IO;
using AutoStudio.Forms;
using Demo.Device;
using BoTech;
using Demo.Task;
using NPOI.SS.Formula.Functions;
using System.Reflection;
using System.Security.Principal;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using AutoStudio.Forms.GUI_Forms;
using NPOI.SS.UserModel;
using Models;
using System.Diagnostics.Eventing.Reader;
using AutoStudio.Core.Views.CustomControls;

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
        public Form1()
        {

            //Lấy dữ liệu model sử dụng
            LoadModel.StartUp();

            


            //var mainPage = PageEngineering.Instance;
            //Cài đặt đa ngôn ngữ
            MultiLanguage.ReadMultiLangFile("Language.xlsx");
            MultiLanguage.LoadFile("Language.xlsx");
            MultiLanguage.RegisterForms(this);



            InitializeComponent();
            //Đặt window thành maximized
            //this.WindowState = FormWindowState.Maximized;
            OriginFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            this.MinimumSize = this.Size;


            Globals.BindDevice();

            //Tạo đường dẫn các file
            Globals.CreateAllDirectory();

            // Init Device, Card, Axis, Task, Station
            Globals.InitDevice();

            XController.Instance.Start();   // Run Event Server

            XAlarmReporter.Instance.Start();  // Start Alarm System

            System.Threading.Thread.Sleep(50);

            XMachine.Instance.Start(); // Machine IO System Start

            CsvServer.Instance.Start();       // Excel 引擎啟動             

            if (Globals.SettingICT.CCD_Using)
            {
                KeyenceService.Instance.Connect();  // Connect Keyence Service
                if (!KeyenceService.Instance.Connected)
                    BzMessagebox.Show(MultiLanguage.GetMessage("CCD connection failed! ", "\r\n",
                                                               "Please check the network connection and restart the program"));
            }
            ;
            if (Globals.SettingICT.Scanner_Using)
            {
                if (Globals.SettingICT.ScanLead)
                {
                    Scanner_TCP.Instance.Connect();  // Connect Scanner using TCP_IP
                    if (!Scanner_TCP.Instance.Connected)
                        BzMessagebox.Show(MultiLanguage.GetMessage("Scanner connection failed!", "\r\n",
                                                                    "Please check the network connection and restart the program"));
                }
            }
            //Khởi tạo thông tin các Pages
            InitPages();
            //foreach (var item in ModelStore.GetModelInfoList())
            //{
            //    lstModelNumber.Add(item.Name);
            //}
            //Globals.SettingICT.ListModel = lstModelNumber;
            //PageSetting.Instance.xSettingGrid_ICT.TextName = "ModelParam";

            //PageSetting.Instance.xSettingGrid_ICT.SelectedObject(Globals.SettingICT);
            //PageSetting.Instance.xSettingGrid_SettingOption.SelectedObject(Globals.SettingOption);

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += new FormClosingEventHandler(MainDlg_FormClosing);

            //Khởi tạo các task
            InitTask();
            //Set run mode
            this.lbEQMStatus.Text = MultiLanguage.GetMessage(Globals.RUNMODE.ToString());
            this.pnEQMStatus.BackColor = MyColor.Green;
            //Load dữ liệu từ SQLite
            DataServerManager.Instance.Load();

            PageLogin.Instance.UpdateLabelRequested += (user, level) =>
            {
                lblUser.Text = "User: " + user;
                lblLevel.Text = "Level: " + level;
            };

            PageEngineering.Instance.UpdateTextBox("Complete Initilize control Page");

            this.lbModelRun.Text = LoadModel.appSettings.currentModel;
        }

        private void InitTask()
        {
            XTaskManager.Instance.Initialize();
        }

        private void MainDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExitTask();
        }

        private void ExitTask()
        {
            XTaskManager.Instance.Exit();
        }

        private void Instance_OnAlarmReportSave(XAlarmEventArgs args)
        {

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
                lblUser.Text = "User: None";
                lblLevel.Text = "Level: Operator";
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
                    if (MessageBox.Show("Xác định xem có nên vào chế độ CPK/GRR hay không", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
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
                lblUser.Text = $"User: {UserAccountControl.currentAccount.Name}";
                lblLevel.Text =$"Level: {UserAccountControl.currentAccount.UserPermission}";
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

        bool isReseting;
        private void menuButton_Start_Click(object sender, EventArgs e)
        {
            PageEngineering.Instance.UpdateTextBox("Click Start");

            //Kiểm tra cửa an toàn
            if (!CheckSafeDoor())
            {
                MessageBox.Show("Cửa an toàn đang mở. Vui lòng đóng cửa an toàn và thử lại.");
                return;
            }

            try
            {
                if (!CsvServer.Instance.Server.IsAlive)
                {
                    BzMessagebox.Show("CVS server có vấn đề. \r\n Hãy khởi động lại phần mềm.");
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

            //Nếu đã reset xong
            if (st1 == XStationState.WAITRUN || st1 == XStationState.PAUSE)
            {
                #region Chế độ tự động
                if (!parameterControl())
                {
                    this.menuButton_Start.IsSelect(false);
                    this.menuButton_Pause.IsSelect(false);
                    this.menuButton_Stop.IsSelect(false);
                    return;
                }
                //Nếu chọn chế độ chạy chuyền, kiểm tra hệ thống MES và PDCA
                if (Globals.RUNMODE == MachineRunMode.NormalRun)
                {

                }
                if (BzMessagebox.Show("Are you sure to start AutoRun? \r\nRun mode：" + Globals.RUNMODE.ToString(), "Tips", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    //Đổi trạng thái button
                    HBMachine.Instance.UploadMachineStateMessage(MachineSts.Running);
                    
                    //Ghi log
                    Globals.WriteCoverCTLog("The user presses the start button, the mode is:" + Globals.RUNMODE);
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
                    else if (Globals.RUNMODE == MachineRunMode.Assemble_Dry_Run)
                    {
                        XStationManager.Instance.FindStationById((int)StationId.Scanner).Start(runMode);
                    }
                    else
                    {
                        BzMessagebox.Show(MultiLanguage.GetMessage("The currently selected operation mode is wrong"),
                            MultiLanguage.GetMessage("Mode Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    HBMachine.Instance.SetMachineStatus(HB_IWatch.MachineSts.Running);
                    this.menuButton_Start.IsSelect(true);
                    this.menuButton_Pause.IsSelect(false);
                    this.menuButton_Stop.IsSelect(false);
                }

                else
                {
                    this.menuButton_Start.IsSelect(false);
                    this.menuButton_Pause.IsSelect(false);
                    this.menuButton_Stop.IsSelect(false);
                }
                #endregion

            }
            else
            {
                if (BzMessagebox.Show(MultiLanguage.GetMessage("Confirm to start reset?"), MultiLanguage.GetMessage("Confirm"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    ETask.homeDoneTaskNum = 0;
                    XStationManager.Instance.FindStationById((int)StationId.Scanner).Stop();
                    XStationManager.Instance.FindStationById((int)StationId.Scanner).Reset();
                    Globals.WriteCoverCTLog("User presses the reset button");
                    HBMachine.Instance.UploadMachineStateMessage(MachineSts.Idle);
                }
            }
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
                    //if (!Globals.SettingICT.Safedoor && (!kvp.Value))
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

        private void menuButton_Stop_Click(object sender, EventArgs e)
        {
            if (BzMessagebox.Show(MultiLanguage.GetMessage("Confirm that the machine is stopped?"), "Tips",
                                         MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
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
                Globals.WriteCoverCTLog("User presses the stop button");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
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
        }
        private void menuButton_Pause_Click(object sender, EventArgs e)
        {
            if (!CheckSafeDoor())
            {
                MessageBox.Show("Cửa an toàn đã được mở. Vui lòng đóng cửa an toàn và thử lại.");
                return;
            }
            try
            {
                if (!CsvServer.Instance.Server.IsAlive)
                {
                    BzMessagebox.Show("CsvServer Error");
                    return;
                }
            }
            catch (Exception ex)
            {
                BzMessagebox.Show("Exception CsvServer:\n" + ex.Message);
                return;
            }

            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(menuButton_Pause_Click), new object[] { sender, e });
                return;
            }

            var st1 = XStationManager.Instance.FindStationById((int)StationId.Scanner).State;
            if (st1 == XStationState.PAUSE)
            {
                HBMachine.Instance.UploadMachineStateMessage(MachineSts.Running);
                XStationManager.Instance.Continue();
                this.menuButton_Start.IsSelect(true);
                Globals.WriteCoverCTLog("The user presses the pause button and the device continues to run automatically");
            }
            else
            {
                HBMachine.Instance.UploadMachineStateMessage(MachineSts.Idle);
                XStationManager.Instance.Pause();
                this.menuButton_Start.IsSelect(false);
                Globals.WriteCoverCTLog("The user presses the pause button and the device pauses");
            }

            this.menuButton_Stop.IsSelect(false);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Globals.SetStateTop += Globals_SetStateTop;
            XTask.OnPauseActive += this.OnPauseActive;
            XTask.OnStopActive += this.OnStopRunning;

            Globals.OnPauseActive += this.OnPauseActive;
            Globals.OnStopActive += this.OnStopRunning;
            timer1.Start();
        }
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

            Globals.WriteCoverCTLog("The device switches to the Idle state");
        }
        private void OnStopRunning(object sender, EventArgs e)
        {
            XStationManager.Instance.Stop();

            this.menuButton_Start.IsSelect(false);
            this.menuButton_Pause.IsSelect(false);
            this.menuButton_Stop.IsSelect(true);

            HBMachine.Instance.SetMachineStatus(HB_IWatch.MachineSts.Idle);
            Globals.WriteCoverCTLog("The device switches to the stopped state");
        }
        private void timer1_Tick(object sender, EventArgs e)
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
                    this.menuButton_Pause.IsSelect(false);
                    this.menuButton_Stop.IsSelect(false);
                }
                var st2 = HBMachine.Instance.CurSts;
            }
            catch (Exception ex)
            {
                throw (new Exception("MainDlg Trigger Exception:" + ex.Message));
            }
        }
        private void menuButton_OpenReport_Click(object sender, EventArgs e)
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
        private void menuButtonCamerImage_Click(object sender, EventArgs e)
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
        
    }

}
