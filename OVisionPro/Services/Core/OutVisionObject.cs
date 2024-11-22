using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVisionPro
{
    class OutVisionObject
    {
        public Mat frameImg;
        public Dictionary<string, String> errCodes = new Dictionary<string, string>();
    }
}
