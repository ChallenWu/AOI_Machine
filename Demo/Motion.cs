using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Demo;
namespace HB_IWatch
{
    /// <summary>
    /// Motion封装常用运控功能，提供更加简洁的接口
    /// </summary>
    class Motion
    {
        public static DISTSTYPE GetDi(DiId id)
        {
            DISTSTYPE sts1 = DISTSTYPE.LOW;
            int rtn = XDevice.Instance.FindDiById((int)id).GetDi(ref sts1);
            if (rtn != 0)
            {
                MessageBox.Show("DI状态获取出错： " + id.ToString() + "\n返回值：" + rtn.ToString());
                return DISTSTYPE.HIGH;
            }
            return sts1;
        }

        public static void SetDo(DoId id, DOSTSTYPE sts)
        {
            int rtn = XDevice.Instance.FindDoById((int)id).SetDo(sts);
            if (rtn != 0)
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("DO状态设置出错","： ", id.ToString()));
            }
        }

        public static DOSTSTYPE GetDo(DoId id)
        {
            return XDevice.Instance.FindDoById((int)id).STS;
        }

        public static bool WaitDi(DiId id, DISTSTYPE expectedState, int timeOutMs = 5000)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                if (GetDi(id) == expectedState)
                    return true;
                if (sw.ElapsedMilliseconds > timeOutMs)
                    return false;
                Thread.Sleep(10);
            } while (true);

        }

        // 检查轴限位
        // @chenliangfeng for protection
        public static bool CheckAxisLimit(AxisId axisId, double pos)
        {
            bool posValid = true;
            double posLimit;
            double negLimit;
            GetAxisLimitPos(axisId, out posLimit, out negLimit);

            if (pos < negLimit || pos > posLimit)
            {
                posValid = false;
            }

            if (!posValid)
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("超出轴限位")); // 严重事故，请检查电机指令位置
                return false; 
            }
            return true;
        }

        // 获取轴限位
        public static void GetAxisLimitPos(AxisId axisId, out double posLimitPos, out double negLimitPos)
        {
            posLimitPos = 1000;
            negLimitPos = -1000;
            if (axisId == AxisId.左贴装X轴 || axisId == AxisId.右贴装X轴 || axisId == AxisId.左贴装Y轴 || axisId == AxisId.右贴装Y轴)
            {
                string posLimitVar = "SRLIMIT";
                string negLimitVar = "SLLIMIT";
                posLimitPos = XDevice.Instance.FindCardById((int)CardId.主设备1).ReadDoubleAxis(posLimitVar, XDevice.Instance.FindAxisById((int)axisId).ActId);
                negLimitPos = XDevice.Instance.FindCardById((int)CardId.主设备1).ReadDoubleAxis(negLimitVar, XDevice.Instance.FindAxisById((int)axisId).ActId);
            }
        }
        public static void WriteAcsLog(string message, int LogType = 0)
        {
            //string path = Globals.Dir_ACS_Log_Dir;
            //if (LogType == 0)  // #Log
            //    path += "AcsLog" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            //else if (LogType == 1) // #EthercatLog
            //    path += "EtherCatLog" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            //else if (LogType == 2) // #ECSTLog
            //    path += "ECSTLog" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            //else if (LogType == 3)  // NSTLog
            //    path += "NSTLog" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            //else
            //    path += "MotionErrLog" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
            //string str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " => " + message;
            //CsvServer.Instance.WriteLine(path, str);
        }
        public static bool MoveAbsAuto(AxisId axisId, double pos)
        {
            if (!CheckAxisLimit(axisId, pos))
                return false;
            if (Globals.RUNMODE == Globals.MachineRunMode.NormalRun)
            {
                if (XDevice.Instance.FindAxisById((int)axisId).IsAxisError())
                {
                    BzMessagebox.Show(MultiLanguage.GetMessage("运动轴出错, 轴号",": ", axisId.ToString(), "!"));
                    return false;
                }
            }
            int errCode = 0;
            string errString = "";
            int realAxisId = XDevice.Instance.FindAxisById((int)axisId).ActId;
            errCode = XDevice.Instance.FindCardById((int)CardId.主设备1).CheckError(realAxisId);
            if (errCode != 0)
            {
                errString = XDevice.Instance.FindCardById((int)CardId.主设备1).GetErrString(errCode);
                WriteAcsLog("电机运动控制错误码：" + errCode + " 错误提示为： " + errString, 4);
                string strLog;
                for (int i = 0; i < 4; i++)
                {
                    strLog = XDevice.Instance.FindCardById((int)CardId.主设备1).GetLogString(i);
                    if (strLog != "")
                    {
                        WriteAcsLog("运动控制系统当前Log：" + strLog, i);
                    }
                    else
                    {
                        WriteAcsLog("运动控制系统获取Log失败!", i);
                    }
                }
            }
            double vel = Globals.motorSpeed.GetMaxVel(axisId) * Globals.motorSpeed.GetSpeedRatio(axisId);
            int ret = XDevice.Instance.FindAxisById((int)axisId).MoveAbs(pos, vel);
            if (ret != 0)
                return false;
            return true;
        }

        public static bool MoveAbsManual(AxisId axisId, double pos)
        {
            if (!CheckAxisLimit(axisId, pos))
                return false;
            if (Globals.RUNMODE == Globals.MachineRunMode.NormalRun)
            {
                if (XDevice.Instance.FindAxisById((int)axisId).IsAxisError())
                {
                    BzMessagebox.Show(MultiLanguage.GetMessage("运动轴出错, 轴号", ": ", axisId.ToString(), "!"));
                    return false;
                }
            }
            
            double vel = Globals.motorSpeed.GetManualVel(axisId);
            int ret = XDevice.Instance.FindAxisById((int)axisId).MoveAbs(pos, vel);
            if (ret != 0)
                return false;
            return true;
        }

        public static bool MoveRelAuto(AxisId axisId, double pos)
        {
            double vel = Globals.motorSpeed.GetMaxVel(axisId) * Globals.motorSpeed.GetSpeedRatio(axisId);
            int ret = XDevice.Instance.FindAxisById((int)axisId).MoveRel(pos, vel);
            if (ret != 0)
                return false;
            return true;
        }

        public static bool MoveRelManual(AxisId axisId, double pos)
        {
            double vel = Globals.motorSpeed.GetManualVel(axisId);
            int ret = XDevice.Instance.FindAxisById((int)axisId).MoveRel(pos, vel);
            if (ret != 0)
                return false;
            return true;
        }

        public static bool MoveAbsAuto(AxisId[] axisId, double[] pos)
        {
            if (axisId.Length != pos.Length)
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("MoveAbsAuto", "参数出错", "!"));
                return false;
            }
            for (int i = 0; i < axisId.Length; i++)
            {
                if (!MoveAbsAuto(axisId[i], pos[i]))
                    return false;
                //if (!WaitMoveDone(axisId[i]))
                //    return false;
            }
            return true;
        }

        public static bool MoveAbsManual(AxisId[] axisId, double[] pos)
        {
            if (axisId.Length != pos.Length)
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("MoveAbsAuto", "参数出错", "!"));
                return false;
            }

            for (int i = 0; i < axisId.Length; i++)
            {
                if ((XDevice.Instance.FindAxisById((int)axisId[i]).IsHomeOk == false) || XDevice.Instance.FindAxisById((int)axisId[i]).IsSVON == false)
                    return false;
            }

            for (int i = 0; i < axisId.Length; i++)
            {
                if (!MoveAbsManual(axisId[i], pos[i]))
                    return false;
            }
            return true;
        }

        public static bool WaitMoveDone(AxisId axisId, bool isPracMoveDone = true, int waitTimeOut = -1)
        {
            try
            {
                if (isPracMoveDone)
                {
                    if (!XDevice.Instance.FindAxisById((int)axisId).WaitMotionEnd(waitTimeOut))
                        return false;
                }
                else
                {
                    if (!XDevice.Instance.FindAxisById((int)axisId).WaitLogicalMotionEnd(waitTimeOut))
                        return false;
                }
            }
            catch (Exception ex)
            {
                BzMessagebox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public static bool WaitMoveDone(AxisId[] axisId, bool isPracMoveDone = true, int waitTimeOut = -1)
        {
            for (int i = 0; i < axisId.Length; i++)
            {
                if (!WaitMoveDone(axisId[i], isPracMoveDone, waitTimeOut))
                    return false;
            }
            return true;
        }
        public static bool WaitMoveDone(AxisId[] axisId, double[] pos,bool isPracMoveDone = true, int waitTimeOut = -1)
        {
            double poscur = 0.0;
            for (int i = 0; i < axisId.Length; i++)
            {
                if (!WaitMoveDone(axisId[i], isPracMoveDone, waitTimeOut))
                    return false;
                if ((int)axisId[i] < 4)
                {
                    XDevice.Instance.FindCardById((int)CardId.主设备1).GetMotionPos((XDevice.Instance.FindAxisById((int)axisId[i]).ActId), ref poscur);
                    if (Math.Abs(poscur - pos[i]) > 1)
                        return false;
                }
            }
            return true;
        }

        public static bool MoveAbsInSettingVel(AxisId axisId, double pos, double vel)
        {
            //double vel = Globals.motorSpeed.GetManualVel(axisId);
            int ret = XDevice.Instance.FindAxisById((int)axisId).MoveAbs(pos, vel);
            if (ret != 0)
                return false;
            return true;
        }

        public static bool MoveRelInSettingVel(AxisId axisId, double pos, double vel)
        {
            //double vel = Globals.motorSpeed.GetManualVel(axisId);
            int ret = XDevice.Instance.FindAxisById((int)axisId).MoveRel(pos, vel);
            if (ret != 0)
                return false;
            return true;
        }

        // 等待多个Di中任意一个Di到位
        public static bool WaitAnyDiReady(DiId[] ids, DISTSTYPE[] expectedStates, int timeOutMs = 5000)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                bool pass = false;
                for (int i = 0; i < ids.Length; i++)
                {
                    if (GetDi(ids[i]) == expectedStates[i])
                    {
                        pass = true;
                        break;
                    }
                }
                if (pass)
                    return true;
                if (sw.ElapsedMilliseconds > timeOutMs)
                    return false;
                Thread.Sleep(0);
            } while (true);
        }

        public static bool WaitDo(DoId ids, DOSTSTYPE expectedStates, int timeOutMs = 5000)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                if (XDevice.Instance.FindDoById((int)ids).STS == expectedStates)
                    return true;
                if (sw.ElapsedMilliseconds > timeOutMs)
                    return false;
                Thread.Sleep(0);
            } while (true);

        }

        public static bool MoveRelAuto(AxisId[] axisId, double[] pos)
        {
            if (axisId.Length != pos.Length)
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("MoveRelAuto", "参数出错", "!"));
                return false;
            }
            for (int i = 0; i < axisId.Length; i++)
            {
                if (!MoveRelAuto(axisId[i], pos[i]))
                    return false;
            }
            return true;
        }

        public static bool MoveRelManual(AxisId[] axisId, double[] pos)
        {
            if (axisId.Length != pos.Length)
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("MoveRelAuto", "参数出错", "!"));
                return false;
            }
            for (int i = 0; i < axisId.Length; i++)
            {
                if (!MoveRelManual(axisId[i], pos[i]))
                    return false;
            }
            return true;
        }

        public static bool WaitDi(DiId[] ids, DISTSTYPE[] expectedStates, int timeOutMs = 5000)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                bool pass = true;
                for (int i = 0; i < ids.Length; i++)
                {
                    if (GetDi(ids[i]) != expectedStates[i])
                    {
                        pass = false;
                        break;
                    }
                }
                if (pass)
                    return true;
                if (sw.ElapsedMilliseconds > timeOutMs)
                    return false;
                Thread.Sleep(0);
            } while (true);
        }

        public static bool SetServo(AxisId axisId,bool sts)
        {
            int ret =  XDevice.Instance.FindAxisById((int)axisId).SetServo(sts);
            if (ret != 0)
                return false;
            return true;
        }
    }
}
