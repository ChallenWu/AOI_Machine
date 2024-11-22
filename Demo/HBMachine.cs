using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCore;
using System.Drawing;
using Demo;
using Demo.UserControls;
using BoTech;
using static NPOI.HSSF.Util.HSSFColor;
using System.Data;
using NPOI.SS.Formula.Functions;
using System.Diagnostics;

namespace HB_IWatch
{

    enum StationRunMode
    {
        EmptyRun,
        AutoRun,
        CPK_Inspection,
    }
    public enum MachineSts
    {
        /// <summary>
        /// machine Working ok
        /// </summary>
        Running = 1,
        /// <summary>
        /// machine waiting and ok
        /// </summary>
        Idle = 2,
        /// <summary>
        /// machine in Standby
        /// </summary>
        Engineering = 3,
        /// <summary>
        /// machine happen need engineering or maintenance
        /// </summary>
        Planned_downtime = 4,
        /// <summary>
        /// machine in Stopped
        /// </summary>
        Downtime = 5,
    }

    public class HBMachine
    {

        private static HBMachine _HBMachine = null;

        private object obj = new object();

        private object m_ListLock = new object();

        private MachineSts curSts;

        public MachineSts CurSts { get { return curSts; } }

        private List<MachineSts> ListBackupMcSts = new List<MachineSts>();

        private DoId m_Light = DoId.绿灯;
        private Timer LightFlash = new Timer();
        private bool m_isFlash;

        private HBMachine()
        {
            //LightFlash.Interval = 500;
            //LightFlash.Enabled = true;
            //LightFlash.Tick += new EventHandler(timer_LightFlash);
        }
        //private void timer_LightFlash(object sender, EventArgs e)
        //{
        //    if (m_isFlash)
        //    {
        //        if (XDevice.Instance.FindDoById((int)m_Light).STS == DOSTSTYPE.HIGH)
        //            Motion.SetDo(m_Light, DOSTSTYPE.LOW);
        //        else if (XDevice.Instance.FindDoById((int)m_Light).STS == DOSTSTYPE.LOW)
        //            Motion.SetDo(m_Light, DOSTSTYPE.HIGH);
        //    }
        //}

        //private void LightClear()
        //{
        //    m_isFlash = false;
        //    Motion.SetDo(DoId.红灯, DOSTSTYPE.LOW);
        //    Motion.SetDo(DoId.绿灯, DOSTSTYPE.LOW);
        //    Motion.SetDo(DoId.黄灯, DOSTSTYPE.LOW);
        //    SetBeeperOn(false);
        //}

        //private void Light_Control(DoId controlLight, bool isFlash, bool isbeeOn)
        //{
        //    LightClear();
        //    m_Light = controlLight;
        //    Motion.SetDo(m_Light, DOSTSTYPE.HIGH);
        //    if (isFlash)
        //        m_isFlash = true;
        //    if (Globals.SettingOption.是否开启蜂鸣器)
        //        SetBeeperOn(isbeeOn);
        //}

        public static HBMachine Instance
        {
            get
            {
                if (_HBMachine == null)
                {
                    _HBMachine = new HBMachine();

                }
                return _HBMachine;
            }
        }

