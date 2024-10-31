using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices; //for COMException class
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using ACS.SPiiPlusNET;

 
namespace XCore
{
    public class XCommandCardACS : XCommandCard
    {
        public ACS.SPiiPlusNET.Api Acs;
        public int TotalAxes;

        private bool runOffline = false; //20190730
        private Object syncLock = new Object();

        private static XCommandCardACS instance = new XCommandCardACS();

        public XCommandCardACS()
        {
            Acs = new ACS.SPiiPlusNET.Api();
            cardType = CardType.ACS;
        }
        public static XCommandCardACS Instance
        {
            get { return instance; }
        }

        // 初始化
        public override int Initial(string IP, bool simulator = false) 
        {
            if (!simulator)
            {
                if (ConnectIp(IP) != 0)
                {
                    return -1;
                }
            }
            else
            {
                runOffline = true;
                //ConnectSimulator();
                //if (!IsConnected)
                //    return -2;
            }
            return 0;
        }

        // 连接控制器
        public int ConnectIp(string ipaddress)
        {
            try
            {
                Acs.OpenCommEthernet(ipaddress, (int)EthernetCommOption.ACSC_SOCKET_STREAM_PORT); //UDP
            }
            catch (Exception ex)
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("无法连接ACS控制器，请检查网络连接是否正常！") + "\r\n" + ex.Message);
                return -1;
            }

