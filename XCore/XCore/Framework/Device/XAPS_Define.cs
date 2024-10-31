using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XCore
{
    class XAPS_Define : XObject
    {
        //报警
        public const int MIO_ALM = 0x01 << 0;
        //positive Limit
        public const int MIO_PEL = 0x01 << 1;
        //negative Limit
        public const int MIO_MEL = 0x01 << 2;
        //原点
        public const int MIO_ORG = 0x01 << 3;
        //急停
        public const int MIO_EMG = 0x01 << 4;
        //使能
        public const int MIO_SVON = 0x01 << 5;
        //电机运动
        public const int MTS_HMV = 0x01 << 0;
        //电机到位
        public const int MTS_MDN = 0x01 << 1;
        //报警？？
        public const int MTS_ASTP = 0x01 << 2;
    }
}
