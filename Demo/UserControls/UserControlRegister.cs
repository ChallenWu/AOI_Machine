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
using Sunny.UI;

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
                this.errorProvider_DiffPassWord.SetError(txt_Account, "Người dùng đã tồn tại");
                return;
            }

            if (txt_Account.Text == "")
            {
                this.errorProvider_DiffPassWord.SetError(txt_Account, "Tài khoản không thể trống");
                return;
            }

            if (txt_Name.Text == "")
            {
                this.errorProvider_DiffPassWord.SetError(txt_Name, "Tên không thể trống");
                return;
            }

            if (txt_PassWord1.Text == "")
            {
                this.errorProvider_DiffPassWord.SetError(txt_PassWord1, "Mật khẩu không thể trống");
                return;
            }

            if (txt_PassWord1.Text != txt_PassWord2.Text)
            {
                this.errorProvider_DiffPassWord.SetError(txt_PassWord2, "Mật khẩu đầu vào không nhất quán");
                return;
            }

            Match mInfo = Regex.Match(txt_Account.Text.Substring(0, 1), @"^[A-Za-z]+$");
            if (!mInfo.Success) //如果是英文
            {
                this.errorProvider_DiffPassWord.SetError(txt_Account, "Chữ số đầu tiên của số tài khoản phải là chữ cái！");
                return;
            }

            UserAccount account = new UserAccount(txt_Account.Text, txt_Name.Text, txt_PassWord1.Text);
            if (!UserAccountControl.AddAccount(account))
                BzMessagebox.Show(MultiLanguage.GetMessage("Đăng ký không thành công"));
            else
            {
                BzMessagebox.Show(MultiLanguage.GetMessage("Đăng ký thành công"));
                if (OnRegisterSuccess != null)
                    OnRegisterSuccess();
            }
            this.Close();
        }
    }
}
