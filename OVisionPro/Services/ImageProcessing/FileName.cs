//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace OVisionPro.Services.ImageProcessing
//{
//    private bool TempleMatch_(Mat sourceImage, PatternData patternData, Rect searchRegionAdaptive, double degStartAdaptive, double degRangeAdaptive, double degStepAdaptive, double searchScaleAdaptive, out MatchingResult matchingResult, double margin = 1.5, bool maskedScore = false, bool fineFind = false)
//    {

//        matchingResult = new MatchingResult(0f, default(Point3f), new Point2f[4], 0f);
//        if (searchScaleAdaptive == 0.0)
//        {
//            return false;
//        }
//        if (searchRegionAdaptive.Width < 0 || searchRegionAdaptive.Height < 0)
//        {
//            return false;
//        }
//        if (double.IsNaN(degStartAdaptive) || double.IsNaN(degRangeAdaptive) || double.IsNaN(degStepAdaptive))
//        {
//            return false;
//        }
//        degStepAdaptive = Math.Round(degStepAdaptive, 10);
//        Mat mat = new Mat();
//        Mat mat2 = new Mat();
//        Point3f point3f = default(Point3f);
//        _ = new Point2f[4];
//        OpenCvSharp.Point point = (searchRegionAdaptive.TopLeft + searchRegionAdaptive.BottomRight) * 0.5 * searchScaleAdaptive;
//        Point3d point3d = new Point3d(0.0, 0.0, 0.0 - degStartAdaptive);
//        double num = -1.0;
//        int width = (int)Math.Round((double)patternData.PatternImage.Width * searchScaleAdaptive);
//        int height = (int)Math.Round((double)patternData.PatternImage.Height * searchScaleAdaptive);
//        mat2?.Dispose();
//        mat2 = new Mat();
//        Cv2.Resize(patternData.PatternImage, mat2, new OpenCvSharp.Size(width, height));
//        if (RunParams.UseEdge)
//        {
//            EdgeEetection(mat2, out mat2);
//            //}
//            mat?.Dispose();
//            mat = new Mat();
//            Cv2.Resize(sourceImage, mat, ResizeSize(sourceImage.Size(), searchScaleAdaptive));
//            if (RunParams.UseEdge)
//            {
//                EdgeEetection(mat, out mat);
//            }
//            double num2 = degRangeAdaptive / 2.0 * Math.PI / 180.0;
//            Point2d point2 = new Point2d(Math.Max(searchRegionAdaptive.Width / 2, patternData.PatternImage.Width / 2), Math.Max(searchRegionAdaptive.Height / 2, patternData.PatternImage.Height / 2));
//            double num3 = Math.Abs(Rotate(point2, 0.0 - num2).X * 2.0 * searchScaleAdaptive * margin);
//            double num4 = Math.Abs(Rotate(point2, num2).Y * 2.0 * searchScaleAdaptive * margin);
//            Rect rect = default(Rect);
//            rect.X = (int)Math.Round((double)point.X - num3 / 2.0);
//            rect.Y = (int)Math.Round((double)point.Y - num4 / 2.0);
//            rect.Size = new OpenCvSharp.Size(num3, num4);
//            Mat mat3 = new Mat(rect.Size, sourceImage.Type());
//            Mat transferMat = new Mat(2, 3, 6, new double[6]
//            {
//            searchScaleAdaptive,
//            0.0,
//            -rect.X,
//            0.0,
//            searchScaleAdaptive,
//            -rect.Y
//            }, 0L);
//            if (degStepAdaptive <= 0.0)
//            {
//                degRangeAdaptive = 0.0;
//                degStepAdaptive = 1.0;
//            }
//            else
//            {
//                degRangeAdaptive -= 2.0 * (degRangeAdaptive / 2.0 % degStepAdaptive);
//            }
//            int num5 = 0;
//            bool flag = true;
//            double minVal = 0.0;
//            double maxVal = 0.0;
//            OpenCvSharp.Point minLoc = default(OpenCvSharp.Point);
//            OpenCvSharp.Point maxLoc = default(OpenCvSharp.Point);
//            List<Point2d> list = new List<Point2d>();
//            Mat mat4 = new Mat();
//            Mat mat5 = null;
//            double num6 = Math.Round((0.0 - degRangeAdaptive) / 2.0 - degStartAdaptive, 10);
//            double num7 = Math.Round(degRangeAdaptive / 2.0 - degStartAdaptive, 10);
//            for (double num8 = num6; num8 <= num7; num8 += degStepAdaptive)
//            {
//                if (double.IsInfinity(num8) || double.IsNaN(num8))
//                {
//                    return false;
//                }
//                mat4 = Cv2.GetRotationMatrix2D(new Point2f(point.X, point.Y), 0.0 - num8, 1.0);
//                mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
//                mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
//                Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, patternData.BorderColor);
//                mat5 = new Mat();
//                Cv2.MatchTemplate(mat3, mat2, mat5, RunParams.MatchModes);
//                Cv2.MinMaxLoc(Area(mat5, transferMat, out var mask), out minVal, out maxVal, out minLoc, out maxLoc, mask);
//                mat5?.Dispose();
//                mask?.Dispose();
//                double num9 = 0.9;
//                list.Add(new Point2d(num8, maxVal));
//                if (maxVal > num)
//                {
//                    num = maxVal;
//                    num5 = list.Count - 1;
//                    point3d.X = maxLoc.X;
//                    point3d.Y = maxLoc.Y;
//                    point3d.Z = num8;
//                    if (num > num9)
//                    {
//                        flag = false;
//                    }
//                }
//                else if (!flag)
//                {
//                    break;
//                }
//            }
//            if (list.Count > 1)
//            {
//                double num10 = num6;
//                int num11 = 0;
//                while (num5 == 0 && num11 < 5)
//                {
//                    num10 -= degStepAdaptive;
//                    mat4 = Cv2.GetRotationMatrix2D(new Point2f(rect.X + rect.Width / 2, rect.Y + rect.Height / 2), 0.0 - num10, 1.0);
//                    mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
//                    mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
//                    Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, patternData.BorderColor);
//                    mat5 = new Mat(mat3.Size(), 5);
//                    Cv2.MatchTemplate(mat3, mat2, mat5, RunParams.MatchModes);
//                    Mat mask2;
//                    Mat mat6 = Area(mat5, transferMat, out mask2);
//                    Cv2.MinMaxLoc(mat5, out minVal, out maxVal, out minLoc, out maxLoc, mask2);
//                    mask2?.Dispose();
//                    mat6?.Dispose();
//                    list.Insert(0, new Point2d(num10, maxVal));
//                    num5++;
//                    if (maxVal > num)
//                    {
//                        num = maxVal;
//                        num5 = 0;
//                        point3d.X = maxLoc.X;
//                        point3d.Y = maxLoc.Y;
//                        point3d.Z = num10;
//                        num5 = 0;
//                    }
//                    mat5.Dispose();
//                    num11++;
//                }
//                num10 = num7;
//                num11 = 0;
//                while (num5 == list.Count - 1 && num11 < 5)
//                {
//                    num10 += degStepAdaptive;
//                    mat4 = Cv2.GetRotationMatrix2D(new Point2f(rect.X + rect.Width / 2, rect.Y + rect.Height / 2), 0.0 - num10, 1.0);
//                    mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
//                    mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
//                    Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, patternData.BorderColor);
//                    mat5 = new Mat(mat3.Size(), 5);
//                    Cv2.MatchTemplate(mat3, mat2, mat5, RunParams.MatchModes);
//                    Mat mask3;
//                    Mat mat7 = Area(mat5, transferMat, out mask3);
//                    Cv2.MinMaxLoc(mat5, out minVal, out maxVal, out minLoc, out maxLoc, mask3);
//                    mask3?.Dispose();
//                    mat7?.Dispose();
//                    list.Add(new Point2d(num10, maxVal));
//                    if (maxVal > num)
//                    {
//                        num = maxVal;
//                        num5 = list.Count - 1;
//                        point3d.X = maxLoc.X;
//                        point3d.Y = maxLoc.Y;
//                        point3d.Z = num10;
//                    }
//                    mat5.Dispose();
//                    num11++;
//                }
//                double num12 = double.MaxValue;
//                maxVal = double.MaxValue;
//                Point2d point2d = default(Point2d);
//                Mat mat8 = null;
//                num11 = 0;
//                while (num12 > degStepAdaptive / 5.0 && num11 < 5)
//                {
//                    double num13 = list[num5].X;
//                    point2d = Localmaximum(list, num5);
//                    num12 = Math.Abs(num13 - point2d.X);
//                    mat4 = Cv2.GetRotationMatrix2D(new Point2f(point.X, point.Y), 0.0 - point2d.X, 1.0);
//                    mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
//                    mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
//                    Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, patternData.BorderColor);
//                    mat8 = new Mat(mat3.Size(), 5);
//                    Cv2.MatchTemplate(mat3, mat2, mat8, RunParams.MatchModes);
//                    Mat mask4;
//                    Mat mat9 = Area(mat8, transferMat, out mask4);
//                    Cv2.MinMaxLoc(mat9, out minVal, out maxVal, out minLoc, out maxLoc, mask4);
//                    mask4?.Dispose();
//                    mat9?.Dispose();
//                    if (!(maxVal > num))
//                    {
//                        break;
//                    }
//                    num = maxVal;
//                    list.Add(new Point2d(point2d.X, maxVal));
//                    list.Sort((Point2d a, Point2d b) => (!(a.X < b.X)) ? 1 : (-1));
//                    num5 = list.FindIndex((Point2d a) => a.Y == maxVal);
//                    point3d.X = maxLoc.X;
//                    point3d.Y = maxLoc.Y;
//                    point3d.Z = point2d.X;
//                    if (Count == 0)
//                    {
//                        break;
//                    }
//                    num11++;
//                }
//                Point2d point2d2 = gradient(mat8, new OpenCvSharp.Point(point3d.X, point3d.Y));
//                point3d.X = point2d2.X;
//                point3d.Y = point2d2.Y;
//                point3d.Z = point2d.X;
//                num = maxVal;
//            }
//            //_ = RunParams.Algorithm == EAlgorithm.EdgeMatching && maskedScore;
//            double num14 = point3d.Z * Math.PI / 180.0;
//            double num15 = ((double)point.X + (point3d.X - (double)(rect.Width / 2)) * Math.Cos(num14) + (point3d.Y - (double)(rect.Height / 2)) * Math.Sin(num14)) / searchScaleAdaptive;
//            double num16 = ((double)point.Y - (point3d.X - (double)(rect.Width / 2)) * Math.Sin(num14) + (point3d.Y - (double)(rect.Height / 2)) * Math.Cos(num14)) / searchScaleAdaptive;
//            point3f = new Point3f((float)num15, (float)num16, (float)(0.0 - num14));
//            mat3.Dispose();
//            matchingResult.Score = (float)num;
//            matchingResult.Box = Rectanglepoimts(point3f, patternData.PatternImage.Width, patternData.PatternImage.Height);
//            matchingResult.CP = (point3f + new Point3f(matchingResult.Box[2].X, matchingResult.Box[2].Y, point3f.Z)) * 0.5;
//            matchingResult.CP = GetRectangleCenter(matchingResult.Box, point3f.Z);


