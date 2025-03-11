using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using OVisionPro.Services.ImageProcessing;
using System.Drawing;
using System.Windows.Forms;
using OVisionPro.Services.ImageProcessing.BASE.TemplateMatching;
using Serilog;

namespace OVisionPro
{
    public partial class ToolBase
    {
        public List<string> modelNames = new List<string>();
        public visionGlob.RunMode testMode = visionGlob.RunMode.production;
        public float timeStart = DateTime.Now.Millisecond;
        public float timeEnd = DateTime.Now.Millisecond;
        public float cycleTime = 0.0f;
        public Dictionary<string, Func<Mat, string, (bool, Dictionary<string, object>)>> ActionToolsBase = new Dictionary<string, Func<Mat, string, (bool, Dictionary<string, object>)>>() { };
        public Dictionary<string, Dictionary<string, object>> resultTools = new Dictionary<string, Dictionary<string, object>>();
        public List<string> tempMatchTypes = new List<string>();
        public Func<Mat, string, (bool, Dictionary<string, object>)> DetectByBTemplateMatching = TemplateMatching.Instance.DetectByBTemplateMatching;
        //public Func<Mat, string, (bool, Dictionary<string, object>)> DetectByPerformPatternMatching = TemplateMatching.Instance.PerformPatternMatching;
        public void writeInfoLog(string content)
        {
            Log.Information(content);
        }
        public void writewWarningLog(string content)
        {
            Log.Information(content);
        }
        public void writewErrorLog(string content)
        {
            Log.Information(content);
        }
        public ToolBase()
        {
            initializeToolsBase();
            object[] keyValuePairs = ActionToolsBase.Keys.Cast<object>().ToArray();
            XImgPatternManager.Instance.ImgPatternDerialization();
            XParameterManager.Instance.AddInitialROI1ParameterCV(keyValuePairs[0], keyValuePairs);
        }
        public void initializeToolsBase()
        {
            ActionToolsBase.Add("DetectBasicTemplateMatching", TemplateMatching.Instance.DetectByBTemplateMatching);
            ActionToolsBase.Add("detectBasicCircles", detectKComponent);
            ActionToolsBase.Add("DetectHIGHTemplateMatching", TemplateMatch3.Instance.TemplMatchO);
            ActionToolsBase.Add("detectA1Component", detectA1Component);
            ActionToolsBase.Add("detectA2Component", detectA2Component);
            ActionToolsBase.Add("detectBComponent", detectBComponent);
            ActionToolsBase.Add("detectCComponent", detectCComponent);
            ActionToolsBase.Add("detectDComponent", detectDComponent);
            ActionToolsBase.Add("detectEComponent", detectEComponent);
            ActionToolsBase.Add("detectFComponent", detectFComponent);
            ActionToolsBase.Add("detectGComponent", detectGComponent);
            ActionToolsBase.Add("detectHComponent", detectHComponent);
            ActionToolsBase.Add("detectKComponent", detectKComponent);
        }


