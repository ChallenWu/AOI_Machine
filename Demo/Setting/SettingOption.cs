using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XCore;
using System.ComponentModel;
using System.Windows.Forms;
using XCore.Framework.MultiLang;
using System.IO;
//using System.ServiceModel.PeerResolvers;
using Demo;

namespace HB_IWatch
{
    public enum MachineType
    {
        PAM1,
        PAM2,
        PAM3,
        BABI,
        HBCLM
    }

    public enum AndOr
    {
        And,
        Or
    }

    public enum MasterSuckerNum
    {
        气路1,
        气路2
    }

    public enum MachineDest
    {
        上海,
        常熟,
        成都
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

    public enum AttachSeq
    {
        吸嘴2 = 1,
        吸嘴3
    }

    class SettingOption : XSetting
    {

        #region A_IP地址及端口
        //***********A_IP地址及端口**************//
        [MyProperty("ACS_IP", "A_IP地址及端口")]
        public string ACS_IP
        {
            get
            {
                return GetNodeValue("ACS_IP", "10.0.0.100");
            }
            set
            {
                SetNodeValue("ACS_IP", value);
            }
        }
        [MyProperty("ACS_Port", "A_IP地址及端口")]
        public int ACS_Port
        {
            get
            {
                return GetNodeValue("ACS_Port", 701);
            }
            set
            {
                SetNodeValue("ACS_Port", value);
            }
        }
        [MyProperty("Left_CCD_IP", "A_IP地址及端口")]
        public string Left_CCD_IP
        {
            get
            {
                return GetNodeValue("CCD_IP", "192.168.1.10");
            }
            set
            {
                SetNodeValue("CCD_IP", value);
            }
        }
        [MyProperty("Left_CCD_Port", "A_IP地址及端口")]
        public int Left_CCD_Port
        {
            get
            {
                return GetNodeValue("CCD_Port", 8500);
            }
            set
            {
                SetNodeValue("CCD_Port", value);
            }
        }
        [MyProperty("Right_CCD_IP", "A_IP地址及端口")]
        public string Right_CCD_IP
        {
            get
            {
                return GetNodeValue("CCD2_IP", "192.168.2.10");
            }
            set
            {
                SetNodeValue("CCD2_IP", value);
            }
        }
        [MyProperty("Right_CCD_Port", "A_IP地址及端口")]
        public int Right_CCD_Port
        {
            get
            {
                return GetNodeValue("CCD2_Port", 8500);
            }
            set
            {
                SetNodeValue("CCD2_Port", value);
            }
        }
        [MyProperty("温控器端口", "A_IP地址及端口")]
        public COM 温控器端口
        {
            get
            {
                return (COM)GetNodeValue("TempCtrl_COM", 9);
            }
            set
            {
                SetNodeValue("TempCtrl_COM", (int)value);
            }
        }
        [MyProperty("点胶机IP地址", "A_IP地址及端口")]
        public string 点胶机IP地址
        {
            get
            {
                return GetNodeValue("DispenserIP", "192.168.10.1");
            }
            set
            {
                SetNodeValue("DispenserIP", value);
            }
        }

        //***********A_IP地址及端口**************//
        #endregion

        #region B_设备保护
        //***********C_设备保护**************//
        [MyProperty("是否开启蜂鸣器", "B_设备保护")]
        public bool 是否开启蜂鸣器
        {
            get
            {
                return true;//GetNodeValue("IsOpenBird", true);
            }
            set
            {
                SetNodeValue("IsOpenBird", value);
            }
        }
        [MyProperty("是否开启安全门保护", "B_设备保护")]
        private bool 是否开启安全门保护
        {
            get
            {
                return GetNodeValue("IsOpenSafeDoor", true);
            }
            set
            {
                SetNodeValue("IsOpenSafeDoor", value);
            }
        }
        public bool IsOpensafeDoor()
        {
            return 是否开启安全门保护;
            //return 是否开启安全门保护;
        }
        [MyProperty("是否开启安全门急停复位", "B_设备保护")]
        public bool 是否开启安全门急停复位
        {
            get
            {
                return GetNodeValue("SafeDoorEStop", false);
            }
            set
            {
                SetNodeValue("SafeDoorEStop", value);
            }
        }
        //***********B_设备保护**************//
        #endregion

        #region C_设备信息
        //***********C_设备信息**************//
        [MyProperty("机器类别", "C_设备信息")]
        public MachineType 机器类别
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
        [MyProperty("项目_部门_楼层_线体_AEID_AEVendor_MachineSn_AESUBID", "C_设备信息")]
        public string[] 项目_部门_楼层_线体_AEID_AEVendor_MachineSn_AESUBID
        {
            get { return GetNodeValue("UpdateInfor", new string[] { "D1,d1,d1,d1,d1,d1,d1,d1" }); }
            set { SetNodeValue("UpdateInfor", value); }
        }
        [MyProperty("软件日期", "C_设备信息")]
        private string 软件日期
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
        [MyProperty("软件版本", "C_设备信息")]
        public string 软件版本
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
        [MyProperty("当前软件版本", "C_设备信息"), ReadOnly(true)]
        private string[] 当前软件版本号_版本
        {
            get { return GetNodeValue("softolddata", new string[2] { "1", "230720" }); }
            set { SetNodeValue("softolddata", value); }
        }
        [MyProperty("历史软件版本", "C_设备信息"), ReadOnly(true)]
        private string[] 历史软件版本号_版本
        {
            get { return GetNodeValue("oldsoftolddata", new string[2] { "1", "230720" }); }
            set { SetNodeValue("oldsoftolddata", value); }
        }
        [MyProperty("设备语言", "C_设备信息")]
        public LanguageType 语言
        {
            get
            {
                return (LanguageType)GetNodeValue("LanguageType", 1);
            }
            set
            {
                SetNodeValue("LanguageType", (int)value);
                MultiLanguage.ChangeLanguage(语言, true);
            }
        }
        [MyProperty("BuildDate", "C_设备信息")]
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
        [MyProperty("HardwareVersion", "C_设备信息")]
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
        [MyProperty("SoftWareUpdate", "C_设备信息")]
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
        [MyProperty("是否是NPI设备", "C_设备信息")]
        public bool 是否是NPI设备
        {
            get
            {
                return GetNodeValue("Is_NPI_Device", false);
            }
            set
            {
                if (!InputNumber(MultiLanguage.GetMessage("输入修改密码")))
                    return;
                SetNodeValue("Is_NPI_Device", value);
            }
        }
        [MyProperty("是否是Buffer线体", "C_设备信息")]
        private bool 是否是Buffer线体
        {
            get
            {
                return GetNodeValue("Is_Buffer_Line", false);
            }
            set
            {
                SetNodeValue("Is_Buffer_Line", value);
            }
        }
        public bool IsBufferLine()
        {
            return 是否是Buffer线体;
        }

