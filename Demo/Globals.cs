using Demo.Id;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCore;
using HB_IWatch;
using Demo.Page;
using Demo.Setting;

namespace Demo
{
    class Globals
    {
        public enum MachineRunMode
        {
            NormalRun,
            Assemble_Dry_Run,
            Conveyor_Dry_Run,
            Entire_Machine_Dry_Run,
            Single_Reinspection
        }

        public enum CPKGRRMode
        {
            AssembleCPK = 0,
            RecheckCPK,
            RecheckGRR,
        }
        public enum SettingParaMode
        {
            _PD = 0,
            _DOE = 1,
        }
        //Tạo ra các setting cần thiết trong folder Settings

        //Setting chạy runing lúc thường và lúc chạy DOE
        public static SettingOption SettingOption;
        public static SettingOption SettingOption_PD = new SettingOption();
        public static SettingOption SettingOption_DOE = new SettingOption();

        public static SettingCalibration SettingCalibration;
        public static SettingCalibration SettingCalibration_PD = new SettingCalibration();
        public static SettingCalibration SettingCalibration_DOE = new SettingCalibration();

        public static SettingNozzlesCompensation SettingNozzlesCompensation;
        public static SettingNozzlesCompensation SettingNozzlesCompensation_PD = new SettingNozzlesCompensation();
        public static SettingNozzlesCompensation SettingNozzlesCompensation_DOE = new SettingNozzlesCompensation();

        public static SettingICT SettingICT;
        public static SettingICT SettingICT_PD = new SettingICT();  
        public static SettingICT SettingICT_DOE = new SettingICT();

        public static event EventHandler AutoRunChangeSettingHandle;

        public static event EventHandler SetStateTop;

        public static event EventHandler OnPauseActive;

        public static event EventHandler OnStopActive;

        public static event EventHandler FlowStateEvent;

        //Đường dẫn thư mục chứa config


        //public delegate void SendInfoToGridViewShow(Dictionary<Enum_UpData, string> keys, string mesState);

        //public static SendInfoToGridViewShow SendInfoToGridViewShowDelegate;

        public delegate void ClearGridViewData();

        public static ClearGridViewData ClearGridViewDataDelegate;

        public delegate void WaitTaskStop();
        public static WaitTaskStop OnWaitTaskStop;        
        
        public static void SetTopState()
        {
            if (SetStateTop != null)
                SetStateTop(null, null);
        }

        public static void SetMainDlgPause()
        {
            if (OnPauseActive != null)
                OnPauseActive(null, null);
        }

        public static void SetMainDlgStop()
        {
            if (OnStopActive != null)
                OnStopActive(null, null);
        }

        public static void FlashFlowState()
        {
            if (FlowStateEvent != null)
                FlowStateEvent(null, null);
        }

        /// <summary>
        /// 1: Stop state 0: Production state
        /// </summary>
        public static int StopStatus = 1;

