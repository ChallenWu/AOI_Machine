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
using OpenCvSharp.XImgProc;
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
            this.StartPosition = FormStartPosition.CenterScreen;
            InitDgv();
            

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

        #region Inovance PLC
        private void btnConnectPLC_Click(object sender, EventArgs e)
        {
            if (!ModbusApiH5U.IsConnected)
            {
                //PublicPLCData.Instance._ModbusApiH5U.PLCConnect("192.168.250.11", 1);
                //var ip = Globals.SettingICT.PLC_IP;
                //var kq = PublicPLCData.Instance.ModbusApiH5U.PLCConnect(ip, 1);
                //if (kq)
                //{

                //}
            }
        }
        private void btnDisconnectPLC_Click(object sender, EventArgs e)
        {
            PublicPLCData.Instance.ModbusApiH5U.DisConnect(1);
        }
        private void btnWritePLC_Click(object sender, EventArgs e)
        {
            //short value = short.Parse(textBox4.Text);
            //ModbusApiH5U.WriteValueInt16(SoftElemType.REGI_H5U_D, PublicPLCData.triggerRegister_D, value);
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
            am.NowAlarm.ErrorCategory = "Safety";
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
            ProductInformation UM = new ProductInformation() { StartTime = DateTime.Now, ModelProduct = LoadModel.appSettings.currentModel, serialNumber1 = "AX001", serialNumber2 = "AX002", serialNumber3 = "AX003",
                                                                EndTime = DateTime.Now.AddMinutes(-1), CT = 60.0, HiveState = 1, UnitSN = "Test00001", ComponentSN = "tttttjfsb", Shift = "NS", Pass = "PASS" };
            ProductMessage ucm = new ProductMessage();
            ucm.Unit = UM;
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
        public void UpdateUIText(string text)
        {
            if (txtLog.InvokeRequired)
            {
                // Sử dụng Invoke để thực hiện cập nhật trên UI thread
                txtLog.Invoke(new Action(() => txtLog.Text += text));
            }
            else
            {                
                if (!string.IsNullOrEmpty(text))
                    txtLog.Text += text;
                txtLog.Update();
                txtLog.Refresh();
            }
        }

        public void Update_TCP(string text)
        {
            if(textBox1.InvokeRequired)
            {
                textBox1.Invoke(new Action(() => textBox1.Text += text + "\r\n"));
            }
            else
            { 
                if (!string.IsNullOrEmpty(text))
                {
                    textBox1.Text += text + "\r\n";
                }
            }
                  
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
                SLMP.Instance.WriteWord(DevideCode.D, 5000, 100);
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
                SLMP.Instance.WriteWord(DevideCode.D, 5000, 1000);
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
            ACD.empNo = AudioSystem.InforMachine.MES.empNo;
            ACD.terminalName = AudioSystem.InforMachine.MES.terminalName;
            ACD.serial_Number = "TEBRCP2087000T";
            ACD.machine = "";
            ACD.toolingNo = "";
            ACD.lotNo = "";
            ACD.kpsn = "";
            ACD.reelNo = "";
            ACD.workOrder = AudioSystem.InforMachine.MES.workOrder;
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
            AGD.empNo = AudioSystem.InforMachine.MES.empNo;
            AGD.terminalName = AudioSystem.InforMachine.MES.terminalName;
            AGD.serial_Number = "TEBRCP2087000T";
            AGD.machine = "";
            AGD.toolingNo = "";
            AGD.lotNo = "";
            AGD.kpsn = "";
            AGD.reelNo = "";
            AGD.workOrder = AudioSystem.InforMachine.MES.workOrder;

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
            GCRD.empNo = AudioSystem.InforMachine.MES.empNo;
            GCRD.terminalName = AudioSystem.InforMachine.MES.terminalName;
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
            pars.Add("software_version", AudioSystem.InforMachine.SW_version);
            pars.Add("station_id", AudioSystem.InforMachine.MES.terminalName);
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
            CTD.empNo = AudioSystem.InforMachine.MES.empNo;
            CTD.terminalName = AudioSystem.InforMachine.MES.terminalName;
            CTD.serial_Number = "KSXXXXXX000T";
            CTD.machine = "";
            CTD.toolingNo = "";
            CTD.lotNo = "";
            CTD.kpsn = "";
            CTD.workOrder = "";
            CTD.cavity = "";
            CTD.testData = "";
            CTD.results = "";
            CTD.collectType = AudioSystem.InforMachine.MES.collectType;

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

        #region PLC Mitsu & Position Model

        private string[] dgvModelHeader = new string[3] { "No", "Model", "Comment" };
        private int[] ColumnsWidth = new int[3] { 22, 22, 50 };
        private void InitDgv()
        {

            try
            {
                //Update model
                dgvModel.AllowUserToAddRows = false;
                dgvModel.AllowUserToDeleteRows = false;
                dgvModel.AllowUserToOrderColumns = false;
                dgvModel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvModel.RowHeadersVisible = false;
                this.tbCurrentModel.Text = LoadModel.appSettings.currentModel;
                this.tbSelectModel.Text = "";

                this.dgvModel.DataSource = null;
                this.dgvModel.DataSource = ModelStore.GetModelInfoList();
                dgvModel.Refresh();

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
                X = Convert.ToInt32(firstRow.Cells["x"].Value ?? 0);
                Y= Convert.ToInt32(firstRow.Cells["y"].Value ?? 0);
                Z = Convert.ToInt32(firstRow.Cells["z"].Value ?? 0);
                R = Convert.ToInt32(firstRow.Cells["r"].Value ?? 0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnSaveModel_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show($"Bạn muốn tạo thêm model {tbModelName.Text} từ {tbSelectModel.Text} không", "Create",MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if(this.tbModelName.Text == "")
                    {
                        MessageBox.Show("Tên model không được để trống");
                        return;
                    }    
                    // Check if the new Model name existing:
                    if (ModelStore.GetModelSettings(this.tbModelName.Text) != null)
                    {
                        MessageBox.Show("The new Package name already existed! Please choose another name!");
                        return;
                    }

                    // Save:
                    //var model = LoadModel.currentModel.Clone();
                    ModelSettings model = new ModelSettings();
                    model.modelName = this.tbModelName.Text;
                    model.updateTime = DateTime.Now;
                    for (int i = 0; i < 5; i++)
                    {
                        model.points.Add(new PLC_Point
                        {
                            Name = $"Point{i}"
                        });
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
        private void LoadDataToDataPoint(ModelSettings Name)
        {            
            dgvPositions.DataSource = Name.points;
        }

        private void LoadDataToDataModel(List<ModelSettings> models)
        {
            dgvModel.Rows.Clear();
            for (int i = 0; i < models.Count; i++)
            {
                {
                    dgvModel.Rows.Add(i, models[i].modelName, models[i].updateTime);
                }
            }
        }
        #endregion

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
                        Globals.SetTopState();
                        Globals.CreateModelFolder();
                        //Thread.Sleep(1000);
                        Popup popup = new Popup("Uploading...", 1500);
                        popup.ShowDialog();
                        PageSetting.Instance.UpDateCompensationSettings();
                        popup = new Popup("Finish loading new model", 500);
                        popup.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        string SlectedPoint;
        //Tọa độ
        int X, Y, Z, R;
        //Thanh ghi 
        int X_Positions = 5000, Y_Positions = 5002, Z_Positions = 5004, R_Positions = 5006, Run = 5000;

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
            string cmd = "start";
            Scanner_TCP.Instance.WriteCmd(cmd, 10);
            if (Scanner_TCP.Instance.are_DataRecerveDone.WaitOne(3000) == false)
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("相机数据反馈超时"), MultiLanguage.GetMessage("错误"), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            textBox1.Text = Scanner_TCP.Instance.RecData;
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

        private void button6_Click(object sender, EventArgs e)
        {
            SLMP.Instance.WriteString(DevideCode.D, 5000, "abcd");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var recData = "";
            SLMP.Instance.ReadString(DevideCode.D,5000,4,out recData);
            if (recData != "") 
            {
                MessageBox.Show(recData);                
            }
            
        }

        private void btnDownloadPoint_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn tải dữ liệu điểm xuống PLC","Question",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
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
            var pointname = "Point" + LoadModel.currentModel.points.Count;

            foreach (var x in LoadModel.currentModel.points)
            {
                if (x.Name.Equals(pointname))
                {
                    MessageBox.Show("Trùng tên với 1 điểm đã cho. Xin hãy tạo lại điểm khác");
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
            MessageBox.Show($"Bạn có muốn xóa điểm {SlectedPoint} không ");
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
            if (MessageBox.Show($"Bạn có ghi tọa độ hiện tại vào {SlectedPoint}", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
                //SLMP.Instance.ReadDoubleWord(DevideCode.D, X_Positions, X);
                //SLMP.Instance.ReadDoubleWord(DevideCode.D, Y_Positions, Y);
                //SLMP.Instance.ReadDoubleWord(DevideCode.D, Z_Positions, Z);
                //SLMP.Instance.ReadDoubleWord(DevideCode.D, R_Positions, R);
                Thread.Sleep(50);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //axisControl1.stopPlcThread = SLMP.Instance.IsConnect;
        }

        private void btnTCPConnect_Click(object sender, EventArgs e)
        {
            Scanner_TCP.Instance.Connect();
        }

        /// <summary>
        /// Chạy điểm bằng chế độ manual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // Hiển thị thông tin của hàng được chọn (hoặc xử lý khác)
            if(MessageBox.Show($"Bạn có muốn chạy đến điểm {SlectedPoint}","Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //MessageBox.Show($"Selected Row:\nName: {name}, x: {X}, y: {Y}, z: {Z}, r: {R}");
                SLMP.Instance.WriteDoubleWord(DevideCode.D, X_Positions, X);
                SLMP.Instance.WriteDoubleWord(DevideCode.D, Y_Positions, Y);
                SLMP.Instance.WriteDoubleWord(DevideCode.D, Z_Positions, Z);
                SLMP.Instance.WriteDoubleWord(DevideCode.D, R_Positions, R); 
                SLMP.Instance.WriteBit(DevideCode.M, Run, true);
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
                x =0, 
                y = 0,
                z = 0,
                r = 0
            };

            // Thêm đối tượng vào danh sách points
            LoadModel.currentModel.points.Add(newPoint);
            InitDgv();
        }
        
        private void dgvPositions_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra chỉ số hàng để đảm bảo không phải tiêu đề hoặc hàng trống cuối cùng
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                // Lấy hàng mà người dùng click vào
                DataGridViewRow selectedRow = dgvPositions.Rows[e.RowIndex];
                // Lấy giá trị từ các ô trong hàng đó
                SlectedPoint = selectedRow.Cells["Name"].Value?.ToString() ?? string.Empty;
                X = Convert.ToInt32(selectedRow.Cells["x"].Value ?? 0);
                Y = Convert.ToInt32(selectedRow.Cells["y"].Value ?? 0);
                Z = Convert.ToInt32(selectedRow.Cells["z"].Value ?? 0);
                R = Convert.ToInt32(selectedRow.Cells["r"].Value ?? 0);
            }
        }

        private bool CheckMachineRunSts()
        {
            if (HBMachine.Instance.IsMachineRunning())
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("当前有任务正在运行", "\n", "请停止设备运行后再修改参数"),
                    MultiLanguage.GetMessage("警告"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            return false;
        }
    }
}














