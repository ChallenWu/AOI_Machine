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
using OpenCvSharp;
using VisionTools;
using System.Threading;

namespace Demo
{
    class Globals
    {
        public static int indexModel = 0;
        public enum MachineRunMode
        {
            NormalRun,
            DryRun,
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
        public static SettingMachine SettingOption;
        public static SettingMachine SettingOption_PD = new SettingMachine();
        public static SettingMachine SettingOption_DOE = new SettingMachine();

        public static SettingMachine Setting_Model = new SettingMachine();     

        public static SettingParameter SettingParameter;
        public static SettingParameter SettingICT_PD = new SettingParameter();  
        public static SettingParameter SettingICT_DOE = new SettingParameter();

        public static SettingParameter Parameter_Model = new SettingParameter();
        public static List<Edit_Vision> ListProcessCreatorUI = new List<Edit_Vision> ();
        public static List<Process> ListProcess = new List<Process> ();

        public static SettingCode SettingSerialNumber = new SettingCode();

        public static bool isDryrun = false;

        public static event EventHandler AutoRunChangeSettingHandle;

        public static event EventHandler SetStateTop;

        public static event EventHandler OnPauseActive;

        public static event EventHandler OnStopActive;

        public static event EventHandler FlowStateEvent;

        public delegate void ClearGridViewData();

        public static ClearGridViewData ClearGridViewDataDelegate;

        public delegate void WaitTaskStop();
        public static WaitTaskStop OnWaitTaskStop;   
        
        public static void AddProcessCreatorUI()
        {
            ListProcessCreatorUI.Add(new Edit_Vision()); 
            ListProcessCreatorUI.Add(new Edit_Vision());
            for (int i = 0; i < ListProcessCreatorUI.Count; i++)
            {
                ListProcessCreatorUI[i].Process = ListProcess[i];
                ListProcessCreatorUI[i].processCreatorUI1.RunTool();
            }
        }


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

