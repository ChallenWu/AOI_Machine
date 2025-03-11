using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using OpenCvSharp;
using OVisionPro.Services._01._Core.ShapesPics;
using OVisionPro.Services.ImageProcessing.BASE.TemplateMatching;
using Serilog;
using YamlDotNet.Core.Events;
using static System.Net.Mime.MediaTypeNames;

namespace OVisionPro
{
    public class cvX
    {
        public static Action<Mat> onUpdateFrameToUIAction;
        public static Action<int, Mat> onUpdateInFrameToUIAction;
        public static Action<int, Mat> onUpdateOutFrameToUIAction;
        public static Action<Mat> onUpdateImshowToUIAction;
        public static Action<string> onListenRunStateAction;
        public string ccd1Path;
        public string ccd2Path;
        public static Dictionary<string, Action<string, Mat>> ListOnUpdateImshowToUIAction = new Dictionary<string, Action<string, Mat>> { };
        public static Dictionary<string, Dictionary<string, bool>> ListImshowInstances = new Dictionary<string, Dictionary<string, bool>>() { };
        public cvX()
        {
            ccd1Path = visionGlob.ccdResoucesPath + $"\\CCD1\\";
            Directory.CreateDirectory(ccd1Path);
            ccd2Path = visionGlob.ccdResoucesPath + $"\\CCD2\\";
            Directory.CreateDirectory(ccd2Path);
        }
        public static Mat ImRead(string fileName, ImreadModes flags = ImreadModes.Color)
        {
            return Cv2.ImRead(fileName, flags);
        }
        private static readonly object lockObject = new object();

        public static void RegisterActionImshow(string funcID, FormUpdatePicCore form)
        {
            ListOnUpdateImshowToUIAction[funcID] = (winname, mat) =>
            {
                form.updateImg(winname, mat);
                var sssss = Shapes.Instance.shapes[funcID];
            };

        }
        public static void UnRegisterActionImshow(string funcID)
        {
            if (ListOnUpdateImshowToUIAction.Keys.Contains(funcID))
            {
                ListOnUpdateImshowToUIAction.Remove(funcID);
            }

        }

        public static void ImShow(string funcID, string winName, Mat mat)
        {
            lock (lockObject)
            {

                if (mat == null || mat.Width <= 0 || mat.Height <= 0) { Log.Information($"[ImShow][{funcID}][{winName}] EX: mat frame is null."); return; }
                var xxx = XParameterManager.Instance.ucBlockBasicControllers;
                bool debugOutput = false;
                if (winName.Contains("MainOutput") && (winName.EndsWith("0") || winName.EndsWith("1"))) {
                    DateTime date = DateTime.Now; // Hoặc một giá trị DateTime cụ thể
                    string formattedDateTime = date.ToString("yyyyMMddHHmmss");
                    formattedDateTime = $"{XVisionManager.Instance.mainSN}_{formattedDateTime}.png";
                    //Cv2.ImWrite(ccd1Path + formattedDateTime, mat);
                    ucVisionPage.instance.UpdatePicOutput(int.Parse(winName[winName.Length - 1].ToString()), mat);
                    debugOutput = true;
                    if (XVisionManager.Instance.VisionRunMode == visionGlob.RunMode.debug)  onUpdateOutFrameToUIAction?.Invoke(int.Parse(winName[winName.Length - 1].ToString()), mat);
                    return;
                } else if (winName.Contains("MainInput") && (winName.EndsWith("0") || winName.EndsWith("1"))) {
                    DateTime date = DateTime.Now; // Hoặc một giá trị DateTime cụ thể
                    string formattedDateTime = date.ToString("yyyyMMddHHmmss");
                    formattedDateTime = $"{XVisionManager.Instance.mainSN}_RES_{formattedDateTime}.png";
                    //Cv2.ImWrite(ccd2Path + formattedDateTime, mat);
                    ucVisionPage.instance.UpdatePicInput(int.Parse(winName[winName.Length - 1].ToString()), mat);
                    //onUpdateInFrameToUIAction?.Invoke(int.Parse(winName[winName.Length - 1].ToString()), mat);
                    return;
                }  else if (!winName.Contains("DISPLAY")) {  winName = $"ImShow_0_{winName}"; XParameterManager.Instance.AddParamManager(funcID, winName); }
                winName = funcID + "_" + winName;
                if (XVisionManager.Instance.VisionRunMode == visionGlob.RunMode.debug)
                {

                    if (onListenRunStateAction != null) { onListenRunStateAction?.Invoke(funcID); };
                    if (ListOnUpdateImshowToUIAction.ContainsKey(funcID))
                    {
                        ListOnUpdateImshowToUIAction[funcID]?.Invoke(winName, mat);
                    }
                    if (winName.Contains("DISPLAY") && XParameterManager.Instance.SelectedBlockID == funcID)
                    {
                        try
                        {
                            onUpdateFrameToUIAction?.Invoke(mat);
                        }
                        catch (Exception ex)
                        {
                            Log.Information($"[imshow] ex: {ex}");
                        }
                        return;
                    }
                    else if (winName.Contains("MainInput") || winName.Contains("MainOutput")) { }
                    else if (XParameterManager.Instance.GetUcBlockBasicData(funcID, winName, "boolean", 0) == 1)
                    {
                        Cv2.ImShow(winName, mat); Cv2.WaitKey(2);
                    }
                }
            }
        }

