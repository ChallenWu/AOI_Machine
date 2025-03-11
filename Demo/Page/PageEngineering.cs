 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Demo.Globals;
using XCore;
using Demo.Device;
using AutoStudio.Core.Tools;
using HB_IWatch;
using Demo.UserControls;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using NPOI.SS.UserModel;


namespace Demo.Page
{
    public partial class PageEngineering : UserControlBase
    {
        private static PageEngineering instance;
        public static PageEngineering Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new PageEngineering();
                return instance;
            }
        }
        
        public PageEngineering()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            switchButton1.SetText("Manual Debug");
            this.switchButton1.ON += SwitchButton1_ON;
            this.switchButton1.OFF += SwitchButton1_OFF;
            Init();

            timer1.Interval = 100;
            timer1.Start();

            //Tạo luồng control alarm
            int enumCount = Enum.GetValues(typeof(XCore.XAlarmId)).Length;
            for (int i = 0; i < 1000; i++)
            {
                m_asyncHandled.Add(i, true);
            }

            //Realtime check error
            Thread thread = new Thread(checkPlcErrors);
            thread.IsBackground = true;
            thread.Start();

            Thread RunTimeThread = new Thread(PC_RunTime);
            RunTimeThread.IsBackground = true;
            RunTimeThread.Start();
        }
        DateTime beginTime;
        private void PC_RunTime()
        {
            beginTime = DateTime.Now;
            TimeSpan pcruntime;
            TimeSpan midTime;
            while(true)
            {
                midTime = DateTime.Now - beginTime;
                SetLabelTextCallback(this.lbSoftwareRun, midTime.Days.ToString("00") + ":" + midTime.Hours.ToString("00") + ":" + midTime.Minutes.ToString("00") + ":" + midTime.Seconds.ToString("00"), Color.Black);
                pcruntime = SysInfo.GetSystemUpTime();
                SetLabelTextCallback(this.lbPcRun, pcruntime.Days.ToString("00") + ":" + pcruntime.Hours.ToString("00") + ":" + pcruntime.Minutes.ToString("00") + ":" + pcruntime.Seconds.ToString("00"), Color.Black);
                Thread.Sleep(1000);
            }    
        }

        delegate void dSetLabelTextCallback(Label label, string msg, Color color);
        public void SetLabelTextCallback(Label label, string msg, Color color)
        {
            try
            {
                if (label.InvokeRequired)
                {
                    dSetLabelTextCallback d = new dSetLabelTextCallback(SetLabelTextCallback);
                    label.Invoke(d, new object[] { label, msg, color });
                }
                else
                {
                    label.Text = msg;
                    if (label.ForeColor != color)
                    {
                        label.ForeColor = color;
                    }
                    //tb.ScrollToCaret();// 捲動到最後一個插入位置
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Init()
        {
            //Add runMode
            this.comboBox_Mode.Items.Add(MultiLanguage.GetMessage(MachineRunMode.NormalRun.ToString()));
            this.comboBox_Mode.Items.Add(MultiLanguage.GetMessage(MachineRunMode.DryRun.ToString()));
            //this.comboBox_Mode.Items.Add(MultiLanguage.GetMessage(MachineRunMode.Conveyor_Dry_Run.ToString()));
            //this.comboBox_Mode.Items.Add(MultiLanguage.GetMessage(MachineRunMode.Entire_Machine_Dry_Run.ToString()));
            //this.comboBox_Mode.Items.Add(MultiLanguage.GetMessage(MachineRunMode.Single_Reinspection.ToString()));
            this.comboBox_Mode.SelectedIndex = 0;
            this.comboBox_Mode.SelectedIndexChanged += ComboBox_Mode_SelectedIndexChanged;

        }

        public void AddLogTextBox(string content, Color color)
        {
            //UpdateText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ff ")} >>> {content}");

        }

        private void UpdateText(string text)
        {
            //if (txtLogEngineer.InvokeRequired)
            //{
            //    txtLogEngineer.Invoke(new Action(() =>
            //    {
            //        PrependText(text, Color.Red);
            //    }));
            //}
            //else
            //{
            //    PrependText(text, Color.Red);
            //}
        }

        private void PrependText(string text, Color color)
        {
            //txtLogEngineer.Text = text + "\r\n" + txtLogEngineer.Text;
            //txtLogEngineer.SelectionStart = 0; // Đưa con trỏ về đầu
            //txtLogEngineer.ScrollToCaret();    // Cuộn lên đầu (nếu cần)
            //txtLogEngineer.ForeColor = color;
        }

        private void ComboBox_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Chon runMode
            var st1 = XStationManager.Instance.FindStationById((int)StationId.Scanner).State;

            if (st1 == XStationState.RUNNING || st1 == XStationState.PAUSE)
            {
                switch (Globals.RUNMODE)
                {
                    case MachineRunMode.NormalRun:
                        this.comboBox_Mode.SelectedIndex = 0;
                        break;
                    case MachineRunMode.DryRun:
                        this.comboBox_Mode.SelectedIndex = 1;
                        break;
                    case MachineRunMode.Conveyor_Dry_Run:
                        this.comboBox_Mode.SelectedIndex = 2;
                        break;
                    case MachineRunMode.Entire_Machine_Dry_Run:
                        this.comboBox_Mode.SelectedIndex = 3;
                        break;
                    case MachineRunMode.Single_Reinspection:
                        this.comboBox_Mode.SelectedIndex = 4;
                        break;
                    default:
                        this.comboBox_Mode.SelectedIndex = 0;
                        break;
                }
                BzMessagebox.Show(("There is a task currently running.\n Please stop the device before switching modes."),
                    "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (this.comboBox_Mode.SelectedIndex)
            {
                case 0:
                    Globals.RUNMODE = MachineRunMode.NormalRun;
                    this.chkMES.Visible = false;
                    this.chkSN.Visible = false;
                    this.chkPN.Visible = false;
                    this.chkImage1.Visible = false;
                    chkImage2.Visible = false;
                    chkImageAOI.Visible = false;
                    chkStation.Visible = false;
                    isDryrun = false;
                    break;
                case 1:
                    Globals.RUNMODE = MachineRunMode.DryRun;
                    this.chkMES.Visible = true;
                    this.chkSN.Visible = true;
                    this.chkPN.Visible = true;
                    chkImage1.Visible = true;
                    chkImage2.Visible = true;
                    chkImageAOI.Visible = true;
                    chkStation.Visible = true;
                    isDryrun = true;
                    
                    break;
                case 2:
                    Globals.RUNMODE = MachineRunMode.Conveyor_Dry_Run;
                    break;
                case 3:
                    Globals.RUNMODE = MachineRunMode.Entire_Machine_Dry_Run;
                    break;
                case 4:
                    Globals.RUNMODE = MachineRunMode.Single_Reinspection;
                    break;
                default:
                    Globals.RUNMODE = MachineRunMode.NormalRun;
                    break;
            }
            //Update UI 
            Globals.SetTopState();
        }

        private void SwitchButton1_OFF()
        {
            DebugDlg.Instance.Close();
        }

        private void SwitchButton1_ON()
        {
            //Chon runMode
            var st1 = XStationManager.Instance.FindStationById((int)StationId.Scanner).State;

            if (st1 == XStationState.RUNNING)
            {
                BzMessagebox.Show(("Task is running.\n Stop task before changes mode"),
                    "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            HBMachine.Instance.UploadMachineStateMessage(MachineSts.Engineering);
            DebugDlg.Instance.Show();
        }
        private int iCunt = 0;
        private int EMGStr = 0;
        
        private Dictionary<int, bool> m_asyncHandled = new Dictionary<int, bool>();
        private void timer1_Tick(object sender, EventArgs e)
        {
            //bool safeDoorSts = true;
            ////Kiểm tra cửa an toàn
            //foreach (KeyValuePair<XDi, bool> kvp in XMachine.Instance.signalDoor)
            //{
            //    if (XMachine.Instance.DictDoorDiCheck.Keys.Contains(kvp.Key) && !XMachine.Instance.DictDoorDiCheck[kvp.Key])
            //    {
            //        continue;
            //    }
            //    DISTSTYPE diKeySts = DISTSTYPE.LOW;
            //    kvp.Key.GetDi(ref diKeySts);

            //    if (diKeySts == DISTSTYPE.LOW)
            //    {
            //        //if (((!Globals.SettingOption.IsOpensafeDoor()) && (!kvp.Value)) || Globals.Offline)
            //        //    continue;
            //        //else
            //        //{
            //        //    safeDoorSts = false;
            //        //    break;
            //        //}
            //    }
            //}

            //this.btnSafeDoor.BackColor = safeDoorSts ? MyColor.Green : MyColor.Red;

            //if (!safeDoorSts && iCunt == 0)
            //{                
            //    //偶尔出现安全门打开task不能暂停的情况，改为异步的
            //    if (m_asyncHandled[0])
            //    {
            //        m_asyncHandled[0] = false;
            //        string erro = "Cửa an toàn đang mở";
            //        HBMachine.Instance.ShowErroAsync(XAlarmId.DOOR_OPEN.ToString(), MultiLanguage.GetMessage(erro),"Error","Cửa an toàn đang bị mở ra",
            //                                        "Safety Door is Opened", MultiLanguage.GetMessage("Confirm"), "", "", 
            //                                        new XCore.CallbackAction(() => { m_asyncHandled[0] = true; return true; }), true);
            //    }
            //}

            //if (XDevice.Instance.FindDiById((int)DiId.主设备急停).STS == DISTSTYPE.LOW &&
            //                            XDevice.Instance.FindDiById((int)DiId.左供料机急停).STS == DISTSTYPE.LOW &&
            //                            XDevice.Instance.FindDiById((int)DiId.右供料机急停).STS == DISTSTYPE.LOW)
            //{
            //    //this.btnEMG.BackColor = MyColor.Green;
            //    if (EMGStr != 1)
            //    {
            //        HBMachine.Instance.SetMachineStatus(MachineSts.Idle);
            //        EMGStr = 1;
            //    }
            //}
            //else
            //{
            //    //this.btnEMG.BackColor = MyColor.Red;
            //    HBMachine.Instance.SetMachineStatus(MachineSts.Downtime);
            //    EMGStr = 2;
            //}
          
        }

        int index = 0;
        private void checkPlcErrors()
        {
            while (true)
            {
                Thread.Sleep(1000);

                XAlarmId alarmId = new XAlarmId();
                //List<bool> resultErr = new List<bool>();
                SLMP.Instance.ReadMultiBit(DevideCode.M, 5300, 4, out List<bool> resultErr);
                Thread.Sleep(50);
                //SLMP.Instance.ReadBit(DevideCode.M, 5300, out bool M5300);
                //SLMP.Instance.ReadBit(DevideCode.M, 5301, out bool M5301);
                //SLMP.Instance.ReadBit(DevideCode.M, 5302, out bool M5302);
                //SLMP.Instance.ReadBit(DevideCode.M, 5303, out bool M5303);
                if(resultErr.Count > 0 )
                {
                    if (resultErr[0])
                    {
                        SLMP.Instance.ReadMultiBit(DevideCode.M, 5010 , 10, out List<bool> CylinderErr);
                        for (int i = 0; i < CylinderErr.Count; i++)
                        {
                            if (CylinderErr[i])
                            {
                                alarmId = (XAlarmId)5010 + i;
                                index = 10 + i;
                            }
                        }
                    }

                    if (resultErr[1])
                    {
                        SLMP.Instance.ReadMultiBit(DevideCode.M, 5020, 4, out List<bool> MotorErr);
                        for (int i = 0; i < MotorErr.Count; i++)
                        {
                            if (MotorErr[i])
                            {
                                alarmId = (XAlarmId)5020 + i;
                                index = 20 + i;
                            }
                        }
                    }

                    if (resultErr[2])
                    {
                        SLMP.Instance.ReadMultiBit(DevideCode.M, 5030, 6, out List<bool> SafetyErr);
                        for (int i = 0; i < SafetyErr.Count; i++)
                        {
                            if (SafetyErr[i])
                            {
                                alarmId = (XAlarmId)5030 + i;
                                index = 30 + i;
                            }
                        }

                        SLMP.Instance.ReadMultiBit(DevideCode.M, 5040, 7, out List<bool> init);
                        for (int i = 0; i < init.Count; i++)
                        {
                            if (init[i])
                            {
                                alarmId = (XAlarmId)5040 + i;
                                index = 40 + i;
                            }
                        }

                        SLMP.Instance.ReadMultiBit(DevideCode.M, 5050, 6, out List<bool> runningError);
                        for (int i = 0; i < runningError.Count; i++)
                        {
                            if (runningError[i])
                            {
                                alarmId = (XAlarmId)5050 + i;
                                index = 50 + i;
                            }
                        }
                    }


                    if (alarmId != XAlarmId.NONE)
                    {
                        string szDescrip;
                        string szErrCode;
                        string szType;
                        string szOk, szCancel, szIgnore;
                        //string szDetail = "";
                        szDescrip = XAlarmReporter.Instance.SystemAlarms[alarmId].Description;
                        szType = XAlarmReporter.Instance.SystemAlarms[alarmId].Category.ToString();
                        szErrCode = XAlarmReporter.Instance.SystemAlarms[alarmId].Code.ToString();

                        //Hướng dẫn xử lý lỗi xảy ra
                        string szSolution = XAlarmReporter.Instance.SystemAlarms[alarmId].Solution.ToString();
                        string szAlarmLevel = XAlarmReporter.Instance.SystemAlarms[alarmId].AlarmLevel.ToString();
                        //szDescrip = szDescrip /*+ "\r\n" + "Solution: " + szSolution*/;

                        szOk = XAlarmReporter.Instance.SystemAlarms[alarmId].OkOptionText;
                        szCancel = XAlarmReporter.Instance.SystemAlarms[alarmId].CancelOptionText;
                        szIgnore = XAlarmReporter.Instance.SystemAlarms[alarmId].IgnoreOptionText;

                        //Chặn tạo ra cửa sổ lỗi giống nhau
                        if (m_asyncHandled[index])
                        {
                            m_asyncHandled[index] = false;
                            HBMachine.Instance.ShowErroAsync(szErrCode, szDescrip, szType, szAlarmLevel, szSolution, szOk, szCancel, szIgnore,
                                                            new CallbackAction(() =>
                                                            {
                                                                m_asyncHandled[index] = true;
                                                                return true;
                                                            }), true);
                        }
                    }
                }                         
            }
        }
        
        public void Async_UnitDaily(IO_Summary ios)
        {
            try
            {
                if (!this.IsHandleCreated)
                {
                    this.CreateControl();
                    //return;
                }
                if (IsHandleCreated)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this.Label_NG_OK.Text = ios.UnitStatus == true ? "OK" : "NG";
                        this.lbPN.Text = ios.SN;
                        this.Product_Num.Text = ios.Yield;
                        this.Cycle.Text = ios.CT;
                        this.pnStatus.BackColor = ios.UnitStatus == true ? Color.Lime : Color.Red;
                        lbPN.BackColor = this.pnStatus.BackColor;
                        Label_NG_OK.BackColor = this.pnStatus.BackColor;
                        lbTime.BackColor = this.pnStatus.BackColor;
                        lbTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
                    }));
                }
            }
            catch
            {

            }
        }

        delegate void dSetTextBoxCallback(RichTextBox tbox, string msg, Color color);
        public void SetTextBoxCallback(RichTextBox tbox, string msg, Color color)
        {
            if (tbox.InvokeRequired)
            {
                dSetTextBoxCallback d = new dSetTextBoxCallback(SetTextBoxCallback);
                tbox.Invoke(d, new object[] { tbox, msg, color });
            }
            else
            {
                tbox.Text += msg + "\r\n";
            }
        }

        public void AddLogRichtextBox(string text, Color color)
        {
            //SetTextBoxCallback(richTextBox1, text, color);
            //return;
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(() =>
                {
                    richTextBox1.SelectionStart = 0;
                    richTextBox1.SelectionLength = 0;
                    string newText = DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss:fff >>> ") + text + Environment.NewLine;

                    richTextBox1.SelectedText = newText;
                    //Đặt vùng chọn cho chuỗi vừa thêm
                    richTextBox1.SelectionStart = 0;
                    richTextBox1.SelectionLength = newText.Length - 1;
                    richTextBox1.SelectionColor = color;

                    //Đặt lại trạng thái vùng chọn
                    richTextBox1.SelectionStart = 0;
                    richTextBox1.ScrollToCaret();
                }));
            }
            else
            {
                richTextBox1.SelectionStart = 0;
                richTextBox1.SelectionLength = 0;
                string newText = DateTime.Now.ToString("yyyy:MM:dd HH:mm:ss:fff >>> ") + text + Environment.NewLine;

                richTextBox1.SelectedText = newText;
                //Đặt vùng chọn cho chuỗi vừa thêm
                richTextBox1.SelectionStart = 0;
                richTextBox1.SelectionLength = newText.Length - 1;
                richTextBox1.SelectionColor = color;

                //Đặt lại trạng thái vùng chọn
                richTextBox1.SelectionStart = 0;
                richTextBox1.ScrollToCaret();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMES.Checked)
            {
                isMES = true;
            }
            else
                isMES = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSN.Checked)
            {
                isSn = true;
            }
            else
                isSn = false;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPN.Checked)
            {
                isPn = true;
            }
            else
                isPn = false;
        }

        private void chkStation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkStation.Checked)
            {
                isStation = true;
            }
            else
                isStation = false;
        }

        private void chkImageProcess_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImageAOI.Checked)
            {
                AOI_Result = true;
            }
            else
                AOI_Result = false;
        }

        private void chkImage1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImage1.Checked)
            {
                isImage1 = true;
            }
            else
                isImage1 = false;
        }

        private void chkImage2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImage2.Checked)
            {
                isImage2 = true;
            }
            else
                isImage2 = false;
        }
    }
}
