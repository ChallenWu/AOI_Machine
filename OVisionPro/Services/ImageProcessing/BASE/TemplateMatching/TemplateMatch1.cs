using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace OVisionPro.Services.ImageProcessing.BASE.TemplateMatching
{
    public class TemplateMatch1
    {
        ///  <summary>
        ///     Find components basic.
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="blockID"></param>
        /// <returns></returns>
        /// 

        //private protected OParameters runParams;
        private MatchingResult matchingResult;
        //private PatternData matchedPattern;
        private int selectedPatternIndex;
        private protected Rect matchingArea;
        private List<Point3f> calibrationPoints;
        private int processedImageIndex;
        private double scoreLimit = 0.7;

        double scaleFirst = 5.0;
        double scaleLast = 1.0;
        int angleNeg = -20;
        int anglePos = 20;
        double firstStep = 5.0;
        double precision = 0.005;
        bool useEdge = false;
        string Algorithm = "MaxAccuracy";
        public OpenCvSharp.Point3f origin = new OpenCvSharp.Point3f(100f, 100f, 100f);
        PatternData patternData;
        public TemplateMatch1()
        {
            UpdateParamMatch("MaxAccuracy");
        }

        private readonly static TemplateMatch3 instance = new TemplateMatch3();
        public static TemplateMatch3 Instance
        {
            get { return instance; }
        }
        public int AngleNeg
        {
            get => this.angleNeg;
            set
            {
                if (this.angleNeg == value)
                    return;
                this.angleNeg = value;
            }
        }
        public int AnglePos
        {
            get => this.anglePos;
            set
            {
                if (this.anglePos == value)
                    return;
                this.anglePos = value;
            }
        }
        public double FirstStep
        {
            get => this.firstStep;
            set
            {
                if (this.firstStep == value)
                    return;
                this.firstStep = value;
            }
        }
        public OpenCvSharp.Point3f Origin
        {
            get => this.origin;
            set
            {
                if (this.origin == value)
                    return;
                this.origin = value;
            }
        }

        public double ScaleFirst
        {
            get => this.scaleFirst;
            set
            {
                if (this.scaleFirst == value)
                    return;
                this.scaleFirst = value;
            }
        }

        [Browsable(false)]
        public double ScaleLast
        {
            get => this.scaleLast;
            set
            {
                if (this.scaleLast == value)
                    return;
                this.scaleLast = value;
            }
        }
        public bool UseEdge
        {
            get => this.useEdge;
            set
            {
                if (this.useEdge == value)
                    return;
                this.useEdge = value;
            }
        }
        public double Precision
        {
            get => this.precision;
            set
            {
                if (this.precision == value)
                    return;
                this.precision = value;
            }
        }
        public double ScoreLimit
        {
            get => this.scoreLimit;
            set
            {
                if (this.scoreLimit == value || value < 0.01 || value > 1.0)
                    return;
                this.scoreLimit = value;
            }
        }

        public void UpdateParamMatch(string algorithm = "QuickFinding")
        {
            this.ScaleLast = 2.0;
            this.Precision = 0.05;
            if (algorithm == "QuickFinding")
            {
                this.ScaleLast = 2.0;
                this.Precision = 0.05;
            }
            else if (algorithm == "MaxAccuracy")
            {
                this.ScaleLast = 1.0;
                this.Precision = 0.005;
            }
            else if (algorithm == "EdgeMatching")
            {
                this.ScaleLast = 1.0;
                this.Precision = 0.01;
            }
        }
        int Count;
        public (bool, Dictionary<string, object>) TemplMatchO(Mat sourceImage, string blockID)
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


            cvX.ImShow(blockID, "ORG_DISPLAY", sourceImage);
            // Load ảnh nguồn và ảnh mẫu
            if (XImgPatternManager.Instance.ImgMatPatternData.ContainsKey(blockID))
            {
                Mat patternImg = XImgPatternManager.Instance.ImgMatPatternData[blockID];
                if (OBasicAlgorithm.IsMat(patternImg) && OBasicAlgorithm.IsMat(sourceImage))
                {
                    if (patternData == null)
                    {
                        patternData?.Dispose();
                        patternData = new PatternData(patternImg, new Point3f((float)(patternImg.Height / 2), (float)(patternImg.Width / 2), 0.0f));
                    }
                    Point3f patternCP = (OBasicAlgorithm.IsMat(patternImg)) ? new Point3f((float)(patternImg.Height / 2), (float)(patternImg.Width / 2), 0.0f) : new Point3f(0, 0, 0.0f);
                    Rect searchingRegion = new Rect(0, 0, sourceImage.Width, sourceImage.Height);
                    matchingArea = searchingRegion;
                    double num = Math.Max(FirstStep, Precision);
                    double num2 = AnglePos - AngleNeg;
                    double num3 = (AnglePos + AngleNeg) / 2;
                    double num4 = 1.0 / ScaleFirst;
                    Rect searchRegionAdaptive = searchingRegion;
                    double num5 = 0.0;
                    double margin = 1.1;
                    Count = 0;
                    MatchingResult matchResult = new MatchingResult(0f, default(Point3f), new Point2f[4]);
                    MatchingResult matchingResult;
                    do
                    {
                        double num6 = num3;
                        TempleMatch_(sourceImage, patternData, searchRegionAdaptive, num3, num2, num, num4, out matchingResult, margin);
                        searchRegionAdaptive = new Rect((int)Math.Round((double)matchingResult.CP.X - 1.5 * (double)patternData.PatternImage.Width / 2.0), (int)Math.Round((double)matchingResult.CP.Y - 1.5 * (double)patternData.PatternImage.Height / 2.0), (int)Math.Round(1.5 * (double)patternData.PatternImage.Width), (int)Math.Round(1.5 * (double)patternData.PatternImage.Height));
                        Count++;
                        num2 = Math.Min(num, num2 / 2.0);
                        num = Math.Max(num2 / 2.0, Precision);
                        num3 = (double)matchingResult.CP.Z * 180.0 / Math.PI;
                        if (Math.Abs(num6 - num3) < Precision && Count > 1) { num2 = Precision * 2.0; break; }
                        num4 *= 2.0;
                        if (num4 < 0.1) { num4 = 0.1; }
                    }
                    while (!(num4 > 1.0 / ScaleLast) && (Count < 3 || this.Algorithm != "QuickFinding") && num > Precision);
                    if (UseEdge && num5 > ScoreLimit / 2.0)
                    {
                        searchRegionAdaptive = new Rect((int)Math.Round(matchingResult.CP.X - (float)(patternData.PatternImage.Width / 2)), (int)Math.Round(matchingResult.CP.Y - (float)(patternData.PatternImage.Height / 2)), patternData.PatternImage.Width, patternData.PatternImage.Height);
                        margin = 1.1;
                        TempleMatch_(sourceImage, patternData, searchRegionAdaptive, num3, 0.0, num, num4, out matchingResult, margin, maskedScore: true);
                    }
                    float num7 = patternCP.X;
                    float num8 = patternCP.Y;
                    num3 = (double)matchingResult.CP.Z * 180.0 / Math.PI;
                    num = num2 / 2.0;
                    num4 = 1.0 / ScaleLast;
                    searchRegionAdaptive = new Rect((int)Math.Round((double)matchingResult.CP.X - 1.1 * (double)patternData.PatternImage.Width / 2.0), (int)Math.Round((double)matchingResult.CP.Y - 1.1 * (double)patternData.PatternImage.Height / 2.0), (int)Math.Round(1.1 * (double)patternData.PatternImage.Width), (int)Math.Round(1.1 * (double)patternData.PatternImage.Height));
                    margin = 1.1;
                    bool result = TempleMatch_(sourceImage, patternData, searchRegionAdaptive, num3, num2, num, num4, out matchingResult, margin, maskedScore: false, fineFind: true);
                    Point3f cp = matchingResult.CP/*; // */+ new Point3f((float)((double)num7 * Math.Cos((double)matchingResult.CP.Z) - (double)num8 * Math.Sin((double)matchingResult.CP.Z)), (float)((double)num7 * Math.Sin((double)matchingResult.CP.Z) + (double)num8 * Math.Cos((double)matchingResult.CP.Z)), 0.0f);
                    ////OriginP = new Point3f((float)(((double)point1.X + (point3d.X - (double)(rect.Width / 2)) * Math.Cos(num10) + (point3d.Y - (double)(rect.Height / 2)) * Math.Sin(num10)) / searchScaleAdaptive), (float)(((double)point1.Y - (point3d.X - (double)(rect.Width / 2)) * Math.Sin(num10) + (point3d.Y - (double)(rect.Height / 2)) * Math.Cos(num10)) / searchScaleAdaptive), (float)-num10);
                    matchResult = new MatchingResult(cp: matchingResult.CP + new Point3f((float)((double)num7 * Math.Cos(matchingResult.CP.Z) - (double)num8 * Math.Sin(matchingResult.CP.Z)), (float)((double)num7 * Math.Sin(matchingResult.CP.Z) + (double)num8 * Math.Cos(matchingResult.CP.Z)), 0f), S: matchingResult.Score, box: matchingResult.Box);
                    Cv2.Circle(sourceImage, new OpenCvSharp.Point(cp.X, cp.Y), 3, new Scalar(0, 0, 255), 5);
                    //Mat sourceImage1 = RotationBound(sourceImage, num3);
                    //sourceImage1?.Dispose();

                    externVal["angle"] = num3;
                    externVal["center"] = new OpenCvSharp.Point(cp.X, cp.Y);
                    externVal["msg"] = "OK";
                    resCheck = true;
                }
                else { externVal["msg"] = "NG: MATCHING - ANH VA ANH MAU KHONG KHOP"; }
            }
            else { externVal["msg"] = "NG: MATCHING - CHUA TRAINNING ANH"; }
            Cv2.PutText(sourceImage, externVal["msg"].ToString(), fontPos, fontT, fontScale, colorScarNG, fontThickness);
            cvX.ImShow(blockID, "ALIGN_DISPLAY", sourceImage);
            return (resCheck, externVal);
        }


        private bool TempleMatch_(Mat sourceImage, PatternData patternData, Rect searchRegionAdaptive, double degStartAdaptive, double degRangeAdaptive, double degStepAdaptive, double searchScaleAdaptive, out MatchingResult matchingResult, double margin = 1.5, bool maskedScore = false, bool fineFind = false)
        {
            matchingResult = new MatchingResult(0f, default(Point3f), new Point2f[4]);
            if (searchScaleAdaptive == 0.0)
            {
                return false;
            }
            if (searchRegionAdaptive.Width < 0 || searchRegionAdaptive.Height < 0)
            {
                return false;
            }
            if (double.IsNaN(degStartAdaptive) || double.IsNaN(degRangeAdaptive) || double.IsNaN(degStepAdaptive))
            {
                return false;
            }
            degStepAdaptive = Math.Round(degStepAdaptive, 10);
            Mat mat2 = new Mat();
            Mat mat = new Mat();
            Point3f point3f = default(Point3f);
            _ = new Point2f[4];
            OpenCvSharp.Point point = (searchRegionAdaptive.TopLeft + searchRegionAdaptive.BottomRight) * 0.5 * searchScaleAdaptive;
            Point3d point3d = new Point3d(0.0, 0.0, 0.0 - degStartAdaptive);
            double num = -1.0;
            int width = (int)Math.Round((double)patternData.PatternImage.Width * searchScaleAdaptive);
            int height = (int)Math.Round((double)patternData.PatternImage.Height * searchScaleAdaptive);
            Cv2.Resize(patternData.PatternImage, mat2, new OpenCvSharp.Size(width, height));
            Cv2.Resize(sourceImage, mat, new OpenCvSharp.Size(sourceImage.Width * searchScaleAdaptive, sourceImage.Height * searchScaleAdaptive));
            double num2 = degRangeAdaptive / 2.0 * Math.PI / 180.0;
            Point2d point2 = new Point2d(Math.Max(searchRegionAdaptive.Width / 2, patternData.PatternImage.Width / 2), Math.Max(searchRegionAdaptive.Height / 2, patternData.PatternImage.Height / 2));
            double num3 = OBasicAlgorithm.Rotate(point2, 0.0 - num2).X * 2.0 * searchScaleAdaptive * margin;
            double num4 = OBasicAlgorithm.Rotate(point2, num2).Y * 2.0 * searchScaleAdaptive * margin;
            Rect rect = default(Rect);
            rect.X = (int)Math.Round((double)point.X - num3 / 2.0);
            rect.Y = (int)Math.Round((double)point.Y - num4 / 2.0);
            rect.Size = new OpenCvSharp.Size(num3, num4);
            Mat mat3 = new Mat(rect.Size, sourceImage.Type());
            Mat transferMat = new Mat(2, 3, 6, new double[6]
            {
            searchScaleAdaptive,
            0.0,
            -rect.X,
            0.0,
            searchScaleAdaptive,
            -rect.Y
            }, 0L);
            if (degStepAdaptive <= 0.0)
            {
                degRangeAdaptive = 0.0;
                degStepAdaptive = 1.0;
            }
            else
            {
                degRangeAdaptive -= 2.0 * (degRangeAdaptive / 2.0 % degStepAdaptive);
            }


            int num5 = 0;
            bool flag = true;
            double minVal = 0.0;
            double maxVal = 0.0;
            OpenCvSharp.Point minLoc = default(OpenCvSharp.Point);
            OpenCvSharp.Point maxLoc = default(OpenCvSharp.Point);
            List<Point2d> list = new List<Point2d>();
            Mat mat4 = new Mat();
            Mat mat5 = null;
            double num6 = Math.Round((0.0 - degRangeAdaptive) / 2.0 - degStartAdaptive, 10);
            double num7 = Math.Round(degRangeAdaptive / 2.0 - degStartAdaptive, 10);
            for (double num8 = num6; num8 <= num7; num8 += degStepAdaptive)
            {
                if (double.IsInfinity(num8) || double.IsNaN(num8))
                {
                    return false;
                }
                mat4 = Cv2.GetRotationMatrix2D(new Point2f(point.X, point.Y), 0.0 - num8, 1.0);
                mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
                mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
                Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, patternData.BorderColor);
                mat5 = new Mat();
                Cv2.MatchTemplate(mat3, mat2, mat5, TemplateMatchModes.CCoeffNormed);
                Cv2.MinMaxLoc(ApplyMask(mat5, transferMat, out var mask), out minVal, out maxVal, out minLoc, out maxLoc, mask);
                mat5?.Dispose();
                mask?.Dispose();
                double num9 = 0.9;
                list.Add(new Point2d(num8, maxVal));
                if (maxVal > num)
                {
                    num = maxVal;
                    num5 = list.Count - 1;
                    point3d.X = maxLoc.X;
                    point3d.Y = maxLoc.Y;
                    point3d.Z = num8;
                    if (num > num9)
                    {
                        flag = false;
                    }
                }
                else if (!flag)
                {
                    break;
                }
            }
            if (list.Count > 1)
            {
                double num10 = num6;
                int num11 = 0;
                while (num5 == 0 && num11 < 5)
                {
                    num10 -= degStepAdaptive;
                    mat4 = Cv2.GetRotationMatrix2D(new Point2f(rect.X + rect.Width / 2, rect.Y + rect.Height / 2), 0.0 - num10, 1.0);
                    mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
                    mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
                    Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, patternData.BorderColor);
                    mat5 = new Mat(mat3.Size(), 5);
                    Cv2.MatchTemplate(mat3, mat2, mat5, TemplateMatchModes.CCoeffNormed);
                    Mat mask2;
                    Mat mat6 = ApplyMask(mat5, transferMat, out mask2);
                    Cv2.MinMaxLoc(mat5, out minVal, out maxVal, out minLoc, out maxLoc, mask2);
                    mask2?.Dispose();
                    mat6?.Dispose();
                    list.Insert(0, new Point2d(num10, maxVal));
                    num5++;
                    if (maxVal > num)
                    {
                        num = maxVal;
                        num5 = 0;
                        point3d.X = maxLoc.X;
                        point3d.Y = maxLoc.Y;
                        point3d.Z = num10;
                        num5 = 0;
                    }
                    mat5.Dispose();
                    num11++;
                }
                num10 = num7;
                num11 = 0;
                while (num5 == list.Count - 1 && num11 < 5)
                {
                    num10 += degStepAdaptive;
                    mat4 = Cv2.GetRotationMatrix2D(new Point2f(rect.X + rect.Width / 2, rect.Y + rect.Height / 2), 0.0 - num10, 1.0);
                    mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
                    mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
                    Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, patternData.BorderColor);
                    mat5 = new Mat(mat3.Size(), 5);
                    Cv2.MatchTemplate(mat3, mat2, mat5, TemplateMatchModes.CCoeffNormed);
                    Mat mask3;
                    Mat mat7 = ApplyMask(mat5, transferMat, out mask3);
                    Cv2.MinMaxLoc(mat5, out minVal, out maxVal, out minLoc, out maxLoc, mask3);
                    mask3?.Dispose();
                    mat7?.Dispose();
                    list.Add(new Point2d(num10, maxVal));
                    if (maxVal > num)
                    {
                        num = maxVal;
                        num5 = list.Count - 1;
                        point3d.X = maxLoc.X;
                        point3d.Y = maxLoc.Y;
                        point3d.Z = num10;
                    }
                    mat5.Dispose();
                    num11++;
                }
                double num12 = double.MaxValue;
                maxVal = double.MaxValue;
                Point2d point2d = default(Point2d);
                Mat mat8 = null;
                num11 = 0;
                while (num12 > degStepAdaptive / 5.0 && num11 < 5)
                {
                    double num13 = list[num5].X;
                    point2d = CalculateAdjustedPoint(list, num5);
                    num12 = Math.Abs(num13 - point2d.X);
                    mat4 = Cv2.GetRotationMatrix2D(new Point2f(point.X, point.Y), 0.0 - point2d.X, 1.0);
                    mat4.Set(0, 2, mat4.At<double>(0, 2) - (double)rect.X);
                    mat4.Set(1, 2, mat4.At<double>(1, 2) - (double)rect.Y);
                    Cv2.WarpAffine(mat, mat3, mat4, mat3.Size(), InterpolationFlags.Linear, BorderTypes.Constant, patternData.BorderColor);
                    mat8 = new Mat(mat3.Size(), 5);
                    Cv2.MatchTemplate(mat3, mat2, mat8, TemplateMatchModes.CCoeffNormed);
                    Mat mask4;
                    Mat mat9 = ApplyMask(mat8, transferMat, out mask4);
                    Cv2.MinMaxLoc(mat9, out minVal, out maxVal, out minLoc, out maxLoc, mask4);
                    mask4?.Dispose();
                    mat9?.Dispose();
                    if (!(maxVal > num))
                    {
                        break;
                    }
                    num = maxVal;
                    list.Add(new Point2d(point2d.X, maxVal));
                    list.Sort((Point2d a, Point2d b) => (!(a.X < b.X)) ? 1 : (-1));
                    num5 = list.FindIndex((Point2d a) => a.Y == maxVal);
                    point3d.X = maxLoc.X;
                    point3d.Y = maxLoc.Y;
                    point3d.Z = point2d.X;
                    if (Count == 0)
                    {
                        break;
                    }
                    num11++;
                }
                Point2d point2d2 = CalculateFinalPoint(mat8, new OpenCvSharp.Point(point3d.X, point3d.Y));
                point3d.X = point2d2.X;
                point3d.Y = point2d2.Y;
                point3d.Z = point2d.X;
                num = maxVal;
            }
            //_ = RunParams.Algorithm == EAlgorithm.EdgeMatching && maskedScore;
            double num14 = point3d.Z * Math.PI / 180.0;
            double num15 = ((double)point.X + (point3d.X - (double)(rect.Width / 2)) * Math.Cos(num14) + (point3d.Y - (double)(rect.Height / 2)) * Math.Sin(num14)) / searchScaleAdaptive;
            double num16 = ((double)point.Y - (point3d.X - (double)(rect.Width / 2)) * Math.Sin(num14) + (point3d.Y - (double)(rect.Height / 2)) * Math.Cos(num14)) / searchScaleAdaptive;
            point3f = new Point3f((float)num15, (float)num16, (float)(0.0 - num14));
            mat3.Dispose();
            matchingResult.Score = (float)num;
            matchingResult.Box = CalculatePatternBox(point3f, patternData.PatternImage.Width, patternData.PatternImage.Height);
            matchingResult.CP = (point3f + new Point3f(matchingResult.Box[2].X, matchingResult.Box[2].Y, point3f.Z));
            matchingResult.CP = GetRectangleCenter(matchingResult.Box, point3f.Z);


            mat?.Dispose();
            mat2?.Dispose();
            mat3?.Dispose();
            transferMat?.Dispose();
            return true;
        }


        private Point2f[] CalculatePatternBox(Point3f OriginP, double W, double H)
        {
            Point2f[] point2fArray = new Point2f[4];
            point2fArray[0] = new Point2f(OriginP.X, OriginP.Y);
            point2fArray[1] = point2fArray[0] + new Point2f((float)Math.Cos((double)OriginP.Z), (float)Math.Sin((double)OriginP.Z)) * W;
            point2fArray[3] = point2fArray[0] + new Point2f((float)Math.Cos((double)OriginP.Z + Math.PI / 2.0), (float)Math.Sin((double)OriginP.Z + Math.PI / 2.0)) * H;
            point2fArray[2] = point2fArray[1] + point2fArray[3] - point2fArray[0];
            return point2fArray;
        }


        private Mat ApplyMask(Mat _C, Mat _transferMat, out Mat mask)
        {
            mask = (Mat)null;
            if (_C == null || _transferMat == null)
                return (Mat)null;
            mask = new Mat(_C.Size(), MatType.CV_8UC1, (Scalar)0.0);
            Point2d[] point2dArray = new Point2d[4]
            {
        new Point2d((double) this.matchingArea.Left, (double) this.matchingArea.Top),
        new Point2d((double) this.matchingArea.Right, (double) this.matchingArea.Top),
        new Point2d((double) this.matchingArea.Right, (double) this.matchingArea.Bottom),
        new Point2d((double) this.matchingArea.Left, (double) this.matchingArea.Bottom)
            };
            OpenCvSharp.Point[] pointArray = new OpenCvSharp.Point[4];
            for (int index = 0; index < 4; ++index)
            {
                Mat mat = (Mat)(_transferMat * new Mat(3, 1, MatType.CV_64FC1, (Array)new double[3]
                {
          point2dArray[index].X,
          point2dArray[index].Y,
          1.0
                }));
                pointArray[index] = new OpenCvSharp.Point(mat.At<double>(0, 0), mat.At<double>(1, 0));
            }
            OpenCvSharp.Cv2.FillPoly(mask, (IEnumerable<IEnumerable<OpenCvSharp.Point>>)new OpenCvSharp.Point[1][]
            {
        pointArray
            }, new Scalar(100.0));
            Mat m = new Mat(_C.Size(), _C.Type(), (Scalar)0.0);
            _C.CopyTo(m, mask);
            return m;
        }
        private Point2d CalculateAdjustedPoint(List<Point2d> listScore, int maxIdx)
        {
            if (maxIdx == listScore.Count - 1 || maxIdx == 0)
                return listScore[maxIdx];
            double[] numArray = new double[3]
            {
        listScore[maxIdx - 1].X,
        listScore[maxIdx].X,
        listScore[maxIdx + 1].X
            };
            double[] data = new double[3]
            {
        listScore[maxIdx - 1].Y,
        listScore[maxIdx].Y,
        listScore[maxIdx + 1].Y
            };
            Mat mat1 = new Mat(3, 3, MatType.CV_64FC1, (Array)new double[9]
            {
        numArray[0] * numArray[0],
        numArray[0],
        1.0,
        numArray[1] * numArray[1],
        numArray[1],
        1.0,
        numArray[2] * numArray[2],
        numArray[2],
        1.0
            });
            Mat mat2 = new Mat(3, 1, MatType.CV_64FC1, (Array)data);
            if (mat1.Determinant() == 0.0)
                return listScore[maxIdx];
            Mat mat3 = (Mat)(mat1.Inv() * mat2);
            double num1 = -1.0 * mat3.At<double>(1) / mat3.At<double>(0) / 2.0;
            double num2 = mat3.At<double>(0) * num1 * num1 + mat3.At<double>(1) * num1 + mat3.At<double>(2);
            return double.IsInfinity(num1) || double.IsInfinity(num2) ? listScore[maxIdx] : new Point2d(num1, num2);
        }

        private Point2d CalculateFinalPoint(Mat CoeffMat, OpenCvSharp.Point maxPoint)
        {
            if (maxPoint.X - 4 < 0 || maxPoint.Y - 4 < 0 || maxPoint.X + 4 > CoeffMat.Width - 1 || maxPoint.Y + 4 > CoeffMat.Height - 1)
                return new Point2d((double)maxPoint.X, (double)maxPoint.Y);
            Mat mat = CoeffMat[maxPoint.Y - 4, maxPoint.Y + 4, maxPoint.X - 4, maxPoint.X + 4].Clone().GaussianBlur(new OpenCvSharp.Size(3, 3), 1.0);
            float[] numArray1 = new float[3];
            float[] numArray2 = new float[3];
            for (int index = -1; index < 2; ++index)
            {
                numArray1[1 + index] = (float)(((double)mat.At<float>(4, 4 + index + 1) - (double)mat.At<float>(4, 4 + index - 1)) / 2.0);
                numArray2[1 + index] = (float)(((double)mat.At<float>(4 + index + 1, 4) - (double)mat.At<float>(4 + index - 1, 4)) / 2.0);
            }
            double num1 = (double)numArray1[1];
            double num2 = ((double)numArray1[2] - (double)numArray1[0]) / 4.0;
            double num3 = ((double)numArray1[2] + (double)numArray1[0] - 2.0 * num1) / 6.0;
            double d1 = num3 != 0.0 ? (num2 < 0.0 ? (-num2 - Math.Sqrt(num2 * num2 - 3.0 * num3 * num1)) / 3.0 / num3 : (-num2 + Math.Sqrt(num2 * num2 - 3.0 * num3 * num1)) / 3.0 / num3) : 0.0;
            double num4 = (double)numArray2[1];
            double num5 = ((double)numArray2[2] - (double)numArray2[0]) / 4.0;
            double num6 = ((double)numArray2[2] + (double)numArray2[0] - 2.0 * num4) / 6.0;
            double d2 = num6 != 0.0 ? (num5 < 0.0 ? (-num5 - Math.Sqrt(num5 * num5 - 3.0 * num6 * num4)) / 3.0 / num6 : (-num5 + Math.Sqrt(num5 * num5 - 3.0 * num6 * num4)) / 3.0 / num6) : 0.0;
            if (Math.Abs(d1) > 1.0 || double.IsNaN(d1))
                d1 = 0.0;
            if (Math.Abs(d2) > 1.0 || double.IsNaN(d2))
                d2 = 0.0;
            mat.Dispose();
            return new Point2d((double)maxPoint.X + d1, (double)maxPoint.Y + d2);
        }
        private void ApplyEdgeDetection(
          Mat SrcImg,
          out Mat Edge,
          int backcolor = 0,
          ThresholdTypes THtype = ThresholdTypes.Binary)
        {
            if (SrcImg == null || SrcImg.Width == 0 || SrcImg.Height == 0)
            {
                Edge = (Mat)null;
            }
            else
            {
                byte[] lut = new byte[256];
                for (int index = 0; index < lut.Length; ++index)
                    lut[index] = index >= 250 ? (byte)index : (byte)0;
                SrcImg.Blur(new OpenCvSharp.Size(3, 3));
                Edge = SrcImg.AdaptiveThreshold((double)byte.MaxValue, AdaptiveThresholdTypes.GaussianC, ThresholdTypes.BinaryInv, 5, 10.0);
                Edge.LUT(lut);
            }
        }

        public Mat RotationBound(Mat frame, double angle, double scale = 1.0)
        {
            if (frame == null) return null;
            Point2f center = new Point2f(frame.Width / 2.0f, frame.Height / 2.0f);
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

        private Point3f GetRectangleCenter(Point2f[] rectanglePoints, double rotationAngle)
        {
            // Tính toán trung điểm của các điểm của hình chữ nhật
            double centerX = 0.0;
            double centerY = 0.0;

            foreach (var point in rectanglePoints)
            {
                centerX += point.X;
                centerY += point.Y;
            }

            centerX /= 4;  // Vì hình chữ nhật có 4 góc
            centerY /= 4;

            // Quay tâm (x, y) của hình chữ nhật theo góc rotationAngle
            double angleInRadians = rotationAngle * Math.PI / 180.0; // Chuyển đổi góc từ độ sang radians
            double cosAngle = Math.Cos(angleInRadians);
            double sinAngle = Math.Sin(angleInRadians);

            // Quay lại tọa độ trung tâm mới
            double rotatedCenterX = centerX * cosAngle - centerY * sinAngle;
            double rotatedCenterY = centerX * sinAngle + centerY * cosAngle;

            // Trả về điểm trung tâm sau khi quay
            return new Point3f((float)rotatedCenterX, (float)rotatedCenterY, (float)rotationAngle);
        }
    }
}
