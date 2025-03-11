using HB_IWatch;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore;
using XCore.Framework.MultiLang;

namespace Demo.Setting
{
    class SettingCode : XSetting
    {
        #region Product Serial Number
        [MyProperty("SN1_Lenght", "SerialNumber_Setting","setting the lenght for serial number 1")]
        public int SN1_Lenght
        {
            get
            {
                return GetNodeValue("SN1_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN1_Lenght", value);
            }
        }
        [MyProperty("SN1_Keywords", "SerialNumber_Setting")]
        public string SN1_Keywords
        {
            get
            {
                return GetNodeValue("SN1_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SN1_Keywords", value);
            }
        }

        [MyProperty("SN2_Lenght", "SerialNumber_Setting")]
        public int SN2_Lenght
        {
            get
            {
                return GetNodeValue("SN2_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN2_Lenght", value);
            }
        }
        [MyProperty("SN2_Keywords", "SerialNumber_Setting")]

        public string SN2_Keywords
        {
            get
            {
                return GetNodeValue("SN2_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SN2_Keywords", value);
            }
        }


        [MyProperty("SN3_Lenght", "SerialNumber_Setting")]
        public int SN3_Lenght
        {
            get
            {
                return GetNodeValue("SN3_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN3_Lenght", value);
            }
        }
        [MyProperty("SN3_Keywords", "SerialNumber_Setting")]

        public string SN3_Keywords
        {
            get
            {
                return GetNodeValue("SN3_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SN3_Keywords", value);
            }
        }

        [MyProperty("SN4_Lenght", "SerialNumber_Setting")]
        public int SN4_Lenght
        {
            get
            {
                return GetNodeValue("SN4_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN4_Lenght", value);
            }
        }
        [MyProperty("SN4_Keywords", "SerialNumber_Setting")]

        public string SN4_Keywords
        {
            get
            {
                return GetNodeValue("SN4_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SN4_Keywords", value);
            }
        }


        [MyProperty("SN5_Lenght", "SerialNumber_Setting")]
        public int SN5_Lenght
        {
            get
            {
                return GetNodeValue("SN5_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN5_Lenght", value);
            }
        }
        [MyProperty("SN5_Keywords", "SerialNumber_Setting")]

        public string SN5_Keywords
        {
            get
            {
                return GetNodeValue("SN5_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SN5_Keywords", value);
            }
        }

        [MyProperty("SN6_Lenght", "SerialNumber_Setting")]
        public int SN6_Lenght
        {
            get
            {
                return GetNodeValue("SN6_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN6_Lenght", value);
            }
        }
        [MyProperty("SN6_Keywords", "SerialNumber_Setting")]

        public string SN6_Keywords
        {
            get
            {
                return GetNodeValue("SN6_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SN6_Keywords", value);
            }
        }

        [MyProperty("SN7_Lenght", "SerialNumber_Setting")]
        public int SN7_Lenght
        {
            get
            {
                return GetNodeValue("SN7_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN7_Lenght", value);
            }
        }
        [MyProperty("SN7_Keywords", "SerialNumber_Setting")]

        public string SN7_Keywords
        {
            get
            {
                return GetNodeValue("SN7_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SN7_Keywords", value);
            }
        }

        [MyProperty("SN8_Lenght", "SerialNumber_Setting")]
        public int SN8_Lenght
        {
            get
            {
                return GetNodeValue("SN8_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN8_Lenght", value);
            }
        }
        [MyProperty("SN8_Keywords", "SerialNumber_Setting")]

        public string SN8_Keywords
        {
            get
            {
                return GetNodeValue("SN8_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SN8_Keywords", value);
            }
        }

        [MyProperty("SN9_Lenght", "SerialNumber_Setting")]
        public int SN9_Lenght
        {
            get
            {
                return GetNodeValue("SN9_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN9_Lenght", value);
            }
        }
        [MyProperty("SN9_Keywords", "SerialNumber_Setting")]

        public string SN9_Keywords
        {
            get
            {
                return GetNodeValue("SN9_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SN9_Keywords", value);
            }
        }

        [MyProperty("SN10_Lenght", "SerialNumber_Setting")]
        public int SN10_Lenght
        {
            get
            {
                return GetNodeValue("SN10_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN10_Lenght", value);
            }
        }
        [MyProperty("SN10_Keywords", "SerialNumber_Setting")]
        public string SN10_Keywords
        {
            get
            {
                return GetNodeValue("SN10_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SN10_Keywords", value);
            }
        }
        [MyProperty("SN11_Lenght", "SerialNumber_Setting")]
        public int SN11_Lenght
        {
            get
            {
                return GetNodeValue("SN11_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN11_Lenght", value);
            }
        }
        [MyProperty("SN11_Keywords", "SerialNumber_Setting")]
        public string SN11_Keywords
        {
            get
            {
                return GetNodeValue("SN11_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SN11_Keywords", value);
            }
        }
        [MyProperty("SN12_Lenght", "SerialNumber_Setting")]
        public int SN12_Lenght
        {
            get
            {
                return GetNodeValue("SN12_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN12_Lenght", value);
            }
        }
        [MyProperty("SN12_Keywords", "SerialNumber_Setting")]
        public string SN12_Keywords
        {
            get
            {
                return GetNodeValue("SN12_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SN12_Keywords", value);
            }
        }
        #endregion

    }
}
