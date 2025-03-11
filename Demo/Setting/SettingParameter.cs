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
    class SettingParameter : XSetting
    {
        #region B_PLC_Setting
        [MyProperty("PLC_Connect", "B_PLC_Setting")]
        [Description("Kết nối PLC")]
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

        [MyProperty("PLC_IP", "B_PLC_Setting")]
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
        [MyProperty("PLC_Port", "B_PLC_Setting")]
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

        [MyProperty("Ping_PLC", "B_PLC_Setting")]
        public bool Ping_PLC
        {
            get
            {
                return GetNodeValue("Ping_PLC", false);
            }
            set
            {
                SetNodeValue("Ping_PLC", value);
            }
        }

        [MyProperty("Async_HMI", "B_PLC_Setting")]
        public bool Async_HMI
        {
            get
            {
                return GetNodeValue("Async_HMI", false);
            }
            set
            {
                SetNodeValue("Async_HMI", value);
            }
        }
        #endregion
        #region C_CCD_Setting
        [MyProperty("Connect_AOI", "C_CCD_Setting", "Whether connect camera Hik")]
        public bool Connect_AOI
        {
            get
            {
                return GetNodeValue("Connect_AOI", false);
            }
            set
            {
                SetNodeValue("Connect_AOI", value);
            }
        }

        [MyProperty("CCD Name", "C_CCD_Setting","Setting the ccd name for connect methoad")]
        public string CCD_Name
        {
            get
            {
                return GetNodeValue("CCDName", "CCD1");
            }
            set
            {
                SetNodeValue("CCDName", value);
            }
        }


        #endregion
        #region F_Scanner_Setting
        [MyProperty("Scanner_Using", "F_Scanner_Setting")]
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

        //[MyProperty("ScannerTCP or ScannerCOM", "Scanner_Setting")]
        //public bool TCPorCOMScanner
        //{
        //    get
        //    {
        //        //true == TCP
        //        //fasle = COM
        //        return GetNodeValue("TCPorCOMScanner", false);
        //    }
        //    set
        //    {
        //        SetNodeValue("TCPorCOMScanner", value);
        //    }
        //}

        [MyProperty("Scanner_IP", "F_Scanner_Setting")]
        public string TCPScanner_IP
        {
            get
            {
                return GetNodeValue("Scanner_IP", "192.168.1.45");
            }
            set
            {
                SetNodeValue("Scanner_IP", value);
            }
        }

        [MyProperty("TCPScanner_Port", "F_Scanner_Setting")]
        public int TCPScanner_Port
        {
            get
            {
                return GetNodeValue("Scanner_Port", 1200);
            }
            set
            {
                SetNodeValue("Scanner_Port", value);
            }
        }
        //[MyProperty("COMScanner_Port", "Scanner_Setting")]
        //public COM COM_Port
        //{
        //    get
        //    {
        //        return (COM)GetNodeValue("COMScanner_Port", 3);
        //    }
        //    set
        //    {
        //        SetNodeValue("COMScanner_Port", (int)value);
        //    }
        //}
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
        [MyProperty("CheckDummySN", "Parameter", "Kiểm tra xem có bị trùng SN với sản phẩm trước đó không")]
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
        #region Model
        [MyProperty("Model", "A_Model","Dislay model"), ReadOnly(true)]
        public string ModelName
        {
            get
            {
                return LoadModel.currentModel.modelName;
            }
            set
            {
            }
        }

        #endregion
        #region Light Setting
        [MyProperty("Light_Connect", "F_Light_Setting")]
        public bool Light_Connect
        {
            get
            {
                return GetNodeValue("Light_Connect", false);
            }
            set
            {
                SetNodeValue("Light_Connect", value);
            }
        }


        [MyProperty("Light_IP", "F_Light_Setting")]
        public string Light_IP
        {
            get
            {
                return GetNodeValue("Light_IP", "192.168.1.100");
            }
            set
            {
                SetNodeValue("Light_IP", value);
            }
        }
        [MyProperty("Light_Port", "F_Light_Setting")]
        public int Light_Port
        {
            get
            {
                return GetNodeValue("Light_Port", 6000);
            }
            set
            {
                SetNodeValue("Light_Port", value);
            }
        }
        [MyProperty("Light_COM", "F_Light_Setting")]
        public COM Light_COM
        {
            get
            {
                return (COM)GetNodeValue("Light_COM", 3);
            }
            set
            {
                SetNodeValue("Light_COM", (int)value);
            }
        }
        [MyProperty("Center_Light_Sensitive", "F_Light_Setting", "Cài đặt độ sáng của đèn")]
        public string MiddleLightSensitive
        {
            get
            {
                return GetNodeValue("CenterLightSensitive", "255");
            }
            set
            {
                var convert = Convert.ToInt16(value);
                if (convert < 10) value = $"00{value}";
                else if (convert >= 10 && convert < 100) value = $"0{value}";
                else if (convert >= 100 && convert < 255) value = $"{value}";
                else if (convert > 255) value = "255";
                SetNodeValue("CenterLightSensitive", value);
            }
        }
        [MyProperty("Right_Sensitive", "F_Light_Setting","Cài đặt độ sáng của đèn bên phải")]
        public string Right_Sensitive
        {
            get
            {
                return GetNodeValue("Right_Sensitive", "2000");
            }
            set
            {
                var convert = Convert.ToInt16(value);
                if (convert < 10) value = $"000{value}";
                else if (convert >= 10 && convert < 100) value = $"00{value}";
                else if (convert >= 100 && convert < 1000) value = $"0{value}";
                else if (convert > 2000) value = "2000";

                SetNodeValue("Right_Sensitive", value);
            }
        }

        [MyProperty("Left_Sensitive", "F_Light_Setting","Cài đặt độ sáng của đèn bên trái")]
        public string Left_Sensitive
        {
            get
            {
                return GetNodeValue("Left_Sensitive", "2000");
            }
            set
            {
                var convert = Convert.ToInt16(value);
                if (convert < 10) value = $"000{value}";
                else if (convert >= 10 && convert < 100) value = $"00{value}";
                else if (convert >= 100 && convert < 1000) value = $"0{value}";
                else if (convert > 2000) value = "2000";

                SetNodeValue("Left_Sensitive", value);
            }
        }


        #endregion
        #region MES
        [MyProperty("MES_Connnect", "D_MES_Setting")]
        public bool MES_Connnect
        {
            get
            {
                return GetNodeValue("MES_Connnect", false);
            }
            set
            {
                SetNodeValue("MES_Connnect", value);
            }
        }

        [MyProperty("MES_IP", "D_MES_Setting")]
        public string MES_IP
        {
            get
            {
                return GetNodeValue("MES_IP", "10.0.0.100");
            }
            set
            {
                SetNodeValue("MES_IP", value);
            }
        }
        #endregion

    }
}