//            return true;
//        }


//public (bool, Dictionary<string, object>) PerformPatternMatching(Mat sourceImage, string blockID)
//    {
//        Dictionary<string, object> externVal = new Dictionary<string, object> { };
//        externVal["angle"] = null;
//        externVal["center"] = null;
//        Rect searchRegionAdaptive = new Rect(10, 10, sourceImage.Width - 10, sourceImage.Height - 10);
//        double degStartAdaptive = 1;
//        double degRangeAdaptive = 45;
//        double degStepAdaptive = 1;
//        double searchScaleAdaptive = 1;
//        double margin = 1.5;
//        bool maskedScore = false;
//        bool fineFind = false;
//        MatchingResult matchingResult = new MatchingResult(0f, default(Point3f), new Point2f[4]);
//        if (searchScaleAdaptive == 0.0)
//        {
//            return (false, externVal);
//        }
//        if (searchRegionAdaptive.Width < 0 || searchRegionAdaptive.Height < 0)
//        {
//            return (false, externVal);
//        }
//        if (double.IsNaN(degStartAdaptive) || double.IsNaN(degRangeAdaptive) || double.IsNaN(degStepAdaptive))
//        {
//            return (false, externVal);
//        }
//        // Load ?nh ngu?n và ?nh m?u
//        if (!XImgPatternManager.Instance.ImgMatPatternData.ContainsKey(blockID)) return (false, externVal);
//        Mat patternimg = XImgPatternManager.Instance.ImgMatPatternData[blockID];
//        if (patternimg == null)
//        {
//            return (false, externVal);
//        }
//        degStepAdaptive = Math.Round(degStepAdaptive, 10);
//        Mat mat = new Mat();
//        Mat mat2 = new Mat();
//        Point3f point3f = default(Point3f);
//        _ = new Point2f[4];
//        OpenCvSharp.Point point = (searchRegionAdaptive.TopLeft + searchRegionAdaptive.BottomRight) * 0.5 * searchScaleAdaptive;
//        Point3d point3d = new Point3d(0.0, 0.0, 0.0 - degStartAdaptive);
//        double num = -1.0;
//        int width = (int)Math.Round((double)patternimg.Width * searchScaleAdaptive);
//        int height = (int)Math.Round((double)patternimg.Height * searchScaleAdaptive);
//        mat2?.Dispose();
//        mat2 = new Mat();
//        Cv2.Resize(patternimg, mat2, new OpenCvSharp.Size(width, height));
//        //if (RunParams.UseEdge)
//        //{
//        //    EdgeEetection(mat2, out mat2);
//        //}
//        mat?.Dispose();
//        mat = new Mat();
//        Cv2.Resize(sourceImage, mat, sourceImage.Size());
//        //if (RunParams.UseEdge)
//        //{
//        //    EdgeEetection(mat, out mat);
//        //}
//        double num2 = degRangeAdaptive / 2.0 * Math.PI / 180.0;
//        Point2d point2 = new Point2d(Math.Max(searchRegionAdaptive.Width / 2, patternimg.Width / 2), Math.Max(searchRegionAdaptive.Height / 2, patternimg.Height / 2));
//        double num3 = Math.Abs(OBasicAlgorithm.Rotate(point2, 0.0 - num2).X * 2.0 * searchScaleAdaptive * margin);
//        double num4 = Math.Abs(OBasicAlgorithm.Rotate(point2, num2).Y * 2.0 * searchScaleAdaptive * margin);
//        Rect rect = default(Rect);
//        rect.X = (int)Math.Round((double)point.X - num3 / 2.0);
//        rect.Y = (int)Math.Round((double)point.Y - num4 / 2.0);
//        rect.Size = new OpenCvSharp.Size(num3, num4);
//        Mat mat3 = new Mat(rect.Size, sourceImage.Type());
//        Mat transferMat = new Mat(2, 3, 6, new double[6]
//        {
//            searchScaleAdaptive,
//            0.0,
//            -rect.X,
//            0.0,
//            searchScaleAdaptive,
//            -rect.Y
//        }, 0L);
//        if (degStepAdaptive <= 0.0)
//        {
//            degRangeAdaptive = 0.0;
//            degStepAdaptive = 1.0;
//        }
//        else
//        {
//            degRangeAdaptive -= 2.0 * (degRangeAdaptive / 2.0 % degStepAdaptive);
//        }
//        int num5 = 0;
//        bool flag = true;
//        double minVal = 0.0;
//        double maxVal = 0.0;
//        OpenCvSharp.Point minLoc = default(OpenCvSharp.Point);
//        OpenCvSharp.Point maxLoc = default(OpenCvSharp.Point);
//        List<Point2d> list = new List<Point2d>();
//        Mat mat4 = new Mat();
//        Mat mat5 = null;
//        double num6 = Math.Round((0.0 - degRangeAdaptive) / 2.0 - degStartAdaptive, 10);
//        double num7 = Math.Round(degRangeAdaptive / 2.0 - degStartAdaptive, 10);
//        for (double num8 = num6; num8 <= num7; num8 += degStepAdaptive)
//        {
//            if (double.IsInfinity(num8) || double.IsNaN(num8))
//            {
//                return (false, externVal);
//            }
//            mat4 = Cv2.GetRotationMatrix2D(new Point2f(point.X, point.Y), 0.0 - num8, 1.0);
//            mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
//            mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
//            Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, OpenCvSharp.Scalar.Red);
//            mat5 = new Mat();
//            Cv2.MatchTemplate(mat3, mat2, mat5, OpenCvSharp.TemplateMatchModes.CCoeffNormed);
//            Cv2.MinMaxLoc(ApplyMask(mat5, transferMat, out var mask), out minVal, out maxVal, out minLoc, out maxLoc, mask);
//            mat5?.Dispose();
//            mask?.Dispose();
//            double num9 = 0.9;
//            list.Add(new Point2d(num8, maxVal));
//            if (maxVal > num)
//            {
//                num = maxVal;
//                num5 = list.Count - 1;
//                point3d.X = maxLoc.X;
//                point3d.Y = maxLoc.Y;
//                point3d.Z = num8;
//                if (num > num9)
//                {
//                    flag = false;
//                }
//            }
//            else if (!flag)
//            {
//                break;
//            }
//        }
//        if (list.Count > 1)
//        {
//            double num10 = num6;
//            int num11 = 0;
//            while (num5 == 0 && num11 < 5)
//            {
//                num10 -= degStepAdaptive;
//                mat4 = Cv2.GetRotationMatrix2D(new Point2f(rect.X + rect.Width / 2, rect.Y + rect.Height / 2), 0.0 - num10, 1.0);
//                mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
//                mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
//                Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, OpenCvSharp.Scalar.Red);
//                mat5 = new Mat(mat3.Size(), 5);
//                Cv2.MatchTemplate(mat3, mat2, mat5, OpenCvSharp.TemplateMatchModes.CCoeffNormed);
//                Mat mask2;
//                Mat mat6 = ApplyMask(mat5, transferMat, out mask2);
//                Cv2.MinMaxLoc(mat5, out minVal, out maxVal, out minLoc, out maxLoc, mask2);
//                mask2?.Dispose();
//                mat6?.Dispose();
//                list.Insert(0, new Point2d(num10, maxVal));
//                num5++;
//                if (maxVal > num)
//                {
//                    num = maxVal;
//                    num5 = 0;
//                    point3d.X = maxLoc.X;
//                    point3d.Y = maxLoc.Y;
//                    point3d.Z = num10;
//                    num5 = 0;
//                }
//                mat5.Dispose();
//                num11++;
//            }
//            num10 = num7;
//            num11 = 0;
//            while (num5 == list.Count - 1 && num11 < 5)
//            {
//                num10 += degStepAdaptive;
//                mat4 = Cv2.GetRotationMatrix2D(new Point2f(rect.X + rect.Width / 2, rect.Y + rect.Height / 2), 0.0 - num10, 1.0);
//                mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
//                mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
//                Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, OpenCvSharp.Scalar.Red);
//                mat5 = new Mat(mat3.Size(), 5);
//                Cv2.MatchTemplate(mat3, mat2, mat5, OpenCvSharp.TemplateMatchModes.CCoeffNormed);
//                Mat mask3;
//                Mat mat7 = ApplyMask(mat5, transferMat, out mask3);
//                Cv2.MinMaxLoc(mat5, out minVal, out maxVal, out minLoc, out maxLoc, mask3);
//                mask3?.Dispose();
//                mat7?.Dispose();
//                list.Add(new Point2d(num10, maxVal));
//                if (maxVal > num)
//                {
//                    num = maxVal;
//                    num5 = list.Count - 1;
//                    point3d.X = maxLoc.X;
//                    point3d.Y = maxLoc.Y;
//                    point3d.Z = num10;
//                }
//                mat5.Dispose();
//                num11++;
//            }
//            double num12 = double.MaxValue;
//            maxVal = double.MaxValue;
//            Point2d point2d = default(Point2d);
//            Mat mat8 = null;
//            num11 = 0;
//            while (num12 > degStepAdaptive / 5.0 && num11 < 5)
//            {
//                double num13 = list[num5].X;
//                point2d = CalculateAdjustedPoint(list, num5);
//                num12 = Math.Abs(num13 - point2d.X);
//                mat4 = Cv2.GetRotationMatrix2D(new Point2f(point.X, point.Y), 0.0 - point2d.X, 1.0);
//                mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
//                mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
//                Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, OpenCvSharp.Scalar.Red);
//                mat8 = new Mat(mat3.Size(), 5);
//                Cv2.MatchTemplate(mat3, mat2, mat8, OpenCvSharp.TemplateMatchModes.CCoeffNormed);
//                Mat mask4;
//                Mat mat9 = ApplyMask(mat8, transferMat, out mask4);
//                Cv2.MinMaxLoc(mat9, out minVal, out maxVal, out minLoc, out maxLoc, mask4);
//                mask4?.Dispose();
//                mat9?.Dispose();
//                if (!(maxVal > num))
//                {
//                    break;
//                }
//                num = maxVal;
//                list.Add(new Point2d(point2d.X, maxVal));
//                list.Sort((Point2d a, Point2d b) => (!(a.X < b.X)) ? 1 : (-1));
//                num5 = list.FindIndex((Point2d a) => a.Y == maxVal);
//                point3d.X = maxLoc.X;
//                point3d.Y = maxLoc.Y;
//                point3d.Z = point2d.X;
//                num11++;
//            }
//            Point2d point2d2 = CalculateFinalPoint(mat8, new OpenCvSharp.Point(point3d.X, point3d.Y));
//            point3d.X = point2d2.X;
//            point3d.Y = point2d2.Y;
//            point3d.Z = point2d.X;
//            num = maxVal;
//        }
//        //_ = RunParams.Algorithm == EAlgorithm.EdgeMatching && maskedScore;
//        double num14 = point3d.Z * Math.PI / 180.0;
//        double num15 = ((double)point.X + (point3d.X - (double)(rect.Width / 2)) * Math.Cos(num14) + (point3d.Y - (double)(rect.Height / 2)) * Math.Sin(num14)) / searchScaleAdaptive;
//        double num16 = ((double)point.Y - (point3d.X - (double)(rect.Width / 2)) * Math.Sin(num14) + (point3d.Y - (double)(rect.Height / 2)) * Math.Cos(num14)) / searchScaleAdaptive;
//        point3f = new Point3f((float)num15, (float)num16, (float)(0.0 - num14));
//        mat3.Dispose();
//        matchingResult.Score = (float)num;
//        matchingResult.Box = CalculatePatternBox(point3f, patternimg.Width, patternimg.Height);
//        matchingResult.CP = (point3f + new Point3f(matchingResult.Box[2].X, matchingResult.Box[2].Y, point3f.Z)) * 0.5;
//        //matchingResult.CP = GetRectangleCenter(matchingResult.Box, point3f.Z);