        public static MotorSpeed motorSpeed = new MotorSpeed();
        public static void BindDevice()
        {
            #region Loading configuration information
            //Tạo đường link cho các path setting
            Globals.SettingOption_PD.SetPathAndRoot("D:\\AOI_Config\\Setting\\SettingOption_PD.xml", "Setting", Globals.Dir_Record_BackupConfig + "SettingOption");
            Globals.SettingCalibration_PD.SetPathAndRoot("D:\\AOI_Config\\Setting\\SettingCalibration_PD.xml", "Setting", Globals.Dir_Record_BackupConfig + "SettingLeftAssemble");
            Globals.SettingNozzlesCompensation_PD.SetPathAndRoot("D:\\AOI_Config\\Setting\\SettingOption_PD.xml", "Setting", Globals.Dir_Record_BackupConfig + "SettingOption");
            
            Globals.SettingICT_PD.SetPathAndRoot("D:\\AOI_Config\\Setting\\SettingICT.xml", "Setting", Globals.Dir_Record_BackupConfig + "SettingOption");
            #endregion

            #region Bind Setting
            // Đọc file config lên setting trong phần mềm
            // Add setting vào từng ID  khai báo trong enum
            XSettingManager.Instance.BindSetting((int)SettingId.选项_PD, SettingOption_PD, SettingId.选项_PD.ToString());
            XSettingManager.Instance.BindSetting((int)SettingId.标定参数_PD, SettingCalibration_PD, SettingCalibration_PD.ToString());
            XSettingManager.Instance.BindSetting((int)SettingId.标定参数_PD,SettingNozzlesCompensation_PD, SettingId.吸头补偿参数_PD.ToString());
            
            XSettingManager.Instance.BindSetting((int)SettingId.ICT,SettingICT_PD, SettingICT_PD.ToString());
            #endregion
            //Load setting vào instance
            XSettingManager.Instance.LoadSettings();
            //Add setting to machine or mode DOE
            if (true)
            {
                SettingICT = SettingICT_PD;
                SettingOption = SettingOption_PD;
            }
            //Đổi ngôn ngữ
            MultiLanguage.ChangeLanguage(Globals.SettingOption.语言, true);
            Globals.SettingOption.语言 = LanguageType.English;
            //Bind dữ liệu
            GlobalsAutoConfig.BindDevice();
            //Update thông số tốc độ Axis
            //XAxis.updateSpeed += motorSpeed.UpdateSpeedParameter;
            #region Gan task
            //Tạo các station id
            XController.Instance.StationId = -1;
            XStationManager.Instance.BindStation((int)StationId.Scanner, StationId.Scanner.ToString());
            //Gán công việc cho các station id
            XStationManager.Instance.FindStationById((int)StationId.Scanner).BindTask((int)TaskId.Task70_ScannerBox);
            #endregion

            #region Gán tọa độ
            //Tạo đường dẫn file tọa độ
            XPositionManager.Instance.SetPositionXmlPathAndRoot("D:\\AOI_Config\\Position\\", "Position.xml", "PositionName.xml", "Position", Dir_BackUpConfigPosition);
            //Gán tọa độ cho từng task
            XPositionManager.Instance.BindPositionTableByTaskId((int)TaskId.Task70_ScannerBox);
            XPositionManager.Instance.LoadPositionSet();
            #endregion
            //Cau hinh tin hieu
            #region binding EMG,Stop,Door signal
            //XMachine.Instance.AddEStopDi((int)DiId.)
            //XMachine.Instance.DoorEnabled = Globals.SettingOption.IsOpensafeDoor();
            //XMachine.Instance.SafeDoorEStop = Globals.SettingOption.是否开启安全门急停复位;
            //XMachine.Instance.AddDoorDi((int)DiId.主设备门禁1, false);
            //XMachine.Instance.AddDoorDi((int)DiId.主设备门禁3, false);

            #endregion
        }

        //Đường dẫn file config
        public const string Dir_Record = "E:\\AOI_Record\\";
        public const string Dir_Record_CoverLog = Dir_Record + "MainLog\\";
        public const string Dir_Record_PDCALog = Dir_Record + "PDCALog\\";
        public const string Dir_Record_MESLog = Dir_Record + "MESLog\\";
        public const string Dir_Task70 = Dir_Record + "Dir_Task70\\";
        //Link backup
        public const string Dir_Record_BackupConfig = "E:\\BackupConfig\\Setting\\";
        public const string Dir_BackUpConfigPosition = "E:\\BackUpConfig\\Position\\";
        //Đọc config
        public const string Dir_GTS_Config = "D:\\AOI_Config\\GTS\\";
        public const string Dir_UserAccount = "D:\\AOI_Config\\UserAccount\\";


        public static void CreateAllDirectory()
        {
            CreateDirectory(Dir_Record);
            CreateDirectory(Dir_Record_CoverLog);
            CreateDirectory(Dir_Task70);
            CreateDirectory(Dir_Record_BackupConfig);
            CreateDirectory(Dir_Record_PDCALog);
            CreateDirectory(Dir_Record_MESLog);
        }

        private static void CreateDirectory(string dir)
        {
            if(Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }    
        }

