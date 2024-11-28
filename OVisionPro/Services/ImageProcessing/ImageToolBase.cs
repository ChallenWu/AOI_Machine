using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OVisionPro
{
    public partial class ImageToolBase
    {
        public List<string> modelNames = new List<string>();
        public Globals.RunMode testMode = Globals.RunMode.production;
        public float timeStart = DateTime.Now.Millisecond;
        public float timeEnd = DateTime.Now.Millisecond;
        public float cycleTime = 0.0f;
        
        public Dictionary<string, Dictionary<string, object>> resultTools = new Dictionary<string, Dictionary<string, object>>();

        public Mat ImgLoad(Mat frame)
        {
            return frame;
        }

        public Mat ImgLoad(string fileName, ImreadModes flags = ImreadModes.Color)
        {
            return Cv2.ImRead(fileName, flags);
        }
    }
}