        public Dictionary<string, object> SelectROIArea(Mat frame, string blockID)
        {
            if (frame == null) return new Dictionary<string, object>();
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            using (Mat dst = new Mat())
            {
                // Vẽ hình vuông (với vị trí và kích thước cụ thể)
                Scalar colorScarOK = cvX.Scalar(blockID, "OK");
                var fontIn = cvX.OFont(blockID, "thresh_Filter");
                HersheyFonts fontT = fontIn.Item1;
                int fontThickness = fontIn.Item2;
                double fontScale = fontIn.Item3;
                OpenCvSharp.Point fontPos = fontIn.Item4;
                cvX.ImShow(funcID: blockID, "ALIGN_DISPLAY", frame);
            }
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Mat ImgLoad(Mat frame, string blockID)
        {
            return frame;
        }

        public (bool, string) ExecSelectROI(Mat inputFrame, string parentKey)
        {
            Dictionary<string, bool> ResultList = new Dictionary<string, bool>();
            OpenCvSharp.Point textPosition;
            string resPut;
            string failPattern = "";
            int inc = 1;
            
            OpenCvSharp.Point firstPosition = new OpenCvSharp.Point(10, 10);
            Scalar colorScarOK = cvX.Scalar(parentKey, "OK");
            Scalar colorScarNG = cvX.Scalar(parentKey, "NG");
            var fontIn = cvX.OFont(parentKey, "KET QUA");
            HersheyFonts fontT = fontIn.Item1;
            int fontThickness = fontIn.Item2;
            double fontScale = fontIn.Item3;
            OpenCvSharp.Point fontPos = fontIn.Item4;
            fontPos.Y += 30;

            var fontPaPattern = cvX.OFont(parentKey, "TUNG PHAN");
            HersheyFonts fontPaT = fontPaPattern.Item1;
            int fontPaThickness = fontPaPattern.Item2;
            double fontPaScale = fontPaPattern.Item3;
            OpenCvSharp.Point fontPaPos = fontPaPattern.Item4 + fontPos;
            Scalar curColor;
            foreach (string keyA in XParameterManager.Instance.ucBlockBasicControllers[parentKey].Keys)
            {
                if (keyA.Contains("ROI1"))
                {
                    var valPos = XParameterManager.Instance.ucBlockBasicControllers[parentKey][keyA];
                    int mRoix1 = (int)valPos["ROI1_1_x1"];
                    int mRoiy1 = (int)valPos["ROI1_1_y1"];
                    int mRoix2 = (int)valPos["ROI1_1_x2"];
                    int mRoiy2 = (int)valPos["ROI1_1_y2"];
                    string blockName = (string)valPos["ROI1_1_BlockName"];
                    string toolKey =  valPos["ROI1_1_BlockID"].ToString();

                    // Vị trí của văn bản
                    textPosition = new OpenCvSharp.Point(10, firstPosition.Y + 70 * inc);
                    bool resSub = false;
                    inc += 1;
                    Dictionary<string, object> resDic;

                    curColor = colorScarNG;
                    if (ActionToolsBase.ContainsKey(toolKey)) {
                        Rect roi = new Rect(mRoix1, mRoiy1, mRoix2 - mRoix1, mRoiy2 - mRoiy1);
                        Mat ROI = inputFrame.Clone(roi);
                        var resBase = ActionToolsBase[toolKey](ROI, keyA);
                        resSub = resBase.Item1;
                        resDic = resBase.Item2;
                        if (resSub) curColor = colorScarOK;
                        ROI.Dispose();
                        resDic.Clear();
                    }
                    blockName = (ResultList.ContainsKey(blockName)) ? blockName + Guid.NewGuid().ToString() : blockName;
                    resPut = (resSub) ? "PASS" : "FAIL";
                    resPut = $"{blockName} : {resPut}";
                    failPattern += (!resSub) ? resPut :"";
                    fontPaPos.Y += fontPaPattern.Item4.Y+5;
                    Cv2.PutText(inputFrame, resPut, fontPaPos, fontPaT, fontPaScale, curColor, fontThickness);
                    ResultList[blockName] = resSub;
                    OpenCvSharp.Point topLeft = new OpenCvSharp.Point(mRoix1, mRoiy1); // Tọa độ góc trên trái
                    OpenCvSharp.Point bottomRight = new OpenCvSharp.Point(mRoix2, mRoiy2); // Tọa độ góc dưới phải
                    Cv2.Rectangle(inputFrame, topLeft, bottomRight, color: curColor, (fontThickness > 2) ? (fontThickness - 2) : fontThickness);
                }
            }


            bool finalRes = (ResultList.Values.Contains(false) || ResultList.Keys.Count <= 0) ? false : true;
            string FinalText = (finalRes) ? "PASS" : "FAIL";
            curColor = (finalRes) ? colorScarOK : colorScarNG;
            Cv2.PutText(inputFrame, FinalText, fontPos, HersheyFonts.HersheySimplex, 5.0, curColor, 4);
            return (finalRes, failPattern);
        }

        public Mat ImgLoad(string fileName, ImreadModes flags = ImreadModes.Color)
        {
            return Cv2.ImRead(fileName, flags);
        }
        public Mat RotationBound(Mat frame, double angle, double scale = 1.0)
        {
            if (frame == null) return null;
            OpenCvSharp.Point2f center = new Point2f(frame.Width / 2.0f, frame.Height / 2.0f);
            // Tỷ lệ phóng đại (1.0 = giữ nguyên kích thước)
            // Tạo ma trận xoay
            Mat rotationMatrix = Cv2.GetRotationMatrix2D(center, angle, scale);

            // Tính bounding box của ảnh sau khi xoay
            double cos = Math.Abs(rotationMatrix.At<double>(0, 0));
            double sin = Math.Abs(rotationMatrix.At<double>(0, 1));
            int newWidth = (int)(frame.Height * sin + frame.Width * cos);
            int newHeight = (int)(frame.Height * cos + frame.Width * sin);

            // Điều chỉnh ma trận xoay sao cho không bị mất ảnh (dịch chuyển ma trận)
            rotationMatrix.Set(0, 2, rotationMatrix.At<double>(0, 2) + ((newWidth / 2.0) - center.X));
            rotationMatrix.Set(1, 2, rotationMatrix.At<double>(1, 2) + ((newHeight / 2.0) - center.Y));


            // Xoay ảnh
            Mat rotatedFrame = new Mat();
            Cv2.WarpAffine(frame, rotatedFrame, rotationMatrix, new OpenCvSharp.Size(newWidth, newHeight));
            return rotatedFrame;
        }

        public static OpenCvSharp.Point RotateNewPoint(OpenCvSharp.Point point, Mat rotationMatrix)
        {
            // Tạo một vector từ điểm (x, y, 1) để nhân với ma trận xoay affine
            double[] v = new double[] { point.X, point.Y, 1 };

            // Tạo ma trận từ vector
            Mat vec = new Mat(3, 1, MatType.CV_64F, v);

            // Nhân ma trận xoay với vector để tính tọa độ mới
            Mat result = rotationMatrix * vec;

            // Trả về tọa độ mới (chuyển từ Mat sang Point)
            return new OpenCvSharp.Point((int)result.At<double>(0, 0), (int)result.At<double>(1, 0));
        }

        public (Mat, OpenCvSharp.Point) RotationAngleBound(Mat frame, Point2f orgPoint, double angle, double scale = 1.0)
        {
            if (frame == null) return (frame, new OpenCvSharp.Point(0,0));
            OpenCvSharp.Point2f center = new Point2f(frame.Width / 2.0f, frame.Height / 2.0f);
            // Tỷ lệ phóng đại (1.0 = giữ nguyên kích thước)
            // Tạo ma trận xoay
            Mat rotationMatrix = Cv2.GetRotationMatrix2D(center, angle, scale);

            // Tính bounding box của ảnh sau khi xoay
            double cos = Math.Abs(rotationMatrix.At<double>(0, 0));
            double sin = Math.Abs(rotationMatrix.At<double>(0, 1));
            int newWidth = (int)(frame.Height * sin + frame.Width * cos);
            int newHeight = (int)(frame.Height * cos + frame.Width * sin);
            // Chuyển góc từ độ sang radian
            double angleRad = angle * Math.PI / 180.0;

            // Tính cos và sin của góc
            double cosTheta = Math.Cos(angleRad);
            double sinTheta = Math.Sin(angleRad);

            // Dịch chuyển điểm về gốc tọa độ (tâm xoay)
            float xShifted = orgPoint.X - center.X;
            float yShifted = orgPoint.Y - center.Y;

            // Áp dụng công thức xoay
            float xN = (float)(xShifted * cosTheta - yShifted * sinTheta);
            float yN = (float)(xShifted * sinTheta + yShifted * cosTheta);

            // Dịch chuyển điểm về vị trí gốc
            xN += center.X;
            yN += center.Y;
            // Điều chỉnh ma trận xoay sao cho không bị mất ảnh (dịch chuyển ma trận)
            rotationMatrix.Set(0, 2, rotationMatrix.At<double>(0, 2) + ((newWidth / 2.0) - center.X));
            rotationMatrix.Set(1, 2, rotationMatrix.At<double>(1, 2) + ((newHeight / 2.0) - center.Y));
            OpenCvSharp.Point newP = RotateNewPoint(new OpenCvSharp.Point((int)orgPoint.X, (int)(orgPoint.Y)), rotationMatrix);
            // Xoay ảnh
            Mat rotatedFrame = new Mat();
            Cv2.WarpAffine(frame, rotatedFrame, rotationMatrix, new OpenCvSharp.Size(newWidth, newHeight));
            Cv2.Circle(rotatedFrame, newP, 1, new Scalar(0, 222, 255), 1);
            return (rotatedFrame, newP);
        }

        public (bool, Dictionary<string, object>) detectA1Component(Mat frame, string blockID)
        {
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID, cvID: "0");
            cvX.ImShow(blockID, "ALIGN_DISPLAY", dst);
            // Chuyển ảnh sang xám (grayscale)
            Mat grayImage = new Mat();
            Cv2.CvtColor(frame, grayImage, ColorConversionCodes.BGR2GRAY);

            // Làm mờ ảnh bằng GaussianBlur
            Mat blurredImage = new Mat();
            Cv2.GaussianBlur(grayImage, blurredImage, new OpenCvSharp.Size(5, 5), 0);

            // Áp dụng Thresholding (ngưỡng 127)
            Mat thresholdedImage = new Mat();
            Cv2.Threshold(blurredImage, thresholdedImage, 127, 255, ThresholdTypes.Binary);
            // Phát hiện vòng tròn bằng HoughCircles (phương pháp mặc định)
            CircleSegment[] circles = cvX.HoughCircles(blurredImage, HoughMethods.Gradient, dp: 1, minDist: 20,
                                                      param1: 50, param2: 30, minRadius: 10, maxRadius: 100, blockID);

            // Vẽ các vòng tròn phát hiện được lên ảnh gốc
            if (circles != null)
            {
                foreach (var circle in circles)
                {
                    // Chuyển đổi từ Point2f sang Point
                    OpenCvSharp.Point center = new OpenCvSharp.Point((int)circle.Center.X, (int)circle.Center.Y);

                    // Vẽ vòng tròn (với bán kính và tọa độ tâm)
                    Cv2.Circle(frame, (int)circle.Center.X, (int)circle.Center.Y, (int)circle.Radius, new Scalar(0, 255, 0), 3);

                    // Vẽ tâm của vòng tròn (với bán kính 3)
                    Cv2.Circle(frame, center, 3, new Scalar(0, 0, 255), 3);
                }
            }

            cvX.ImShow(blockID, "ALIGN_DISPLAY", frame);
            grayImage.Dispose();
            blurredImage.Dispose();
            thresholdedImage.Dispose();
            dst.Dispose();
            Dictionary<string, object> externVal = new Dictionary<string, object> { };
            if (circles.Length == 1) return (true, externVal);
            return (false, externVal);
        }

