using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Demo.Globals;
using XCore;
using Demo.Device;
using AutoStudio.Core.Tools;
using HB_IWatch;
using Demo.UserControls;
using System.Threading;


namespace Demo.Page
{
    public partial class PageEngineering : UserControlBase
    {
        private static PageEngineering instance;
        public static PageEngineering Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new PageEngineering();
                return instance;
            }
        }
        public PageEngineering()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            switchButton1.SetText("Manual Debug");
            this.switchButton1.ON += SwitchButton1_ON;
            this.switchButton1.OFF += SwitchButton1_OFF;
            Init();
            timer1.Interval = 100;
            timer1.Start();
        }

        private void Init()
        {
            //Add runMode
            this.comboBox_Mode.Items.Add(MultiLanguage.GetMessage(MachineRunMode.NormalRun.ToString()));
            this.comboBox_Mode.Items.Add(MultiLanguage.GetMessage(MachineRunMode.Assemble_Dry_Run.ToString()));
            this.comboBox_Mode.Items.Add(MultiLanguage.GetMessage(MachineRunMode.Conveyor_Dry_Run.ToString()));
            this.comboBox_Mode.Items.Add(MultiLanguage.GetMessage(MachineRunMode.Entire_Machine_Dry_Run.ToString()));
            this.comboBox_Mode.Items.Add(MultiLanguage.GetMessage(MachineRunMode.Single_Reinspection.ToString()));
            this.comboBox_Mode.SelectedIndexChanged += ComboBox_Mode_SelectedIndexChanged; ;
            this.comboBox_Mode.SelectedIndex = 0;
        }

        public void UpdateTextBox(string content)
        {
                UpdateText($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:dd:ss")}: {content}");
        } 

        private void UpdateText(string text)
        {
            // Kiểm tra nếu InvokeRequired là true, tức là đang gọi từ thread khác
            if (txtLogEngineer.InvokeRequired)
            {
                // Sử dụng Invoke để thực thi UpdateText trên UI thread
                txtLogEngineer.Invoke(new Action(() => txtLogEngineer.Text += text + "\r\n"));
            }
            else
            {
                // Nếu không cần Invoke, cập nhật trực tiếp
                txtLogEngineer.Text += text + "\r\n";
            }
        }

        private void ComboBox_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Chon runMode
            var st1 = XStationManager.Instance.FindStationById((int)StationId.Scanner).State;

            if (st1 == XStationState.RUNNING || st1 == XStationState.PAUSE)
            {
                switch (Globals.RUNMODE)
                {
                    case MachineRunMode.NormalRun:
                        this.comboBox_Mode.SelectedIndex = 0;
                        break;
                    case MachineRunMode.Assemble_Dry_Run:
                        this.comboBox_Mode.SelectedIndex = 1;
                        break;
                    case MachineRunMode.Conveyor_Dry_Run:
                        this.comboBox_Mode.SelectedIndex = 2;
                        break;
                    case MachineRunMode.Entire_Machine_Dry_Run:
                        this.comboBox_Mode.SelectedIndex = 3;
                        break;
                    case MachineRunMode.Single_Reinspection:
                        this.comboBox_Mode.SelectedIndex = 4;
                        break;
                    default:
                        this.comboBox_Mode.SelectedIndex = 0;
                        break;
                }
                BzMessagebox.Show(("There is a task currently running.\n Please stop the device before switching modes."),
                    "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (this.comboBox_Mode.SelectedIndex)
            {
                case 0:
                    Globals.RUNMODE = MachineRunMode.NormalRun;
                    break;
                case 1:
                    Globals.RUNMODE = MachineRunMode.Assemble_Dry_Run;
                    break;
                case 2:
                    Globals.RUNMODE = MachineRunMode.Conveyor_Dry_Run;
                    break;
                case 3:
                    Globals.RUNMODE = MachineRunMode.Entire_Machine_Dry_Run;
                    break;
                case 4:
                    Globals.RUNMODE = MachineRunMode.Single_Reinspection;
                    break;
                default:
                    Globals.RUNMODE = MachineRunMode.NormalRun;
                    break;
            }
            //Update UI 
            Globals.SetTopState();
        }

        private void SwitchButton1_OFF()
        {
            DebugDlg.Instance.Close();
        }

        private void SwitchButton1_ON()
        {
            DebugDlg.Instance.Show();
        }
        private int iCunt = 0;
        private int EMGStr = 0;
        private Dictionary<int, bool> m_asyncHandled = new Dictionary<int, bool>()
        {
            {0,true}
        };
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool safeDoorSts = true;

            //Kiểm tra cửa an toàn
            foreach (KeyValuePair<XDi, bool> kvp in XMachine.Instance.signalDoor)
            {
                if (XMachine.Instance.DictDoorDiCheck.Keys.Contains(kvp.Key) && !XMachine.Instance.DictDoorDiCheck[kvp.Key])
                {
                    continue;
                }
                DISTSTYPE diKeySts = DISTSTYPE.LOW;
                kvp.Key.GetDi(ref diKeySts);

                if (diKeySts == DISTSTYPE.LOW)
                {
                    //if (((!Globals.SettingOption.IsOpensafeDoor()) && (!kvp.Value)) || Globals.Offline)
                    //    continue;
                    //else
                    //{
                    //    safeDoorSts = false;
                    //    break;
                    //}
                }
            }

            this.btnSafeDoor.BackColor = safeDoorSts ? MyColor.Green : MyColor.Red;

            if (!safeDoorSts && iCunt == 0)
            {

                //偶尔出现安全门打开task不能暂停的情况，改为异步的
                if (m_asyncHandled[0])
                {
                    m_asyncHandled[0] = false;
                    string erro = "Cửa an toàn đang mở";
                    HBMachine.Instance.ShowErroAsync(XAlarmId.DOOR_OPEN.ToString(), MultiLanguage.GetMessage(erro),"error","Đóng cửa an toàn",
                                                    "安全门报警", MultiLanguage.GetMessage("确认"), "", "", 
                                                    new XCore.CallbackAction(() => { m_asyncHandled[0] = true; return true; }), true);
                }
            }

            if (XDevice.Instance.FindDiById((int)DiId.主设备急停).STS == DISTSTYPE.LOW &&
                                        XDevice.Instance.FindDiById((int)DiId.左供料机急停).STS == DISTSTYPE.LOW &&
                                        XDevice.Instance.FindDiById((int)DiId.右供料机急停).STS == DISTSTYPE.LOW)
            {
                this.btnEMG.BackColor = MyColor.Green;
                if (EMGStr != 1)
                {
                    HBMachine.Instance.SetMachineStatus(MachineSts.Idle);
                    EMGStr = 1;
                }
            }
            else
            {
                this.btnEMG.BackColor = MyColor.Red;
                HBMachine.Instance.SetMachineStatus(MachineSts.Downtime);
                EMGStr = 2;
            }
          
        }
    }
}
