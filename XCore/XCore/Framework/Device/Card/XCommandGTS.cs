using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCore;
using System.Threading;
using System.Windows.Forms;
using gts;
using System.Diagnostics;

namespace XCore
{
    public class XCommandCardGTS : XCommandCard
    {
        private bool runOffline = false;
        private object locker = new object();//线程锁
        private string errorMessage;
        private int totalAxes;
        private int totalExtIOChannel;
        private bool didoReverse = true;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public XCommandCardGTS()
        {
            cardType = CardType.GTS;
        }


        // @sjx 如果R轴改增量式编码器，这种回零方式精度不够
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actCardId"></param>
        /// <param name="axisId"></param>
        /// <param name="homeDir">1为正向，-1为负向</param>
        /// <param name="homeVel">寻找Home点速度，寻找限位为该速度的2倍</param>
        /// <param name="homeAcc"></param>
        /// <returns></returns>
        public override int GoHome(int actCardId, int axisId)
        {
            short rtn;
            short  homeDir;
            short homeMode;
            double homeVelH;
            double homeVelL;
            double lead;
            double escapeStep;
            double homeOffset;

            //homeMode 和homeDir写到轴参数里，设备初始化的时候赋值，XDevice新加函数从实际ID寻找轴，拿到回零参数，R轴绝对式编码器
            homeMode = XDevice.Instance.FindAxisByActId(actCardId, axisId, cardType).HomeMode;
            homeDir = XDevice.Instance.FindAxisByActId(actCardId, axisId, cardType).HomeDir;
            homeVelH = XDevice.Instance.FindAxisByActId(actCardId, axisId, cardType).HomeSpeedH;
            homeVelL = XDevice.Instance.FindAxisByActId(actCardId, axisId, cardType).HomeSpeedL;
            lead = XDevice.Instance.FindAxisByActId(actCardId, axisId, cardType).Lead;
            escapeStep = XDevice.Instance.FindAxisByActId(actCardId, axisId, cardType).EscapeStep;
            homeOffset = XDevice.Instance.FindAxisByActId(actCardId, axisId, cardType).HomeOffset;

            if (SetServo(actCardId, axisId, true) != 0)
                return 1;

            //0模式自己定义
            if (homeMode == (int)HomeMode.UserDefined)
                return SetServo(actCardId, axisId, true);
            else
            {
                bool ret = true;
                bool dir = (homeDir != 1);
                if (homeMode == (int)HomeMode.Limit_Home || homeMode == (int)HomeMode.Limit || homeMode == (int)HomeMode.Limit_Home_Index || homeMode == (int)HomeMode.Limit_Index)
                {
                    if (GetPosLimitSts(actCardId, (short)axisId) && homeDir == 1)
                    {
                        // 离开正限位传感器
                        ret = MoveOutOfSnr(actCardId, axisId, 1, dir, homeVelH, lead);

                    }
                    else if (GetNegLimitSts(actCardId, (short)axisId) && homeDir == -1)
                    {
                        // 离开负限位传感器
                        ret = MoveOutOfSnr(actCardId, axisId, 2, dir, homeVelH, lead);
                    }
                }
                else if (homeMode == (int)HomeMode.Home || homeMode == (int)HomeMode.Home_Index)
                {
                    if (GetHomeSnrSts(actCardId, (short)axisId))
                    {
                        // 离开回零传感器
                        ret = MoveOutOfSnr(actCardId, axisId, 0, dir, homeVelH, lead);
                    }
                }
                if (!ret)
                {
                    Stop(actCardId, axisId);
                    EStop(actCardId, axisId);
                    return 1;
                }
                
                SetZero((short)actCardId, axisId);
                mc.THomePrm thomeprm;
                mc.THomeStatus homests;
                lock (locker)
                {
                    rtn = mc.GT_GetHomePrm((short)actCardId, (short)axisId, out thomeprm);
                    if (rtn != 0)
                        return 1;
                }
                thomeprm.mode = homeMode;
                thomeprm.moveDir = homeDir;
                thomeprm.velHigh = XConvert.MM2PULS(homeVelH, lead) / 1000;
                thomeprm.velLow = XConvert.MM2PULS(homeVelL, lead) / 1000;
                thomeprm.acc = XConvert.MM2PULS(5, 1);
                thomeprm.dec = XConvert.MM2PULS(5, 1);
                if (actCardId == 0)
                    thomeprm.edge = 1;
                else
                    thomeprm.edge = 0;
                thomeprm.smoothTime = 30;
                thomeprm.searchHomeDistance = 0;//最大距离
                thomeprm.escapeStep = Convert.ToInt32(XConvert.MM2PULS(escapeStep, lead));
                thomeprm.homeOffset = (int)XConvert.MM2PULS(homeOffset, lead);
                lock (locker)
                {
                    rtn = mc.GT_GoHome((short)actCardId, (short)axisId, ref thomeprm);  //启动回零   
                }
                if (rtn != 0)
                {
                    Stop(actCardId, axisId);
                    EStop(actCardId, axisId);
                    return 1;
                }
                int count = 0;
                do
                {
                    lock (locker)
                    {
                        rtn = mc.GT_GetHomeStatus((short)actCardId, (short)axisId, out homests);//取回零状态
                    }
                    Thread.Sleep(100);
                    count++;
                    if (count > 200)
                    {
                        Stop(actCardId, axisId);
                        EStop(actCardId, axisId);
                        return 1;
                    }
                } while (homests.run == 1);

                if (homests.stage == 100 && homests.error == 0)//回零完成并且回零没有错误
                {
                    Thread.Sleep(1000);
                    SetZero((short)actCardId, (short)axisId);
                    ClearError(actCardId, axisId);
                    return 0;
                }
                else
                {
                    Stop(actCardId, axisId);
                    EStop(actCardId, axisId);
                    return 1;
                }
            }
        }

