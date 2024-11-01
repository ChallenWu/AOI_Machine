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
            RegisterAlarm(XAlarmId.ESTOP, AlarmCategory.PLCAlarm, "Emergency", "","Stop");
            RegisterAlarm(XAlarmId.DOOR_OPEN, AlarmCategory.SafetyDoorAlarm, "Safetydoor", "", "Stop");
            RegisterAlarm(XAlarmId.CURTAIN_ACT, AlarmCategory.SafetyDoorAlarm, "Curtain sensor", "", "Stop");
            RegisterAlarm(XAlarmId.AXIS_SERVON_FAIL, AlarmCategory.PLCAlarm, "Axis Servo On Fail", "", "Stop");
            RegisterAlarm(XAlarmId.AXIS_ASTP, AlarmCategory.PLCAlarm, "Axis Negative Limit", "", "Stop");
            RegisterAlarm(XAlarmId.AXIS_ALM, AlarmCategory.PLCAlarm, "Axis Alarm", "", "Stop");
            RegisterAlarm(XAlarmId.AXIS_PEL, AlarmCategory.PLCAlarm, "Axis Positive Limit", "", "Stop");
            RegisterAlarm(XAlarmId.AXIS_MEL, AlarmCategory.PLCAlarm, "Axis Alarm", "", "Stop");
            RegisterAlarm(XAlarmId.AXIS_POSERROR, AlarmCategory.PLCAlarm, "Axis Alarm", "", "Stop");
            RegisterAlarm(XAlarmId.WAITDI_TIMEOUT, AlarmCategory.PLCAlarm, "DI Error", "", "Stop");
            RegisterAlarm(XAlarmId.CARD_INIT_FAIL, AlarmCategory.ConfigAlarm, "Init Card Err", "", "Stop");
            RegisterAlarm(XAlarmId.CARD_LOAD_PARAM_FAIL, AlarmCategory.ConfigAlarm, "Load Card paramter Err", "", "Stop");
            RegisterAlarm(XAlarmId.AIR_LOW, AlarmCategory.PLCAlarm, "Supply Air Err", "", "Stop");
            RegisterAlarm(XAlarmId.Parameter_Abnormality, AlarmCategory.ConfigAlarm, XAlarmId.Parameter_Abnormality.ToString(), "", "Stop");
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
        SafetyDoorAlarm,
        PLCAlarm,
        CCDAlarm,
        ScanerAlarm,
        ConfigAlarm
    }

    public enum XAlarmId
    {
        //NONE,
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

        // CCD
        CONNECT_CCD_FAIL =20000,
        OUT_OF_STANDARD_SPEC,
        // SCANNER
        CONNECT_SCANNER_FAIL = 20100,
        OUT_OF_LENGHT,
        SN_NOT_MATCH,
        //PLC
        CONNECT_PLC_FAILURE = 20200,
        WRITE_PLC_FAILURE,
        READ_PLC_FAILUTE


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