        public (bool, Dictionary<string, object>) detectCircles(Mat frame, string blockID)
        {
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID, cvID: "0");
            cvX.ImShow(blockID, "ALIGN_DISPLAY", dst);
            // Chuyển ảnh sang xám (grayscale)
            Mat grayImage = new Mat();
            Cv2.CvtColor(frame, grayImage, ColorConversionCodes.BGR2GRAY);

            // Làm mờ ảnh bằng GaussianBlur
            Mat blurredImage = new Mat();
            Cv2.GaussianBlur(grayImage, blurredImage, new OpenCvSharp.Size(5, 5), 0);

            // Áp dụng Thresholding (ngưỡng 127)
            Mat thresholdedImage = new Mat();
            Cv2.Threshold(blurredImage, thresholdedImage, 127, 255, ThresholdTypes.Binary);
            // Phát hiện vòng tròn bằng HoughCircles (phương pháp mặc định)
            CircleSegment[] circles = cvX.HoughCircles(blurredImage, HoughMethods.Gradient, dp: 1, minDist: 20, param1: 50, param2: 30, minRadius: 10, maxRadius: 100, blockID);
            // Vẽ các vòng tròn phát hiện được lên ảnh gốc
            if (circles != null)
            {
                foreach (var circle in circles)
                {
                    // Chuyển đổi từ Point2f sang Point
                    OpenCvSharp.Point center = new OpenCvSharp.Point((int)circle.Center.X, (int)circle.Center.Y);
                    // Vẽ vòng tròn (với bán kính và tọa độ tâm)
                    Cv2.Circle(frame, (int)circle.Center.X, (int)circle.Center.Y, (int)circle.Radius, new Scalar(0, 255, 0), 3);
                    // Vẽ tâm của vòng tròn (với bán kính 3)
                    Cv2.Circle(frame, center, 3, new Scalar(0, 0, 255), 3);
                }
            }
            int cirlesTarget = cvX.CVNumber(blockID, "0");
            cvX.ImShow(blockID, "ALIGN_DISPLAY", frame);

