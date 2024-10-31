using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCore
{
    public enum SettingId
    {
        [OtherLang("Option")]
        选项_PD = 1,
        [OtherLang("Calib Param")]
        标定参数_PD = 2, // 运动
        [OtherLang("Nozzle Comp Param")]
        吸头补偿参数_PD = 3, //标定参数
        [OtherLang("L_Asm Cave X Comp Param")]
        左机穴位补偿参数X_PD = 4,
        [OtherLang("L_Asm Cave Y Comp Param")]
        左机穴位补偿参数Y_PD = 5,
        [OtherLang("L_Asm Cave R Comp Param")]
        左机穴位补偿参数R_PD = 6,
        [OtherLang("R_Asm Cave X Comp Param")]
        右机穴位补偿参数X_PD = 7,
        [OtherLang("R_Asm Cave Y Comp Param")]
        右机穴位补偿参数Y_PD = 8,
        [OtherLang("R_Asm Cave R Comp Param")]
        右机穴位补偿参数R_PD = 9,
        [OtherLang("DOE Nozzle Comp Param")]
        吸头补偿参数_DOE,
        [OtherLang("DOE L_Asm Cave X Comp Param")]
        左机穴位补偿参数X_DOE,
        [OtherLang("DOE L_Asm Cave Y Comp Param")]
        左机穴位补偿参数Y_DOE,
        [OtherLang("DOE L_Asm Cave R Comp Param")]
        左机穴位补偿参数R_DOE,
        [OtherLang("DOE R_Asm Cave X Comp Param")]
        右机穴位补偿参数X_DOE,
        [OtherLang("DOE R_Asm Cave Y Comp Param")]
        右机穴位补偿参数Y_DOE,
        [OtherLang("DOE R_Asm Cave R Comp Param")]
        右机穴位补偿参数R_DOE,
        [OtherLang("Cali Param")]
        标定参数_DOE = 17,
        [OtherLang("Option Param")]
        选项_DOE = 18,
        [OtherLang("Option ICT")]
        ICT = 20

    }
}
