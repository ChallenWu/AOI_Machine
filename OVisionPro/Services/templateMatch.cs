//using OpenCvSharp;
//using System;
//using System.Collections.Generic;

//namespace OVisionPro.Services
//{
//    public class PatternData
//    {
//        public Mat PatternImage { get; set; } // Ảnh mẫu
//        public Scalar BorderColor { get; set; } // Màu viền, nếu cần thiết cho padding ảnh
//    }

//    public class MatchingResult
//    {
//        public float MatchScore { get; set; } // Điểm khớp
//        public Point3f Position { get; set; } // Vị trí (x, y, góc)
//        public Point2f[] Points { get; set; } // Các điểm trong ảnh
//        public float RotationAngle { get; set; } // Góc quay tìm thấy

//        public MatchingResult(float matchScore, Point3f position, Point2f[] points, float rotationAngle)
//        {
//            MatchScore = matchScore;
//            Position = position;
//            Points = points;
//            RotationAngle = rotationAngle;
//        }
//    }

//    public static class RunParams
//    {
//        public static bool UseEdge { get; set; } = false; // Có sử dụng xử lý biên không
//        public static int MatchModes { get; set; } = 4; // Chế độ so khớp (Cv2.TM_CCOEFF_NORMED, v.v.)
//    }

//    public static class ImageUtils
//    {
//        // Hàm phát hiện biên
//        public static void EdgeEetection(Mat inputImage, out Mat outputImage)
//        {
//            outputImage = new Mat();
//            Cv2.Canny(inputImage, outputImage, 100, 200); // Thực hiện phát hiện biên với Canny
//        }

//        // Hàm vùng (mask)
//        public static Mat Area(Mat source, Mat transferMat, out Mat mask)
//        {
//            mask = new Mat();
//            Mat result = new Mat();
//            Cv2.WarpAffine(source, result, transferMat, source.Size());
//            // Tạo mask giả sử nếu cần thiết
//            return result;
//        }

//        // Hàm xoay điểm theo góc
//        public static Point2d Rotate(Point2d point, double angle)
//        {
//            double rad = angle * Math.PI / 180.0;
//            double xNew = point.X * Math.Cos(rad) - point.Y * Math.Sin(rad);
//            double yNew = point.X * Math.Sin(rad) + point.Y * Math.Cos(rad);
//            return new Point2d(xNew, yNew);
//        }

//        // Hàm thay đổi kích thước ảnh
//        public static OpenCvSharp.Size ResizeSize(OpenCvSharp.Size originalSize, double scale)
//        {
//            return new OpenCvSharp.Size(
//                (int)(originalSize.Width * scale),
//                (int)(originalSize.Height * scale)
//            );
//        }
//    }

//    public class PatternMatcher
//    {
//        // Hàm chính thực hiện tìm kiếm mẫu
//        public bool TemplateMatchAdaptive(Mat sourceImage, PatternData patternData, Rect searchRegionAdaptive,
//            double degStartAdaptive, double degRangeAdaptive, double degStepAdaptive, double searchScaleAdaptive,
//            out MatchingResult matchingResult, double margin = 1.5, bool maskedScore = false, bool fineFind = false)
//        {
//            matchingResult = new MatchingResult(0f, default(Point3f), new Point2f[4], 0f);

//            if (searchScaleAdaptive == 0.0 || searchRegionAdaptive.Width < 0 || searchRegionAdaptive.Height < 0 ||
//                double.IsNaN(degStartAdaptive) || double.IsNaN(degRangeAdaptive) || double.IsNaN(degStepAdaptive))
//            {
//                return false;
//            }

//            degStepAdaptive = Math.Round(degStepAdaptive, 10);
//            Mat mat = new Mat(), mat2 = new Mat();
//            Point3f point3f = default(Point3f);
//            OpenCvSharp.Point center = (searchRegionAdaptive.TopLeft + searchRegionAdaptive.BottomRight) * 0.5 * searchScaleAdaptive;

//            // Khởi tạo các biến phục vụ cho việc xoay, tỷ lệ và lưu kết quả
//            double num = -1.0;
//            int width = (int)Math.Round(patternData.PatternImage.Width * searchScaleAdaptive);
//            int height = (int)Math.Round(patternData.PatternImage.Height * searchScaleAdaptive);

//            // Thay đổi kích thước mẫu
//            mat2?.Dispose();
//            mat2 = new Mat();
//            Cv2.Resize(patternData.PatternImage, mat2, new OpenCvSharp.Size(width, height));