        public override int InitialGTS(int actCardId, int totalAxisNum, int extIONums,string cardConfigPath,string extIOConfigPath,bool Offline = false)
        {
            lock (locker)
            {
                totalAxes = totalAxisNum;
                totalExtIOChannel = extIONums;
                runOffline = Offline;
                if (true == runOffline)
                {
                    return 0;  //1;  
                }
                short rtn;
                rtn = mc.GT_Open((short)actCardId, 0, 1);
                if (0 != rtn)
                {
                    errorMessage = "控制卡打开失败";
                    return 1;
                }
                IsConnected = true;
                rtn = mc.GT_Reset((short)actCardId);
                if (rtn != 0)
                {
                    errorMessage = "控制卡复位失败！";
                    return 1;
                }
                rtn = mc.GT_LoadConfig((short)actCardId, cardConfigPath);
                if (0 != rtn)
                {
                    errorMessage = "控制卡配置文件加载失败！";
                    return 1;
                }
                // @sjx 这里可以单轴逐个清除状态，也可以多轴一起清除状态
                rtn = mc.GT_ClrSts((short)actCardId, 1, (short)totalAxisNum);
                if (rtn != 0)
                {
                    errorMessage = string.Format("轴清错存在问题");
                    return 1;
                }
                if (extIOConfigPath == "")
                {
                    return 0;
                }

                rtn = mc.GT_HomeInit((short)actCardId);

                if (InitExtIOModule(actCardId, extIOConfigPath) != 0)
                    return 1;
                else
                    return 0;
            }
        }

        // @sjx Global里绑定时，一定要填对Channel和Index 
        public override int SetDo(int actCardId, int channel, int index, int sts)
        {
            if (runOffline) return 0;

            if (didoReverse)
                sts = (sts == 0) ? 1 : 0;
            if (channel == 0)
            {
                lock (locker)
                {
                    int ret = mc.GT_SetDoBit((short)actCardId, (short)mc.MC_GPO, (short)(index + 1), (short)sts);//按位输出，0表示输出，1表示关闭, index索引从1 开始
                    if (ret == 0)
                        return 0;
                    else
                        return ret;
                }
            }
            else
            {
                lock (locker)
                {
                    int ret = mc.GT_SetExtIoBit((short)actCardId, (short)(channel - 1), (short)index, (ushort)sts);//channel 和 index 索引从0 开始
                    if (ret == 0)
                        return 0;
                    else
                        return ret;
                }
            }
        }