            grayImage.Dispose();
            blurredImage.Dispose();
            thresholdedImage.Dispose();
            dst.Dispose();

            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            Dictionary<string, object> externVal = new Dictionary<string, object> { };
            return (false, externVal);
        }

        public (bool, Mat) CropImgFollowPoint(Mat OrgFrame, int pX, int pY, string BlockID)
        {
            var roiItemTL = cvX.ROI2(BlockID, "SEL", pX, pY);
            int W1 = roiItemTL.Item5;
            int H1 = roiItemTL.Item6;
            int W2 = roiItemTL.Item7;
            int H2 = roiItemTL.Item8;

            int mRoix1 = (pX - W1 < 0) ? 0 : pX - W1;
            int mRoiy1 = (pY - H1 < 0) ? 0 : pY - H1;
            int mRoix2 = (pX + W2 > OrgFrame.Width) ? OrgFrame.Width : pX + W2;
            int mRoiy2 = (pY + H2 > OrgFrame.Height) ? OrgFrame.Height : pY + H2;
            
            if (mRoix1 < 0 || mRoiy1 < 0 ) return (false, OrgFrame);

            // Vẽ điểm lên ảnh (bằng cách sử dụng Circle với bán kính nhỏ)
            Cv2.Circle(OrgFrame, new OpenCvSharp.Point(pX, pY), 1, Scalar.Red, -1);

            OpenCvSharp.Point topLeft = new OpenCvSharp.Point(mRoix1, mRoiy1); // Tọa độ góc trên trái
            OpenCvSharp.Point bottomRight = new OpenCvSharp.Point(mRoix2, mRoiy2); // Tọa độ góc dưới phải
            Cv2.Rectangle(OrgFrame, topLeft, bottomRight, color: Scalar.Red, thickness: 2);

            Rect roi = new Rect(mRoix1, mRoiy1, mRoix2 - mRoix1, mRoiy2 - mRoiy1);
            cvX.ImShow(BlockID, "ALIGN_DISPLAY", OrgFrame);
            Mat cropFrame = OrgFrame.Clone(roi);
            return (true, cropFrame);
        }
        public (bool, Dictionary<string, object>) detectA2Component(Mat frame, string blockID)
        {
            
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID, cvID: "0");
            cvX.ImShow(blockID, "DISPLAY", dst);
            dst.Dispose();

            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            Dictionary<string, object> externVal = new Dictionary<string, object> { };
            return (false, externVal);
        }
        public (bool, Dictionary<string, object>) detectBComponent(Mat frame, string blockID)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID, cvID: "0");
            cvX.ImShow(blockID, "DISPLAY", dst);
            dst.Dispose();

            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            Dictionary<string, object> externVal = new Dictionary<string, object> { };
            return (false, externVal);
        }
        public (bool, Dictionary<string, object>) detectCComponent(Mat frame, string blockID)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID, cvID: "0");
            cvX.ImShow(blockID, "DISPLAY", dst);
            dst.Dispose();

            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            Dictionary<string, object> externVal = new Dictionary<string, object> { };
            return (false, externVal);
        }
        public (bool, Dictionary<string, object>) detectDComponent(Mat frame, string blockID)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID, cvID: "0");
            cvX.ImShow(blockID, "DISPLAY", dst);
            dst.Dispose();

            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            Dictionary<string, object> externVal = new Dictionary<string, object> { };
            return (false, externVal);
        }
        public (bool, Dictionary<string, object>) detectEComponent(Mat frame, string blockID)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID, cvID: "0");
            cvX.ImShow(blockID, "DISPLAY", dst);
            dst.Dispose();

            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            Dictionary<string, object> externVal = new Dictionary<string, object> { };
            return (false, externVal);
        }
        public (bool, Dictionary<string, object>) detectFComponent(Mat frame, string blockID)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID, cvID: "0");
            cvX.ImShow(blockID, "DISPLAY", dst);
            dst.Dispose();

            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            Dictionary<string, object> externVal = new Dictionary<string, object> { };
            return (false, externVal);
        }
        public (bool, Dictionary<string, object>) detectGComponent(Mat frame, string blockID)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID, cvID: "0");
            cvX.ImShow(blockID, "DISPLAY", dst);
            dst.Dispose();

            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            Dictionary<string, object> externVal = new Dictionary<string, object> { };
            return (false, externVal);
        }
        public (bool, Dictionary<string, object>) detectHComponent(Mat frame, string blockID)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID, cvID: "0");
            cvX.ImShow(blockID, "DISPLAY", dst);
            dst.Dispose();

            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            Dictionary<string, object> externVal = new Dictionary<string, object> { };
            return (false, externVal);
        }
        public (bool, Dictionary<string, object>) detectKComponent(Mat frame, string blockID)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID, cvID: "0");
            cvX.ImShow(blockID, "DISPLAY", dst);
            dst.Dispose();

            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            Dictionary<string, object> externVal = new Dictionary<string, object> { };
            return (false, externVal);
        }

    }
}