        [MyProperty("回流线控制方式", "C_设备信息")]
        public int 回流线控制方式
        {
            get
            {
                return GetNodeValue("FlowBackLineMode", 0);
            }
            set
            {
                SetNodeValue("FlowBackLineMode", value);
            }
        }

        [MyProperty("是否是OMRON温控器", "C_设备信息")]
        public bool 是否是OMRON温控器
        {
            get
            {
                return GetNodeValue("Is_OMRON_TempControl", false);
            }
            set
            {
                if (!InputNumber(MultiLanguage.GetMessage("输入修改密码")))
                    return;
                SetNodeValue("Is_OMRON_TempControl", value);
            }
        }
        [MyProperty("吸嘴1物料名称", "C_设备信息")]
        public string 吸嘴1物料名称
        {
            get
            {
                return GetNodeValue("Nozzle1_MaterialName", "WBAR");
            }
            set
            {
                SetNodeValue("Nozzle1_MaterialName", value);
            }
        }
        [MyProperty("吸嘴2物料名称", "C_设备信息")]
        public string 吸嘴2物料名称
        {
            get
            {
                return GetNodeValue("Nozzle2_MaterialName", "FCM");
            }
            set
            {
                SetNodeValue("Nozzle2_MaterialName", value);
            }
        }
        [MyProperty("吸嘴3物料名称", "C_设备信息")]
        public string 吸嘴3物料名称
        {
            get
            {
                return GetNodeValue("Nozzle3_MaterialName", "SBCM");
            }
            set
            {
                SetNodeValue("Nozzle3_MaterialName", value);
            }
        }
        [MyProperty("流线与下一站联机", "C_设备信息")]
        public bool 流线与下一站联机
        {
            get
            {
                return GetNodeValue("WaitForNextStationSignal", true);
            }
            set
            {
                SetNodeValue("WaitForNextStationSignal", value);
            }
        }
        //swh added@20201113
        [MyProperty("是否开启压力报警", "C_设备信息")]
        public bool 是否开启压力报警
        {
            get
            {
                return GetNodeValue("Is_PressAlarm", false);
            }
            set
            {
                if (!InputNumber(MultiLanguage.GetMessage("输入修改密码")))
                    return;
                SetNodeValue("Is_PressAlarm", value);
            }
        }
        //swh added@20201113
        [MyProperty("是否开启温度报警", "C_设备信息")]
        public bool 是否开启温度报警
        {
            get
            {
                return GetNodeValue("Is_TempAlarm", false);
            }
            set
            {
                if (!InputNumber(MultiLanguage.GetMessage("输入修改密码")))
                    return;
                SetNodeValue("Is_TempAlarm", value);
            }
        }
        //swh added @20201112
        //[MyProperty("压力值_压力限制_温度_温度限制", "C_设备信息"), ReadOnly(true)]
        //public double[] 压力值_压力限制_温度_温度限制
        //{
        //    get { return XConvert.Str2DoubleG(settingMap["TempAndPressure"], ','); }
        //    set { settingMap["TempAndPressure"] = XConvert.DoubleG2Str(value, ","); }
        //}

        //***********D_设备信息**************//
        #endregion

