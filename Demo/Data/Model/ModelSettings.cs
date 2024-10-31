using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Model
{
    class ModelSettings
    {
        public const String Default_Model = "default";
        public string modelName { get; set; }
        public PLCPosition position { get; set; }
    }
    class PLCPosition
    {
        //Tọa độ chạy
        private int float_X;
        private int float_Y;
        private int float_Z;
        private int float_R;
        public int Float_X { get => float_X; set => float_X = value; }
        public int Float_Y { get => float_Y; set => float_Y = value; }
        public int Float_Z { get => float_Z; set => float_Z = value; }
        public int Float_R { get => float_R; set => float_R = value; }

        public PLCPosition()
            {
            this.Float_X = 0;
            this.Float_Y = 0;
            this.Float_Z = 0;
            this.Float_R = 0;
            }
    }
}
