using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;

namespace BoTech
{
    public sealed class AudioSystem
    {

        public static string[] Errors = new string[] { "MES Error", "Safety Error", "Scanning Error", "Vision Error", "Cylinder Error", "Motion Error", "Sensor Error", "PDI Error" };

        #region language
        //public static GeneralInfo.MachineLanguage_CN CurLanguage;
        //public static string[] PageName = new string[] {"HomePage","AlarmPage", "ConfigPage", "DataPage", "VisionPage","SettingPage","SensorPage","VisionPageToDetails" };
        public enum PageName
        {
            HomePage = 1,
            AlarmPage,
            ConfigPage,
            DataPage,
            VisionPage,
            SettingPage = 6,
        }

        public static DataSet DSet = new DataSet();//用于存语言文件；
        //public static string StrLanguageFile = FilePath.mFilePath.BZ_NewPar + "SystemLanguage.xlsx";
        #endregion

        #region LAD

        //public static LadOper[] ladOpers = new LadOper[] { new LadOper(), new LadOper(), new LadOper() };
        //public static LadOper ladRead = new LadOper();


        #endregion

        #region CCDRepeatability
        //Plandown状态下的功能选择项
        public static LADstateType cPlandownstateType = LADstateType.Normal;
        //CCDRepeatability测试次数
        public static int CCDwork_Times = 0;
        public static string[] CCD_RepeatabilityBack = new string[] { "", "", "" };
        public static double[] CCD1静态测试差值 = new double[2];
        public static double[] CCD2静态测试差值 = new double[2];
        public static double[] CCD3静态测试差值 = new double[2];
        public static double[] CCD1动态测试差值 = new double[2];
        public static double[] CCD2动态测试差值 = new double[2];
        public static double[] CCD3动态测试差值 = new double[2];
        //存储相机返回数据
        public static List<double> CCD1动静态X轴 = new List<double>();
        public static List<double> CCD2动静态X轴 = new List<double>();
        public static List<double> CCD3动静态X轴 = new List<double>();
        public static List<double> CCD1动静态Y轴 = new List<double>();
        public static List<double> CCD2动静态Y轴 = new List<double>();
        public static List<double> CCD3动静态Y轴 = new List<double>();
        public enum LADstateType
        {
            Normal,
            Repeatability,
            LadPD_11,
            DispensingPoseCalibration
        }
        //默认机台CCD动静态测试处于静态测试
        public static CCDRepeatabilityMode cCCDRepeatabilityMode = CCDRepeatabilityMode.CCDUCStatic;
        public enum CCDRepeatabilityMode
        {
            CCDUCStatic = 1,
            CCDStaticUCDynamic = 2,
            CCDDynamicUCStatic = 3
        }

        public struct AlarmCode
        {

            //public string PLCAddressType;
            //public string PLCAddressAll;
            //public string PLCAddress;
            //public string PLCAddress2;
            public string ErrorCode;
            public string ErrorCatrgory;
            public string Severity;
            public string MessageEn;
            //public string MessageCn;
            public string ErrorDetail;
            public string[] DealtMethods;
            public string DealtMethod;
        }
        


        #endregion

        #region/*(PDCA文件设置路径)*/
        public static string PDCA_ImagePath = @"d:\ITKS_PDCA\";
        #endregion

        #region 机台名称，如TI040_1,const string;
        public const string MachineName_TI040_1 = "TI040-1";
        public const string MachineName_BE010_1 = "BE010-1";
        #endregion

        #region 时区

        /*(临时用，后续用数据表配置；)*/
        public const int AlarmToPlcAddress_Pdca = 1203;
        public const int AlarmToPlcAddress_Mes = 1204;
        public const int AlarmToPlcAddress_Hive = 1205;
        public const int ManualChangeHiveToPlc = 1209;//寄存器数值写6，给PLC反馈有账户登录；如果写99则是用于LAD模式，PLC模式写PlannedDown模式；



        public static ManualResetEvent mre = new ManualResetEvent(false);

        public static int HiveStateIndex = 0;
        //10月27改回
        public static bool IsLoginConnectMES = true;//是否通过MES验ID权限；
        //1019要求用本地登录
        //public static bool IsLoginConnectMES = false;//是否通过MES验ID权限；

