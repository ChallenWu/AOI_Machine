using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OVisionPro.Services.ImageProcessing.BASE.TemplateMatching
{
    public class TemplateMatch2
    {
        ///  <summary>
        ///     Find components basic.
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="blockID"></param>
        /// <returns></returns>
        /// 
        private protected Mat inputImage;
        private protected Mat templateImage;
        //private protected OParameters runParams;
        private Mat processedImage;
        private Mat resultImage;
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

        private readonly static TemplateMatch2 instance = new TemplateMatch2();
        public static TemplateMatch2 Instance
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

        public bool MatchPattern()
        {
            Mat sourceImage = Cv2.ImRead("sourceImg.png");
            Mat pattern = Cv2.ImRead("pattern.png");
            Rect searchingRegion = new Rect(100, 100, 1200, 400);
            Cv2.Rectangle(sourceImage, searchingRegion, Scalar.Red);

            PatternData patternData = new PatternData(pattern, new Point3f(pattern.Width / 2, pattern.Height / 2, 0.0f));
            MatchingResult matchResult = new MatchingResult(0.0f, new Point3f(), new Point2f[4]);
            MatchingResult matchingResult;
            double num1 = Math.Max(FirstStep, Precision);
            double degRangeAdaptive = (double)(this.AnglePos - this.AngleNeg);
            double degStartAdaptive1 = (double)((this.AnglePos + this.AngleNeg) / 2);
            double searchScaleAdaptive1 = 1.0 / this.ScaleFirst;
            Rect searchRegionAdaptive = searchingRegion;
            double num2 = 0.0;
            double margin1 = 1.1;
            UpdateParamMatch("MaxAccuracy");
            this.processedImageIndex = 0;
            do
            {
                double num3 = degStartAdaptive1;
                this.PerformPatternMatching(sourceImage, patternData, searchRegionAdaptive, degStartAdaptive1, degRangeAdaptive, num1, searchScaleAdaptive1, out matchingResult, margin1);
                searchRegionAdaptive = new Rect(OBasicAlgorithm.Round((double)matchingResult.CP.X - 1.5 * (double)patternData.PatternImage.Width / 2.0), OBasicAlgorithm.Round((double)matchingResult.CP.Y - 1.5 * (double)patternData.PatternImage.Height / 2.0), OBasicAlgorithm.Round(1.5 * (double)patternData.PatternImage.Width), OBasicAlgorithm.Round(1.5 * (double)patternData.PatternImage.Height));
                ++this.processedImageIndex;
                degRangeAdaptive = Math.Min(num1, degRangeAdaptive / 2.0);
                num1 = Math.Max(degRangeAdaptive / 2.0, this.Precision);
                degStartAdaptive1 = (double)matchingResult.CP.Z * 180.0 / Math.PI;
                double num4 = degStartAdaptive1;
                if (Math.Abs(num3 - num4) < this.Precision && this.processedImageIndex > 1)
                {
                    degRangeAdaptive = this.Precision * 2.0;
                    break;
                }
                searchScaleAdaptive1 *= 2.0;
                if (searchScaleAdaptive1 < 0.1)
                    searchScaleAdaptive1 = 0.1;
            }
            while (searchScaleAdaptive1 <= 1.0 / this.ScaleLast && (this.processedImageIndex < 3 || this.Algorithm != "QuickFinding") && num1 > this.Precision);
            if (this.UseEdge && num2 > this.ScoreLimit / 2.0)
            {
                searchRegionAdaptive = new Rect(OBasicAlgorithm.Round((double)matchingResult.CP.X - (double)(patternData.PatternImage.Width / 2)), OBasicAlgorithm.Round((double)matchingResult.CP.Y - (double)(patternData.PatternImage.Height / 2)), OBasicAlgorithm.Round((double)patternData.PatternImage.Width), OBasicAlgorithm.Round((double)patternData.PatternImage.Height));
                double margin2 = 1.1;
                this.PerformPatternMatching(sourceImage, patternData, searchRegionAdaptive, degStartAdaptive1, 0.0, num1, searchScaleAdaptive1, out matchingResult, margin2, true);
            }
            double degStartAdaptive2 = (double)matchingResult.CP.Z * 180.0 / Math.PI;
            double degStepAdaptive = degRangeAdaptive / 2.0;
            double searchScaleAdaptive2 = 1.0 / this.ScaleLast;
            searchRegionAdaptive = new Rect(OBasicAlgorithm.Round((double)matchingResult.CP.X - 1.1 * (double)patternData.PatternImage.Width / 2.0), OBasicAlgorithm.Round((double)matchingResult.CP.Y - 1.1 * (double)patternData.PatternImage.Height / 2.0), OBasicAlgorithm.Round(1.1 * (double)patternData.PatternImage.Width), OBasicAlgorithm.Round(1.1 * (double)patternData.PatternImage.Height));
            double margin3 = 1.1;
            int num5 = this.PerformPatternMatching(sourceImage, patternData, searchRegionAdaptive, degStartAdaptive2, degRangeAdaptive, degStepAdaptive, searchScaleAdaptive2, out matchingResult, margin3, fineFind: true) ? 1 : 0;
            //float num6 = patternData.PatternCP.X;
            //float num7 = patternData.PatternCP.Y;
            //Point3f cp = matchingResult.CP + new Point3f((float)((double)num6 * Math.Cos((double)matchingResult.CP.Z) - (double)num7 * Math.Sin((double)matchingResult.CP.Z)), (float)((double)num6 * Math.Sin((double)matchingResult.CP.Z) + (double)num7 * Math.Cos((double)matchingResult.CP.Z)), 0.0f);
            //matchResult = new MatchingResult(matchingResult.Score, cp, matchingResult.Box);
            //Cv2.Circle(sourceImage, new OpenCvSharp.Point(matchResult.CP.X, matchResult.CP.Y), 3, new Scalar(0, 0, 255), 5);
            //Cv2.ImShow("ALIGN_DISPLAY", sourceImage);
            Cv2.WaitKey(0);
            return num5 != 0;
        }

        private bool PerformPatternMatching(
          Mat sourceImage,
          PatternData patternData,
          Rect searchRegionAdaptive,
          double degStartAdaptive,
          double degRangeAdaptive,
          double degStepAdaptive,
          double searchScaleAdaptive,
          out MatchingResult matchResult,
          double margin = 1.5,
          bool maskedScore = false,
          bool fineFind = false)
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            matchResult = new MatchingResult(0.0f, new Point3f(), new Point2f[4]);
            if (searchScaleAdaptive == 0.0 || 
                searchRegionAdaptive.Width < 0 ||
                searchRegionAdaptive.Height < 0 || 
                double.IsNaN(degStartAdaptive) ||
                double.IsNaN(degRangeAdaptive) || 
                double.IsNaN(degStepAdaptive))
                return false;
            degStepAdaptive = Math.Round(degStepAdaptive, 10);
            Mat Edge = sourceImage;
            Mat mat1 = patternData.PatternImage;
            Point3f OriginP = new Point3f();
            Point2f[] point2fArray = new Point2f[4];
            OpenCvSharp.Point point1 = (searchRegionAdaptive.TopLeft + searchRegionAdaptive.BottomRight) * 0.5 * searchScaleAdaptive;
            Point3d point3d = new Point3d(0.0, 0.0, -degStartAdaptive);
            double num1 = -1.0;
            OBasicAlgorithm.Round((double)patternData.PatternImage.Width * searchScaleAdaptive);
            OBasicAlgorithm.Round((double)patternData.PatternImage.Height * searchScaleAdaptive);
            if (this.UseEdge)
                this.ApplyEdgeDetection(Edge, out Edge);
            double thetaRad = degRangeAdaptive / 2.0 * Math.PI / 180.0;
            Point2d point2 = new Point2d((double)Math.Max(searchRegionAdaptive.Width / 2, patternData.PatternImage.Width / 2), (double)Math.Max(searchRegionAdaptive.Height / 2, patternData.PatternImage.Height / 2));
            double width = OBasicAlgorithm.Rotate(point2, -thetaRad).X * 2.0 * searchScaleAdaptive * margin;
            double height = OBasicAlgorithm.Rotate(point2, thetaRad).Y * 2.0 * searchScaleAdaptive * margin;
            Rect rect = new Rect();
            rect.X = OBasicAlgorithm.Round((double)point1.X - width / 2.0);
            rect.Y = OBasicAlgorithm.Round((double)point1.Y - height / 2.0);
            rect.Size = new OpenCvSharp.Size(width, height);
            Mat mat2 = new Mat(rect.Size, sourceImage.Type());
            Mat _transferMat = new Mat(2, 3, (MatType)6, (Array)new double[6]
            {
                searchScaleAdaptive,
                0.0,
                (double) -rect.X,
                0.0,
                searchScaleAdaptive,
                (double) -rect.Y
            });
            if (degStepAdaptive <= 0.0)
            {
                degRangeAdaptive = 0.0;
                degStepAdaptive = 1.0;
            }
            else
                degRangeAdaptive -= 2.0 * (degRangeAdaptive / 2.0 % degStepAdaptive);
            if (fineFind && this.Algorithm == "MaxAccuracy")
            {
                degStepAdaptive = this.Precision;
                degRangeAdaptive = Math.Round(degRangeAdaptive, 3);
                degStartAdaptive = Math.Round(degStartAdaptive, 3);
            }
            int num2 = 0;
            bool flag = true;
            double minVal = 0.0;
            // ISSUE: reference to a compiler-generated field
            double maxVal = 0.0;
            OpenCvSharp.Point minLoc = new OpenCvSharp.Point();
            OpenCvSharp.Point maxLoc = new OpenCvSharp.Point();
            List<Point2d> listScore = new List<Point2d>();
            Mat mat3 = new Mat();
            int num3 = 0;
            double num4 = Math.Round(-degRangeAdaptive / 2.0 - degStartAdaptive, 10);
            double num5 = Math.Round(degRangeAdaptive / 2.0 - degStartAdaptive, 10);
            double num6 = num4;
            while (num6 <= num5)
            {
                if (double.IsInfinity(num6) || double.IsNaN(num6))
                    return false;
                Mat rotationMatrix2D = OpenCvSharp.Cv2.GetRotationMatrix2D(new Point2f((float)point1.X, (float)point1.Y), -num6, 1.0);
                rotationMatrix2D.Set<double>(0, 2, rotationMatrix2D.At<double>(0, 2) - (double)rect.X);
                rotationMatrix2D.Set<double>(1, 2, rotationMatrix2D.At<double>(1, 2) - (double)rect.Y);
                OpenCvSharp.Cv2.WarpAffine((InputArray)Edge, (OutputArray)mat2, (InputArray)rotationMatrix2D, mat2.Size(), borderValue: new Scalar?((Scalar)patternData.BorderColor));
                Mat mat4 = new Mat();
                Cv2.ImShow("1", mat2);
                Cv2.WaitKey(111);
                Cv2.ImShow("2", mat1);
                Cv2.WaitKey(110);
                OpenCvSharp.Cv2.MatchTemplate((InputArray)mat2, (InputArray)mat1, (OutputArray)mat4, OpenCvSharp.TemplateMatchModes.CCoeffNormed);
                Mat mask;
                // ISSUE: reference to a compiler-generated field
                OpenCvSharp.Cv2.MinMaxLoc((InputArray)this.ApplyMask(mat4, _transferMat, out mask), out minVal, out maxVal, out minLoc, out maxLoc, (InputArray)mask);
                mat4?.Dispose();
                mask?.Dispose();
                double num7 = 0.9;
                // ISSUE: reference to a compiler-generated field
                listScore.Add(new Point2d(num6, maxVal));
                // ISSUE: reference to a compiler-generated field
                if (maxVal > num1)
                {
                    // ISSUE: reference to a compiler-generated field
                    num1 = maxVal;
                    num2 = listScore.Count - 1;
                    point3d.X = (double)maxLoc.X;
                    point3d.Y = (double)maxLoc.Y;
                    point3d.Z = num6;
                    if (num1 > num7 && !fineFind)
                        flag = false;
                }
                else if (!flag)
                    break;
                num6 += degStepAdaptive;
                ++num3;
            }
            if (listScore.Count > 1)
            {
                double x1 = num4;
                for (int index = 0; num2 == 0 && index < 5; ++index)
                {
                    x1 -= degStepAdaptive;
                    Mat rotationMatrix2D = OpenCvSharp.Cv2.GetRotationMatrix2D(new Point2f((float)(rect.X + rect.Width / 2), (float)(rect.Y + rect.Height / 2)), -x1, 1.0);
                    rotationMatrix2D.Set<double>(0, 2, rotationMatrix2D.At<double>(0, 2) - (double)rect.X);
                    rotationMatrix2D.Set<double>(1, 2, rotationMatrix2D.At<double>(1, 2) - (double)rect.Y);
                    OpenCvSharp.Cv2.WarpAffine((InputArray)Edge, (OutputArray)mat2, (InputArray)rotationMatrix2D, mat2.Size(), borderValue: new Scalar?((Scalar)patternData.BorderColor));
                    Mat mat5 = new Mat(mat2.Size(), (MatType)5);
                    OpenCvSharp.Cv2.MatchTemplate((InputArray)mat2, (InputArray)mat1, (OutputArray)mat5, OpenCvSharp.TemplateMatchModes.CCoeffNormed);
                    Mat mask;
                    Mat mat6 = this.ApplyMask(mat5, _transferMat, out mask);
                    // ISSUE: reference to a compiler-generated field
                    OpenCvSharp.Cv2.MinMaxLoc((InputArray)mat5, out minVal, out maxVal, out minLoc, out maxLoc, (InputArray)mask);
                    mask?.Dispose();
                    mat6?.Dispose();
                    // ISSUE: reference to a compiler-generated field
                    listScore.Insert(0, new Point2d(x1, maxVal));
                    ++num2;
                    // ISSUE: reference to a compiler-generated field
                    if (maxVal > num1)
                    {
                        // ISSUE: reference to a compiler-generated field
                        num1 = maxVal;
                        point3d.X = (double)maxLoc.X;
                        point3d.Y = (double)maxLoc.Y;
                        point3d.Z = x1;
                        num2 = 0;
                    }
                    mat5.Dispose();
                }
                double x2 = num5;
                for (int index = 0; num2 == listScore.Count - 1 && index < 5; ++index)
                {
                    x2 += degStepAdaptive;
                    Mat rotationMatrix2D = OpenCvSharp.Cv2.GetRotationMatrix2D(new Point2f((float)(rect.X + rect.Width / 2), (float)(rect.Y + rect.Height / 2)), -x2, 1.0);
                    rotationMatrix2D.Set<double>(0, 2, rotationMatrix2D.At<double>(0, 2) - (double)rect.X);
                    rotationMatrix2D.Set<double>(1, 2, rotationMatrix2D.At<double>(1, 2) - (double)rect.Y);
                    OpenCvSharp.Cv2.WarpAffine((InputArray)Edge, (OutputArray)mat2, (InputArray)rotationMatrix2D, mat2.Size(), borderValue: new Scalar?((Scalar)patternData.BorderColor));
                    Mat mat7 = new Mat(mat2.Size(), (MatType)5);
                    OpenCvSharp.Cv2.MatchTemplate((InputArray)mat2, (InputArray)mat1, (OutputArray)mat7, OpenCvSharp.TemplateMatchModes.CCoeffNormed);
                    Mat mask;
                    Mat mat8 = this.ApplyMask(mat7, _transferMat, out mask);
                    // ISSUE: reference to a compiler-generated field
                    OpenCvSharp.Cv2.MinMaxLoc((InputArray)mat7, out minVal, out maxVal, out minLoc, out maxLoc, (InputArray)mask);

                    mask?.Dispose();
                    mat8?.Dispose();

                    listScore.Add(new Point2d(x2, maxVal));
                    if (maxVal > num1)
                    {
                        num1 = maxVal;
                        num2 = listScore.Count - 1;
                        point3d.X = (double)maxLoc.X;
                        point3d.Y = (double)maxLoc.Y;
                        point3d.Z = x2;
                    }
                    mat7.Dispose();
                }
                double num8 = double.MaxValue;
                // ISSUE: reference to a compiler-generated field
                maxVal = double.MaxValue;
                Point2d point2d1 = new Point2d();
                Mat mat9 = (Mat)null;
                for (int index = 0; num8 > degStepAdaptive / 5.0 && index < 5; ++index)
                {
                    double x3 = listScore[num2].X;
                    point2d1 = this.CalculateAdjustedPoint(listScore, num2);
                    double x4 = point2d1.X;
                    num8 = Math.Abs(x3 - x4);
                    Mat rotationMatrix2D = OpenCvSharp.Cv2.GetRotationMatrix2D(new Point2f((float)point1.X, (float)point1.Y), -point2d1.X, 1.0);
                    rotationMatrix2D.Set<double>(0, 2, rotationMatrix2D.At<double>(0, 2) - (double)rect.X);
                    rotationMatrix2D.Set<double>(1, 2, rotationMatrix2D.At<double>(1, 2) - (double)rect.Y);
                    OpenCvSharp.Cv2.WarpAffine((InputArray)Edge, (OutputArray)mat2, (InputArray)rotationMatrix2D, mat2.Size(), borderValue: new Scalar?((Scalar)patternData.BorderColor));
                    mat9 = new Mat(mat2.Size(), (MatType)5);
                    OpenCvSharp.Cv2.MatchTemplate((InputArray)mat2, (InputArray)mat1, (OutputArray)mat9, OpenCvSharp.TemplateMatchModes.CCoeffNormed);
                    Mat mask;
                    Mat src = this.ApplyMask(mat9, _transferMat, out mask);
                    // ISSUE: reference to a compiler-generated field
                    OpenCvSharp.Cv2.MinMaxLoc((InputArray)src, out minVal, out maxVal, out minLoc, out maxLoc, (InputArray)mask);
                    mask?.Dispose();
                    src?.Dispose();
                    // ISSUE: reference to a compiler-generated field
                    if (maxVal > num1)
                    {
                        // ISSUE: reference to a compiler-generated field
                        num1 = maxVal;
                        // ISSUE: reference to a compiler-generated field
                        listScore.Add(new Point2d(point2d1.X, maxVal));
                        listScore.Sort((Comparison<Point2d>)((a, b) => a.X >= b.X ? 1 : -1));
                        num2 = listScore.FindIndex((Point2d a) => a.Y == maxVal);
                        //num2 = listScore.FindIndex(resultData.MatchPredicate ?? (resultData.MatchPredicate = new Predicate<Point2d>(resultData._MwHzMDEE3bMWTSCVDpsJ7Gd5x2A)));
                        point3d.X = (double)maxLoc.X;
                        point3d.Y = (double)maxLoc.Y;
                        point3d.Z = point2d1.X;
                        if (this.processedImageIndex == 0)
                            break;
                    }
                    else
                        break;
                }

                Point2d point2d2 = this.CalculateFinalPoint(mat9, new OpenCvSharp.Point(point3d.X, point3d.Y));
                point3d.X = point2d2.X;
                point3d.Y = point2d2.Y;
                point3d.Z = point2d1.X;
                // ISSUE: reference to a compiler-generated field
                num1 = maxVal;
            }
            int num9 = this.Algorithm == "EdgeMatching" & maskedScore ? 1 : 0;
            double num10 = point3d.Z * Math.PI / 180.0;
            OriginP = new Point3f((float)(((double)point1.X + (point3d.X - (double)(rect.Width / 2)) * Math.Cos(num10) + (point3d.Y - (double)(rect.Height / 2)) * Math.Sin(num10)) / searchScaleAdaptive), (float)(((double)point1.Y - (point3d.X - (double)(rect.Width / 2)) * Math.Sin(num10) + (point3d.Y - (double)(rect.Height / 2)) * Math.Cos(num10)) / searchScaleAdaptive), (float)-num10);
            mat2.Dispose();
            matchResult.Score = (float)num1;
            matchResult.Box = this.CalculatePatternBox(OriginP, (double)patternData.PatternImage.Width, (double)patternData.PatternImage.Height);
            matchResult.CP = (OriginP + new Point3f(matchResult.Box[2].X, matchResult.Box[2].Y, OriginP.Z)) * 0.5;
            return true;
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
        private Point2f[] CalculatePatternBox(Point3f OriginP, double W, double H)
        {
            Point2f[] point2fArray = new Point2f[4];
            point2fArray[0] = new Point2f(OriginP.X, OriginP.Y);
            point2fArray[1] = point2fArray[0] + new Point2f((float)Math.Cos((double)OriginP.Z), (float)Math.Sin((double)OriginP.Z)) * W;
            point2fArray[3] = point2fArray[0] + new Point2f((float)Math.Cos((double)OriginP.Z + Math.PI / 2.0), (float)Math.Sin((double)OriginP.Z + Math.PI / 2.0)) * H;
            point2fArray[2] = point2fArray[1] + point2fArray[3] - point2fArray[0];
            return point2fArray;
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
