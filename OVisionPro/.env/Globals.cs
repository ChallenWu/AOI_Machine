using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVisionPro
{
    class Globals
    {
        public enum RunState
        {
            stopping,
            running,
            idling,
            pausing,
        }

        public enum TaskID
        {
            BlobTool,
            FilterTool,
            EdgeTool,
            ThresholdTool
        }
        public List<Type> InputDataType = new List<Type>
        {
            typeof(int),
            typeof(double),
            typeof(string),
            typeof(bool),
            typeof(Mat)
        };
    }
}
