using Demo.UserControls;
using HB_IWatch;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using XCore;

namespace Demo.Page
{
    public enum ModeType
    {
        Production,
        Engineering,
        CPK,
        GRR
    }

    public partial class PageLogin : UserControlBase
    {
        private static PageLogin instance;
        public static PageLogin Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new PageLogin();
                return instance;
            }
        }

        private Dictionary<SwitchButton, ModeType> modeMap = new Dictionary<SwitchButton, ModeType>();
        private ModeType mode = 0;
        public Action<ModeType> ONShowPage;

        public PageLogin()
        {
            InitializeComponent();
            this.BackColor = Color.White;
            this.switchButton_Production.SetText("Production");
            this.switchButton_Production.SetColor(MyColor.Blue, MyColor.None);
            this.switchButton_Engineering.SetText("Engineering");
            this.switchButton_Engineering.SetColor(MyColor.Blue, MyColor.None);
            this.switchButton_CPKGRR.SetText("CPK");
            this.switchButton_CPKGRR.SetColor(MyColor.Blue, MyColor.None);
            this.switchButtonGRR.SetText("GRR");
            this.switchButtonGRR.SetColor(MyColor.Blue, MyColor.None);

            modeMap.Add(this.switchButton_Production, ModeType.Production);
            modeMap.Add(this.switchButton_Engineering, ModeType.Engineering);
            modeMap.Add(this.switchButton_CPKGRR, ModeType.CPK);
            //modeMap.Add(this.switchButtonGRR, ModeType.GRR);

            foreach (SwitchButton item in modeMap.Keys)
            {
                item.Trigger += item_Trigger;
            }
            ResetComboBox_User();
            NoneUserPrivilige();

            userAccountManager1.OnDeleteAccount += ResetComboBox_User;
        }

        private void item_Trigger(SwitchButton button)
        {

            foreach (SwitchButton item in modeMap.Keys)
            {
                if (button != item)
                {
                    item.STS = false;
                }
            }
            this.mode = modeMap[button];
            if (ONShowPage != null)
            {
                ONShowPage(this.mode);
            }
        }


        private void button_Login_Click(object sender, EventArgs e)
        {

            NoneUserPrivilige();

            switch (comboBox_User.Text)
            {
                case "Administrator":
                    AdministratorLoginCheck();
                    break;
                case "BOTECH":
                    BOTECHLoginCheck();
                    break;
                case "Operator":
                    OperatorLoginCheck();
                    break;
                default:
                    OthersLoginCheck();
                    break;
            }
            foreach (XSetting setting in XSettingManager.Instance.SettingMap.Values)
            {
                setting.UserAccountName = "_" + comboBox_User.Text; ;
            }
            this.textBox_Password.Text = "";
        }
        public event Action<string, string> UpdateUserResquest;
        private void AdministratorLoginCheck()
        {
            if (textBox_Password.Text == UserAccountControl.Administrator.PassWord)
            {
                errorProvider1.Clear();
                AdminUserPrivilige();
                UserAccountControl.currentAccount = UserAccountControl.Administrator;                
                RequestUpdateLabel(comboBox_User.SelectedItem.ToString(), "Admin Level");

            }
            else
                errorProvider1.SetError(textBox_Password, MultiLanguage.GetMessage("密码错误"));

        }

        private void BOTECHLoginCheck()
        {
            if (UserAccountControl.BOTECH.PassWord == textBox_Password.Text)
            {
                errorProvider1.Clear();
                EngineerPrivilige();
                UserAccountControl.currentAccount = UserAccountControl.BOTECH;
                UpdateUserResquest?.Invoke(comboBox_User.Text, "BOTECH");

                RequestUpdateLabel(comboBox_User.SelectedItem.ToString(), "BOTECH Level");
            }
            else
                errorProvider1.SetError(textBox_Password, MultiLanguage.GetMessage("密码错误"));
        }

        private void OperatorLoginCheck()
        {
            if (textBox_Password.Text == "")
            {
                errorProvider1.Clear();
                OpUserPrivilige();
                UserAccountControl.currentAccount = UserAccountControl.Operator;
                UpdateUserResquest?.Invoke(comboBox_User.Text, "Operator");

                RequestUpdateLabel(comboBox_User.SelectedItem.ToString(), "Operator Level");
            }
            else
                errorProvider1.SetError(textBox_Password, MultiLanguage.GetMessage("密码错误"));
        }

        private void OthersLoginCheck()
        {
            if (!UserAccountControl.AllUserAccounts.ContainsKey(comboBox_User.Text))
                errorProvider1.SetError(comboBox_User, MultiLanguage.GetMessage("用户不存在"));
            else
            {
                if (UserAccountControl.AllUserAccounts[comboBox_User.Text].PassWord == textBox_Password.Text)
                {
                    errorProvider1.Clear();
                    EngineerPrivilige();
                    UserAccountControl.currentAccount = UserAccountControl.AllUserAccounts[comboBox_User.Text];
                    RequestUpdateLabel(comboBox_User.SelectedItem.ToString(), "Engineer Level");
                }
                else
                    errorProvider1.SetError(textBox_Password, MultiLanguage.GetMessage("密码错误"));
            }


        }


        private void EngineerPrivilige()
        {
            this.switchButton_Production.Locked = false;
            this.switchButton_Engineering.Locked = false;
            this.switchButton_CPKGRR.Locked = false;
            this.switchButtonGRR.Locked = false;

            this.switchButton_Production.Visible = true;
            this.switchButton_Engineering.Visible = true;
            this.switchButton_CPKGRR.Visible = false;
            this.switchButtonGRR.Visible = false;
            this.btn_Register.Visible = false;
            this.userAccountManager1.Visible = false;
        }

        public void NoneUserPrivilige()
        {
            OpUserPrivilige();
        }

        private void OpUserPrivilige()
        {
            this.switchButton_Production.Locked = false;
            this.switchButton_Engineering.Locked = true;
            this.switchButton_CPKGRR.Locked = true;
            this.switchButtonGRR.Locked = true;

            this.switchButton_Production.Visible = true;
            this.switchButton_Engineering.Visible = false;
            this.switchButton_CPKGRR.Visible = false;
            this.switchButtonGRR.Visible = false;
            this.btn_Register.Visible = false;
            this.userAccountManager1.Visible = false;
        }

        private void AdminUserPrivilige()
        {
            this.switchButton_Production.Locked = false;
            this.switchButton_Engineering.Locked = false;
            this.switchButton_CPKGRR.Locked = false;
            this.switchButtonGRR.Locked = false;

            this.switchButton_Production.Visible = true;
            this.switchButton_Engineering.Visible = true;
            this.switchButton_CPKGRR.Visible = false;
            this.switchButtonGRR.Visible = false;
            this.btn_Register.Visible = true;
            this.userAccountManager1.Visible = true;
            
        }


        //用户注册之后要在ComboBox里面添加选项
        public void ResetComboBox_User()
        {
            this.comboBox_User.Items.Clear();
            UserAccountControl.ReadFromXml();

            this.comboBox_User.Items.Add("Administrator");
            this.comboBox_User.Items.Add("BOTECH");
            this.comboBox_User.Items.Add("Operator");
            foreach (KeyValuePair<string, UserAccount> kvp in UserAccountControl.AllUserAccounts)
            {
                this.comboBox_User.Items.Add(kvp.Value.Account);
            }
            this.comboBox_User.SelectedIndex = 0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (BzMessagebox.Show(MultiLanguage.GetMessage("是否确认退出软件？"), MultiLanguage.GetMessage("提示"),
                System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.OK)
            {
                //XStationManager.Instance.FindStationById((int)StationId.扫码).Stop();
                //XStationManager.Instance.FindStationById((int)StationId.支架组装).Stop();
                //XStationManager.Instance.FindStationById((int)StationId.盖板组装).Stop();
                //XStationManager.Instance.FindStationById((int)StationId.盖板供料).Stop();
                //XStationManager.Instance.FindStationById((int)StationId.Station31_SupportMaterial).Stop();
                //Carrier_FlowBack.Instance.StopConveyor();
                //DataManager.Instance.currentyield.SaveCurrentYield();
                //DataManager.Instance.dummySNManager.Save();
                //DataManager.Instance.toosing.Save();//刷新抛料率
                Application.Exit();
                Environment.Exit(0);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            NoneUserPrivilige();
        }

        private void btn_Register_Click(object sender, EventArgs e)
        {
            UserAccountRegister register = new UserAccountRegister();
            register.OnRegisterSuccess += ResetComboBox_User;
            register.OnRegisterSuccess += userAccountManager1.UpDataGridView;
            register.ShowDialog();
        }

        private void PageLogin_Load(object sender, EventArgs e)
        {

        }

        private void button_Exit_Click(object sender, EventArgs e)
        {
            if (BzMessagebox.Show(MultiLanguage.GetMessage("Bạn có chắc chắn thoát khỏi phần mềm không?"), MultiLanguage.GetMessage("Promt"),
                System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.OK)
            {
                XStationManager.Instance.FindStationById((int)StationId.Scanner).Stop();
                Application.Exit();
                Environment.Exit(0);
            }
        }

        private void button_LogOut_Click(object sender, EventArgs e)
        {
            if (BzMessagebox.Show(MultiLanguage.GetMessage("是否确认退出软件？"), MultiLanguage.GetMessage("提示"),
                System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.OK)
            {
                XStationManager.Instance.FindStationById((int)StationId.Scanner).Stop();
                //XStationManager.Instance.FindStationById((int)StationId.扫码).Stop();
                //XStationManager.Instance.FindStationById((int)StationId.支架组装).Stop();
                //XStationManager.Instance.FindStationById((int)StationId.盖板组装).Stop();
                //XStationManager.Instance.FindStationById((int)StationId.盖板供料).Stop();
                //XStationManager.Instance.FindStationById((int)StationId.Station31_SupportMaterial).Stop();
                //Carrier_FlowBack.Instance.StopConveyor();
                //DataManager.Instance.currentyield.SaveCurrentYield();
                //DataManager.Instance.dummySNManager.Save();
                //DataManager.Instance.toosing.Save();//刷新抛料率
                Application.Exit();
                Environment.Exit(0);
            }
        }

        public event Action<string, string> UpdateLabelRequested;

        public void RequestUpdateLabel(string user, string level)
        {
            UpdateLabelRequested?.Invoke(user, level); // Kích hoạt sự kiện
        }

    }


}