        //这里跟XCommandCard和XCommandACS接口统一
        public override int GetDi(int actCardId, int channel, int index, ref int sts)
        {
            int dists;
            if (channel == 0)//channel0为控制卡自带的IO
            {
                lock (locker)
                {
                    short rtn = mc.GT_GetDi((short)actCardId, mc.MC_GPI, out dists);
                    if ( rtn!= 0)
                        return rtn;
                }
            }
            else//其余的为扩展IO
            {
                ushort dists1;
                lock (locker)
                {
                    short rtn = mc.GT_GetExtIoValue((short)actCardId, (short)(channel - 1), out dists1);
                    if (rtn != 0)  //  扩展IO的channel从0开始索引
                        return rtn;
                }
                dists = dists1;
            }
            if (didoReverse)
            {
                    sts = (dists & (1 << index)) == 0 ? 1 : 0;
            }
            else
            {
                    sts = (dists & (1 << index)) == 0 ? 0 : 1;
            }
            return 0;

            //实时性不够
            //int array_Index = channel * 16 + index;
            //sts = DI_Data[array_Index];
            //return 0;
        }
        //接口统一，暂时不改
        public override int GetDo(int actCardId, int channel, int index, ref int sts)
        {
            int dosts;
            if (channel == 0)//channel0为控制卡自带的IO
            {
                lock (locker)
                {
                    if (mc.GT_GetDo((short)actCardId, mc.MC_GPO, out dosts) != 0)
                        return 1;
                }
            }
            else//其余的为扩展IO
            {
                ushort dosts1;
                lock (locker)
                {
                    if (mc.GT_GetExtDoValue((short)actCardId, (short)(channel - 1), out dosts1) != 0)  //  扩展IO的channel从0开始索引
                        return 1;
                }
                dosts = dosts1;
            }
            if (didoReverse)
            {
                sts = (dosts & (1 << index)) == 0 ? 1 : 0;
            }
            else
            {
                sts = (dosts & (1 << index)) == 0 ? 0 : 1;
            }
            return 0;

            //实时性不够
            //int array_Index = channel * 16 + index;
            //sts = DO_Data[array_Index];
            //return 0;
        }

        public override int SetServo(int actCardId, int axis, bool on)
        {
            lock (locker)
            {
                if (true == runOffline)
                {
                    return 0;
                }

                Stop(actCardId, axis);

                short rtn;
                if (on)
                {
                    //拉料步进在清错里就已经被使能
                    rtn = ClearError(actCardId, axis);
                    if (rtn != 0)
                        return 1;
                    rtn = mc.GT_AxisOn((short)actCardId, (short)axis);
                    if (rtn != 0)
                    {
                        return 1;
                    }
                }
                else
                {
                    if (actCardId == 3)
                    {
                        rtn = mc.GT_SetDoBit((short)actCardId, mc.MC_CLEAR, (short)axis, 1);
                        if (rtn != 0)
                            return rtn;
                    }
                    rtn = mc.GT_AxisOff((short)actCardId, (short)axis);
                    if (rtn != 0)
                    {
                        return 1;
                    }
                }
                return 0;
            }
        }

        // position单位是pulse，vel单位是pulse/s
        // @sjx 这里position单位是毫米，SetPos单位是Pulse， 脉冲当量多少?导程10mm，电机转一圈暂定10000个脉冲
        public override int MoveAbs(int actCardId, int axisId, double position, double vel)
        {
            if (axisId >= 10 && axisId <= 15) // R1/R2/R3位置保护@11.25
            {
                if (position >= 10)
                {
                    BzMessagebox.Show(MultiLanguage.GetMessage("R轴超极限, 位置保护"));
                    return 1;
                }
            }
            //position = XConvert.MM2PULS(position, 0.001); //convert to pules position*1000 在axis里面转过
            //vel = XConvert.MM2PULS(vel, 1);//convert to pules/ms 
            vel = vel / 1000; // convert to pules/ms 

            if (!IsAxisEnabled(actCardId, axisId))
                SetServo(actCardId, axisId, true);

            if (!WaitMotionEnd(actCardId, axisId,10000))  // 等待上次电机运动完成
            {
                double encPos = 0;
                GetMotionPos(actCardId, axisId, ref encPos);
                double commandPos = 0;
                GetCommandPos(actCardId, axisId, ref commandPos);
                BzMessagebox.Show("等待上次电机运动完成失败" + XDevice.Instance.FindAxisByActId(actCardId, axisId, this.cardType).Name + 
                                              "\n轴状态：" + ReadStatus(actCardId, axisId).ToString() + 
                                              "\n当前位置(PLS)：" + encPos.ToString() + 
                                              "\n指令位置(PLS)：" + commandPos.ToString() + 
                                              "\n目标位置(PLS)：" + position.ToString());
                return 1;
            }
            lock (locker)
            {
                mc.TTrapPrm pra;
                // 切换至点位模式
                if (mc.GT_PrfTrap((short)actCardId, (short)axisId) != 0)
                    return 1;
                if (mc.GT_GetTrapPrm((short)actCardId, (short)axisId, out pra) != 0)
                    return 1;
                //pra.acc = 10;
                //pra.dec = 10;
                pra.smoothTime = 50;
                pra.velStart = 20;

                if (actCardId == 3)     //供料流线电机需要等待500ms
                    Thread.Sleep(500);


                if (mc.GT_SetTrapPrm((short)actCardId, (short)axisId, ref pra) != 0)
                    return 1;
                if (mc.GT_SetPos((short)actCardId, (short)axisId, (int)position) != 0)
                    return 1;
                if (mc.GT_SetVel((short)actCardId, (short)axisId, vel) != 0)
                    return 1;
                int rtn = mc.GT_Update((short)actCardId, 1 << (axisId - 1));
                if (rtn != 0)
                    return 1;
                return 0;
            }

        }


