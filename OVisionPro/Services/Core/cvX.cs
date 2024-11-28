using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace OVisionPro
{
    public class cvX
    {
        public static Action<Mat> onUpdateFrameToUIAction;
        public static Action<Mat> onUpdateImshowToUIAction;
        public static Mat ImRead(string fileName, ImreadModes flags = ImreadModes.Color)
        {
            return Cv2.ImRead(fileName, flags);
        }
        private static readonly object lockObject = new object();

        public static void ImShow(string funcID, string winName, Mat mat)
        {
            lock (lockObject)
            {
                var xxx = XParameterManager.Instance.ucBlockBasicControllers;
                if (XVisionManager.Instance.VisionRunMode == Globals.RunMode.debug && XVisionManager.Instance.GetSelectFuncID() == funcID)
                {
                    if (winName.Contains("DISPLAY"))
                    {
                        try
                        {
                            onUpdateFrameToUIAction?.Invoke(mat);
                            //Cv2.ImShow(winName, mat);
                        }
                        catch (Exception ex)
                        {
                            // Xử lý ngoại lệ ở đây
                        }
                        //mat.Dispose();
                        return;
                    }
                    // them điều kiên check box
                    //Cv2.ImShow(winName, mat);
                    //mat.Dispose();
                    return;
                }
            }
        }

        public static Mat ImgLoad(Mat frame)
        {
            return frame;
        }

        public static (int, int, int, int) SelectROI(string blockID = "SelectROI", string cvID = "0")
        {
            cvID = $"SelectROI_0_{cvID}";
            XParameterManager.Instance.AddParamManagerROI(blockID, cvID);
            int x1 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "x1", 0);
            int y1 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "y1", 0);
            int x2 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "x2", 1);
            int y2 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "y2", 1);
            return (x1, y1, x2, y2);
        }

        public static double Threshold(InputArray src, OutputArray dst, string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"Threshold_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            double thresh = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "thresh", 127);
            double maxval = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "maxval", 127);
            string typeStr = XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "type", "Binary");
            ThresholdTypes typeType = (ThresholdTypes)Enum.Parse(typeof(ThresholdTypes), typeStr);
            return Cv2.Threshold(src, dst, thresh, maxval, typeType);
        }

        public static void AdaptiveThreshold(InputArray src, OutputArray dst, string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"AdaptiveThreshold_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            double maxValue = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "maxValue", 255);
            AdaptiveThresholdTypes adaptiveMethod = (AdaptiveThresholdTypes)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "adaptiveMethod", AdaptiveThresholdTypes.GaussianC);
            ThresholdTypes thresholdType = (ThresholdTypes)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "thresholdType", ThresholdTypes.Binary);
            int blockSize = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "blockSize", 3);
            double c = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "c", 3.0);
            Cv2.AdaptiveThreshold(src, dst, maxValue, adaptiveMethod, thresholdType, blockSize, c);
        }

        public static void Add(InputArray src1, InputArray src2, OutputArray dst, InputArray mask = null, string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"Add_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            int dtype = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "dtype", -1);
            Cv2.Add(src1, src2, dst, mask, dtype);
        }

        public static void Blur(InputArray src, OutputArray dst, OpenCvSharp.Size ksize, OpenCvSharp.Point? anchor = null, BorderTypes borderType = BorderTypes.Reflect101, string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"blur_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            borderType = (BorderTypes)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "borderType", BorderTypes.Reflect101);
            double maxval = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "maxval", 127);
            Cv2.Blur(src, dst, ksize, anchor, borderType);
        }
    }
}
