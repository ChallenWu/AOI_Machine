using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APS_Define_W32;
using APS168_W32;
using System.Diagnostics;

namespace XCore
{
    public class XCard : XObject
    {
        private int actCardId;
        private string name;
        private XCommandCard commandCard;
        
        public XCard(int actCardId, XCommandCard commandCard, string name)
        {
            this.actCardId = actCardId;
            this.commandCard = commandCard;
            this.name = name;
        }
        public int ActId
        {
            get { return this.actCardId; }
        }
        public string Name
        {
            get { return this.name; }
        }
        public CardType CardType
        {
            get { return commandCard.cardType; }
        }
        public bool IsConnect
        {
            get { return commandCard.IsConnected; }
        }
        public int Initial(string IP, bool simulator = false)
        {
            if (commandCard.cardType == CardType.GTS)
                return -1;
            int iRtn = commandCard.Initial(IP, simulator);
            if (iRtn < 0)
            {
                string append = "[" + name + "]:" + iRtn;
                XAlarmReporter.Instance.NotifyStations(XAlarmLevel.STOP, (int)XAlarmId.CARD_INIT_FAIL, append);
            }
            return iRtn;
        }

        public int InitialGTS(int totalAxisNum, int totalExtIOChannel,string cardConfigPath,string extIOConfigPath,bool Offline)
        {
            if (commandCard.cardType == CardType.ACS)
                return -1;
            return commandCard.InitialGTS(actCardId, totalAxisNum, totalExtIOChannel, cardConfigPath, extIOConfigPath, Offline);
        }

        public int GoHome(int axisId)
        {
            return commandCard.GoHome(actCardId, axisId);
        }

        public int GoHome_All(int value)
        {
            return commandCard.GoHome_All(actCardId, value);
        }

        public int ReadChannel(int channel, out double value)
        {
            return commandCard.ReadChannel(actCardId, channel, out value);
        }

        public int WriteChannel(int channel, double value)
        {
            return commandCard.WriteChannel(actCardId, channel, value);
        }

        public int CloseCard()
        {
            return commandCard.Close(actCardId);
        }

        #region ACS 专用
        public string ReadStringScalar(string variableName)
        {
            return commandCard.ReadStringScalar(variableName);
        }

        public string ReadVariableBuffer(string variableName, int buffer)
        {
            return commandCard.ReadVariableBuffer(variableName, buffer);
        }

        public  object ReadVarToVar(string var) 
        {
            return commandCard.ReadVarToVar(var);
        }

        public  int ReadVarToInt(string var)
        {
            return commandCard.ReadVarToInt(var); 
        }

        public  double ReadVarToDouble(string var) 
        {
            return commandCard.ReadVarToDouble(var); 
        }

        public  string ReadVarToString(string var)
        {
            return commandCard.ReadVarToString(var); 
        }

        public  void WriteVarToString(string variableName, string value) 
        {
            commandCard.WriteVarToString(variableName, value); 
        }


        public int ReadIntAxis(string variableName, int buffer)
        {
            return commandCard.ReadIntAxis(variableName, buffer);
        }

        public double ReadDoubleAxis(string variableName, int axisId)
        {
            return commandCard.ReadDoubleAxis(variableName, axisId);
        }

        public int WriteLocalVariable(string variableName, string value, int BufferId)
        {
            return commandCard.WriteLocalVariable(variableName, value, BufferId);
        }

        public int WriteGlobalVariable(string variableName, string value)
        {
            return commandCard.WriteGlobalVariable(variableName, value);
        }

        public int RunBuffer(int buffer, string label)
        {
            return commandCard.RunBuffer(actCardId, buffer, label);
        }

        public int StopBuffer(int buffer)   
        {
            return commandCard.StopBuffer(actCardId, buffer);
        }