        /// <summary>
        /// Gọi trạng thái máy đẩy vào trong SQLite
        /// </summary>
        /// <param name="hs"></param>
        /// <returns></returns>
        public string UploadMachineStateMessage(MachineSts machineStatus)
        {
            //Lấy trạng thái previousStatus từ SQLite
            DataTable temDt = DataServerManager.Instance.SelectLastMachineState();  

            HiveMessage hm = new HiveMessage() { HappenTime = DateTime.Now, MachineState = (int)machineStatus, PreviousState = 0, TimeDuration = 0 };

            #region Modify data
            //Không có dữ liệu nào được đọc ra
            if (temDt.Rows.Count <= 0)
            {
                DataServerManager.Instance.InsertMachineState(hm);
            }
            else
            {
                DateTime dtt = (DateTime)temDt.Rows[0][1];
                hm.PreviousState = (int)temDt.Rows[0][3];
                try
                {
                    var intDay = hm.HappenTime.Date.Day - dtt.Day;
                    //if (intDay == 1)
                    //{
                    //    //for(int i = 0; i < temDt.Rows.Count;)
                    //    //If the next day, insert a piece of data with unchanged status into the database;
                    //    hm.TimeDuration = (long)(hm.HappenTime - dtt).TotalSeconds;
                    //    hm.MachineState = (int)temDt.Rows[0][3];
                    //    DataServerManager.Instance.InsertMachineState(hm);


                    //}
                    if (intDay == 0)
                    {
                        //If it is produced within one day;
                        hm.TimeDuration = (long)(hm.HappenTime - dtt).TotalSeconds;
                        //edit
                        DataServerManager.Instance.InsertMachineState(hm);
                    }
                    else
                    {
                        //Multiple days of data will be insert a status on 00:00:00 AM everyday
                        if (intDay >= 1)
                        {
                            if (intDay > 7)
                                intDay = 7;

                            //Bù vào thời điểm trống các ngày
                            for (int i = 0; i < intDay; i++)
                            {
                                //Lấy dữ liệu trạng thái gần nhất
                                temDt = DataServerManager.Instance.SelectLastMachineState();
                                //Tính thời điểm

                                var transday = DateTime.Now.AddDays(-intDay + 1 +  i).Date.AddSeconds(-1);
                                hm.TimeDuration = (long)(transday - (DateTime)temDt.Rows[0][1]).TotalSeconds;
                                //Trạng thái thiết bị
                                hm.MachineState = (int)temDt.Rows[0][3];
                                //Thời gian insert
                                hm.HappenTime = transday;
                                //Insert trạng thái vào sql
                                DataServerManager.Instance.InsertMachineState(hm);
                            }


                            temDt = DataServerManager.Instance.SelectLastMachineState();
                            hm.HappenTime = DateTime.Now;
                            hm.TimeDuration = (long)(DateTime.Now - (DateTime)temDt.Rows[0][1]).TotalSeconds;
                            hm.MachineState = (int)machineStatus;
                            DataServerManager.Instance.InsertMachineState(hm);
                        }
                    }
                }
                catch
                { }
            }
            return "";
        }

        // 显示错误对话框，异步非模态, 仅显示信息，选项默认只有一个
        public void ShowWarningAsync(string strDescription, string OKText = "Confirm", CallbackAction callbackAction = null)
        {
            //if (AsynShowWarningTask != null)
            //    AsynShowWarningTask.Wait();

            Task AsynShowWarningTask = new Task(() => ShowDlgAsync(strDescription, OKText, callbackAction));
            AsynShowWarningTask.Start();
        }

        public void ShowErroAsync(string ErrCode, string strDescription, string szType, string AlarmLevel, string Solution, string OKText = "Confirm", string CancelText = "Stop", string IgnoreText = "Inorge", CallbackAction callbackAction = null, bool topmost = false)
        {
            //Them errcode
            Task task = new Task(() =>
            {
                ShowError(ErrCode, strDescription, AlarmLevel, Solution, szType, OKText, CancelText, IgnoreText, -1, topmost);
                if (callbackAction != null)
                    callbackAction();
            });

            task.Start();
        }

        public void ShowDlgAsync(string strDescription, string OKText = "Confirm", CallbackAction callbackAction = null)
        {
            //lock (obj)
            {
                FailTipEx ft = new FailTipEx(strDescription, false, false, callbackAction);
                ft.StartPosition = FormStartPosition.Manual;
                ft.Left = 50;
                ft.Top = 100;
                ft.BackColor = Color.Yellow;

                ft.SelectAll();
                ft.SetSubmitText(OKText);
                ft.SelectAll();
                ft.SetSubmitText(OKText);


                //供料机异步报警暂时不切换三色灯状态，上下料仓都没有物料会报警
                // 备份设备状态
                //MachineSts bkupSts = CurSts;

                //AddBackupMcStatus(CurSts);

                //SetMachineStatus(MachineSts.Warning);
                ft.ShowDialog();
                ft.WaitOne();

                // 还原设备状态 
                //RecoverMachineStatus();
                //SetMachineStatus(bkupSts);
            }
        }

