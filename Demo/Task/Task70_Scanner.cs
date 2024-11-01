using BoTech;
using Demo.Device;
using Demo.Page;
using HB_IWatch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCore;

namespace Demo.Task
{
    class Task70_Scanner : ETask
    {
        protected DialogResult result;
        public static string SN = "";
        public int intResult = 0;
        public int i;
        public static string CCDResult = "";       

        //SerialNumber
        Dictionary<string, string> lstSN = new Dictionary<string, string>();

        byte[] scanlead_cmd = { 0x02, 0xF4, 0x03 };
        public override void Initialize()
        {
            InitialTask();
        }
        public Task70_Scanner(string path)
            : base(path)
        {
        }

        public void InitialTask()
        {
            base.Initialize();
        }
        RunState m_runStep = new RunState();
        private enum RunState
        {
            WaitCarrierInSignal,
            Inspection,
            TriggerScanner_P1,
            TriggerScanner_P2,
            TriggerScanner_P3,
            TriggerScanner_P4,           
            Home
        }
        public override void Exit()
        {
            base.Exit();    
        }
        public bool TestAlarm()
        {
            //Show bảng báo lỗi
            result = ShowAlarm(XAlarmId.DOOR_OPEN);
            if (result == DialogResult.Cancel)
            {
                if (OnPauseActive != null)
                    OnPauseActive(null, null);
                return false;
            }
            return false;
        }
        protected override void Homing()
        {
            try
            {
                WriteCTLog("PC Reset Task 70");
                //Connect ICW_Scanner
                //ICW_Scanner.Instance.Connect();

                //Connect Mitsu Plc

                if (Globals.SettingICT.PLC_Connect)
                {
                    if(SLMP.Instance.Open() < 0)
                    {
                        var content = "Không thể kết nối tới Fx5U. Reset thất bại";
                        ShowAlarm(XAlarmId.CONNECT_PLC_FAILURE);
                        WriteCTLog(content);
                        return;
                    }
                }

                m_runStep = RunState.WaitCarrierInSignal;
                SetStep("Reset Complete", MyColor.Green);
                SetStation_StateWaitRun();
                WriteCTLog("Hoàn thành reset");
                PageEngineering.Instance.UpdateTextBox("Hoàn thành reset Task70");
                homeDoneTaskNum++;

                if (homeDoneTaskNum == RequestHomeTaskNum)
                    BzMessagebox.Show(MultiLanguage.GetMessage("Hoàn thành reset"));
                 
            }
            catch (Exception e)
            {
                
            }


        }
        protected override void Running(object runMode)
        {
            switch ((StationRunMode)runMode)
            {
                case StationRunMode.AutoRun:
                    SetStep("WaitCarrierInSignal", Color.Green);
                    AutoRun();
                    break;
                default:
                    break;
            }
            SetStep("Stop Running", MyColor.Green);
        }

