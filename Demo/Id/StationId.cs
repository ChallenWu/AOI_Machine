using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore;

namespace XCore
{
    public enum StationId
    {
        [OtherLang("Left Assemble")]
        Left,
        [OtherLang("Right Assemble")]
        Right,
        [OtherLang("Flow Line")]
        FlowLine,
        [OtherLang("Feeder")]
        Feeder,
        [OtherLang("Scanner")]
        Scanner

    }
}