//            // Áp dụng edge detection nếu được yêu cầu
//            if (RunParams.UseEdge)
//            {
//                ImageUtils.EdgeEetection(mat2, out mat2);
//            }

//            // Thay đổi kích thước ảnh nguồn
//            mat?.Dispose();
//            mat = new Mat();
//            Cv2.Resize(sourceImage, mat, ImageUtils.ResizeSize(sourceImage.Size(), searchScaleAdaptive));

//            // Áp dụng edge detection cho ảnh nguồn
//            if (RunParams.UseEdge)
//            {
//                ImageUtils.EdgeEetection(mat, out mat);
//            }

//            // Tính toán phạm vi tìm kiếm với các góc và tỷ lệ
//            double num2 = degRangeAdaptive / 2.0 * Math.PI / 180.0;
//            Point2d point2 = new Point2d(Math.Max(searchRegionAdaptive.Width / 2, patternData.PatternImage.Width / 2),
//                                        Math.Max(searchRegionAdaptive.Height / 2, patternData.PatternImage.Height / 2));

//            // Xác định phạm vi của vùng tìm kiếm
//            double num3 = Math.Abs(ImageUtils.Rotate(point2, 0.0 - num2).X * 2.0 * searchScaleAdaptive * margin);
//            double num4 = Math.Abs(ImageUtils.Rotate(point2, num2).Y * 2.0 * searchScaleAdaptive * margin);

//            Rect searchRect = new Rect((int)Math.Round(center.X - num3 / 2.0), (int)Math.Round(center.Y - num4 / 2.0),
//                                    (int)Math.Round(num3), (int)Math.Round(num4));

//            Mat mat3 = new Mat(searchRect.Size, sourceImage.Type());
//            Mat transferMat = new Mat(2, 3, 6, new double[] { searchScaleAdaptive, 0.0, -searchRect.X, 0.0, searchScaleAdaptive, -searchRect.Y }, 0L);

//            // Điều chỉnh phạm vi góc xoay và bước góc
//            degRangeAdaptive -= 2.0 * (degRangeAdaptive / 2.0 % degStepAdaptive);

//            // Biến dùng để theo dõi giá trị khớp cao nhất
//            int num5 = 0;
//            bool isMatchingFound = true;
//            double minVal = 0.0, maxVal = 0.0;
//            OpenCvSharp.Point minLoc = default(OpenCvSharp.Point), maxLoc = default(OpenCvSharp.Point);

//            // Lưu trữ kết quả khớp
//            List<Point2d> matchList = new List<Point2d>();
//            Mat mat4 = new Mat();
//            Mat mat5 = null;

//            // Tính toán các góc để tìm kiếm mẫu trong phạm vi xoay
//            double degStart = Math.Round((0.0 - degRangeAdaptive) / 2.0 - degStartAdaptive, 10);
//            double degEnd = Math.Round(degRangeAdaptive / 2.0 - degStartAdaptive, 10);

//            for (double angle = degStart; angle <= degEnd; angle += degStepAdaptive)
//            {
//                if (double.IsInfinity(angle) || double.IsNaN(angle))
//                {
//                    return false;
//                }

//                mat4 = Cv2.GetRotationMatrix2D(new Point2f(center.X, center.Y), -angle, 1.0);
//                mat4.Set(0, 2, mat4.At<double>(0, 2) - searchRect.X);
//                mat4.Set(1, 2, mat4.At<double>(1, 2) - searchRect.Y);

//                // Áp dụng phép xoay và lấy kết quả so khớp
//                Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant,
//                                patternData.BorderColor);

//                mat5 = new Mat(mat3.Size(), 5);
//                Cv2.MatchTemplate(mat3, mat2, mat5, TemplateMatchModes.CCoeff);

//                // Xử lý mask và trích xuất vùng khớp
//                Mat mask2;
//                Mat mat6 = ImageUtils.Area(mat5, transferMat, out mask2);
//                Cv2.MinMaxLoc(mat5, out minVal, out maxVal, out minLoc, out maxLoc, mask2);

//                mask2?.Dispose();
//                mat6?.Dispose();

//                matchList.Add(new Point2d(degEnd, maxVal));

//                if (maxVal > num)
//                {
//                    num = maxVal;
//                    num5 = matchList.Count - 1;
//                    point3f = new Point3f((float)maxLoc.X, (float)maxLoc.Y, (float)-degEnd);
//                }
//                mat5?.Dispose();
//            }

//            matchingResult = new MatchingResult((float)num, point3f, new Point2f[4], (float)num);
//            return num > 0.6;
//        }
//    }
//}
