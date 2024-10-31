using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace XCore
{
    public enum DoId
    {
        [OtherLang("L_Asm R1 Vacuum1 Suck")]
        左贴装R1真空1吸 = 0,
        [OtherLang("L_Asm R1 Vacuum1 Break")]
        左贴装R1真空1破,
        [OtherLang("L_Asm R2 Vacuum1 Suck")]
        左贴装R2真空1吸,
        [OtherLang("L_Asm R2 Vacuum1 Break")]
        左贴装R2真空1破,
        [OtherLang("L_Asm R3 Vacuum1 Suck")]
        左贴装R3真空1吸,
        [OtherLang("L_Asm R3 Vacuum1 Break")]
        左贴装R3真空1破,
        [OtherLang("L_Asm Up Light")]
        左贴装上定位光源,
        [OtherLang("L_Asm Up CCD Trig")]
        左贴装上相机触发,
        [OtherLang("L_Asm Down Light")]
        左下定位光源,
        [OtherLang("L_Asm Down CCD Trig")]
        左下相机触发,
        [OtherLang("R_Asm R1 Vacuum1 Suck")]
        右贴装R1真空1吸,
        [OtherLang("R_Asm R1 Vacuum1 Break")]
        右贴装R1真空1破,
        [OtherLang("R_Asm R2 Vacuum1 Suck")]
        右贴装R2真空1吸,
        [OtherLang("R_Asm R2 Vacuum1 Break")]
        右贴装R2真空1破,
        [OtherLang("R_Asm R3 Vacuum1 Suck")]
        右贴装R3真空1吸,
        [OtherLang("R_Asm R3 Vacuum1 Break")]
        右贴装R3真空1破,
        [OtherLang("R_Asm Up Light")]
        右贴装上定位光源,
        [OtherLang("R_Asm Up CCD Trig")]
        右贴装上相机触发,
        [OtherLang("R_Asm Down Light")]
        右下定位光源,
        [OtherLang("R_Asm Down CCD Trig")]
        右下相机触发,
        [OtherLang("L_Asm R1 Vacuum2 Suck")]
        左贴装R1真空2吸,
        [OtherLang("L_Asm R1 Vacuum2 Break")]
        左贴装R1真空2破,
        [OtherLang("L_Asm R2 Vacuum2 Suck")]
        左贴装R2真空2吸,
        [OtherLang("L_Asm R2 Vacuum2 Break")]
        左贴装R2真空2破,
        [OtherLang("L_Asm R3 Vacuum2 Suck")]
        左贴装R3真空2吸,
        [OtherLang("L_Asm R3 Vacuum2 Break")]
        左贴装R3真空2破,
        [OtherLang("R_Asm R1 Vacuum2 Suck")]
        右贴装R1真空2吸,
        [OtherLang("R_Asm R1 Vacuum2 Break")]
        右贴装R1真空2破,
        [OtherLang("R_Asm R2 Vacuum2 Suck")]
        右贴装R2真空2吸,
        [OtherLang("R_Asm R2 Vacuum2 Break")]
        右贴装R2真空2破,
        [OtherLang("R_Asm R3 Vacuum2 Suck")]
        右贴装R3真空2吸,
        [OtherLang("R_Asm R3 Vacuum2 Break")]
        右贴装R3真空2破,
        [OtherLang("Red Light")]
        红灯,
        [OtherLang("Yellow Light")]
        黄灯,
        [OtherLang("Green Light")]
        绿灯,
        [OtherLang("Beeper")]
        蜂鸣,
        [OtherLang("Carrier Ready To Next")]
        给下一台有载具信号,
        [OtherLang("Asm Fail To Next")]
        给下一台装配失败信号,
        [OtherLang("Carrier In To Prev")]
        给上一台可以接收信号,
        [OtherLang("L_Asm Trans Vacuum Suck")]
        左贴装中转真空吸,
        [OtherLang("L_Asm Trans Vacuum Break")]
        左贴装中转真空破,
        [OtherLang("R_Asm Trans Vacuum Suck")]
        右贴装中转真空吸,
        [OtherLang("R_Asm Trans Vacuum Break")]
        右贴装中转真空破,
        [OtherLang("L_Asm Jack Vacuum Suck")]
        左贴装顶升真空吸,
        [OtherLang("L_Asm Jack Vacuum Break")]
        左贴装顶升真空破,
        [OtherLang("R_Asm Jack Vacuum Suck")]
        右贴装顶升真空吸,
        [OtherLang("R_Asm Jack Vacuum Break")]
        右贴装顶升真空破,
        [OtherLang("Reserved DO_0_0_15")]
        备用DO_0_0_15,
        [OtherLang("Carrier In Block")]
        进载单控阻挡,
        [OtherLang("Carrier In Flow Line Start")]
        进载启动PU,
        [OtherLang("Carrier In Flow Line Dir")]
        进载方向DR,
        [OtherLang("Carrier In Flow Line Release")]
        进载释放MF,
        [OtherLang("L_Asm Flow Line Block")]
        左贴装单控阻挡,
        [OtherLang("Reserved DO_0_1_5")]
        备用DO_0_1_5,
        [OtherLang("L_Asm Jack Rise")]
        左贴装顶升上升,
        [OtherLang("L_Asm Flow Line Start")]
        左贴装启动PU,
        [OtherLang("L_Asm Flow Line Dir")]
        左贴装方向DR,
        [OtherLang("L_Asm Flow Line Release")]
        左贴装释放MF,
        [OtherLang("Trans Flow Line Block")]
        过渡单控阻挡,
        [OtherLang("Trans Flow Line Start")]
        过渡启动PU,
        [OtherLang("Trans Flow Line Dir")]
        过渡方向DR,
        [OtherLang("Trans Flow Line Release")]
        过渡释放MF,
        [OtherLang("R_Asm Flow Line Block")]
        右贴装单控阻挡,
        [OtherLang("Reserved DO_0_1_15")]
        备用DO_0_1_15,
        [OtherLang("R_Asm Jack Rise")]
        右贴装顶升上升,
        [OtherLang("R_Asm Flow Line Start")]
        右贴装启动PU,
        [OtherLang("R_Asm Flow Line Dir")]
        右贴装方向DR,
        [OtherLang("R_Asm Flow Line Release")]
        右贴装释放MF,
        [OtherLang("Reserved DO_0_2_4")]
        备用DO_0_2_4,
        [OtherLang("Carrier Out Flow Line Start")]
        出载启动PU,
        [OtherLang("Carrier Out Flow Line Dir")]
        出载方向DR,
        [OtherLang("Carrier Out Flow Line Release")]
        出载释放MF,
        [OtherLang("Reserved DO_0_2_8")]
        备用DO_0_2_8,
        [OtherLang("Reserved DO_0_2_9")]
        备用DO_0_2_9,
        [OtherLang("Reserved DO_0_2_10")]
        备用DO_0_2_10,
        [OtherLang("Reserved DO_0_2_11")]
        备用DO_0_2_11,
        [OtherLang("L_Asm PrePick1 Vacuum Suck")]
        左预取料1真空吸,
        [OtherLang("L_Asm PrePick1 Vacuum Break")]
        左预取料1真空破,
        [OtherLang("L_Asm PrePick1 Jack Rise")]
        左预取料1顶升上升,
        [OtherLang("Reserved DO_0_2_15")]
        备用DO_0_2_15,
        [OtherLang("Reserved DO_0_3_0")]
        备用DO_0_3_0,
        [OtherLang("Reserved DO_0_3_1")]
        备用DO_0_3_1,
        [OtherLang("Reserved DO_0_3_2")]
        备用DO_0_3_2,
        [OtherLang("Reserved DO_0_3_3")]
        备用DO_0_3_3,
        [OtherLang("Reserved DO_0_3_4")]
        备用DO_0_3_4,
        [OtherLang("Reserved DO_0_3_5")]
        备用DO_0_3_5,
        [OtherLang("Reserved DO_0_3_6")]
        备用DO_0_3_6,
        [OtherLang("L_Asm PrePick2 Vacuum Suck")]
        左预取料2真空吸,
        [OtherLang("L_Asm PrePick2 Vacuum Break")]
        左预取料2真空破,
        [OtherLang("L_Asm PrePick2 Jack Rise")]
        左预取料2顶升上升,
        [OtherLang("Reserved DO_0_3_10")]
        备用DO_0_3_10,
        [OtherLang("Reserved DO_0_3_11")]
        备用DO_0_3_11,
        [OtherLang("Reserved DO_0_3_12")]
        备用DO_0_3_12,
        [OtherLang("Reserved DO_0_3_13")]
        备用DO_0_3_13,
        [OtherLang("L_Asm Temp Ctrl")]
        左工位温度控制,
        [OtherLang("R_Asm Temp Ctrl")]
        右工位温度控制,
        [OtherLang("Reserved DO_0_4_0")]
        备用DO_0_4_0,
        [OtherLang("Reserved DO_0_4_1")]
        备用DO_0_4_1,
        [OtherLang("R_Asm PrePick1 Vacuum Suck")]
        右预取料1真空吸,
        [OtherLang("R_Asm PrePick1 Vacuum Break")]
        右预取料1真空破,
        [OtherLang("R_Asm PrePick1 Jack Rise")]
        右预取料1顶升上升,
        [OtherLang("Reserved DO_0_4_5")]
        备用DO_0_4_5,
        [OtherLang("Reserved DO_0_4_6")]
        备用DO_0_4_6,
        [OtherLang("Reserved DO_0_4_7")]
        备用DO_0_4_7,
        [OtherLang("Reserved DO_0_4_8")]
        备用DO_0_4_8,
        [OtherLang("Reserved DO_0_4_9")]
        备用DO_0_4_9,
        [OtherLang("Reserved DO_0_4_10")]
        备用DO_0_4_10,
        [OtherLang("Reserved DO_0_4_11")]
        备用DO_0_4_11,
        [OtherLang("Reserved DO_0_4_12")]
        备用DO_0_4_12,
        [OtherLang("R_Asm PrePick2 Vacuum Suck")]
        右预取料2真空吸,
        [OtherLang("R_Asm PrePick2 Vacuum Break")]
        右预取料2真空破,
        [OtherLang("R_Asm PrePick2 Jack Rise")]
        右预取料2顶升上升,
        [OtherLang("Reserved DO_0_5_0")]
        备用DO_0_5_0,
        [OtherLang("Reserved DO_0_5_1")]
        备用DO_0_5_1,
        [OtherLang("Reserved DO_0_5_2")]
        备用DO_0_5_2,
        [OtherLang("Reserved DO_0_5_3")]
        备用DO_0_5_3,
        [OtherLang("Reserved DO_0_5_4")]
        备用DO_0_5_4,
        [OtherLang("Reserved DO_0_5_5")]
        备用DO_0_5_5,
        [OtherLang("Reserved DO_0_5_6")]
        备用DO_0_5_6,
        [OtherLang("Reserved DO_0_5_7")]
        备用DO_0_5_7,
        [OtherLang("Reserved DO_0_5_8")]
        备用DO_0_5_8,
        [OtherLang("Reserved DO_0_5_9")]
        备用DO_0_5_9,
        [OtherLang("Reserved DO_0_5_10")]
        备用DO_0_5_10,
        [OtherLang("Reserved DO_0_5_11")]
        备用DO_0_5_11,
        [OtherLang("Reserved DO_0_5_12")]
        备用DO_0_5_12,
        [OtherLang("Reserved DO_0_5_13")]
        备用DO_0_5_13,
        [OtherLang("Reserved DO_0_5_14")]
        备用DO_0_5_14,
        [OtherLang("Reserved DO_0_5_15")]
        备用DO_0_5_15,
        [OtherLang("Reserved DO_1_0_0")]
        备用DO_1_0_0,
        [OtherLang("Reserved DO_1_0_1")]
        备用DO_1_0_1,
        [OtherLang("L_Feeder L_Up Bin CYL")]
        左供料机左上仓气缸,
        [OtherLang("L_Feeder L_Down Bin CYL")]
        左供料机左下仓气缸,
        [OtherLang("L_Feeder R_Up Bin CYL")]
        左供料机右上仓气缸,
        [OtherLang("L_Feeder L_Down Bin CYL")]
        左供料机右下仓气缸,
        [OtherLang("L_Feeder L_Up Axis Break")]
        左供料机左上轴刹车,
        [OtherLang("L_Feeder L_Down Axis Break")]
        左供料机左下轴刹车,
        [OtherLang("L_Feeder R_Up Axis Break")]
        左供料机右上轴刹车,
        [OtherLang("L_Feeder R_Down Axis Break")]
        左供料机右下轴刹车,
        [OtherLang("L_Feeder L_Up Bin Btn Light")]
        左供料机左上仓按钮灯,
        [OtherLang("L_Feeder L_Down Bin Btn Light")]
        左供料机左下仓按钮灯,
        [OtherLang("L_Feeder R_Up Bin Btn Light")]
        左供料机右上仓按钮灯,
        [OtherLang("L_Feeder R_Down Bin Btn Light")]
        左供料机右下仓按钮灯,
        [OtherLang("Reserved DO_1_0_14")]
        备用DO_1_0_14,
        [OtherLang("Reserved DO_1_0_15")]
        备用DO_1_0_15,
        [OtherLang("Reserved DO_1_1_0")]
        备用DO_1_1_0,
        [OtherLang("Reserved DO_1_1_1")]
        备用DO_1_1_1,
        [OtherLang("Reserved DO_1_1_2")]
        备用DO_1_1_2,
        [OtherLang("Reserved DO_1_1_3")]
        备用DO_1_1_3,
        [OtherLang("Reserved DO_1_1_4")]
        备用DO_1_1_4,
        [OtherLang("Reserved DO_1_1_5")]
        备用DO_1_1_5,
        [OtherLang("Reserved DO_1_1_6")]
        备用DO_1_1_6,
        [OtherLang("Reserved DO_1_1_7")]
        备用DO_1_1_7,
        [OtherLang("Reserved DO_1_1_8")]
        备用DO_1_1_8,
        [OtherLang("Reserved DO_1_1_9")]
        备用DO_1_1_9,
        [OtherLang("Reserved DO_1_1_10")]
        备用DO_1_1_10,
        [OtherLang("Reserved DO_1_1_11")]
        备用DO_1_1_11,
        [OtherLang("Reserved DO_1_1_12")]
        备用DO_1_1_12,
        [OtherLang("Reserved DO_1_1_13")]
        备用DO_1_1_13,
        [OtherLang("Reserved DO_1_1_14")]
        备用DO_1_1_14,
        [OtherLang("Reserved DO_1_1_15")]
        备用DO_1_1_15,
        [OtherLang("Reserved DO_2_0_0")]
        备用DO_2_0_0,
        [OtherLang("Reserved DO_2_0_1")]
        备用DO_2_0_1,
        [OtherLang("R_Feeder L_Up Bin CYL")]
        右供料机左上仓气缸,
        [OtherLang("R_Feeder L_Down Bin CYL")]
        右供料机左下仓气缸,
        [OtherLang("R_Feeder R_Up Bin CYL")]
        右供料机右上仓气缸,
        [OtherLang("R_Feeder R_Down Bin CYL")]
        右供料机右下仓气缸,
        [OtherLang("R_Feeder L_Up Axis Break")]
        右供料机左上轴刹车,
        [OtherLang("R_Feeder L_Down Axis Break")]
        右供料机左下轴刹车,
        [OtherLang("R_Feeder R_Up Axis Break")]
        右供料机右上轴刹车,
        [OtherLang("R_Feeder R_Down Axis Break")]
        右供料机右下轴刹车,
        [OtherLang("R_Feeder L_Up Bin Btn Light")]
        右供料机左上仓按钮灯,
        [OtherLang("R_Feeder L_Down Bin Btn Light")]
        右供料机左下仓按钮灯,
        [OtherLang("R_Feeder R_Up Bin Btn Light")]
        右供料机右上仓按钮灯,
        [OtherLang("R_Feeder R_Down Bin Btn Light")]
        右供料机右下仓按钮灯,
        [OtherLang("Reserved DO_2_0_14")]
        备用DO_2_0_14,
        [OtherLang("Reserved DO_2_0_15")]
        备用DO_2_0_15,
        [OtherLang("Reserved DO_2_1_0")]
        备用DO_2_1_0,
        [OtherLang("Reserved DO_2_1_1")]
        备用DO_2_1_1,
        [OtherLang("Reserved DO_2_1_2")]
        备用DO_2_1_2,
        [OtherLang("Reserved DO_2_1_3")]
        备用DO_2_1_3,
        [OtherLang("Reserved DO_2_1_4")]
        备用DO_2_1_4,
        [OtherLang("Reserved DO_2_1_5")]
        备用DO_2_1_5,
        [OtherLang("Reserved DO_2_1_6")]
        备用DO_2_1_6,
        [OtherLang("Reserved DO_2_1_7")]
        备用DO_2_1_7,
        [OtherLang("Reserved DO_2_1_8")]
        备用DO_2_1_8,
        [OtherLang("Reserved DO_2_1_9")]
        备用DO_2_1_9,
        [OtherLang("Reserved DO_2_1_10")]
        备用DO_2_1_10,
        [OtherLang("Reserved DO_2_1_11")]
        备用DO_2_1_11,
        [OtherLang("Reserved DO_2_1_12")]
        备用DO_2_1_12,
        [OtherLang("Reserved DO_2_1_13")]
        备用DO_2_1_13,
        [OtherLang("Reserved DO_2_1_14")]
        备用DO_2_1_14,
        [OtherLang("Reserved DO_2_1_15")]
        备用DO_2_1_15,
        [OtherLang("Reserved DO_3_0_0")]
        备用DO_3_0_0,
        [OtherLang("Reserved DO_3_0_1")]
        备用DO_3_0_1,
        [OtherLang("Reserved DO_3_0_2")]
        备用DO_3_0_2,
        [OtherLang("Reserved DO_3_0_3")]
        备用DO_3_0_3,
        [OtherLang("Reserved DO_3_0_4")]
        备用DO_3_0_4,
        [OtherLang("Reserved DO_3_0_5")]
        备用DO_3_0_5,
        [OtherLang("Inv Flow Line Rotate Pos")]
        回流线正转,
        [OtherLang("Inv Flow Line Rotate Neg")]
        回流线反转,
        [OtherLang("Reserved DO_3_0_8")]
        备用DO_3_0_8,
        [OtherLang("Reserved DO_3_0_9")]
        备用DO_3_0_9,
        [OtherLang("Reserved DO_3_0_10")]
        备用DO_3_0_10,
        [OtherLang("Reserved DO_3_0_11")]
        备用DO_3_0_11,
        [OtherLang("Inv Flow Line Carrier Exist To Next")]
        回流线给下一台有载具信号,
        [OtherLang("Inv Flow Line Ready To Prev")]
        回流线给上一台可以接收信号,
        [OtherLang("Reserved DO_3_0_14")]
        备用DO_3_0_14,
        [OtherLang("Reserved DO_3_0_15")]
        备用DO_3_0_15

    }
}

