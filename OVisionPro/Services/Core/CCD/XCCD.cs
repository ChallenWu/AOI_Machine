using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace OVisionPro
{
    public class XCCDHIK: XTask
    {
        public HikCam CCDInstance;
        private readonly static XCCDHIK instance = new XCCDHIK();
        private static OpenCvSharp.Mat _frame;


        public OpenCvSharp.Mat frame
        {
            get { return _frame; }
            set
            {
                _frame = value;
            }
        }

        public static XCCDHIK Instance
        {
            get { return instance; }
        }

        public void initialize()
        {
            CCDInstance =  new HikCam("CCD1");
            CCDInstance.Open();
        }


        public override void Running(object runMode)
        {
            //initialize();
            Thread.Sleep(1000);
            frame = OpenCvSharp.Cv2.ImRead("D:\\test1.jpg");
            while (true)
            {
                //Bitmap frame1 = CCDInstance.CaptureImage();
                //OpenCvSharp.Mat frame = OpenCvSharp.Extensions.BitmapConverter.ToMat(frame1);
                //frame1.Dispose();
                if (frame != null)
                {
                    //OpenCvSharp.Cv2.ImShow("11", frame);
                    //OpenCvSharp.Cv2.WaitKey(1);
                }
                Thread.Sleep(100);
                //frame = OpenCvSharp.Cv2.ImRead("D:\\test1.jpg");
            }
        }
    }

    class XCCDCognex: XTask
    {

    }

    class XCCDUSB: XTask
    {

    }
}