        private void AutoRun()
        {
            mIsExecuatingAuto = true;
            while (true)
            {
                Thread.Sleep(10); 

                if(StopRun)
                {
                    SetStep("Stop Task70", Color.Green);
                    break;
                }                    
                if(PauseRun)
                {
                    SetStep("Pause Task70", Color.Green);
                    Thread.Sleep(10);
                    continue;
                }
                try
                {
                    PerformAutoRun();
                }
                catch (Exception ex)
                {
                    mIsExecuatingAuto = false;
                    return;
                }
            }
            mIsExecuatingAuto = false;
        }
        private void PerformAutoRun()
        {
            //Step Auto
            switch ((RunState)m_runStep)
            {
                case RunState.WaitCarrierInSignal:
                    SetStep("WaitCarrierInSignal", Color.Green);
                    short DValue;
                    SLMP.Instance.ReadWord(DevideCode.D, 110, out DValue);
                    if(DValue == 1)
                    //if(SLMP.Instance.D110 == 1)
                    {
                        WriteCTLog("PLC >> PC: Register D110 = 1");
                        SLMP.Instance.WriteWord(DevideCode.D, 110, 0);
                        //Record thời điểm bắt đầu
                        AudioSystem.UCM.Units[0].StartTime = DateTime.Now;
                        DateTime startdt = DateTime.Now;
                        Console.WriteLine(startdt.ToString());
                        SN = "";
                        SetStep("Inspection", Color.Green);
                        m_runStep = RunState.Inspection;
                    }
                    if(SLMP.Instance.D110 == 2)
                    {
                        WriteCTLog("PLC >> PC: Register D110 = 2");
                        SLMP.Instance.WriteWord(DevideCode.D, 100, 0);
                        m_runStep = RunState.TriggerScanner_P1;
                    }
                    break;
                    //Inspection product => Send to CCD
                case RunState.Inspection:
                    SetStep("Inspection", Color.Green);
                    WriteCTLog("PC >> CCD: Inspection product command CC,46 ");
                    string cmd = "CC,46";
                    if(TriggerKenyceSN(cmd, out CCDResult) >= 0)
                    {
                        string[] arr = CCDResult.Split(',');
                        //inspect result OK
                        if (arr[3] == "OK")
                        {
                            WriteCTLog("Inspection OK");
                            m_runStep = RunState.TriggerScanner_P1;
                            SLMP.Instance.WriteWord(DevideCode.D, 110, 1);
                            WriteCTLog("PC >> PLC: D110 = 1");
                        }
                        else
                        {
                            WriteCTLog("Inspection NG");
                            //Error to plc
                            SLMP.Instance.WriteWord(DevideCode.D, 110, 2);
                            WriteCTLog("PC >> PLC: D110 = 2");
                            SetStep("WaitCarrierInSignal", Color.Green);
                            m_runStep = RunState.WaitCarrierInSignal;
                        }
                    }
                    else
                    {
                        m_runStep = RunState.WaitCarrierInSignal;
                    }

                    break ;
                    //Inspect ok, then scan barcode
                case RunState.TriggerScanner_P1:
                    SetStep("Reading Scanner_P1", Color.Green);
                    if (SLMP.Instance.D100 == 1)
                    {
                        
                        SLMP.Instance.WriteWord(DevideCode.D, 100, 1);
                        string scanresult = "";
                        if (TriggerScanner(scanlead_cmd, out scanresult) >= 0)
                        {
                            //Scan ma OK
                            SLMP.Instance.WriteWord(DevideCode.D, 110, 1);
                            //Insert task MES here

                            //Scan OK, send result to Plc to go next step
                            Thread.Sleep(100);
                            m_runStep = RunState.TriggerScanner_P2;
                        }
                        else
                        {
                            SLMP.Instance.WriteWord(DevideCode.D, 110, 2);
                            m_runStep = RunState.TriggerScanner_P1;
                        }
                    }
                    else if (SLMP.Instance.D100== 2)
                    {
                        SLMP.Instance.WriteWord(DevideCode.D, 100, 0);
                        m_runStep = RunState.TriggerScanner_P2;
                    }

                    break;
                case RunState.TriggerScanner_P2:
                    SetStep("Reading Scanner_P2", Color.Green);
                    if (SLMP.Instance.D100 == 1)
                    {
                        SLMP.Instance.WriteWord(DevideCode.D, 100, 0);
                        string scanresult = "";
                        if (TriggerScanner(scanlead_cmd, out scanresult) >= 0)
                        {

                            //Scan ma OK
                            SLMP.Instance.WriteWord(DevideCode.D, 110, 1);
                            //Insert task MES here
                            //Scan OK, send result to Plc to go next step
                            Thread.Sleep(100);
                            m_runStep = RunState.TriggerScanner_P3;
                        }
                        else
                        {
                            SLMP.Instance.WriteWord(DevideCode.D, 110, 1);
                            m_runStep = RunState.TriggerScanner_P1;
                        }
                    }
                    else if (SLMP.Instance.D100 == 2)
                    {
                        SLMP.Instance.WriteWord(DevideCode.D, 100, 0);
                        m_runStep = RunState.TriggerScanner_P3;
                    }
                    Thread.Sleep(100);
                    break;
                case RunState.TriggerScanner_P3:
                    SetStep("Reading Scanner_P3", Color.Green);
                    if (SLMP.Instance.D100 == 1)
                    {
                        SLMP.Instance.WriteWord(DevideCode.D, 100, 0);
                        string scanresult = "";
                        if (TriggerScanner(scanlead_cmd, out scanresult) >= 0)
                        {
                            //Scan ma OK
                            SLMP.Instance.WriteWord(DevideCode.D, 110, 1);
                            Thread.Sleep(100);
                            m_runStep = RunState.TriggerScanner_P4;
                        }
                        else
                        {
                            //send NG to PLC
                            PageEngineering.Instance.UpdateTextBox("PC -> PLC: Send D110 = 2. Scan label NG");
                            SLMP.Instance.WriteWord(DevideCode.D, 110, 2);
                            m_runStep = RunState.TriggerScanner_P3;
                        }
                    }
                    else if (SLMP.Instance.D100 == 2)
                    {
                        SLMP.Instance.WriteWord(DevideCode.D, 100, 0);
                        m_runStep = RunState.TriggerScanner_P4;
                    }
                    Thread.Sleep(100);
                    break;
                case RunState.TriggerScanner_P4:
                    SetStep("Trigger Scanner Position 4", Color.Green);
                    if (SLMP.Instance.D100 == 1)
                    {
                        WriteCTLog("PLC >> PC: D100 = 1");
                        SLMP.Instance.WriteWord(DevideCode.D, 100, 0);
                        string scanresult = "";
                        if (TriggerScanner(scanlead_cmd, out scanresult) >= 0)
                        {
                            SLMP.Instance.WriteWord(DevideCode.D, 110, 1);
                            Thread.Sleep(100);
                            m_runStep = RunState.WaitCarrierInSignal;
                        }
                        else
                        {
                            SLMP.Instance.WriteWord(DevideCode.D, 110, 2);
                            m_runStep = RunState.TriggerScanner_P4;
                        }
                    }
                    else if (SLMP.Instance.D100 == 2)
                    {
                        SLMP.Instance.WriteWord(DevideCode.D, 100, 0);
                        m_runStep = RunState.WaitCarrierInSignal;
                    }
                        Thread.Sleep(100);
                    break;
                default:
                    break;
            }

        }
        #region ICW_Serial Port
        private bool ReadSN()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            string ret = null;
            ret = ICW_Scanner.Instance.ReadQR();
            if (ret == "")
            {
                return false;
            }
            else
            {
                SN = ret;
                return true;
            }
        }
        #endregion

