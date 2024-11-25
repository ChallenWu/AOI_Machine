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
using AutoStudio.Core.Views.CustomControls;
using Models;
using System.Xml.Linq;
using AutoStudio.Forms;

namespace Demo
{
    class Globals
    {
        public static int indexModel = 0;
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

        public static SettingOption Setting_Model1 = new SettingOption();
        public static SettingOption Setting_Model2 = new SettingOption();
        public static SettingOption Setting_Model3 = new SettingOption();        

        public static SettingICT SettingICT;
        public static SettingICT SettingICT_PD = new SettingICT();  
        public static SettingICT SettingICT_DOE = new SettingICT();

        public static SettingICT Parameter_mode1 = new SettingICT();
        public static SettingICT Parameter_mode2 = new SettingICT();
        public static SettingICT Parameter_mode3 = new SettingICT();

        public static event EventHandler AutoRunChangeSettingHandle;

        public static event EventHandler SetStateTop;

        public static event EventHandler OnPauseActive;

        public static event EventHandler OnStopActive;

        public static event EventHandler FlowStateEvent;

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

        public static void CreateModelFolder()
        {
            Setting_Model1 = new SettingOption();
            Parameter_mode1 = new SettingICT();


            XSettingManager.Instance.SettingMap.Clear();
            //XSettingManager.Instance.sett.Clear();
            #region Loading configuration information
            //Tạo đường link cho các path setting
            //Globals.SettingOption_PD.SetPathAndRoot("D:\\AOI_Config\\Setting\\SettingOption_PD.xml", "Setting", Globals.Dir_Record_BackupConfig + "SettingOption");
            //Globals.SettingICT_PD.SetPathAndRoot("D:\\AOI_Config\\Setting\\SettingICT.xml", "Setting", Globals.Dir_Record_BackupConfig + "SettingOption");

            Globals.Setting_Model1.SetPathAndRoot($"D:\\AOI_Config\\Setting\\{LoadModel.currentModel.modelName}\\SettingOption_Model.xml", "Setting", Globals.Dir_Record_BackupConfig + $"\\{LoadModel.currentModel.modelName}\\" + "SettingOption");
            Globals.Parameter_mode1.SetPathAndRoot($"D:\\AOI_Config\\Setting\\{LoadModel.currentModel.modelName}\\Paramter_Model.xml", "Setting", Globals.Dir_Record_BackupConfig + $"\\{LoadModel.currentModel.modelName}\\" + "SettingOption");
            #endregion

            #region Bind Setting
            // Bind các setting theo id được đặt trong file enum
            // XSettingManager.Instance.BindSetting((int)SettingId.Option_PD, SettingOption_PD, SettingId.Option_PD.ToString());
            //XSettingManager.Instance.BindSetting((int)SettingId.ICT, SettingICT_PD, SettingICT_PD.ToString());

            var lst = ModelStore.GetModelInfoList();

            foreach (var model in lst)
            {
                if (model.Name == LoadModel.currentModel.modelName)
                {
                    indexModel = model.Index;
                }
            }
            //Console.WriteLine($"id Option {(int)SettingId.Option_model1 +indexModel}");
            //Console.WriteLine($"id Parameter {(int)SettingId.Paramter_Model + indexModel}");

            XSettingManager.Instance.BindSetting((int)SettingId.Option_model1 + indexModel, Setting_Model1, SettingId.Option_model1.ToString());
            XSettingManager.Instance.BindSetting((int)SettingId.Paramter_Model + indexModel, Parameter_mode1, SettingId.Paramter_Model.ToString());

            #endregion
            //Load setting vào instance
            XSettingManager.Instance.LoadSettings();
        }        
        public static void BindDevice()
        {
            CreateModelFolder();
            //Add setting to machine or mode DOE
            //Chạy chế độ DOE

            SettingICT = Parameter_mode1;
            SettingOption = Setting_Model1;


           // ChangeParaterForEachModel();




            //if (Globals.IsDOE)
            //    AddDOECompensationXYR();
            //else
            //    AddPDCompensationXYR();

            //SettingICT = SettingICT_PD;
            //SettingOption = SettingOption_PD;



            //Đổi ngôn ngữ
            //MultiLanguage.ChangeLanguage(Globals.SettingOption.语言, true);
            //Globals.SettingOption.语言 = LanguageType.English;
            //Bind dữ liệu
            GlobalsAutoConfig.BindDevice();
            //Update thông số tốc độ Axis
            //XAxis.updateSpeed += motorSpeed.UpdateSpeedParameter;
            #region Gan task
            //Tạo các station id
            XController.Instance.StationId = -1;
            XStationManager.Instance.BindStation((int)StationId.Scanner, StationId.Scanner.ToString());
            //Gán task cho tứng station
            XStationManager.Instance.FindStationById((int)StationId.Scanner).BindTask((int)TaskId.Task70_ScannerBox);
            #endregion

            #region Gán tọa độ
            //Tạo đường dẫn file tọa độ
            XPositionManager.Instance.SetPositionXmlPathAndRoot("D:\\AOI_Config\\Position\\", "Position.xml", "PositionName.xml", "Position", Dir_BackUpConfigPosition);
            //Gán tọa độ cho từng task
            XPositionManager.Instance.BindPositionTableByTaskId((int)TaskId.Task70_ScannerBox);
            XPositionManager.Instance.LoadPositionSet();

            //XModelManager.Instance.SetModelXmlPathAndRoot("D:\\AOI_Config\\Position\\", "Models.xml", "models", Dir_BackUpConfigPosition, 
            //(int)TaskId.Task70_ScannerBox, "Position.xml", "PositionName.xml", "Position", Dir_BackUpConfigPosition);
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
            SettingOption = SettingOption_PD;
        }
        public static bool IsDOE = false;

        public static void AddDOECompensationXYR()
        {
            SettingOption = SettingOption_DOE;
        }

        public static void ChangeParaterForEachModel()
        {
            
            SettingOption = Setting_Model1;
            SettingICT = Parameter_mode1;
            Console.WriteLine(SettingICT.PLC_IP);
            return;
        }
    }
}