        public override int MoveRel(int actCardId, int axisId, double distance, double vel)
        {
            double commandPos = 0;
            GetCommandPos(actCardId, axisId, ref commandPos);
            return MoveAbs(actCardId, axisId, commandPos + distance, vel);
        }

        // 轴规划器是否在运动
        public bool IsAxisBusy(int actCardId, int axis)
        {
            if (runOffline) return true;
            int sts = GetAxisSts(actCardId, axis);
            return (sts  & 0x400) > 0;
        }

        // 轴电机是否到位，位置误差到设置的误差带范围内
        public bool IsAxisSettled(int actCardId, int axis)
        {
            if (runOffline) return true;
            int sts = GetAxisSts(actCardId, axis);
            return (sts & 0x800) > 0;
        }

        // vel单位是pulse/s
        // @sjx 速度单位: mm/s， 要转为pulse/ms
        public override int MoveJog(int actCardId, int axisId, int IsStart, double vel)
        {
            if (runOffline) return 0;

            //vel = XConvert.MM2PULS(vel, 1);
            //vel = vel / 1000; // convert to pules/ms 
            double lead = XDevice.Instance.FindAxisByActId(actCardId, axisId, cardType).Lead;
            vel = XConvert.MM2PULS(vel, lead) / 1000;
            if (!IsAxisEnabled(actCardId, axisId))
                SetServo(actCardId, axisId, true);

            //if (!WaitMotionEnd(actCardId, axisId))  // 等待上次电机运动完成
            //    return false;

            if (mc.GT_PrfJog((short)actCardId, (short)axisId) != 0)
                return 1;
            mc.TJogPrm jogPrm;
            if (mc.GT_GetJogPrm((short)actCardId, (short)axisId, out jogPrm) != 0)
                return 1;
            jogPrm.acc = 0.3;
            jogPrm.dec = 0.3;
            jogPrm.smooth = 0.3;
            if (mc.GT_SetJogPrm((short)actCardId, (short)axisId, ref jogPrm) != 0)
                return 1;
            if (mc.GT_SetVel((short)actCardId, (short)axisId, vel) != 0)
                return 1;
            try
            {
                if (IsStart == 1)
                {
                    if (mc.GT_Update((short)actCardId, 1 << (axisId - 1)) != 0)
                        return 1;
                }
                else
                {
                    Stop(actCardId, axisId);
                }
            }
            catch (System.Exception ex)
            {
                return 1;
            }
            return 0;
        }

        public override int Stop(int actCardId, int axisId)
        {
            lock (locker)
            {
                short rtn;
                rtn = mc.GT_Stop((short)actCardId, 1 << (axisId - 1), 0);
                if (rtn != 0)
                {
                    return 1;
                }
                return 0;
            }
        }

        public override int EStop(int actCardId, int axisId)
        {
            lock (locker)
            {
                short rtn;
                rtn = mc.GT_Stop((short)actCardId, 1 << (axisId - 1), 1 << (axisId - 1));
                if (rtn != 0)
                {
                    return 1;
                }
                return 0;
            }
        }

        public override int GetMotionIo(int actCardId, int axisId, ref int sts)
        {
            if (runOffline) return 0;

            //int axisSts;
            //uint clk;
            int _rSts = 0;
            //mc.GT_GetSts((short)actCardId, (short)axisId, out axisSts, 1, out clk);
            int axisSts = GetAxisSts(actCardId, axisId);

            if (XConvert.BitEnable(axisSts, 0x01 << 1))
            {
                XConvert.SetBits(ref _rSts, XAPS_Define.MIO_ALM);
            }

            // Positive Limit
            //if (XConvert.BitEnable(axisSts, 0x01 << 5))
            if (GetPosLimitSts(actCardId, (short)axisId))
            {
                XConvert.SetBits(ref _rSts, XAPS_Define.MIO_PEL);
            }
            else
            {
                XConvert.ClrBits(ref _rSts, XAPS_Define.MIO_PEL);
            }

            // Negative Limit
            //if (XConvert.BitEnable(axisSts, 0x01 << 6))
            if (GetNegLimitSts(actCardId, (short)axisId))
            {
                XConvert.SetBits(ref _rSts, XAPS_Define.MIO_MEL);
            }
            else
            {
                XConvert.ClrBits(ref _rSts, XAPS_Define.MIO_MEL);
            }

            if (XConvert.BitEnable(axisSts, 0x01 << 8))
            {
                XConvert.SetBits(ref _rSts, XAPS_Define.MIO_EMG);
            }

            if (XConvert.BitEnable(axisSts, 0x01 << 9))
            {
                XConvert.SetBits(ref _rSts, XAPS_Define.MIO_SVON);
            }

            int homests;
            lock (locker)
            {
                mc.GT_GetDi((short)actCardId, mc.MC_HOME, out homests);
            }
            //if (XConvert.BitEnable(homests, 0x01 << (axisId - 1)))
            if (GetHomeSnrSts(actCardId, (short)axisId))
            {
                XConvert.SetBits(ref _rSts, XAPS_Define.MIO_ORG);
            }
            else
            {
                XConvert.ClrBits(ref _rSts, XAPS_Define.MIO_ORG);
            }

            sts = _rSts;
            return sts;
        }