        public static void InitDevice()
        {
            #region Initialize the board
            try
            {
                //int ret = XDevice.Instance.FindCardById((int)CardId.主设备1).Initial(Globals.SettingOption.ACS_IP, Offline);//xm1119

                //if (ret != 0)
                //{
                //    BzMessagebox.Show("ACS" + MultiLanguage.GetMessage("控制器初始化失败") + "\r\n");
                //    return;
                //}

                //InitGTS();
                //InitAxis();
            }
            catch (Exception exc)
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("Unable to connect to the controller, please check whether the network connection is normal！") + "\r\n");
                return;
            }
            #endregion
            //设置配置文件目录
            InitGobalRemeoSn();
        }
        public static Dictionary<int, string> GobalRemeoSn = new Dictionary<int, string>();
        public static void InitGobalRemeoSn()
        {
            for (int i = 1; i <= 10; i++)
            {
                GobalRemeoSn.Add(i, "N/A");
            }
        }

        public static bool Offline = false; //@sjx
        public static bool InitGTS()
        {
            int ret;
            ret = XDevice.Instance.FindCardById((int)CardId.主设备2).InitialGTS(6, 5, Globals.Dir_GTS_Config + CardId.主设备2.ToString() + ".cfg"
                , Globals.Dir_GTS_Config + CardId.主设备2.ToString() + "ext.cfg", Offline);

            if (ret != 0)
            {
                string str = MultiLanguage.GetMessage("主设备2", "控制器初始化失败", "\r\n");
                BzMessagebox.Show(str);
                return false;
            }

            ret = XDevice.Instance.FindCardById((int)CardId.供料机1).InitialGTS(4, 1, Globals.Dir_GTS_Config + CardId.供料机1.ToString() + ".cfg"
                , Globals.Dir_GTS_Config + CardId.供料机1.ToString() + "ext.cfg", Offline);

            if (ret != 0)
            {
                string str = MultiLanguage.GetMessage("供料机1", "控制器初始化失败", "\r\n");
                BzMessagebox.Show(str);
                return false;
            }

            ret = XDevice.Instance.FindCardById((int)CardId.供料机2).InitialGTS(4, 1, Globals.Dir_GTS_Config + CardId.供料机2.ToString() + ".cfg"
                , Globals.Dir_GTS_Config + CardId.供料机2.ToString() + "ext.cfg", Offline);
            if (ret != 0)
            {
                string str = MultiLanguage.GetMessage("供料机2", "控制器初始化失败", "\r\n");
                BzMessagebox.Show(str);
                return false;
            }

            ret = XDevice.Instance.FindCardById((int)CardId.主设备3).InitialGTS(4, 1, Globals.Dir_GTS_Config + CardId.主设备3.ToString() + ".cfg"
                , "", Offline);

            if (ret != 0)
            {
                string str = MultiLanguage.GetMessage("主设备3", "控制器初始化失败", "\r\n");
                BzMessagebox.Show(str);
                return false;
            }

            return true;
        }

        private static void InitAxis()
        {
            XDevice.Instance.FindAxisById((int)AxisId.右贴装R1轴).InitAxis((short)HomeMode.Limit, (short)HomeDir.Negative, 50, 10, 0.02, 30, 30);
            XDevice.Instance.FindAxisById((int)AxisId.右贴装R2轴).InitAxis((short)HomeMode.Limit, (short)HomeDir.Negative, 50, 10, 0.02, 30, 30);
            XDevice.Instance.FindAxisById((int)AxisId.右贴装R3轴).InitAxis((short)HomeMode.Limit, (short)HomeDir.Negative, 50, 10, 0.02, 30, 30);
            XDevice.Instance.FindAxisById((int)AxisId.左贴装R1轴).InitAxis((short)HomeMode.Limit, (short)HomeDir.Negative, 50, 10, 0.02, 30, 30);
            XDevice.Instance.FindAxisById((int)AxisId.左贴装R2轴).InitAxis((short)HomeMode.Limit, (short)HomeDir.Negative, 50, 10, 0.02, 30, 30);
            XDevice.Instance.FindAxisById((int)AxisId.左贴装R3轴).InitAxis((short)HomeMode.Limit, (short)HomeDir.Negative, 50, 10, 0.02, 30, 30);
            XDevice.Instance.FindAxisById((int)AxisId.左供料右上Z轴).InitAxis((short)HomeMode.Limit_Home, (short)HomeDir.Positive, 20, 10, 0.1, 50, 0, (int)DoId.左供料机右上轴刹车);
            XDevice.Instance.FindAxisById((int)AxisId.左供料右下Z轴).InitAxis((short)HomeMode.Limit_Home, (short)HomeDir.Negative, 20, 10, 0.1, 50, 0, (int)DoId.左供料机右下轴刹车);
            XDevice.Instance.FindAxisById((int)AxisId.左供料左上Z轴).InitAxis((short)HomeMode.Limit_Home, (short)HomeDir.Positive, 20, 10, 0.1, 50, 0, (int)DoId.左供料机左上轴刹车);
            XDevice.Instance.FindAxisById((int)AxisId.左供料左下Z轴).InitAxis((short)HomeMode.Limit_Home, (short)HomeDir.Negative, 20, 10, 0.1, 50, 0, (int)DoId.左供料机左下轴刹车);
            XDevice.Instance.FindAxisById((int)AxisId.右供料右上Z轴).InitAxis((short)HomeMode.Limit_Home, (short)HomeDir.Positive, 20, 10, 0.1, 50, 0, (int)DoId.右供料机右上轴刹车);
            XDevice.Instance.FindAxisById((int)AxisId.右供料右下Z轴).InitAxis((short)HomeMode.Limit_Home, (short)HomeDir.Negative, 20, 10, 0.1, 50, 0, (int)DoId.右供料机右下轴刹车);
            XDevice.Instance.FindAxisById((int)AxisId.右供料左上Z轴).InitAxis((short)HomeMode.Limit_Home, (short)HomeDir.Positive, 20, 10, 0.1, 50, 0, (int)DoId.右供料机左上轴刹车);
            XDevice.Instance.FindAxisById((int)AxisId.右供料左下Z轴).InitAxis((short)HomeMode.Limit_Home, (short)HomeDir.Negative, 20, 10, 0.1, 50, 0, (int)DoId.右供料机左下轴刹车);
            XDevice.Instance.FindAxisById((int)AxisId.左预取料1Y轴).InitAxis((short)HomeMode.Home, (short)HomeDir.Negative, 50, 10, 0.1, 50, 0);
            XDevice.Instance.FindAxisById((int)AxisId.左预取料2Y轴).InitAxis((short)HomeMode.Home, (short)HomeDir.Negative, 50, 10, 0.1, 50, 0);
            XDevice.Instance.FindAxisById((int)AxisId.右预取料1Y轴).InitAxis((short)HomeMode.Home, (short)HomeDir.Negative, 50, 10, 0.1, 50, 0);
            XDevice.Instance.FindAxisById((int)AxisId.右预取料2Y轴).InitAxis((short)HomeMode.Home, (short)HomeDir.Negative, 50, 10, 0.1, 50, 0);

            //int ret;

            //ret = XDevice.Instance.FindAxisById((int)AxisId.右贴装R1轴).SetSoftLimt(359, -359);
            //ret = XDevice.Instance.FindAxisById((int)AxisId.右贴装R3轴).SetSoftLimt(359, -359);
            //ret = XDevice.Instance.FindAxisById((int)AxisId.左贴装R1轴).SetSoftLimt(359, -359);
            //ret = XDevice.Instance.FindAxisById((int)AxisId.左贴装R2轴).SetSoftLimt(359, -359);
            //ret = XDevice.Instance.FindAxisById((int)AxisId.左贴装R3轴).SetSoftLimt(359, -359);

        }

        public static MachineRunMode RUNMODE;
        //Ghi log main
        public static void WriteCoverCTLog(string message)
        {
            string path = Dir_Record_CoverLog + "//MainLog" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            string str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " => " + message;
            CsvServer.Instance.WriteLine(path, str);
        }
        public static void AddPDCompensationXYR()
        {
            //Globals.SettingCalibration = Globals.SettingCalibration_PD;
            SettingOption = SettingOption_PD;
            Globals.SettingCalibration = Globals.SettingCalibration_PD;
        }
        public static bool IsDOEPCB = false;

        public static void AddDOECompensationXYR()
        {
            SettingOption = SettingOption_PD;
            Globals.SettingCalibration = Globals.SettingCalibration_PD;
            //SettingOption = SettingOption_DOE;
        }
    }
}
