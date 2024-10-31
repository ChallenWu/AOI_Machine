using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Concurrent;
using System.Threading;


namespace XCore
{
    public sealed class XAlarmReporter : XAlarmEventHandler
    {
        private XAlarmEventArgs currentAlarm = new XAlarmEventArgs(0, "NONE", "NONE");
        private bool IsAlarming = false;
        private Dictionary<XAlarmId, XAlarmEventArgs> systemAlarms = new Dictionary<XAlarmId, XAlarmEventArgs>();
        private Dictionary<XAlarmLevel, XAlarmColor> alarmsColor = new Dictionary<XAlarmLevel, XAlarmColor>();
        private readonly static XAlarmReporter instance = new XAlarmReporter();
        private ConcurrentQueue<XAlarmEventArgs> OnAlarmingPageQueue = new ConcurrentQueue<XAlarmEventArgs>();
        private ConcurrentQueue<XAlarmEventArgs> OnProductingPageQueue = new ConcurrentQueue<XAlarmEventArgs>();
        private Thread _thread;
        private bool isAlarmingPageCreated = false;
        private bool isProductingPageCreated = false;
        XAlarmReporter()
        {
            InitSystemAlarms();
            InitAlarmsColor();

        }
        public bool IsAlarmingPageCreated
        {
            get { return isAlarmingPageCreated; }
            set { isAlarmingPageCreated = value; }
        }
        public bool IsProductingPageCreated
        {
            get { return isProductingPageCreated; }
            set { isProductingPageCreated = value; }
        }
        public static XAlarmReporter Instance
        {
            get { return instance; }
        }

        public event Action<XAlarmEventArgs> OnAlarmingPage;

        public event Action<XAlarmEventArgs> OnProductingPage;

        public event Action<XAlarmEventArgs> OnAlarmCleared;

        public event Action<XAlarmEventArgs> OnSaveAlarmReport;

        public Dictionary<XAlarmLevel, XAlarmColor> AlarmsColor
        {
            get { return alarmsColor; }
        }
        private void InitAlarmsColor()
        {
            alarmsColor.Add(XAlarmLevel.ONLYLOG, XAlarmColor.Green);
            alarmsColor.Add(XAlarmLevel.PAUSE, XAlarmColor.Yellow);
            alarmsColor.Add(XAlarmLevel.STOP, XAlarmColor.Red);
            alarmsColor.Add(XAlarmLevel.TIP, XAlarmColor.Red);
        }
        public void Stop()
        {
            if (this._thread != null)
            {
                this._thread.Abort();
            }
        }
        public void Start()
        {
            Stop();
            _thread = new Thread(new ThreadStart(ProcessEventQueue));
            _thread.IsBackground = true;
            _thread.Start();
        }
        private void RegisterAlarm(XAlarmId alarmId, AlarmCategory category, string description,
                string okOptiontext = "确认", string cancelOptionText = "", string ignoreOptionText = "")
        {
            description = MultiLanguage.GetMessage(description);
            okOptiontext = MultiLanguage.GetMessage(okOptiontext);
            cancelOptionText = MultiLanguage.GetMessage(cancelOptionText);
            ignoreOptionText = MultiLanguage.GetMessage(ignoreOptionText);
            systemAlarms.Add(alarmId, new XAlarmEventArgs((int)alarmId, category.ToString(), description, okOptiontext, cancelOptionText, ignoreOptionText));
        }
        public void RegisterCallbackAction(XAlarmId alarmId, CallbackAction callbackAction)
        {
            systemAlarms[alarmId].CallbackAct = callbackAction;
        }

