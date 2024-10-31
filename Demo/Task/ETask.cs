using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using Demo;
using HB_IWatch;
using Demo.UserControls;
using BoTech;

namespace Demo.Task
{
    public enum WP_STATE
    {
        PART_FREE = 0,   // 没有产品
        PART_NEW = 1,    // 载具从上工位进入到该工位
        PART_WORKED = 4, // 拿到结果后，改为Worked
        PART_FOR_WORK = 5  // 上料OK信号
    }
    public enum Home_State
    {
        Homing,
        Failure,
        Successfull
    }

    public enum StopState //阻挡状态
    {
        Stop = 0,
        Unstop
    }

    class ETask : XTask
    {
        protected object m_runStep = 0;
        protected WP_STATE m_partState = WP_STATE.PART_FREE;
        public static int homeDoneTaskNum = 0;
        //Số task thực hiện
        public static int RequestHomeTaskNum
        {
            //get { return Globals.SettingOption.GetStationCellAssignMode() != 0 ? 3 : 4; }   
            get { return 1; }
        }

        public WP_STATE PartState
        {
            get { return m_partState; }
            set { m_partState = value; }
        }

        public ETask(string path)
            : base(path)
        {
        }

        string logMessage = "";
        protected void WriteLog(string message)
        {
            //avoid same content
            if (logMessage == message)
                return;
            logMessage = message;
            string path = LogPath + ((TaskId)TaskId).ToString() + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            string str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " => " + message;
            CsvServer.Instance.WriteLine(path, str);
        }

        
        //Thông báo lỗi protected
        protected DialogResult ShowAlarm(XAlarmId alarmId, string szDetail = "", int timeout = -1)
        {
            //Liệt kê dữ liệu báo lỗi
            string szDescrip;
            string szErrCode;
            string szType;
            string szOk, szCancel, szIgnore;
            
            szDescrip = XAlarmReporter.Instance.SystemAlarms[alarmId].Description;
            szType = XAlarmReporter.Instance.SystemAlarms[alarmId].Category.ToString();
            szErrCode = XAlarmReporter.Instance.SystemAlarms[alarmId].Code.ToString();
            
            if (szDetail != "")
                szDescrip += ":" + MultiLanguage.GetMessage(szDetail);

            szOk = XAlarmReporter.Instance.SystemAlarms[alarmId].OkOptionText;
            szCancel = XAlarmReporter.Instance.SystemAlarms[alarmId].CancelOptionText;
            szIgnore = XAlarmReporter.Instance.SystemAlarms[alarmId].IgnoreOptionText;
            

            string szSeverity = XAlarmReporter.Instance.SystemAlarms[alarmId].AlarmLevel.ToString();
            string szSolution = XAlarmReporter.Instance.SystemAlarms[alarmId].Solution;

            WriteLog("Device error, error code:" + (int)alarmId + ", error message:" + alarmId.ToString() + ", " + szDescrip);

            
            ReportAlarm(XAlarmLevel.TIP, 
                        (int)XAlarmReporter.Instance.SystemAlarms[alarmId].Code,
                        XAlarmReporter.Instance.SystemAlarms[alarmId].Category.ToString(), 
                        szDescrip);
                      
            return HBMachine.Instance.ShowError(szErrCode, szDescrip, szSeverity, szSolution, szType, szOk, szCancel, szIgnore, timeout);
        }

        protected void ShowAlarmAsync(XAlarmId alarmId, string szDetail = "", CallbackAction callbackAction = null)
        {
            string szDescrip, szOk, szCancel, szIgnore;
            szDescrip = XAlarmReporter.Instance.SystemAlarms[alarmId].Description;
            szOk = XAlarmReporter.Instance.SystemAlarms[alarmId].OkOptionText;
            szCancel = XAlarmReporter.Instance.SystemAlarms[alarmId].CancelOptionText;
            szIgnore = XAlarmReporter.Instance.SystemAlarms[alarmId].IgnoreOptionText;

            WriteLog("设备报错, 错误码:" + (int)alarmId + ", 错误信息:" + alarmId.ToString() + ", " + szDescrip);
            ReportAlarm(XAlarmLevel.TIP, (int)XAlarmReporter.Instance.SystemAlarms[alarmId].Code,
                    XAlarmReporter.Instance.SystemAlarms[alarmId].Category.ToString(), szDescrip);
            HBMachine.Instance.ShowWarningAsync(szDescrip, szOk, callbackAction);
        }