        #region D_数据上传
        //***********D_数据上传**************//
        [MyProperty("A是否开启PDCA", "D_数据上传")]
        public bool A是否开启PDCA
        {
            get
            {
                return GetNodeValue("IsOpenPDCA", true);
            }
            set
            {
                if (!InputNumber(MultiLanguage.GetMessage("输入修改密码")))
                    return;
                if (!Globals.SettingOption.B是否开启MES && value)
                {
                    BzMessagebox.Show(MultiLanguage.GetMessage("未开启MES"));
                    return;
                }
                SetNodeValue("IsOpenPDCA", value);
            }
        }
        [MyProperty("A是否开启PDCA打开提示功能", "D_数据上传")]
        public bool A是否开启PDCA打开提示功能
        {
            get
            {
                return GetNodeValue("IsWarnPDCAOpen", true);
            }
            set
            {
                SetNodeValue("IsWarnPDCAOpen", value);
            }
        }
        [MyProperty("B是否开启MES", "D_数据上传")]
        public bool B是否开启MES
        {
            get
            {
                return GetNodeValue("IsOpenMES", true);
            }
            set
            {
                if (!InputNumber(MultiLanguage.GetMessage("输入修改密码")))
                    return;
                if (!Globals.SettingOption.是否开启载具SN扫描 && value)
                {
                    BzMessagebox.Show(MultiLanguage.GetMessage("未开启扫描载具SN"));
                    //MessageBox.Show("未开启扫描载具SN");
                    return;
                }
                SetNodeValue("IsOpenMES", value);
            }
        }
        [MyProperty("B是否开启MES打开提示功能", "D_数据上传")]
        public bool B是否开启MES打开提示功能
        {
            get
            {
                return GetNodeValue("IsWarnSFCOpen", true);
            }
            set
            {
                SetNodeValue("IsWarnSFCOpen", value);
            }
        }
        [MyProperty("C是否开启MES卡关", "D_数据上传")]
        public bool C是否开启MES卡关
        {
            get
            {
                return GetNodeValue("IsOpenMESCheck", true);
            }
            set
            {
                string path = Globals.Dir_Record_MESLog + "MESLog_" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
                StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
                if (是否开启载具SN扫描)
                {
                    SetNodeValue("IsOpenMESCheck", value);
                }
                else
                if (!value)
                    SetNodeValue("IsOpenMESCheck", value);
                else
                    MessageBox.Show("请开启载具扫码!");
                string str = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " => " + value.ToString();
                sw.WriteLine(str);
                sw.Dispose();
                sw.Close();
            }
        }
        [MyProperty("C是否开启MES卡关打开提示功能", "D_数据上传")]
        public bool C是否开启MES卡关打开提示功能
        {
            get
            {
                return GetNodeValue("IsWarnMESCheckOpen", true);
            }
            set
            {
                SetNodeValue("IsWarnMESCheckOpen", value);
            }
        }
        [MyProperty("D是否开启报警上传", "D_数据上传")]
        public bool 是否开启报警上传
        {
            get
            {
                return GetNodeValue("IsupdataAlarm", true);
            }
            set
            {
                SetNodeValue("IsupdataAlarm", value);
            }
        }
        [MyProperty("E是否开启抛料上传", "D_数据上传")]
        public bool 是否开启抛料上传
        {
            get
            {
                return GetNodeValue("IsupdataTossing", true);
            }
            set
            {
                SetNodeValue("IsupdataTossing", value);
            }
        }
        [MyProperty("F是否开启宕机上传", "D_数据上传")]
        public bool 是否开启宕机上传
        {
            get
            {
                return GetNodeValue("IsupdataDown", true);
            }
            set
            {
                SetNodeValue("IsupdataDown", value);
            }
        }
        [MyProperty("G是否开启设备状态上传", "D_数据上传")]
        public bool 是否开启设备状态上传
        {
            get
            {
                return GetNodeValue("IsupdataRunning", true);
            }
            set
            {
                SetNodeValue("IsupdataRunning", value);
            }
        }
        [MyProperty("PDCA_IP", "D_数据上传")]
        public string PDCA_IP
        {
            get
            {
                return GetNodeValue("Scanner3_IP", "169.254.1.10");
            }
            set
            {
                SetNodeValue("Scanner3_IP", value);
            }
        }
        [MyProperty("PDCA_Port", "D_数据上传")]
        public int PDCA_Port
        {
            get
            {
                return GetNodeValue("Scanner3_Port", 1111);
            }
            set
            {
                SetNodeValue("Scanner3_Port", value);
            }
        }
        [MyProperty("PDCA_VendorSW", "D_数据上传")]
        public string PDCA_VendorSW
        {
            get
            {
                return GetNodeValue("PDCA_VendorSW", "HB-CLM1.0.0.6");
            }
            set
            {
                SetNodeValue("PDCA_VendorSW", value);
            }

        }
        [MyProperty("PDCA_VendorSW", "D_数据上传")]
        public string PDCA_VisionSW
        {
            get
            {
                return GetNodeValue("PDCA_VisionSW", "1.6");
            }
            set
            {
                SetNodeValue("PDCA_VisionSW", value);
            }

        }
        [MyProperty("PDCA_设备编号", "D_数据上传")]
        private string PDCA_设备编号
        {
            get
            {
                return GetNodeValue("PDCA_ID", "21");
            }
            set
            {
                SetNodeValue("PDCA_ID", value);
            }
        }
        [MyProperty("PDCA_StationID", "D_数据上传")]
        public string PDCA_StationID
        {
            get
            {
                return GetNodeValue("PDCA_StationID", "S-008");
            }
            set
            {
                SetNodeValue("PDCA_StationID", value);
            }

        }
        [MyProperty("MES_AutomationAddress", "D_数据上传")]
        public string MES_AutomationAddress
        {
            get
            {
                return GetNodeValue("MES_AutomationAddress", "http://10.171.13.173/Automation_Sub/LinkSF.asmx");
            }
            set
            {
                SetNodeValue("MES_AutomationAddress", value);
            }
        }
        [MyProperty("MES_LineStopAddress", "D_数据上传")]
        public string MES_LineStopAddress
        {
            get
            {
                return GetNodeValue("MES_LineStopAddress", "http://10.171.13.173/LineStop/LineStop.asmx");
            }
            set
            {
                SetNodeValue("MES_LineStopAddress", value);
            }
        }
        [MyProperty("MES_FixID", "D_数据上传")]
        public string MES_FixID
        {
            get
            {
                return GetNodeValue("MES_FixID", "D3-3F-L1L-PNP");
            }
            set
            {
                SetNodeValue("MES_FixID", value);
            }

        }
        [MyProperty("MES_线体名称", "D_数据上传")]
        public string MES_线体名称
        {
            get
            {
                return GetNodeValue("SFC_Line_Name", "BU22-J6106");
            }
            set
            {
                SetNodeValue("SFC_Line_Name", value);
            }
        }
        [MyProperty("是否开启MES上传正式接口", "D_数据上传")]
        public bool 是否开启MES上传正式接口
        {
            get
            {
                return GetNodeValue("OpenMessNormalWebsite", false);
            }
            set
            {
                SetNodeValue("OpenMessNormalWebsite", value);
            }
        }
        [MyProperty("是否开启异常值MES上传", "D_数据上传")]
        public bool 是否开启异常值MES上传
        {
            get
            {
                return GetNodeValue("WrongValuePassToMes", true);
            }
            set
            {
                SetNodeValue("WrongValuePassToMes", value);
            }
        }
        [MyProperty("MES_工站名称", "D_数据上传")]
        public string MES_工站名称
        {
            get
            {
                return GetNodeValue("SFC_Station_Name", "Alignment 3x flex(PNP)");
            }
            set
            {
                SetNodeValue("SFC_Station_Name", value);
            }
        }
        [MyProperty("MES_工单号", "D_数据上传")]
        private string MES_工单号
        {
            get
            {
                return GetNodeValue("SFC_Line_WO", "wo111111111111");
            }
            set
            {
                SetNodeValue("SFC_Line_WO", value);
            }
        }
        [MyProperty("允许PDCA上传失败的次数", "D_数据上传")]
        public int 允许数据上传失败的次数
        {
            get
            {
                return GetNodeValue("Admit_UploadFail_Num", 5);
            }
            set
            {
                SetNodeValue("Admit_UploadFail_Num", value);
            }

        }
        [MyProperty("左工位吸嘴1SN", "D_数据上传")]
        public string 左工位吸嘴1SN
        {
            get
            {
                return GetNodeValue("L_Nozzle_1_SN", "20200531A001");
            }
            set
            {
                SetNodeValue("L_Nozzle_1_SN", value);
            }

        }
        [MyProperty("左工位吸嘴2SN", "D_数据上传")]
        public string 左工位吸嘴2SN
        {
            get
            {
                return GetNodeValue("L_Nozzle_2_SN", "20200531A001");
            }
            set
            {
                SetNodeValue("L_Nozzle_2_SN", value);
            }

        }
        [MyProperty("左工位吸嘴3SN", "D_数据上传")]
        public string 左工位吸嘴3SN
        {
            get
            {
                return GetNodeValue("L_Nozzle_3_SN", "20200531A001");
            }
            set
            {
                SetNodeValue("L_Nozzle_3_SN", value);
            }

        }
        [MyProperty("右工位吸嘴1SN", "D_数据上传")]
        public string 右工位吸嘴1SN
        {
            get
            {
                return GetNodeValue("R_Nozzle_1_SN", "20200531A001");
            }
            set
            {
                SetNodeValue("R_Nozzle_1_SN", value);
            }

        }
        [MyProperty("右工位吸嘴2SN", "D_数据上传")]
        public string 右工位吸嘴2SN
        {
            get
            {
                return GetNodeValue("R_Nozzle_2_SN", "20200531A001");
            }
            set
            {
                SetNodeValue("R_Nozzle_2_SN", value);
            }

        }
        [MyProperty("右工位吸嘴3SN", "D_数据上传")]
        public string 右工位吸嘴3SN
        {
            get
            {
                return GetNodeValue("R_Nozzle_3_SN", "20200531A001");
            }
            set
            {
                SetNodeValue("R_Nozzle_3_SN", value);
            }

        }


