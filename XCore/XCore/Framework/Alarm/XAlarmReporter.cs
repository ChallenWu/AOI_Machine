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
                string okOptiontext = "Confirm", string cancelOptionText = "", string ignoreOptionText = "")
        {
            description = MultiLanguage.GetMessage(description);
            okOptiontext = MultiLanguage.GetMessage(okOptiontext);
            cancelOptionText = MultiLanguage.GetMessage(cancelOptionText);
            ignoreOptionText = MultiLanguage.GetMessage(ignoreOptionText);
           
            //string solution = 
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
            RegisterAlarm(XAlarmId.ESTOP, AlarmCategory.SafetyError, "EMG IS PRESSED", "Retry","Stop","Continue");
            RegisterAlarm(XAlarmId.DOOR_OPEN, AlarmCategory.SafetyError, "SAFEDOOR IS OPENED", "", "Stop");
            RegisterAlarm(XAlarmId.CURTAIN_ACT, AlarmCategory.SafetyError, "CURTAIN SENSOR IS ACTIVE", "", "Stop");
            RegisterAlarm(XAlarmId.AXIS_SERVON_FAIL, AlarmCategory.MotionError, "SERVO ON FAIL", "", "Stop");
            RegisterAlarm(XAlarmId.AXIS_ASTP, AlarmCategory.MotionError, "AXIS MOTION ABNORMAL", "", "Stop");
            RegisterAlarm(XAlarmId.AXIS_ALM, AlarmCategory.MotionError, "SERVO ALARM", "", "Stop");
            RegisterAlarm(XAlarmId.AXIS_PEL, AlarmCategory.MotionError, "GAP LIMIT+", "", "Stop");
            RegisterAlarm(XAlarmId.AXIS_MEL, AlarmCategory.MotionError, "GAP LIMIT-", "", "Stop");
            RegisterAlarm(XAlarmId.AXIS_POSERROR, AlarmCategory.MotionError, "AXIS POS ERROR", "", "Stop");
            RegisterAlarm(XAlarmId.WAITDI_TIMEOUT, AlarmCategory.SensorError, "TIME OUT INPUT", "", "Stop");
            RegisterAlarm(XAlarmId.AIR_LOW, AlarmCategory.AirError, "AIR SUPPLY IS NOT ENOUGH", "Stop");
            #endregion

            #region CCD
            RegisterAlarm(XAlarmId.CCD_ERROR, AlarmCategory.VisionError, "CCD ERROR. INSPECTION ASSY PART FAIL", "", "STOP");
            RegisterAlarm(XAlarmId.CAPTURE_IMAGE_FAIL, AlarmCategory.VisionError, "CAPTURE IMAGE FAIL", "", "STOP");
            RegisterAlarm(XAlarmId.CONNECT_CCD_FAIL, AlarmCategory.VisionError, "CANT CONNECT TO CCD", "", "STOP");
            #endregion

            #region SCANNER
            
            RegisterAlarm(XAlarmId.READ_MAIN_CODE, AlarmCategory.ScanningError, "SCANNER READ PN FAIL", "", "PUT NG");
            RegisterAlarm(XAlarmId.READ_CODE_FAIL, AlarmCategory.ScanningError, "SCANNER READ SN FAIL", "", "PUT NG");
            RegisterAlarm(XAlarmId.SN_DUMMY, AlarmCategory.ScanningError, "DUMMY THE LAST PRODUCT", "", "PUT NG");
            #endregion

            #region PLC

            RegisterAlarm(XAlarmId.CONNECT_PLC_FAIL, AlarmCategory.PLCError, "CANT CONNECT TO PLC", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.WRITE_DOWN_PLC_ERROR, AlarmCategory.PLCError, "WRITE DATA TO PLC ERROR", "", "CLEAR ALARM", "");


            RegisterAlarm(XAlarmId.LEFT_STOPPER_ORG_ERR, AlarmCategory.CylinderError, "M5010 : ORIGIN SENSOR OF BLOCKER CYLINDER HAS ISSUES ", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.LEFT_STOPPER_END_ERR, AlarmCategory.CylinderError, "M5011 : END SENSOR OF BLOCKER CYLINDER HAS ISSUES", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.RIGHT_STOPPER_ORG_ERR, AlarmCategory.CylinderError, "M5012 : END SENSOR OF UP/DOWN CYLINDER HAS ISSUES", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.RIGHT_STOPPER_END_ERR, AlarmCategory.CylinderError, "M5013 : ORIGIN SENSOR OF UP/DOWN BLOCKER CYLINDER HAS ISSUES", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.LEFT_CLAMP_ORG_ERR, AlarmCategory.CylinderError, "M5014 : ORIGIN SENSOR OF LEFT CLAMP CYLINDER HAS ISSUES", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.LEFT_CLAMP_END_ERR, AlarmCategory.CylinderError, "M5015 : END SENSOR OF LEFT CLAMP CYLINDER HAS ISSUE", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.RIGHT_CLAMP_ORG_ERR, AlarmCategory.CylinderError, "M5016 : ORIGIN SENSOR OF RIGHT CLAMP CYLINDER HAS ISSUES", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.RIGHT_CLAMP_END_ERR, AlarmCategory.CylinderError, "M5017: END SENSOR OF RIGHT CLAMP CYLINDER HAS ISSUES", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.CCD_TURN_ORG_ERR, AlarmCategory.CylinderError, "M5018: ORIGIN SENSOR OF CCD TURN CYLINDER HAS ISSUES", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.CCD_TURN_END_ERR, AlarmCategory.CylinderError, "M5019: END SENSOR OF CCD TURN CYLINDER HAS ISSUES", "", "CLEAR ALARM");


            RegisterAlarm(XAlarmId.X_AXIS_ERROR, AlarmCategory.MotionError, "M5020 : AXIS X ERROR", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.Y_AXIS_ERROR, AlarmCategory.MotionError, "M5021 : AXIS Y ERROR", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.Z_AXIS_ERROR, AlarmCategory.MotionError, "M5022 : AXIS Z ERORR", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.R_AXIS_ERROR, AlarmCategory.MotionError, "M5023 : AXIS R ERROR", "", "CLEAR ALARM");

            RegisterAlarm(XAlarmId.DOOR_1_ERROR, AlarmCategory.SafetyError, "M5030: SAFEDOOR 1 IS OPENED", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.DOOR_2_ERROR, AlarmCategory.SafetyError, "M5031: SAFEDOOR 2 IS OPENED", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.DOOR_3_ERROR, AlarmCategory.SafetyError, "M5032: SAFEDOOR 3 IS OPENED", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.DOOR_4_ERROR, AlarmCategory.SafetyError, "M5033: SAFEDOOR 4 IS OPENED", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.CURTAIN_1_ERROR, AlarmCategory.SafetyError, "M5034: CURTAIN SENSOR INPUT HAS ISSUE", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.CURTAIN_2_ERROR, AlarmCategory.SafetyError, "M5035: CURTAIN SENSOR OUTPUT HAS ISSUE", "", "CLEAR ALARM");

            #endregion

            RegisterAlarm(XAlarmId.INIT_DETECT_SEN_ERR, AlarmCategory.PLCError, "M5040: INITIAZATION DECTECT SENSOR ERROR ", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.INIT_OVERTIME, AlarmCategory.PLCError, "M5041: INITIAZATION OVERTIME ERROR", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.INIT_Z_OFF, AlarmCategory.MotionError, "M5042: INITIAZATION AXIS Z OFF", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.INIT_W_OFF, AlarmCategory.MotionError, "M5043: INITIAZATION AXIS W OFF", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.INIT_X_OFF, AlarmCategory.MotionError, "M5044: INITIAZATION AXIS X OFF", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.INIT_Y_OFF, AlarmCategory.MotionError, "M5045: INITIAZATION AXIS Y OFF", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.INIT_X24_ON, AlarmCategory.PLCError, "M5046: INITIAZATION X24 ON ERROR", "", "CLEAR ALARM");

            RegisterAlarm(XAlarmId.VISION_OVERTIME, AlarmCategory.PLCError, "M5050: VISION OVERTIME ", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.SCAN_CODE_OVERTIME, AlarmCategory.PLCError, "M5051: SCANCODE OVERTIME", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.SCAN_CODEMES_OVERTIME, AlarmCategory.MotionError, "M5052: SCANCODE MES OVERTIME", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.CAM_NG, AlarmCategory.MotionError, "M5053: CAM NG", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.MES_NG, AlarmCategory.MotionError, "M5054: MES NG", "", "CLEAR ALARM");
            RegisterAlarm(XAlarmId.SCAN_CODE_NG, AlarmCategory.MotionError, "M5055: SCAN CODE NG", "", "CLEAR ALARM");

            #region MES
            RegisterAlarm(XAlarmId.WRONG_STATION, AlarmCategory.MES_Error, "PRODUCT STATION IS INCORRECT", "", "PUT NG");
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
                                case 0:
                                    break;
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

                                case (int)XAlarmId.DOOR_OPEN:
                                    append = "Safety door" +"\"" + xEvent.EventArgs.StringValue  + "\""+ "Opened";
                                    description = SystemAlarms[(XAlarmId)alarmId].Description + append;
                                    break;
                                case (int)XAlarmId.AIR_LOW:
                                    append = "Positive air pressure is lower than the standard value";
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
        MES_Error,
        SafetyError,
        ScanningError,
        VisionError,
        CylinderError,
        MotionError,
        SensorError,
        PDIError,
        AirError,
        PLCError
    }

    public enum XAlarmId
    {
        NONE,
        // System
        ESTOP = 10001,
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
        AIR_LOW,
        RST,
        WAIT_MOTION_TIMEOUT,




        /// <summary>
        /// CCD
        /// </summary>
        CONNECT_CCD_FAIL,
        CCD_ERROR,
        CAPTURE_IMAGE_FAIL,
        
        /// <summary>
        /// SCANNER
        /// </summary>
        CONNECT_BARCODE_FAIL,
        READ_CODE_FAIL,
        READ_MAIN_CODE,
        SN_DUMMY,
        CONNECT_PLC_FAIL,
        WRITE_DOWN_PLC_ERROR,


        /// <summary>
        /// MES
        /// </summary>
        WRONG_STATION,
        UPLOAD_TO_MES_FAIL,

        //Cylinder
        LEFT_STOPPER_ORG_ERR = 5010,
        LEFT_STOPPER_END_ERR,
        RIGHT_STOPPER_ORG_ERR,
        RIGHT_STOPPER_END_ERR,
        LEFT_CLAMP_ORG_ERR,
        LEFT_CLAMP_END_ERR,
        RIGHT_CLAMP_ORG_ERR,
        RIGHT_CLAMP_END_ERR,
        CCD_TURN_ORG_ERR,
        CCD_TURN_END_ERR,

        /// <summary>
        /// PLC
        /// </summary>
        X_AXIS_ERROR = 5020,
        Y_AXIS_ERROR,
        Z_AXIS_ERROR,
        R_AXIS_ERROR,

        /// <summary>
        /// SafetyError
        /// </summary>
        DOOR_1_ERROR = 5030,
        DOOR_2_ERROR,
        DOOR_3_ERROR,
        DOOR_4_ERROR,
        CURTAIN_1_ERROR,
        CURTAIN_2_ERROR,
        //INITIAL PROCESS
        INIT_DETECT_SEN_ERR = 5040,
        INIT_OVERTIME,
        INIT_Z_OFF,
        INIT_W_OFF,
        INIT_X_OFF,
        INIT_Y_OFF,
        INIT_X24_ON,
        //PC ERROR
        VISION_OVERTIME = 5050,
        SCAN_CODE_OVERTIME = 5051,
        SCAN_CODEMES_OVERTIME,
        CAM_NG,
        MES_NG,
        SCAN_CODE_NG,
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