        public int StopBufferAll()
        {
            return commandCard.StopBufferAll(actCardId);
        }
        public double GetAinValue(int portId)
        {
            return commandCard.GetAinValue(portId);
        }
        // 等待动作完成，目前用于等待ACS的信号量到位 
        public bool WaitActionDone(string doneFlag, int waitValue = 1, int timeOutMilliseconds = -1)
        {
            if (doneFlag == "")
                return false;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                int isDone = (int)ReadVarToVar(doneFlag);
                if (isDone == waitValue)
                    break;
                System.Threading.Thread.Sleep(100);
                if (timeOutMilliseconds > 0)
                {
                    if (sw.ElapsedMilliseconds > timeOutMilliseconds)
                    {
                        sw.Stop();
                        return false;
                    }
                }

            } while (true);
            sw.Stop();
            return true;
        }

        // Buffer是否在运行
        public bool IsBufferRunning(int bufId)
        {
            return commandCard.IsBufferRunning(bufId);
        }

        // 获取日志
        public string GetLogString(int logType = 0)
        {
            return commandCard.GetLogString(logType);
        }
        public string GetErrString(int errCode)
        {
            return commandCard.GetErrString(errCode);
        }
        public int WritePos(object array, string name)
        {
            return commandCard.WritePos(actCardId, array, name);
        }


#endregion

        #region 没用
        public int Register(int cardId)
        {
            int iRtn = commandCard.Register(cardId);
            if (iRtn != 0)
            {
                string append = "[" + name + "]:" + iRtn;
                XAlarmReporter.Instance.NotifyStations(XAlarmLevel.STOP, (int)XAlarmId.CARD_INIT_FAIL, append);
            }
            return iRtn;
        }
        public int LoadParam(string configFn)
        {
            int iRtn = commandCard.LoadParam(configFn);
            if (iRtn != 0)
            {
                string append = "[" + name + "]:" + iRtn;
                XAlarmReporter.Instance.NotifyStations(XAlarmLevel.STOP, (int)XAlarmId.CARD_LOAD_PARAM_FAIL, append);
            }
            return iRtn;
        }
        public int MoveLineAbs(int[] axisId, double[] pos, double vel)
        {
            return commandCard.MoveLineAbs(axisId, pos, vel);
        }
        public int MoveLineRel(int[] axisId, double[] pos, double vel)
        {
            return commandCard.MoveLineRel(axisId, pos, vel);
        }
        public int MoveArcAbs(int[] axisId, double[] center, double angle, double vel)
        {
            return commandCard.MoveArcAbs(axisId, center, angle, vel);
        }
        public int MoveArcAbs(int[] axisId, double[] center, double[] pos, ArcDir dir, double vel)
        {
            return commandCard.MoveArcAbs(axisId, center, pos, (short)dir, vel);
        }
        public int MoveArcRel(int[] axisId, double[] center, double angle, double vel)
        {
            return commandCard.MoveArcRel(axisId, center, angle, vel);
        }
        public int MoveArcRel(int[] axisId, double[] center, double[] pos, ArcDir dir, double vel)
        {
            return commandCard.MoveArcRel(axisId, center, pos, (short)dir, vel);
        }
        public int CheckError(int axisId = -1)
        {
            return commandCard.CheckError(axisId);
        }

        #endregion

        #region 通用
        public int Update()
        {
            return commandCard.Update(actCardId);
        }
        public int SetDo(int channel, int index, int sts)
        {
            return commandCard.SetDo(actCardId, channel, index, sts);
        }
        public int GetDo(int channel, int index, ref int sts)
        {
            return commandCard.GetDo(actCardId, channel, index, ref sts);
        }
        public int GetDi(int channel, int index, ref int sts)
        {
            return commandCard.GetDi(actCardId, channel, index, ref sts);
        }
        public int SetServo(int axisId, bool on)
        {
            return commandCard.SetServo(actCardId, axisId, on);
        }
        // position单位是运控卡内部单位，如acs是mm，gts是pulse
        // vel单位是运控卡内部单位，如acs是mm/s，gts是pulse/s
        public int MoveAbs(int axisId, double position, int vel)
        {
            return commandCard.MoveAbs(actCardId, axisId, position, vel);
        }
        public int MoveRel(int axisId, double distance, int vel)
        {
            return commandCard.MoveRel(actCardId, axisId, distance, vel);
        }
        public int MoveJog(int axisId, int IsStart, double vel)
        {
            return commandCard.MoveJog(actCardId, axisId, IsStart, vel);
        }
        public int Stop(int axisId)
        {
            return commandCard.Stop(actCardId, axisId);
        }
        public int EStop(int axisId)
        {
            return commandCard.EStop(actCardId, axisId);
        }
        public int EStopAll()
        {
            return commandCard.EStopAll(actCardId);
        }

