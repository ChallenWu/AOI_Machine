using BoTech;
using Demo.Device;
using Demo.Page;
using Demo.Setting;
using HB_IWatch;
using NPOI.SS.Formula.Functions;
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
using System.Windows.Forms.DataVisualization.Charting;
using XCore;
using Models;

using VisionTools;
using VisionTools.Tools;
using VisionTools.Tools.ImageFile;
using VisionTools.Tools.TotalGraphic;
using OpenCvSharp;
using static Demo.Globals;
using NPOI.HSSF.Record.Chart;
//using OVisionPro;

namespace Demo.Task
{
    class Task70_Scanner : ETask
    {
        protected DialogResult result;
        public static string serialNumber = "";
        public int intResult = 0;
        public int i;
        public string CCDResult = "";
        //Thanh ghi giao tiếp với PLC
        public int PLC_CCD_Request = 110;
        public int PLC_Scanner_Request = 100;
        public int PLC_MES_Request = 120;

        public int PC_CCD_Reponse = 210;
        public int PC_Scanner_Response = 200;
        public int PC_MES_Response = 220;

        private short RequestValue;

        private short NumberOfCode = 0;

        public static int PLC_ErrorCodeReg = 6000;
        ProductInformation pd;
        BackMessage messageResponse;
        private List<string> lstSerialNumber;

        Mat ImageSource1 = new Mat();
        Mat ImageSource2 = new Mat();

        Mat ImageResult1 = new Mat();
        Mat ImageResult2 = new Mat();

        protected XAlarmId m_AsmError = XAlarmId.NONE;

        private int timeScan = 0;
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
            Wait_Capture_Image1,
            Wait_Capture_Image2,
            GetProductPN,
            CheckSNDummy,
            AssyCheck,            
            Read_SN_AssyPart,
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

                WriteTaskLog("Reset running task");
                if(Globals.SettingParameter.Light_Connect)
                {
                    TurnOnTheLight();
                    Thread.Sleep(100);
                    TurnOffTheLight();
                }   
                lstSerialNumber = new List<string>();

                //Số lượng mã SN cần đọc
                SLMP.Instance.ReadWord(DevideCode.D, 280, out NumberOfCode);
                if (Globals.SettingParameter.PLC_Connect)
                {
                    if (SLMP.Instance.Open() < 0)
                    {                       
                        PageEngineering.Instance.AddLogRichtextBox($"PC cant connect to PLC. Please re check the connection between PC and PLC",Color.Red);
                        WriteTaskLog($"PC cant connect to PLC. Please re check the connection between PC and PLC");
                        result = ShowAlarm(XAlarmId.CONNECT_PLC_FAIL);
                        return;
                    }
                    else
                    {
                        SLMP.Instance.WriteBit(DevideCode.M, 4010, true);
                    }
                }

                PageEngineering.Instance.AddLogRichtextBox("Complete finish taskrun",Color.Green);
                m_runStep = RunState.Wait_Capture_Image1;
                SetStep("Reset Complete", MyColor.Green);
                SetStation_StateWaitRun();
                WriteTaskLog("Complete initilize all the run task of software");
                homeDoneTaskNum++;

                if (homeDoneTaskNum == RequestHomeTaskNum)
                    //BzMessagebox.Show(MultiLanguage.GetMessage("complete initilize all the run task of this software"));

