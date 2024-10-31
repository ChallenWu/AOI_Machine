using AutoStudio.Forms;
using AutoStudio.Core;
using Demo.Device;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCore;
using System.Threading;
using System.Collections.Concurrent;
using BoTech;
using System.Runtime.InteropServices.ComTypes;
using Newtonsoft.Json;
using System.Collections;

namespace Demo.Page
{
    public partial class DebugDlg : Form
    {
        private ConcurrentQueue<byte> rxQueue = new ConcurrentQueue<byte>();
        private AutoResetEvent rxSignal = new AutoResetEvent(false);
        SerialPort serialPort = new SerialPort();
        string DataIn;
        System.DateTime dt = System.DateTime.Now;
        bool keepReading = false;


        public DebugDlg()
        {
            InitializeComponent();
            timer1.Start();
            this.StartPosition = FormStartPosition.CenterScreen;
            
        }
        private static DebugDlg instance;
        public static DebugDlg Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new DebugDlg();
                return instance;
            }
        }

        //Update UI
        private void timer1_Tick(object sender, EventArgs e)
        {

        }


        #region Inovance PLC
        private void btnConnectPLC_Click(object sender, EventArgs e)
        {
            if (!ModbusApiH5U.IsConnected)
            {
                //PublicPLCData.Instance._ModbusApiH5U.PLCConnect("192.168.250.11", 1);
                var ip = Globals.SettingICT.PLC_IP;
                var kq = PublicPLCData.Instance.ModbusApiH5U.PLCConnect(ip, 1);
                if (kq)
                {

                }
            }
        }
        private void btnDisconnectPLC_Click(object sender, EventArgs e)
        {
            PublicPLCData.Instance.ModbusApiH5U.DisConnect(1);
        }
        private void btnWritePLC_Click(object sender, EventArgs e)
        {
            short value = short.Parse(textBox4.Text);
            ModbusApiH5U.WriteValueInt16(SoftElemType.REGI_H5U_D, PublicPLCData.triggerRegister_D, value);
        }
        #endregion

        #region Scanner through COM Port
        private void btnConnectScanner_Click(object sender, EventArgs e)
        {
           // ScannerComm.Instance.Start();
            ICW_Scanner.Instance.Connect();
        }
        private void btnDisconnectScanner_Click(object sender, EventArgs e)
        {
            //ScannerComm.Instance.Stop();
            ICW_Scanner.Instance.Disconnect();
        }
        private void btnTriggerScanner_Click(object sender, EventArgs e)
        {
            //ScannerComm.Instance.Start();
            //txtLog.Text = ScannerComm.Instance.ReadQR();
            //ScannerComm.Instance.Stop();
            Thread new1 = new Thread(() =>
            ICW_Scanner.Instance.ReadQR()
            );
            new1.Start();        
        }
        private void btnClearCom_Click(object sender, EventArgs e)
        {
            ICW_Scanner.Instance.StrData = "";
        }
        #endregion

        #region Alarm
        AlarmMessage am;
        private void btnAlarmIn_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime after = now.AddMinutes(1);
            am = new AlarmMessage() { HappenTime = now, EffectiveHappenTime = now, EndTime = after, Duration = 5.8, DurationCatigory = 5, AlarmSerialNumber = DateTime.Now.ToString("yyyyMMddHHmmssfff") };
            am.NowAlarm.ErrorCode = "Alarm0001";
            am.NowAlarm.ErrorCatrgory = "Safety";
            am.NowAlarm.MessageEn = "SafetyAlaem";
            am.NowAlarm.MessageCn = "安全门报警";
            am.NowAlarm.DealtMethod = "Close the door";
            DataServerManager.Instance.InsertAlarmON(am);
        }

        private void AlarmOu_DT_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime before = now.AddDays(-1);

            this.dataGridView1.DataSource = DataServerManager.Instance.SelectDT_Statistic(before, now);
        }

        private void AlarmOu_DC_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime before = now.AddDays(-1);
            this.dataGridView1.DataSource = DataServerManager.Instance.SelectAlarmDurationCategory(before, now);
        }

        private void AlarmOu_All_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime before = now.AddDays(-1);
            this.dataGridView1.DataSource = DataServerManager.Instance.SelectAlarmAll(before, now);
        }

        private void AlarmOu_Allall_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = DataServerManager.Instance.SelectAlarmAll();
        }

        private void AlarmUpdate_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime after = now.AddMinutes(1);
            //am.ErrorCategory = "Sensor";
            DataServerManager.Instance.UpdateAlarm(am);
        }

        private void AlarmDe_Click(object sender, EventArgs e)
        {
            DataServerManager.Instance.DeleteAlarmStaleData();
        }

        #endregion

        #region Unit
        private void UnitIn_Click(object sender, EventArgs e)
        {
            UnitMessage UM = new UnitMessage() { StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(-1), CT = 60.0, HiveState = 1, UnitSN = "Test00001", ComponentSN = "tttttjfsb", Shift = "NS", Pass = "PASS" };
            UCMessage ucm = new UCMessage();
            ucm.Units[0] = UM;
            DataServerManager.Instance.InsertUnitMessage(ucm, 0);
        }

        private void UnitOut_8H_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime before = now.AddHours(-10);

            this.dataGridView1.DataSource = DataServerManager.Instance.Select_8H(before, now);
        }

        private void UnitOut_7D_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime before = now.AddDays(-1).AddHours(-10);
            this.dataGridView1.DataSource = DataServerManager.Instance.Select_7D(before, now);
        }

        private void UnitOut_Du_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime before = now.AddHours(-10);

            this.dataGridView1.DataSource = DataServerManager.Instance.SelectDurationUnit(before, now);
        }

        private void UnitOutAll_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = DataServerManager.Instance.SelectUnitAll();
        }
        #endregion

        #region Machine state
        private void MachineStateIn_Click(object sender, EventArgs e)
        {
            DataTable temDt = DataServerManager.Instance.SelectLastMachineState();
            HiveMessage hm = new HiveMessage() { HappenTime = DateTime.Now, MachineState = 2, PreviousState = 5, TimeDuration = 0 };

            if (temDt.Rows.Count <= 0)
            {

            }
            else
            {
                DateTime dtt = (DateTime)temDt.Rows[0][1];
                hm.TimeDuration = (hm.HappenTime - dtt).Ticks;
                hm.TimeDuration = (long)((hm.HappenTime - dtt).TotalSeconds);
            }

            DataServerManager.Instance.InsertMachineState(hm);
        }

        private void MachineStateOu_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime before = now.AddDays(-1).AddHours(-1);
            this.dataGridView1.DataSource = DataServerManager.Instance.SelectMachineState(before, now);
        }

        private void MachineStateOuALL_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = DataServerManager.Instance.SelectMachineStateAll();
        }
        #endregion

        #region Operation Log
        private void testOOL_Click(object sender, EventArgs e)
        {
            //DateTime now = DateTime.Now;
            //DateTime before = now.AddHours(-1);
            //this.dataGridView1.DataSource = DataServerManager.Instance.SelectOperationLog(before, now);
        }

        private void testIOL_Click(object sender, EventArgs e)
        {
            //OperationLog ol = new OperationLog() { HappenTime = DateTime.Now, AI = AudioLoginManager.Instance.LoggedInAccount, OperationMessage = "Nghỉ: là trạng thái máy không" };
            //DataServerManager.Instance.InsertOperationLog(ol);
        }
        #endregion

        private void DebugDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void DebugDlg_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        // Phương thức cập nhật giao diện người dùng
        private  void UpdateUIText(string text)
        {
            if (txtLog.InvokeRequired)
            {
                // Sử dụng Invoke để thực hiện cập nhật trên UI thread
                txtLog.Invoke(new Action(() => txtLog.Text = text));
            }
            else
            {
                // Nếu đã ở trên UI thread, cập nhật trực tiếp
                if (!string.IsNullOrEmpty(text))
                    txtLog.Text += text;
                txtLog.Update();
                txtLog.Refresh();
            }
        }

        public  void DataReceivedHandler(string strData)
        {
            UpdateUIText(strData);
        }
        #region PLC Mitsubishi
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                SLMP.Instance.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }                
        }

        private  void button1_Click_1(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region MES
        PostType PT = new PostType();
        private void btn_Send_Click(object sender, EventArgs e)
        {
            string sendData = txt_MesCreate.Text.Trim();
            this.txt_Mes.Text = "PC-->Mes: " + sendData;
            string back = "";
            if (ICTMES.HttpPost(sendData, ref back, PT))
            {
                this.txt_Mes.Text += "\r\nMes-->PC: " + back;
            }
            else
                this.txt_Mes.Text += "\r\nMes-->PC: Error " + back;
        }

        private void btn_checkLoggin_Click(object sender, EventArgs e)
        {
            //PT = PostType.AssyCheck;
            //this.lbl_empNo.BackColor = Color.LightGreen;
            //this.lbl_terminalName.BackColor = Color.LightGreen;
            //this.lbl_SerialNumber.BackColor = Color.LightGreen;

            CheckLogin Check_login = new CheckLogin();
            //Dictionary<string, string> tmp = new Dictionary<string, string> { };
            //tmp.Add("user", "0797039");
            //tmp.Add("pwd", "123456a");
            Check_login.SerializeData = "{\"user\":\"0791347\",\"pwd\":\"0791347X\"}";
            Check_login.Indicator = null;
            Check_login.Hwd = "";
            Check_login.cmd = 2;

            string jsonData = JsonConvert.SerializeObject(Check_login, Formatting.Indented);
            this.txt_MesCreate.Text = jsonData;
        }

        private void btn_AssyCheckCreate_Click(object sender, EventArgs e)
        {
            PT = PostType.AssyCheck;
            this.lbl_empNo.BackColor = Color.LightGreen;
            this.lbl_terminalName.BackColor = Color.LightGreen;
            this.lbl_SerialNumber.BackColor = Color.LightGreen;

            AssyCheckData ACD = new AssyCheckData();
            ACD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            ACD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            ACD.serial_Number = "TEBRCP2087000T";
            ACD.machine = "";
            ACD.toolingNo = "";
            ACD.lotNo = "";
            ACD.kpsn = "";
            ACD.reelNo = "";
            ACD.workOrder = AudioSystem.AudioMachineMessage.MES.workOrder;
            string jsonData = JsonConvert.SerializeObject(ACD, Formatting.Indented);
            this.txt_MesCreate.Text = jsonData;
        }

        private void btn_AssyGoCreate_Click(object sender, EventArgs e)
        {
            PT = PostType.AssyGo;
            this.lbl_empNo.BackColor = Color.LightGreen;
            this.lbl_terminalName.BackColor = Color.LightGreen;
            this.lbl_SerialNumber.BackColor = Color.LightGreen;
            AssyGoData AGD = new AssyGoData();
            AGD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            AGD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            AGD.serial_Number = "TEBRCP2087000T";
            AGD.machine = "";
            AGD.toolingNo = "";
            AGD.lotNo = "";
            AGD.kpsn = "";
            AGD.reelNo = "";
            AGD.workOrder = AudioSystem.AudioMachineMessage.MES.workOrder;

            string jsonData = JsonConvert.SerializeObject(AGD, Formatting.Indented);//设置json显示格式
            this.txt_MesCreate.Text = jsonData;
        }

        private void btn_GetCmdCreate_Click(object sender, EventArgs e)
        {
            PT = PostType.GetCmdResult;
            this.lbl_empNo.BackColor = Color.LightGreen;
            this.lbl_terminalName.BackColor = Color.LightGreen;
            this.lbl_SerialNumber.BackColor = Color.LightGreen;
            this.lbl_pCmd.BackColor = Color.LightGreen;
            GetCmdResultData GCRD = new GetCmdResultData();
            GCRD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            GCRD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            GCRD.serial_Number = "";
            GCRD.machine = "";
            GCRD.toolingNo = "";
            GCRD.lotNo = "";
            GCRD.kpsn = "";
            GCRD.workOrder = "";
            GCRD.cavity = "";
            GCRD.testData = "";
            GCRD.results = "";
            GCRD.status = "";
            GCRD.pCmd = cb_pCmd.Text.Trim();
            GCRD.defectCode = "";


            string jsonData = JsonConvert.SerializeObject(GCRD, Formatting.Indented);//设置json显示格式
            this.txt_MesCreate.Text = jsonData;
        }

        private void btn_testDataCreate_Click(object sender, EventArgs e)
        {
            this.lbl_testResult.BackColor = Color.LightGreen;
            this.lbl_unitSn.BackColor = Color.LightGreen;
            this.lbl_uutStart.BackColor = Color.LightGreen;
            this.lbl_uutStop.BackColor = Color.LightGreen;
            this.lbl_SW.BackColor = Color.LightGreen;
            this.lbl_stationID.BackColor = Color.LightGreen;
            Hashtable pars = new Hashtable();
            pars.Add("test_result", "PASS");
            pars.Add("unit_sn", "KSXXXXXXXX00T");
            pars.Add("uut_start", "2022-10-24 10:03:25");
            pars.Add("uut_stop", "2022-10-24 10:04:25");
            pars.Add("limits_version", "v1.0");
            pars.Add("software_name", "BZ1.0");
            pars.Add("software_version", AudioSystem.AudioMachineMessage.SW_version);
            pars.Add("station_id", AudioSystem.AudioMachineMessage.MES.terminalName);
            pars.Add("fixture_id", "");
            string postData = ParsToString(pars);
            this.txt_Test.Text = postData;
        }

        private void btn_AssyResultCreate_Click(object sender, EventArgs e)
        {
            this.lbl_test.BackColor = Color.LightGreen;
            this.lbl_value.BackColor = Color.LightGreen;
            Hashtable pars = new Hashtable();
            pars.Add("lower_limit", "2");
            pars.Add("parametric_key", "");
            pars.Add("priority", "");
            pars.Add("result", "PASS");
            pars.Add("sub_sub_test", "");
            pars.Add("sub_test", "");
            pars.Add("test", "Air_Pressure");
            pars.Add("units", "");
            pars.Add("upper_limit", "3");
            pars.Add("value", "2.54");
            pars.Add("Message", "");
            string postData = ParsToString(pars);
            this.txt_Test.Text = postData;
        }

        private void btn_CollectCreate_Click(object sender, EventArgs e)
        {
            PT = PostType.CollectTestData;
            this.lbl_empNo.BackColor = Color.LightGreen;
            this.lbl_terminalName.BackColor = Color.LightGreen;
            this.lbl_SerialNumber.BackColor = Color.LightGreen;
            this.lbl_testData.BackColor = Color.LightGreen;
            this.lbl_collectType.BackColor = Color.LightGreen;

            CollectTestData CTD = new CollectTestData();
            CTD.empNo = AudioSystem.AudioMachineMessage.MES.empNo;
            CTD.terminalName = AudioSystem.AudioMachineMessage.MES.terminalName;
            CTD.serial_Number = "KSXXXXXX000T";
            CTD.machine = "";
            CTD.toolingNo = "";
            CTD.lotNo = "";
            CTD.kpsn = "";
            CTD.workOrder = "";
            CTD.cavity = "";
            CTD.testData = "";
            CTD.results = "";
            CTD.collectType = AudioSystem.AudioMachineMessage.MES.collectType;

            string jsonData = JsonConvert.SerializeObject(CTD, Formatting.Indented);
            this.txt_MesCreate.Text = jsonData;
        }

        private static string ParsToString(Hashtable Pars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("$");
                }
                //sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));
                sb.Append(k.ToString() + "=" + Pars[k].ToString());
            }
            return sb.ToString();
        }

        #endregion
    }


}
