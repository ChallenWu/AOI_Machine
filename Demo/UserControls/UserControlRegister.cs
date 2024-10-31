using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using XCore;
using HB_IWatch;

namespace Demo
{
    public partial class UserAccountRegister : Form
    {
        public UserAccountRegister()
        {
            InitializeComponent();
        }


        public delegate void OnRegisterSuccessDelegate();
        public OnRegisterSuccessDelegate OnRegisterSuccess;

        private void btn_Register_Click(object sender, EventArgs e)
        {
            this.errorProvider_DiffPassWord.Clear();
            if (UserAccountControl.IsRepeatCount(txt_Account.Text))
            {
                this.errorProvider_DiffPassWord.SetError(txt_Account, "用户已存在");
                return;
            }

            if (txt_Account.Text == "")
            {
                this.errorProvider_DiffPassWord.SetError(txt_Account, "账号不能为空");
                return;
            }

            if (txt_Name.Text == "")
            {
                this.errorProvider_DiffPassWord.SetError(txt_Name, "姓名不能为空");
                return;
            }

            if (txt_PassWord1.Text == "")
            {
                this.errorProvider_DiffPassWord.SetError(txt_PassWord1, "密码不能为空");
                return;
            }

            if (txt_PassWord1.Text != txt_PassWord2.Text)
            {
                this.errorProvider_DiffPassWord.SetError(txt_PassWord2, "输入密码不一致");
                return;
            }

            Match mInfo = Regex.Match(txt_Account.Text.Substring(0, 1), @"^[A-Za-z]+$");
            if (!mInfo.Success) //如果是英文
            {
                this.errorProvider_DiffPassWord.SetError(txt_Account, "账号第一位必须为字母！");
                return;
            }

            UserAccount account = new UserAccount(txt_Account.Text, txt_Name.Text, txt_PassWord1.Text);
            if (!UserAccountControl.AddAccount(account))
                BzMessagebox.Show(MultiLanguage.GetMessage("注册失败"));
            else
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("注册成功"));
                if (OnRegisterSuccess != null)
                    OnRegisterSuccess();
            }
            this.Close();
        }
    }
}