        #region 暂时不用
        [MyProperty("PDCA_地区", "D_数据上传")]
        private MachineDest PDCA_地区
        {
            get
            {
                return (MachineDest)GetNodeValue("PDCA_Dest", 0);
            }
            set
            {
                SetNodeValue("PDCA_Dest", (int)value);
            }
        }
        [MyProperty("PDCA_Fixtrue_ID", "D_数据上传")]
        private string PDCA_Fixtrue_ID
        {
            get
            {
                return GetNodeValue("PDCA_MachineName", "BZ-PNP");
            }
            set
            {
                SetNodeValue("PDCA_MachineName", value);
            }

        }
        [MyProperty("MES_机器序号", "D_数据上传")]
        private string MES_机器序号
        {
            get
            {
                return GetNodeValue("SFC_Machine_No", "TEST");
            }
            set
            {
                SetNodeValue("SFC_Machine_No", value);
            }

        }
        [MyProperty("SFC_上道工站名称", "D_数据上传")]
        private string SFC_上道工站名称
        {
            get
            {
                return GetNodeValue("SFC_Prev_Station_Name", "CFD");
            }
            set
            {
                SetNodeValue("SFC_Prev_Station_Name", value);
            }

        }

        #endregion
        //***********D_数据上传**************//
        #endregion

        #region E_组装配置
        //***********E_组装配置**************//
        [MyProperty("载具SN长度", "E_组装配置")]
        public int 载具SN长度
        {
            get
            {
                return GetNodeValue("CarrierSNlength", 16);
            }
            set
            {
                SetNodeValue("CarrierSNlength", value);
            }
        }
        [MyProperty("流线延时减速时间设定", "E_组装配置")]
        public int 流线延时减速时间设定
        {
            get
            {
                return GetNodeValue("FlowLineDelaySlowDownSetting", 150);
            }
            set
            {
                if (value < 1 || value > 200)
                {
                    ShowParamOverRangeError("流线延时减速时间设定", 1, 200);
                }
                else
                    SetNodeValue("FlowLineDelaySlowDownSetting", value);
            }
        }
        [MyProperty("吸嘴物料超时检查开启", "E_组装配置")]
        public bool 吸嘴物料超时检查开启
        {
            get
            {
                return GetNodeValue("IsCheckNozzleSuckTimeOutLimit", false);
            }
            set
            {
                SetNodeValue("IsCheckNozzleSuckTimeOutLimit", value);
            }
        }

        public bool IsCheckNozzleSuckTimeOutLimit()
        {
            return false;
        }

        [MyProperty("吸嘴物料超时检查时间", "E_组装配置")]
        private double 吸嘴物料超时检查时间
        {
            get
            {
                return GetNodeValue("NozzleSuckTimeOutLimit", 300);
            }
            set
            {
                SetNodeValue("NozzleSuckTimeOutLimit", value);
            }
        }
        public double NozzleSuckTimeOutLimit()
        {
            return 300;
        }

