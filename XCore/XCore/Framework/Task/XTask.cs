using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.IO;
using APS168_W32;
using APS_Define_W32;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace XCore
{
    public abstract class XTask : XTaskEventHandler
    {
        public event Action<string, Color> OnStep;

        private Thread _thread;
        protected XMove xMove;
        private XSetDo xSetDo; 
        private XWaitDi xWaitDi;
        private XStation xStation;
        private Dictionary<int, XAxis> axisMap = new Dictionary<int, XAxis>();
        private Dictionary<int, XAxis> positionTableAxisMap = new Dictionary<int, XAxis>();
        private Dictionary<int, XDo> doMap = new Dictionary<int, XDo>();
        private Dictionary<int, XDi> diMap = new Dictionary<int, XDi>();
        private int stationId;
        private string logpath;
        protected bool StopRun = false;
        protected bool PauseRun = false;
        protected bool mIsExecuatingAuto = false;

        public bool IsExecuatingAuto { get { return mIsExecuatingAuto; } }
        

        //单位：ms
        [DllImport("kernel32", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetTickCount();

        protected int LastStartTime;
        protected int StartTimeForEmptyRun;

        public static EventHandler OnPauseActive;

        public static EventHandler OnStopActive;

        
        public XTask()
        {
            Z_PositionAxisIdIndex = -1;
        }
        public XTask(string logpath):this()
        {
            this.logpath = logpath;
        }
        public string LogPath
        {
            get { return logpath; }
            set { logpath = value; }
        }
        public int TaskId { get; set; }

        public string Name { get; set; }

        public int StationId
        {
            get { return this.stationId; }
            set
            {
                this.stationId = value;
                xStation = XStationManager.Instance.FindStationById(stationId);
                xMove = new XMove(xStation);
                xSetDo = new XSetDo(xStation);
                xWaitDi = new XWaitDi(xStation);
            }
        }

        public void SetStep(string step, Color color)
        {
            if (OnStep != null)
            {
                string showStr = MultiLanguage.GetMessage(step);
                OnStep(step, color);
            }
        }

        public void SetStep(string step, Color color, bool preTranslate)
        {
            if (preTranslate)
            {
                if (OnStep != null)
                    OnStep(step, color);
            }
            else
            {
                if (OnStep != null)
                {
                    string showStr = MultiLanguage.GetMessage(step);
                    OnStep(step, color);
                }
            }
        }

        public void isSysControlMove(bool isSys)
        {
            xMove.IsSysControl = isSys;
        }

        public double Z_Safe { get; set; }

        public int Z_PositionAxisIdIndex { get; set; }

        #region Device

        public void RegisterAxis(int axisSetId, bool IsShownInPositionTable = true)
        {
            if (axisMap.ContainsKey(axisSetId))
            {
                return;
            }
            axisMap.Add(axisSetId, XDevice.Instance.FindAxisById(axisSetId));
            XDevice.Instance.FindAxisById(axisSetId).TaskId = TaskId;

            if (IsShownInPositionTable)
            {
                if (positionTableAxisMap.ContainsKey(axisSetId))
                {
                    return;
                }
                PositionTableAxisMap.Add(axisSetId, XDevice.Instance.FindAxisById(axisSetId));
            }
        }

        public void RegisterDo(int doSetId)
        {
            if (doMap.ContainsKey(doSetId))
            {
                return;
            }
            doMap.Add(doSetId, XDevice.Instance.FindDoById(doSetId));
            XDevice.Instance.FindDoById(doSetId).TaskId = TaskId;
        }

        public void RegisterDi(int diSetId)
        {
            if (diMap.ContainsKey(diSetId))
            {
                return;
            }
            diMap.Add(diSetId, XDevice.Instance.FindDiById(diSetId));
            XDevice.Instance.FindDiById(diSetId).TaskId = TaskId;
        }

        public Dictionary<int, XAxis> AxisMap
        {
            get { return this.axisMap; }
        }

        public Dictionary<int, XAxis> PositionTableAxisMap
        {
            get { return this.positionTableAxisMap; }
        }

        public Dictionary<int, XDo> DoMap
        {
            get { return this.doMap; }
        }

        public Dictionary<int, XDi> DiMap
        {
            get { return this.diMap; }
        }

        #endregion
       

        #region HandleEvent

        public override int HandleEvent(XEvent xEvent)
        {
            if (XEventID.STOPMUSTRESET == xEvent.EventID)   // 等待执行完成
            {
                StopRun = true;
                LastStartTime = GetTickCount();
                do
                {
                    if (GetTickCount() - LastStartTime > 3000)
                        break;
                    Thread.Sleep(0);
                } while (mIsExecuatingAuto);
            }
            xMove.HandleEvent(xEvent);
            xSetDo.HandleEvent(xEvent);
            xWaitDi.HandleEvent(xEvent);
            switch (xEvent.EventID)
            {
                case XEventID.ESTOP:
                case XEventID.STOPMUSTRESET:
                    Exit();
                    break;
                case XEventID.PAUSE:
                    PauseActive();
                    break;
                case XEventID.CONTINUE:
                    ContinueActive();
                    ClearAlarm();
                    break;
            }

            if (xEvent.EventArgs != null)
            {
                switch (xEvent.EventArgs.AlarmLevel)
                {
                    case (int)XAlarmLevel.PAUSE:
                        PauseActive();
                        break;

                }
            }
            
            return 0;
        }

        #endregion


        /// <summary>
        /// 启动，调用Running
        /// </summary>
        public void Start(object runMode)
        {
            if (_thread != null)
            {
                _thread.Abort();
            }

            _thread = new Thread(new ParameterizedThreadStart(Running));
            _thread.IsBackground = true;
            StopRun = false;
            PauseRun = false;
            SetStep("运行中", Color.Green);
            _thread.Start(runMode);


        }
        /// <summary>
        /// 复位，调用Homing
        /// </summary>
        public void Reset()
        {
            if (_thread != null)
            {
                _thread.Abort();
            }
            _thread = new Thread(new ThreadStart(Homing));
            _thread.IsBackground = true;
            StopRun = false;
            PauseRun = false;
            _thread.Start();
        }
        /// <summary>
        /// 任务线程取消
        /// </summary>
        public void Cancel()
        {
            //byyk 急停停不了供料机的轴
            foreach (XAxis axis in axisMap.Values)
            {
                axis.EStop();
            }
            if (_thread != null)
            {
                _thread.Abort();
            }
            // 任务停止
            SetStation_StateStop();
            SetStep("停止运行", Color.Green);
        }

        /// <summary>
        /// 任务初始化
        /// </summary>
        public virtual void Initialize()
        {

        }
        /// <summary>
        /// 任务退出
        /// </summary>
        public virtual void Exit()
        {
            Cancel();
        }
        /// <summary>
        /// 任务运行，需用户重写
        /// </summary>
        protected virtual void Running(object runMode) { }
        /// <summary>
        /// 任务复位，需用户重写
        /// </summary>
        protected virtual void Homing() 
        {
        }

        protected virtual void PauseActive()        // 暂停激活
        {
            PauseRun = true;
        }
        
        protected virtual void ContinueActive()     // 继续运行
        {
            PauseRun = false;
            LastStartTime = GetTickCount();         // 重设上个状态时间
        }


        #region 任务所在工站的相应操作

        /// <summary>
        /// 设置工站的状态：等待运行，复位完成后调用
        /// 对应用户控件XStationStateBar显示
        /// </summary>
        protected void SetStation_StateWaitRun()
        {
            xStation.SetState(XStationState.WAITRUN);
        }

        protected void SetStation_StateStop()
        {
            xStation.SetState(XStationState.STOP);
        }


        #region 秒表，用以记录Task所在Station的CycleTime，开始和停止需用户操作
        /// <summary>
        /// 秒表开始
        /// </summary>
        protected void SetStation_StopWatchStart()
        {
            xStation.StopWatch_Start();
        }
        /// <summary>
        /// 秒表复位
        /// </summary>
        protected void SetStation_StopWatchReset()
        {
            xStation.StopWatch_Reset();
        }
        /// <summary>
        /// 秒表停止
        /// </summary>
        protected void SetStation_StopWatchStop()
        {
            xStation.StopWatch_Stop();
        }
        /// <summary>
        /// 秒表重启动
        /// </summary>
        protected void SetStation_StopWatchRestart()
        {
            xStation.StopWatch_ReStart();
        }
        /// <summary>
        /// 获取秒表开始后的毫秒数
        /// </summary>
        /// <returns></returns>
        protected double GetStation_StopWatchElapsedMilliseconds()
        {
            return xStation.ElapsedMilliseconds;
        }

        #endregion

        #endregion

        #region 发送告警

        private XAlarmEventArgs _alarmEventArgs = new XAlarmEventArgs(Int32.MinValue, "NONE", "NONE");
        /// <summary>
        /// 发送告警
        /// </summary>
        /// <param name="alarmLevel">报警等级</param>
        /// <param name="alarmCode">报警代号</param>
        /// <param name="append">用户自定义的额外报警信息</param>
        private void PostAlarm(XAlarmLevel alarmLevel, XAlarmEventArgs args, string append = "")
        {
            PostEvent(xStation, alarmLevel, args, append);
        }
        //ReportAlarm(XAlarmLevel.TIP, (int)AlarmCode.获取前站数据失败, AlarmCategory.TRAY.ToString(),
        //                            AlarmCode.获取前站数据失败.ToString(), "PAM1");
        protected void ReportAlarm(XAlarmLevel level, int code, string category, string description, string append = "")
        {
            _alarmEventArgs.Code = code;
            _alarmEventArgs.Category = category;
            _alarmEventArgs.Description = description;
            PostAlarm(level, _alarmEventArgs, append);
        }

        protected void ClearAlarm()
        {
            //if (this.stationId == XAlarmReporter.Instance.CurrentAlarm.StationId)
            {
                XAlarmReporter.Instance.ClearAlarm();
            }
        }

        #endregion

        #region 轴操作

        /// <summary>
        /// 从对应的PositionTable获取点位
        /// </summary>
        /// <param name="positionName"></param>
        /// <returns></returns>
        protected XPosition GetPositon(string positionName)
        {
            //add by tang
            if (XPositionManager.Instance.PositionSet.ContainsKey(TaskId) &&
                XPositionManager.Instance.PositionSet[TaskId].PositionMap.ContainsKey(positionName))
            {
                return XPositionManager.Instance.PositionSet[TaskId].PositionMap[positionName];
            }
            return null;
        }

        /// <summary>
        /// 从对应的PositionTable获取点位
        /// </summary>
        /// <param name="positionName"></param>
        /// <returns></returns>
        protected XPosition GetPositon(int taskId, string positionName)
        {
			if (XPositionManager.Instance.PositionSet.ContainsKey(taskId) &&
                XPositionManager.Instance.PositionSet[taskId].PositionMap.ContainsKey(positionName))
            {
                return XPositionManager.Instance.PositionSet[taskId].PositionMap[positionName];
            }
            return null;        }
        protected void GetPosData(string posName, int count, out double[] dataArr)  // @sjx add
        {
            double[] posData = new double[count];
            try
            {
                XPosition pos = GetPositon(posName);
                if (pos == null)
                {
                    dataArr = posData;
                    return;
                }
                for (int i = 0; i < count; i++)
                {
                    posData[i] = pos.Positions[i];
                }
            }
            catch
            { }

            dataArr = posData;
        }

        protected void GetPosData(int taskId, string posName, int count, out double[] dataArr)  // @sjx add
        {
            double[] posData = new double[count];
            try
            {
                XPosition pos = GetPositon(taskId, posName);
                if (pos == null)
                {
                    dataArr = posData;
                    return;
                }
                for (int i = 0; i < count; i++)
                {
                    posData[i] = pos.Positions[i];
                }
            }
            catch
            { }

            dataArr = posData;
        }
        /// <summary>
        /// 多轴使能
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="sts"></param>
        /// <returns></returns>
        protected int SetServo(int[] axisId, bool sts)
        {
            int ret;
            for (int i = 0; i < axisId.Length; i++)
            {
                ret = XDevice.Instance.FindAxisById(axisId[i]).SetServo(sts);
                if (ret != 0)
                {
                    return -1;
                }
            }
            return 0;

        }
        /// <summary>
        /// 单轴使能
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="sts"></param>
        /// <returns></returns>
        protected int SetServo(int axisId, bool sts)
        {
            return XDevice.Instance.FindAxisById(axisId).SetServo(sts);
        }
        /// <summary>
        /// 多轴回零
        /// </summary>
        /// <param name="axisId"></param>
        /// <returns></returns>
        protected int MoveHome(int[] axisId)
        {
            return xMove.MoveHome(axisId, axisId.Length);
        }
        /// <summary>
        /// 单轴回零
        /// </summary>
        /// <param name="axisId"></param>
        /// <returns></returns>
        protected int MoveHome(int axisId)
        {
            // 因为安全问题，单轴回零可能会涉及其他轴的运动，不用原始的XMove的回零
            //return xMove.MoveHome(new int[] { axisId }, 1);
            return XDevice.Instance.FindAxisById((int)axisId).GoHome();
        }
        /// <summary>
        /// 多轴绝对运动，不同速度
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="pos"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        protected int MoveAbs(int[] axisId, double[] pos, double[] vel, bool checkLmt = true)
        {
            return xMove.MoveAbs(axisId, pos, vel, axisId.Length, checkLmt);
        }
        /// <summary>
        /// 单轴绝对运动
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="pos"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        protected int MoveAbs(int axisId, double pos, double vel, bool checkLmt = true)
        {
            return xMove.MoveAbs(new int[] { axisId }, new double[] { pos }, new double[] { vel }, 1, checkLmt);
        }
        /// <summary>
        /// 多轴绝对运动，相同速度
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="pos"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        protected int MoveAbs(int[] axisId, double[] pos, double vel, bool checkLmt = true)
        {
            double[] vels = new double[axisId.Length];
            for (int i = 0; i < axisId.Length; i++)
            {
                vels[i] = vel;
            }
            return xMove.MoveAbs(axisId, pos, vels, axisId.Length, checkLmt);
        }
        /// <summary>
        /// 多轴根据示教点位运动，不同速度
        /// </summary>
        /// <param name="position"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        protected int MovePosition(XPosition position, double[] vel, bool checkLmt = true)
        {
            int[] axisId = position.AxisId;
            double[] pos = position.Positions;
            return xMove.MoveAbs(axisId, pos, vel, position.Count, checkLmt);
        }
        /// <summary>
        /// 多轴根据示教点位运动，相同速度
        /// </summary>
        /// <param name="position"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        protected int MovePosition(XPosition position, double vel, bool checkLmt = true)
        {
            int[] axisId = position.AxisId;
            double[] pos = position.Positions;
            int count = position.Count;
            double[] vels = new double[count];
            for (int i = 0; i < count; i++)
            {
                vels[i] = vel;
            }
            return xMove.MoveAbs(axisId, pos, vels, count, checkLmt);
        }
        /// <summary>
        /// 多轴相对运动，不同速度
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="distance"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        protected int MoveRel(int[] axisId, double[] distance, double[] vel, bool checkLmt = true)
        {
            return xMove.MoveRel(axisId, distance, vel, axisId.Length, checkLmt);
        }
        /// <summary>
        /// 单轴相对运动
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="distance"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        protected int MoveRel(int axisId, double distance, double vel, bool checkLmt = true)
        {
            return xMove.MoveRel(new int[] { axisId }, new double[] { distance }, new double[] { vel }, 1, checkLmt);
        }
        /// <summary>
        /// 单轴JOG运动
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="distance"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        protected int MoveJog(int axisId,  int  isStart, bool checkLmt = true)
        {
            return xMove.MoveJog(new int[] { axisId }, new int[] { isStart }, 1, checkLmt);
        }
        /// <summary>
        /// 多轴相对运动，相同速度
        /// </summary>
        /// <param name="axisId"></param>
        /// <param name="distance"></param>
        /// <param name="vel"></param>
        /// <returns></returns>
        protected int MoveRel(int[] axisId, double[] distance, double vel, bool checkLmt = true)
        {
            double[] vels = new double[axisId.Length];
            for (int i = 0; i < axisId.Length; i++)
            {
                vels[i] = vel;
            }
            return xMove.MoveRel(axisId, distance, vels, axisId.Length, checkLmt);
        }

        /// <summary>
        /// 运动停止
        /// </summary>
        /// <returns></returns>
        protected int MoveStop()
        {
            return xMove.MoveStop();
        }
        /// <summary>
        /// 判断规划运动是否完成，每次运动时必须调用
        /// </summary>
        /// <returns></returns>
        protected bool WaitMoveDone(int timeout = -1)  // @sjx add
        {
            return xMove.WaitEvent(timeout) == 0 ? true : false;
        }

        /// <summary>
        /// 判断实际运动是否完成，每次运动时必须调用
        /// </summary>
        /// <returns></returns>
        //protected bool WaitPracMoveDone(int timeout = -1)
        //{
        //    return xMove.WaitEvent(timeout, true) == 0 ? true : false;
        //}

        protected int SetAxisAccAndDec(int axisId, double acc, double dec)
        {
            return XDevice.Instance.FindAxisById(axisId).SetAxisAccAndDec(acc, dec);
        }
        protected int SetAxisJogAccAndDec(int axisId, int mode ,int dir, double acc, double dec,int vel)
        {
            return XDevice.Instance.FindAxisById(axisId).APS_SetAxisJogParam (mode ,dir,acc,dec,vel);
        }
        protected int SetStopDec(int axisId, double dec)
        {
            return XDevice.Instance.FindAxisById(axisId).SetStopDec(dec);
        }


        #endregion

        #region Do操作

        /// <summary>
        /// 设置多个Do状态，不同状态
        /// </summary>
        /// <param name="doId"></param>
        /// <param name="doStsType"></param>
        /// <returns></returns>
        protected int SetDo(int[] doId, DOSTSTYPE[] doStsType)
        {
            return xSetDo.SetDo(doId, doStsType, doId.Length);
        }
        /// <summary>
        /// 设置单个Do状态
        /// </summary>
        /// <param name="doId"></param>
        /// <param name="doStsType"></param>
        /// <returns></returns>
        protected int SetDo(int doId, DOSTSTYPE doStsType)
        {
            return xSetDo.SetDo(new int[] { doId }, new DOSTSTYPE[] { doStsType }, 1);
        }
        /// <summary>
        /// 设置多个Do状态，相同状态
        /// </summary>
        /// <param name="doId"></param>
        /// <param name="doStsType"></param>
        /// <returns></returns>
        protected int SetDo(int[] doId, DOSTSTYPE doStsType)
        {
            DOSTSTYPE[] types = new DOSTSTYPE[doId.Length];
            for (int i = 0; i < doId.Length; i++)
            {
                types[i] = doStsType;
            }
            return xSetDo.SetDo(doId, types, doId.Length);
        }

        #endregion

        #region Di操作

        /// <summary>
        /// 等待多个Di信号，不同状态，超时后暂停或停止
        /// </summary>
        /// <param name="diId"></param>
        /// <param name="diStsType"></param>
        /// <param name="timeout">超时时间，单位毫秒</param>
        /// <param name="timeoutToBeContinue">若为true，超时后停止，必须复位；若为false，超时后暂停，可继续</param>
        /// <returns></returns>
        protected bool WaitDi(int[] diId, DISTSTYPE[] diStsType, int timeout, bool timeoutToBeContinue = false, string append = "")
        {
            return xWaitDi.WaitDi(diId, diStsType, diId.Length, timeout, timeoutToBeContinue, append) == 0 ? true : false;
        }
        /// <summary>
        /// 等待多个Di信号，超时后暂停或停止
        /// </summary>
        /// <param name="diId"></param>
        /// <param name="diStsType"></param>
        /// <param name="timeout">超时时间，单位毫秒</param>
        /// <param name="timeoutToBeContinue">若为false，超时后停止，必须复位；若为true，超时后暂停，可继续</param>
        /// <returns></returns>
        protected bool WaitDi(int diId, DISTSTYPE diStsType, int timeout, bool timeoutToBeContinue = false, string append = "")
        {
            return xWaitDi.WaitDi(new int[] { diId }, new DISTSTYPE[] { diStsType }, 1, timeout, timeoutToBeContinue, append) == 0 ? true : false;
        }
        /// <summary>
        /// 等待多个Di信号，相同状态，超时后暂停或停止
        /// </summary>
        /// <param name="diId"></param>
        /// <param name="diStsType"></param>
        /// <param name="timeout">超时时间，单位毫秒</param>
        /// <param name="timeoutToBeContinue">若为true，超时后停止，必须复位；若为false，超时后暂停，可继续</param>
        /// <returns></returns>
        protected bool WaitDi(int[] diId, DISTSTYPE diStsType, int timeout, bool timeoutToBeContinue = false, string append = "")
        {
            DISTSTYPE[] types = new DISTSTYPE[diId.Length];
            for (int i = 0; i < diId.Length; i++)
            {
                types[i] = diStsType;
            }
            return xWaitDi.WaitDi(diId, types, diId.Length, timeout, timeoutToBeContinue, append) == 0 ? true : false;
        }
        /// <summary>
        /// 等待多个Di信号，不同状态，超时后无动作
        /// </summary>
        /// <param name="diId"></param>
        /// <param name="diStsType"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected bool WaitDiSignal(int[] diId, DISTSTYPE[] diStsType, int timeout)
        {
            return xWaitDi.WaitDiSignal(diId, diStsType, diId.Length, timeout) == 0 ? true : false;
        }
        /// <summary>
        /// 等待单个Di信号，超时后无动作
        /// </summary>
        /// <param name="diId"></param>
        /// <param name="diStsType"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected bool WaitDiSignal(int diId, DISTSTYPE diStsType, int timeout)
        {
            return xWaitDi.WaitDiSignal(new int[] { diId }, new DISTSTYPE[] { diStsType }, 1, timeout) == 0 ? true : false;
        }
        /// <summary>
        /// 等待多个Di信号，相同状态，超时后无动作
        /// </summary>
        /// <param name="diId"></param>
        /// <param name="diStsType"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        protected bool WaitDiSignal(int[] diId, DISTSTYPE diStsType, int timeout)
        {
            DISTSTYPE[] types = new DISTSTYPE[diId.Length];
            for (int i = 0; i < diId.Length; i++)
            {
                types[i] = diStsType;
            }
            return xWaitDi.WaitDiSignal(diId, types, diId.Length, timeout) == 0 ? true : false;
        }

        #endregion

        protected int APS_pt_start(int[] axisId, int axisCount, int ptbId, int ptbCount)
        {
            return xMove.APS_pt_start(axisId, axisCount, ptbId, ptbCount);
        }

        public virtual void ShowErrorMsg(string errMsg, string append)
        { 
        }


    }
    
}
