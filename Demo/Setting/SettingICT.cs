using HB_IWatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore;
using XCore.Framework.MultiLang;

namespace Demo.Setting
{
    class SettingICT : XSetting
    {
        #region PLC_Setting
        [MyProperty("PLC_Connect", "PLC_Setting")]
        public bool PLC_Connect
        {
            get
            {
                return GetNodeValue("PLC_Connect", false);
            }
            set
            {
                SetNodeValue("PLC_Connect", value);
            }
        }

        [MyProperty("PLC_IP", "PLC_Setting")]
        public string PLC_IP
        {
            get
            {
                return GetNodeValue("PLC_IP", "10.0.0.100");
            }
            set
            {
                SetNodeValue("PLC_IP", value);
            }
        }
        [MyProperty("PLC_Port", "PLC_Setting")]
        public string PLC_Port
        {
            get
            {
                return GetNodeValue("PLC_Port", "2000");
            }
            set
            {
                SetNodeValue("PLC_Port", value);
            }
        }
        #endregion
        #region CCD_Setting
        [MyProperty("CCD_Using", "CCD_Setting")]
        public bool CCD_Using
        {
            get
            {
                return GetNodeValue("CCD_Using", false);
            }
            set
            {
                SetNodeValue("CCD_Using", value);
            }
        }
        [MyProperty("CCD_IP", "CCD_Setting")]
        public string CCD_IP
        {
            get
            {
                return GetNodeValue("CCD_IP", "127.0.0.1");
            }
            set
            {
                SetNodeValue("CCD_IP", value);
            }
        }


        [MyProperty("CCD_Port", "CCD_Setting")]
        public int CCD_Port
        {
            get
            {
                return GetNodeValue("CCD_Port", 9999);
            }
            set
            {
                SetNodeValue("CCD_Port", value);
            }
        }
        #endregion
        #region Scanner_Setting
        [MyProperty("Scanner_Using", "Scanner_Setting")]
        public bool Scanner_Using
        {
            get
            {
                return GetNodeValue("Scanner_Using", false);
            }
            set
            {
                SetNodeValue("Scanner_Using", value);
            }
        }

        [MyProperty("ScanLead or ICW", "Scanner_Setting")]
        public bool ScanLead
        {
            get
            {
                return GetNodeValue("ScanLead", false);
            }
            set
            {
                SetNodeValue("ScanLead", value);
            }
        }

        [MyProperty("ScanLead_IP", "Scanner_Setting")]
        public string ScanLead_IP
        {
            get
            {
                return GetNodeValue("ScanLead_IP", "192.168.1.45");
            }
            set
            {
                SetNodeValue("ScanLead_IP", value);
            }
        }

        [MyProperty("ScanLead_Port", "Scanner_Setting")]
        public int ScanLead_Port
        {
            get
            {
                return GetNodeValue("ScanLead_Port", 1200);
            }
            set
            {
                SetNodeValue("ScanLead_Port", value);
            }
        }



        [MyProperty("ICW_Port", "Scanner_Setting")]
        public COM COM_Port
        {
            get
            {
                return (COM)GetNodeValue("COM_Port", 3);
            }
            set
            {
                SetNodeValue("COM_Port", (int)value);
            }
        }


        [MyProperty("SerialNumber_Lenght", "Scanner_Setting")]
        public int SN_Lenght
        {
            get
            {
                return GetNodeValue("SN_Lenght", 15);
            }
            set
            {
                SetNodeValue("SN_Lenght", value);
            }
        }
        [MyProperty("SerialNumber_Keywords", "Scanner_Setting")]
        public string SerialNumber_Keywords
        {
            get
            {
                return GetNodeValue("SerialNumber_Keywords", "XY");
            }
            set
            {
                SetNodeValue("SerialNumber_Keywords", value);
            }
        }
        #endregion
        #region Safedoor
        [MyProperty("Safedoor", "Parameter")]
        public bool Safedoor
        {
            get
            {
                return GetNodeValue("Safedoor", false);
            }
            set
            {
                SetNodeValue("Safedoor", value);
            }
        }
        [MyProperty("CheckDummySN", "Parameter")]
        public bool CheckDummySN
        {
            get
            {
                return GetNodeValue("CheckDummySN", false);
            }
            set
            {
                SetNodeValue("CheckDummySN", value);
            }
        }

        #endregion
    }
}