        [MyProperty("是否预先取料", "E_组装配置")]
        public bool 是否预先取料
        {
            get
            {
                return GetNodeValue("IsPrePick", false);
            }
            set
            {
                SetNodeValue("IsPrePick", value);
            }
        }
        [MyProperty("点胶超时检查时间", "E_组装配置")]
        public double 点胶超时检查时间
        {
            get
            {
                return GetNodeValue("DispenserTimeOutLimit", 2);
            }
            set
            {
                SetNodeValue("DispenserTimeOutLimit", value);
            }
        }
        [MyProperty("是否开启参数管控", "E_组装配置")]
        public bool 是否开启参数管控
        {
            get
            {
                return GetNodeValue("ParameterControl", false);
            }
            set
            {
                SetNodeValue("ParameterControl", value);
            }
        }
        [MyProperty("抛料吹气时间", "E_组装配置")]
        public int 抛料吹气时间
        {
            get
            {
                return GetNodeValue("ThrowFlexVacuumBlowTime", 1000);
            }
            set
            {
                SetNodeValue("ThrowFlexVacuumBlowTime", (int)value);
            }
        }
        [MyProperty("二次定位失败报警次数", "E_组装配置")]
        public int 二次定位失败报警次数
        {
            get
            {
                return GetNodeValue("SecondLocateFailCount", 3);
            }
            set
            {
                SetNodeValue("SecondLocateFailCount", (int)value);
            }
        }
        [MyProperty("取料失败报警次数", "E_组装配置")]
        public int 取料失败报警次数
        {
            get
            {
                return GetNodeValue("PickFlexFailCount", 0);
            }
            set
            {
                SetNodeValue("PickFlexFailCount", (int)value);
            }
        }
        //[MyProperty("Tray盘类别", "E_组装配置")]
        //public TrayId Tray盘类别
        //{
        //    get
        //    {
        //        return (TrayId)GetNodeValue("TrayId", 1);
        //    }
        //    set
        //    {
        //        SetNodeValue("TrayId", (int)value);
        //    }
        //}
        [MyProperty("上相机稳停时间", "E_组装配置")]
        public int 上相机稳停时间
        {
            get
            {
                int settingTime = GetNodeValue("UpCCDSettleTime", 50);
                if ((settingTime < 50) || (settingTime > 5000))
                {
                    settingTime = 50;
                }
                return settingTime;
            }
            set
            {
                if ((value < 50) || (value > 5000))
                {
                    value = 50;
                }
                SetNodeValue("UpCCDSettleTime", (int)value);
            }
        }
        [MyProperty("下相机稳停时间", "E_组装配置")]
        public int 下相机稳停时间
        {
            get
            {
                int settingTime = GetNodeValue("DownCCDSettleTime", 50);
                if ((settingTime < 50) || (settingTime > 5000))
                {
                    settingTime = 50;
                }
                return settingTime;
            }
            set
            {
                if ((value < 50) || (value > 5000))
                {
                    value = 50;
                }
                SetNodeValue("DownCCDSettleTime", (int)value);
            }
        }
        [MyProperty("上光源点亮时间", "E_组装配置")]
        public int 上光源点亮时间
        {
            get
            {
                int settingTime = GetNodeValue("UpLightSettleTime", 50);
                if ((settingTime < 50) || (settingTime > 5000))
                {
                    settingTime = 50;
                }
                return settingTime;
            }
            set
            {
                if ((value < 50) || (value > 5000))
                {
                    value = 50;
                }
                SetNodeValue("UpLightSettleTime", (int)value);
            }
        }
        [MyProperty("下光源点亮时间", "E_组装配置")]
        public int 下光源点亮时间
        {
            get
            {
                int settingTime = GetNodeValue("DownLightSettleTime", 50);
                if ((settingTime < 50) || (settingTime > 5000))
                {
                    settingTime = 50;
                }
                return settingTime;
            }
            set
            {
                if ((value < 50) || (value > 5000))
                {
                    value = 50;
                }
                SetNodeValue("DownLightSettleTime", (int)value);
            }
        }
        [MyProperty("组装保压时间", "E_组装配置"), ReadOnly(true)]
        public int 组装保压时间
        {
            get
            {
                int settingTime = GetNodeValue("AssembleHoldTime", 250);
                if ((settingTime < 50) || (settingTime > 1000))
                {
                    settingTime = 250;
                }
                return settingTime;
            }
            set
            {
                if ((value < 50) || (value > 1000))
                {
                    value = 250;
                }
                SetNodeValue("AssembleHoldTime", (int)value);
            }
        }
        [MyProperty("吸嘴吸真空延迟时间", "E_组装配置")]
        public int 吸嘴吸真空延迟时间
        {
            get
            {
                int settingTime = GetNodeValue("NozzleHoldDelayTime", 100);
                if ((settingTime < 100) || (settingTime > 1000))
                {
                    settingTime = 100;
                }
                return settingTime;
            }
            set
            {
                if ((value < 100) || (value > 1000))
                {
                    value = 100;
                }
                SetNodeValue("NozzleHoldDelayTime", (int)value);
            }
        }
        [MyProperty("组装真空破时间", "E_组装配置")]
        public int 组装真空破时间
        {
            get
            {
                int settingTime = GetNodeValue("VacuumBlowTime", 100);
                if ((settingTime < 50) || (settingTime > 200))
                {
                    settingTime = 100;
                }
                return settingTime;
            }
            set
            {
                if ((value < 50) || (value > 200))
                {
                    value = 100;
                }
                SetNodeValue("VacuumBlowTime", (int)value);
            }
        }
        [MyProperty("是否开启载具SN扫描", "E_组装配置")]
        public bool 是否开启载具SN扫描
        {
            get
            {
                return GetNodeValue("IsOpenSNScan", true);
            }
            set
            {
                if (!value)
                {
                    B是否开启MES = false;
                    A是否开启PDCA = false;
                    C是否开启MES卡关 = false;
                }
                SetNodeValue("IsOpenSNScan", value);
            }
        }
        [MyProperty("是否开启点胶检测", "E_组装配置")]
        private bool 是否开启点胶检测
        {
            get
            {
                return GetNodeValue("IsOpenCarrierTimeOut", true);
            }
            set
            {
                SetNodeValue("IsOpenCarrierTimeOut", value);
            }
        }
        [MyProperty("是否开启上下设备联机", "E_组装配置")]
        public bool 是否开启上下设备联机
        {
            get
            {
                return GetNodeValue("ConnectionByWriteFile", false);
            }
            set
            {
                SetNodeValue("ConnectionByWriteFile", value);
            }
        }
        [MyProperty("点胶机用户名", "E_组装配置")]
        public string 点胶机用户名
        {
            get
            {
                return GetNodeValue("DispenserUser", "Anson");
            }
            set
            {
                SetNodeValue("DispenserUser", value);
            }
        }
        [MyProperty("点胶机密码", "E_组装配置")]
        public string 点胶机密码
        {
            get
            {
                return GetNodeValue("DispenserPassword", "anson123");
            }
            set
            {
                SetNodeValue("DispenserPassword", value);
            }
        }
        [MyProperty("穴位分配模式", "E_组装配置")]
        private int 穴位分配模式
        {
            get
            {
                return GetNodeValue("CellAssignMode", 0);
            }
            set
            {
                SetNodeValue("CellAssignMode", value);
            }
        }
        [MyProperty("取料判断逻辑", "E_组装配置")]
        public AndOr 取料判断逻辑
        {
            get
            {
                return (AndOr)GetNodeValue("NozzlePickLogic", 1);
            }
            set
            {
                SetNodeValue("NozzlePickLogic", (int)value);
            }
        }
        [MyProperty("贴合顺序", "E_组装配置")]
        public AttachSeq 贴合顺序
        {
            get
            {
                return (AttachSeq)GetNodeValue("AttachSequence", 1);
            }
            set
            {
                SetNodeValue("AttachSequence", (int)value);
            }
        }
        [MyProperty("回流线进载延时", "E_组装配置")]
        public int 回流线进载延时
        {
            get
            {
                return GetNodeValue("FlowBackInDelay", 0);
            }
            set
            {
                if (value < 1 || value > 200)
                {
                    ShowParamOverRangeError("回流线进载延时", 1, 200);
                }
                else
                    SetNodeValue("FlowBackInDelay", value);
            }
        }
        [MyProperty("回流线出载延时", "E_组装配置")]
        public int 回流线出载延时
        {
            get
            {
                return GetNodeValue("FlowBackOutDelay", 200);
            }
            set
            {
                if (value < 1 || value > 200)
                {
                    ShowParamOverRangeError("回流线出载延时", 1, 200);
                }
                else
                    SetNodeValue("FlowBackOutDelay", value);
            }
        }
        [MyProperty("温度参数管控设置", "E_组装配置"), ReadOnly(true)]
        public int 温度参数管控设置
        {
            get
            {
                return GetNodeValue("TempParameterControlTest", 120);
            }
            set
            {
                SetNodeValue("TempParameterControlTest", value);
            }
        }
        [MyProperty("压力参数管控设置", "E_组装配置"), ReadOnly(true)]
        public double 压力参数管控设置
        {
            get
            {
                return GetNodeValue("ForceParameterControlTest", 1.0);
            }
            set
            {
                SetNodeValue("ForceParameterControlTest", value);
            }
        }
        #region 隐藏
        //[MyProperty("Carrier盘类别", "F_组装配置")]
        //private CarrierId Carrier盘类别
        //{
        //    get
        //    {
        //        return (CarrierId)GetNodeValue("CarrierId", 1);
        //    }
        //    set
        //    {
        //        SetNodeValue("CarrierId", (int)value);
        //    }
        //}

