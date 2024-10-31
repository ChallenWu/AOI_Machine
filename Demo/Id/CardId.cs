using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace XCore
{
    public enum CardId
    {
        [OtherLang("Main1")]
        主设备1 = 1,
        [OtherLang("Main2")]
        主设备2,
        [OtherLang("Mat-Supply1")]
        供料机1,
        [OtherLang("Mat-Supply2")]
        供料机2,
        [OtherLang("Main3")]
        主设备3
    }
}

