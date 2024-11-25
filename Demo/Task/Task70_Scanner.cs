using BoTech;
using Demo.Device;
using Demo.Page;
using Demo.Setting;
using HB_IWatch;
using System;
using System.Collections.Generic;
using System.Data;
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
        public static string serialNumber = "";
        public int intResult = 0;
        public int i;
        public static string CCDResult = "";
        public static int PLC_CarrierInSignal = 100;
        ProductInfor pd = new ProductInfor();
        BackMessage messageResponse;
        private Dictionary<string,string> lstSerialNumber = new Dictionary<string,string>();
        string data;
        protected XAlarmId m_AsmError = XAlarmId.NONE;

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
            Home,
            WaitCarrierInSignal,
            CheckSNDummy,
            AssyCheck,
            Capture_Image_1,
            Capture_Image_2,
            Read_SN_Product,
            POST_Mes,
            Send_NG_To_PLC
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
                PageEngineering.Instance.UpdateTextBox("User reset task 70");
                if (Globals.SettingICT.PLC_Connect)
                {
                    if (SLMP.Instance.Open() < 0)
                    {
                        var content = "Không thể kết nối tới Fx5U. Reset thất bại";
                        ShowAlarm(XAlarmId.CARD_INIT_FAIL);
                        WriteCTLog(content);
                        return;
                    }
                }

                m_runStep = RunState.WaitCarrierInSignal;
                SetStep("Reset Complete", MyColor.Green);
                SetStation_StateWaitRun();
                WriteCTLog("Hoàn thành reset Task70");
                PageEngineering.Instance.UpdateTextBox("Hoàn thành reset Task70");
                homeDoneTaskNum++;

                if (homeDoneTaskNum == RequestHomeTaskNum)
                    BzMessagebox.Show(MultiLanguage.GetMessage("Hoàn thành reset thiết bị"));
                 
            }
            catch (Exception e)
            {
                
            }


        }
        protected override void Running(object runMode)
        {
            switch ((StationRunMode)runMode)
            {
                case StationRunMode.EmptyRun:
                    break;
                case StationRunMode.AutoRun:
                    SetStep("WaitCarrierInSignal", Color.Green);
                    AutoRun();
                    break;
                case StationRunMode.CPK_Inspection:
                    SetStep("CPK_Inspection Runing", Color.Orange);
                    PageEngineering.Instance.UpdateTextBox("Chạy chế độ test CPK Inspection");
                    CalibrationInspection();
                    break;
                default:
                    break;
            }
        }

        private static void CalibrationInspection()
        {
            
        }

        private void AutoRun()
        {
            mIsExecuatingAuto = true;
            while (true)
            {
                Thread.Sleep(10);

                if(StopRun)
                {
                    SetStep("Stop Task", Color.Red);
                    break;
                }                    
                if(PauseRun)
                {
                    SetStep("Pause Task", Color.Green);
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
            switch ((RunState)m_runStep)
            {
                case RunState.WaitCarrierInSignal:

                    //result = ShowAlarm(XAlarmId.Write_PLC_Err);
                    ////Thử đọc lại
                    //if(result == DialogResult.OK)
                    //{
                    //    m_runStep = RunState.CheckSNDummy;
                    //}
                    //if(result == DialogResult.Cancel)
                    //{
                    //    return;
                    //}
                    //if(result == DialogResult.Ignore)
                    //{
                    //    m_runStep = RunState.CheckSNDummy;
                    //}                


                    SetStep("Wait Carrier In", Color.Green);
                    short CarrierSignal_Value = 0;
                    SLMP.Instance.ReadWord(DevideCode.D, PLC_CarrierInSignal, out CarrierSignal_Value);
                    if(CarrierSignal_Value == 1)
                    {
                        AudioSystem.UCM.Units[0].StartTime = DateTime.Now;
                        m_runStep = RunState.CheckSNDummy;
                    }
                    break;
                    //Kiểm tra xem sản phẩm có đúng trạm không.
                case RunState.CheckSNDummy:
                    SetStep("PC >> CCD get SN", Color.Green);                               
                    if (TriggerScanner(scanlead_cmd, out serialNumber))
                    {
                        SetStep("check SN Dummy State",Color.Green);
                        if (Globals.SettingICT.CheckDummySN)
                        {
                            //Lấy dữ liệu sản phẩm cuối cùng
                            DataTable data = DataServerManager.Instance.SelectLastProduct();
                            if (serialNumber == data.Rows[0][1].ToString())
                            {
                                result = ShowAlarm(XAlarmId.Write_PLC_Err, "Write data to PLC failure");
                                //Xóa lỗi, báo sản phẩm NG
                                if (result == DialogResult.Cancel)
                                {
                                    m_runStep = RunState.WaitCarrierInSignal;
                                    WriteCTLog("PLC >> PC: Write D110 = 1");
                                    if (SLMP.Instance.WriteWord(DevideCode.D, PLC_CarrierInSignal, 0) == -1)
                                    {
                                        ShowAlarm(XAlarmId.Write_PLC_Err);
                                        return;
                                    }
                                }
                                //Đọc lại mã code
                                if (result == DialogResult.Yes)
                                {
                                    return;
                                }
                            }
                            if (Globals.RUNMODE != Globals.MachineRunMode.Assemble_Dry_Run)
                            {
                                m_runStep = RunState.AssyCheck;
                            }
                            else
                            {
                                m_runStep = RunState.Capture_Image_1;
                            }
                        }
                        SetStep("PC >> MES get Station", Color.Green);
                    }    
                    else
                    {
                        SetStep("Read serial number fail", Color.Red);
                        result = ShowAlarm(XAlarmId.CCD_Error, "Error Scanner");
                        //Thử lại
                        if (result == DialogResult.OK)
                        {
                            m_runStep = RunState.CheckSNDummy;
                        }   
                        //Dừng lại
                        else if (result == DialogResult.Cancel)
                        {
                            //if (OnPauseActive != null)
                            //    OnPauseActive(null, null);
                            SLMP.Instance.WriteWord(DevideCode.D, PLC_CarrierInSignal, 0);
                            return;
                        }

                    }    
                    break;
                case RunState.AssyCheck:
                    if (MES.AssyCheck_AOI(serialNumber, out messageResponse))
                    {
                        SetStep("MES >> PC: Correct Station, ", Color.Green);
                        m_runStep = RunState.Capture_Image_1;
                    }
                    else
                    {
                        //Wrong station
                        SetStep("MES >> PC: Wrong Station", Color.Red);
                        if (SLMP.Instance.WriteWord(DevideCode.D, PLC_CarrierInSignal, 0) == -1)
                        {
                            result = ShowAlarm(XAlarmId.Write_PLC_Err, "Write data to PLC failure");
                        }
                        m_runStep = RunState.WaitCarrierInSignal;
                    }
                    break;
                case RunState.Capture_Image_1:
                    SetStep("Capture Image 1", Color.Green);
                    //Run chương trình cam 1
                    Thread.Sleep(100);
                    m_runStep = RunState.Capture_Image_2;
                    break;

                case RunState.Capture_Image_2:
                    SetStep("Capture Image 2", Color.Green);
                    //Run chương trình cam 2
                    Thread.Sleep(100);
                    m_runStep = RunState.Read_SN_Product;
                    break;

                case RunState.Read_SN_Product:
                    //Đọc dữ liệu trên PLC
                    SLMP.Instance.ReadWord(DevideCode.D, PLC_CarrierInSignal, out CarrierSignal_Value);
                    if(CarrierSignal_Value == 1)
                    {
                        //Đọc mã vị trí 1
                        if(TriggerScanner(scanlead_cmd, out data))
                        {
                            string[] dataSpilit = data.Split(',');
                            lstSerialNumber.Add("SN1", dataSpilit[0]);
                        } 
                        else
                        {
                            WriteLog("Barcode >> PC : Đọc mã 1 lỗi!");
                            ShowAlarm(XAlarmId.BarCode_Err);
                        }
                    } 
                    if(CarrierSignal_Value == 2)
                    {
                        if (TriggerScanner(scanlead_cmd, out data))
                        {
                            string[] dataSpilit = data.Split(',');
                            lstSerialNumber.Add("SN2", dataSpilit[0]);
                        }                      
                        else
                        {
                            WriteLog("Barcode >> PC : Đọc mã 2 lỗi!");
                            ShowAlarm(XAlarmId.BarCode_Err);
                        }
                    }
                    if(CarrierSignal_Value == 3)
                    {
                        if (TriggerScanner(scanlead_cmd, out data))
                        {
                            string[] dataSpilit = data.Split(',');
                            lstSerialNumber.Add("SN3", dataSpilit[0]);
                        }
                        else
                        {
                            WriteLog("Barcode >> PC : Đọc mã 3 lỗi!");
                            ShowAlarm(XAlarmId.BarCode_Err);
                        }
                    }    
                    if(CarrierSignal_Value == 4)
                    {
                        if (TriggerScanner(scanlead_cmd, out data))
                        {
                            string[] dataSpilit = data.Split(',');
                            lstSerialNumber.Add("SN4", dataSpilit[0]);
                        }
                        else
                        {
                            WriteLog("Barcode >> PC : Đọc mã 4 lỗi!");
                            ShowAlarm(XAlarmId.BarCode_Err);
                        }
                    }   
                    if(CarrierSignal_Value == 5)
                    {
                        if (TriggerScanner(scanlead_cmd, out data))
                        {
                            string[] dataSpilit = data.Split(',');
                            lstSerialNumber.Add("SN5", dataSpilit[0]);
                            //Đọc giá trị cuối cùng rồi gửi lên hệ thống Mes
                            m_runStep = RunState.POST_Mes;
                        }
                        else
                        {
                            WriteLog("Barcode >> PC : Đọc mã 5 lỗi!");
                            ShowAlarm(XAlarmId.BarCode_Err);
                        }    

                    }
                    SLMP.Instance.WriteWord(DevideCode.D, PLC_CarrierInSignal, 0);
                    break;
                case RunState.POST_Mes:
                    SetStep("PC > MES : Send SN to Mes System", Color.Green);
                    pd.listSerialNumber = lstSerialNumber;
                    if (MES.AssyGo_AOI(pd, out messageResponse))
                    {
                        WriteProductDataToSQL("PASS");
                        SLMP.Instance.WriteWord(DevideCode.D, PLC_CarrierInSignal, 1);
                    }
                    else
                    {
                        WriteProductDataToSQL("FAIL");
                        SLMP.Instance.WriteWord(DevideCode.D, PLC_CarrierInSignal, 0);
                    }
                    m_runStep = RunState.WaitCarrierInSignal;
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
                serialNumber = ret;
                return true;
            }
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
        #region Scanner
        protected bool TriggerScanner(byte[] cmd, out string Data)
        {
            UpCCDScanSN:
            int iRetCCD = 0;
            Data = "";
            Thread.Sleep(200);

            Scanner_TCP.Instance.WriteCmd(cmd, 0);

            SetStep("Waiting to receive camera feedback data...", MyColor.LightGreen);
            if (Scanner_TCP.Instance.are_DataRecerveDone.WaitOne(5000) == false) // wait CCD reponse within 10sec
            {
                SetStep("Camera feedback data timeout!", MyColor.LightRed);
                WriteLog("Camera feedback data timeout!");
                iRetCCD = TriggerKeyenceError(MultiLanguage.GetMessage("Camera feedback data timeout"));
                return false;
            }
            SetStep("Parsing camera data…", MyColor.LightGreen);
            if (WaitScannerData() == false)
            {
                iRetCCD = TriggerKeyenceError(cmd + "-" + MultiLanguage.GetMessage("The camera feedback data format of this command is incorrect"));
                if (iRetCCD == 1)
                    goto UpCCDScanSN;
                else
                    return false;
            }
            Data = Scanner_TCP.Instance.RecData.ToString();           
            return true;
        }
        //Kiểm tra format của SN
        public bool WaitScannerData(int timeOutMs = 3000)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                //So sánh chuỗi dữ liệu
                if (Scanner_TCP.Instance.RecData.Length > 5 && Scanner_TCP.Instance.RecData.Contains("SPXVN"))
                {
                    int index = Scanner_TCP.Instance.RecData.IndexOf("SPXVN");
                    //Ngắt đi những ký tự không cần thiết
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


        protected int TriggerKeyenceError(string strTitle, XAlarmId alarmId = XAlarmId.CCD_Error)
        {
            SetStep(strTitle, MyColor.LightRed);
            m_AsmError = alarmId;
            DialogResult ret = ShowAlarmEx((XAlarmId)m_AsmError, strTitle);// ReportError(true, "", strTitle);
            if (ret == DialogResult.OK)
            {
                Thread.Sleep(500);
                return 1;
            }
            else if (ret == DialogResult.Ignore)
            {
                return 2;
            }
            else if (ret == DialogResult.Abort)
                return 99;
            else
            {
                return -1;
            }
        }

        protected DialogResult ShowAlarmEx(XAlarmId alarmId, string szDetail = "")
        {
            return ShowAlarm(alarmId, szDetail, -1);
        }

        private bool AddSerialNumberToList(string str)
        {
            string[] kq = str.Split(',');
            foreach (string k in kq)
            {
                if (k.Contains("A"))
                {
                    lstSerialNumber.Add("SN1", k);
                }
                if (k.Contains("B"))
                {
                    lstSerialNumber.Add("SN2", k);
                }
                if (k.Contains("C"))
                {
                    lstSerialNumber.Add("SN3", k);
                }
                if (k.Contains("D"))
                {
                    lstSerialNumber.Add("SN4", k);
                }
            }
            return true;
        }
        #endregion
        #region SQL Querry
        private void WriteProductDataToSQL(string result)
        {
            AudioSystem.UCM.Units[0].HiveState = (int)MachineSts.Running;
            AudioSystem.UCM.Units[0].UnitSN = serialNumber;
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

        private void WriteCTLog(string message)
        {
            string path = Globals.Dir_Task70 + "MainLog_" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            string str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " => " + message;
            CsvServer.Instance.WriteLine(path, str);
        }
    }

}
