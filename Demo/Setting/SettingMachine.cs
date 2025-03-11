using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCore;
using System.ComponentModel;
using System.Windows.Forms;
using XCore.Framework.MultiLang;
using System.IO;
using Demo;

namespace HB_IWatch
{
    public enum MachineType
    {
        PAM1,
        PAM2,
        PAM3,
        BABI,
        HBCLM,
        AOI
    }
    public enum COM
    {
        COM1 = 1,
        COM2,
        COM3,
        COM4,
        COM5,
        COM6,
        COM7,
        COM8,
        COM9,
        COM10,
        COM11,
        COM12,
        COM13,
        COM14,
        COM15,
        COM16,
        COM17,
        COM18,
        COM19,
        COM20
    }

    class SettingMachine : XSetting
    {
        #region C_Device Information
        //***********C_Device Information**************//
        [MyProperty("MachineType", "C_Device Information")]
        public MachineType MachineType
        {
            get
            {
                return (MachineType)GetNodeValue("MachineType", 1);
            }
            set
            {
                SetNodeValue("MachineType", (int)value);
            }
        }

        public bool AutoClearImage
        {
            get
            {
                return GetNodeValue("AutoClearImage", false);
            }
            set
            {
                SetNodeValue("AutoClearImage", value);
            }
        }
        [MyProperty("softdate", "C_Device Information")]
        private string softdate
        {
            get
            {
                return GetNodeValue("softdate", "20230723");
            }
            set
            {
                SetNodeValue("softdate", value);
            }
        }
        [MyProperty("ProductVersion", "C_Device Information")]
        public string ProductVersion
        {
            get
            {
                return GetNodeValue("ProductVersion", "BZ_a.b.0.0_c_POR");
            }
            set
            {
                SetNodeValue("ProductVersion", value);
            }
        }
        [MyProperty("Current software version", "C_Device Information"), ReadOnly(true)]
        private string[] currentsoft_data
        {
            get { return GetNodeValue("softolddata", new string[2] { "1", "230720" }); }
            set { SetNodeValue("softolddata", value); }
        }
        [MyProperty("Historical software versions", "C_Device Information"), ReadOnly(true)]
        private string[] presoft_data
        {
            get { return GetNodeValue("oldsoftolddata", new string[2] { "1", "230720" }); }
            set { SetNodeValue("oldsoftolddata", value); }
        }
        [MyProperty("Machine language", "C_Device Information")]
        public LanguageType LanguageType
        {
            get
            {
                return (LanguageType)GetNodeValue("LanguageType", 1);
            }
            set
            {
                SetNodeValue("LanguageType", (int)value);
                MultiLanguage.ChangeLanguage(LanguageType, true);
            }
        }
        [MyProperty("BuildDate", "C_Device Information")]
        public string BuildDate
        {
            get
            {
                return GetNodeValue("BuldDate", "2017/8/14");
            }
            set
            {
                SetNodeValue("BuldDate", value);
            }
        }
        [MyProperty("HardwareVersion", "C_Device Information")]
        public string HardwareVersion
        {
            get
            {
                return GetNodeValue("HardwareVersion", "1.0");
            }
            set
            {
                SetNodeValue("HardwareVersion", value);
            }
        }
        [MyProperty("SoftWareUpdate", "C_Device Information")]
        public string SoftWareUpdate
        {
            get
            {
                return GetNodeValue("SoftWareUpdate", "2017/8/14");
            }
            set
            {
                SetNodeValue("SoftWareUpdate", value);
            }
        }
        //[MyProperty("是否是NPI设备", "C_Device Information")]
        //public bool 是否是NPI设备
        //{
        //    get
        //    {
        //        return GetNodeValue("Is_NPI_Device", false);
        //    }
        //    set
        //    {
        //        if (!InputNumber(MultiLanguage.GetMessage("输入修改密码")))
        //            return;
        //        SetNodeValue("Is_NPI_Device", value);
        //    }
        //}

        #endregion

     
        public string GetSoftVersion()
        {
            string str = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var arrVersion = str.Split('.');
            DateTime dt1 = Convert.ToDateTime("2000-01-01");
            DateTime result = dt1.AddDays(Convert.ToInt64(arrVersion[2]));
            string[] str1 = new string[2];
            if (currentsoft_data[1] != result.ToString("yyMMdd"))
            {
                str1[0] = (Convert.ToInt32(currentsoft_data[0]) + 1).ToString();
                str1[1] = result.ToString("yyMMdd");
                presoft_data = currentsoft_data;
                currentsoft_data = str1;
            }
            string str2 = ProductVersion.Replace("a", currentsoft_data[0]);
            return str2.Replace("c", currentsoft_data[1]);
        }
        public string GetoldSoftVersion()
        {
            string str2 = ProductVersion.Replace("a", presoft_data[0]);
            return str2.Replace("c", presoft_data[1]);
        }
        protected bool InputNumber(string szMsg)
        {
            FailTip ft = new FailTip(szMsg);
            ft.SelectAll();
            ft.SetSubmitText(MultiLanguage.GetMessage("确认"));
            ft.SetCancelText(MultiLanguage.GetMessage("退出"));
            ft.ShowDialog();
            ft.WaitOne();
            if (ft.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (ft.GetText() == DateTime.Now.ToString("yyyyMMdd"))
                    {
                        return true;
                    }
                    else
                    {
                        BzMessagebox.Show(MultiLanguage.GetMessage("输入密码不正确") + "!", MultiLanguage.GetMessage("错误"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                catch
                {
                    BzMessagebox.Show(MultiLanguage.GetMessage("输入密码不正确") + "!", MultiLanguage.GetMessage("错误"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
                return false;
        }
    }
}