        public int GetMotionIo(int axisId, ref int sts)
        {
            return commandCard.GetMotionIo(actCardId, axisId, ref sts);
        }
        public int GetMotionSts(int axisId, ref int sts)
        {
            return commandCard.GetMotionSts(actCardId, axisId, ref sts);
        }
        public int GetMotionPos(int axisId, ref double pos)
        {
            return commandCard.GetMotionPos(actCardId, axisId, ref pos);
        }
        public double GetCommandPos(int axisId, ref double pos)
        {
            return commandCard.GetCommandPos(actCardId, axisId, ref pos);
        }
        public int ReadDoneInt_HomeAll()
        {
            return commandCard.ReadDoneInt_HomeAll(actCardId);
        }

        // acc,dec单位是运控卡内部单位，如acs是mm/s，gts是pulse/s²
        public int SetAxisAccAndDec(int axisId, double acc, double dec)
        {
            return commandCard.SetAxisAccAndDec(actCardId, axisId, acc, dec);
        }
        public int SetAxisJerkAndKDec(int axisId, double jerk, double kdec)
        {
            return commandCard.SetAxisJerkAndKDec(actCardId, axisId, jerk, kdec);
        }
        public int SetStopDec(int axisId, double lead, double dec)
        {
            return commandCard.SetStopDec(actCardId, axisId, lead, dec);
        }

        public int setAxisBand(int axisId, int band, int sattleTime)
        {
            return commandCard.SetAxisBand(actCardId, axisId, band, sattleTime);
        }
        // 清除错误
        public void ClearError()
        {
            commandCard.ClearError(actCardId);
        }

        public bool WaitMotionEnd(int axisId, int timeOutMilliseconds = -1)  // @sjx add
        {
            return commandCard.WaitMotionEnd(actCardId, axisId, timeOutMilliseconds);
        }

        public bool WaitLogicalMotionEnd(int axisId, int timeOutMilliseconds = -1)
        {
            return commandCard.WaitLogicalMotionEnd(actCardId, axisId, timeOutMilliseconds);
        }

        public int SetSoftLimit(int axisId, double positive, double negative)
        {
            if (CardType == CardType.ACS)
                return 1;
            return commandCard.SetSoftLimit(actCardId, axisId, positive, negative);
        }

        public bool IsAxisError(int axisId)
        {
            return commandCard.IsAxisError(actCardId, axisId);
        }

        #endregion

        #region APS
        public int APS_SetJogParam(int axisId, int mode, int dir, double lead, double acc, double dec, int vel)
        {
            return commandCard.APS_SetJogParam(actCardId, axisId, mode, dir, lead, acc, dec, vel);
        }
        public int APS_SetAxisParam(int axisId, double lead, APS_Define PRA, double value)
        {
            return commandCard.APS_SetAxisParam(actCardId, axisId, lead, PRA, value);
        }
        public int APS_DIO_SetCOSInterrupt32(byte Port, uint ctl, out uint hEvent, bool ManualReset)
        {
            return commandCard.APS_DIO_SetCOSInterrupt32(actCardId, Port, ctl, out hEvent, ManualReset);
        }
        public int APS_DIO_INT1_EventMessage(int index, uint windowHandle, uint message, MulticastDelegate callbackAddr)
        {
            return commandCard.APS_DIO_INT1_EventMessage(actCardId, index, windowHandle, message, callbackAddr);
        }
        public int APS_SetBacklashEnable(int axisId, int on)
        {
            return commandCard.APS_SetBacklashEnable(actCardId, axisId, on);
        }
        public int APS_pt_start(int ptbId)
        {
            return commandCard.APS_pt_start(actCardId, ptbId);
        }
        public int APS_get_pt_status(int ptbId, ref PTSTS Status)
        {
            return commandCard.APS_get_pt_status(actCardId, ptbId, ref Status);
        }

#endregion
    }

    
}