        private void InitSystemAlarms()
        {
            //ID lỗi, loại lỗi, miêu tả lỗi, 
            #region System
            RegisterAlarm(XAlarmId.ESTOP, AlarmCategory.其他类报警, "紧急停止", "","停止");
            RegisterAlarm(XAlarmId.DOOR_OPEN, AlarmCategory.SafetyDoorAlarm, "触发安全门限", "", "停止");
            RegisterAlarm(XAlarmId.CURTAIN_ACT, AlarmCategory.SafetyDoorAlarm, "触发光幕", "", "停止");
            RegisterAlarm(XAlarmId.AXIS_SERVON_FAIL, AlarmCategory.伺服电机类异常报警, "轴使能失败", "", "停止");
            RegisterAlarm(XAlarmId.AXIS_ASTP, AlarmCategory.伺服电机类异常报警, "轴异常停止", "", "停止");
            RegisterAlarm(XAlarmId.AXIS_ALM, AlarmCategory.伺服电机类异常报警, "轴伺服报警", "", "停止");
            RegisterAlarm(XAlarmId.AXIS_PEL, AlarmCategory.伺服电机类异常报警, "轴触发正极限", "", "停止");
            RegisterAlarm(XAlarmId.AXIS_MEL, AlarmCategory.伺服电机类异常报警, "轴触发负极限", "", "停止");
            RegisterAlarm(XAlarmId.AXIS_POSERROR, AlarmCategory.伺服电机类异常报警, "轴跟随误差过大", "", "停止");
            RegisterAlarm(XAlarmId.WAITDI_TIMEOUT, AlarmCategory.信号故障报警, "等待信号超时", "", "停止");
            RegisterAlarm(XAlarmId.CARD_INIT_FAIL, AlarmCategory.其他类报警, "板卡初始化失败", "", "停止");
            RegisterAlarm(XAlarmId.CARD_LOAD_PARAM_FAIL, AlarmCategory.其他类报警, "板卡加载参数失败", "", "停止");
            RegisterAlarm(XAlarmId.AIR_LOW, AlarmCategory.气路异常报警, "正气压不足", "", "停止");
            RegisterAlarm(XAlarmId.Parameter_Abnormality, AlarmCategory.其他类报警, XAlarmId.Parameter_Abnormality.ToString(), "重试", "停止");
            #endregion

            #region Vision
            RegisterAlarm(XAlarmId.相机出现异常, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.相机出现异常.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.相机错误, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.相机错误.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.相机数据错误, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.相机数据错误.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.CCD数据超出正常值过多, AlarmCategory.视觉异常报警, XAlarmId.CCD数据超出正常值过多.ToString(), "重新拍照", "停止", "抛料");
            RegisterAlarm(XAlarmId.检查图像并选择是否要抛PCB, AlarmCategory.视觉异常报警, XAlarmId.检查图像并选择是否要抛PCB.ToString(), "抛PCB", "停止", "继续");
            RegisterAlarm(XAlarmId.检查图像并选择是否要抛Flex, AlarmCategory.视觉异常报警, XAlarmId.检查图像并选择是否要抛Flex.ToString(), "抛Flex", "停止", "继续");
            RegisterAlarm(XAlarmId.扫描载具二维码失败, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.扫描载具二维码失败.ToString(), "", "停止", "继续");
            #endregion

            #region Servo
            RegisterAlarm(XAlarmId.运动超时错误, AlarmCategory.伺服电机类异常报警, XAlarmId.运动超时错误.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.马达运动错误, AlarmCategory.伺服电机类异常报警, XAlarmId.马达运动错误.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.Acs通信错误, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.Acs通信错误.ToString(), "重试", "停止");
            #endregion

            #region Comunication 
            RegisterAlarm(XAlarmId.运行流水程序异常, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.运行流水程序异常.ToString(), "", "Stop");
            #endregion

            #region Tray
            //RegisterAlarm(XAlarmId.左供料机左上仓对射超时, AlarmCategory.TRAY, "左供料机左上仓对射超时，请检查左供料机左上仓对射", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机左上仓气缸原点超时, AlarmCategory.TRAY, "左供料机左上仓气缸原点，请检查左供料机左上仓气缸原点", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机左上仓气缸动点超时, AlarmCategory.TRAY, "左供料机左上仓气缸动点超时，请检查左供料机左上仓气缸动点", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机左上仓换料, AlarmCategory.TRAY, "左供料机左上仓请求换料，请更换左供料机左上仓Tray盘", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机左上仓门未关闭, AlarmCategory.TRAY, "左供料机左上仓门未关闭，请关闭左供料机左上仓门", "重试", "停止");


            //RegisterAlarm(XAlarmId.左供料机左下仓对射超时, AlarmCategory.TRAY, "左供料机左下仓对射超时，请检查左供料机左下仓对射", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机左下仓气缸原点超时, AlarmCategory.TRAY, "左供料机左下仓气缸原点超时，请检查左供料机左下仓气缸原点", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机左下仓气缸动点超时, AlarmCategory.TRAY, "左供料机左下仓气缸动点超时，请检查左供料机左下仓气缸动点", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机左下仓换料, AlarmCategory.TRAY, "左供料机左下仓请求换料，请更换左供料机左下仓Tray盘", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机左下仓门未关闭, AlarmCategory.TRAY, "左供料机左下仓门未关闭，请关闭左供料机左下仓门", "重试", "停止");


            //RegisterAlarm(XAlarmId.左供料机右上仓对射超时, AlarmCategory.TRAY, "左供料机右上仓对射超时，请检查左供料机右上仓对射", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机右上仓气缸原点超时, AlarmCategory.TRAY, "左供料机右上仓气缸原点超时，请检查左供料机右上仓气缸原点", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机右上仓气缸动点超时, AlarmCategory.TRAY, "左供料机右上仓气缸动点超时，请检查左供料机右上仓气缸动点", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机右上仓换料, AlarmCategory.TRAY, "左供料机右上仓请求换料，请更换左供料机右上仓Tray盘", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机右上仓门未关闭, AlarmCategory.TRAY, "左供料机右上仓门未关闭，请关闭左供料机右上仓门", "重试", "停止");


            //RegisterAlarm(XAlarmId.左供料机右下仓对射超时, AlarmCategory.TRAY, "左供料机右下仓对射超时，请检查左供料机右下仓对射", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机右下仓气缸原点超时, AlarmCategory.TRAY, "左供料机右下仓气缸原点超时，请检查左供料机右下仓气缸原点", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机右下仓气缸动点超时, AlarmCategory.TRAY, "左供料机右下仓气缸动点超时，请检查左供料机右下仓气缸动点", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机右下仓换料, AlarmCategory.TRAY, "左供料机右下仓请求换料，请更换左供料机右下仓Tray盘", "重试", "停止");
            //RegisterAlarm(XAlarmId.左供料机右下仓门未关闭, AlarmCategory.TRAY, "左供料机右下仓门未关闭，请关闭左供料机右下仓门", "重试", "停止");

            //RegisterAlarm(XAlarmId.右供料机左上仓对射超时, AlarmCategory.TRAY, "右供料机左上仓对射超时，请检查右供料机左上仓对射", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机左上仓气缸原点超时, AlarmCategory.TRAY, "右供料机左上仓气缸原点，请检查右供料机左上仓气缸原点", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机左上仓气缸动点超时, AlarmCategory.TRAY, "右供料机左上仓气缸动点超时，请检查右供料机左上仓气缸动点", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机左上仓换料, AlarmCategory.TRAY, "右供料机左上仓请求换料，请更换右供料机左上仓Tray盘", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机左上仓门未关闭, AlarmCategory.TRAY, "右供料机左上仓门未关闭，请关闭右供料机左上仓门", "重试", "停止");

            //RegisterAlarm(XAlarmId.右供料机左下仓对射超时, AlarmCategory.TRAY, "右供料机左下仓对射超时，请检查右供料机左下仓对射", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机左下仓气缸原点超时, AlarmCategory.TRAY, "右供料机左下仓气缸原点超时，请检查右供料机左下仓气缸原点", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机左下仓气缸动点超时, AlarmCategory.TRAY, "右供料机左下仓气缸动点超时，请检查右供料机左下仓气缸动点", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机左下仓换料, AlarmCategory.TRAY, "右供料机左下仓请求换料，请更换右供料机左下仓Tray盘", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机左下仓门未关闭, AlarmCategory.TRAY, "右供料机左下仓门未关闭，请关闭右供料机左下仓门", "重试", "停止");


            //RegisterAlarm(XAlarmId.右供料机右上仓对射超时, AlarmCategory.TRAY, "右供料机右上仓对射超时，请检查右供料机右上仓对射", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机右上仓气缸原点超时, AlarmCategory.TRAY, "右供料机右上仓气缸原点超时，请检查右供料机右上仓气缸原点", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机右上仓气缸动点超时, AlarmCategory.TRAY, "右供料机右上仓气缸动点超时，请检查右供料机右上仓气缸动点", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机右上仓换料, AlarmCategory.TRAY, "右供料机右上仓请求换料，请更换右供料机右上仓Tray盘", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机右上仓门未关闭, AlarmCategory.TRAY, "右供料机右上仓门未关闭，请关闭右供料机右上仓门", "重试", "停止");

            //RegisterAlarm(XAlarmId.右供料机右下仓对射超时, AlarmCategory.TRAY, "右供料机右下仓对射超时，请检查右供料机右下仓对射", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机右下仓气缸原点超时, AlarmCategory.TRAY, "右供料机右下仓气缸原点超时，请检查右供料机右下仓气缸原点", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机右下仓气缸动点超时, AlarmCategory.TRAY, "右供料机右下仓气缸动点超时，请检查右供料机右下仓气缸动点", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机右下仓换料, AlarmCategory.TRAY, "右供料机右下仓请求换料，请更换右供料机右下仓Tray盘", "重试", "停止");
            //RegisterAlarm(XAlarmId.右供料机右下仓门未关闭, AlarmCategory.TRAY, "右供料机右下仓门未关闭，请关闭右供料机右下仓门", "重试", "停止");
            #endregion

            #region Error
            RegisterAlarm(XAlarmId.左供料机左上仓对射超时, AlarmCategory.信号故障报警, XAlarmId.左供料机左上仓对射超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机左上仓气缸原点超时, AlarmCategory.信号故障报警, XAlarmId.左供料机左上仓气缸原点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机左上仓气缸动点超时, AlarmCategory.信号故障报警, XAlarmId.左供料机左上仓气缸动点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机左上仓换料, AlarmCategory.信号故障报警, XAlarmId.左供料机左上仓换料.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机左上仓门未关闭, AlarmCategory.信号故障报警, XAlarmId.左供料机左上仓门未关闭.ToString(), "重试", "停止");


            RegisterAlarm(XAlarmId.左供料机左下仓对射超时, AlarmCategory.信号故障报警, XAlarmId.左供料机左下仓对射超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机左下仓气缸原点超时, AlarmCategory.信号故障报警, XAlarmId.左供料机左下仓气缸原点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机左下仓气缸动点超时, AlarmCategory.信号故障报警, XAlarmId.左供料机左下仓气缸动点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机左下仓换料, AlarmCategory.信号故障报警, XAlarmId.左供料机左下仓换料.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机左下仓门未关闭, AlarmCategory.信号故障报警, XAlarmId.左供料机左下仓门未关闭.ToString(), "重试", "停止");


            RegisterAlarm(XAlarmId.左供料机右上仓对射超时, AlarmCategory.信号故障报警, XAlarmId.左供料机右上仓对射超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机右上仓气缸原点超时, AlarmCategory.信号故障报警, XAlarmId.左供料机右上仓气缸原点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机右上仓气缸动点超时, AlarmCategory.信号故障报警, XAlarmId.左供料机右上仓气缸动点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机右上仓换料, AlarmCategory.信号故障报警, XAlarmId.左供料机右上仓换料.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机右上仓门未关闭, AlarmCategory.信号故障报警, XAlarmId.左供料机右上仓门未关闭.ToString(), "重试", "停止");


            RegisterAlarm(XAlarmId.左供料机右下仓对射超时, AlarmCategory.信号故障报警, XAlarmId.左供料机右下仓对射超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机右下仓气缸原点超时, AlarmCategory.信号故障报警, XAlarmId.左供料机右下仓气缸原点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机右下仓气缸动点超时, AlarmCategory.信号故障报警, XAlarmId.左供料机右下仓气缸动点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机右下仓换料, AlarmCategory.信号故障报警, XAlarmId.左供料机右下仓换料.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左供料机右下仓门未关闭, AlarmCategory.信号故障报警, XAlarmId.左供料机右下仓门未关闭.ToString(), "重试", "停止");

            RegisterAlarm(XAlarmId.右供料机左上仓对射超时, AlarmCategory.信号故障报警, XAlarmId.右供料机左上仓对射超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机左上仓气缸原点超时, AlarmCategory.信号故障报警, XAlarmId.右供料机左上仓气缸原点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机左上仓气缸动点超时, AlarmCategory.信号故障报警, XAlarmId.右供料机左上仓气缸动点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机左上仓换料, AlarmCategory.信号故障报警, XAlarmId.右供料机左上仓换料.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机左上仓门未关闭, AlarmCategory.信号故障报警, XAlarmId.右供料机左上仓门未关闭.ToString(), "重试", "停止");

            RegisterAlarm(XAlarmId.右供料机左下仓对射超时, AlarmCategory.信号故障报警, XAlarmId.右供料机左下仓对射超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机左下仓气缸原点超时, AlarmCategory.信号故障报警, XAlarmId.右供料机左下仓气缸原点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机左下仓气缸动点超时, AlarmCategory.信号故障报警, XAlarmId.右供料机左下仓气缸动点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机左下仓换料, AlarmCategory.信号故障报警, XAlarmId.右供料机左下仓换料.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机左下仓门未关闭, AlarmCategory.信号故障报警, XAlarmId.右供料机左下仓门未关闭.ToString(), "重试", "停止");


            RegisterAlarm(XAlarmId.右供料机右上仓对射超时, AlarmCategory.信号故障报警, XAlarmId.右供料机右上仓对射超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机右上仓气缸原点超时, AlarmCategory.信号故障报警, XAlarmId.右供料机右上仓气缸原点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机右上仓气缸动点超时, AlarmCategory.信号故障报警, XAlarmId.右供料机右上仓气缸动点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机右上仓换料, AlarmCategory.信号故障报警, XAlarmId.右供料机右上仓换料.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机右上仓门未关闭, AlarmCategory.信号故障报警, XAlarmId.右供料机右上仓门未关闭.ToString(), "重试", "停止");

            RegisterAlarm(XAlarmId.右供料机右下仓对射超时, AlarmCategory.信号故障报警, XAlarmId.右供料机右下仓对射超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机右下仓气缸原点超时, AlarmCategory.信号故障报警, XAlarmId.右供料机右下仓气缸原点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机右下仓气缸动点超时, AlarmCategory.信号故障报警, XAlarmId.右供料机右下仓气缸动点超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机右下仓换料, AlarmCategory.信号故障报警, XAlarmId.右供料机右下仓换料.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右供料机右下仓门未关闭, AlarmCategory.信号故障报警, XAlarmId.右供料机右下仓门未关闭.ToString(), "重试", "停止");

            RegisterAlarm(XAlarmId.料仓料槽无料, AlarmCategory.信号故障报警, XAlarmId.料仓料槽无料.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.料仓移动出错, AlarmCategory.信号故障报警, XAlarmId.料仓移动出错.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.料仓不存在空的料槽, AlarmCategory.信号故障报警, XAlarmId.料仓不存在空的料槽.ToString(), "重试", "停止");

            RegisterAlarm(XAlarmId.左预取料1顶升上升超时, AlarmCategory.信号故障报警, XAlarmId.左预取料1顶升上升超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左预取料1顶升下降超时, AlarmCategory.信号故障报警, XAlarmId.左预取料1顶升下降超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左预取料1Tray盘流料超时, AlarmCategory.信号故障报警, XAlarmId.左预取料1Tray盘流料超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左预取料1Tray盘真空吸到位超时, AlarmCategory.气路异常报警, XAlarmId.左预取料1Tray盘真空吸到位超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左预取料1Tray盘真空吸解除超时, AlarmCategory.气路异常报警, XAlarmId.左预取料1Tray盘真空吸解除超时.ToString(), "重试", "停止");

            RegisterAlarm(XAlarmId.左预取料2顶升上升超时, AlarmCategory.信号故障报警, XAlarmId.左预取料2顶升上升超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左预取料2顶升下降超时, AlarmCategory.信号故障报警, XAlarmId.左预取料2顶升下降超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左预取料2Tray盘流料超时, AlarmCategory.信号故障报警, XAlarmId.左预取料2Tray盘流料超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左预取料2Tray盘真空吸到位超时, AlarmCategory.气路异常报警, XAlarmId.左预取料2Tray盘真空吸到位超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左预取料2Tray盘真空吸解除超时, AlarmCategory.气路异常报警, XAlarmId.左预取料2Tray盘真空吸解除超时.ToString(), "重试", "停止");

            RegisterAlarm(XAlarmId.右预取料1顶升上升超时, AlarmCategory.信号故障报警, XAlarmId.右预取料1顶升上升超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右预取料1顶升下降超时, AlarmCategory.信号故障报警, XAlarmId.右预取料1顶升下降超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右预取料1Tray盘流料超时, AlarmCategory.信号故障报警, XAlarmId.右预取料1Tray盘流料超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右预取料1Tray盘真空吸到位超时, AlarmCategory.气路异常报警, XAlarmId.右预取料1Tray盘真空吸到位超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右预取料1Tray盘真空吸解除超时, AlarmCategory.气路异常报警, XAlarmId.右预取料1Tray盘真空吸解除超时.ToString(), "重试", "停止");

            RegisterAlarm(XAlarmId.右预取料2顶升上升超时, AlarmCategory.信号故障报警, XAlarmId.右预取料2顶升上升超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右预取料2顶升下降超时, AlarmCategory.信号故障报警, XAlarmId.右预取料2顶升下降超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右预取料2Tray盘流料超时, AlarmCategory.信号故障报警, XAlarmId.右预取料2Tray盘流料超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右预取料2Tray盘真空吸到位超时, AlarmCategory.气路异常报警, XAlarmId.右预取料2Tray盘真空吸到位超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右预取料2Tray盘真空吸解除超时, AlarmCategory.气路异常报警, XAlarmId.右预取料2Tray盘真空吸解除超时.ToString(), "重试", "停止");


            RegisterAlarm(XAlarmId.供料流线Tray盘状态不正确, AlarmCategory.信号故障报警, XAlarmId.供料流线Tray盘状态不正确.ToString(), "有", "停止");
            RegisterAlarm(XAlarmId.温控器异常, AlarmCategory.温控异常报警, XAlarmId.温控器异常.ToString(), "停止");
            RegisterAlarm(XAlarmId.压力数据异常, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.压力数据异常.ToString(), "停止");
            #endregion

            #region Carrier
            RegisterAlarm(XAlarmId.进载流料超时, AlarmCategory.信号故障报警, XAlarmId.进载流料超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.进载阻挡打开超时, AlarmCategory.信号故障报警, XAlarmId.进载阻挡打开超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.进载阻挡关闭超时, AlarmCategory.信号故障报警, XAlarmId.进载阻挡关闭超时.ToString(), "重试", "停止");

            RegisterAlarm(XAlarmId.左贴装载具流料超时, AlarmCategory.信号故障报警, XAlarmId.左贴装载具流料超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左贴装阻挡打开超时, AlarmCategory.信号故障报警, XAlarmId.左贴装阻挡打开超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左贴装阻挡关闭超时, AlarmCategory.信号故障报警, XAlarmId.左贴装阻挡关闭超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左贴装顶升上升超时, AlarmCategory.信号故障报警, XAlarmId.左贴装顶升上升超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左贴装顶升下降超时, AlarmCategory.信号故障报警, XAlarmId.左贴装顶升下降超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左贴装载具真空吸到位超时, AlarmCategory.气路异常报警, XAlarmId.左贴装载具真空吸到位超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.左贴装载具真空吸解除超时, AlarmCategory.气路异常报警, XAlarmId.左贴装载具真空吸解除超时.ToString(), "重试", "停止");


            RegisterAlarm(XAlarmId.过渡载具流料超时, AlarmCategory.信号故障报警, XAlarmId.过渡载具流料超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.过渡阻挡打开超时, AlarmCategory.信号故障报警, XAlarmId.过渡阻挡打开超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.过渡阻挡关闭超时, AlarmCategory.信号故障报警, XAlarmId.过渡阻挡关闭超时.ToString(), "重试", "停止");

            RegisterAlarm(XAlarmId.右贴装载具流料超时, AlarmCategory.信号故障报警, XAlarmId.右贴装载具流料超时.ToString(), "", "停止");
            RegisterAlarm(XAlarmId.右贴装阻挡打开超时, AlarmCategory.信号故障报警, XAlarmId.右贴装阻挡打开超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右贴装阻挡关闭超时, AlarmCategory.信号故障报警, XAlarmId.右贴装阻挡关闭超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右贴装顶升上升超时, AlarmCategory.信号故障报警, XAlarmId.右贴装顶升上升超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右贴装顶升下降超时, AlarmCategory.信号故障报警, XAlarmId.右贴装顶升下降超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右贴装载具真空吸到位超时, AlarmCategory.气路异常报警, XAlarmId.右贴装载具真空吸到位超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.右贴装载具真空吸解除超时, AlarmCategory.气路异常报警, XAlarmId.右贴装载具真空吸解除超时.ToString(), "重试", "停止");

            RegisterAlarm(XAlarmId.出载载具流料超时, AlarmCategory.信号故障报警, XAlarmId.出载载具流料超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.回流线流入超时, AlarmCategory.信号故障报警, XAlarmId.回流线流入超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.回流线流出超时, AlarmCategory.信号故障报警, XAlarmId.回流线流出超时.ToString(), "重试", "停止");


            #endregion

            #region Assemble
            RegisterAlarm(XAlarmId.取Cyclone连续失败次数超出范围, AlarmCategory.提示类报警, XAlarmId.取Cyclone连续失败次数超出范围.ToString(), "继续", "停止");
            RegisterAlarm(XAlarmId.SFC反馈数据超时, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.SFC反馈数据超时.ToString(), "重新查询", "停止", "忽略");
            RegisterAlarm(XAlarmId.SFC反馈数据错误, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.SFC反馈数据错误.ToString(), "重新查询", "停止", "忽略");
            RegisterAlarm(XAlarmId.MES绑定数据错误, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.MES绑定数据错误.ToString(), "确定OK后继续", "停止");
            RegisterAlarm(XAlarmId.载具定位失败, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.载具定位失败.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.触发取Flex异常, AlarmCategory.气路异常报警, XAlarmId.触发取Flex异常.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.吸取Flex异常, AlarmCategory.气路异常报警, XAlarmId.吸取Flex异常.ToString(), "", "停止");

            RegisterAlarm(XAlarmId.Flex取料失败超出限制, AlarmCategory.提示类报警, XAlarmId.Flex取料失败超出限制.ToString(), "继续", "停止");

            RegisterAlarm(XAlarmId.Flex连续失败个数超出限制, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.Flex连续失败个数超出限制.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.吸取Flex超时错误, AlarmCategory.气路异常报警, XAlarmId.吸取Flex超时错误.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.二次定位失败, AlarmCategory.视觉异常报警, XAlarmId.二次定位失败.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.吸头传感器状态不正确, AlarmCategory.信号故障报警, XAlarmId.吸头传感器状态不正确.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.软吸嘴状态异常, AlarmCategory.气路异常报警, XAlarmId.软吸嘴状态异常.ToString(), "继续", "停止");
            RegisterAlarm(XAlarmId.贴合超时错误, AlarmCategory.提示类报警, XAlarmId.贴合超时错误.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.计算贴合位置错误, AlarmCategory.提示类报警, XAlarmId.计算贴合位置错误.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.贴合异常, AlarmCategory.提示类报警, XAlarmId.贴合异常.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.复检出错, AlarmCategory.提示类报警, XAlarmId.复检出错.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.马达回到安全位置错误, AlarmCategory.伺服电机类异常报警, XAlarmId.Parameter_Abnormality.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.点胶检测超时, AlarmCategory.CommunicationConnectionAlarm, XAlarmId.点胶检测超时.ToString(), "重试", "停止");
            RegisterAlarm(XAlarmId.吸头存在未贴合的物料, AlarmCategory.提示类报警, XAlarmId.吸头存在未贴合的物料.ToString(), "重试", "停止");
            #endregion
        }