        #region SQL 
        private void WriteDataToSQLite(string result)
        {
            AudioSystem.UCM.Units[0].HiveState = 1;
            AudioSystem.UCM.Units[0].UnitSN = SN;
            AudioSystem.UCM.Units[0].ComponentSN = "ABC";
            AudioSystem.UCM.Units[0].Pass = result;
            AudioSystem.UCM.Units[0].EndTime = DateTime.Now;
            AudioSystem.UCM.Units[0].CT = (AudioSystem.UCM.Units[0].EndTime - AudioSystem.UCM.Units[0].StartTime).TotalSeconds;

            DateTime enddt = DateTime.Now;
            if (AudioSystem.UCM.Units[0].StartTime.Hour >= 8 && AudioSystem.UCM.Units[0].StartTime.Hour <= 20)
                AudioSystem.UCM.Units[0].Shift = "DS";
            else
                AudioSystem.UCM.Units[0].Shift = "NS";

            DataServerManager.Instance.InsertUnitMessage(AudioSystem.UCM, 0);
            PageProduction.Instance.Async_IO_Refresh(AudioSystem.UCM, 0);
        }
        #endregion

        #region CCD
        protected int TriggerKenyceSN(string cmd, out string Data)
        {
            UpCCDScanSN:
            int iRetCCD = 0;
            Data = "";
            Thread.Sleep(100);
            KeyenceService.Instance.WriteCmd(cmd, 0);

            SetStep("Waiting to receive camera feedback data...", MyColor.LightGreen);
            if (KeyenceService.Instance.are_DataRecerveDone.WaitOne(10000) == false) // wait CCD reponse within 10sec
            {
                SetStep("Camera feedback data timeout!", MyColor.LightRed);
                WriteLog("Camera feedback data timeout!");
                MultiLanguage.GetMessage("Camera feedback data timeout");
                return -1;
            }
            SetStep("Parsing camera data…", MyColor.LightGreen);
            if (!WaitKeyenceData())
            {
                MultiLanguage.GetMessage("The camera feedback data format of this command is incorrect");
                if (iRetCCD == 1)
                    goto UpCCDScanSN;
                else
                    return -1;
            }
            Data = KeyenceService.Instance.RecData.ToString();
            return 0;
        }