        // 显示错误对话框
        public DialogResult ShowError(string ErrCode, string strDescription, string AlarmLevel, string Solution, string szType = "", 
                                      string OKText = "Retry", string CancelText = "Stop", string IgnoreText = "Inorge", 
                                      int timeout = -1, bool topmost = false)
        {
            lock (obj)
            {

                if (XTask.OnPauseActive != null)
                    XTask.OnPauseActive(null, null);


                DateTime startTime = DateTime.Now;
                bool isOKVisable = (OKText != "");
                bool isCancalVisable = (CancelText != "");
                bool isIgnoreVisable = (IgnoreText != "");
                DateTime lastAlarmEndTime = DateTime.Now;

                //Dung giao dien NewFailTip
                AlarmCode alarmCode = new AlarmCode();
                //ID code
                alarmCode.ErrorCode = ErrCode;
                alarmCode.ErrorCategory = szType;
                alarmCode.MessageEn = strDescription;
                alarmCode.Severity = AlarmLevel;
                alarmCode.DealtMethod = Solution;
                //AC.EndTime = AC.HappenTime.AddSeconds(1);

                AlarmMessage Am = new AlarmMessage() { EffectiveHappenTime = DateTime.Now, HappenTime = DateTime.Now,
                                                       AlarmSerialNumber = DateTime.Now.ToString("yyyyMMddHHmmssffffff") };
                if (alarmCode.ErrorCode.Length >= 1)
                    Am.NowAlarm = alarmCode;
                Am.EndTime = Am.HappenTime.AddSeconds(1);

                //Add dữ liệu lỗi vào SQLite
                DataServerManager.Instance.InsertAlarmON(Am);
                UploadMachineStateMessage(MachineSts.Downtime);
                //Dung giao dien FailTip
                FailTip ft = new FailTip(strDescription, isCancalVisable, isIgnoreVisable, timeout, isOKVisable);
                ft.SelectAll();
                if (isOKVisable || (!isCancalVisable && !isIgnoreVisable))
                    ft.SetSubmitText(OKText);
                if (isCancalVisable)
                    ft.SetCancelText(CancelText);
                if (isIgnoreVisable)
                    ft.SetIgnoreText(IgnoreText);

                ft.TopMost = topmost;

                //Lưu trạng thái lỗi
                AddBackupMcStatus(curSts);
                //Bật đèn báo hiệu alarm
                SetMachineStatus(MachineSts.Downtime);

                ft.ShowDialog();
                ft.WaitOne();
                //if(ft.ShowDialog() == DialogResult.OK)

                //还原设备状态
                //if (ft.DialogResult == DialogResult.OK || ft.DialogResult == DialogResult.Ignore || ft.DialogResult == DialogResult.Cancel)
                    //SetMachineStatus(bkupSts);
                RecoverMachineStatus();
                if (ft.DialogResult == DialogResult.Cancel || ft.DialogResult == DialogResult.Ignore)
                {
                    //if (XTask.OnStopActive != null)
                    //    XTask.OnStopActive(null, null);
                }
                if (ft.DialogResult == DialogResult.OK)
                {
                    XStationManager.Instance.Continue();
                }
                //Update thời gian xử lý lỗi                
                DateTime now = DateTime.Now;
                Am.EffectiveHappenTime = Am.EffectiveHappenTime >= lastAlarmEndTime ? Am.EffectiveHappenTime : lastAlarmEndTime;
                lastAlarmEndTime = now;
                Am.EndTime = now;

                if (Am.EndTime.ToString("yyyy").Contains("0001"))
                {
                    Am.EndTime = DateTime.Now;
                }
                DataServerManager.Instance.UpdateAlarm(Am);
                return ft.DialogResult;
            }
        }