                PageEngineering.Instance.AddLogRichtextBox("Complete initilize all the run task of software", Color.Green);
            }
            catch (Exception e)
            {
                Console.WriteLine("The Exception is" + e.ToString());
            }
        }
        protected override void Running(object runMode)
        {
            switch ((StationRunMode)runMode)
            {
                case StationRunMode.EmptyRun:
                    break;
                case StationRunMode.AutoRun:
                    SetStep("Wait_Capture_Image1", Color.Green);
                    AutoRun();
                    break;
                case StationRunMode.CPK_Inspection:
                    SetStep("CPK_Inspection Runing", Color.Orange);
                    PageEngineering.Instance.AddLogRichtextBox("RUN CPK Inspection MODE",Color.Green);
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
                if (StopRun)
                {
                    SetStep("Stop Task", Color.Red);
                    break;
                }
                if (PauseRun)
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
            try
            {
                switch ((RunState)m_runStep)
                {
                    case RunState.Wait_Capture_Image1:
                        SetStep("Wait Carrier", Color.Green);
                        pd = new ProductInformation();
                        SLMP.Instance.ReadWord(DevideCode.D, PLC_CCD_Request, out RequestValue);
                        //Không kết nối PLC và chọn chế độ Dryrun
                        if (!Globals.SettingParameter.PLC_Connect && Globals.isDryrun)
                        {
                            SystemVariable.UCM.Unit.StartTime = DateTime.Now;
                            TurnOnTheLight();
                            Thread.Sleep(500);
                            PageEngineering.Instance.AddLogRichtextBox($"Capture the image 1", Color.Green);
                            WriteTaskLog("Capture the image 1");
                            //Chay kiem tra tool 1
                            Stopwatch sw = new Stopwatch();
                            sw.Start();
                            Mat Image1 = CaptureImage();
                            RunVisionTool1(Image1);
                            sw.Stop();
                            WriteTaskLog("Finish capture image 1");
                            PageEngineering.Instance.AddLogRichtextBox($" Finish capture image 1. Tacktime process vision tool 1 = {sw.ElapsedMilliseconds}ms", Color.Red);
                            m_runStep = RunState.Wait_Capture_Image2;
                        }

                        if (RequestValue != 0)
                        {
                            SystemVariable.UCM.Unit.StartTime = DateTime.Now;
                            if (SLMP.Instance.WriteWord(DevideCode.D, PLC_CCD_Request, 0) == -1)
                            {
                                result = ShowAlarm(XAlarmId.WRITE_DOWN_PLC_ERROR);
                                if (result == DialogResult.Cancel)
                                    break;
                            }
                            if (RequestValue == 1)
                            {
                                WriteTaskLog($"PLC >> PC : D{PLC_CCD_Request} = {RequestValue}, Capture the image 1");
                                PageEngineering.Instance.AddLogRichtextBox($"PLC >> PC : D{PLC_CCD_Request} = {RequestValue}, Capture the image 1", Color.Red);
                                TurnOnTheLight();
                                Thread.Sleep(1000);

                                Stopwatch sw = new Stopwatch();
                                sw.Start();
                                var retryTime = 0;
                                CaptureImage1:
                                ImageSource1 = CaptureImage();

                                if(Globals.isDryrun && Globals.isImage1)
                                {
                                    TurnOffTheLight();
                                    SLMP.Instance.WriteWord(DevideCode.D, PC_CCD_Reponse, 2);
                                    result = ShowAlarm(XAlarmId.CAPTURE_IMAGE_FAIL);
                                    WriteResultToSQL("FAIL");
                                    m_runStep = RunState.Wait_Capture_Image1;
                                    break;
                                }

                                if (ImageSource1.Rows ==0 || ImageSource1.Cols == 0)
                                {
                                    if(retryTime < 1)
                                    {
                                        retryTime++;
                                        WriteTaskLog($"Image is null, try capture image again");
                                        goto CaptureImage1;
                                    }   
                                    else
                                    {
                                        TurnOffTheLight();
                                        SLMP.Instance.WriteWord(DevideCode.D, PC_CCD_Reponse, 2);
                                        result = ShowAlarm(XAlarmId.CAPTURE_IMAGE_FAIL);                                        
                                        WriteResultToSQL("FAIL");                                       
                                        m_runStep = RunState.Wait_Capture_Image1;
                                        break;
                                    }                                         
                                }


                                //TurnOffTheLight();
                                //RunVisionTool1(ImageSource1);
                                PageEngineering.Instance.AddLogRichtextBox("Start image processing 1", Color.Green);
                                Thread VisionTool1 = new Thread(new ThreadStart(() =>
                                {
                                    RunVisionTool1(ImageSource1);
                                }));
                                VisionTool1.IsBackground = true;
                                VisionTool1.Start();
                                
                                sw.Stop();

                                SetStep("Capturing 1st Image", Color.Green);
                                PageEngineering.Instance.AddLogRichtextBox($"Tacktime for get Image 1 = {sw.ElapsedMilliseconds}ms. Complete get the image 1." +
                                                                            $"Move to the image 2 position", Color.Green);
                                WriteTaskLog($"Tacktime for get Image 1 = {sw.ElapsedMilliseconds}ms. Complete get the image 1." +
                                                                            $"Move to the image 2 position");
                                if (SLMP.Instance.WriteWord(DevideCode.D, PC_CCD_Reponse, 1) == -1)
                                {
                                    result = ShowAlarm(XAlarmId.WRITE_DOWN_PLC_ERROR);
                                }
                                m_runStep = RunState.Wait_Capture_Image2;
                            }
                            //Bypass từ plc
                            if (RequestValue == 2)
                            {
                                m_runStep = RunState.Wait_Capture_Image2;
                                WriteTaskLog($"PLC >> PC : D{PLC_CCD_Request} = {RequestValue}, PLC requires bypass capture the first image. Jump to next Step");
                                PageEngineering.Instance.AddLogRichtextBox($"PLC >> PC: D{PLC_CCD_Request} = {RequestValue}, PLC requires bypass capture the first image. Jump to next Step", Color.Red);
                            }
                            PageEngineering.Instance.AddLogRichtextBox($"PLC >> PC: D{PLC_CCD_Request} = 0; D{PC_CCD_Reponse} = 1", Color.Green);
                        }
                        break;

                    case RunState.Wait_Capture_Image2:
                        SetStep("Wait Capture second Image", Color.Green);

                        if (!Globals.SettingParameter.PLC_Connect)
                        {
                            WriteTaskLog($"Capture the second image");
                            PageEngineering.Instance.AddLogRichtextBox($"Capture the second image", Color.Green);
                            Thread.Sleep(2000);
                            Stopwatch sw = new Stopwatch();
                            sw.Start();
                            ImageSource2 = CaptureImage();
                            RunVisionTool1(ImageSource2);
                            sw.Stop();
                            PageEngineering.Instance.AddLogRichtextBox($"Tacktime process vision tool 2 = {sw.ElapsedMilliseconds}ms", Color.Red);
                            m_runStep = RunState.GetProductPN;
                        }

                        SLMP.Instance.ReadWord(DevideCode.D, PLC_CCD_Request, out RequestValue);
                        if (RequestValue != 0)
                        {
                            WriteTaskLog("Start capture image 2");
                            SetStep("Capturing 2nd Image...", Color.Green);
                            if (SLMP.Instance.WriteWord(DevideCode.D, PLC_CCD_Request, 0) == -1)
                            {
                                result = ShowAlarm(XAlarmId.WRITE_DOWN_PLC_ERROR);
                            }

                            if (RequestValue == 1)
                            {
                                WriteTaskLog($"PLC >> PC : D{PLC_CCD_Request} = {RequestValue},PLC requires capture the second image");
                                PageEngineering.Instance.AddLogRichtextBox($"PLC >> PC: D{PLC_CCD_Request} = {RequestValue}, PLC requires capture the second image", Color.Red);
                                //Run Vision 2
                                //TurnOnTheLight();
                                //Thread.Sleep(1000);
                                Stopwatch sw = new Stopwatch();
                                sw.Start();
                                var retryTime = 0;
                                CaptureImage2:
                                ImageSource2 = CaptureImage();

                                if (Globals.isDryrun && Globals.isImage2)
                                {
                                    TurnOffTheLight();
                                    SLMP.Instance.WriteWord(DevideCode.D, PC_CCD_Reponse, 2);
                                    result = ShowAlarm(XAlarmId.CAPTURE_IMAGE_FAIL);
                                    WriteResultToSQL("FAIL");
                                    m_runStep = RunState.Wait_Capture_Image1;
                                    break;
                                }


                                if (ImageSource2.Rows ==0 || ImageSource2.Cols == 0)
                                {
                                    if(retryTime<1)
                                    {
                                        WriteTaskLog($"Image is null, try capture image again");
                                        retryTime++;
                                        goto CaptureImage2;
                                    }    
                                    else
                                    {
                                        TurnOffTheLight();
                                        SLMP.Instance.WriteWord(DevideCode.D, PC_CCD_Reponse, 2);
                                        result = ShowAlarm(XAlarmId.CAPTURE_IMAGE_FAIL);
                                        WriteResultToSQL("FAIL");
                                        m_runStep = RunState.Wait_Capture_Image1;
                                        break;
                                    }                                          
                                }
                                TurnOffTheLight();
                                //RunVisionTool2(ImageSource2);
                                PageEngineering.Instance.AddLogRichtextBox("Start image processing 2", Color.Green);
                                Thread VisionTool2 = new Thread(new ThreadStart(() =>
                                {
                                    RunVisionTool2(ImageSource2);

                                }));
                                VisionTool2.IsBackground = true;
                                VisionTool2.Start();
                                

                                sw.Stop();
                                PageEngineering.Instance.AddLogRichtextBox($"Tacktime process vision tool 2 = {sw.ElapsedMilliseconds}ms", Color.Red);
                                WriteTaskLog($"Tacktime process vision tool 2 = {sw.ElapsedMilliseconds}ms");
                                if (SLMP.Instance.WriteWord(DevideCode.D, PC_CCD_Reponse, 3) == -1)
                                {
                                    result = ShowAlarm(XAlarmId.WRITE_DOWN_PLC_ERROR);
                                    if (result == DialogResult.Cancel)
                                        break;
                                }
                            }
                            else
                            {
                                PageEngineering.Instance.AddLogRichtextBox($"PLC >> PC : D{PLC_CCD_Request} = {RequestValue}, Bypass ", Color.Red);
                            }
                            m_runStep = RunState.GetProductPN;
                            WriteTaskLog($"PC >> PLC: D{PLC_CCD_Request} = 0, D{PC_CCD_Reponse} = 1. " +
                                $"Finish the capture image. Go to the read Product's PN/SN position");
                            PageEngineering.Instance.AddLogRichtextBox($"PC >> PLC: D{PLC_CCD_Request} = 0, D{PC_CCD_Reponse} = 1. " +
                                $"Finish the capture image. Go to the read Product's PN/SN position", Color.Green);

                            lstSerialNumber.Clear();
                            timeScan = 0;
                        }
                        break;

                    case RunState.GetProductPN:
                        TurnOffTheLight();
                        SetStep("Go to read PN/SN product product", Color.Green);

                        if (!Globals.SettingParameter.PLC_Connect && Globals.isDryrun)
                        {
                            Thread.Sleep(2000);
                            WriteTaskLog($"Read PN/SN OK. Check system data");
                            PageEngineering.Instance.AddLogRichtextBox($"Read PN/SN OK. Check system data", Color.Black);
                            m_runStep = RunState.AssyCheck;
                        }

                        SLMP.Instance.ReadWord(DevideCode.D, PLC_MES_Request, out RequestValue);
                        if (RequestValue != 0)
                        {
                            WriteTaskLog($"PLC >> PC : D{PLC_CCD_Request} = {RequestValue}, Start read PN");
                            PageEngineering.Instance.AddLogRichtextBox($"PLC >> PC: D{PLC_CCD_Request} = {RequestValue}, Start read PN", Color.Red);
                            if (RequestValue == 1)
                            {
                                if (TriggerScanner(scanlead_cmd, out serialNumber))
                                {
                                    WriteTaskLog($"Complete read PN. PN = {serialNumber}");
                                    PageEngineering.Instance.AddLogRichtextBox($"Complete read PN. PN = {serialNumber}", Color.Red);
                                    if (Globals.SettingParameter.CheckDummySN)
                                    {
                                        m_runStep = RunState.CheckSNDummy;
                                    }
                                    else
                                    {
                                        m_runStep = RunState.AssyCheck;
                                    }
                                }
                                else
                                {
                                    if (!Globals.isPn && Globals.isDryrun)
                                    {
                                        WriteTaskLog("[SIM MODE] Read PN product fail, jump to RunState Assycheck");
                                        PageEngineering.Instance.AddLogRichtextBox("[SIM MODE] Bypass SN, jump to Assycheck state", Color.Red);
                                        m_runStep = RunState.AssyCheck;
                                        break;
                                    }
                                    PageEngineering.Instance.AddLogRichtextBox("Read PN error", Color.Red);
                                    WriteTaskLog("Read PN error");
                                    //Send ErrCode to PLC
                                    SLMP.Instance.WriteWord(DevideCode.D, PC_MES_Response, 2);
                                    result = ShowAlarm(XAlarmId.READ_MAIN_CODE);
                                    //Chạy bước tiếp
                                    if (result == DialogResult.Cancel)
                                    {
                                        //Đẩy sản phẩm về NG
                                        WriteResultToSQL("FAIL");
                                        XStationManager.Instance.Continue();
                                        SetStep("Wait Capture Image 1",Color.Green);
                                        m_runStep = RunState.Wait_Capture_Image1;
                                        break;
                                    }
                                }
                            }

                            if (RequestValue == 2)
                            {
                                if (TriggerScanner(scanlead_cmd, out serialNumber))
                                {
                                    serialNumber = "PC_BYPASS_SN";
                                }
                                PageEngineering.Instance.AddLogRichtextBox("PLC send bypass SN, go to Assy Check", Color.Red);
                                WriteTaskLog("PLC send bypass SN, đến go to Assy Check");
                                //Go assyCheck
                                m_runStep = RunState.AssyCheck;
                                break;
                            }
                        }
                        break;

                    case RunState.CheckSNDummy:
                        PageEngineering.Instance.AddLogRichtextBox("Check if dummy the PN of the last product", Color.Green);
                        WriteTaskLog("Check if dummy the PN of the last previous product");
                        SetStep("Check PN/SN Dummy >>>", Color.Green);
                        DataTable dataTable = DataServerManager.Instance.SelectLastProduct();
                        if (serialNumber == dataTable.Rows[0][1].ToString())
                        {
                            result = ShowAlarm(XAlarmId.SN_DUMMY, "Dummy the last product");
                            PageEngineering.Instance.AddLogRichtextBox("Error: The current product is the same as the PN/SN of last product", Color.Red);
                            //Bỏ qua
                            if (result == DialogResult.OK)
                            {
                                PageEngineering.Instance.AddLogRichtextBox("User press the NextStep button", Color.Black);
                                WriteTaskLog("User press the NextStep button");
                                m_runStep = RunState.AssyCheck;
                            }
                            //Dừng máy, gửi NG cho PLC
                            else if (result == DialogResult.Cancel)
                            {
                                PageEngineering.Instance.AddLogRichtextBox("Equipment stops, push product to the NG Area", Color.Black);
                                WriteTaskLog("Equipment stops, push product to the NG Area");
                                m_runStep = RunState.Wait_Capture_Image1;
                                SLMP.Instance.WriteWord(DevideCode.D, PC_Scanner_Response, 2);
                            }
                        }
                        else
                        {
                            m_runStep = RunState.AssyCheck;
                        }
                        break;

                    case RunState.AssyCheck:
                        SetStep("Stage: AssyCheck", Color.Green);
                        if (Globals.SettingParameter.MES_Connnect == false && Globals.isMES == false)
                        {
                            WriteTaskLog("[SIM MODE] Send PN to MES system");
                            Thread.Sleep(1000);
                            SLMP.Instance.WriteWord(DevideCode.D, PLC_MES_Request, 0);
                            SLMP.Instance.WriteWord(DevideCode.D, PC_MES_Response, 1);
                            m_runStep = RunState.Read_SN_AssyPart;
                            PageEngineering.Instance.AddLogRichtextBox($"PC >> PLC: Write D{PLC_MES_Request} = 0, D{PC_MES_Response} = 1," +
                                                                       $"[SIM MODE] Compelte check MES. Correct station. Go to step read SN of assemble part", Color.Green);
                            break;
                        }

                        WriteTaskLog("Start Upload PN to MES system...");

                        if (Globals.isStation && Globals.isDryrun)
                        {
                            SLMP.Instance.WriteWord(DevideCode.D, 220, 2);
                            SetStep("MES >> PC: Station is incorrect", Color.Red);
                            PageEngineering.Instance.AddLogRichtextBox($"PC >> PLC: Write D220 = 2, incorrect station", Color.Green);
                            WriteTaskLog($"PC >> PLC: PC >> PLC: Write D220 = TRUE, incorrect station");
                            result = ShowAlarm(XAlarmId.WRONG_STATION);
                            //Put NG
                            if (result == DialogResult.Cancel)
                            {
                                //Quay về step 1, chờ sản phẩm khác
                                PageEngineering.Instance.AddLogRichtextBox($"Save Log fail to SQL. Back to Wait Capture Image 1", Color.Green);
                                WriteTaskLog($"Save Log fail to SQL. Back to Wait Capture Image 1");
                                WriteResultToSQL("FAIL");
                                m_runStep = RunState.Wait_Capture_Image1;
                            }
                        }


                        if (MES.AssyCheck_AOI(serialNumber, out messageResponse))
                        {
                            SetStep("MES >> PC: Correct Station", Color.Green);
                            SLMP.Instance.WriteWord(DevideCode.D, PLC_MES_Request, 0);
                            SLMP.Instance.WriteWord(DevideCode.D, PC_MES_Response, 1);
                            PageEngineering.Instance.AddLogRichtextBox($"PC >> PLC: Write D{PLC_MES_Request} = 0,D{PC_MES_Response} = 1" +
                                                                       $"Go to read the SN of assemble part", Color.Green);
                            m_runStep = RunState.Read_SN_AssyPart;
                        }
                        else
                        {                            
                            //Wrong station,
                            //Send Error to PLC
                            SLMP.Instance.WriteWord(DevideCode.D, 220, 2);
                            SetStep("MES >> PC: Station is incorrect", Color.Red);
                            PageEngineering.Instance.AddLogRichtextBox($"PC >> PLC: Write D220 = 2, incorrect station", Color.Green);
                            WriteTaskLog($"PC >> PLC: PC >> PLC: Write D220 = 2, incorrect station");
                            result = ShowAlarm(XAlarmId.WRONG_STATION);
                            //Put NG
                            if (result == DialogResult.Cancel)
                            {
                                //Quay về step 1, chờ sản phẩm khác
                                PageEngineering.Instance.AddLogRichtextBox($"Save Log fail to SQL. Back to Wait Capture Image 1", Color.Green);
                                WriteTaskLog($"Save Log fail to SQL. Back to Wait Capture Image 1");
                                WriteResultToSQL("FAIL");
                                m_runStep = RunState.Wait_Capture_Image1;
                            }
                        }
                        break;

                    case RunState.Read_SN_AssyPart:
                        SetStep("PC >> SCANNER: SN for each material Part", Color.Green);

                        SLMP.Instance.ReadWord(DevideCode.D, PLC_Scanner_Request, out RequestValue);
                        if (RequestValue != 0 || !Globals.SettingParameter.PLC_Connect)
                        {
                            timeScan++;
                            PageEngineering.Instance.AddLogRichtextBox($"PLC >> PC: D{PLC_CCD_Request} = {RequestValue}. " +
                                                                       $"is reading the SN of the {timeScan} assemble part", Color.Red);

                            // Kiểm tra kết quả 2 tool
                            if (ResultVision.Count > 0 ||  (Globals.isDryrun && Globals.AOI_Result))
                            {                                 
                                //Ghi mã lỗi xuống PLC.
                                SLMP.Instance.WriteWord(DevideCode.D, 212, 2);
                                //SLMP.Instance.WriteBit(DevideCode.M, 4021, true);
                                result = ShowAlarm(XAlarmId.CCD_ERROR);
                                //Put NG
                                if (result == DialogResult.Cancel)
                                {
                                    WriteTaskLog("Inspection NG, User select put product to NG Area");
                                    PageEngineering.Instance.AddLogRichtextBox("Inspection NG, User select put product to NG Area", Color.Red);
                                    WriteResultToSQL("FAIL");
                                    //SLMP.Instance.WriteBit(DevideCode.M, 800, true);
                                    Thread.Sleep(50);
                                    //SLMP.Instance.WriteBit(DevideCode.M, 800, false);

                                    m_runStep = RunState.Wait_Capture_Image1;
                                    XStationManager.Instance.Continue();

                                    ImageResult1 = PageVision.Instance.displayViewInteract1.GetDisplayImage();
                                    ImageResult2 = PageVision.Instance.displayViewInteract2.GetDisplayImage();
                                    Globals.SaveImageToFolder(ImageSource1, ImageSource2, ImageResult1, ImageResult2, false);
                                    timeScan = 0;
                                    break;
                                }
                            }

                            //Không dùng tới scanner, Giả lập mã ảo cho từng vị trí
                            WriteTaskLog("[SIM MODE] Reading SN...");
                            if (Globals.isDryrun)
                            {
                                TriggerScanner(scanlead_cmd, out string serialNumber);
                                if(Globals.isSn)
                                {
                                    PageEngineering.Instance.AddLogRichtextBox($"SN is incorrect. ", Color.Black);
                                    SLMP.Instance.WriteWord(DevideCode.D, PC_Scanner_Response, 2);
                                    result = ShowAlarm(XAlarmId.READ_MAIN_CODE);
                                    //PutNG
                                    if (result == DialogResult.Cancel)
                                    {
                                        WriteResultToSQL("FAIL");
                                        //Send NG to PLC, request new sample
                                        timeScan = 0;
                                        XStationManager.Instance.Continue();
                                        m_runStep = RunState.Wait_Capture_Image1;
                                        break;
                                    }
                                }
                                if (serialNumber == "")
                                {
                                    WriteTaskLog($"[SIM MODE] The SN of assemble part {timeScan} is ABC_XYZ");
                                    serialNumber = "ABC_XYZ";
                                }
                                SN_List($"{serialNumber}");
                                PageEngineering.Instance.AddLogRichtextBox($"[SIM MODE] The serial number of assy part is {serialNumber}", Color.Black);
                            }
                            else
                            {
                                if (TriggerScanner(scanlead_cmd, out string serialNumber))
                                {
                                    SN_List(serialNumber);
                                }
                                else
                                {                                    
                                    PageEngineering.Instance.AddLogRichtextBox($"SN is incorrect. ", Color.Black);
                                    SLMP.Instance.WriteWord(DevideCode.D, PC_Scanner_Response, 2);
                                    result = ShowAlarm(XAlarmId.READ_MAIN_CODE);
                                    //PutNG
                                    if (result == DialogResult.Cancel)
                                    {
                                        WriteResultToSQL("FAIL");
                                        timeScan = 0;
                                        XStationManager.Instance.Continue();
                                        m_runStep = RunState.Wait_Capture_Image1;
                                        break;
                                    }
                                }
                            }

                            //Đủ số lượng code cài đặt
                            if (lstSerialNumber.Count == NumberOfCode)
                            {
                                m_runStep = RunState.POST_Mes;
                                timeScan = 0;
                                PageEngineering.Instance.AddLogRichtextBox($"Complete read all the SN paste on sample. Start upload data to MES...", Color.Green);
                                WriteTaskLog("Complete read all the SN paste on sample.Start upload data to MES...");
                                break;
                            }
                            else
                            {
                                SLMP.Instance.WriteWord(DevideCode.D, PLC_Scanner_Request, 0);
                                if (SLMP.Instance.WriteWord(DevideCode.D, PC_Scanner_Response, 1) == -1)
                                {
                                    MessageBox.Show("Cant write data to the PLC");

                                }
                                PageEngineering.Instance.AddLogRichtextBox($"PC >> PLC : Write D{PLC_Scanner_Request} = 0; D{PC_Scanner_Response} = 1," +
                                                                           $"Continue go to the next position {timeScan + 1}", Color.Green);
                            }
                        }
                        break;

                    case RunState.POST_Mes:
                        SetStep("PC > MES : Send SN to Mes System", Color.Green);
                        //isMes de tao ma loi MES
                        if (Globals.isDryrun)
                        {
                            if(Globals.isMES)
                            {
                                WriteResultToSQL("FAIL");
                                result = ShowAlarm(XAlarmId.MES_NG);
                                SLMP.Instance.WriteWord(DevideCode.D, PLC_Scanner_Request, 0);
                                SLMP.Instance.WriteWord(DevideCode.D, PC_Scanner_Response, 2);
                                if (result == DialogResult.Cancel)
                                {
                                    m_runStep = RunState.Wait_Capture_Image1;
                                }
                                SetStep("Wait Capture Image 1", Color.Green);
                                WriteTaskLog($"PC >> PLC : Write D{PLC_Scanner_Request} = 0, D{PC_Scanner_Response} = 2, Push data to the MES system FAIL.");
                                PageEngineering.Instance.AddLogRichtextBox($"PC >> PLC : Write D{PLC_Scanner_Request} = 0, D{PC_Scanner_Response} = 2," +
                                                                           $" Push data to the MES system FAIL.", Color.Red);
                                break;
                            }

                            SLMP.Instance.WriteWord(DevideCode.D, 212, 1);
                            WriteTaskLog("Upload data to MES");
                            Thread.Sleep(1000);
                            WriteResultToSQL("PASS");
                            m_runStep = RunState.Wait_Capture_Image1;
                            SLMP.Instance.WriteWord(DevideCode.D, PLC_Scanner_Request, 0);
                            if (SLMP.Instance.WriteWord(DevideCode.D, PC_Scanner_Response, 1) == -1)
                            {
                                MessageBox.Show("Cant write down data to the PLC");
                            }
                            WriteTaskLog("Upload succes");
                            PageEngineering.Instance.AddLogRichtextBox($"PC >> PLC: D{PLC_Scanner_Request} = 0, D{PC_Scanner_Response} = 1, " +
                                                                       $"[SIM MODE] push data to MES system SUCCESS", Color.Green);
                            //Lưu ảnh vào folder
                            ImageResult1 = PageVision.Instance.displayViewInteract1.GetDisplayImage();
                            ImageResult2 = PageVision.Instance.displayViewInteract2.GetDisplayImage();
                            Globals.SaveImageToFolder(ImageSource1, ImageSource2, ImageResult1, ImageResult2, true);

                            //Clear ImageBox khi kết thúc lưu trình
                            PageVision.Instance.displayViewInteract1.NotifyDrawing.Clear();
                            PageVision.Instance.displayViewInteract2.NotifyDrawing.Clear();

                            PageVision.Instance.displayViewInteract1.Image = null;
                            PageVision.Instance.displayViewInteract2.Image = null;

                            break;
                        }
                        //Đấy dữ liệu lên hệ thống
                        if (MES.AssyGo_AOI(SystemVariable.UCM.Unit, out messageResponse) && Globals.isMES)
                        {
                            WriteResultToSQL("PASS");
                            WriteTaskLog("Upload data to the MES system SUCCESS.");
                            SLMP.Instance.WriteWord(DevideCode.D, PLC_Scanner_Request, 0);
                            SLMP.Instance.WriteWord(DevideCode.D, PC_Scanner_Response, 1);
                            PageEngineering.Instance.AddLogRichtextBox($"PLC >> PC : Write D{PLC_Scanner_Request} = 0; D{PC_Scanner_Response} = 1," +
                                                                       $"Upload data to the MES system SUCCESS", Color.Green);
                        }
                        else
                        {
                            WriteResultToSQL("FAIL");
                            result = ShowAlarm(XAlarmId.MES_NG);
                            SLMP.Instance.WriteWord(DevideCode.D, PLC_Scanner_Request, 0);
                            SLMP.Instance.WriteWord(DevideCode.D, PC_Scanner_Response, 2);
                            if (result == DialogResult.Cancel)
                            {
                                m_runStep = RunState.Wait_Capture_Image1;
                            }
                            SetStep("Wait Capture Image 1",Color.Green);
                            WriteTaskLog($"PC >> PLC : Write D{PLC_Scanner_Request} = 0, D{PC_Scanner_Response} = 2, Push data to the MES system FAIL.");
                            PageEngineering.Instance.AddLogRichtextBox($"PC >> PLC : Write D{PLC_Scanner_Request} = 0, D{PC_Scanner_Response} = 2," +
                                                                       $" Push data to the MES system FAIL.", Color.Red);
                        }
                        break;

                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("The exception is:" + ex.ToString());
            }
        }

        #region ICW_Serial Port
        private bool ReadSN()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            string ret = null;
            ret = ScannerComm.Instance.ReadQR();
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
        #region Comunication with CCD device
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
                //if (KeyenceService.Instance.RecData.Length == Globals.SettingParameter.SN1_Lenght)
                //    return true;

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
            int iRetCCD = 0;
            ScanSN:
            Data = "";
            Thread.Sleep(10);

            //Scanner_TCP.Instance.WriteCmd(cmd, 0);
            Scanner_TCP.Instance.WriteCmd("TRIGGER", 0);

            SetStep("Chờ Scanner phản hồi dữ liệu", MyColor.LightGreen);
            if (Scanner_TCP.Instance.are_DataRecerveDone.WaitOne(5000) == false) // wait CCD reponse within 5s
            {
                SetStep("Scanner không phản hồi!", MyColor.LightRed);
                WriteLog("Scanner không phản hồi dữ liệu!");
                //iRetCCD = TriggerKeyenceError(MultiLanguage.GetMessage("Camera feedback data timeout"));
                return false;
            }
            SetStep("Parsing data…", MyColor.LightGreen);
            if (WaitScannerData(1000) == false)
            {
                WriteLog("The camera feedback data format of this command is incorrect!!");
                if (iRetCCD < 2)
                {
                    iRetCCD++;
                    goto ScanSN;
                }
                else
                    return false;
                //iRetCCD = TriggerKeyenceError(cmd + "-" + MultiLanguage.GetMessage("The camera feedback data format of this command is incorrect"));
                //if (iRetCCD == 1)
                //    goto UpCCDScanSN;
                //else
                //    return false;
            }
            Data = Scanner_TCP.Instance.RecData.ToString();
            return true;
        }
        //Kiểm tra format của SN
        public bool WaitScannerData(int timeOutMs)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                if (Scanner_TCP.Instance.RecData == "FAIL")
                    return false;
                //switch Case part list PN + 12 SN list
                //So sánh chuỗi dữ liệu
                if (Scanner_TCP.Instance.RecData.Length > 5 && Scanner_TCP.Instance.RecData.Contains("SPXVN"))
                {
                    int index = Scanner_TCP.Instance.RecData.IndexOf("SPXVN");
                    //Ngắt đi những ký tự không cần thiết
                    Scanner_TCP.Instance.RecData = Scanner_TCP.Instance.RecData.Substring(index);
                    return true;
                }
                //if (Scanner_TCP.Instance.RecData.Length == Globals.SettingParameter.SN1_Lenght)
                //    return true;
                if (sw.ElapsedMilliseconds > timeOutMs)
                    return false;
                Thread.Sleep(3);
            }
            while (true);
        }
        protected int TriggerKeyenceError(string strTitle, XAlarmId alarmId = XAlarmId.READ_CODE_FAIL)
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
        private bool SN_List(string serialNumber)
        {
            //Kiểm tra xem mã có đúng yêu cầu không
            if (serialNumber.Length != Globals.SettingSerialNumber.SN1_Lenght || !serialNumber.Contains(Globals.SettingSerialNumber.SN11_Keywords))
            {
                return false;
            }
            return true;
        }
        #endregion
        #region SQL Querry
        private void WriteResultToSQL(string result)
        {
            SystemVariable.UCM.Unit.HiveState = (int)MachineSts.Running;
            if (serialNumber == "")
            {
                serialNumber = "TEST_01";
            }
            SystemVariable.UCM.Unit.UnitSN = serialNumber;
            SystemVariable.UCM.Unit.ComponentSN = "ABC";
            SystemVariable.UCM.Unit.Pass = result;
            SystemVariable.UCM.Unit.EndTime = DateTime.Now;
            SystemVariable.UCM.Unit.CT = (SystemVariable.UCM.Unit.EndTime - SystemVariable.UCM.Unit.StartTime).TotalSeconds;
            SystemVariable.UCM.Unit.ModelProduct = LoadModel.currentModel.modelName;
            DateTime enddt = DateTime.Now;
            if (SystemVariable.UCM.Unit.StartTime.Hour >= 8 && SystemVariable.UCM.Unit.StartTime.Hour <= 20)
                SystemVariable.UCM.Unit.Shift = "DS";
            else
                SystemVariable.UCM.Unit.Shift = "NS";

            //Insert vào sql 
            DataServerManager.Instance.InsertUnitMessage(SystemVariable.UCM, 0);
            //Async UI
            PageProduction.Instance.Async_IO_Refresh(SystemVariable.UCM, 0);

            //Query lại dữ liệu khi update thêm product
            Thread Query = new Thread(new ThreadStart(() =>
            {
                PageChart.Instance.QueryFromOtherForm();
            }));
            Query.IsBackground = true;
            Query.Start();


        }
        #endregion
        #region Control Light
        private void TurnOnTheLight()
        {
            LR_Light.Instance.LightOn(Globals.SettingParameter.MiddleLightSensitive);
            Mid_Light_TCP.Instance.WriteCmd($"IDC,OPPS,1,{Globals.SettingParameter.Left_Sensitive},\r\n", 0);
            Mid_Light_TCP.Instance.WriteCmd($"IDC,OPPS,2,{Globals.SettingParameter.Right_Sensitive},\r\n", 0);
        }

        private void TurnOffTheLight()
        {
            LR_Light.Instance.LightOff();
            Mid_Light_TCP.Instance.WriteCmd("IDC,CLS,1,\r\n", 0);
            Mid_Light_TCP.Instance.WriteCmd("IDC,CLS,2,\r\n", 0);
        }
        #endregion
        #region Run Vision Tools
        List<bool> ResultVision = new List<bool>();
        Mat mat = new Mat();
        private Mat CaptureImage()
        {
            //Capture 
            HikCam cam = Globals.ListCams.Find(x => x.CameraName == Globals.SettingParameter.CCD_Name);
            if (cam != null && cam.isConnected)
            {
                Bitmap bitmap = cam.CaptureImage();
                mat = bitmap.ToMat();                
                bitmap.Dispose();
            }
            return mat;
        }
        private void RunVisionTool1(Mat Image)
        {
            try
            {
                ProcessTool1 = false;
                ResultVision.Clear();
                //Choose Tools
                var tools = LoadModel.currentModel.visionModels[0].tools;

                VisionTools.Process process = Globals.ListProcess.Find(x => x.Name == LoadModel.currentModel.visionModels[0].VisionModelName);
                //Send Image to Tools
                if (mat != null)
                {
                    VisionTools.ToolBase tool = process.Tools.Find(x => x.Name == "Image Load 0");
                    ImageLoad imageLoad = tool as ImageLoad;
                    imageLoad.InImage = new VisionTools.Image(Image);
                }

                //Run tools
                Globals.ListProcessCreatorUI[0].Process = process;
                Globals.ListProcessCreatorUI[0].processCreatorUI1.RunTool();

                ResultVision.Add(process.ProcessResult);

                //Lấy kết quả của các Tool
                foreach (var a in tools)
                {
                    if(a.VisionToolName.Contains("Blob"))
                    {
                        FindBlob tool = process.Tools.Find(x => x.Name == a.VisionToolName) as FindBlob;
                        if (tool != null)
                        {

                            //null == fasle
                            //if (tool.FirstBlob == null)
                            //{
                            //    tool.RegionGraphicColor = Color.Red;
                            //    ResultVision.Add(false);
                            //    PageEngineering.Instance.AddLogRichtextBox($"Vision Tool {a.VisionToolName} Fail", Color.Green);
                            //    break;
                            //}
                            //else
                            //{
                            //    tool.RegionGraphicColor = Color.Green;
                            //}
                        }
                    }
                }                

                OutputImage outputImage = process.Tools.Find(x => x.Name == "OutputImage 0") as OutputImage;

                if (outputImage != null)
                {
                    if (outputImage.ImageList[0].Image != null)
                    {
                        PageVision.Instance.displayViewInteract1.Image = outputImage.ImageList[0].Image;
                        PageVision.Instance.displayViewInteract1.NotifyDrawing = Globals.ListProcessCreatorUI[0].processCreatorUI1.GetOutputGraphic("OutputImage 0");
                        PageVision.Instance.displayViewInteract1.FitToWindow();
                        //Vẽ text lên ảnh
                        Drawing item = delegate (Graphics gdi)
                        {
                            gdi.DrawString("PASS", new Font("Time New Roman", 200, FontStyle.Bold), new SolidBrush(Color.Red), 100, 100);
                        };
                        PageVision.Instance.displayViewInteract1.NotifyDrawing.Add(item);
                    }
                }
                ProcessTool1 = true;
            }
            catch(Exception ex)
            {
                result = ShowAlarm(XAlarmId.CCD_ERROR, ex.ToString());
            }
        }

        bool ProcessTool1 = false;
        bool ProcessTool2 = false;
        private void RunVisionTool2(Mat Image)
        {
            try
            {
                ProcessTool2 = false;

                var tools = LoadModel.currentModel.visionModels[1].tools;
                //Choose Tools
                VisionTools.Process process = Globals.ListProcess.Find(x => x.Name == LoadModel.currentModel.visionModels[1].VisionModelName);
                //Send Image to Tools
                if (mat != null)
                {
                    VisionTools.ToolBase tool = process.Tools.Find(x => x.Name == "Image Load 1");
                    ImageLoad imageLoad = tool as ImageLoad;
                    imageLoad.InImage = new VisionTools.Image(Image);                                     
                }
                else
                {
                    MessageBox.Show("The input image is null");
                }

                //Run tools
                Globals.ListProcessCreatorUI[1].Process = process;
                Globals.ListProcessCreatorUI[1].processCreatorUI1.RunTool();
                ResultVision.Add(process.ProcessResult);
                foreach (var tool in tools)
                {
                   FindBlob findBlob = process.Tools.Find(x=>x.Name == tool.VisionToolName) as FindBlob;
                    if (findBlob != null)
                    {
                        ////null == fasle
                        //if (findBlob.FirstBlob == null)
                        //{
                        //    findBlob.RegionGraphicColor = Color.Red;
                        //    ResultVision.Add(false);
                        //    PageEngineering.Instance.AddLogRichtextBox($"VisionTool {tool.VisionToolName} Fail", Color.Green);
                        //}
                        //else
                        //{
                        //    findBlob.RegionGraphicColor = Color.Green;
                        //    //ResultVision.Add(true);
                        //}
                    }
                }

                //Lấy kết quả của các Tool
                //FindBlob findblod0 = process.Tools.Find(x => x.Name == "Find Blob 0") as FindBlob;
                //if (findblod0 != null)
                //{
                //    //null == fasle
                //    if (findblod0.FirstBlob == null)
                //    {
                //        findblod0.RegionGraphicColor = Color.Red;
                //        ResultVision.Add(false);
                //    }
                //    else
                //    {
                //        findblod0.RegionGraphicColor = Color.Green;
                //        //ResultVision.Add(true);
                //    }
                //}

                OutputImage outputImage = process.Tools.Find(x => x.Name == "OutputImage 0") as OutputImage;

                if (outputImage != null)
                {
                    if (outputImage.ImageList[0].Image != null)
                    {
                        PageVision.Instance.displayViewInteract2.Image = outputImage.ImageList[0].Image;
                        PageVision.Instance.displayViewInteract2.NotifyDrawing = Globals.ListProcessCreatorUI[1].processCreatorUI1.GetOutputGraphic("OutputImage 0");
                        PageVision.Instance.displayViewInteract2.FitToWindow();
                    }
                }
                ProcessTool2 = true;
            }
            catch (Exception ex)
            {
                result = ShowAlarm(XAlarmId.CCD_ERROR, ex.ToString());
            }
        }
        #endregion
        private void WriteTaskLog(string message)
        {
            string path = Globals.Dir_Task70 + "MainLog_" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            string str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " => " + message;
            CsvServer.Instance.WriteLine(path, str);
        }
    }   

}
