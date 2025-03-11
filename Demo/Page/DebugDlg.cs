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
using Models;
using HB_IWatch;
using Demo.UserControls;
using Demo.Setting;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using OpenCvSharp;
using VisionTools;
using VisionTools.Tools;
using VisionTools.Tools.ImageFile;
using VisionTools.Tools.TotalGraphic;
using Sunny.UI;
using VM_Pro;
namespace Demo.Page
{
    public partial class DebugDlg : UIForm
    {
        private ConcurrentQueue<byte> rxQueue = new ConcurrentQueue<byte>();
        private AutoResetEvent rxSignal = new AutoResetEvent(false);
        SerialPort serialPort = new SerialPort();
        string DataIn;
        System.DateTime dt = System.DateTime.Now;
        bool keepReading = false;
        Thread thread = null;

        private int Address;
        private DevideCode plcDeviceCode;
        public DebugDlg()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            if (SLMP.Instance.IsConnect)
            {
                btn_Connect.BackColor = Color.Green;
            }
            else
            {
                btn_Connect.BackColor = Color.Red;
            }
            tabControl1.TabPages.Remove(tabPLCDevice);
            lbSpeedX.Text = trackBar0.Value.ToString() + "mm/s";
            lbSpeedY.Text = trackBar1.Value.ToString() + "mm/s";
            lbSpeedZ.Text = trackBar2.Value.ToString() + "mm/s";
            lbSpeedR.Text = trackBar3.Value.ToString() + "mm/s";
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

        #region Alarm
        AlarmMessage am;
        private void btnAlarmIn_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime after = now.AddMinutes(1);
            am = new AlarmMessage() { HappenTime = now, EffectiveHappenTime = now, EndTime = after, Duration = 5.8, DurationCatigory = 5, AlarmSerialNumber = DateTime.Now.ToString("yyyyMMddHHmmssfff") };
            am.NowAlarm.ErrorCode = "Alarm0001";
            am.NowAlarm.ErrorCategory = "Safety";
            am.NowAlarm.MessageEn = "SafetyAlaem";
            am.NowAlarm.MessageCn = "Security door alarm";
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
            ProductInformation UM = new ProductInformation() { StartTime = DateTime.Now, ModelProduct = LoadModel.appSettings.currentModel, 
                                                                serialNumber1 = "AX001", serialNumber2 = "AX002", serialNumber3 = "AX003",
                                                                EndTime = DateTime.Now.AddMinutes(-1), CT = 60.0, HiveState = 1, 
                                                                UnitSN = "Test00001", ComponentSN = "tttttjfsb", Shift = "NS", Pass = "PASS" };
            ProductMessage ucm = new ProductMessage();
            ucm.Unit = UM;
            DataServerManager.Instance.InsertUnitMessage(ucm, 0);
        }