        // 显示警告对话框
        public DialogResult ShowWarning(string strDescription, string OKText = "Confirm", string CancelText = "Stop", string IgnoreText = "Inorge", int timeout = -1)
        {
            lock (obj)
            {
                bool isCancalVisable = (CancelText != "");
                bool isIgnoreVisable = (IgnoreText != "");
                FailTip ft = new FailTip(strDescription, isCancalVisable, isIgnoreVisable, timeout);
                ft.SelectAll();
                ft.SetSubmitText(OKText);
                if (isCancalVisable)
                    ft.SetCancelText(CancelText);
                if (isIgnoreVisable)
                    ft.SetIgnoreText(IgnoreText);

                // 备份设备状态
                //MachineSts bkupSts = CurSts;
                AddBackupMcStatus(curSts);

                SetMachineStatus(MachineSts.Downtime);
                ft.ShowDialog();
                ft.WaitOne();

                // 还原设备状态
                //SetMachineStatus(bkupSts);
                RecoverMachineStatus();

                return ft.DialogResult;
            }
        }

        public void RecoverMachineStatus()
        {
            lock (m_ListLock)
            {
                if (ListBackupMcSts.Count > 0)
                {
                    if (ListBackupMcSts.Contains(MachineSts.Downtime))
                    {
                        RemoveBackupMcSts(MachineSts.Downtime);
                        SetMachineStatus(MachineSts.Downtime);
                    }
                    else if (ListBackupMcSts.Contains(MachineSts.Planned_downtime))
                    {
                        RemoveBackupMcSts(MachineSts.Planned_downtime);
                        SetMachineStatus(MachineSts.Planned_downtime);
                    }
                    else if (ListBackupMcSts.Contains(MachineSts.Running))
                    {
                        SetMachineStatus(MachineSts.Running);
                    }   
                    else
                    {
                        SetMachineStatus(MachineSts.Idle);
                    }
                }
                else
                {
                    if (IsMachineRunning())
                        SetMachineStatus(MachineSts.Running);
                    else
                        SetMachineStatus(MachineSts.Idle);
                }
            }
        }

        public void AddBackupMcStatus(MachineSts mcSts)
        {
            lock (m_ListLock)
            {
                ListBackupMcSts.Add(mcSts);
            }
        }

        private void RemoveBackupMcSts(MachineSts mcSts)
        {
            lock (m_ListLock)
            {
                if (ListBackupMcSts.Contains(mcSts))
                    ListBackupMcSts.Remove(mcSts);
            }
        }

        private void ClearBackupMcSts()
        {
            lock (m_ListLock)
            {
                ListBackupMcSts.Clear();
            }
        }

        // 更新设备状态
        //Setup chế độ đèn cho thiết bị
        //Dùng cho chế độ PC sử dụng PCI Motion + I/O
        public void SetMachineStatus(MachineSts sts)
        {
            curSts = sts;
            //switch (sts)
            //{
            //    case MachineSts.Idle:
            //        ClearBackupMcSts();
            //        // 黄灯闪烁
            //        Light_Control(DoId.黄灯, false, false);
            //        break;
            //    case MachineSts.Running:
            //        ClearBackupMcSts();
            //        // 绿灯亮
            //        Light_Control(DoId.绿灯, false, false);
            //        break;
            //    case MachineSts.Alarm:
            //        //红灯亮，开蜂鸣器
            //        Light_Control(DoId.黄灯, true, true);
            //        break;
            //    case MachineSts.Warning:
            //        //黄灯闪烁，开蜂鸣器
            //        Light_Control(DoId.黄灯, true, true);
            //        break;
            //    case MachineSts.WaitCarrier:
            //        //绿灯闪烁
            //        Light_Control(DoId.绿灯, true, false);
            //        break;
            //}
        }

        // Turn on the buzzer
        public void SetBeeperOn(bool on)
        {
            XDevice.Instance.FindDoById((int)DoId.蜂鸣).SetDo(on ? DOSTSTYPE.HIGH : DOSTSTYPE.LOW);
        }

        public bool IsMachineRunning()
        {
            var st2 = XStationManager.Instance.FindStationById((int)StationId.Scanner).State;
            if (st2 == XStationState.RUNNING || st2 == XStationState.PAUSE )
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
