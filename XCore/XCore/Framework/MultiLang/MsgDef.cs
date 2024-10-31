using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace XCore
{
    public enum MsgDef
    {   
        None = 0,
        [OtherLang("Option")]
        选项,
        [OtherLang("Category")]
        类别,
        [OtherLang("Notice")]
        提示,
        [OtherLang("Alarm")]
        报警,
        [OtherLang("Help")]
        帮助,
        [OtherLang("Error")]
        错误,
        [OtherLang("Left Assemble Params")]
        左组装参数,
        [OtherLang("Right Assemble Params")]
        右组装参数,
        [OtherLang("Input parameter not correct")]
        输入参数不正确,
        [OtherLang("ContinuousMove")]
        连续运动,
        [OtherLang("RelativeMove")]
        寸动,
        [OtherLang("Slow")]
        慢,
        [OtherLang("Fast")]
        快,
        [OtherLang("Nozzle")]
        吸嘴,
        [OtherLang("LeftAssembleTray1")]
        左组装Tray1,
        [OtherLang("LeftAssembleTray2")]
        左组装Tray2,
        [OtherLang("RightAssembleTray1")]
        右组装Tray1,
        [OtherLang("RightAssembleTray2")]
        右组装Tray2,
        [OtherLang("Asm Cave1 Offset")]
        组装穴位1补偿值,
        [OtherLang("Asm Cave2 Offset")]
        组装穴位2补偿值,
        [OtherLang("Asm Cave3 Offset")]
        组装穴位3补偿值,
        [OtherLang("Asm Params")]
        组装参数,
        [OtherLang("Mat Supply Vel Ratio")]
        支架供料_速度百分比,
        [OtherLang("Initialized")]
        已初始化,
        [OtherLang("Uninitialized")]
        未初始化,
        [OtherLang("Input non numeric characters")]
        输入非数字字符,
        [OtherLang("Home Done")]
        回零完成,
        [OtherLang("Home Fail")]
        回零失败,
        [OtherLang("Normal Channel")]
        常规通道,
        [OtherLang("Extend Channel")]
        扩展通道,
        [OtherLang("Channel Pos")]
        通道位置,
        [OtherLang("Channel Id")]
        通道号,
        [OtherLang("Actual")]
        实际,
        [OtherLang("Input Name")]
        输入点名称,
        [OtherLang("Card Id Mismatch With Name")]
        卡号和卡名称不对应,
        [OtherLang("Output Name")]
        输出点名称,
        [OtherLang("Wait Signal Timeout")]
        等待信号超时,
        [OtherLang("Input format wrong")]
        输入格式不正确,
        [OtherLang("Please input again")]
        请重新输入,
        [OtherLang("Retry")]
        重试,
        [OtherLang("Ignore")]
        忽略,
        [OtherLang("Emergency Stop")]
        紧急停止,
        [OtherLang("Machine Error")]
        设备报错,
        [OtherLang("Error Code")]
        错误码,
        [OtherLang("Error Info")]
        错误信息,
        [OtherLang("Safe Door Triggered")]
        触发安全门限,
        [OtherLang("Curtain Triggered")]
        触发光幕,
        [OtherLang("Motor Enable Failed")]
        轴使能失败,
        [OtherLang("Motor Stop Abnormal")]
        轴异常停止,
        [OtherLang("Servo Alarm")]
        轴伺服报警,
        [OtherLang("Motor Pos Limit Triggered")]
        轴触发正极限,
        [OtherLang("Motor Neg Limit Triggered")]
        轴触发负极限,
        [OtherLang("Motor Pos Error Over Range")]
        轴跟随误差过大,
        [OtherLang("Motion Card Init Fail")]
        板卡初始化失败,
        [OtherLang("Motion Card Load Param Fail")]
        板卡加载参数失败,
        [OtherLang("MC Air Pressure Low")]
        正气压不足,
        [OtherLang("Param Exception")]
        参数异常,
        [OtherLang("CCD Exception")]
        相机出现异常,
        [OtherLang("CCD Error")]
        相机错误,
        [OtherLang("CCD Data Error")]
        相机数据错误,
        [OtherLang("CCD Data Over Range Error")]
        CCD数据超出正常值过多,
        [OtherLang("Check Image And Select If To Tosse PCB")]
        检查图像并选择是否要抛PCB,
        [OtherLang("Check Image And Select If To Tosse Flex")]
        检查图像并选择是否要抛Flex,
        [OtherLang("Motion Over Time Error")]
        运动超时错误,
        [OtherLang("Motion Error")]
        马达运动错误,
        [OtherLang("ACS Connect Error")]
        Acs通信错误,
        [OtherLang("Flow Line Program Error")]
        运行流水程序异常,
    }
}
