using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace XCore
{
    public enum DiId
    {
        [OtherLang("L_Asm R1 Vacuum1 Snr")]
        左贴装R1真空1到位 = 0,
        [OtherLang("L_Asm R2 Vacuum1 Snr")]
        左贴装R2真空1到位,
        [OtherLang("L_Asm R3 Vacuum1 Snr")]
        左贴装R3真空1到位,
        [OtherLang("L_Asm Trans Vacuum Snr")]
        左贴装中转到位,
        [OtherLang("R_Asm R1 Vacuum1 Snr")]
        右贴装R1真空1到位,
        [OtherLang("R_Asm R2 Vacuum1 Snr")]
        右贴装R2真空1到位,
        [OtherLang("R_Asm R3 Vacuum1 Snr")]
        右贴装R3真空1到位,
        [OtherLang("R_Asm Trans Vacuum Snr")]
        右贴装中转到位,
        [OtherLang("L_Asm R1 Vacuum2 Snr")]
        左贴装R1真空2到位,
        [OtherLang("L_Asm R2 Vacuum2 Snr")]
        左贴装R2真空2到位,
        [OtherLang("L_Asm R3 Vacuum2 Snr")]
        左贴装R3真空2到位,
        [OtherLang("R_Asm R1 Vacuum2 Snr")]
        右贴装R1真空2到位,
        [OtherLang("R_Asm R2 Vacuum2 Snr")]
        右贴装R2真空2到位,
        [OtherLang("R_Asm R3 Vacuum2 Snr")]
        右贴装R3真空2到位,
        [OtherLang("Reserved_DI_8_1_6")]
        备用DI_8_1_6,
        [OtherLang("Reserved_DI_8_1_7")]
        备用DI_8_1_7,
        [OtherLang("Main EMRG Stop")]
        主设备急停,
        [OtherLang("Start Btn")]
        启动按钮,
        [OtherLang("Stop Btn")]
        停止按钮,
        [OtherLang("Reset Btn")]
        复位按钮,
        [OtherLang("Next Mc Ready")]
        下一台可以接收信号,
        [OtherLang("Prev Mc Carrier Exist")]
        上一台有载具信号,
        [OtherLang("Prev Mc Failure")]
        上一台已装配失败信号,
        [OtherLang("Air Tank Pressure")]
        储气罐压力侦测,
        [OtherLang("Reserved_DI_0_0_8")]
        备用DI_0_0_8,
        [OtherLang("L_Asm Jack Vacuum Snr")]
        左贴装顶升真空吸到位,
        [OtherLang("R_Asm Jack Vacuum Snr")]
        右贴装顶升真空吸到位,
        [OtherLang("Reserved_DI_0_0_11")]
        备用DI_0_0_11,
        [OtherLang("Reserved_DI_0_0_12")]
        备用DI_0_0_12,
        [OtherLang("Reserved_DI_0_0_13")]
        备用DI_0_0_13,
        [OtherLang("Reserved_DI_0_0_14")]
        备用DI_0_0_14,
        [OtherLang("Reserved_DI_0_0_15")]
        备用DI_0_0_15,
        [OtherLang("Carrier In PHO Snr1")]
        进载对射1,
        [OtherLang("Carrier In PHO Snr2")]
        进载对射2,
        [OtherLang("Carrier In PHO Snr3")]
        进载对射3,
        [OtherLang("Carrier In Block Home Pt")]
        进载阻挡原点,
        [OtherLang("Carrier In Block Work Pt")]
        进载阻挡动点,
        [OtherLang("Carrier L_Asm PHO Snr1")]
        左贴装对射1,
        [OtherLang("Carrier L_Asm PHO Snr2")]
        左贴装对射2,
        [OtherLang("Carrier L_Asm Block Home Pt")]
        左贴装阻挡原点,
        [OtherLang("Carrier L_Asm Block Work Pt")]
        左贴装阻挡动点,
        [OtherLang("L_Asm Jack Fall Check")]
        左贴装顶升下降到位,
        [OtherLang("L_Asm Jack Rise Check")]
        左贴装顶升上升到位,
        [OtherLang("Transit PHO Snr1")]
        过渡对射1,
        [OtherLang("Transit PHO Snr2")]
        过渡对射2,
        [OtherLang("Transit In Block Home Pt")]
        过渡阻挡原点,
        [OtherLang("Transit In Block Work Pt")]
        过渡阻挡动点,
        [OtherLang("Carrier R_Asm PHO Snr1")]
        右贴装对射1,
        [OtherLang("Carrier R_Asm PHO Snr2")]
        右贴装对射2,
        [OtherLang("Carrier R_Asm Block Home Pt")]
        右贴装阻挡原点,
        [OtherLang("Carrier R_Asm Block Work Pt")]
        右贴装阻挡动点,
        [OtherLang("R_Asm Jack Fall Check")]
        右贴装顶升下降到位,
        [OtherLang("R_Asm Jack Rise Check")]
        右贴装顶升上升到位,
        [OtherLang("Carrier Out PHO Snr1")]
        出载对射1,
        [OtherLang("Carrier Out PHO Snr2")]
        出载对射2,
        [OtherLang("Reserved_DI_0_2_7")]
        备用DI_0_2_7,
        [OtherLang("Reserved_DI_0_2_8")]
        备用DI_0_2_8,
        [OtherLang("L_PrePick1 PHO Snr1")]
        左预取料1对射1,
        [OtherLang("L_PrePick1 PHO Snr2")]
        左预取料1对射2,
        [OtherLang("Reserved_DI_0_2_11")]
        备用DI_0_2_11,
        [OtherLang("Reserved_DI_0_2_12")]
        备用DI_0_2_12,
        [OtherLang("Reserved_DI_0_2_13")]
        备用DI_0_2_13,
        [OtherLang("Reserved_DI_0_2_14")]
        备用DI_0_2_14,
        [OtherLang("L_PrePick1 Back Pt Check")]
        左预取料1送回位置检测,
        [OtherLang("Reserved_DI_0_3_0")]
        备用DI_0_3_0,
        [OtherLang("Reserved_DI_0_3_1")]
        备用DI_0_3_1,
        [OtherLang("Reserved_DI_0_3_2")]
        备用DI_0_3_2,
        [OtherLang("L_PrePick1 Vacuum Snr")]
        左预取料1真空吸到位,
        [OtherLang("L_PrePick1 Jack Rise Check")]
        左预取料1顶升上升到位,
        [OtherLang("L_PrePick1 Jack Fall Check")]
        左预取料1顶升下降到位,
        [OtherLang("L_PrePick2 PHO Snr1")]
        左预取料2对射1,
        [OtherLang("L_PrePick2 PHO Snr2")]
        左预取料2对射2,
        [OtherLang("Reserved_DI_0_3_8")]
        备用DI_0_3_8,
        [OtherLang("Reserved_DI_0_3_9")]
        备用DI_0_3_9,
        [OtherLang("Reserved_DI_0_3_10")]
        备用DI_0_3_10,
        [OtherLang("Reserved_DI_0_3_11")]
        备用DI_0_3_11,
        [OtherLang("L_PrePick2 Back Pt Check")]
        左预取料2送回位置检测,
        [OtherLang("Reserved_DI_0_3_13")]
        备用DI_0_3_13,
        [OtherLang("L_Asm Temp Ctller Sts")]
        左贴装温度控制器状态,
        [OtherLang("R_Asm Temp Ctller Sts")]
        右贴装温度控制器状态,
        [OtherLang("L_PrePick2 Vacuum Snr")]
        左预取料2真空吸到位,
        [OtherLang("L_PrePick2 Jack Rise Check")]
        左预取料2顶升上升到位,
        [OtherLang("L_PrePick2 Jack Fall Check")]
        左预取料2顶升下降到位,
        [OtherLang("R_PrePick1 PHO Snr1")]
        右预取料1对射1,
        [OtherLang("R_PrePick1 PHO Snr2")]
        右预取料1对射2,
        [OtherLang("Reserved_DI_0_4_5")]
        备用DI_0_4_5,
        [OtherLang("Reserved_DI_0_4_6")]
        备用DI_0_4_6,
        [OtherLang("Reserved_DI_0_4_7")]
        备用DI_0_4_7,
        [OtherLang("Reserved_DI_0_4_8")]
        备用DI_0_4_8,
        [OtherLang("R_PrePick1 Back Pt Check")]
        右预取料1送回位置检测,
        [OtherLang("Reserved_DI_0_4_10")]
        备用DI_0_4_10,
        [OtherLang("Reserved_DI_0_4_11")]
        备用DI_0_4_11,
        [OtherLang("Reserved_DI_0_4_12")]
        备用DI_0_4_12,
        [OtherLang("R_PrePick1 Vacuum Snr")]
        右预取料1真空吸到位,
        [OtherLang("R_PrePick1 Jack Rise Check")]
        右预取料1顶升上升到位,
        [OtherLang("R_PrePick1 Jack Fall Check")]
        右预取料1顶升下降到位,
        [OtherLang("R_PrePick2 PHO Snr1")]
        右预取料2对射1,
        [OtherLang("R_PrePick2 PHO Snr2")]
        右预取料2对射2,
        [OtherLang("Reserved_DI_0_5_2")]
        备用DI_0_5_2,
        [OtherLang("Reserved_DI_0_5_3")]
        备用DI_0_5_3,
        [OtherLang("Reserved_DI_0_5_4")]
        备用DI_0_5_4,
        [OtherLang("Reserved_DI_0_5_5")]
        备用DI_0_5_5,
        [OtherLang("R_PrePick2 Back Pt Check")]
        右预取料2送回位置检测,
        [OtherLang("Reserved_DI_0_5_7")]
        备用DI_0_5_7,
        [OtherLang("Reserved_DI_0_5_8")]
        备用DI_0_5_8,
        [OtherLang("Reserved_DI_0_5_9")]
        备用DI_0_5_9,
        [OtherLang("R_PrePick2 Vacuum Snr")]
        右预取料2真空吸到位,
        [OtherLang("R_PrePick2 Jack Rise Check")]
        右预取料2顶升上升到位,
        [OtherLang("R_PrePick2 Jack Fall Check")]
        右预取料2顶升下降到位,
        [OtherLang("Reserved_DI_0_5_13")]
        备用DI_0_5_13,
        [OtherLang("Reserved_DI_0_5_14")]
        备用DI_0_5_14,
        [OtherLang("Reserved_DI_0_5_15")]
        备用DI_0_5_15,
        [OtherLang("L_Feeder EMRG Stop")]
        左供料机急停,
        [OtherLang("Reserved_DI_1_0_1")]
        备用DI_1_0_1,
        [OtherLang("L_Feeder L_Up Bin PHO")]
        左供料机左上仓对射,
        [OtherLang("L_Feeder L_Down Bin PHO")]
        左供料机左下仓对射,
        [OtherLang("L_Feeder R_Up Bin PHO")]
        左供料机右上仓对射,
        [OtherLang("L_Feeder R_Down Bin PHO")]
        左供料机右下仓对射,
        [OtherLang("L_Feeder L_Up Bin Btn")]
        左供料机左上仓按钮,
        [OtherLang("L_Feeder L_Down Bin Btn")]
        左供料机左下仓按钮,
        [OtherLang("L_Feeder R_Up Bin Btn")]
        左供料机右上仓按钮,
        [OtherLang("L_Feeder R_Down Bin Btn")]
        左供料机右下仓按钮,
        [OtherLang("L_Feeder L_Up Bin CYL Home")]
        左供料机左上仓气缸原点,
        [OtherLang("L_Feeder L_Down Bin CYL Home")]
        左供料机左下仓气缸原点,
        [OtherLang("L_Feeder R_Up Bin CYL Home")]
        左供料机右上仓气缸原点,
        [OtherLang("L_Feeder R_Down Bin CYL Home")]
        左供料机右下仓气缸原点,
        [OtherLang("L_Feeder L_Up Bin CYL Work")]
        左供料机左上仓气缸动点,
        [OtherLang("L_Feeder L_Down Bin CYL Work")]
        左供料机左下仓气缸动点,
        [OtherLang("L_Feeder R_Up Bin CYL Work")]
        左供料机右上仓气缸动点,
        [OtherLang("L_Feeder R_Down Bin CYL Work")]
        左供料机右下仓气缸动点,
        [OtherLang("L_Feeder L_Up Bin Safe Door")]
        左供料机左上仓门禁,
        [OtherLang("L_Feeder L_Down Bin Safe Door")]
        左供料机左下仓门禁,
        [OtherLang("L_Feeder R_Up Bin Safe Door")]
        左供料机右上仓门禁,
        [OtherLang("L_Feeder R_Down Bin Safe Door")]
        左供料机右下仓门禁,
        [OtherLang("L_Feeder1 Bin Tray Exist Snr")]
        左供料机1料仓有料,
        [OtherLang("L_Feeder2 Bin Tray Exist Snr")]
        左供料机2料仓有料,
        [OtherLang("Reserved_DI_1_1_8")]
        备用DI_1_1_8,
        [OtherLang("Reserved_DI_1_1_9")]
        备用DI_1_1_9,
        [OtherLang("Reserved_DI_1_1_10")]
        备用DI_1_1_10,
        [OtherLang("Reserved_DI_1_1_11")]
        备用DI_1_1_11,
        [OtherLang("Reserved_DI_1_1_12")]
        备用DI_1_1_12,
        [OtherLang("Reserved_DI_1_1_13")]
        备用DI_1_1_13,
        [OtherLang("Reserved_DI_1_1_14")]
        备用DI_1_1_14,
        [OtherLang("Reserved_DI_1_1_15")]
        备用DI_1_1_15,
        [OtherLang("R_Feeder EMRG Stop")]
        右供料机急停,
        [OtherLang("Reserved_DI_2_0_1")]
        备用DI_2_0_1,
        [OtherLang("R_Feeder L_Up Bin PHO")]
        右供料机左上仓对射,
        [OtherLang("R_Feeder L_Down Bin PHO")]
        右供料机左下仓对射,
        [OtherLang("R_Feeder R_Up Bin PHO")]
        右供料机右上仓对射,
        [OtherLang("R_Feeder R_Down Bin PHO")]
        右供料机右下仓对射,
        [OtherLang("R_Feeder L_Up Bin Btn")]
        右供料机左上仓按钮,
        [OtherLang("R_Feeder L_Down Bin Btn")]
        右供料机左下仓按钮,
        [OtherLang("R_Feeder R_Up Bin Btn")]
        右供料机右上仓按钮,
        [OtherLang("R_Feeder R_Down Bin Btn")]
        右供料机右下仓按钮,
        [OtherLang("R_Feeder L_Up Bin CYL Home")]
        右供料机左上仓气缸原点,
        [OtherLang("R_Feeder L_Down Bin CYL Home")]
        右供料机左下仓气缸原点,
        [OtherLang("R_Feeder R_Up Bin CYL Home")]
        右供料机右上仓气缸原点,
        [OtherLang("R_Feeder R_Down Bin CYL Home")]
        右供料机右下仓气缸原点,
        [OtherLang("R_Feeder L_Up Bin CYL Work")]
        右供料机左上仓气缸动点,
        [OtherLang("R_Feeder L_Down Bin CYL Work")]
        右供料机左下仓气缸动点,
        [OtherLang("R_Feeder R_Up Bin CYL Work")]
        右供料机右上仓气缸动点,
        [OtherLang("R_Feeder R_Down Bin CYL Work")]
        右供料机右下仓气缸动点,
        [OtherLang("R_Feeder L_Up Bin Safe Door")]
        右供料机左上仓门禁,
        [OtherLang("R_Feeder L_Down Bin Safe Door")]
        右供料机左下仓门禁,
        [OtherLang("R_Feeder R_Up Bin Safe Door")]
        右供料机右上仓门禁,
        [OtherLang("R_Feeder R_Down Bin Safe Door")]
        右供料机右下仓门禁,
        [OtherLang("R_Feeder1 Bin Tray Exist Snr")]
        右供料机1料仓有料,
        [OtherLang("R_Feeder2 Bin Tray Exist Snr")]
        右供料机2料仓有料,
        [OtherLang("Reserved_DI_2_1_8")]
        备用DI_2_1_8,
        [OtherLang("Reserved_DI_2_1_9")]
        备用DI_2_1_9,
        [OtherLang("Reserved_DI_2_1_10")]
        备用DI_2_1_10,
        [OtherLang("Reserved_DI_2_1_11")]
        备用DI_2_1_11,
        [OtherLang("Reserved_DI_2_1_12")]
        备用DI_2_1_12,
        [OtherLang("Reserved_DI_2_1_13")]
        备用DI_2_1_13,
        [OtherLang("Reserved_DI_2_1_14")]
        备用DI_2_1_14,
        [OtherLang("Reserved_DI_2_1_15")]
        备用DI_2_1_15,
        [OtherLang("Main Safe Door1")]
        主设备门禁1,
        [OtherLang("Reserved_DI_3_0_1")]
        备用DI_3_0_1,
        [OtherLang("Main Safe Door3")]
        主设备门禁3,
        [OtherLang("Reserved_DI_3_0_3")]
        备用DI_3_0_3,
        [OtherLang("Reserved_DI_3_0_4")]
        备用DI_3_0_4,
        [OtherLang("Reserved_DI_3_0_5")]
        备用DI_3_0_5,
        [OtherLang("L_Asm R1 Temp Ctrller Alarm")]
        左工位R1温控报警,
        [OtherLang("L_Asm R2 Temp Ctrller Alarm")]
        左工位R2温控报警,
        [OtherLang("L_Asm R3 Temp Ctrller Alarm")]
        左工位R3温控报警,
        [OtherLang("R_Asm R1 Temp Ctrller Alarm")]
        右工位R1温控报警,
        [OtherLang("R_Asm R2 Temp Ctrller Alarm")]
        右工位R2温控报警,
        [OtherLang("R_Asm R3 Temp Ctrller Alarm")]
        右工位R3温控报警,
        [OtherLang("Inv Flow Line Check Ready To Next")]
        回流线下一台可以接收信号,
        [OtherLang("Inv Flow Line Carrier Exist To Prev")]
        回流线上一台有载具信号,
        [OtherLang("Inv Flow Line Carrier In Reflect Snr")]
        回流线进载漫反射,
        [OtherLang("Inv Flow Line Carrier Out Reflect Snr")]
        回流线出载漫反射
    }
}