        public override int GetMotionSts(int actCardId, int axisId, ref int sts)
        {
            if (runOffline) return 0;

            int axisSts;
            uint clk;
            int _rSts = 0;
            lock (locker)
            {
                mc.GT_GetSts((short)actCardId, (short)axisId, out axisSts, 1, out clk);

                if (XConvert.BitEnable(axisSts, 0x01 << 1))
                {
                    XConvert.SetBits(ref _rSts, XAPS_Define.MTS_ASTP);
                }

                //if (XConvert.BitEnable(axisSts, 0x01 << 10))            //@sjx 这个放在回零代码里
                //{
                //    XConvert.SetBits(ref _rSts, XAPS_Define.MTS_HMV);
                //}

                mc.GT_SetAxisBand((short)actCardId, (short)axisId, 100, 5);
                if (XConvert.BitEnable(axisSts, 0x01 << 11))
                {
                    XConvert.SetBits(ref _rSts, XAPS_Define.MTS_MDN);
                }

                sts = _rSts;
            }
            return sts;
        }

        //@sjx 这里获取的位置单位是脉冲，要转化为mm
        public override int GetMotionPos(int actCardId, int axisId, ref double pos)
        {
            lock (locker)
            {
                if (runOffline)
                    return 0;
                uint pclock = 0;
                int ret = mc.GT_GetEncPos((short)actCardId, (short)axisId, out pos, 1, out pclock);
                if (ret != 0)
                    return 1;
                //pos = XConvert.PULS2MM(pos, 0.001);//Axis里面转了
                return 0;
            }
        }

        //@sjx 这里获取的位置单位是脉冲，要转化为mm
        public override int GetCommandPos(int actCardId, int axisId, ref double pos)
        {
            lock (locker)
            {
                if (runOffline)
                    return 0;
                uint pclock = 0;
                if (mc.GT_GetPrfPos((short)actCardId, (short)axisId, out pos, 1, out pclock) != 0)
                    return 1;
                //pos = XConvert.PULS2MM(pos, 0.001);//Axis里面转了
                return 0;
            }
        }

        public override int GoHome_All(int actCardId, int value)
        {
            if (value == 1)
            {
                for (int i = 1; i < totalAxes + 1; i++)
                {
                    short homeDir;
                    short homeMode;

                    homeMode = XDevice.Instance.FindAxisByActId(actCardId, i,cardType).HomeMode;
                    homeDir = XDevice.Instance.FindAxisByActId(actCardId, i,cardType).HomeDir;

                    if (homeMode == 0)
                    {
                        if (SetServo(actCardId, i, true) != 0)
                            return 1;
                    }
                    else
                    {
                        SetZero((short)actCardId, i);
                        mc.THomePrm thomeprm;
                        short rtn = mc.GT_GetHomePrm((short)actCardId, (short)i, out thomeprm);
                        thomeprm.mode = homeMode;//限位+Home点回零
                        thomeprm.moveDir = homeDir;
                        thomeprm.velHigh = XConvert.MM2PULS(50, 1); //convert to pules/ms
                        thomeprm.velLow = XConvert.MM2PULS(50, 1);
                        thomeprm.acc = XConvert.MM2PULS(5, 1);  //convert to pules/ms²
                        thomeprm.dec = XConvert.MM2PULS(5, 1);
                        thomeprm.edge = 0;
                        thomeprm.smoothTime = 30;
                        thomeprm.searchHomeDistance = 0;//最大距离
                        rtn = mc.GT_GoHome((short)actCardId, (short)i, ref thomeprm);  //启动回零   
                        if (rtn != 0)
                        {
                            EStop(actCardId, i);
                            return 1;
                        }
                    }
                }
                return 0;
            }
            else
            {
                for (int i = 0; i < totalAxes; i++)
                {
                    Stop(actCardId, i);
                }
                return 0;
            }
        }