        public DISTSTYPE GetDi(DiId id)
        {
            return Motion.GetDi(id);
        }

        public void SetDo(DoId id, DOSTSTYPE sts)
        {
            Motion.SetDo(id, sts);
        }

        public bool WaitDi(DiId[] ids, DISTSTYPE[] expectedStates, int timeOutMs = 5000)
        {
            return Motion.WaitDi(ids, expectedStates, timeOutMs);
        }

        public bool WaitDiWithAlarm(DiId[] ids, DISTSTYPE[] expectedStates, XAlarmId alarmId, int timeOutMs = 5000)
        {
            while (!WaitDi(ids, expectedStates))
            {
                if (ReportAlarm(alarmId) == DialogResult.Cancel)
                {
                    if (!mIsExecuatingAuto)
                        return false;
                }
            }
            return true;
        }

        public bool WaitDiWithAlarm(DiId id, DISTSTYPE expectedState, XAlarmId alarmId, int timeOutMs = 5000)
        {
            return WaitDiWithAlarm(new DiId[] { id }, new DISTSTYPE[] { expectedState }, alarmId, timeOutMs);
        }

        // 等待多个Di中任意一个Di到位
        public bool WaitAnyDiReady(DiId[] ids, DISTSTYPE[] expectedStates, int timeOutMs = 5000)
        {
            return Motion.WaitAnyDiReady(ids, expectedStates, timeOutMs);
        }

        public bool WaitAnyDiReadyWithAlarm(DiId[] ids, DISTSTYPE[] expectedStates, XAlarmId alarmId, int timeOutMs = 5000)
        {
            while (!WaitAnyDiReady(ids, expectedStates))
            {
                if (ReportAlarm(alarmId) == DialogResult.Cancel)
                {
                    if (!mIsExecuatingAuto)
                        return false;
                }
            }
            return true;
        }

        public bool WaitDi(DiId id, DISTSTYPE expectedState, int timeOutMs = 5000)
        {
            return Motion.WaitDi(id, expectedState, timeOutMs);
        }

        public bool WaitDo(DoId ids, DOSTSTYPE expectedStates, int timeOutMs = 5000)
        {
            return Motion.WaitDo(ids, expectedStates, timeOutMs);
        }

        public bool WaitDoWithAlarm(DoId ids, DOSTSTYPE expectedStates, XAlarmId alarmId, int timeOutMs = 5000)
        {

            while (!WaitDo(ids, expectedStates))
            {
                if (ReportAlarm(alarmId) == DialogResult.Cancel)
                {
                    if (!mIsExecuatingAuto)
                        return false;
                }
            }
            return true;
        }