        public bool WaitKeyenceData(int timeOutMs = 10000)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                if (KeyenceService.Instance.RecData.Length > 15 && KeyenceService.Instance.RecData.Contains("XY"))
                {
                    int index = KeyenceService.Instance.RecData.IndexOf("XY");
                    KeyenceService.Instance.RecData = KeyenceService.Instance.RecData.Substring(index);
                    return true;
                }
                 if (KeyenceService.Instance.RecData.Length == Globals.SettingICT.SN_Lenght)
                    return true;
               
                if (sw.ElapsedMilliseconds > timeOutMs)
                    return false;
                Thread.Sleep(3);
            } 
            while (true);    
        }
        #endregion
        #region Scanlead Scanner
        protected int TriggerScanner(byte[] cmd, out string Data)
        {
            UpCCDScanSN:
            int iRetCCD = 0;
            Data = "";
            Thread.Sleep(Globals.SettingOption.上相机稳停时间);

            Scanner_TCP.Instance.WriteCmd(cmd, 0);

            SetStep("Waiting to receive camera feedback data...", MyColor.LightGreen);
            if (Scanner_TCP.Instance.are_DataRecerveDone.WaitOne(10000) == false) // wait CCD reponse within 10sec
            {
                SetStep("Camera feedback data timeout!", MyColor.LightRed);
                WriteLog("Camera feedback data timeout!");
                MultiLanguage.GetMessage("Camera feedback data timeout");
                return -1;
            }
            SetStep("Parsing camera data…", MyColor.LightGreen);
            if (!WaitScannerData())
            {
                MultiLanguage.GetMessage("The camera feedback data format of this command is incorrect");
                if (iRetCCD == 1)
                    goto UpCCDScanSN;
                else
                    return -1;
            }
            Data = Scanner_TCP.Instance.RecData.ToString();           
            return 0;
        }
        public bool WaitScannerData(int timeOutMs = 3000)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                //So sanh chuoi du lieu
                if (Scanner_TCP.Instance.RecData.Length > 5 && Scanner_TCP.Instance.RecData.Contains("SPX"))
                {
                    int index = Scanner_TCP.Instance.RecData.IndexOf("SPXVN");
                    Scanner_TCP.Instance.RecData = Scanner_TCP.Instance.RecData.Substring(index);
                    return true;
                }
                if (Scanner_TCP.Instance.RecData.Length == Globals.SettingOption.载具SN长度)
                    return true;

                if (sw.ElapsedMilliseconds > timeOutMs)
                    return false;
                Thread.Sleep(3);
            }
            while (true);
        }

        private bool AddSNtoList(string str)
        {
            string[] kq = str.Split(',');
            foreach (string k in kq)
            {
                if (k.Contains("A"))
                {
                    lstSN.Add("SN1", k);
                }
                if (k.Contains("B"))
                {
                    lstSN.Add("SN2", k);
                }
                if (k.Contains("C"))
                {
                    lstSN.Add("SN3", k);
                }
                if (k.Contains("D"))
                {
                    lstSN.Add("SN4", k);
                }
            }
            return true;
        }
        #endregion
        #region MES

        #endregion

        private void WriteCTLog(string message)
        {
            string path = Globals.Dir_Task70 + "MainLog_" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            string str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " => " + message;
            CsvServer.Instance.WriteLine(path, str);
        }
    }

}