        //public CarrierId GetSettingCarrierID()
        //{
        //    return Carrier盘类别;
        //}

        //[MyProperty("是否是DOE物料", "F_组装配置")]
        //private bool 是否是DOE物料
        //{
        //    get
        //    {
        //        return GetNodeValue("Is_DOE_PCB", false);
        //    }
        //    set
        //    {
        //        SetNodeValue("Is_DOE_PCB", value);
        //    }
        //}

        public bool GetSettingDOEPCB()
        {
            //return 是否是DOE物料;
            return true;
        }

        [MyProperty("是否开启上下接驳通信", "F_组装配置")]
        private bool 是否开启上下接驳通信
        {
            get
            {
                return GetNodeValue("IsEnablePrevNextComm", true);
            }
            set
            {
                SetNodeValue("IsEnablePrevNextComm", value);
            }
        }

        [MyProperty("Flex最大连续定位失败个数", "F_组装配置")]
        private int Flex最大连续定位失败个数
        {
            get
            {
                return GetNodeValue("CycloneMaxLocFailCnt", 3);
            }
            set
            {
                if (value < 3 || value > 10)
                    ShowParamOverRangeError("Cyclone最大连续定位失败个数", 3, 10);
                //BzMessageBox.Show("Cyclone最大连续定位失败个数：3~10", "超出参数设置范围", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    SetNodeValue("CycloneMaxLocFailCnt", value);
            }
        }

        [MyProperty("组装配置Flex定位失败后重拍", "F_组装配置")]
        private bool 组装配置Flex定位失败后重拍
        {
            get
            {
                return GetNodeValue("CycloneLocRetry", false);
            }
            set
            {
                SetNodeValue("CycloneLocRetry", value);
            }
        }

        [MyProperty("Flex扫码失败后是否重扫", "F_组装配置")]
        private bool Flex扫码失败后是否重扫
        {
            get
            {
                return GetNodeValue("RetryScanCycloneBarcode", false);
            }
            set
            {
                SetNodeValue("RetryScanCycloneBarcode", value);
            }
        }

        [MyProperty("是否开启Cyclone扫码失败抛料", "F_组装配置")]
        private bool 是否开启Cyclone扫码失败抛料
        {
            get
            {
                return GetNodeValue("IsEnableTosseForScanFail", true);
            }
            set
            {
                SetNodeValue("IsEnableTosseForScanFail", value);
            }
        }

        [MyProperty("是否开启LB摆放位置超出范围抛料", "F_组装配置")]
        private bool 是否开启LB摆放位置超出范围抛料
        {
            get
            {
                return GetNodeValue("IsEnableTosseForLBPosOverRange", false);
            }
            set
            {
                SetNodeValue("IsEnableTosseForLBPosOverRange", value);
            }
        }

        [MyProperty("PCB摆放位置范围设定", "F_组装配置")]
        private double PCB摆放位置范围设定
        {
            get
            {
                return GetNodeValue("LBPlacePosRange", 0.5);
            }
            set
            {
                SetNodeValue("LBPlacePosRange", value);
            }
        }

        [MyProperty("PCB定位基准值X", "F_组装配置")]
        private double PCB定位基准值X
        {
            get
            {
                return GetNodeValue("LBPlaceX", 2.8);
            }
            set
            {
                SetNodeValue("LBPlaceX", value);
            }
        }