//        Cv2.Circle(sourceImage, new OpenCvSharp.Point(point3f.X, (int)point3f.Y), 13, new OpenCvSharp.Scalar(0, 0, 255), 15);
//        Cv2.ImShow("ALIGN_DISPLAY", sourceImage);
//        Cv2.WaitKey(10);
//        return (false, externVal);
//    }




//    public (bool, Dictionary<string, object>) PerformPatternMatching(Mat sourceImage, string blockID)
//    {
//        Dictionary<string, object> externVal = new Dictionary<string, object> { };
//        externVal["angle"] = null;
//        externVal["center"] = null;
//        Rect searchRegionAdaptive = new Rect(10, 10, sourceImage.Width - 10, sourceImage.Height - 10);
//        double degStartAdaptive = 0;
//        double degRangeAdaptive = 40;
//        double degStepAdaptive = 5;
//        double searchScaleAdaptive = 0.2;
//        double margin = 1.5;
//        bool maskedScore = false;
//        bool fineFind = false;
//        MatchingResult matchingResult = new MatchingResult(0f, default(Point3f), new Point2f[4]);
//        if (searchScaleAdaptive == 0.0)
//        {
//            return (false, externVal);
//        }
//        if (searchRegionAdaptive.Width < 0 || searchRegionAdaptive.Height < 0)
//        {
//            return (false, externVal);
//        }
//        if (double.IsNaN(degStartAdaptive) || double.IsNaN(degRangeAdaptive) || double.IsNaN(degStepAdaptive))
//        {
//            return (false, externVal);
//        }
//        // Load ?nh ngu?n và ?nh m?u
//        if (!XImgPatternManager.Instance.ImgMatPatternData.ContainsKey(blockID)) return (false, externVal);
//        Mat patternimg = XImgPatternManager.Instance.ImgMatPatternData[blockID];
//        if (patternimg == null)
//        {
//            return (false, externVal);
//        }
//        degStepAdaptive = Math.Round(degStepAdaptive, 10);
//        Mat mat = new Mat();
//        Mat mat2 = new Mat();
//        Point3f point3f = default(Point3f);
//        _ = new Point2f[4];
//        OpenCvSharp.Point point = (searchRegionAdaptive.TopLeft + searchRegionAdaptive.BottomRight) * 0.5 * searchScaleAdaptive;
//        Point3d point3d = new Point3d(0.0, 0.0, 0.0 - degStartAdaptive);
//        double num = -1.0;
//        int width = (int)Math.Round((double)patternimg.Width * searchScaleAdaptive);
//        int height = (int)Math.Round((double)patternimg.Height * searchScaleAdaptive);
//        mat2?.Dispose();
//        mat2 = new Mat();
//        Cv2.Resize(patternimg, mat2, new OpenCvSharp.Size(width, height));
//        //if (RunParams.UseEdge)
//        //{
//        //    EdgeEetection(mat2, out mat2);
//        //}
//        mat?.Dispose();
//        mat = new Mat();
//        Cv2.Resize(sourceImage, mat, new OpenCvSharp.Size(sourceImage.Width * searchScaleAdaptive, height * searchScaleAdaptive));
//        //if (RunParams.UseEdge)
//        //{
//        //    EdgeEetection(mat, out mat);
//        //}
//        double num2 = degRangeAdaptive / 2.0 * Math.PI / 180.0;
//        Point2d point2 = new Point2d(Math.Max(searchRegionAdaptive.Width / 2, patternimg.Width / 2), Math.Max(searchRegionAdaptive.Height / 2, patternimg.Height / 2));
//        double num3 = Math.Abs(OBasicAlgorithm.Rotate(point2, 0.0 - num2).X * 2.0 * searchScaleAdaptive * margin);
//        double num4 = Math.Abs(OBasicAlgorithm.Rotate(point2, num2).Y * 2.0 * searchScaleAdaptive * margin);
//        Rect rect = default(Rect);
//        rect.X = (int)Math.Round((double)point.X - num3 / 2.0);
//        rect.Y = (int)Math.Round((double)point.Y - num4 / 2.0);
//        rect.Size = new OpenCvSharp.Size(num3, num4);
//        Mat mat3 = new Mat(rect.Size, sourceImage.Type());
//        Mat transferMat = new Mat(2, 3, 6, new double[6]
//        {
//            searchScaleAdaptive,
//            0.0,
//            -rect.X,
//            0.0,
//            searchScaleAdaptive,
//            -rect.Y
//        }, 0L);
//        if (degStepAdaptive <= 0.0)
//        {
//            degRangeAdaptive = 0.0;
//            degStepAdaptive = 1.0;
//        }
//        else
//        {
//            degRangeAdaptive -= 2.0 * (degRangeAdaptive / 2.0 % degStepAdaptive);
//        }
//        int num5 = 0;
//        bool flag = true;
//        double minVal = 0.0;
//        double maxVal = 0.0;
//        OpenCvSharp.Point minLoc = default(OpenCvSharp.Point);
//        OpenCvSharp.Point maxLoc = default(OpenCvSharp.Point);
//        List<Point2d> list = new List<Point2d>();
//        Mat mat4 = new Mat();
//        Mat mat5 = null;
//        double num6 = Math.Round((0.0 - degRangeAdaptive) / 2.0 - degStartAdaptive, 10);
//        double num7 = Math.Round(degRangeAdaptive / 2.0 - degStartAdaptive, 10);
//        for (double num8 = num6; num8 <= num7; num8 += degStepAdaptive)
//        {
//            if (double.IsInfinity(num8) || double.IsNaN(num8))
//            {
//                return (false, externVal);
//            }
//            mat4 = Cv2.GetRotationMatrix2D(new Point2f(point.X, point.Y), 0.0 - num8, 1.0);
//            mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
//            mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
//            Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, OpenCvSharp.Scalar.Red);
//            mat5 = new Mat();
//            Cv2.MatchTemplate(mat3, mat2, mat5, OpenCvSharp.TemplateMatchModes.CCoeffNormed);
//            Cv2.MinMaxLoc(ApplyMask(mat5, transferMat, out var mask), out minVal, out maxVal, out minLoc, out maxLoc, mask);
//            mat5?.Dispose();
//            mask?.Dispose();
//            double num9 = 0.9;
//            list.Add(new Point2d(num8, maxVal));
//            if (maxVal > num)
//            {
//                num = maxVal;
//                num5 = list.Count - 1;
//                point3d.X = maxLoc.X;
//                point3d.Y = maxLoc.Y;
//                point3d.Z = num8;
//                if (num > num9)
//                {
//                    flag = false;
//                }
//            }
//            else if (!flag)
//            {
//                break;
//            }
//        }
//        if (list.Count > 1)
//        {
//            double num10 = num6;
//            int num11 = 0;
//            while (num5 == 0 && num11 < 5)
//            {
//                num10 -= degStepAdaptive;
//                mat4 = Cv2.GetRotationMatrix2D(new Point2f(rect.X + rect.Width / 2, rect.Y + rect.Height / 2), 0.0 - num10, 1.0);
//                mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
//                mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
//                Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, OpenCvSharp.Scalar.Red);
//                mat5 = new Mat(mat3.Size(), 5);
//                Cv2.MatchTemplate(mat3, mat2, mat5, OpenCvSharp.TemplateMatchModes.CCoeffNormed);
//                Mat mask2;
//                Mat mat6 = ApplyMask(mat5, transferMat, out mask2);
//                Cv2.MinMaxLoc(mat5, out minVal, out maxVal, out minLoc, out maxLoc, mask2);
//                mask2?.Dispose();
//                mat6?.Dispose();
//                list.Insert(0, new Point2d(num10, maxVal));
//                num5++;
//                if (maxVal > num)
//                {
//                    num = maxVal;
//                    num5 = 0;
//                    point3d.X = maxLoc.X;
//                    point3d.Y = maxLoc.Y;
//                    point3d.Z = num10;
//                    num5 = 0;
//                }
//                mat5.Dispose();
//                num11++;
//            }
//            num10 = num7;
//            num11 = 0;
//            while (num5 == list.Count - 1 && num11 < 5)
//            {
//                num10 += degStepAdaptive;
//                mat4 = Cv2.GetRotationMatrix2D(new Point2f(rect.X + rect.Width / 2, rect.Y + rect.Height / 2), 0.0 - num10, 1.0);
//                mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
//                mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
//                Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, OpenCvSharp.Scalar.Red);
//                mat5 = new Mat(mat3.Size(), 5);
//                Cv2.MatchTemplate(mat3, mat2, mat5, OpenCvSharp.TemplateMatchModes.CCoeffNormed);
//                Mat mask3;
//                Mat mat7 = ApplyMask(mat5, transferMat, out mask3);
//                Cv2.MinMaxLoc(mat5, out minVal, out maxVal, out minLoc, out maxLoc, mask3);
//                mask3?.Dispose();
//                mat7?.Dispose();
//                list.Add(new Point2d(num10, maxVal));
//                if (maxVal > num)
//                {
//                    num = maxVal;
//                    num5 = list.Count - 1;
//                    point3d.X = maxLoc.X;
//                    point3d.Y = maxLoc.Y;
//                    point3d.Z = num10;
//                }
//                mat5.Dispose();
//                num11++;
//            }
//            double num12 = double.MaxValue;
//            maxVal = double.MaxValue;
//            Point2d point2d = default(Point2d);
//            Mat mat8 = null;
//            num11 = 0;
//            while (num12 > degStepAdaptive / 5.0 && num11 < 5)
//            {
//                double num13 = list[num5].X;
//                point2d = CalculateAdjustedPoint(list, num5);
//                num12 = Math.Abs(num13 - point2d.X);
//                mat4 = Cv2.GetRotationMatrix2D(new Point2f(point.X, point.Y), 0.0 - point2d.X, 1.0);
//                mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
//                mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
//                Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, OpenCvSharp.Scalar.Red);
//                mat8 = new Mat(mat3.Size(), 5);
//                Cv2.MatchTemplate(mat3, mat2, mat8, OpenCvSharp.TemplateMatchModes.CCoeffNormed);
//                Mat mask4;
//                Mat mat9 = ApplyMask(mat8, transferMat, out mask4);
//                Cv2.MinMaxLoc(mat9, out minVal, out maxVal, out minLoc, out maxLoc, mask4);
//                mask4?.Dispose();
//                mat9?.Dispose();
//                if (!(maxVal > num))
//                {
//                    break;
//                }
//                num = maxVal;
//                list.Add(new Point2d(point2d.X, maxVal));
//                list.Sort((Point2d a, Point2d b) => (!(a.X < b.X)) ? 1 : (-1));
//                num5 = list.FindIndex((Point2d a) => a.Y == maxVal);
//                point3d.X = maxLoc.X;
//                point3d.Y = maxLoc.Y;
//                point3d.Z = point2d.X;
//                num11++;
//            }
//            Point2d point2d2 = CalculateFinalPoint(mat8, new OpenCvSharp.Point(point3d.X, point3d.Y));
//            point3d.X = point2d2.X;
//            point3d.Y = point2d2.Y;
//            point3d.Z = point2d.X;
//            num = maxVal;
//        }
//        //_ = RunParams.Algorithm == EAlgorithm.EdgeMatching && maskedScore;
//        double num14 = point3d.Z * Math.PI / 180.0;
//        double num15 = ((double)point.X + (point3d.X - (double)(rect.Width / 2)) * Math.Cos(num14) + (point3d.Y - (double)(rect.Height / 2)) * Math.Sin(num14)) / searchScaleAdaptive;
//        double num16 = ((double)point.Y - (point3d.X - (double)(rect.Width / 2)) * Math.Sin(num14) + (point3d.Y - (double)(rect.Height / 2)) * Math.Cos(num14)) / searchScaleAdaptive;
//        point3f = new Point3f((float)num15, (float)num16, (float)(0.0 - num14));
//        mat3.Dispose();
//        matchingResult.Score = (float)num;
//        matchingResult.Box = CalculatePatternBox(point3f, patternimg.Width, patternimg.Height);
//        matchingResult.CP = (point3f + new Point3f(matchingResult.Box[2].X, matchingResult.Box[2].Y, point3f.Z)) * 0.5;
//        //matchingResult.CP = GetRectangleCenter(matchingResult.Box, point3f.Z);

//        Cv2.Circle(sourceImage, new OpenCvSharp.Point(matchingResult.CP.X, matchingResult.CP.Y), 13, new OpenCvSharp.Scalar(0, 0, 255), 15);
//        cvX.ImShow(blockID, "ALIGN_DISPLAY", sourceImage);
//        return (false, externVal);
//    }

//}
