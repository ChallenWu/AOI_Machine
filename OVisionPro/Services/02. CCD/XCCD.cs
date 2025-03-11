using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.IO;
using OpenCvSharp;
using System.Windows.Controls;
using OVisionPro.Services.ImageProcessing;

namespace OVisionPro
{
    public class XCCD
    {
        public XCCDHIK HIK1;
        public XCCDUSB USB1 = new XCCDUSB();
        public XCCDCognex Cognex1 = new XCCDCognex();
        private readonly static XCCD instance = new XCCD();
        public static XCCD Instance { get { return instance; } }
        public XCCD() { HIK1 = new XCCDHIK(); }
    }

    public class XCCDHIK : XTask
    {
        public HikCam HIKCCD;
        private static OpenCvSharp.Mat _frame;
        private static Bitmap _frameBitmap;
        private static bool living = false;
        private bool liveNDFlag = false;
        public static bool CCDVirtual = false;
        public string ImgPathVirtual;
        public string CCDName;
        public Action<Bitmap> postBitmap;

        public XCCDHIK(string IMGPathCCDVirtual= "D:\\test3.jpg", string CcdName="CCD1") 
        {
            ImgPathVirtual = IMGPathCCDVirtual;
            CCDName = CcdName;
            Start(visionGlob.RunState.running);
        }

        public OpenCvSharp.Mat frame
        {
            get
            {
                if (CCDVirtual)
                {
                    return (File.Exists(ImgPathVirtual)) ? OpenCvSharp.Cv2.ImRead(ImgPathVirtual) : null;
                }
                else if (HIKCCD != null && HIKCCD.isConnected)
                {
                    return HIKCCD.CaptureImageMat();
                }
                return null;
            }
        }

        public Bitmap frameBitmap
        {
            get
            {
                _frameBitmap?.Dispose();
                _frameBitmap = null;
                if (CCDVirtual)
                {
                    if (File.Exists(ImgPathVirtual))
                    {
                        _frame?.Dispose();
                        _frame = frame;
                        _frameBitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_frame);
                    }
                    else
                    {
                        _frameBitmap = null;
                    }
                }
                else if (HIKCCD != null && HIKCCD.isConnected)
                {
                    _frameBitmap = HIKCCD.CaptureImage();
                }
                else
                {
                    _frameBitmap = null;
                }

                return _frameBitmap;
            }
        }
        public void ReleaseFrame() { _frame?.Dispose(); }
        public void ReleaseFrameBitmap() { _frameBitmap?.Dispose(); }

        public void StartLiveCCD()
        {
            liveNDFlag = false;
            living = true;
        }

        public void PauseLiveCCD()
        {
            liveNDFlag = false;
            living = false;
        }
        public void StopLiveCCD()
        {
            living = false;
        }

        public int CCDinitialize()
        {
            int resOpen = -1;
            try
            {
                HIKCCD = new HikCam("CCD1");
                resOpen = HIKCCD.Open();
            } 
            catch (Exception ex)
            {
                resOpen = - 1;
            }
            if (resOpen != 0)
            {
                using (var dia = new CustomMessageBox("KET NOI CCD THAT BAI."))
                {
                    dia.ShowDialog();
                    dia.Dispose();
                }
            }
            Start(visionGlob.RunMode.production);
            return resOpen;
        }

        public bool CCDRelease()
        {
            if (HIKCCD == null) return true;
            if (!HIKCCD.isConnected) return true;
            HIKCCD.Close();
            return true;
        }

        
        public override void Running(object runMode)
        {
            Bitmap matPost;
            while (true)
            {
                if (living)
                {
                    if (XCCD.Instance.HIK1.frameBitmap == null && !liveNDFlag)
                    {
                        string msg = "CCD MAT KET NOI.";
                        liveNDFlag = true;
                        logW.Ins.warning($"[LIVECCD] {msg}");
                    }
                    else
                    {
                        matPost = frameBitmap;
                        postBitmap?.Invoke(matPost);
                    }
                }
                Thread.Sleep(300);
            }
        }
    }

    public class XCCDCognex : XTask
    {

    }

    public class XCCDUSB : XTask
    {

    }
}