        public static bool OpenDebugForm = false;
        public static void CreateModelFolder()
        {
            Setting_Model = new SettingMachine();
            Parameter_Model = new SettingParameter();
            SettingSerialNumber = new SettingCode();

            XSettingManager.Instance.SettingMap.Clear();
            //XSettingManager.Instance.sett.Clear();
            #region Loading configuration information
            //Tạo đường link cho các path setting
            var backupConfig = Globals.Dir_Record_BackupConfig + $"\\{LoadModel.currentModel.modelName}\\" + "SettingOption";
            Globals.Setting_Model.SetPathAndRoot($"D:\\AOI_Config\\Setting\\{LoadModel.currentModel.modelName}\\SettingOption_Model.xml", "Setting", backupConfig);
            Globals.Parameter_Model.SetPathAndRoot($"D:\\AOI_Config\\Setting\\{LoadModel.currentModel.modelName}\\Paramter_Model.xml", "Setting", backupConfig);
            Globals.SettingSerialNumber.SetPathAndRoot($"D:\\AOI_Config\\Setting\\{LoadModel.currentModel.modelName}\\SerialNumber_Model.xml", "Setting", backupConfig);


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

            XSettingManager.Instance.BindSetting((int)SettingId.Option_Model + indexModel, Setting_Model, SettingId.Option_Model.ToString());
            XSettingManager.Instance.BindSetting((int)SettingId.Paramter_Model + indexModel, Parameter_Model, SettingId.Paramter_Model.ToString());
           XSettingManager.Instance.BindSetting((int)SettingId.SerialNumber_Model + indexModel, SettingSerialNumber, SettingId.SerialNumber_Model.ToString());

            #endregion
            //Load setting vào instance
            XSettingManager.Instance.LoadSettings();
        }        
        public static void BindDevice()
        {
            CreateModelFolder();
            //Add setting to machine or mode DOE
            //Chạy chế độ DOE


            SettingParameter = Parameter_Model;
            SettingOption = Setting_Model;           

            //if (Globals.IsDOE)
            //    AddDOECompensationXYR();
            //else
            //    AddPDCompensationXYR();

            //SettingParameter = SettingICT_PD;
            //SettingOption = SettingOption_PD;



            //Đổi ngôn ngữ
            MultiLanguage.ChangeLanguage(Globals.SettingOption.LanguageType, true);
            Globals.SettingOption.LanguageType = LanguageType.English;
            //Gán dữ liệu vào chương trình chạy
            GlobalsAutoConfig.BindDevice();
            //Update thông số tốc độ Axis
            //XAxis.updateSpeed += motorSpeed.UpdateSpeedParameter;

            //Tạo các station id
            XController.Instance.StationId = -1;
            XStationManager.Instance.BindStation((int)StationId.Scanner, StationId.Scanner.ToString());
            //Gán task cho từng station
            //Gán task70 vào station 4
            //1 station có thể có nhiều task, báo trạng thái của state
            XStationManager.Instance.FindStationById((int)StationId.Scanner).BindTask((int)TaskId.Task70_ScannerBox);


            //Tạo đường dẫn file tọa độ
            XPositionManager.Instance.SetPositionXmlPathAndRoot("D:\\AOI_Config\\Position\\", "Position.xml", "PositionName.xml", "Position", Dir_BackUpConfigPosition);
            //Gán tọa độ cho từng task
            XPositionManager.Instance.BindPositionTableByTaskId((int)TaskId.Task70_ScannerBox);
            //Load tọa độ
            XPositionManager.Instance.LoadPositionSet();

            //Cau hinh tin hieu
            //XMachine.Instance.AddEStopDi((int)DiId.)
            //XMachine.Instance.DoorEnabled = Globals.SettingOption.IsOpensafeDoor();
            //XMachine.Instance.SafeDoorEStop = Globals.SettingOption.是否开启安全门急停复位;
            //XMachine.Instance.AddDoorDi((int)DiId.主设备门禁1, false);
            //XMachine.Instance.AddDoorDi((int)DiId.主设备门禁3, false);

            LoadVisionTools();
        }
        public static void LoadVisionTools()
        {
            try
            {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                string VisionPath = $"D:\\AOI_Config\\Setting\\{LoadModel.currentModel.modelName}\\VisionTools";
                string[] visionTool = { "Capture1.vpj", "Capture2.vpj" };
                for (int i = 0; i < visionTool.Length; i++)
                {
                    if (!File.Exists(Path.Combine(VisionPath, visionTool[i])))
                    {
                        Process process = new Process();
                        process.Path = Path.Combine(VisionPath, visionTool[i]);
                        process.Name = visionTool[i];
                        CreateProcess(Edit_Vision.Instance.processCreatorUI1, process);
                    }
                }
                Console.WriteLine("Create tool {0}",sw.ElapsedMilliseconds);
                string[] vpjFiles = Directory.GetFiles(VisionPath, "*.vpj", SearchOption.AllDirectories);
                foreach (string vpjFile in vpjFiles)
                {
                    Edit_Vision.Instance.processCreatorUI1.OpenProcessFile(vpjFile);
                    ListProcess.Add(Edit_Vision.Instance.processCreatorUI1.ProcessDisplay.Process);                   
                }
                Console.WriteLine("load tool {0}", sw.ElapsedMilliseconds);


                Thread thread = new Thread(() => AddProcessCreatorUI());
                thread.IsBackground = true;
                thread.Start();
                Console.WriteLine("run tool {0}", sw.ElapsedMilliseconds);
                sw.Stop();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void CreateProcess(ProcessCreatorUI processCreatorUI, Process process)
        {

            processCreatorUI.ProcessDisplay.Process = process;
            bool num = processCreatorUI.serializer.SaveProcess(process, process.Path);
        }

        //Đường dẫn file config
        public const string Dir_Record = "E:\\AOI_Record\\";
        public const string Dir_Record_CoverLog = Dir_Record + "MainLog\\";
        public const string Dir_Record_PDCALog = Dir_Record + "PDCALog\\";
        public const string Dir_Record_MESLog = Dir_Record + "MESLog\\";
        public const string Dir_Task70 = Dir_Record + "Dir_Task70\\";

        public const string Dir_SourceImage = "E:\\Vision\\";
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
        public static void WriteMainLog(string message)
        {
            string path = Dir_Record_CoverLog + "//MainLog" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            string str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " => " + message;
            CsvServer.Instance.WriteLine(path, str);
        }

        public static void SaveImageToFolder(Mat source1, Mat source2, Mat ImageResult1, Mat ImageResult2, bool result)
        {
            System.Threading.Thread SaveImage = new System.Threading.Thread(new System.Threading.ThreadStart(()=>
                {
                    var url = Globals.Dir_SourceImage + LoadModel.currentModel.modelName + "\\" + DateTime.Now.ToString("yyyy_MM_dd");

                    ClearOldImage(url);
                    // "E:\\Vision\\";
                    //Tạo đường dẫn cho path save image

                    var source = url + "Pass" + "\\" + DateTime.Now.ToString("HH:00") + "\\";
                    var fileOK = url + "OK" + "\\" + DateTime.Now.ToString("HH:00") + "\\";
                    var fileNG = url + "NG" + "\\" + DateTime.Now.ToString("HH:00") + "\\";
                    CreateDirectory(source);
                    CreateDirectory(fileOK);
                    CreateDirectory(fileNG);
                    //Lưu ảnh gốc
                    var fileName = source + DateTime.Now.ToString("HH_mm_ss_ff") + "_(1)" + ".jpg";
                    source1.SaveImage(fileName);

                    fileName = source + DateTime.Now.ToString("HH_mm_ss_ff") + "_(2)" + ".jpg";
                    source2.SaveImage(fileName);

                    //save image pass
                    if (result)
                    {
                        //Lưu ảnh pass
                        fileName = fileOK + DateTime.Now.ToString("HH_mm_ss_ff") + "_(1)" + ".jpg";
                        if (ImageResult1 != null)
                        {
                            ImageResult1.SaveImage(fileName);
                           // ImageResult1.Dispose();
                        }
                        fileName = fileOK + DateTime.Now.ToString("HH_mm_ss_ff") + "_(2)" + ".jpg";
                        if (ImageResult2 != null)
                        {
                            ImageResult2.SaveImage(fileName);
                            //ImageResult2.Dispose();
                        }

                    }
                    else
                    {
                        // Lưu ảnh kiểm tra lỗi
                        fileName = fileNG + DateTime.Now.ToString("HH_mm_ss_ff") + "_(1)" + ".jpg";
                        if (ImageResult1 != null)
                        {
                            ImageResult1.SaveImage(fileName);
                           // ImageResult1.Dispose();
                        }

                        fileName = fileNG + DateTime.Now.ToString("HH_mm_ss_ff") + "_(2)" + ".jpg";
                        if (ImageResult2 != null)
                        {
                            ImageResult2.SaveImage(fileName);
                           //ImageResult2.Dispose();

                        }
                    }
                }            
            ));
            SaveImage.IsBackground = true;
            SaveImage.Start();

        }
        //Clear old image depend on date
        private static void ClearOldImage(string url)
        {
            DateTime now = DateTime.Now;
            string[] fileList = Directory.GetDirectories(url);
            for (int i = 0; i < fileList.Length; i++)
            {
                DirectoryInfo dir = new System.IO.DirectoryInfo(fileList[i]);
                DateTime dt = dir.CreationTime;
                int tep = (now - dt).Days;
                if ((now - dt).Days > 30)
                {
                    try
                    {
                        Directory.Delete(fileList[i], true);
                    }
                    catch { }
                }
            }
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
            SettingOption = Setting_Model;
            SettingParameter = Parameter_Model;
            return;
        }

        public static List<HikCam> ListCams = new List<HikCam>();
        public static int OpenCameraRet = -1;
        public static HikCam Cam1;
        public static bool ConnectCamera()
        {
            if(OpenCameraRet != -1)
            {
                return true;
            }
            Cam1 = new HikCam(Globals.SettingParameter.CCD_Name);
            OpenCameraRet = Cam1.Open();
            if(OpenCameraRet == -1)
            {
                return false;
            }
            Cam1.SetExposeTime(5000);
            ListCams.Add(Cam1);
            return true;

        }

        public static void DisconectCamera()
        {
            if(Cam1!= null)
               Cam1.Close();
        }

        public static bool isMES = false;
        public static bool isSn = false;
        public static bool isPn = false;
        public static bool isImage1 = false;
        public static bool isImage2 = false;
        public static bool AOI_Result = false;
        public static bool isStation = false;


    }
}