        public static Mat ImgLoad(Mat frame)
        {
            return frame;
        }
        public static string CharArray(string blockID = "CharArray", string cvID = "0")
        {
            cvID = $"CharArray_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            string Characters = XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "Characters", "Char1,char2");
            Characters = Characters.Replace(" ", "");
            return Characters;
        }

        public static (int, int, int, int) ROI1(string blockID = "ROI1", string cvID = "0")
        {
            cvID = $"ROI1_0_{cvID}";
            XParameterManager.Instance.AddParamManagerROI1(blockID, cvID);
            int x1 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "x1", 0);
            int y1 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "y1", 0);
            int x2 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "x2", 10);
            int y2 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "y2", 10);
            return (x1, y1, x2, y2);
        }
        public static (int, int, int, int, bool, Dictionary<string, object>) ROITEMPMATCH(Mat frame, string blockID = "ROI1", string cvID = "0")
        {
            cvID = $"ROITEMPMATCH{cvID}";
            XParameterManager.Instance.AddParamManagerROI1(blockID, cvID);
            int x1 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "x1", 0);
            int y1 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "y1", 0);
            int x2 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "x2", 10);
            int y2 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "y2", 10);

            Dictionary<string, object> externVal = new Dictionary<string, object> { { "angle", null}, { "center", null}, { "msg", "NG" } };
            bool resCheck = false;

            Rect roi = new Rect(x1, y1, x2 - x1, y2 - y1);
            if (frame.Width >= x2 - x1 || frame.Height >= y2 - y1)
            {
                Mat roiCheck = frame.Clone(roi);
                var temp = TemplateMatch3.Instance.TemplMatchO(roiCheck, cvID);
                resCheck = temp.Item1;
                externVal = temp.Item2;
            }
            return (x1, y1, x2, y2, resCheck, externVal);
        }

        public static (int, int) CVLimits(string blockID = "ROI1", string cvID = "0")
        {
            cvID = $"CVLimits_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            int mi = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "min", 0);
            int ma = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "max", 0);
            if (mi <= ma) { return (mi, ma); }
            return (ma, mi);
        }

        public static (int, int, int, int, int, int, int, int) ROI2(string blockID = "ROI1", string cvID = "0", int xT=0, int yT=0)
        {
            cvID = $"ROI2_0_{cvID}";
            XParameterManager.Instance.AddParamManagerROI2(blockID, cvID);

            XParameterManager.Instance.SetUcBlockBasicData(blockID, cvID, "xT", xT);
            XParameterManager.Instance.SetUcBlockBasicData(blockID, cvID, "yT", yT);
            int w1 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "w1", 1);
            int h1 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "h1", 1);
            int w2 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "w2", 1);
            int h2 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "h2", 1);

            int x1 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "x1", 0);
            int y1 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "y1", 0);
            int x2 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "x2", 10);
            int y2 = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "y2", 10);
            return (x1, y1, x2, y2, w1, h1, w2, h2);
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

        public static CircleSegment[] HoughCircles(InputArray image, HoughMethods method, double dp, double minDist, double param1 = 100, double param2 = 100, int minRadius = 0, int maxRadius = 0, string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"HoughCircles_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            method = (HoughMethods)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "method", HoughMethods.Gradient);
            dp = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "dp", 1);
            minDist = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "minDist", 10);
            param1 = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "param1", 30);
            param2 = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "param2", 60);
            minRadius = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "minRadius", 10);
            maxRadius = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "maxRadius", 20);
            return Cv2.HoughCircles(image, method, dp, minDist, param1, param2, minRadius, maxRadius);
        }

        public static void Add(InputArray src1, InputArray src2, OutputArray dst, InputArray mask = null, string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"Add_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            int dtype = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "dtype", -1);
            Cv2.Add(src1, src2, dst, mask, dtype);
        }

        public static bool CVBool(string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"CVBool_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            int boolNum = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "boolean", 0);
            return (boolNum == 1) ? true : false;
        }
        public static float CVNumberF(string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"CVNumberF_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            float numberCV = (float)XParameterManager.Instance.GetUcBlockBasicDataDouble(blockID, cvID, "number", 0.2f);
            return (float)numberCV;
        }
        public static float CVNumberD(string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"CVNumberD_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            float numberCV = (float)XParameterManager.Instance.GetUcBlockBasicDataDouble(blockID, cvID, "number", 0.2f);
            return (float)numberCV;
        }
        public static int CVNumber(string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"CVNumber_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            int numberCV = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "number", 1);
            return numberCV;
        }
        public static void CVReadOnly(string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"CVReadOnly_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            XParameterManager.Instance.SetUcBlockBasicData(blockID, cvID, "ReadOnly", 1);
        }

        public static void Blur(InputArray src, OutputArray dst, OpenCvSharp.Size ksize, OpenCvSharp.Point? anchor = null, BorderTypes borderType = BorderTypes.Reflect101, string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"blur_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            borderType = (BorderTypes)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "borderType", BorderTypes.Reflect101);
            double maxval = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "maxval", 127);
            Cv2.Blur(src, dst, ksize, anchor, borderType);
        }

        public static void MatchTemplate(InputArray image, InputArray templ, OutputArray result, TemplateMatchModes method, InputArray mask = null, string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"MatchTemplate_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            string mmatch = XParameterManager.Instance.GetUcBlockBasicData1(blockID, cvID, "TemplateMatchModes", "CCoeffNormed");
            TemplateMatchModes enumValue = (TemplateMatchModes)Enum.Parse(typeof(TemplateMatchModes), mmatch);
            TemplateMatchModes enumValue1 = Enum.TryParse(mmatch, true, out TemplateMatchModes methodtest) ? methodtest : TemplateMatchModes.CCoeffNormed;
            Cv2.MatchTemplate(image, templ, result, enumValue1, mask);
        }
        public static string MatchTemplateAlgorithm(string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"MatchTemplateAlgorithm_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            string algorithmMatch = XParameterManager.Instance.GetUcBlockBasicData1(blockID, cvID, "MatchAlgorithm", "MaxAccuracy");
            return algorithmMatch;
        }
        public static void MinMaxLoc1(InputArray src, out double minVal, out double maxVal, string blockID = "FuncID", string cvID = "0")
        {
            /// <summary>
            /// finds global minimum and maximum array elements and returns their values and their locations
            /// </summary>
            cvID = $"MinMaxLoc1_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            minVal = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "minVal", 127);
            maxVal = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "maxVal", 127);
            Cv2.MinMaxLoc(src, out minVal, out maxVal);
        }
        public static void MinMaxLoc2(InputArray src, out OpenCvSharp.Point minLoc, out OpenCvSharp.Point maxLoc, string blockID = "FuncID", string cvID = "0")
        {
            /// <summary>
            /// finds global minimum and maximum array elements and returns their values and their locations
            /// </summary>
            /// <param name="src">The source single-channel array</param>
            /// <param name="minLoc">Pointer to returned minimum location</param>
            /// <param name="maxLoc">Pointer to returned maximum location</param>
            cvID = $"MinMaxLoc2_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            //minLoc = new OpenCvSharp.Point(x1, y1);
            //maxLoc = new OpenCvSharp.Point(x2, y2);
            Cv2.MinMaxLoc(src, out minLoc, out maxLoc);
        }
        public static void MinMaxLoc3(InputArray src, out double minVal, out double maxVal, out OpenCvSharp.Point minLoc,
            out OpenCvSharp.Point maxLoc, InputArray mask = null, string blockID = "FuncID", string cvID = "0")
        {
            /// <summary>
            /// Finds global minimum and maximum array elements and returns their values and their locations.
            /// </summary>
            /// <param name="src">The source single-channel array</param>
            /// <param name="minLoc">Pointer to returned minimum location</param>
            /// <param name="maxLoc">Pointer to returned maximum location</param>
            cvID = $"MinMaxLoc3_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            // Tính giá trị min/max và vị trí của chúng
            Cv2.MinMaxLoc(src, out minVal, out maxVal, out minLoc, out maxLoc);
        }

        public static OpenCvSharp.Scalar Scalar(string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"Scalar_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            string mScalar = XParameterManager.Instance.GetUcBlockBasicData1(blockID, cvID, "color", "ActiveCaptionText");
            KnownColor enumValue = (KnownColor)Enum.Parse(typeof(KnownColor), mScalar);
            return ColorToScalar(Color.FromKnownColor(enumValue));
        }

        public static Dictionary<string, OpenCvSharp.Point> fontPositions = new Dictionary<string, OpenCvSharp.Point>();
        public static (HersheyFonts, int, double, OpenCvSharp.Point) OFont(string blockID = "FuncID", string cvID = "0")
        {
            cvID = $"OFont_0_{cvID}";
            XParameterManager.Instance.AddParamManager(blockID, cvID);
            string oHerShey = XParameterManager.Instance.GetUcBlockBasicData1(blockID, cvID, "HerSheyType", "HersheySimplex");
            int thickness = (int)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "thickness", 1);
            double fontScale = (double)XParameterManager.Instance.GetUcBlockBasicData(blockID, cvID, "fontScale", 127);
            HersheyFonts enumValue = (HersheyFonts)Enum.Parse(typeof(HersheyFonts), oHerShey);

            OpenCvSharp.Point pos;
            int baseLine;
            int xText = 5;
            OpenCvSharp.Size fontSize = Cv2.GetTextSize("X", enumValue, fontScale, thickness, out baseLine);
            int yText = fontSize.Height + baseLine + 5;
            pos = new OpenCvSharp.Point(xText, yText);
            return (enumValue, thickness, fontScale, pos);


        }
        static OpenCvSharp.Scalar ColorToScalar(Color color)
        {
            // Lưu ý: OpenCVSharp sử dụng hệ màu BGRA, nghĩa là B trước, G sau cùng R
            return new OpenCvSharp.Scalar(color.B, color.G, color.R);
        }

    }
}