        private void UnitOut_8H_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now.AddHours(1);
            DateTime before = now.AddHours(-9);

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
            //this.dataGridView1.DataSource = DataServerManager.Instance.SelectAlarmAll();
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
            Globals.OpenDebugForm = false;
            thread.Abort();
        }
        private void DebugDlg_Load(object sender, EventArgs e)
        {
            Globals.OpenDebugForm = true;

            if (UserAccountControl.currentAccount.UserPermission == Privilige.Administrator)
            {
                Edit_Vision.Instance.processCreatorUI1.Drag = true;
            }
            else
                Edit_Vision.Instance.processCreatorUI1.Drag = false;

            LoadViewEdit();

            InitDgv();

            thread = new Thread(() => {
            while (true)
            {
                SLMP.Instance.ReadDoubleWord(DevideCode.D, 1000, out int CurrentX);
                SetLabelTextCallback(label6, $"X: " + CurrentX.ToString() + " pulse", Color.Black);
                SLMP.Instance.ReadDoubleWord(DevideCode.D, 1002, out int CurrentY);
                SetLabelTextCallback(label22, $"Y: " + CurrentY.ToString() + " pulse", Color.Black);
                SLMP.Instance.ReadDoubleWord(DevideCode.D, 1004, out int CurrentZ);
                SetLabelTextCallback(label23, $"Z: " + CurrentZ.ToString() + " pulse", Color.Black);
                SLMP.Instance.ReadDoubleWord(DevideCode.D, 1006, out int CurrentR);
                SetLabelTextCallback(label24, $"R: " + CurrentR.ToString() + " pulse", Color.Black);
                Thread.Sleep(100);                                
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        private void LoadViewEdit()
        {           
            string VisionPath = $"D:\\AOI_Config\\Setting\\{LoadModel.currentModel.modelName}\\VisionTools";
            string[] vpjFiles = Directory.GetFiles(VisionPath, "*.vpj", SearchOption.AllDirectories);
            viewEditCCD1.cb_Tool.Items.Clear();

            foreach (string vpjFile in vpjFiles)
            {
                Edit_Vision.Instance.processCreatorUI1.OpenProcessFile(vpjFile);
                viewEditCCD1.cb_Tool.Items.Add(Edit_Vision.Instance.processCreatorUI1.ProcessDisplay.Process.Name);
                
            }
        

            if (viewEditCCD1.cb_Tool.Items.Count > 0)
            {
                viewEditCCD1.cb_Tool.SelectedIndex = 0;
            }
        }

        delegate void dSetLabelTextCallback(Label label, string msg, Color color);
        public void SetLabelTextCallback(Label label, string msg, Color color)
        {
            if (label.InvokeRequired)
            {
                dSetLabelTextCallback d = new dSetLabelTextCallback(SetLabelTextCallback);
                label.Invoke(d, new object[] { label, msg, color });
            }
            else
            {
                label.Text = msg;
                if (label.ForeColor != color)
                {
                    label.ForeColor = color;
                }
                //tb.ScrollToCaret();// 捲動到最後一個插入位置
            }
        }

        delegate void dSetTextBoxCallback(TextBox tbox, string msg);
        public void SetTextBoxCallback(TextBox tbox, string msg)
        {
            if (tbox.InvokeRequired)
            {
                dSetTextBoxCallback d = new dSetTextBoxCallback(SetTextBoxCallback);
                tbox.Invoke(d, new object[] { tbox, msg});
            }
            else
            {
                tbox.Text = msg + "\r\n";
            }
        }


        delegate void dSetDataSource(DataGridView dgv, DataTable dt);
        public void SetDataSource(DataGridView dgv, DataTable dt)
        {
            if (dgv.InvokeRequired)
            {
                dSetDataSource d = new dSetDataSource(SetDataSource);
                dgv.Invoke(d, new object[] { dgv, dt });
            }
            else
            {
                dgv.DataSource = dt;
            }
        }

        //TextLog TCP Scanner
        public void Update_TCP(string text)
        {
            SetTextBoxCallback(textBox1, text);

            //if(textBox1.InvokeRequired)
            //{
            //    textBox1.Invoke(new Action(() => textBox1.Text += text + "\r\n"));
            //}
            //else
            //{ 
            //    if (!string.IsNullOrEmpty(text))
            //    {
            //        textBox1.Text += text + "\r\n";
            //    }
            //}
        }

        #region Manual Read/Write PLC Mitsubishi
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                Thread threadPlc = new Thread(() =>
                {
                    if(SLMP.Instance.IsConnect == false)
                    {
                        if (SLMP.Instance.Open(tb_Plc.Text, int.Parse(tb_PlcPort.Text)) == 0)
                        {
                            MessageBox.Show("Kết nối tới plc thành công");
                            invokeTextBox("Connect", Color.Green);
                        }
                        else
                        {
                            MessageBox.Show("Kết nối thất bại, xin hãy kiểm tra lại IP của plc và dây dẫn mạng");
                        }
                    }    
                    else
                    {
                        var result = SLMP.Instance.Close();
                        if(result == 0)
                        {
                            MessageBox.Show("Đã ngắt kết nối với Plc");

                            invokeTextBox("Disconnect", Color.Red);
                        }    
                    }
                });
                threadPlc.IsBackground  = true;
                threadPlc.Start();  
            }
            catch (Exception ex)
            {
               
            }                
        }
        private void invokeTextBox(string status, Color colorButton)
        {
            btn_Connect.Invoke(new Action(() =>
            {
                btn_Connect.Text = status;
                btn_Connect.BackColor = colorButton;
            }));
        }
        private void SortTypeData(string str)
        {            
            Address = 0;
            Address = Convert.ToInt16(str.Substring(1).Trim());
            var RegisterType = str.Substring(0, 1).Trim().ToUpper();            
            if (RegisterType == "D")
            {
                plcDeviceCode = DevideCode.D;
            }
            else if (RegisterType == "M")
            {
                plcDeviceCode = DevideCode.M;
            }
            else if(RegisterType == "Y")
            {
                plcDeviceCode = DevideCode.Y;
            }
            else if (RegisterType == "X")
            {
                plcDeviceCode = DevideCode.X;
            }

        }
        private void btnReadBitPlc_Click(object sender, EventArgs e)
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                SortTypeData(textBox_Adr_1.Text);
                var value = false;
                SLMP.Instance.ReadBit(plcDeviceCode, Address, out value);
                if (value)
                {
                    textBox_Value_1.Text = "ON";
                }
                else
                {
                    textBox_Value_1.Text = "OFF";
                }
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var value = 0;
                SLMP.Instance.ReadDoubleWord(DevideCode.D, 5000, out value);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
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
            if (MES.HttpPost(sendData, ref back, PT))
            {
                this.txt_Mes.Text += "\r\nMes-->PC: " + back;
            }
            else
                this.txt_Mes.Text += "\r\nMes-->PC: Error " + back;
        }

        private void btn_checkLoggin_Click(object sender, EventArgs e)
        {
            PT = PostType.AssyCheck;
            this.lbl_empNo.BackColor = Color.LightGreen;
            this.lbl_terminalName.BackColor = Color.LightGreen;
            this.lbl_SerialNumber.BackColor = Color.LightGreen;

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
            ACD.empNo = SystemVariable.InforMachine.MES.empNo;
            ACD.terminalName = SystemVariable.InforMachine.MES.terminalName;
            ACD.serial_Number = "TEBRCP2087000T";
            ACD.machine = "";
            ACD.toolingNo = "";
            ACD.lotNo = "";
            ACD.kpsn = "";
            ACD.reelNo = "";
            ACD.workOrder = SystemVariable.InforMachine.MES.workOrder;
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
            AGD.empNo = SystemVariable.InforMachine.MES.empNo;
            AGD.terminalName = SystemVariable.InforMachine.MES.terminalName;
            AGD.serial_Number = "TEBRCP2087000T";
            AGD.machine = "";
            AGD.toolingNo = "";
            AGD.lotNo = "";
            AGD.kpsn = "";
            AGD.reelNo = "";
            AGD.workOrder = SystemVariable.InforMachine.MES.workOrder;

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
            GCRD.empNo = SystemVariable.InforMachine.MES.empNo;
            GCRD.terminalName = SystemVariable.InforMachine.MES.terminalName;
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
            pars.Add("software_version", SystemVariable.InforMachine.SW_version);
            pars.Add("station_id", SystemVariable.InforMachine.MES.terminalName);
            pars.Add("fixture_id", "AOI");
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
            CTD.empNo = SystemVariable.InforMachine.MES.empNo;
            CTD.terminalName = SystemVariable.InforMachine.MES.terminalName;
            CTD.serial_Number = "KSXXXXXX000T";
            CTD.machine = "";
            CTD.toolingNo = "";
            CTD.lotNo = "";
            CTD.kpsn = "";
            CTD.workOrder = "";
            CTD.cavity = "";
            CTD.testData = "";
            CTD.results = "";
            CTD.collectType = SystemVariable.InforMachine.MES.collectType;

            string jsonData = JsonConvert.SerializeObject(CTD, Formatting.Indented);
            this.txt_MesCreate.Text = jsonData;
        }

        private void btnGetStation_Click(object sender, EventArgs e)
        {
            PT = PostType.AssyCheck;
            this.lbl_empNo.BackColor = Color.LightGreen;
            this.lbl_terminalName.BackColor = Color.LightGreen;
            this.lbl_SerialNumber.BackColor = Color.LightGreen;

            EquipmentStage EqmState = new EquipmentStage();
            Dictionary<string, string> tmp = new Dictionary<string, string> { };
            tmp.Add("user", "0797039");
            tmp.Add("pwd", "123456a");
            EqmState.SerializeData = "{\"user\":\"0791347\",\"pwd\":\"0791347X\"}";
            EqmState.cmd = 2;
            EqmState.machine = "AOIMachine";  
            EqmState.lstSerialNumber = tmp;
            string jsonData = JsonConvert.SerializeObject(EqmState, Formatting.Indented);
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
                sb.Append(k.ToString() + "=" + Pars[k].ToString());
            }
            return sb.ToString();
        }

        #endregion

        #region Position Model
        private void InitDgv()
        {
            try
            {
                tbCurrentModel.Text = LoadModel.appSettings.currentModel;
                tbSelectModel.Text = "";
                //Update model
                this.dgvModel.AllowUserToAddRows = false;
                this.dgvModel.AllowUserToDeleteRows = false;
                this.dgvModel.AllowUserToOrderColumns = false;
                this.dgvModel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                this.dgvModel.RowHeadersVisible = false;
                this.dgvModel.DataSource = null;
                this.dgvModel.DataSource = ModelStore.GetModelInfoList();
                this.dgvModel.Refresh();


                //Update Positions
                dgvPositions.AllowUserToAddRows = false;
                dgvPositions.AllowUserToDeleteRows = false;
                dgvPositions.AllowUserToOrderColumns = false;
                dgvPositions.MultiSelect = false;
                dgvPositions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvPositions.RowHeadersVisible = false;
                dgvPositions.DataSource = null;
                dgvPositions.DataSource = LoadModel.currentModel.points;
                dgvPositions.MultiSelect = false;
                dgvPositions.Refresh();
                dgvPositions.Rows[0].Selected = true;

                DataGridViewRow firstRow = dgvPositions.Rows[0]; // Lấy hàng đầu tiên
                // Lấy dữ liệu từ các cột của hàng đầu tiên
                SlectedPoint = firstRow.Cells["Name"].Value?.ToString() ?? string.Empty;
                X_Axis = Convert.ToInt32(firstRow.Cells["x"].Value ?? 0);
                Y = Convert.ToInt32(firstRow.Cells["y"].Value ?? 0);
                Z = Convert.ToInt32(firstRow.Cells["z"].Value ?? 0);
                R = Convert.ToInt32(firstRow.Cells["r"].Value ?? 0);
                toolStripLabel1.Text = LoadModel.currentModel.modelName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Add items to list Tool
        /// </summary>

        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show($"Do you want create new Model: {tbModelName.Text} from {tbSelectModel.Text}?", "Create Model",MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if(this.tbModelName.Text == "")
                    {
                        MessageBox.Show("Model name cant empty");
                        return;
                    }    
                    // Check if the new Model name existing:
                    if (ModelStore.GetModelSettings(this.tbModelName.Text) != null)
                    {
                        MessageBox.Show("The new Package name already existed! Please choose another name!");
                        return;
                    }

                    var url = "D:\\AOI_Config\\Setting\\" + this.tbModelName.Text;
                    if (!Directory.Exists(url))
                    {
                        Directory.CreateDirectory(url);
                    }


                    // Save:
                    var model = LoadModel.currentModel.Clone();
                    //ModelSettings model = new ModelSettings();
                    model.modelName = this.tbModelName.Text;
                    model.updateTime = DateTime.Now;

                    if(LoadModel.currentModel.points == null)
                    {
                        model.points = LoadModel.currentModel.points;
                        if (model.points.Count == 0)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                model.points.Add(new PLC_Point
                                {
                                    Name = $"Point{i}"
                                });
                            }
                        }
                    }

                    ModelStore.UpdateModelSettings(model);
                    // Reload models:
                    this.InitDgv();
                    //Globals.CreateModelFolder();
                }   

            }
            catch (Exception ex)
            {
                
            }
        }

        private void dgvModel_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                string modelName = dgvModel.CurrentRow.Cells[1].Value.ToString();
                this.tbSelectModel.Text = modelName;
            }
            catch
            {

            }
        }

        private void btnLoadModel_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Are you sure to load new Model?", "Note",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var loadedModel = ModelStore.GetModelSettings(this.tbSelectModel.Text);
                    if (loadedModel != null)
                    {
                        LoadModel.ReplaceModel(loadedModel);
                        InitDgv();
                        //Globals.SetTopState();
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        Globals.CreateModelFolder();
                        Console.WriteLine("Create model comsume time:{0}",sw.ElapsedMilliseconds);
                        PageSetting.Instance.UpDateCompensationSettings();
                        Console.WriteLine("UpDateCompensationSettings model comsume time:{0}", sw.ElapsedMilliseconds);
                        //PageVision.Instance.VisionModel();
                        Globals.LoadVisionTools();
                        Console.WriteLine("Create model comsume time:{0}", sw.ElapsedMilliseconds);
                        LoadViewEdit();
                        Console.WriteLine("Load model comsume time:{0}", sw.ElapsedMilliseconds);
                        sw.Stop();
                        MessageBox.Show("Load Model success");
                    }
                    else
                    {
                        MessageBox.Show("Load Model fail");
                    }    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load Model fail");
            }
        }

        string SlectedPoint;
        //Tọa độ
        int X_Axis, Y, Z, R;
        //Thanh ghi 
        int X_Positions = 5000, Y_Positions = 5002, Z_Positions = 5004, R_Positions = 5006, RunPoint = 5000;

        private void btnDeleteModel_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirm:
                if (MessageBox.Show("Are you sure to delete the selected Package?", "Note",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                {
                    return;
                }

                // Delete:
                var model = this.tbSelectModel.Text;
                if (this.tbCurrentModel.Text.Equals(model))
                {
                    MessageBox.Show("You cannot delete current model!");
                    return;
                }
                //XModels.Instance.RemoveModel(model);
                string folderPath = $"D:\\AOI_Config\\Setting\\{model}";
                Directory.Delete(folderPath, true);
                ModelStore.DeleteModel(model);
                // Reload models:
                InitDgv();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnSendCmd_Click(object sender, EventArgs e)
        {
            //byte[] cmd = { 0x02, 0xF4, 0x03 };
            string cmd = "TRIGGER";
            Thread TrggierClick = new Thread(new ThreadStart(() =>
            {
                Scanner_TCP.Instance.WriteCmd(cmd, 10);
                if (Scanner_TCP.Instance.are_DataRecerveDone.WaitOne(3000) == false)
                {
                    BzMessagebox.Show(MultiLanguage.GetMessage("Receive timeout"), MultiLanguage.GetMessage("Error"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return;
                }
                //SetTextBoxCallback(textBox1, Scanner_TCP.Instance.RecData);
                //textBox1.Text = Scanner_TCP.Instance.RecData;
            }
            ));
            TrggierClick.IsBackground = true;
            TrggierClick.Start();
        }

        private void btnPassStation_Click(object sender, EventArgs e)
        {
            this.lbl_test.BackColor = Color.LightGreen;
            this.lbl_value.BackColor = Color.LightGreen;
            Hashtable pars = new Hashtable();
            Dictionary<string, string> productSN = new Dictionary<string, string>();
            productSN.Add("Serial1", "45451ewq");
            productSN.Add("Serial2", "45451ewq");
            productSN.Add("Serial3", "45451ewq");
            string jsonData = JsonConvert.SerializeObject(productSN, Formatting.Indented);
            this.txt_MesCreate.Text = jsonData;
            //string postData = ParsToString(productSN);
            this.txt_Test.Text = jsonData;
        }

        private void btnDownloadPoint_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want download model data point to PLC","Question",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                foreach (var i in LoadModel.currentModel.points)
                {
                    SLMP.Instance.WriteDoubleWord(DevideCode.D, i.X_Reg, i.x);
                    SLMP.Instance.WriteDoubleWord(DevideCode.D, i.Y_Reg, i.y);
                    SLMP.Instance.WriteDoubleWord(DevideCode.D, i.R_Reg, i.r);
                    SLMP.Instance.WriteDoubleWord(DevideCode.D, i.Z_Reg, i.z);
                }
            }    
        }

        private void btnAddPoints_Click(object sender, EventArgs e)
        {
            //Thêm điểm vào trong listPoint
            var pointname = "Point_" + LoadModel.currentModel.points.Count;

            foreach (var x in LoadModel.currentModel.points)
            {
                if (x.Name.Equals(pointname))
                {
                    MessageBox.Show("The name point is same as the created point's name. Please set another name.");
                    return;
                }
            }


            PLC_Point newPoint = new PLC_Point
            {
                Name = pointname,
            };

            // Thêm đối tượng vào danh sách points
            LoadModel.currentModel.points.Add(newPoint);
            InitDgv();
        }

        private void btnDeletePoints_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure delete the Point {SlectedPoint}?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            
            var newList = new List<PLC_Point>();
            for (int i = 0; i < LoadModel.currentModel.points.Count; i++)
            {
                if (!LoadModel.currentModel.points[i].Name.Equals(SlectedPoint))
                {
                    newList.Add(LoadModel.currentModel.points[i]);
                }
            }
            //Cập nhật lại listPoint tạm thời
            LoadModel.currentModel.points = newList;
            InitDgv();
        }

        private void btnReadCurrentPoint_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Write currrent position to {SlectedPoint}", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(SLMP.Instance.IsConnect)
                {
                    var value = 0;
                    SLMP.Instance.ReadDoubleWord(DevideCode.D, 1000, out value);
                    dgvPositions.Rows[indexRowDgv].Cells["x"].Value = value;
                    SLMP.Instance.ReadDoubleWord(DevideCode.D, 1002, out value);
                    dgvPositions.Rows[indexRowDgv].Cells["y"].Value = value;
                    SLMP.Instance.ReadDoubleWord(DevideCode.D, 1004, out value);
                    dgvPositions.Rows[indexRowDgv].Cells["z"].Value = value;
                    SLMP.Instance.ReadDoubleWord(DevideCode.D, 1006, out value);
                    dgvPositions.Rows[indexRowDgv].Cells["r"].Value = value;
                    Thread.Sleep(50);
                }
                else
                {
                    BzMessagebox.Show("You arent connect to PLC. Please connect first.");
                }
            }
        }

        #endregion

        #region Scanner
        private void btnTCPConnect_Click(object sender, EventArgs e)
        {
            //if(!Scanner_TCP.Instance.Connected)
            //{
            //    Scanner_TCP.Instance.Connect();
            //    btnTCPConnect.BackColor = Color.Green;
            //}     
        }

        private void btnClearTcp_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        #endregion

        #region Control Source light
        bool isLight1 = false;
        private void button2_Click_1(object sender, EventArgs e)
        {
            if(!isLight1)
            {
               var cmd = $"IDC,OPPS,1, {tb_Light1_Sensitive.Text},\r\n";
               //var cmdON = "IDC,OPS,1,\r\n";
                Mid_Light_TCP.Instance.WriteCmd(cmd, 0);
            }
            else
            {
                Mid_Light_TCP.Instance.WriteCmd("IDC,CLS,1,\r\n", 0);
            }
            isLight1 = !isLight1;
           
        }
        bool isLight2 = false;
        private void button5_Click(object sender, EventArgs e)
        {
            if (!isLight2)
            {
                Mid_Light_TCP.Instance.WriteCmd($"IDC,OPPS,2,{tb_Light2_Sensitive.Text},\r\n", 0);
            }
            else
            {
                Mid_Light_TCP.Instance.WriteCmd("IDC,CLS,2,\r\n", 0);
            }
            isLight2 = !isLight2;
        }
        bool isMiddleLight = false;
        private void button6_Click(object sender, EventArgs e)
        {
            if (!isMiddleLight)
            {
                LR_Light.Instance.LightOn(tb_Light3_Sensitive.Text);
            }
            else
            {
                LR_Light.Instance.LightOff();
            }
            isMiddleLight = !isMiddleLight;
        }

        #endregion

        #region CCD Vision
        Thread _thread;
        bool IsLive = false;
        private void btnLiveView_Click(object sender, EventArgs e)
        {
            if (!IsLive)
            {
                if (_thread != null)
                {
                    _thread.Abort();
                }
                viewEditCCD1.displayViewInteract1.NotifyDrawing.Clear();
                HikCam cam = Globals.ListCams.Find(x => x.CameraName == Globals.SettingParameter.CCD_Name);
                _thread = new Thread(() => CaptureImage(cam));
                _thread.IsBackground = true;
                _thread.Start();
                IsLive = true;
                btnLiveView.Text = "Accquiring";

            }
            else
            {
                _thread.Abort();
                IsLive = false;
                btnLiveView.Text = "Accquiring";
            }
            
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            viewEditCCD1.displayViewInteract1.NotifyDrawing.Clear();
            HikCam cam = Globals.ListCams.Find(x => x.CameraName == Globals.SettingParameter.CCD_Name);
            if (cam != null && cam.isConnected)
            {
                Bitmap bitmap = cam.CaptureImage();
                Mat mat = bitmap.ToMat();
                viewEditCCD1.displayViewInteract1.Image = new VisionTools.Image(mat);
                viewEditCCD1.displayViewInteract1.FitToWindow();
                bitmap.Dispose();
            }
        }
        private void CaptureImage(HikCam cam)
        {
            while (true)
            {
                if (cam != null && cam.isConnected)
                {
                    Bitmap bitmap = cam.CaptureImage();
                    Mat mat = bitmap?.ToMat();
                    viewEditCCD1.displayViewInteract1.Image = new VisionTools.Image(mat);
                    bitmap.Dispose();
                }
                GC.Collect();
                Thread.Sleep(200);
            }

        }       
        #endregion

        #region Control Axis

        //Truc X
        private void bt_JOG_NX_MouseDown(object sender, MouseEventArgs e)
        {
            if (trackBar0.Value <= 0)
                return;
            SLMP.Instance.WriteDoubleWord(DevideCode.D, 1700, trackBar0.Value);
            SLMP.Instance.WriteBit(DevideCode.L, 2100, true);
        }

        private void bt_JOG_NX_MouseUp(object sender, MouseEventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.L, 2100, false);
        }

        private void bt_JOG_PX_MouseDown(object sender, MouseEventArgs e)
        {
            if (trackBar0.Value <= 0)
                return;
            SLMP.Instance.WriteDoubleWord(DevideCode.D, 1700, trackBar0.Value);
            SLMP.Instance.WriteBit(DevideCode.L, 2120, true);
        }

        private void bt_JOG_PX_MouseUp(object sender, MouseEventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.L, 2120, false);
        }

        //Truc Y
        private void button9_MouseDown(object sender, MouseEventArgs e)
        {
            if (trackBar1.Value <= 0)
                return;
            SLMP.Instance.WriteDoubleWord(DevideCode.D, 1702, trackBar1.Value);
            SLMP.Instance.WriteBit(DevideCode.L, 2101, true);
        }

        private void button9_MouseUp(object sender, MouseEventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.L, 2101, false);
        }

        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
            if (trackBar1.Value <= 0)
                return;
            SLMP.Instance.WriteDoubleWord(DevideCode.D, 1702, trackBar1.Value);
            SLMP.Instance.WriteBit(DevideCode.L, 2121, true);
        }

        private void button10_MouseUp(object sender, MouseEventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.L, 2121, false);

        }

        //Truc Z
        private void button11_MouseDown(object sender, MouseEventArgs e)
        {
            if (trackBar2.Value <= 0)
                return;
            SLMP.Instance.WriteDoubleWord(DevideCode.D, 1704, trackBar2.Value);
            SLMP.Instance.WriteBit(DevideCode.L, 2102, true);
        }

        private void button11_MouseUp(object sender, MouseEventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.L, 2102, false);
        }

        private void button12_MouseDown(object sender, MouseEventArgs e)
        {
            if (trackBar2.Value <= 0)
                return;
            SLMP.Instance.WriteDoubleWord(DevideCode.D, 1706, trackBar2.Value);
            SLMP.Instance.WriteBit(DevideCode.L, 2122, true);
        }

        private void button12_MouseUp(object sender, MouseEventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.L, 2122, false);
        }

        
        //Truc R
        private void button13_MouseDown(object sender, MouseEventArgs e)
        {
            if (trackBar3.Value <= 0)
                return;
            SLMP.Instance.WriteDoubleWord(DevideCode.D, 1706, trackBar3.Value);
            SLMP.Instance.WriteBit(DevideCode.L, 2103, true);
        }

        private void button13_MouseUp(object sender, MouseEventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.L, 2103, false);
        }

        private void button14_MouseDown(object sender, MouseEventArgs e)
        {
            if (trackBar3.Value <= 0)
                return;
            SLMP.Instance.WriteDoubleWord(DevideCode.D, 1706, trackBar3.Value);
            SLMP.Instance.WriteBit(DevideCode.L, 2123, true);
        }

        //Speed Jog
        private void button14_MouseUp(object sender, MouseEventArgs e)
        {
            SLMP.Instance.WriteBit(DevideCode.L, 2123, false);
        }

        private void trackBar0_ValueChanged(object sender, EventArgs e)
        {
            lbSpeedX.Text = trackBar0.Value.ToString() + "mm/s";
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            lbSpeedY.Text = trackBar1.Value.ToString() + "mm/s";
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            lbSpeedZ.Text = trackBar2.Value.ToString() + "mm/s";
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            lbSpeedR.Text = trackBar3.Value.ToString() + "mm/s";
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Read All Point Data from PLC", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (SLMP.Instance.IsConnect)
                {
                    for(int i = 0; i < dgvPositions.RowCount;i++)
                    {
                        var value = 0;
                        //int XAxis = ;
                        SLMP.Instance.ReadDoubleWord(DevideCode.D, Convert.ToInt16(dgvPositions.Rows[i].Cells["X_Reg"].Value), out value);
                        dgvPositions.Rows[i].Cells["x"].Value = value;
                        SLMP.Instance.ReadDoubleWord(DevideCode.D, Convert.ToInt16(dgvPositions.Rows[i].Cells["Y_Reg"].Value), out value);
                        dgvPositions.Rows[i].Cells["y"].Value = value;
                        SLMP.Instance.ReadDoubleWord(DevideCode.D, Convert.ToInt16(dgvPositions.Rows[i].Cells["Z_Reg"].Value), out value);
                        dgvPositions.Rows[i].Cells["z"].Value = value;
                        SLMP.Instance.ReadDoubleWord(DevideCode.D, Convert.ToInt16(dgvPositions.Rows[i].Cells["R_Reg"].Value), out value);
                        dgvPositions.Rows[i].Cells["r"].Value = value;
                        Thread.Sleep(20);
                    }

                }
                else
                {
                    BzMessagebox.Show("Not connected PLC. Please connect PLC first");
                }
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }


        #endregion

        /// <summary>
        /// Chạy điểm bằng chế độ manual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // Hiển thị thông tin của hàng được chọn (hoặc xử lý khác)
            if(MessageBox.Show($"Are you sure want run to {SlectedPoint} Pos","Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //MessageBox.Show($"Selected Row:\nName: {name}, x: {X}, y: {Y}, z: {Z}, r: {R}");
                SLMP.Instance.WriteDoubleWord(DevideCode.D, X_Positions, X_Axis);
                SLMP.Instance.WriteDoubleWord(DevideCode.D, Y_Positions, Y);
                SLMP.Instance.WriteDoubleWord(DevideCode.D, Z_Positions, Z);
                SLMP.Instance.WriteDoubleWord(DevideCode.D, R_Positions, R); 
                SLMP.Instance.WriteBit(DevideCode.M, RunPoint, true);
                Thread.Sleep(50);
            }    
            
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // Confirm:
            if (MessageBox.Show("Are you sure to saving (override) all changes of the current Package?", "Note",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Save:
                    LoadModel.currentModel.updateTime = DateTime.Now;
                    UpdatePointsFromDataGridView();
                    ModelStore.UpdateModelSettings(LoadModel.currentModel);
                }            
        }

        //Cập nhật giá trị mới vào 
        private void UpdatePointsFromDataGridView()
        {
            for (int i = 0; i < dgvPositions.Rows.Count; i++)
            {
                // Kiểm tra hàng có hợp lệ (không phải hàng trống cuối cùng)
                if (dgvPositions.Rows[i].IsNewRow)
                    continue;

                // Lấy đối tượng PLC_Point tương ứng từ danh sách points
                PLC_Point point = LoadModel.currentModel.points[i];

                // Cập nhật các giá trị từ DataGridView vào đối tượng PLC_Point
                point.Name = dgvPositions.Rows[i].Cells["Name"].Value?.ToString() ?? string.Empty;
                point.x = Convert.ToInt32(dgvPositions.Rows[i].Cells["x"].Value ?? 0);
                point.y = Convert.ToInt32(dgvPositions.Rows[i].Cells["y"].Value ?? 0);
                point.z = Convert.ToInt32(dgvPositions.Rows[i].Cells["z"].Value ?? 0);
                point.r = Convert.ToInt32(dgvPositions.Rows[i].Cells["r"].Value ?? 0);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PLC_Point newPoint = new PLC_Point
            {
                Name = "Point" + LoadModel.currentModel.points.Count,
                x = 0, 
                y = 0,
                z = 0,
                r = 0
            };

            // Thêm đối tượng vào danh sách points
            LoadModel.currentModel.points.Add(newPoint);
            InitDgv();
        }
        int indexRowDgv;
        private void dgvPositions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra chỉ số hàng để đảm bảo không phải tiêu đề hoặc hàng trống cuối cùng
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                // Lấy hàng mà người dùng click vào
                DataGridViewRow selectedRow = dgvPositions.Rows[e.RowIndex];
                indexRowDgv = e.RowIndex;
                // Lấy giá trị từ các ô trong hàng đó
                SlectedPoint = selectedRow.Cells["Name"].Value?.ToString() ?? string.Empty;
                X_Axis = Convert.ToInt32(selectedRow.Cells["x"].Value ?? 0);
                Y = Convert.ToInt32(selectedRow.Cells["y"].Value ?? 0);
                Z = Convert.ToInt32(selectedRow.Cells["z"].Value ?? 0);
                R = Convert.ToInt32(selectedRow.Cells["r"].Value ?? 0);
            }
        }
    }
}














