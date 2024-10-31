using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace XCore
{
    public enum AxisId
    {
        [OtherLang("Left X Axis")]
        左贴装X轴 = 0,
        [OtherLang("Right X Axis")]
        右贴装X轴,
        [OtherLang("Left Y Axis")]
        左贴装Y轴,
        [OtherLang("Right Y Axis")]
        右贴装Y轴,
        [OtherLang("Left Z1 Axis")]
        左贴装Z1轴,
        [OtherLang("Left Z2 Axis")]
        左贴装Z2轴,
        [OtherLang("Left Z3 Axis")]
        左贴装Z3轴,
        [OtherLang("Right Z1 Axis")]
        右贴装Z1轴,
        [OtherLang("Right Z2 Axis")]
        右贴装Z2轴,
        [OtherLang("Right Z3 Axis")]
        右贴装Z3轴,
        [OtherLang("Left R1 Axis")]
        左贴装R1轴,
        [OtherLang("Left R2 Axis")]
        左贴装R2轴,
        [OtherLang("Left R3 Axis")]
        左贴装R3轴,
        [OtherLang("Right R1 Axis")]
        右贴装R1轴,
        [OtherLang("Right R2 Axis")]
        右贴装R2轴,
        [OtherLang("Right R3 Axis")]
        右贴装R3轴,
        [OtherLang("Left Mat-Supply Left-Up Axis")]
        左供料左上Z轴,
        [OtherLang("Left Mat-Supply Left-Down Axis")]
        左供料左下Z轴,
        [OtherLang("Left Mat-Supply Right-Up Axis")]
        左供料右上Z轴,
        [OtherLang("Left Mat-Supply Right-Down Axis")]
        左供料右下Z轴,
        [OtherLang("Right Mat-Supply Left-Up Axis")]
        右供料左上Z轴,
        [OtherLang("Right Mat-Supply Left-Down Axis")]
        右供料左下Z轴,
        [OtherLang("Right Mat-Supply Right-Up Axis")]
        右供料右上Z轴,
        [OtherLang("Right Mat-Supply Right-Down Axis")]
        右供料右下Z轴,
        [OtherLang("Left Pre-Pick 1Y Axis")]
        左预取料1Y轴,
        [OtherLang("Left Pre-Pick 2Y Axis")]
        左预取料2Y轴,
        [OtherLang("Right Pre-Pick 1Y Axis")]
        右预取料1Y轴,
        [OtherLang("Right Pre-Pick 2Y Axis")]
        右预取料2Y轴,
        [OtherLang("PLC X-Axis")]
        XAxis,
        [OtherLang("PLC Y-Axis")]
        YAxis,
        [OtherLang("PLC Z-Axis")]
        ZAxis,
        [OtherLang("PLC R-Axis")]
        RAxis,
    }
}