        public Dictionary<XAlarmId, XAlarmEventArgs> SystemAlarms
        {
            get { return this.systemAlarms; }
        }

        public XAlarmEventArgs CurrentAlarm
        {
            get { return currentAlarm; }
        }

        public void ClearAlarm()
        {
            if (IsAlarming)
            {
                if (OnAlarmCleared != null)
                {
                    
                    OnAlarmCleared(this.currentAlarm);
                    if (OnSaveAlarmReport != null)
                    {
                        this.currentAlarm.TimeMode = 1;
                        OnSaveAlarmReport(this.currentAlarm);
                    }
                }
                IsAlarming = false;
            }
        }

        public override int HandleEvent(XEvent xEvent)
        {
            try
            {
                int stationId = 0;
                int alarmId = 0;
                int alarmlevel=0;
                string description = "";
                switch (xEvent.EventID)
                {
                    case XEventID.ALARM:
                        {
                            stationId = xEvent.EventArgs.StationId;
                            alarmId = xEvent.EventArgs.AlarmId;
                            alarmlevel = xEvent.EventArgs.AlarmLevel;
                            string append = "";
                            switch (alarmId)
                            {
                                case (int)XAlarmId.AXIS_SERVON_FAIL:
                                    append = "(AxisId:" + xEvent.EventArgs.IntValue + "[" +
                                        XDevice.Instance.FindAxisById(xEvent.EventArgs.IntValue).Name + "])";
                                    description = SystemAlarms[(XAlarmId)alarmId].Description + append;
                                    break;
                                case (int)XAlarmId.AXIS_ASTP:
                                    append = "(AxisId:" + xEvent.EventArgs.IntValue + "[" +
                                        XDevice.Instance.FindAxisById(xEvent.EventArgs.IntValue).Name + "])";
                                    description = SystemAlarms[(XAlarmId)alarmId].Description + append;
                                    break;
                                case (int)XAlarmId.AXIS_POSERROR:
                                    append = "(AxisId:" + xEvent.EventArgs.IntValue + "[" +
                                        XDevice.Instance.FindAxisById(xEvent.EventArgs.IntValue).Name + "])";
                                    description = SystemAlarms[(XAlarmId)alarmId].Description + append;
                                    break;
                                case (int)XAlarmId.AXIS_ALM:
                                    append = "(AxisId:" + xEvent.EventArgs.IntValue + "[" +
                                        XDevice.Instance.FindAxisById(xEvent.EventArgs.IntValue).Name + "])";
                                    description = SystemAlarms[(XAlarmId)alarmId].Description + append;
                                    break;
                                case (int)XAlarmId.AXIS_PEL:
                                    append = "(AxisId:" + xEvent.EventArgs.IntValue + "[" +
                                        XDevice.Instance.FindAxisById(xEvent.EventArgs.IntValue).Name + "])";
                                    description = SystemAlarms[(XAlarmId)alarmId].Description + append;
                                    break;
                                case (int)XAlarmId.AXIS_MEL:
                                    append = "(AxisId:" + xEvent.EventArgs.IntValue + "[" +
                                        XDevice.Instance.FindAxisById(xEvent.EventArgs.IntValue).Name + "])";
                                    description = SystemAlarms[(XAlarmId)alarmId].Description + append;
                                    break;
                                case (int)XAlarmId.WAITDI_TIMEOUT:
                                    string diName = XDevice.Instance.FindDiById(xEvent.EventArgs.IntValue).Name;
                                    string state = (xEvent.EventArgs.BoolValue) ? "ON" : "OFF";
                                    append = "(DiId:" + xEvent.EventArgs.IntValue + "["
                                        + diName + "]_"
                                        + state + ")";
                                    description = SystemAlarms[(XAlarmId)alarmId].Description + append;
                                    break;
                                case 0:


                                    break;
                                case (int)XAlarmId.DOOR_OPEN:
                                    append = "安全门" +"\"" + xEvent.EventArgs.StringValue  + "\""+ "被打开";
                                    description = SystemAlarms[(XAlarmId)alarmId].Description + append;
                                    break;
                                case (int)XAlarmId.AIR_LOW:
                                    append = "正气压低于标准值";
                                    description = SystemAlarms[(XAlarmId)alarmId].Description + append;
                                    break;
                                case (int)XAlarmId.温控器异常:
                                    append = "吸头焊台温度异常,请检查 "+ xEvent.EventArgs.IntValue + " 吸头焊台";;
                                    description = SystemAlarms[(XAlarmId)alarmId].Description + append;
                                    break;
                                default:
                                    append = xEvent.EventArgs.StringValue;
                                    description = SystemAlarms[(XAlarmId)alarmId].Description + append;
                                    break;
                            }
                            
                        }
                        break;
                    case XEventID.ESTOP:
                        stationId = xEvent.EventArgs.StationId;
                        alarmId = (int)XAlarmId.ESTOP;
                        description = SystemAlarms[(XAlarmId)alarmId].Description;
                        alarmlevel = xEvent.EventArgs.AlarmLevel;
                        break;
                    case XEventID.LOG:
                        xEvent.EventHandler.HandleEvent(xEvent);
                        break;
                }

                if (xEvent.EventID != XEventID.LOG)
                {
                    if (alarmId == 0)
                    {
                        this.currentAlarm = xEvent.EventArgs.AlarmEventArgs;
                    }
                    else
                    {
                        this.currentAlarm = new XAlarmEventArgs(SystemAlarms[(XAlarmId)alarmId].Code, SystemAlarms[(XAlarmId)alarmId].Category, description);
                    }
                    this.currentAlarm.StationId = stationId;
                    this.currentAlarm.StartTime = DateTime.Now;
                    this.currentAlarm.TimeMode = 0;
                    this.currentAlarm.AlarmLevel = alarmlevel;
                    if (xEvent.EventArgs.AlarmLevel != (int)XAlarmLevel.ONLYLOG)
                    {
                        
                        if (OnSaveAlarmReport != null)
                        {
                            OnSaveAlarmReport(this.currentAlarm);
                            IsAlarming = true;
                        }
                        if (OnProductingPage != null || OnAlarmingPage != null)
                        {
                            //OnProductingPage(this.currentAlarm);
                            CloneToQueue(this.currentAlarm);
                        }
                        //if (OnAlarmingPage != null)
                        //{
                        //    OnAlarmingPage(this.currentAlarm);
                            
                        //}


                    }

                    XLogger.Instance.WriteLine(
                            "Station" + this.currentAlarm.StationId
                          + " => AlarmCode:" + this.currentAlarm.Code.ToString()
                          + " => Category:" + this.currentAlarm.Category
                          + " => " + this.currentAlarm.Description);
                }

                return 0;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex, "");
                return -1;
            }
        }
        private void ProcessEventQueue()
        {
            while (true)
            {
                if (isProductingPageCreated)
                {
                    if (OnProductingPageQueue.Count > 0)
                    {
                        XAlarmEventArgs ProductingAlarm;
                        OnProductingPageQueue.TryDequeue(out ProductingAlarm);
                        OnProductingPage(ProductingAlarm);
        
                    } 
                }
                if (isAlarmingPageCreated)
                {
                    if (OnAlarmingPageQueue.Count > 0)
                    {
                        XAlarmEventArgs Alarm;
                        if (OnAlarmingPageQueue.TryDequeue(out Alarm))
                        {
                            OnAlarmingPage(Alarm);
                        }
                        else
                        {
                            Thread.Sleep(10);
                            continue;
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }
        private void CloneToQueue(XAlarmEventArgs HistoryAlarm)
        {
            XAlarmEventArgs QueueAlarm = new XAlarmEventArgs(0, "NONE", "NONE");
            QueueAlarm.AlarmLevel = HistoryAlarm.AlarmLevel;
            QueueAlarm.Category  = HistoryAlarm.Category;
            QueueAlarm.Code = HistoryAlarm.Code;
            QueueAlarm.Description = HistoryAlarm.Description;
            QueueAlarm.Duration = HistoryAlarm.Duration;
            QueueAlarm.StartTime = HistoryAlarm.StartTime;
            QueueAlarm.StationId = HistoryAlarm.StationId;
            QueueAlarm.TimeMode = HistoryAlarm.TimeMode;
            OnAlarmingPageQueue.Enqueue(QueueAlarm);
            OnProductingPageQueue.Enqueue(QueueAlarm);
        }


        
    }

    public enum AlarmCategory
    {
        其他类报警,
        视觉异常报警,
        信号故障报警,
        伺服电机类异常报警,
        气路异常报警,
        提示类报警,
        CommunicationConnectionAlarm,
        温控异常报警,
        SafetyDoorAlarm
    }

    public enum XAlarmId
    {
        NONE,
        // System
        ESTOP = 10000,
        DOOR_OPEN,
        CURTAIN_ACT,
        AXIS_SERVON_FAIL,
        AXIS_ASTP,
        AXIS_ALM,
        AXIS_PEL,
        AXIS_MEL,
        AXIS_POSERROR,
        WAITDI_TIMEOUT,
        CARD_INIT_FAIL,
        CARD_LOAD_PARAM_FAIL,
        TCP_DISCONNECT,
        AIR_LOW,
        RST,
        WAIT_MOTION_TIMEOUT,
        Parameter_Abnormality,
        Test,

        // Vision
        相机出现异常 = 11000,
        相机错误,
        相机数据错误,
        CCD数据超出正常值过多,
        检查图像并选择是否要抛PCB,
        检查图像并选择是否要抛Flex,

        // Scanner
        扫描枪通讯错误 = 12000,
        扫描载具二维码失败,
        扫描载具连续失败次数超出限制,

        // 伺服
        运动超时错误 = 13000,
        马达回到安全位置错误,
        Acs通信错误,

        // Tray Flow Line
        运行流水程序异常 = 14000,

        // Carrier In
        进载流料超时 = 15000,
        进载阻挡打开超时,
        进载阻挡关闭超时,

        //AssembleNIO
        左贴装载具流料超时,
        左贴装阻挡打开超时,
        左贴装阻挡关闭超时,
        左贴装顶升上升超时,
        左贴装顶升下降超时,
        左贴装载具真空吸到位超时,
        左贴装载具真空吸解除超时,


        //CarrierTransfer
        过渡载具流料超时,
        过渡阻挡打开超时,
        过渡阻挡关闭超时,


        //AssembleRIO
        右贴装载具流料超时,
        右贴装阻挡打开超时,
        右贴装阻挡关闭超时,
        右贴装顶升上升超时,
        右贴装顶升下降超时,
        右贴装载具真空吸到位超时,
        右贴装载具真空吸解除超时,

        //CarrierOut
        出载载具流料超时,

        //CarrierFlowBack
        回流线流入超时,
        回流线流出超时,

        //Tray Supply
        左供料机左上仓对射超时,
        左供料机左上仓气缸原点超时,
        左供料机左上仓气缸动点超时,
        左供料机左上仓换料,
        左供料机左上仓门未关闭,

        左供料机左下仓对射超时,
        左供料机左下仓气缸原点超时,
        左供料机左下仓气缸动点超时,
        左供料机左下仓换料,
        左供料机左下仓门未关闭,

        左供料机右上仓对射超时,
        左供料机右上仓气缸原点超时,
        左供料机右上仓气缸动点超时,
        左供料机右上仓换料,
        左供料机右上仓门未关闭,

        左供料机右下仓对射超时,
        左供料机右下仓气缸原点超时,
        左供料机右下仓气缸动点超时,
        左供料机右下仓换料,
        左供料机右下仓门未关闭,

        右供料机左上仓对射超时,
        右供料机左上仓气缸原点超时,
        右供料机左上仓气缸动点超时,
        右供料机左上仓换料,
        右供料机左上仓门未关闭,

        右供料机左下仓对射超时,
        右供料机左下仓气缸原点超时,
        右供料机左下仓气缸动点超时,
        右供料机左下仓换料,
        右供料机左下仓门未关闭,

        右供料机右上仓对射超时,
        右供料机右上仓气缸原点超时,
        右供料机右上仓气缸动点超时,
        右供料机右上仓换料,
        右供料机右上仓门未关闭,

        右供料机右下仓对射超时,
        右供料机右下仓气缸原点超时,
        右供料机右下仓气缸动点超时,
        右供料机右下仓换料,
        右供料机右下仓门未关闭,

        料仓料槽无料,

        料仓移动出错,

        料仓不存在空的料槽,

        供料流线Tray盘状态不正确,

        //Tray Feed
        左预取料1顶升上升超时,
        左预取料1顶升下降超时,
        左预取料1Tray盘流料超时,
        左预取料1Tray盘真空吸到位超时,
        左预取料1Tray盘真空吸解除超时,

        左预取料2顶升上升超时,
        左预取料2顶升下降超时,
        左预取料2Tray盘流料超时,
        左预取料2Tray盘真空吸到位超时,
        左预取料2Tray盘真空吸解除超时,

        右预取料1顶升上升超时,
        右预取料1顶升下降超时,
        右预取料1Tray盘流料超时,
        右预取料1Tray盘真空吸到位超时,
        右预取料1Tray盘真空吸解除超时,

        右预取料2顶升上升超时,
        右预取料2顶升下降超时,
        右预取料2Tray盘流料超时,
        右预取料2Tray盘真空吸到位超时,
        右预取料2Tray盘真空吸解除超时,

        // 贴合
        载具定位失败,
        触发取Flex异常,
        吸取Flex异常,
        Flex取料失败超出限制,
        Flex连续失败个数超出限制,
        二次定位失败,
        吸头传感器状态不正确, //吸头传感器状态不正确
        软吸嘴状态异常,
        吸取Flex超时错误, //吸取Flex超时错误
        贴合超时错误, // 贴合超时错误
        计算贴合位置错误,
        贴合异常,
        马达运动错误,
        取Cyclone连续失败次数超出范围,
        SFC反馈数据超时,
        SFC反馈数据错误,
        MES绑定数据错误,
        温控器异常,
        温控器通信异常,
        压力数据异常,
        吸头存在未贴合的物料,
        // 复检
        条码器反馈数据超时 = 17000,
        上传PDCA失败,
        复检出错,
        //联机
        点胶检测超时,


    }

    public enum XAlarmLevel
    {
        TIP,
        PAUSE,
        STOP,
        ONLYLOG
    }

    public enum XAlarmColor
    {
        Red,
        Yellow,
        Green
    }
    
}
