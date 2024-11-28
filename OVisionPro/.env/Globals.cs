using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVisionPro
{
    public class Globals
    {
        public enum Permission
        {
            user,
            admin
        }

        public enum RunState
        {
            stopping,
            running,
            idling,
            pausing,
        }

        public enum ToolTypleData
        {
            Img,
            Pos,
            Doublue,
            Str,
            Float,
            Line,
        }

        public Dictionary<ToolTypleData, Type> ToolTypleDataVals = new Dictionary<ToolTypleData, Type>()
        {
            { ToolTypleData.Img, typeof(Mat) },
            { ToolTypleData.Pos, typeof(Tuple<int, int>) },
            { ToolTypleData.Str, typeof(string) },
            { ToolTypleData.Doublue, typeof(double) },
            { ToolTypleData.Float, typeof(float) },
            { ToolTypleData.Line, typeof(Tuple<int, int, int, int>) },
        };

        public enum RunMode
        {
            production,
            debug,
        }

        public enum TaskID
        {
            BlobTool,
            FilterTool,
            EdgeTool,
            ThresholdTool
        }
    }
}