        public bool MoveJogTillDi(AxisId axisId, DiId waitDiId, XAlarmId alarmId, double vel, int timeOutMs = 20000)
        {
            //如果电机位置已经触碰到信号
            if (GetDi(waitDiId) == DISTSTYPE.LOW)
            {
                //电机下降
                if (XDevice.Instance.FindAxisById((int)axisId).MoveJog(1, -vel) != 0)
                {
                    return false;
                }
                //等待信号未被出发
                while (!WaitDi(waitDiId, DISTSTYPE.HIGH, timeOutMs))
                {
                    //超过时间停止电机，报警
                    if (XDevice.Instance.FindAxisById((int)axisId).MoveJog(0, -vel) != 0)
                    {
                        return false;
                    }
                    ReportAlarm(alarmId);
                }
                //碰到未被触发之后停止电机
                if (XDevice.Instance.FindAxisById((int)axisId).MoveJog(0, -vel) != 0)
                {
                    return false;
                }
            }
            //未触碰到信号
            else if (GetDi(waitDiId) == DISTSTYPE.HIGH)
            {
                //电机上升
                if (XDevice.Instance.FindAxisById((int)axisId).MoveJog(1, vel) != 0)
                {
                    return false;
                }
                //等待信号被触碰
                while (!WaitDi(waitDiId, DISTSTYPE.LOW, timeOutMs))
                {
                    //超过时间未触碰信号，停止电机，报警
                    if (XDevice.Instance.FindAxisById((int)axisId).MoveJog(0, vel) != 0)
                    {
                        return false;
                    }
                    ReportAlarm(alarmId);
                }
                //触碰到信号之后停止电机
                if (XDevice.Instance.FindAxisById((int)axisId).MoveJog(0, vel) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool MoveAbsAuto(AxisId axisId, double pos)
        {
            return Motion.MoveAbsAuto(axisId, pos);
        }

        public bool MoveAbsManual(AxisId axisId, double pos)
        {
            return Motion.MoveAbsManual(axisId, pos);
        }

        public bool MoveRelAuto(AxisId axisId, double pos)
        {
            return Motion.MoveRelAuto(axisId, pos);
        }

        public bool MoveRelManual(AxisId axisId, double pos)
        {
            return Motion.MoveRelManual(axisId, pos);
        }

        public bool MoveAbsInSettingVel(AxisId axisId, double pos, double vel)
        {

            return Motion.MoveAbsInSettingVel(axisId, pos, vel);
        }

        public bool MoveRelInSettingVel(AxisId axisId, double pos, double vel)
        {
            return Motion.MoveRelInSettingVel(axisId, pos, vel);
        }

        public bool MoveAbsAuto(AxisId[] axisId, double[] pos)
        {
            return Motion.MoveAbsAuto(axisId, pos);
        }

        public bool MoveAbs(AxisId[] axisId, double[] pos, bool autoMove = true)
        {
            if (autoMove)
                return Motion.MoveAbsAuto(axisId, pos);
            else
                return Motion.MoveAbsManual(axisId, pos);
        }

        public bool MoveAbsManual(AxisId[] axisId, double[] pos)
        {
            return Motion.MoveAbsManual(axisId, pos);
        }

        public bool MoveRelAuto(AxisId[] axisId, double[] pos)
        {
            return Motion.MoveRelAuto(axisId, pos);
        }

        public bool MoveRelManual(AxisId[] axisId, double[] pos)
        {
            return Motion.MoveRelManual(axisId, pos);
        }

        public bool WaitMoveDone(AxisId axisId, bool isPracMoveDone = true, int waitTimeOut = -1)
        {
            return Motion.WaitMoveDone(axisId, isPracMoveDone, waitTimeOut);
        }

        public bool WaitMoveDone(AxisId[] axisId, bool isPracMoveDone = true, int waitTimeOut = -1)
        {
            return Motion.WaitMoveDone(axisId, isPracMoveDone, waitTimeOut);
        }
        public bool WaitMoveDone(AxisId[] axisId, double[] pos, bool isPracMoveDone = true, int waitTimeOut = -1)
        {
            return Motion.WaitMoveDone(axisId, pos, isPracMoveDone, waitTimeOut);
        }


        public DialogResult ReportAlarm(XAlarmId alarmId, string szDetail = "")
        {
            DialogResult dlgResult = ShowAlarm(alarmId, szDetail);
            if (dlgResult == DialogResult.Cancel)
            {
                if (!mIsExecuatingAuto)
                    return dlgResult;
                // 设备进入暂停状态
                if (OnPauseActive != null)
                    OnPauseActive(null, null);
                Thread.Sleep(500);
                // 等待设备恢复
                while (PauseRun)
                    Thread.Sleep(100);
                //return dlgResult;
            }
            return dlgResult;
        }

        //protected ECylinder CreateNewCyclinder(DoId forwardDoId, DiId forwardDiId, XAlarmId forwardAlarmId,
        //    DoId backwardDoId, DiId backwardDiId, XAlarmId backwardAlarmId,
        //    CylinderType cyclinderType = CylinderType.双控)
        //{
        //    return new ECylinder(forwardDoId, forwardDiId, forwardAlarmId, backwardDoId, backwardDiId,
        //        backwardAlarmId, cyclinderType, this.ReportAlarm);
        //}

    }
}