        [MyProperty("PCB定位基准值Y", "F_组装配置")]
        private double PCB定位基准值Y
        {
            get
            {
                return GetNodeValue("LBPlaceY", -0.8);
            }
            set
            {
                SetNodeValue("LBPlaceY", value);
            }
        }

        [MyProperty("是否开启Flex取料自动补齐", "F_组装配置")]
        private bool 是否开启Flex取料自动补齐
        {
            get
            {
                return GetNodeValue("EnableAutoFillNozzles", true);
            }
            set
            {
                SetNodeValue("EnableAutoFillNozzles", value);
            }
        }

        [MyProperty("是否开启复位后检查吸嘴和载具托盘位置", "F_组装配置")]
        private bool 是否开启复位后检查吸嘴和载具托盘位置
        {
            get
            {
                return GetNodeValue("EnableCheckAbsPosAfterHoming", true);
            }
            set
            {
                SetNodeValue("EnableCheckAbsPosAfterHoming", value);
            }
        }

        [MyProperty("是否开启FX传输数据", "F_组装配置")]
        private bool 是否开启FX传输数据
        {
            get
            {
                return GetNodeValue("IsSendStrToFX", true);
            }
            set
            {
                SetNodeValue("IsSendStrToFX", value);
            }
        }

        #endregion

        //***********E_组装配置**************//
        #endregion

        #region F_防呆设置

        [MyProperty("是否开启抛料后检查真空吸", "F_防呆设置")]
        public bool 是否开启抛料后检查真空吸
        {
            get
            {
                return GetNodeValue("CheckVacuumAfterThrownFlex", true);
            }
            set
            {
                SetNodeValue("CheckVacuumAfterThrownFlex", value);
            }
        }

        [MyProperty("是否开启抛料后吸嘴拍照", "F_防呆设置")]
        public bool 是否开启抛料后吸嘴拍照
        {
            get
            {
                return GetNodeValue("CheckNozzleCCDAfterThrownFlex", true);
            }
            set
            {
                SetNodeValue("CheckNozzleCCDAfterThrownFlex", value);
            }
        }


        [MyProperty("贴合完成后检查吸嘴真空吸", "F_防呆设置")]
        public bool 贴合完成后检查吸嘴真空吸
        {
            get
            {
                return GetNodeValue("CheckNozzleVacuum", false);
            }
            set
            {
                SetNodeValue("CheckNozzleVacuum", value);
            }
        }

        [MyProperty("贴合完成后检查吸嘴时间", "F_防呆设置")]
        public double 贴合完成后检查吸嘴时间
        {
            get
            {
                return GetNodeValue("CheckNozzleVacuumTime", 200.00);
            }
            set
            {
                SetNodeValue("CheckNozzleVacuumTime", value);
            }
        }

        [MyProperty("吸嘴老化检测开启", "F_防呆设置")]
        public bool 吸嘴老化检测开启
        {
            get
            {
                return GetNodeValue("NozzleAttachCheck", false);
            }
            set
            {
                SetNodeValue("NozzleAttachCheck", value);
            }
        }
        [MyProperty("吸嘴老化检测次数", "F_防呆设置"), ReadOnly(true)]
        public int 吸嘴老化检测次数
        {
            get
            {
                return GetNodeValue("NozzleAttachTimes", 100000);
            }
            set
            {
                SetNodeValue("NozzleAttachTimes", value);
            }
        }
        [MyProperty("贴合过程中定时吹气排助焊剂", "F_防呆设置")]
        public bool 贴合过程中定时吹气排助焊剂
        {
            get
            {
                return GetNodeValue("VacuumOnAfterTimeOut", false);
            }
            set
            {
                SetNodeValue("VacuumOnAfterTimeOut", value);
            }
        }
        [MyProperty("贴合过程中定时吹气间隔时间", "F_防呆设置")]
        public double 贴合过程中定时吹气间隔时间
        {
            get
            {
                return GetNodeValue("VacuumOnTimeInterval", 2.00);
            }
            set
            {
                SetNodeValue("VacuumOnTimeInterval", value);
            }
        }
        [MyProperty("贴合过程中定时吹气时间", "F_防呆设置")]
        public double 贴合过程中定时吹气时间
        {
            get
            {
                return GetNodeValue("VacuumOnTime", 60.00);
            }
            set
            {
                SetNodeValue("VacuumOnTime", value);
            }
        }
        [MyProperty("左机吸嘴1主气路", "F_防呆设置")]
        public MasterSuckerNum 左机吸嘴1主气路
        {
            get
            {
                return (MasterSuckerNum)GetNodeValue("LeftNozzle1Master", 1);
            }
            set
            {
                SetNodeValue("LeftNozzle1Master", (int)value);
            }
        }

        [MyProperty("左机吸嘴2主气路", "F_防呆设置")]
        public MasterSuckerNum 左机吸嘴2主气路
        {
            get
            {
                return (MasterSuckerNum)GetNodeValue("LeftNozzle2Master", 1);
            }
            set
            {
                SetNodeValue("LeftNozzle2Master", (int)value);
            }
        }

        [MyProperty("左机吸嘴3主气路", "F_防呆设置")]
        public MasterSuckerNum 左机吸嘴3主气路
        {
            get
            {
                return (MasterSuckerNum)GetNodeValue("LeftNozzle3Master", 1);
            }
            set
            {
                SetNodeValue("LeftNozzle3Master", (int)value);
            }
        }

        [MyProperty("右机吸嘴1主气路", "F_防呆设置")]
        public MasterSuckerNum 右机吸嘴1主气路
        {
            get
            {
                return (MasterSuckerNum)GetNodeValue("RightNozzle1Master", 1);
            }
            set
            {
                SetNodeValue("RightNozzle1Master", (int)value);
            }
        }

        [MyProperty("右机吸嘴2主气路", "F_防呆设置")]
        public MasterSuckerNum 右机吸嘴2主气路
        {
            get
            {
                return (MasterSuckerNum)GetNodeValue("RightNozzle2Master", 1);
            }
            set
            {
                SetNodeValue("RightNozzle2Master", (int)value);
            }
        }

