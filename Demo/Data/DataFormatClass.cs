using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoTech
{
    class DataFormatClass
    {
    }
    public class PlanDownMessage
    {
        public string PlanDownReason { get; set; }
    }
    public class AlarmMessage
    {
        public string AlarmSerialNumber { get; set; }
        public DateTime HappenTime { get; set; }
        public DateTime EffectiveHappenTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }
        public int DurationCatigory { get; set; }

        public AlarmCode NowAlarm = new AlarmCode();

        /*DFM2.5新增↓*/

        /*DFM2.5新增↑*/



    }


    public class LadOper
    {
        public string audit { get; set; } = "true";
        public string LadKeys { get; set; } = "";
        public int Mode { get; set; } = 0;
        public int OperatorID { get; set; } = 1;
        public long TestSeriesID { get; set; }

        public int Count { get; set; } = 0;


        //public bool isContinueCPK { get; set; } //是否继续CPK模式；
        //public bool isContinueGRR_1 { get; set; }//ID1是否继续；
        //public bool isContinueGRR_2 { get; set; }//ID2是否继续；
        //public bool isContinueGRR_3 { get; set; }//ID3是否继续；
        //public bool isContinueSCS { get; set; }//SCS是否继续；

    }

    public enum LADKeys
    {
        CPK,
        GRR,
        SCS,
    }



    public class SensorCom
    {
        public string PLCAddressType;
        public string SensorMarkAddress;
        public string SensorMark;
        public string PC_SensorMark;
        public string SensorBackAddress;
        public string ComControlIndex;
    }
    public class MachineState
    {
        public string PLCAddressType;
        public string State;
        public string AlarmStartAddress;
        public string AlarmLong;
    }

    public struct AlarmCode
    {
        public string PLCAddressType;
        public string PLCAddressAll;
        public string PLCAddress;
        public string PLCAddress2;
        public string ErrorCode;
        public string ErrorCatrgory;
        public string Severity;
        public string MessageEn;
        public string MessageCn;
        public string ErrorDetail;
        public string DealtMethod;
    }
    public struct PLCComMessage
    {
        public int ThreadIndex;
        public string ReadAddressType;
        public string ReadFunctionAddress;
        public string ReadFunctionAddressValue;
        public string ReadFunctionAddressCode;
        public string CameraNum;
        public string NuzzleNumAddress;
        public string MovePointAddress;
        public string PointXAddress;
        public string PointYAddress;
        public string PointRaddress;
        public string PointX2Address;
        public string PointY2Address;
        public string SNStartAddress;
        public string SNLength;
        public string WritePLCAddress;
    }


    public class HiveMessage
    {
        public DateTime HappenTime { get; set; }
        public int MachineState { get; set; }
        public int PreviousState { get; set; }
        public Int64 TimeDuration { get; set; }
    }

    public class OperationLog
    {
        public DateTime HappenTime { get; set; }

        //public AccountInfo AI { get; set; }
        public string OperationMessage { get; set; }
    }
    public class UCMessage
    {
        public string UC_SN { get; set; } = "";
        public string TR_Lot_No { get; set; }
        public UnitMessage[] Units { get; set; } = new UnitMessage[] { new UnitMessage(), new UnitMessage(), new UnitMessage(), new UnitMessage() };

        public string OpID;//添加OP卡号；

    }
    public class UnitMessage
    {

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double CT { get; set; }
        public int HiveState { get; set; }
        public string UnitSN { get; set; }
        public string ComponentSN { get; set; }
        public string Shift { get; set; }
        public string Pass { get; set; }
        public string WO { get; set; }
        public string STNID { get; set; }
        public string TR_NO { get; set; }
        //public AccountInfo AIF { get; set; }
        public bool CanStart { get; set; }
        public string CarrierPath { get; set; }
        public string UnitPath { get; set; }
        public string workPath { get; set; }

        /// <summary>
        /// 料盘图片路径；
        /// </summary>
        public string TR_Lot_No_Path { get; set; }
    }

    public class SystemMessage
    {
        public string MachineType { get; set; }
        public short TCPStartNum { get; set; }
        public short TCPCount { get; set; }
        public int PLCThreadCount { get; set; }
    }
    public class UserMessage
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string EmployeeID { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public int UserLevel { get; set; }
        public string JobTitle { get; set; }
    }


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

        public string SW_version = "";
        public string MS_Hash = "";
        public string VS_Hash = "";
        public string CCDParh = "";
        public string Site { get; set; }
        public string Line { get; set; }
        public string Station { get; set; }
        public string MachineNo { get; set; }
        public string language { get; set; }
        public int CurrentLanugaeIndex { get; set; }


        public string Main_SW_Path { get; set; }
        public bool IsRemote { get; set; }

        public PDCAMessage PDCANeed = new PDCAMessage();

        public PCMessage PC { get; set; } = new PCMessage();
        public class PDCAMessage
        {
            public string LocalIP { get; set; } = "169.254.1.12";
            public string FilePath { get; set; } = "";
            public string UserName { get; set; } = "";
            public string Password { get; set; } = "";

        }

        public CCDMessage[] CCD { get; set; } = new CCDMessage[] { new CCDMessage() };

        public PLCMessage PLC { get; set; } = new PLCMessage();
        public DriverMessage Driver { get; set; } = new DriverMessage();

        public MesNeed MES { get; set; } = new MesNeed();

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
        public class PCMessage
        {
            public string CPU { get; set; } = "i7-7700";
            public string[] HardDIsk { get; set; } = new string[] { "8T" };
        }
        public class CCDMessage
        {
            public string TYPE { get; set; } = "Mono";
            public string Resolution { get; set; }
        }
        public class PLCMessage
        {
            public string Conveyor { get; set; } = "";
            public string Rob_Arm { get; set; } = "INOVANCE";
        }
        public class DriverMessage
        {
            public string Dirver { get; set; }
        }


    }


}
