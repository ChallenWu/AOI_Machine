using XCore;
using XCore.Framework.MultiLang;

namespace HB_IWatch
{
    class SettingNozzlesCompensation : XSetting
    {

        #region 左机补偿参数
        [MyProperty("左吸头1对位补偿调整X", "1左组装补偿值")]
        public double 左吸头1对位补偿调整X
        {
            get
            {
                return GetNodeValue("LAssemble1OffsetX", 0.0);
            }
            set
            {
                SetNodeValue("LAssemble1OffsetX", value);
            }
        }
        [MyProperty("左吸头1对位补偿调整Y", "1左组装补偿值")]
        public double 左吸头1对位补偿调整Y
        {
            get
            {
                return GetNodeValue("LAssemble1OffsetY", 0.0);
            }
            set
            {
                SetNodeValue("LAssemble1OffsetY", value);
            }
        }
        [MyProperty("左吸头1对位补偿调整R", "1左组装补偿值")]
        public double 左吸头1对位补偿调整R
        {
            get
            {
                return GetNodeValue("LAssemble1OffsetR", 0.0);
            }
            set
            {
                SetNodeValue("LAssemble1OffsetR", value);
            }
        }

        [MyProperty("左吸头2对位补偿调整X", "1左组装补偿值")]
        public double 左吸头2对位补偿调整X
        {
            get
            {
                return GetNodeValue("LAssemble2OffsetX", 0.0);
            }
            set
            {
                SetNodeValue("LAssemble2OffsetX", value);
            }
        }
        [MyProperty("左吸头2对位补偿调整Y", "1左组装补偿值")]
        public double 左吸头2对位补偿调整Y
        {
            get
            {
                return GetNodeValue("LAssemble2OffsetY", 0.0);
            }
            set
            {
                SetNodeValue("LAssemble2OffsetY", value);
            }
        }
        [MyProperty("左吸头2对位补偿调整R", "1左组装补偿值")]
        public double 左吸头2对位补偿调整R
        {
            get
            {
                return GetNodeValue("LAssemble2OffsetR", 0.0);
            }
            set
            {
                SetNodeValue("LAssemble2OffsetR", value);
            }
        }

        [MyProperty("左吸头3对位补偿调整X", "1左组装补偿值")]
        public double 左吸头3对位补偿调整X
        {
            get
            {
                return GetNodeValue("LAssemble3OffsetX", 0.0);
            }
            set
            {
                SetNodeValue("LAssemble3OffsetX", value);
            }
        }
        [MyProperty("左吸头3对位补偿调整Y", "1左组装补偿值")]
        public double 左吸头3对位补偿调整Y
        {
            get
            {
                return GetNodeValue("LAssemble3OffsetY", 0.0);
            }
            set
            {
                SetNodeValue("LAssemble3OffsetY", value);
            }
        }
        [MyProperty("左吸头3对位补偿调整R", "1左组装补偿值")]
        public double 左吸头3对位补偿调整R
        {
            get
            {
                return GetNodeValue("LAssemble3OffsetR", 0.0);
            }
            set
            {
                SetNodeValue("LAssemble3OffsetR", value);
            }
        }

        #endregion

        #region 右机补偿参数
        [MyProperty("右吸头1对位补偿调整X", "2右组装补偿值")]
        public double 右吸头1对位补偿调整X
        {
            get
            {
                return GetNodeValue("RAssemble1OffsetX", 0.0);
            }
            set
            {
                SetNodeValue("RAssemble1OffsetX", value);
            }
        }
        [MyProperty("右吸头1对位补偿调整Y", "2右组装补偿值")]
        public double 右吸头1对位补偿调整Y
        {
            get
            {
                return GetNodeValue("RAssemble1OffsetY", 0.0);
            }
            set
            {
                SetNodeValue("RAssemble1OffsetY", value);
            }
        }
        [MyProperty("右吸头1对位补偿调整R", "2右组装补偿值")]
        public double 右吸头1对位补偿调整R
        {
            get
            {
                return GetNodeValue("RAssemble1OffsetR", 0.0);
            }
            set
            {
                SetNodeValue("RAssemble1OffsetR", value);
            }
        }

        [MyProperty("右吸头2对位补偿调整X", "2右组装补偿值")]
        public double 右吸头2对位补偿调整X
        {
            get
            {
                return GetNodeValue("RAssemble2OffsetX", 0.0);
            }
            set
            {
                SetNodeValue("RAssemble2OffsetX", value);
            }
        }
        [MyProperty("右吸头2对位补偿调整Y", "2右组装补偿值")]
        public double 右吸头2对位补偿调整Y
        {
            get
            {
                return GetNodeValue("RAssemble2OffsetY", 0.0);
            }
            set
            {
                SetNodeValue("RAssemble2OffsetY", value);
            }
        }
        [MyProperty("右吸头2对位补偿调整R", "2右组装补偿值")]
        public double 右吸头2对位补偿调整R
        {
            get
            {
                return GetNodeValue("RAssemble2OffsetR", 0.0);
            }
            set
            {
                SetNodeValue("RAssemble2OffsetR", value);
            }
        }

        [MyProperty("右吸头3对位补偿调整X", "2右组装补偿值")]
        public double 右吸头3对位补偿调整X
        {
            get
            {
                return GetNodeValue("RAssemble3OffsetX", 0.0);
            }
            set
            {
                SetNodeValue("RAssemble3OffsetX", value);
            }
        }
        [MyProperty("右吸头3对位补偿调整Y", "2右组装补偿值")]
        public double 右吸头3对位补偿调整Y
        {
            get
            {
                return GetNodeValue("RAssemble3OffsetY", 0.0);
            }
            set
            {
                SetNodeValue("RAssemble3OffsetY", value);
            }
        }
        [MyProperty("右吸头3对位补偿调整R", "2右组装补偿值")]
        public double 右吸头3对位补偿调整R
        {
            get
            {
                return GetNodeValue("RAssemble3OffsetR", 0.0);
            }
            set
            {
                SetNodeValue("RAssemble3OffsetR", value);
            }
        }

        #endregion

    }
}