        public static bool IsMesPassOk = false;//MES过站是否成功；成功传PDCA;
        public static DateTime GetScanTime;//用于第一次计算CT使用；第一片料从扫码开始计时；
        public static int BadgeIDLength = 10;//id 的长度一般为10；
        //public static AccountInfo AI;

        public const int TraySNLength = 24;//料盘SN的长度为24；BE010-1上料的料盘；
        public enum ETimeZone
        {
            昆山,
        }
        public static ETimeZone MachineTimeZone;   // 时区
        public static string MachineTimeZoneFormat;
        #endregion

        #region 昆山图片压缩包
        public static string HiveImageFullPath = "";
        public static string HiveImageFile;
        public static Dictionary<string, string>[] blobs;
        #endregion

        #region pdca/hive/mes 
        public static bool isPDCAOk = true;
        public static bool isMESOk = true;
        public static bool isHiveOk = true;
        #endregion

        #region AudioNeed
        //public static GTK_MESData_Model GTK_MESData_Model = new GTK_MESData_Model();
        public static MachineMessage AudioMachineMessage = new MachineMessage();
        //public static MachineState AudioMachineState;
        public static AlarmCode[] AudioAlarmCodes; //报警代码集合 
        
        public static string ErrorOld = "";
        //public static PlanDownMessage[] AudioPlanDownReason;//PlanDown 集合  
        //public static SensorCom[] AudioSacnSensorCom;
        public static string[] AudioScanBack;
       // public static Dictionary<int, PLCComMessage[]> AudioCommunicationMessages = new Dictionary<int, PLCComMessage[]>();//PLC通讯信息
        //public static PLCComMessage[] AudioComMessagesTem;
        //public static SystemMessage AudioSystemMessage;  //系统参数
        //public static OmronPLC AudioPLC;             //PLC 通讯实体
        public static string[] CameraBackMessage = new string[] { "", "", "" };
        public static Thread[] PLCThrad;
        public static Thread PLCMachineStateThread;
        public static Thread PLCSensorScanThread;
        public static Thread PLCMesThread;

        public static UCMessage UCM = new UCMessage();
        #endregion

        #region AudioSystem

        public static float NewX = 1; //窗体缩放系数X
        public static float NewY = 1; //窗体缩放系数X
        public static string[] MacInfo = new string[21];

        public static string PassWordNow;
        public static string UserNameNow;

        public static Point ChildFrmOffsetPoint; //各子界面在主界面中位置定位点
        public static string sOpenTargetForm; //主界面开启相关界面时，传递窗口名称变量
        public static bool IsSysOnEngineerMode;
        public static bool IsOpenFrmEngineering;
        public static bool IsOpenFrmProduction;
        public static bool IsLoadFrmEngineering;
        public static bool IsOpenFrmPar;
        public static bool IsOpenFrmLogin;

        public static bool LoginFrmEngineeringEnable; //工程界面
        public static bool LoginFrmParEnable; //参数界面
        public static bool LoginFrmParCCDEnable;
        public static bool LoginOutputEnable; //I/O输出界面
        public static bool LoginManualEnable; //机械参数界面
        public static bool LoginMachineParEnable; //机械参数界面
        public static bool LoginNoneParEnable; //机械参数界面

        public static bool IsOK;
        public static long LoadStartTime;

        #endregion

        #region 自定义颜色常量
        public static Color Color_SelectedBtn = Color.FromArgb(175, 218, 150);
        public static Color Color_UnselectedBtn = Color.FromArgb(238, 238, 238);
        public static Color Color_SelectedPauseBtn = Color.FromArgb(132, 193, 251);
        public static Color Color_SelectedEndBtn = Color.FromArgb(228, 146, 138);
        public static Color Color_ErrorRed = Color.FromArgb(236, 93, 87);

        public static Color Color_Run = Color.LightGreen;
        public static Color Color_Pause = Color.Yellow;
        public static Color Color_PauseBtn = Color.LightBlue;
        public static Color Color_Alarm = Color.Red;
        #endregion

        #region 文件保存相关变量及路径

        public static DataTable DTable;//读出语言文件DataTable;

        public static string BZ_DataPath = "E:\\BZ-Data\\";
        public static string BZ_CheckDataPath = "E:\\BZ-Data\\CheckData\\";
        //public static string BZ_CPKDataFilePath = (new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.DirectoryPath + "\\CPKData.dat";
        #endregion