            TotalAxes = (int)GetSysinfo(13);
            IsConnected = TotalAxes > 0;
            return 0;
            //Thread.Sleep(500);
        }

        // 连接仿真器
        public void ConnectSimulator()
        {
            try
            {
                Acs.OpenCommSimulator();
            }
            catch (Exception ex)
            {
                BzMessagebox.Show("无法连接ACS仿真程序，请检查驱动是否已经安装！\r\n" + ex.Message);
                return;
            }

            TotalAxes = (int)GetSysinfo(13);
            IsConnected = TotalAxes > 0;

        }

        public override int Close(int actCardId)
        {
            IsConnected = false;
            Thread.Sleep(500);
            if (!runOffline)
                Acs.CloseComm();
            return 0;
        }


        public double GetSysinfo(int key)
        {
            double value = Acs.SysInfo(key);
            return value;

        }

        public override string ReadStringScalar(string variableName)
        {
            if (runOffline) return "0";
            var result = Acs.ReadVariable(variableName);
            return  "";
        }

        public override string ReadVariableBuffer(string variableName, int buffer)
        {
            if (runOffline) return "0";
            var result = Acs.ReadVariable(variableName, (ProgramBuffer)buffer, 0, 0);
            return Convert.ToString(result);
        }

        private int ReadIntAxis(string variableName, int axisId)
        {
            if (runOffline) return 0;
            var result = Acs.ReadVariable(variableName, ProgramBuffer.ACSC_NONE, axisId, axisId);
            return Convert.ToInt32(result);
        }

        public override double  ReadDoubleAxis(string variableName, int axisId)
        {
            if (runOffline) return 0;
            var result = Acs.ReadVariable(variableName, ProgramBuffer.ACSC_NONE, axisId, axisId);
            return (double)result;
        }

        public override int WritePos(int actCardId, object array, string name)
        {
            if (runOffline) return 0;
            //double[] testaarray = (double[])array; 
            lock (syncLock)
            {
                try
                {
                    Acs.WriteVariable(array, name);
                }
                catch (Exception ex)
                {
                    return -1;
                }
                return 0;
            }
        }

        

        public void WriteIntAxis(string variableName, uint value, int axisId)
        {
            if (runOffline) return;
            Acs.WriteVariable(value, variableName, ProgramBuffer.ACSC_NONE, axisId, axisId);
        }
          
        public override int WriteLocalVariable(string variableName, string value, int BufferId)
        {
            if (runOffline) return 0;
            Acs.WriteVariable(value, variableName, (ProgramBuffer)(BufferId));
            return 0;
        }

        public override int WriteGlobalVariable(string variableName, string value)
        {
            if (runOffline) return 0;
            Acs.WriteVariable(value, variableName);
            return 0;
        }

        public void WriteDoubleScalar(string variableName, double value)
        {
            if (runOffline) return;
            Acs.WriteVariable(value, variableName);
        }

        public void WriteDoubleAxis(string variableName, double value, int axisId)
        {
            if (runOffline) return;
            Acs.WriteVariable(value, variableName, ProgramBuffer.ACSC_NONE, axisId, axisId);
        }

        public override int ReadDoneInt_HomeAll(int actCardId)
        {
            if (runOffline) return 1;
            var result = 0;
            result = (int)Acs.ReadVariable("isHomeDone", ProgramBuffer.ACSC_NONE);
            return Convert.ToInt32(result);
        }

        public override void WriteVarToString(string variableName, string value)
        {
            if (runOffline) return;
            Acs.WriteVariable(value, variableName, ProgramBuffer.ACSC_NONE);
            return;
        }


        public override object ReadVarToVar(string var)
        {
            if (runOffline) return "0";
            try
            {
                lock (syncLock)
                {
                    object result = Acs.ReadVariable(var, ProgramBuffer.ACSC_NONE);
                    return result;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public override int ReadVarToInt(string var )
        {
            if (runOffline) return 0;
            var result = Acs.ReadVariable(var, ProgramBuffer.ACSC_NONE);
            return Convert.ToInt32(result);
        }

        public override string ReadVarToString(string var)
        {
            if (runOffline) return "0";
            var result = Acs.ReadVariable(var, ProgramBuffer.ACSC_NONE);
            return (string)result;
        }

        public override double  ReadVarToDouble(string var)
        {
            if (runOffline) return 0;
            var result = Acs.ReadVariable(var, ProgramBuffer.ACSC_NONE);
            return (double )result;
        }

        private  void WriteInt(string variableName, int value)
        {
            if (runOffline) return;
            Acs.WriteVariable(value, variableName);
        }
        public void WriteIntBuffer(string variableName, int value, int bufferId)
        {
            if (runOffline) return;
            Acs.WriteVariable(value, variableName, (ProgramBuffer)bufferId);
        }

        public void WriteDoubleBuffer(string variableName, double value, int bufferId)
        {
            if (runOffline) return;
            Acs.WriteVariable(value, variableName, (ProgramBuffer)bufferId);
        }

        //Write double array to buffer 
        public void WriteDoubleArrayBuf(string variableName, double[] value, int bufferId)
        {
            if (runOffline) return;
            Acs.WriteVariable(value, variableName, (ProgramBuffer)bufferId);
        }

        public void WriteDoubleArray(string variableName, double[] value)
        {
            if (runOffline) return;
            Acs.WriteVariable(value, variableName);
        }

        public override int Update(int actCardId)
        {
            if (runOffline) return 0;
            lock (syncLock)
            {
                try
                {
                    object objDi = Acs.ReadVariable("Din", ProgramBuffer.ACSC_NONE);
                    int[] readDi = (int[])objDi;
                    for (int j = 0; j < readDi.Length && j < DI_Data.Length; j++)
                    {
                        int result = readDi[j];
                        for (int i = 0; i < 8; i++)
                        {
                            DI_Data[j * 8 + i] = (result & (1 << i)) == 0 ? 0 : 1;
                        }
                    }

                    object objDo = Acs.ReadVariable("Dout", ProgramBuffer.ACSC_NONE);
                    int[] readDo = (int[])objDo;
                    for (int j = 0; j < readDo.Length && j < DO_Data.Length; j++)
                    {
                        int result = readDo[j];
                        for (int i = 0; i < 8; i++)
                        {
                            DO_Data[j * 8 + i] = (result & (1 << i)) == 0 ? 0 : 1;
                        }
                    }

                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine("ACS update DI/DO error, " + e.ToString());
                    return -1;
                }
            }
            return 0;
        }

        public override int SetDo(int actCardId, int channel, int index, int sts)
        {
            if (runOffline) return 0;
            lock (syncLock)
            {
                try
                {
                    object obj = Acs.ReadVariable("Dout", ProgramBuffer.ACSC_NONE, channel, channel);
                    //int[] readDo = (int[])obj;
                    int readDo = (int)obj;

                    if (sts == 0)
                    {
                        int nMark = (~0) ^ (1 << index);
                        //readDo[channel] &= nMark;
                        readDo &= nMark;
                    }
                    else
                        //readDo[channel] |= (1 << index);
                        readDo |= (1 << index);
                    //Acs.WriteVariable(readDo[channel], "Dout", ProgramBuffer.ACSC_NONE, channel, channel);
                    Acs.WriteVariable(readDo, "Dout", ProgramBuffer.ACSC_NONE, channel, channel);
                 



                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine("ACS Set DO error, " + e.ToString());
                    return -1;
                }
            }
            return 0;
        }

        public override int GetDo(int actCardId, int channel, int index, ref int sts)
        {
            if (runOffline) return 0;

            //int array_Index = channel * 8 + index;
            //sts = DO_Data[array_Index];
            object obj = Acs.ReadVariable("Dout", ProgramBuffer.ACSC_NONE, channel, channel);
            //int[] readDo = (int[])obj;
            int readDo = (int)obj;
            sts = (readDo & (1 << index)) > 0 ? 1 : 0;

            return 0;
        }

        public override int GetDi(int actCardId, int channel, int index, ref int sts)
        {
            if (runOffline) return 0;

            //int array_Index = channel * 8 + index;
            //sts = DI_Data[array_Index];
            int readDi = (int)Acs.ReadVariable("Din", ProgramBuffer.ACSC_NONE, channel, channel);
            sts = ((readDi & (1 << index)) > 0) ? 1 : 0;
            return 0;
        }

        private int GetDiBit(int portId, int bitId)
        {
            if (runOffline) return 0;
            int result = 0;
            lock (syncLock)
            {
                result = Acs.GetInput(portId, bitId);
            }
            return result;
        }

        public override int SetServo(int actCardId, int axisId, bool on)
        {
            if (runOffline) return 0;
            try
            {
                if (on)
                {
                    Acs.Enable((Axis)axisId); // Enable motor
                    Acs.WaitMotorEnabled((Axis)axisId, 1, 1000);
                }
                else
                {
                    Acs.Disable((Axis)axisId); // Disable motor
                }
                return 0;
            }
            catch (Exception ex)
            {
                //BzMessageBox.Show("Axis " + axisId + " 使能不成功！\r\n" + ex.Message);
                return -1;
            }
        }

        // 整机回零
        //public override int GoHome_All(int actCardId,int value)
        //{
        //    if (runOffline) return 0;
        //    if (value == 1)
        //        RunBuffer(actCardId, 3, "Homing");  
        //    //RunBuffer(actCardId, 11, "GOBASICMODE_EXEC");
        //    else
        //    {
        //        StopBuffer(actCardId, 14);
        //        Thread.Sleep(20);
        //        StopBuffer(actCardId, 15);
        //        Thread.Sleep(20);
        //        StopBuffer(actCardId, 16);
        //        Thread.Sleep(20);
        //        StopBuffer(actCardId, 17);
        //        Thread.Sleep(20);
        //        StopBuffer(actCardId, 13);
        //        Thread.Sleep(20);
        //    }
        //    return 0;
        //}

        public override int GoHome(int actCardId, int axisId)
        {
            if (runOffline) return 0;
            ProgramBuffer buffer;
            string label = "Manual_Homing";
            string checkHomeDoneVar = "";
            int waitCount = 1000;
            switch (axisId )
            {
                case 1:  // 左贴装X轴
                    label += "LPasteX";
                    checkHomeDoneVar = "HomingLPasteXOver";
                    buffer = ProgramBuffer.ACSC_BUFFER_1;
                    waitCount = 300;
                    break;
                case 5:  // 右贴装X轴
                    label += "RPasteX";
                    checkHomeDoneVar = "HomingRPasteXOver";
                    buffer = ProgramBuffer.ACSC_BUFFER_2;
                    waitCount = 300;
                    break;
                case 0:  // 左贴装Y轴
                    label += "LPasteY";
                    checkHomeDoneVar = "HomingLPasteYOver";
                    buffer = ProgramBuffer.ACSC_BUFFER_1;
                    waitCount = 500;
                    break;
                case 4:  // 右贴装Y轴
                    label += "RPasteY";
                    checkHomeDoneVar = "HomingRPasteYOver";
                    buffer = ProgramBuffer.ACSC_BUFFER_2;
                    waitCount = 500;
                    break;
                case 8:  // 左贴装Z1
                    label += "LPasteZ1";
                    checkHomeDoneVar = "HomingLPasteZ1Over";
                    buffer = ProgramBuffer.ACSC_BUFFER_1;
                    waitCount = 100;
                    break;
                case 9:  // 左贴装Z2
                    label += "LPasteZ2";
                    checkHomeDoneVar = "HomingLPasteZ2Over";
                    buffer = ProgramBuffer.ACSC_BUFFER_1;
                    waitCount = 100;
                    break;
                case 10:  // 左贴装Z3
                    label += "LPasteZ3";
                    checkHomeDoneVar = "HomingLPasteZ3Over";
                    buffer = ProgramBuffer.ACSC_BUFFER_1;
                    waitCount = 100;
                    break;
                case 11:  // 右贴装Z1
                    label += "RPasteZ1";
                    checkHomeDoneVar = "HomingRPasteZ1Over";
                    buffer = ProgramBuffer.ACSC_BUFFER_2;
                    waitCount = 100;
                    break;
                case 12:  // 右贴装Z2
                    label += "RPasteZ2";
                    checkHomeDoneVar = "HomingRPasteZ2Over";
                    buffer = ProgramBuffer.ACSC_BUFFER_2;
                    waitCount = 100;
                    break;
                case 13:  // 右贴装Z3
                    label += "RPasteZ3";
                    checkHomeDoneVar = "HomingRPasteZ3Over";
                    buffer = ProgramBuffer.ACSC_BUFFER_2;
                    waitCount = 100;
                    break;
                default:
                    return 1;
            }
            Acs.StopBuffer(buffer);
            // 切换模式至手动模式
            WritePos(actCardId, 0, "ModeSwitch");
            // 重置变量
            WritePos(actCardId, 0, checkHomeDoneVar);
            Thread.Sleep(200);
            // 运行程序
            Acs.RunBuffer(buffer, label);
            // 检查运行完成标记
            int count = 0;
            do
            {
                int isAxisHomeDone = (int)ReadVarToVar(checkHomeDoneVar);
                if (isAxisHomeDone == 1)
                    break;
                Thread.Sleep(100);
                count++;
                if (count > waitCount)  // 如果超时，需要停止Buffer
                {
                    Stop(actCardId, axisId);
                    Acs.StopBuffer(buffer);
                    return -1;
                }
            } while (true);

            return 0;
        }

        public override int RunBuffer(int actCardId,int buffer, string label)
        {
            if (runOffline) return 0;
            try
            {
                Acs.StopBuffer((ProgramBuffer)buffer);
                Acs.RunBuffer((ProgramBuffer)buffer, label);
            }
            catch (COMException ex)
            {
                return-1 ;
            }
            return 0 ;
        }

        public override int StopBuffer(int actCardId, int buffer)
        {
            if (runOffline) return 0;
            try
            {
                Acs.StopBuffer((ProgramBuffer)buffer);
            }
            catch (COMException ex)
            {
                return -1;
            }
            return 0;
        }

        public override int StopBufferAll(int actCardId)
        {
            if (runOffline) return 0;
            try
            {
                for (int i = 0; i < (int)ProgramBuffer.ACSC_BUFFER_15; i++)
                    Acs.StopBuffer((ProgramBuffer)i);
            }
            catch (COMException ex)
            {
                return -1;
            }
            return 0;
        }

        public override int MoveAbs(int actCardId, int axisId, double position, double vel)
        {
            if (runOffline) return 0;
            try
            {
                SetServo(actCardId, axisId,true);
                Acs.SetVelocity((Axis)axisId, vel);
                Acs.ToPoint(MotionFlags.ACSC_AMF_WAIT, (Axis)axisId, position);
                Acs.Go((Axis)axisId);
            }
            catch (COMException ex)
            {
                return 1;
            }
            return 0;
        }

        public override int MoveRel(int actCardId, int axisId, double distance, double vel)
        {
            if (runOffline) return 0;
            try
            {
                SetServo(actCardId, axisId, true);
                Acs.SetVelocity((Axis)axisId, vel);
                Acs.ToPoint(MotionFlags.ACSC_AMF_RELATIVE, (Axis)axisId, distance);
                Acs.Go((Axis)axisId);
            }
            catch (COMException ex)
            {
                return 1;
            }
            return 0;
        }

        public override int SetAxisBand(int actCardId, int axisId, int band, int sattleTime)
        {
            return 1;
        }

        public override int MoveJog(int actCardId, int axisId, int IsStart,double vel)
        {
            if (runOffline) return 0;
            object pWait = 0;
            if (IsConnected)
            {
                try
                {
                    if (IsStart == 1)
                    {
                        Acs.Jog(MotionFlags.ACSC_AMF_VELOCITY, (Axis)axisId, vel);
                    }
                    else
                    {
                        Stop(0, axisId);
                    }
                }
                catch (System.Exception ex)
                {
                    return 1;
                }
            }
            return 0;
        }

        public override int Stop(int actCardId, int axisId)
        {
            if (runOffline) return 0;
            Acs.Halt((Axis)axisId);
            return 0;
        }

        public override int EStop(int actCardId, int axisId)
        {
            if (runOffline) return 0;
            Acs.Halt((Axis)axisId);
            return 0;
        }

        public override int EStopAll(int actCardId)
        {
            if (runOffline) return 0;
            for (int i = 0; i < (int)Axis.ACSC_AXIS_13; i++)
            {
                Acs.Halt((Axis)i);
            }
            return 0;
        }

        public override int SetAxisAccAndDec(int actCardId, int axisId, double acc, double dec)
        {
            if (runOffline) return 0;
            int ret = 0;

            Acs.SetAcceleration((Axis)axisId, acc);
            Acs.SetDeceleration((Axis)axisId, dec);
            return ret;
        }

        public override int SetAxisJerkAndKDec(int actCardId, int axisId, double jerk, double kdec)
        {
            if (runOffline) return 0;
            int ret = 0;

            Acs.SetJerk((Axis)axisId, jerk);
            Acs.SetKillDeceleration((Axis)axisId, kdec);
            return ret;
        }

        public override int GetMotionIo(int actCardId, int axisId, ref int sts)
        {
            if (runOffline) return 0;
            int _sts1 = 0, _sts2 = 0, _sts3 = 0;
            int _rSts = 0;
            int actId = 0;
            Dictionary<int, int> dictAxisInPortMap = new Dictionary<int, int>();
            dictAxisInPortMap.Add(11, 6);  // SX
            dictAxisInPortMap.Add(12, 7);  // SX
            dictAxisInPortMap.Add(13, 8);  // Z5
            dictAxisInPortMap.Add(14, 9);  // Z6
            object VA;
            _sts1 = ReadIntAxis("MST", axisId);
            actId = axisId;

            _sts2 = ReadIntAxis("FAULT", actId);

            _sts3 = ReadIntAxis("IST", actId);


            if (XConvert.BitEnable(_sts2, 0x01 << 9))
            {
                XConvert.SetBits(ref _rSts, XAPS_Define.MIO_ALM);
            }

            switch (axisId)
            {
                case 0:
                case 1:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    // Positive Limit
                    if (XConvert.BitEnable(_sts2, 0x01 << 0))
                    {
                        XConvert.SetBits(ref _rSts, XAPS_Define.MIO_PEL);
                    }
                    // Negative Limit
                    if (XConvert.BitEnable(_sts2, 0x01 << 1))
                    {
                        XConvert.SetBits(ref _rSts, XAPS_Define.MIO_MEL);
                    }
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                    // 正限位
                    if (GetDiBit(dictAxisInPortMap[axisId], 22) == 1)
                        XConvert.SetBits(ref _rSts, XAPS_Define.MIO_PEL);
                    // 负限位
                    if (GetDiBit(dictAxisInPortMap[axisId], 21) == 1)
                        XConvert.SetBits(ref _rSts, XAPS_Define.MIO_MEL);
                    // 零点
                    if (GetDiBit(dictAxisInPortMap[axisId], 20) == 1)
                        XConvert.SetBits(ref _rSts, XAPS_Define.MIO_ORG);
                    break;
                default:
                    // 正限位
                    if (XConvert.BitEnable(_sts2, 0x01 << 0))
                    {
                        XConvert.SetBits(ref _rSts, XAPS_Define.MIO_PEL);
                    }
                    // 负限位
                    if (XConvert.BitEnable(_sts2, 0x01 << 1))
                    {
                        XConvert.SetBits(ref _rSts, XAPS_Define.MIO_MEL);
                    }
                    break;
            }

            if (XConvert.BitEnable(_sts2, 0x01 << 9))
            {
                XConvert.SetBits(ref _rSts, XAPS_Define.MIO_EMG);
            }

            if (XConvert.BitEnable(_sts1, 0x01 << 0))
            {
                XConvert.SetBits(ref _rSts, XAPS_Define.MIO_SVON);
            }

            sts = _rSts;
            return sts;
        }

        public override int GetMotionSts(int actCardId, int axisId, ref int sts)
        {

            if (runOffline) return 0;
            int _sts1 = 0, _sts2 = 0;
            int _rSts = 0;
            _sts1 = ReadIntAxis("MST", axisId);
            if (XConvert.BitEnable(_sts1, 0x01 << 4))
            {
                XConvert.SetBits(ref _rSts, XAPS_Define.MTS_MDN);
            }
            if (XConvert.BitEnable(_sts1, 0x01 << 5))
            {
                XConvert.SetBits(ref _rSts, XAPS_Define.MTS_HMV);
            }
            _sts2 = ReadIntAxis("FAULT", axisId);
            if (XConvert.BitEnable(_sts2, 0x01 << 9))
            {
                XConvert.SetBits(ref _rSts, XAPS_Define.MTS_ASTP);
            }
            sts = _rSts;
            return 0;

        }

        public override int GetMotionPos(int actCardId, int axisId, ref double pos)
        {
            if (runOffline) return 0;
            pos = ReadDoubleAxis("FPOS", axisId);
            return 0;
           
        }

        public override int GetCommandPos(int actCardId, int axisId, ref double pos)
        {
            if (runOffline) return 0;
            pos = ReadDoubleAxis("RPOS", axisId);
            return 0;
        }

        public override bool WaitMotionEnd(int actCardId,int axis, int timeOutMilliseconds = -1)  // @sjx
        {
            if (runOffline) return true;
            Acs.WaitMotionEnd((Axis)axis, timeOutMilliseconds);
            return true;
        }

        public override bool WaitLogicalMotionEnd(int actCardId,int axis, int timeOutMilliseconds = -1)
        {
            if (runOffline) return true;
            Acs.WaitLogicalMotionEnd((Axis)axis, timeOutMilliseconds);
            return true;
        }
        // 读取Acs模拟量输入的值
        public override double GetAinValue(int portId)
        {
            if (runOffline) return 0;
            try
            {
                return (double)Acs.GetAnalogInputNT(portId);
            }
            catch
            {
                return 0.0;
            }
        }

        // 读取Acs Buffer是否在运行
        public override bool IsBufferRunning(int bufId)
        {
            if (runOffline) return true;
            try
            {
                int[] pstArr = (int[])ReadVarToVar("PST");
                return ((pstArr[bufId] & 0x0002) > 0);
            }
            catch
            {
                return false;
            }
        }
        //根据错误编号获取错误信息 add xjt20200909
        public override string GetErrString(int errCode)
        {
            string ErrorString;
            try
            {
                ErrorString = Acs.GetErrorString(errCode);
            }
            catch (Exception)
            {

                return "";
            }
            return ErrorString;
        }

        // 读取运动控制器当前是否错误
        public override int CheckError(int axisId = -1)
        {
            if (runOffline) return 0;
            int errCode;
            //int[] actAxisArr = new int[] { 0, 1, 4, 5, 8, 9, 10, 11, 12, 13, 14 };
            //for (int i = 0; i < actAxisArr.Length; i++)
            //{
            //    if ((errCode = Acs.GetMotorError((Axis)actAxisArr[i])) != 0)
            //        return errCode;
            //}
            if ((errCode = Acs.GetMotorError((Axis)axisId)) != 0)
                return errCode;
            return 0;
        }

        // 清除错误
        public override void ClearError(int actCardId)
        {
            if (runOffline) return;
            int[] actAxisArr = new int[] { 0, 1, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            for (int i = 0; i < actAxisArr.Length; i++)
            {
                Acs.FaultClear((Axis)actAxisArr[i]);
            }
        }

        // 获取日志
        // 输入参数：
        //     0-Log, 1-EthercatLog, 2-ECSTLog, 3-NSTLog
        public override string GetLogString(int logType) 
        {
            try
            {
                if(logType == 0)
                    return Acs.Transaction("#LOG");
                else if(logType == 1)
                    return Acs.Transaction("#ETHERCAT");
                else if (logType == 2)
                    return Acs.Transaction("?ECST");
                else
                    return Acs.Transaction("?NST");
            }
            catch
            { 

            }
            return ""; 
        }
        public override int SetSoftLimit(int actCardId, int axis, double positive, double negative)
        {
            return 1;
        }
    }

}