        [MyProperty("右机吸嘴3主气路", "F_防呆设置")]
        public MasterSuckerNum 右机吸嘴3主气路
        {
            get
            {
                return (MasterSuckerNum)GetNodeValue("RightNozzle3Master", 1);
            }
            set
            {
                SetNodeValue("RightNozzle3Master", (int)value);
            }
        }
        #endregion
        #region I_吸嘴使能
        [MyProperty("吸嘴1使能", "I_吸嘴使能")]
        public bool 吸嘴1使能
        {
            get
            {
                return GetNodeValue("Nozzle_Enable_1", true);
            }
            set
            {
                SetNodeValue("Nozzle_Enable_1", value);
            }
        }

        [MyProperty("吸嘴2使能", "I_吸嘴使能")]
        public bool 吸嘴2使能
        {
            get
            {
                return GetNodeValue("Nozzle_Enable_2", true);
            }
            set
            {
                SetNodeValue("Nozzle_Enable_2", value);
            }
        }

        [MyProperty("吸嘴3使能", "I_吸嘴使能")]
        public bool 吸嘴3使能
        {
            get
            {
                return GetNodeValue("Nozzle_Enable_3", true);
            }
            set
            {
                SetNodeValue("Nozzle_Enable_3", value);
            }
        }
        #endregion
        #region SPC管控
        [MyProperty("SPC使能", "G_SPC管控")]
        public bool SPC使能
        {
            get
            {
                return GetNodeValue("IsopenSPC", true);
            }
            set
            {
                SetNodeValue("IsopenSPC", value);
            }
        }
        [MyProperty("SPC管控软件地址", "G_SPC管控")]
        public string SFC管控软件地址
        {
            get
            {
                return GetNodeValue("SPCsoftwarePath", @"D:\SPC\net6.0\FoxCSVProcesser.exe");
            }
            set
            {
                SetNodeValue("SPCsoftwarePath", value);
            }
        }
        [MyProperty("SPC管控文件地址", "G_SPC管控")]
        public string SPC管控文件地址
        {
            get
            {
                return GetNodeValue("SPCFliePath", @"D:\SPC\read\");
            }
            set
            {
                SetNodeValue("SPCFliePath", value);
            }
        }
        [MyProperty("SPC扫描时间", "G_SPC管控")]
        public int SPC扫描时间
        {
            get
            {
                return GetNodeValue("SPCwaitTime", 300);
            }
            set
            {
                SetNodeValue("SPCwaitTime", value);
            }
        }
        [MyProperty("SPC温度文件名格式", "G_SPC管控")]
        public string SPC温度文件名格式
        {
            get
            {
                return GetNodeValue("SPCTempFormat", "N207B_ITBG_BU22-V3020_LA4-Alignment 3x flex(PNP)_01_STATION3_Nozzle_Temp");
            }
            set
            {
                SetNodeValue("SPCTempFormat", value);
            }
        }
        [MyProperty("SPC压力文件名格式", "G_SPC管控")]
        public string SPC压力文件名格式
        {
            get
            {
                return GetNodeValue("SPCPressFormat", "N207B_ITBG_BU22-V3020_LA4-Alignment 3x flex(PNP)_01_STATION3_Nozzle_Press");
            }
            set
            {
                SetNodeValue("SPCPressFormat", value);
            }
        }
        [MyProperty("SPC温度stationID", "G_SPC管控")]
        public string SPC温度stationID
        {
            get
            {
                return GetNodeValue("SPCTempstationID", "LA4_L1_PNP_Nozzle_Temp");
            }
            set
            {
                SetNodeValue("SPCTempstationID", value);
            }
        }
        [MyProperty("SPC压力stationID", "G_SPC管控")]
        public string SPC压力stationID
        {
            get
            {
                return GetNodeValue("SPCPressstationID", "LA4_L1_PNP_Nozzle_Press");
            }
            set
            {
                SetNodeValue("SPCPressstationID", value);
            }
        }
        #endregion
        public int GetStationCellAssignMode()
        {
            return 穴位分配模式;
        }
        public string GetSoftVersion()
        {
            string str = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            var arrVersion = str.Split('.');
            DateTime dt1 = Convert.ToDateTime("2000-01-01");
            DateTime result = dt1.AddDays(Convert.ToInt64(arrVersion[2]));
            string[] str1 = new string[2];
            if (当前软件版本号_版本[1] != result.ToString("yyMMdd"))
            {
                str1[0] = (Convert.ToInt32(当前软件版本号_版本[0]) + 1).ToString();
                str1[1] = result.ToString("yyMMdd");
                历史软件版本号_版本 = 当前软件版本号_版本;
                当前软件版本号_版本 = str1;
            }
            string str2 = 软件版本.Replace("a", 当前软件版本号_版本[0]);
            return str2.Replace("c", 当前软件版本号_版本[1]);
        }
        public string GetoldSoftVersion()
        {
            string str2 = 软件版本.Replace("a", 历史软件版本号_版本[0]);
            return str2.Replace("c", 历史软件版本号_版本[1]);
        }
        public void SetStationCellAssignMode(int mode)
        {
            穴位分配模式 = mode;
        }
        protected bool InputNumber(string szMsg)
        {
            //FailTip ft = new FailTip(szMsg);
            //ft.SelectAll();
            //ft.SetSubmitText(MultiLanguage.GetMessage("确认"));
            //ft.SetCancelText(MultiLanguage.GetMessage("退出"));
            //ft.ShowDialog();
            //ft.WaitOne();
            //if (ft.DialogResult == System.Windows.Forms.DialogResult.OK)
            //{
            //    try
            //    {
            //        if (ft.GetText() == DateTime.Now.ToString("yyyyMMdd"))
            //        {
            //            return true;
            //        }
            //        else
            //        {
            //            BzMessagebox.Show(MultiLanguage.GetMessage("输入密码不正确") + "!", MultiLanguage.GetMessage("错误"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return false;
            //        }
            //    }
            //    catch
            //    {
            //        BzMessagebox.Show(MultiLanguage.GetMessage("输入密码不正确") + "!", MultiLanguage.GetMessage("错误"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return false;
            //    }
            //}
            //else
            return false;
        }
    }
}
