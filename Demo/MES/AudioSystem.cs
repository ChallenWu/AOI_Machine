using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;

namespace BoTech
{
    public sealed class AudioSystem
    {

        public static string[] Errors = new string[] 
        { "MES Error", "Safety Error", "Scanning Error", "Vision Error", "Cylinder Error", "Motion Error", "Sensor Error", "PDI Error" };

        #region MachineName
        public const string MachineName_TI040_1 = "TI040-1";
        public const string MachineName_BE010_1 = "BE010-1";
        #endregion

        #region Thông số thiết bị
        //Thông tin thiết bị
        public static MachineMessage InforMachine = new MachineMessage();
        // Thông tin sản phẩm
        /// <summary>
        /// public static UCMessage UCM = new UCMessage();
        /// </summary>
        public static ProductMessage UCM = new ProductMessage();
        #endregion     

        #region Custom color constants
        public static Color Color_SelectedBtn = Color.FromArgb(175, 218, 150);
        public static Color Color_UnselectedBtn = Color.FromArgb(238, 238, 238);
        public static Color Color_SelectedPauseBtn = Color.FromArgb(132, 193, 251);
        public static Color Color_SelectedEndBtn = Color.FromArgb(228, 146, 138);
        public static Color Color_ErrorRed = Color.FromArgb(236, 93, 87);

        public static Color Color_Run = Color.LightGreen;
        public static Color Color_Pause = Color.Yellow;
        public static Color Color_PauseBtn = Color.LightBlue;
        public static Color Color_Alarm = Color.Red;
        #endregion
        /// <summary>
        /// Thông tin cơ bản của thiết bị
        /// </summary>
        public class MachineMessage
        {            
            public string ProType = "E-SKU";
            public string Vendor { get; set; } = "BZ";
            public string Main_SW_version = "BZ_2.1.0.1_230324_POR";
            public string Vision_SW_version = "1";
            public string Laser_SW_version = "0";
            public string Robot_SW_version = "1";
            public string SW_release_data = "230324";
            /// <summary>
            /// SW type
            /// POR/BKP/RTF/VAL
            /// POR:SW for POR machine
            /// BKP:SW for backup machine
            /// RTF:SW for retrofit machine
            /// VAL:SW version under validation
            /// </summary>
            public string SW_Type = "POR";
            public string SW_version { get; set; } = "BZ_2.1.0.1_230324_POR";
            public string MS_Hash = "";
            public string VS_Hash = "";
            public string CCDParh = "";
            public string Site { get; set; } = "Fuyu";
            public string Line { get; set; } = "Line 1";
            public string Station { get; set; } = "AOI";
            public string MachineNo { get; set; } = "001";
            public string language { get; set; }
            public int CurrentLanugaeIndex { get; set; }
            public string Main_SW_Path { get; set; }
            public MesNeed MES { get; set; } = new MesNeed();
        }
        /// <summary>
        /// Khay vật liệu/ - nếu dùng trong công trạm lắp ráp liệu vào sản phẩm
        /// </summary>     
        public class MesNeed
        {
            public string TR_NO { get; set; }
            /// <summary>
            /// 旧料盘；
            /// </summary>
            public string TR_NO_Old { get; set; }
            public string empNo { get; set; }
            public string terminalName { get; set; }
            public string collectType { get; set; }
            public string workOrder { get; set; }

        }
    }

}
