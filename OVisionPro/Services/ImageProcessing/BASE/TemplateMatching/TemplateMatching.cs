using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Media3D;

namespace OVisionPro.Services.ImageProcessing
{
    class TemplateMatching
    {
        ///  <summary>
        ///     Find components basic.
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="blockID"></param>
        /// <returns></returns>
        /// 
        private readonly static TemplateMatching instance = new TemplateMatching();
        public static TemplateMatching Instance
        {
            get { return instance; }
        }

        public (bool, Dictionary<string, object>) DetectByBTemplateMatching(Mat frame, string blockID)
        {
            Dictionary<string, object> externVal = new Dictionary<string, object> { };

            bool resCheck = false;
            externVal["angle"] = null;
            externVal["center"] = null;
            externVal["msg"] = "NG";
            Scalar colorScarOK = cvX.Scalar(blockID, "OK");
            Scalar colorScarNG = cvX.Scalar(blockID, "NormaL");
            var fontIn = cvX.OFont(blockID, "thresh_Filter");
            HersheyFonts fontT = fontIn.Item1;
            int fontThickness = fontIn.Item2;
            double fontScale = fontIn.Item3;
            OpenCvSharp.Point fontPos = fontIn.Item4;

            float threshFilter = cvX.CVNumberF(blockID, "thresh_Filter");
            var linmitCount = cvX.CVLimits(blockID, "Lenght");
            int minLim = linmitCount.Item1;
            int maxLim = linmitCount.Item2;

            cvX.ImShow(blockID, "ORG_DISPLAY", frame);
            // Load ảnh nguồn và ảnh mẫu
            if (XImgPatternManager.Instance.ImgMatPatternData.ContainsKey(blockID))
            {
                Mat template = XImgPatternManager.Instance.ImgMatPatternData[blockID];
                if (OBasicAlgorithm.IsMat(template) && OBasicAlgorithm.IsMat(frame))
                {

                    // Chuyển đổi sang ảnh xám
                    Mat sourceGray = new Mat();
                    Mat templateGray = new Mat();

                    Cv2.CvtColor(frame, sourceGray, ColorConversionCodes.BGR2GRAY);
                    Cv2.CvtColor(template, templateGray, ColorConversionCodes.BGR2GRAY);

                    // Tạo ma trận lưu kết quả
                    int resultCols = frame.Cols - template.Cols + 1;
                    int resultRows = frame.Rows - template.Rows + 1;
                    Mat result = new Mat(resultRows, resultCols, OpenCvSharp.MatType.CV_32F);

                    if (OBasicAlgorithm.checkInputsMatch(template, frame))
                    {
                        // Sử dụng Template Matching
                        cvX.MatchTemplate(frame, template, result, TemplateMatchModes.CCoeffNormed, blockID: blockID, cvID: "0");
                        // Danh sách lưu tất cả các điểm khớp
                        List<Rect> matchLocations = new List<Rect>();
                        List<float> idxSort = new List<float>();
                        List<Rect> nonOverlappingBoxes = new List<Rect>();
                        List<float> nonidxSort = new List<float>();
                        // Tìm vị trí tốt nhất
                        Cv2.MinMaxLoc(result, out double minVal, out double maxVal, out OpenCvSharp.Point minLoc, out OpenCvSharp.Point maxLoc);
                        // Crop vùng khớp trong ảnh gốc
                        Mat matchedRegion = new Mat(sourceGray, new Rect(maxLoc.X, maxLoc.Y, templateGray.Cols, templateGray.Rows));
                        // Duyệt qua ma trận kết quả để tìm tất cả các giá trị vượt qua ngưỡng
                        // Vẽ hình chữ nhật quanh vùng tìm thấy

                        for (int y = 0; y < result.Rows; y++) {
                            for (int x = 0; x < result.Cols; x++) {
                                float similarity = result.At<float>(y, x);
                                // Nếu giá trị vượt ngưỡng
                                if (similarity >= (float)threshFilter / 10) {
                                    Rect rectTemp = new Rect(x, y, template.Cols, template.Rows);
                                    idxSort.Add(similarity);
                                    matchLocations.Add(rectTemp);
                                }
                            }
                        }
                        // ignore overlapping shapes
                        foreach (var box in matchLocations) {
                            bool isOverlapping = false;
                            foreach (var otherBox in nonOverlappingBoxes) {
                                if (box.IntersectsWith(otherBox)) { isOverlapping = true; break; }
                            }
                            // Nếu không giao nhau với bất kỳ hình nào đã được lưu, thêm vào danh sách
                            if (!isOverlapping) {
                                nonOverlappingBoxes.Add(box);
                                nonidxSort.Add(idxSort[matchLocations.IndexOf(box)]);
                            }
                        }
                        // drawing rectangle shape.
                        foreach (var oBox in nonOverlappingBoxes) { Cv2.Rectangle(frame, oBox, colorScarNG, fontThickness); }
                        if (nonidxSort.Count > 0)
                        {
                            resCheck = true;
                            // Lấy chỉ số sắp xếp của array1
                            float[] array1 = nonidxSort.ToArray();
                            Rect[] array2 = nonOverlappingBoxes.ToArray();
                            int[] sortedIndices = array1
                                .Select((value, index) => new { Value = value, Index = index }) // Tạo cặp (giá trị, chỉ số)
                                .OrderBy(pair => pair.Value)                                   // Sắp xếp theo giá trị
                                .Select(pair => pair.Index)                                   // Lấy lại chỉ số
                                .ToArray();
                            // Sắp xếp array1 và array2 dựa trên chỉ số đã sắp xếp
                            float[] sortedArray1 = sortedIndices.Select(index => array1[index]).ToArray();
                            Rect[] sortedArray2 = sortedIndices.Select(index => array2[index]).ToArray();
                            if (sortedArray2.Length < maxLim || sortedArray2.Length > minLim) {
                                externVal["msg"] = $"OK";
                                resCheck = true;
                                // Tính Moments cho cả template và vùng khớp
                                Moments momentsTemplate = Cv2.Moments(templateGray, true);
                                Moments momentsMatched = Cv2.Moments(matchedRegion, true);

                                // Tính tâm
                                Point2f centerTemplate = new Point2f(
                                    (float)(momentsTemplate.M10 / momentsTemplate.M00),
                                    (float)(momentsTemplate.M01 / momentsTemplate.M00)
                                );

                                Point2f centerMatched = new Point2f(
                                    (float)(momentsMatched.M10 / momentsMatched.M00),
                                    (float)(momentsMatched.M01 / momentsMatched.M00)
                                );

                                // Tính góc xoay
                                double angleTemplate = 0.5 * Math.Atan2(2 * momentsTemplate.Mu11, momentsTemplate.Mu20 - momentsTemplate.Mu02) * (180 / Math.PI);
                                double angleMatched = 0.5 * Math.Atan2(2 * momentsMatched.Mu11, momentsMatched.Mu20 - momentsMatched.Mu02) * (180 / Math.PI);

                                double rotationAngle = angleMatched - angleTemplate;
                                externVal["angle"] = rotationAngle;
                                externVal["center"] = new Point2f(maxLoc.X + template.Cols / 2, maxLoc.Y + template.Rows / 2);
                                externVal["msg"] = "OK";
                            } else {
                                externVal["msg"] = $"NG: MATCHING - TIM DUOC {nonidxSort.Count} / MIN:{minLim}, MAX:{maxLim}";
                                resCheck = false;
                            }
                            // Vị trí khớp tốt nhất (tùy phương pháp, chọn minLoc hoặc maxLoc)
                            // Vẽ hình chữ nhật quanh vùng tìm thấy
                            Cv2.Rectangle(frame, new OpenCvSharp.Point(maxLoc.X - 2, maxLoc.Y - 2), new OpenCvSharp.Point(maxLoc.X + template.Cols + 2, maxLoc.Y + template.Rows + 2), colorScarOK, fontThickness);
                        }
                        else { externVal["msg"] = "NG: MATCHING - KHONG KHOP DUOC ANH."; }
                    }
                    result?.Dispose();
                    sourceGray?.Dispose();
                    templateGray?.Dispose();
                } else { externVal["msg"] = "NG: MATCHING - ANH VA ANH MAU KHONG KHOP"; }
            } else { externVal["msg"] = "NG: MATCHING - CHUA TRAINNING ANH"; }
            Cv2.PutText(frame,  externVal["msg"].ToString(), fontPos, fontT, fontScale,  colorScarOK, fontThickness);
            cvX.ImShow(blockID, "ALIGN_DISPLAY", frame);
            return (resCheck, externVal);
        }
    }

    public class MatchingResult
    {
        public MatchingResult(float S, Point3f cp, Point2f[] box)
        {
            Score = S;
            CP = cp;
            Box = box;
        }

        public float Score { get; set; }
        public Point3f CP { get; set; }
        public Point2f[] Box { get; set; }

        //public override string ToString();
    }
}