        #region 必须开启的功能
        public static void IsFunctionOpen()
        {

        }

        #endregion

        #region 用户密码
        public struct sPassWord
        {
            public string[] NewGroup;
            public string[] NewUser; //新用户
            public string[] NewPassword; //新密码
            public string[] NewPasswordChecked; //新密码确认
            public int[] NewUserAuthority; //用户权限
        }
        public static sPassWord Login;
        public static void ReDimPassWord()
        {
            Login.NewGroup = new string[21];
            Login.NewUser = new string[21];
            Login.NewPassword = new string[21];
            Login.NewPasswordChecked = new string[21];
            Login.NewUserAuthority = new int[21];
        }

        #endregion

        #region LAD

        public static LadOper[] ladOpers = new LadOper[] { new LadOper(), new LadOper(), new LadOper() };
        public static LadOper ladRead = new LadOper();
        #endregion

        #region"自定义变量"
        //public static ReadOptrisActuator Serias1;
        #endregion

        #region"自定义方法"


        /// <summary>
        /// 判断是否处于Engineer状态
        /// </summary>
        public static bool Engineer = false;
        /// <summary>
        /// 第一次查询穴位是否解绑
        /// </summary>
        public static bool FirQuery;
        /// <summary>
        /// 换料盘时长
        /// </summary>
        public static double ChangeTray_CT;
        /// <summary>
        /// 暂定
        /// </summary>
        public static double Unit_set_CT;
        public static string Operator { get; set; } = "";
        #endregion
        public class MachineMessage
        {            
            public string ProType = "E-SKU";
            public string Vendor { get; set; } = "BZ";
            public string Main_SW_version = "BZ_2.1.0.1_230324_POR";
            public string Vision_SW_version = "1";
            public string Laser_SW_version = "0";
            public string Robot_SW_version = "1";
            public string SW_release_data = "230324";
            /// <summary>
            /// SW type
            /// POR/BKP/RTF/VAL
            /// POR:SW for POR machine
            /// BKP:SW for backup machine
            /// RTF:SW for retrofit machine
            /// VAL:SW version under validation
            /// </summary>
            public string SW_Type = "POR";

            public string SW_version { get; set; } = "BZ_2.1.0.1_230324_POR";
            public string MS_Hash = "";
            public string VS_Hash = "";
            public string CCDParh = "";
            public string Site { get; set; } = "Fukang";
            public string Line { get; set; } = "Line 1";
            public string Station { get; set; } = "AOI";
            public string MachineNo { get; set; } = "001";
            public string language { get; set; }
            public int CurrentLanugaeIndex { get; set; }


            public string Main_SW_Path { get; set; }
            public bool IsRemote { get; set; }

            public PDCAMessage PDCANeed = new PDCAMessage();

            public PCMessage PC { get; set; } = new PCMessage();
            public class PDCAMessage
            {
                public string LocalIP { get; set; } = "169.254.1.12";
                public string FilePath { get; set; } = "";
                public string UserName { get; set; } = "";
                public string Password { get; set; } = "";

            }

            public CCDMessage[] CCD { get; set; } = new CCDMessage[] { new CCDMessage() };

            public PLCMessage PLC { get; set; } = new PLCMessage();
            public DriverMessage Driver { get; set; } = new DriverMessage();

            public MesNeed MES { get; set; } = new MesNeed();

            public class MesNeed
            {
                public string TR_NO { get; set; }
                /// <summary>
                /// 旧料盘；
                /// </summary>
                public string TR_NO_Old { get; set; }
                public string empNo { get; set; }
                public string terminalName { get; set; }
                public string collectType { get; set; }
                public string workOrder { get; set; }


            }
            public class PCMessage
            {
                public string CPU { get; set; } = "i7-7700";
                public string[] HardDIsk { get; set; } = new string[] { "8T" };
            }
            public class CCDMessage
            {
                public string TYPE { get; set; } = "Mono";
                public string Resolution { get; set; }
            }
            public class PLCMessage
            {
                public string Conveyor { get; set; } = "";
                public string Rob_Arm { get; set; } = "INOVANCE";
            }
            public class DriverMessage
            {
                public string Dirver { get; set; }
            }


        }
    }

}