        public override int ReadDoneInt_HomeAll(int actCardId)
        {
            bool isHomeDone = true;
            
            if (actCardId == 0)
            {
                for (int i = 1; i < totalAxes+1; i++)
                {
                    isHomeDone = isHomeDone && IsAxisEnabled(actCardId, i);
                }
                if (isHomeDone == true)
                    return 0;
                else
                    return 1;
            }
            else
            {
                mc.THomeStatus homests;
                int[] homestage = new int[4];
                int[] homeerror = new int[4];
                do
                {
                    bool isHomeEnd = true;
                    for (int i = 1; i < totalAxes+1; i++)
                    {
                        mc.GT_GetHomeStatus((short)actCardId, (short)i, out homests);
                        isHomeEnd = isHomeEnd && (homests.run == 0);
                        homestage[i-1] = homests.stage;
                        homeerror[i-1] = homests.error;
                    }
                    if (isHomeEnd)
                        break;

                    Thread.Sleep(2);
                } while (true);

                for (int i = 0; i < 4; i++)
                {
                    if (homestage[i] != 100 || homeerror[i] != 0)//回零完成并且回零没有错误
                        return 1;
                }
                return 0;
            }
        }

        // acc,dec单位是pulse/s²
        // @sjx 单位？ pules/ms²
        public override int SetAxisAccAndDec(int actCardId, int axisId, double acc, double dec)
        {
            int prfMode;
            uint clk;
            mc.TTrapPrm trapPrm;
            lock (locker)
            {
                //if (mc.GT_GetPrfMode((short)actCardId, (short)axisId, out prfMode, 1, out clk) != 0)
                //    return 1;
                //if (prfMode != 2)
                //    return 1;
                if (mc.GT_GetTrapPrm((short)actCardId, (short)axisId, out trapPrm) != 0)
                    return 1;
                trapPrm.acc = acc / 1000000; // convert to pulse/ms²
                trapPrm.dec = dec / 1000000;
                if (mc.GT_SetTrapPrm((short)actCardId, (short)axisId, ref trapPrm) != 0)
                    return 1;
            }
            return 0;
        }

        //接口统一
        public override void ClearError(int actCardId)
        {
            lock (locker)
            {
                mc.GT_SetDo((short)actCardId, mc.MC_CLEAR, (int)(Math.Pow(2, totalAxes) - 1));
                Thread.Sleep(300);
                mc.GT_SetDo((short)actCardId, mc.MC_CLEAR, 0);
                mc.GT_ClrSts((short)actCardId, 1, (short)totalAxes); //0号卡位8轴控制器
            }
        }

        private short ClearError(int actCardId, int axisId)
        {
            lock (locker)
            {
                short rtn;
                if (actCardId == 3)
                {
                    rtn = mc.GT_SetDoBit((short)actCardId, mc.MC_CLEAR, (short)axisId, 1);
                    if (rtn != 0)
                        return rtn;
                    Thread.Sleep(300);
                    rtn = mc.GT_SetDoBit((short)actCardId, mc.MC_CLEAR, (short)axisId, 0);
                    if (rtn != 0)
                        return rtn;
                }
                return mc.GT_ClrSts((short)actCardId, (short)axisId, 1);
            }
        }

        public override bool WaitLogicalMotionEnd(int actCardId, int axis, int timeOutMilliseconds = -1)
        {
            //int axisSts;
            //uint clk;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                if(!IsAxisBusy(actCardId, axis))
                    break;
                //mc.GT_GetSts((short)actCardId, (short)axis, out axisSts, 1, out clk);
                if (timeOutMilliseconds > 0 && sw.ElapsedMilliseconds > timeOutMilliseconds)
                    return false;
                Thread.Sleep(1);
            } while (true);//规划器停止
            //@sjx while ((axisSts & 0x400) != 0x400);//规划器停止
            return true ;
        }

        public override int SetAxisBand(int actCardId, int axisId, int band, int sattleTime)
        {
            lock (locker)
            {
                if (runOffline)
                    return 0;
                //int bandInPulse = (int)XConvert.MM2PULS(band, XDevice.Instance.FindAxisByActId(actCardId, axisId, cardType).Lead);
                return mc.GT_SetAxisBand((short)actCardId, (short)axisId, band, sattleTime);//设置误差带和整定时间
            }
        }

        public override bool WaitMotionEnd(int actCardId, int axis, int timeOutMilliseconds = -1)
        {
            //int axisSts;
            //uint clk;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                if(IsAxisSettled(actCardId, axis))
                    break;
                //mc.GT_GetSts((short)actCardId, (short)axis, out axisSts, 1, out clk);
                if (timeOutMilliseconds > 0 && sw.ElapsedMilliseconds > timeOutMilliseconds)
                    return false;
                Thread.Sleep(3);
            } while (true);
            return true;
            //@sjx while ((axisSts & 0x800) != 0x800);
        }

        // @sjx 获取轴的状态
        private int GetAxisSts(int actCardId, int axis)
        {
            if (runOffline) return 0;

            int axisSts;
            uint clk;
            lock (locker)
            {
                mc.GT_GetSts((short)actCardId, (short)axis, out axisSts, 1, out clk);
            }
            return axisSts;
        }

  
        //// 获取轴的限位传感器状态
        //private int GetAxisLimitSnrSts(int actCardId, int axis)
        //{
        //    if (runOffline) return 0;

        //    int axisSts;
        //    uint clk;
        //    lock (locker)
        //    {
        //        mc.GT_GetSts((short)actCardId, (short)axis, out axisSts, 1, out clk);
        //    }
        //    return axisSts;
        //}


        public override int Update(int actCardId)
        {
            if (runOffline) return 0;
			lock (locker)
            {
	            try
	            {
	                int dists, dosts;
	                for (int i = 0; i < totalExtIOChannel + 1; i++)
	                {
	                    if (i == 0)//channel0为控制卡自带的IO
	                    {
	                        mc.GT_GetDi((short)actCardId, mc.MC_GPI, out dists);
	                        mc.GT_GetDo((short)actCardId, mc.MC_GPO, out dosts);
	                    }
	                    else//其余的为扩展IO
	                    {
	                        ushort dists1, dosts1;
	                        mc.GT_GetExtIoValue((short)actCardId, (short)(i - 1), out dists1);  //  扩展IO的channel从0开始索引
	                        mc.GT_GetExtDoValue((short)actCardId, (short)(i - 1), out dosts1);
	                        dists = dists1;
	                        dosts = dosts1;
	                    }
	                    if (didoReverse)
	                    {
	                        for (int j = 0; j < 16; j++)
	                        {
	                            DI_Data[i * 16 + j] = (dists & (1 << j)) == 0 ? 1 : 0;
	                            DO_Data[i * 16 + j] = (dosts & (1 << j)) == 0 ? 1 : 0;
	                        }
	                    }
	                    else
	                    {
	                        for (int j = 0; j < 16; j++)
	                        {
	                            DI_Data[i * 16 + j] = (dists & (1 << j)) == 0 ? 0 : 1;
	                            DO_Data[i * 16 + j] = (dosts & (1 << j)) == 0 ? 0 : 1;
	                        }
	                    }
	                }
	            }
	            catch (Exception e)
	            {
	                return 1;
	            }
			}
            return 0;

        }

        //@sjx 单位?
        public int SetVel(int actCardId,int axis, double vel)
        {
            lock (locker)
            {
                short rtn;
                //vel = XConvert.MM2PULS(vel, 1); // mm/s convert to pules/ms
                rtn = mc.GT_SetVel((short)actCardId, (short)axis, vel);
                if (rtn != 0)
                    return 1;
                return 0;
            }
        }
        public AxisState ReadStatus(int actCardId, int axisId)
        {
            lock (locker)
            {
                int status;
                short rtn;
                uint clock;
                rtn = mc.GT_GetSts((short)actCardId, (short)axisId, out status, 1, out clock);
                if (rtn != 0)
                {
                    return AxisState.NODEFINED;
                }
                else
                {
                    if (0 != (status & 0x100))//emergencyon
                        return AxisState.EMERGENCYALARM;
                    if (0 != (status & 0x2))//alarmOn
                        return AxisState.SERVOALARMON;
                    if (0 != (status & 0x20))//positivetrig
                        return AxisState.POSITIVETRIG;
                    if (0 != (status & 0x40))//negativetrig
                        return AxisState.NEGATIVETRIG;
                    if (0 != (status & 0x400))//isMoving
                        return AxisState.INMOVING;
                    else if (0 == (status & 0x400))
                        return AxisState.AXISSTOPPED;
                    if (0 != (status & 0x200))//poweron
                        return AxisState.POWERON;
                    else if (0 == (status & 0x200))
                        return AxisState.POWEROFF;
                    return AxisState.NODEFINED;
                }
            }
        }
        public bool GetHomeSnrSts(int actCardId, short axisn)
        {
            int value;
            lock (locker)
            {
                mc.GT_GetDi((short)actCardId, mc.MC_HOME, out value);
            }
            if ((value & 1 << (axisn - 1)) > 0)
                return false;//0表示触发限位,返回false，
            else
                return true;//1表示没有触发,返回true
        }

        public bool GetPosLimitSts(int actCardId,short axisn)
        {
            int value;
            lock (locker)
            {
                mc.GT_GetDi((short)actCardId, mc.MC_LIMIT_POSITIVE, out value);
            }
            if ((value & 1 << (axisn - 1)) > 0)
                return true;//0表示触发限位,返回true，
            else
                return false;//1表示没有触发,返回false
        }
        public bool GetNegLimitSts(int actCardId,short axisn)
        {
            int value;
            lock (locker)
            {
                mc.GT_GetDi((short)actCardId, mc.MC_LIMIT_NEGATIVE, out value);
            }
            if ((value & 1 << (axisn - 1)) > 0)
                return true;//0表示触发限位,返回true，
            else
                return false;//1表示没有触发,返回false
        }

        private int InitExtIOModule(int actCardId, string extIOConfigPath)
        {
            short rtn;
            rtn = mc.GT_OpenExtMdl((short)actCardId, "gts.dll");
            if (rtn != 0)
                return 1;
            rtn = mc.GT_ResetExtMdl((short)actCardId);
            if (rtn != 0)
                return 1;
            rtn = mc.GT_LoadExtConfig((short)actCardId, extIOConfigPath);
            if (rtn != 0)
                return 1;
            for (short i = 0; i < totalExtIOChannel; i++)
            {
                short chn = 0;
                ushort sts = 0;
                int ret = mc.GT_GetStsExtMdl((short)actCardId, i, chn, out sts);
                if (ret != 0)
                {
                    return 1;
                }
                if (sts != 0)
                {
                    return 1;
                }
            }
            return 0;
        }
        private int SetZero(int actCardId, int axis)
        {
            short rtn;
            lock (locker)
            {
                rtn = mc.GT_ZeroPos((short)actCardId, (short)axis, 1);
            }
            if (rtn != 0)
                return 1;
            return 0;
        }
        private bool IsAxisEnabled(int actCardId,int axis)
        {
            int psts;
            uint clk;
            mc.GT_GetSts((short)actCardId,(short)axis,out psts,1,out clk);
            if (0 != (psts & 0x200))//poweron
                return true;
            else
                return false;
        }


        public override int WritePos(int actCardId, object array, string name)
        {
            return 1;
        }
        public override int Close(int actCardId)
        {
            lock (locker)
            {
                short rtn;
                rtn = mc.GT_CloseExtMdl((short)actCardId);
                if (rtn != 0)
                {
                    errorMessage = "扩展模块关闭失败！";
                    return 1;
                }

                rtn = mc.GT_Close((short)actCardId);
                if (rtn != 0)
                {
                    errorMessage = "控制卡关闭失败！";
                    return 1;
                }
                IsConnected = false;
                return 0;
            }
        }

        public override int SetSoftLimit(int actCardId, int axis, double positive, double negative)
        {
            double lead = XDevice.Instance.FindAxisByActId(actCardId, axis, this.cardType).Lead;
            positive = XConvert.MM2PULS(positive, lead);
            negative = XConvert.MM2PULS(negative, lead);
            int rtn = mc.GT_SetSoftLimit((short)actCardId, (short)axis, (int)positive, (int)negative);
            if (rtn != 0)
                return 1;
            return 0;
        }

        // snrType： 0, Home; 1, Pos Limit; 2, Neg Limit
        private bool MoveOutOfSnr(int actCardId, int axis, short snrType, bool posDir,double vel,double lead)
        {
            vel = posDir ? vel : -vel;

            lead = XDevice.Instance.FindAxisByActId(actCardId, axis, cardType).Lead;

            double startPos = 0;
            GetMotionPos(actCardId, axis, ref startPos);
            startPos = XConvert.PULS2MM(startPos, lead); // 起始位置
            MoveJog(actCardId, axis, 1, vel);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                bool snrSts;
                if (snrType == 0)
                {
                    snrSts = GetHomeSnrSts(actCardId, (short)axis);
                }
                else if (snrType == 1)
                {
                    snrSts = GetPosLimitSts(actCardId, (short)axis);
                }
                else
                {
                    snrSts = GetNegLimitSts(actCardId, (short)axis);
                }
                if (snrSts == false)
                {
                    Stop(actCardId, axis);
                    break;
                }

                double curPos = 0;
                GetMotionPos(actCardId, axis, ref curPos);
                curPos = XConvert.PULS2MM(curPos, lead); // 起始位置

                if (sw.ElapsedMilliseconds > 5000 || Math.Abs(curPos - startPos) > 80)  // 最大时间30s, 角度或者位移超出80度或者80mm
                {
                    Stop(actCardId, axis);
                    return false;
                }
                Thread.Sleep(5);
            } while (true);

            Thread.Sleep(200);
            return true;
        }

        public override bool IsAxisError(int actCardId, int axis)
        {
            int status = GetAxisSts(actCardId, axis);
            if (0 != (status & 0x2))//alarmOn
                return true;
            return false;
        }
    }
}

