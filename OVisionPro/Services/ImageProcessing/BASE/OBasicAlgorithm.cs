using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Globalization;
using System.IO;
using OpenCvSharp;
namespace OVisionPro.Services.ImageProcessing
{
    public class OBasicAlgorithm
    {
        private static DateTime _qjjt840sKAgRhQ8GhEAPfW9B9Uq;

        public static Mat EMat => new Mat(3, 3, MatType.CV_32FC1, new float[9] { 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f }, 0L);

        public static byte[] GetByteContatiner(short lowWord, short highWord)
        {
            return new byte[4]
            {
            (byte)((uint)lowWord & 0xFFu),
            (byte)(lowWord >> 8),
            (byte)((uint)highWord & 0xFFu),
            (byte)(highWord >> 8)
            };
        }

        public static bool checkInputsMatch(Mat temp, Mat org)
        {
            if (OBasicAlgorithm.IsMat(temp) && OBasicAlgorithm.IsMat(org))
            {
                if (org.Height > temp.Height && org.Width > temp.Width)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsMat(Mat mat)
        {
            if (mat != null && !mat.IsDisposed && mat.Height > 0 && mat.Width > 0) return true;
            return false;
        }

        public static float TwoWordToFloat(short lowWord, short highWord)
        {
            return BitConverter.ToSingle(GetByteContatiner(lowWord, highWord), 0);
        }

        public static float TwoWordToFloat(short lowWord, short highWord, int sosu)
        {
            return (float)BitConverter.ToInt32(GetByteContatiner(lowWord, highWord), 0) * (float)Math.Pow(10.0, sosu * -1);
        }

        public static float TwoWordToFloat(short[] twoWord, int startIndex)
        {
            return TwoWordToFloat(twoWord[startIndex], twoWord[startIndex + 1]);
        }

        public static float TwoWordToFloat(short[] twoWord, int startIndex, int sosu)
        {
            return TwoWordToFloat(twoWord[startIndex], twoWord[startIndex + 1], sosu);
        }

        public static short[] FloatToTwoWord(float value)
        {
            short[] array = new short[2];
            byte[] bytes = BitConverter.GetBytes(value);
            array[0] = (short)((bytes[0] | (bytes[1] << 8)) & 0xFFFF);
            array[1] = (short)((bytes[2] | (bytes[3] << 8)) & 0xFFFF);
            return array;
        }

        public static short[] FloatToTwoWord(float value, int sosu)
        {
            value = (int)Math.Floor(value * (float)(int)Math.Pow(10.0, sosu));
            short[] array = new short[2];
            byte[] bytes = BitConverter.GetBytes((int)value);
            array[0] = (short)((bytes[0] | (bytes[1] << 8)) & 0xFFFF);
            array[1] = (short)((bytes[2] | (bytes[3] << 8)) & 0xFFFF);
            return array;
        }

        public static void FloatToTwoWord(float value, short[] twoWord, int startIndex)
        {
            if (twoWord == null || twoWord.Length < 2)
            {
                throw new Exception("배열 크기 초과 또는 배열 Null");
            }

            byte[] bytes = BitConverter.GetBytes(value);
            twoWord[startIndex] = (short)((bytes[0] | (bytes[1] << 8)) & 0xFFFF);
            twoWord[startIndex + 1] = (short)((bytes[2] | (bytes[3] << 8)) & 0xFFFF);
        }

        public static void FloatToTwoWord(float value, int sosu, short[] twoWord, int startIndex)
        {
            value = (int)Math.Floor(value * (float)(int)Math.Pow(10.0, sosu));
            byte[] bytes = BitConverter.GetBytes((int)value);
            twoWord[startIndex] = (short)((bytes[0] | (bytes[1] << 8)) & 0xFFFF);
            twoWord[startIndex + 1] = (short)((bytes[2] | (bytes[3] << 8)) & 0xFFFF);
        }

        public static short[] IntToWord(int value)
        {
            short[] array = new short[1];
            IntToWord(value, array, 0);
            return array;
        }

        public static void IntToWord(int value, short[] oneWord, int startIndex)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            oneWord[startIndex] = (short)((bytes[0] | (bytes[1] << 8)) & 0xFFFF);
        }

        public static int TwoWordToInt(short lowWord, short highWord)
        {
            return BitConverter.ToInt32(GetByteContatiner(lowWord, highWord), 0);
        }

        public static int TwoWordToInt(short[] twoWord, int startIndex)
        {
            return TwoWordToInt(twoWord[startIndex], twoWord[startIndex + 1]);
        }

        public static short[] IntToTwoWord(int value)
        {
            short[] array = new short[2];
            IntToTwoWord(value, array, 0);
            return array;
        }

        public static string ShortToString(short value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            string text = "";
            for (int num = bytes.Length; num > 0; num--)
            {
                text += System.Convert.ToChar(bytes[num - 1]);
            }

            return text;
        }

        public static void IntToTwoWord(int value, short[] twoWord, int startIndex)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            twoWord[startIndex] = (short)((bytes[0] | (bytes[1] << 8)) & 0xFFFF);
            twoWord[startIndex + 1] = (short)((bytes[2] | (bytes[3] << 8)) & 0xFFFF);
        }

        public static string HexToAscii(int nRrecvData)
        {
            string text = $"{nRrecvData:X}";
            if (text.Length < 4)
            {
                return "";
            }

            int num = 2;
            int length = 2;
            string text2 = string.Empty;
            for (int i = 0; i < 2; i++)
            {
                _ = string.Empty;
                string s = text.ToString().Substring(num, length);
                num -= 2;
                short value = short.Parse(s, NumberStyles.HexNumber);
                text2 += System.Convert.ToChar(value);
            }

            return text2;
        }

        public static string HexToAscii2(int nRrecvData)
        {
            string text = $"{nRrecvData:X}";
            string text2 = string.Empty;
            if ((nRrecvData >= 48 && nRrecvData <= 57) || (nRrecvData >= 65 && nRrecvData <= 90) || (nRrecvData >= 97 && nRrecvData <= 122))
            {
                int length = 2;
                for (int i = 0; i < text.Length; i += 2)
                {
                    _ = string.Empty;
                    uint value = System.Convert.ToUInt32(text.Substring(i, length), 16);
                    text2 += System.Convert.ToChar(value);
                }
            }

            return text2;
        }

        public static string WordToString(int[] word, int length)
        {
            string text = "";
            for (int i = 0; i < length; i++)
            {
                text += HexToAscii2(word[i]);
            }

            return text;
        }

        public static string WordToTwoString(int[] word, int length)
        {
            string text = "";
            short[] array = new short[2];
            for (int i = 0; i < length; i++)
            {
                array = IntToTwoWord(word[i]);
                text += System.Convert.ToChar(array[0]);
                text += System.Convert.ToChar(array[1]);
            }

            return text;
        }

        public static bool IsDirectory(string path, bool bCreate = true)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists && bCreate)
            {
                directoryInfo.Create();
                directoryInfo = null;
                return true;
            }

            return directoryInfo.Exists;
        }

        public static bool IsFile(string path, bool bCreate = true)
        {
            FileInfo fileInfo = new FileInfo(path);
            string directoryName = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            if (!fileInfo.Exists && bCreate)
            {
                fileInfo.Create().Close();
                fileInfo = null;
                return true;
            }

            return fileInfo.Exists;
        }

        public static bool DeleteFile(string path)
        {
            if (IsFile(path))
            {
                new FileInfo(path).Delete();
            }

            return true;
        }

        public static bool DeleteFiles(string path, string[] extension)
        {
            try
            {
                if (IsDirectory(path, bCreate: false))
                {
                    FileInfo[] files = new DirectoryInfo(path).GetFiles();
                    foreach (string text in extension)
                    {
                        FileInfo[] array = files;
                        foreach (FileInfo fileInfo in array)
                        {
                            if (fileInfo.Extension == text)
                            {
                                fileInfo.Delete();
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool CreateFile(string path)
        {
            if (!IsFile(path))
            {
                new FileInfo(path).Create().Close();
            }

            return true;
        }

        public static bool ChangeNewFile(string path)
        {
            DeleteFile(path);
            CreateFile(path);
            return true;
        }

        public static string[] GetFiles(string path)
        {
            if (!IsDirectory(path))
            {
                return null;
            }

            FileInfo[] files = new DirectoryInfo(path).GetFiles();
            List<string> list = new List<string>();
            FileInfo[] array = files;
            foreach (FileInfo fileInfo in array)
            {
                list.Add(fileInfo.Name);
            }

            return list.ToArray();
        }

        public static bool DeleteDirectory(string path, bool recursive = false)
        {
            if (IsDirectory(path))
            {
                new DirectoryInfo(path).Delete(recursive);
            }

            return true;
        }

        public static bool DeleteDirectoryInDirectory(string path)
        {
            if (IsDirectory(path))
            {
                DirectoryInfo[] directories = new DirectoryInfo(path).GetDirectories();
                foreach (DirectoryInfo directoryInfo in directories)
                {
                    FileInfo[] files = directoryInfo.GetFiles();
                    for (int j = 0; j < files.Length; j++)
                    {
                        files[j].Delete();
                    }

                    directoryInfo.Delete();
                }
            }

            return true;
        }

        public static bool CreateDirectory(string path)
        {
            if (!IsDirectory(path))
            {
                new DirectoryInfo(path).Create();
            }

            return true;
        }

        public static bool CopyDirectory(string source, string dest)
        {
            return true;
        }

        public static bool BackDirectory(string source, string dest)
        {
            return true;
        }

        public static Mat FindCalibrationMat(List<Point3f> visionPt, List<Point3f> robotPt)
        {
            if (visionPt.Count != robotPt.Count)
            {
                throw new Exception("Vision Point Length != Robot Point Length");
            }

            if (robotPt.Count <= 3)
            {
                return null;
            }

            Point2d[] array = new Point2d[visionPt.Count];
            Point2d[] array2 = new Point2d[robotPt.Count];
            for (int i = 0; i < visionPt.Count; i++)
            {
                array[i] = new Point2d(visionPt[i].X, visionPt[i].Y);
                array2[i] = new Point2d(robotPt[i].X, robotPt[i].Y);
            }

            Mat mat = OpenCvSharp.Cv2.FindHomography(array, array2);
            Mat mat2 = new Mat(3, 3, MatType.CV_32FC1);
            if (Math.Abs(mat.Determinant()) > Math.Pow(10.0, -10.0))
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        mat2.Set(j, k, (float)mat.At<double>(j, k));
                    }
                }

                mat.Dispose();
                return mat2;
            }
            return null;
        }

        public static Point2f FindRotationCenter(List<Point3f> Pts)
        {
            if (Pts.Count <= 2)
            {
                return new Point2f(0f, 0f);
            }

            if (Pts.Count == 2)
            {
                Point2f p = new Point2f(Pts[0].X, Pts[0].Y);
                Point2f p2 = new Point2f(Pts[1].X, Pts[1].Y);
                return FindRotationCenter(p, p2, Pts[0].Z, Pts[1].Z);
            }

            Mat mat = new Mat(Pts.Count, 3, MatType.CV_32FC1);
            Mat mat2 = new Mat(Pts.Count, 1, MatType.CV_32FC1);
            Mat mat3 = new Mat(3, 1, MatType.CV_32FC1);
            for (int i = 0; i < Pts.Count; i++)
            {
                mat.Set(i, 0, Pts[i].X);
                mat.Set(i, 1, Pts[i].Y);
                mat.Set(i, 2, 1f);
                mat2.Set(i, 0, (0f - Pts[i].X) * Pts[i].X - Pts[i].Y * Pts[i].Y);
            }

            mat3 = (mat.Transpose() * mat).Inv() * (mat.Transpose() * mat2);
            Point2f result = new Point2f(mat3.At<float>(0, 0) * -0.5f, mat3.At<float>(1, 0) * -0.5f);
            for (int j = 0; j < Pts.Count; j++)
            {
                Math.Sqrt(Math.Pow(Pts[j].X - result.X, 2.0) + Math.Pow(Pts[j].Y - result.Y, 2.0));
            }

            return result;
        }

        public static Point2f FindRotationCenter(Point2f p1, Point2f p2, float t1, float t2)
        {
            float num = t2 - t1;
            Mat mat = new Mat(2, 1, MatType.CV_32FC1);
            Mat mat2 = new Mat(2, 2, MatType.CV_32FC1, new float[4]
            {
            (float)Math.Cos(num),
            (float)Math.Sin(0f - num),
            (float)Math.Sin(num),
            (float)Math.Cos(num)
            }, 0L);
            Mat mat3 = Mat.Eye(2, 2, MatType.CV_32FC1);
            Mat mat4 = new Mat(2, 1, MatType.CV_32FC1, new float[2] { p1.X, p1.Y }, 0L);
            Mat mat5 = new Mat(2, 1, MatType.CV_32FC1, new float[2] { p2.X, p2.Y }, 0L);
            mat = (mat2 - mat3).Inv() * (mat2 * mat4 - mat5);
            return new Point2f(mat.At<float>(0), mat.At<float>(1));
        }

        public static Point2f FindRotationCenter(List<Point3f> P, double angleStepDeg)
        {
            Point2f result = default(Point2f);
            int count = P.Count;
            double num = 0.0;
            Point3f point3f = default(Point3f);
            for (int i = 0; i < count - 1; i++)
            {
                point3f = P[i + 1] - P[i];
                num += Math.Atan2(point3f.X, 0f - point3f.Y);
            }

            num /= (double)(count - 1);
            int num2 = ((num < Math.Atan2(point3f.X, 0f - point3f.Y)) ? 1 : (-1));
            double num3 = angleStepDeg * Math.PI / 180.0;
            double num4 = (double)(count + 1) / 2.0 - 1.0;
            Mat mat = new Mat(count, 3, 6, 0.0);
            for (int j = 0; j < count; j++)
            {
                double num5 = num + (double)num2 * num3 * ((double)j - num4);
                double num6 = Math.Cos(num5);
                double num7 = Math.Sin(num5);
                mat.Set(j, 0, num6);
                mat.Set(j, 1, num7);
                mat.Set(j, 2, (double)P[j].X * num7 - (double)P[j].Y * num6);
            }

            Mat mat2 = new Mat();
            SVD.Compute(mat, new Mat(), new Mat(), mat2);
            result.X = (0f - mat2.At<float>(2, 1)) / mat2.At<float>(2, 2);
            result.Y = mat2.At<float>(2, 0) / mat2.At<float>(2, 2);
            return result;
        }


        public static Point3f RToT(Point3f Start, Point3f Target)
        {
            float num = Target.Z - Start.Z;
            Mat mat = new Mat(2, 2, MatType.CV_32FC1, new float[4]
            {
            (float)Math.Cos(num),
            0f - (float)Math.Sin(num),
            (float)Math.Sin(num),
            (float)Math.Cos(num)
            }, 0L);
            Mat mat2 = new Mat(2, 1, MatType.CV_32FC1, new float[2] { Start.X, Start.Y }, 0L);
            mat = new Mat(2, 1, MatType.CV_32FC1, new float[2] { Target.X, Target.Y }, 0L) - mat * mat2;
            return new Point3f(mat.At<float>(0, 0), mat.At<float>(1, 0), num);
        }

        public static OpenCvSharp.Point ImageToFixture(OpenCvSharp.Point point, Mat mat)
        {
            if (mat == null)
            {
                return point;
            }

            OpenCvSharp.Point result = default(OpenCvSharp.Point);
            Mat mat2 = new Mat(3, 1, MatType.CV_32FC1);
            mat2.Set(0, 0, (float)point.X);
            mat2.Set(1, 0, (float)point.Y);
            mat2.Set(2, 0, 1f);
            Mat mat3 = mat * mat2;
            result.X = (int)(mat3.Get<float>(0, 0) / mat3.Get<float>(2, 0));
            result.Y = (int)(mat3.Get<float>(1, 0) / mat3.Get<float>(2, 0));
            return result;
        }

        public static Point2d ImageToFixture2D(Point2d point, Mat mat)
        {
            if (mat == null)
            {
                return point;
            }

            Point2d result = default(Point2d);
            Mat mat2 = new Mat(3, 1, MatType.CV_32FC1);
            mat2.Set(0, 0, (float)point.X);
            mat2.Set(1, 0, (float)point.Y);
            mat2.Set(2, 0, 1f);
            Mat mat3 = mat * mat2;
            result.X = (double)mat3.Get<float>(0, 0) / (double)mat3.Get<float>(2, 0);
            result.Y = (double)mat3.Get<float>(1, 0) / (double)mat3.Get<float>(2, 0);
            return result;
        }

        public static Point2f ImageToFixture2F(Point2f point, Mat mat)
        {
            Point2d point2d = ImageToFixture2D(new Point2d(point.X, point.Y), mat);
            return new Point2f((float)point2d.X, (float)point2d.Y);
        }

        public static OpenCvSharp.Point FixtureToImage(OpenCvSharp.Point point, Mat mat)
        {
            if (mat == null)
            {
                return point;
            }

            Mat mat2 = mat.Inv();
            return ImageToFixture(point, mat2);
        }

        public static Point2d FixtureToImage2D(Point2d point, Mat mat)
        {
            if (mat == null)
            {
                return point;
            }

            Mat mat2 = mat.Inv();
            return ImageToFixture2D(point, mat2);
        }

        public static Point2f FixtureToImage2F(Point2f point, Mat mat)
        {
            if (mat == null)
            {
                return point;
            }

            Mat mat2 = mat.Inv();
            return ImageToFixture2F(point, mat2);
        }

        public static Point3d FixtureToImage3D(Point3d point, Mat mat)
        {
            Point2d point2d = FixtureToImage2D(new Point2d(point.X, point.Y), mat);
            Point2d point2d2 = ImageToFixture2D(new Point2d(0.0, 0.0), mat);
            Point2d point2d3 = ImageToFixture2D(new Point2d(1.0, 0.0), mat);
            return new Point3d(point2d.X, point2d.Y, point.Z - Math.Atan2(point2d3.Y - point2d2.Y, point2d3.X - point2d2.X));
        }

        public static Point3d ImageToFixture3D(Point3d point, Mat mat)
        {
            Point2d point2d = ImageToFixture2D(new Point2d(point.X, point.Y), mat);
            Point2d point2d2 = FixtureToImage2D(new Point2d(0.0, 0.0), mat);
            Point2d point2d3 = FixtureToImage2D(new Point2d(1.0, 0.0), mat);
            return new Point3d(point2d.X, point2d.Y, point.Z - Math.Atan2(point2d3.Y - point2d2.Y, point2d3.X - point2d2.X));
        }

        public static PointF Multiply(PointF P, float f)
        {
            return new PointF(P.X * f, P.Y * f);
        }

        public static float getLength(PointF P0, PointF P1)
        {
            return (float)Math.Sqrt((P1.X - P0.X) * (P1.X - P0.X) + (P1.Y - P0.Y) * (P1.Y - P0.Y));
        }

        public static OpenCvSharp.Point FindCrossPoint(OpenCvSharp.Point P0, OpenCvSharp.Point P1, OpenCvSharp.Point P2, OpenCvSharp.Point P3)
        {
            Mat mat = new Mat(2, 2, MatType.CV_64FC1, new double[4]
            {
            (P1 - P0).X,
            (P2 - P3).X,
            (P1 - P0).Y,
            (P2 - P3).Y
            }, 0L);
            Mat mat2 = new Mat(2, 1, MatType.CV_64FC1, new double[2]
            {
            (P2 - P0).X,
            (P2 - P0).Y
            }, 0L);
            mat = mat.Inv();
            mat2 = mat * mat2;
            return (P1 - P0) * mat2.At<double>(0) + P0;
        }

        public static int Round(double d)
        {
            return (int)Math.Round(d);
        }

        public static double Getlength(Point2d P)
        {
            return Math.Sqrt(P.X * P.X + P.Y * P.Y);
        }

        public static double Getlength(Point2d P0, Point2d P1)
        {
            return Getlength(P1 - P0);
        }

        public static double Getlength(System.Drawing.Point P0, System.Drawing.Point P1)
        {
            return Getlength(new Point2d(P1.X - P0.X, P1.Y - P0.Y));
        }

        public static double Getlength(PointF P0, PointF P1)
        {
            return Getlength(new Point2d(P1.X - P0.X, P1.Y - P0.Y));
        }

        public static double GetAlongDistance(Point2d VectorP0, Point2d VectorP1, Point2d P)
        {
            return Point2d.DotProduct(VectorP1 - VectorP0, P - VectorP0) / Getlength(VectorP1 - VectorP0);
        }

        public static double GetNormalDistance(Point2d P0, Point2d P1, Point2d P2)
        {
            return Math.Abs(Point2d.CrossProduct(P1 - P0, P2 - P0) / Math.Max(Point2d.Distance(P0, P1), Point2d.Distance(P0, P2)));
        }

        public static double GetNormalDistance(Point2f P0, Point2f P1, Point2f P2)
        {
            return Math.Abs(Point2f.CrossProduct(P1 - P0, P2 - P0) / Math.Max(Point2f.Distance(P0, P1), Point2f.Distance(P0, P2)));
        }

        public static double GetNormalDistance(OpenCvSharp.Point P0, OpenCvSharp.Point P1, OpenCvSharp.Point P2)
        {
            return Math.Abs(OpenCvSharp.Point.CrossProduct(P1 - P0, P2 - P0) / Math.Max(OpenCvSharp.Point.Distance(P0, P1), OpenCvSharp.Point.Distance(P0, P2)));
        }

        public static double GetNormalDistance(PointF P0F, PointF P1F, PointF P2F)
        {
            Point2d point2d = new Point2d(P0F.X, P0F.Y);
            Point2d point2d2 = new Point2d(P1F.X, P1F.Y);
            Point2d point2d3 = new Point2d(P2F.X, P2F.Y);
            return Math.Abs(Point2d.CrossProduct(point2d2 - point2d, point2d3 - point2d) / Math.Max(Point2d.Distance(point2d, point2d2), Point2d.Distance(point2d, point2d3)));
        }

        public static Mat WarpImage(Mat src, Mat warp)
        {
            if (src == null)
            {
                return null;
            }

            Point2d point2d = WarpPoint(new Point2d(0.0, 0.0), warp);
            Point2d point2d2 = WarpPoint(new Point2d(src.Width, 0.0), warp);
            Point2d point2d3 = WarpPoint(new Point2d(0.0, src.Height), warp);
            Point2d point2d4 = WarpPoint(new Point2d(src.Width, src.Height), warp);
            double num = 1.0 * Math.Min(Math.Min(Math.Min(point2d.X, point2d2.X), point2d3.X), point2d4.X);
            double num2 = 1.0 * Math.Max(Math.Max(Math.Max(point2d.X, point2d2.X), point2d3.X), point2d4.X);
            double num3 = 1.0 * Math.Min(Math.Min(Math.Min(point2d.Y, point2d2.Y), point2d3.Y), point2d4.Y);
            double num4 = 1.0 * Math.Max(Math.Max(Math.Max(point2d.Y, point2d2.Y), point2d3.Y), point2d4.Y);
            OpenCvSharp.Size size = new OpenCvSharp.Size((int)Math.Round(num2 - num + 1.0), (int)Math.Round(num4 - num3 + 1.0));
            Mat mat = new Mat(size, src.Type());
            OpenCvSharp.Cv2.WarpPerspective(src, mat, warp, size);
            return mat.Clone();
        }

        public static Point2d WarpPoint(Point2d pt, Mat warp)
        {
            if (warp == null)
            {
                throw new Exception("WarpMat is null");
            }

            Mat mat = new Mat(3, 1, MatType.CV_64FC1, new double[3] { pt.X, pt.Y, 1.0 }, 0L);
            mat = warp * mat;
            Point2d result = new Point2d(double.NaN, double.NaN);
            if (mat.At<double>(2, 0) != 0.0)
            {
                return new Point2d(mat.At<double>(0, 0) / mat.At<double>(2, 0), mat.At<double>(1, 0) / mat.At<double>(2, 0));
            }

            mat.Dispose();
            return result;
        }

        public static Point3d WarpPoint(Point3d pt, Mat warp)
        {
            if (warp == null)
            {
                throw new Exception("WarpMat is null");
            }
            new Point3d(0.0, 0.0, 0.0);
            double z = pt.Z;
            Point2d point2d = WarpPoint(Rotate(new Point2d(pt.X + 100.0, pt.Y), pt.Z), warp);
            Point2d point2d2 = WarpPoint(new Point2d(pt.X, pt.Y), warp);
            if (point2d2.X == double.NaN || point2d2.Y == double.NaN)
            {
                return new Point3d(double.NaN, double.NaN, double.NaN);
            }
            z = Math.Atan2((point2d - point2d2).Y, (point2d - point2d2).X);
            return new Point3d(point2d2.X, point2d2.Y, z);
        }

        public static Point3d CalMomentResult(Mat Img)
        {
            if (Img == null || Img.Channels() > 1)
            {
                return default(Point3d);
            }
            Moments moments = OpenCvSharp.Cv2.Moments(Img, binaryImage: true);
            double x = moments.M10 / moments.M00;
            double y = moments.M01 / moments.M00;
            if (moments.M00 == 0.0)
            {
                return default(Point3d);
            }
            double num = ((moments.Nu20 - moments.Nu02 != 0.0) ? (Math.Atan(2.0 * moments.Nu11 / (moments.Nu20 - moments.Nu02)) / 2.0) : 0.0);
            if (num > 0.0)
            {
                if (moments.Mu20 < moments.Mu02)
                {
                    num -= Math.PI / 2.0;
                }
            }
            else if (moments.Mu20 < moments.Mu02)
            {
                num = Math.PI / 2.0 + num;
            }
            return new Point3d(x, y, num);
        }

        public static Point3d CalMomentResult(OpenCvSharp.Point[] Contour)
        {
            if (Contour.Length < 3)
            {
                return new Point3d(0.0, 0.0, 0.0);
            }
            OpenCvSharp.Point point = new OpenCvSharp.Point(int.MaxValue, int.MaxValue);
            OpenCvSharp.Point point2 = default(OpenCvSharp.Point);
            for (int i = 0; i < Contour.Length; i++)
            {
                point.X = Math.Min(point.X, Contour[i].X);
                point.Y = Math.Min(point.Y, Contour[i].Y);
                point2.X = Math.Max(point2.X, Contour[i].X);
                point2.Y = Math.Max(point2.Y, Contour[i].Y);
            }
            OpenCvSharp.Point[] array = new OpenCvSharp.Point[Contour.Length];
            for (int j = 0; j < Contour.Length; j++)
            {
                array[j] = Contour[j];
                array[j] -= point;
            }
            Mat mat = new Mat(new OpenCvSharp.Size(point2.X - point.X + 1, point2.Y - point.Y + 1), MatType.CV_8UC1, new Scalar(0.0));
            OpenCvSharp.Cv2.DrawContours(mat, new OpenCvSharp.Point[1][] { array }, 0, new Scalar(255.0), -1, LineTypes.AntiAlias);
            Point3d result = CalMomentResult(mat);
            result.X += point.X;
            result.Y += point.Y;
            mat.Dispose();
            return result;
        }

        public static Point3d CalMomentResult(Point2f[] Contour2f, int scale = 1)
        {
            if (Contour2f.Length < 3)
            {
                return new Point3d(0.0, 0.0, 0.0);
            }
            int num = Contour2f.Length;
            Point2f[] array = new Point2f[num];
            for (int i = 0; i < num; i++)
            {
                array[i] = Contour2f[i] * scale;
            }
            OpenCvSharp.Point point = new OpenCvSharp.Point(int.MaxValue, int.MaxValue);
            OpenCvSharp.Point point2 = default(OpenCvSharp.Point);
            for (int j = 0; j < array.Length; j++)
            {
                point.X = (int)Math.Min(point.X, array[j].X);
                point.Y = (int)Math.Min(point.Y, array[j].Y);
                point2.X = (int)Math.Max(point2.X, array[j].X);
                point2.Y = (int)Math.Max(point2.Y, array[j].Y);
            }
            OpenCvSharp.Point[] array2 = new OpenCvSharp.Point[array.Length];
            for (int k = 0; k < array.Length; k++)
            {
                array2[k] = new OpenCvSharp.Point(array[k].X, array[k].Y);
                array2[k] -= point;
            }
            Mat mat = new Mat(new OpenCvSharp.Size(point2.X - point.X + 1, point2.Y - point.Y + 1), MatType.CV_8UC1, new Scalar(0.0));
            OpenCvSharp.Cv2.DrawContours(mat, new OpenCvSharp.Point[1][] { array2 }, 0, new Scalar(255.0), -1, LineTypes.AntiAlias);
            Point3d result = CalMomentResult(mat);
            result.X += point.X;
            result.Y += point.Y;
            result.X *= 1.0 / (double)scale;
            result.Y *= 1.0 / (double)scale;
            mat.Dispose();
            return result;
        }

        public static Mat HistoAnalysis(Mat src)
        {
            Mat[] images = new Mat[1] { src };
            int[] channels = new int[1];
            float[][] ranges = new float[1][] { new float[2] { 0f, 255f } };
            int[] histSize = new int[1] { 255 };
            Mat mat = new Mat();
            OpenCvSharp.Cv2.CalcHist(images, channels, new Mat(), mat, 1, histSize, ranges);
            return mat;
        }

        public static double[,] DisplayMat(Mat src)
        {
            double[,] array = new double[src.Height, src.Width];
            for (int i = 0; i < src.Height; i++)
            {
                for (int j = 0; j < src.Width; j++)
                {
                    array[i, j] = src.At<double>(i, j);
                }
            }

            return array;
        }

        public static float[,] DisplayMatF(Mat src)
        {
            if (src == null)
            {
                return new float[0, 0];
            }

            float[,] array = new float[src.Height, src.Width];
            for (int i = 0; i < src.Height; i++)
            {
                for (int j = 0; j < src.Width; j++)
                {
                    array[i, j] = src.At<float>(i, j);
                }
            }

            return array;
        }

        public static Point2d[] RotatePts(Point2d[] pts, Point3d center)
        {
            if (pts == null)
            {
                return null;
            }

            Point2d[] array = new Point2d[pts.Length];
            double z = center.Z;
            Point2d point2d = new Point2d(center.X, center.Y);
            for (int i = 0; i < pts.Length; i++)
            {
                array[i] = pts[i] - point2d;
                array[i] = point2d + Rotate(array[i], z);
            }

            return array;
        }

        public static Point2d Rotate(Point2d point, double thetaRad)
        {
            Point2d result = default(Point2d);
            result.X = Math.Cos(thetaRad) * point.X - Math.Sin(thetaRad) * point.Y;
            result.Y = Math.Sin(thetaRad) * point.X + Math.Cos(thetaRad) * point.Y;
            return result;
        }

        public static Point2f Rotate(Point2f point, double thetaRad)
        {
            Point2d point2d = Rotate(new Point2d(point.X, point.Y), thetaRad);
            return new Point2f((float)point2d.X, (float)point2d.Y);
        }

        public static PointF Rotate(PointF point, double thetaRad)
        {
            PointF result = default(PointF);
            result.X = (float)(Math.Cos(thetaRad) * (double)point.X - Math.Sin(thetaRad) * (double)point.Y);
            result.Y = (float)(Math.Sin(thetaRad) * (double)point.X + Math.Cos(thetaRad) * (double)point.Y);
            return result;
        }

        public static Point2d RotateAtCenter(Point2d point, Point2d center, double thetaRad)
        {
            Point2d result = default(Point2d);
            result.X = Math.Cos(thetaRad) * (point.X - center.X) - Math.Sin(thetaRad) * (point.Y - center.Y) + center.X;
            result.Y = Math.Sin(thetaRad) * (point.X - center.X) + Math.Cos(thetaRad) * (point.Y - center.Y) + center.Y;
            return result;
        }

        public static PointF RotateAtCenter(PointF point, PointF center, double thetaRad)
        {
            PointF result = default(PointF);
            result.X = (float)(Math.Cos(thetaRad) * (double)(point.X - center.X) - Math.Sin(thetaRad) * (double)(point.Y - center.Y) + (double)center.X);
            result.Y = (float)(Math.Sin(thetaRad) * (double)(point.X - center.X) + Math.Cos(thetaRad) * (double)(point.Y - center.Y) + (double)center.Y);
            return result;
        }

        public static Point2f RotateAtCenter(Point2f point, Point2f center, double thetaRad)
        {
            Point2f result = default(Point2f);
            result.X = (float)(Math.Cos(thetaRad) * (double)(point.X - center.X) - Math.Sin(thetaRad) * (double)(point.Y - center.Y) + (double)center.X);
            result.Y = (float)(Math.Sin(thetaRad) * (double)(point.X - center.X) + Math.Cos(thetaRad) * (double)(point.Y - center.Y) + (double)center.Y);
            return result;
        }

        public static Mat Rotate(Mat mat, double thetaRad)
        {
            if (mat.Depth() != 64)
            {
                throw new Exception("double 형식 Mat만 됩니다.");
            }

            Mat mat2 = mat.Clone();
            mat2.Set(0, 0, Math.Cos(thetaRad) * mat.At<double>(0, 0) - Math.Sin(thetaRad) * mat.At<double>(1, 0));
            mat2.Set(1, 0, Math.Sin(thetaRad) * mat.At<double>(0, 0) + Math.Cos(thetaRad) * mat.At<double>(1, 0));
            return mat2;
        }

        public static Point2f Point2dTo2f(Point2d P2d)
        {
            return new Point2f((float)P2d.X, (float)P2d.Y);
        }

        public static Point2d Point2fTo2d(Point2f P2d)
        {
            return new Point2d(P2d.X, P2d.Y);
        }

        public static PointF Point2fToF(Point2f P2d)
        {
            return new PointF(P2d.X, P2d.Y);
        }

        public static void StartWatch(string name)
        {
            _qjjt840sKAgRhQ8GhEAPfW9B9Uq = DateTime.Now;
        }

        public static void StopWatch()
        {
            _ = DateTime.Now.Subtract(_qjjt840sKAgRhQ8GhEAPfW9B9Uq).TotalMilliseconds;
        }

        public static double TimeWatch(DateTime dt, string param)
        {
            return DateTime.Now.Subtract(dt).TotalMilliseconds;
        }

        public static Mat PtoMat(Point2d P)
        {
            return new Mat(2, 1, MatType.CV_64FC1, new double[2] { P.X, P.Y }, 0L);
        }

        public static Mat PtoMat(Point3d P)
        {
            return new Mat(3, 1, MatType.CV_64FC1, new double[3] { P.X, P.Y, 1.0 }, 0L);
        }

        public static Point3d MattoP(Mat M, bool isHomo = false)
        {
            Point3d result = new Point3d(0.0, 0.0, 1.0);
            result.X = M.At<double>(0);
            result.Y = M.At<double>(1);
            if (M.Height == 3)
            {
                result.Z = M.At<double>(2);
            }

            if (isHomo && result.Z != 0.0)
            {
                result.X /= result.Z;
                result.Y /= result.Z;
            }

            return result;
        }

        public static Rect ResizeRect(Rect rect, double scale)
        {
            return new Rect((int)Math.Round((double)rect.X * scale), (int)Math.Round((double)rect.Y * scale), (int)Math.Round((double)rect.Width * scale), (int)Math.Round((double)rect.Height * scale));
        }

        public static OpenCvSharp.Size ResizeSize(OpenCvSharp.Size rect, double scale)
        {
            return new OpenCvSharp.Size((int)Math.Round((double)rect.Width * scale), (int)Math.Round((double)rect.Height * scale));
        }

        public static Rect GetRectFixCenter(Rect rect, int W, int H)
        {
            OpenCvSharp.Point point = (rect.TopLeft + rect.BottomRight) * 0.5;
            return new Rect(point.X - W / 2, point.Y - H / 2, W, H);
        }

        public static Rect MoveRect(Rect rect, Point2d move)
        {
            rect.Location += (OpenCvSharp.Point)move;
            return rect;
        }

        public static T GetInstance<T>(Type type) where T : class
        {
            try
            {
                return (T)Activator.CreateInstance(type);
            }
            catch
            {
                return null;
            }
        }

        public static void WaitTime(int milisecond)
        {
            DateTime now = DateTime.Now;
            while ((double)milisecond < DateTime.Now.Subtract(now).TotalMilliseconds)
            {
            }
        }

        public static bool IsPossibleImage(Mat img)
        {
            if (img == null || img.Width == 0 || img.Height == 0)
            {
                return false;
            }

            return true;
        }

        public static Matrix MatToMatrix(Mat mat)
        {
            if (mat.Width != 3 || mat.Height != 3)
            {
                throw new Exception("Mat must be 3x3");
            }

            if (mat.Type() != MatType.CV_32FC1)
            {
                throw new Exception("Mat muyt be CV_32FC1");
            }

            return new Matrix(mat.At<float>(0, 0), mat.At<float>(1, 0), mat.At<float>(0, 1), mat.At<float>(1, 1), mat.At<float>(0, 2), mat.At<float>(1, 2));
        }

        public static Mat MatrixToMat(Matrix matrix)
        {
            return new Mat(3, 3, MatType.CV_32FC1, new float[9]
            {
            matrix.Elements[0],
            matrix.Elements[2],
            matrix.Elements[4],
            matrix.Elements[1],
            matrix.Elements[3],
            matrix.Elements[5],
            0f,
            0f,
            1f
            }, 0L);
        }

        public static Mat ConvertMatTypeDtoF(Mat _d)
        {
            if (_d == null)
            {
                return null;
            }

            Mat mat = new Mat(_d.Rows, _d.Cols, MatType.CV_32FC1);
            for (int i = 0; i < _d.Rows; i++)
            {
                for (int j = 0; j < _d.Cols; j++)
                {
                    mat.Set(i, j, (float)_d.At<double>(i, j));
                }
            }

            return mat;
        }

        public static Point2f[] ChangeArray(PointF[] _pts)
        {
            Point2f[] array = new Point2f[_pts.Length];
            for (int i = 0; i < _pts.Length; i++)
            {
                array[i] = new Point2f(_pts[i].X, _pts[i].Y);
            }

            return array;
        }

        public static PointF[] ChangeArray(Point2f[] _pts)
        {
            PointF[] array = new PointF[_pts.Length];
            for (int i = 0; i < _pts.Length; i++)
            {
                array[i] = new PointF(_pts[i].X, _pts[i].Y);
            }

            return array;
        }

        public static Mat CopyTo(Mat _src, Mat _des, Point2f[] _srcPts, Point2f[] _desPts)
        {
            Mat mat = new Mat(_src.Size(), _src.Type());
            mat.SetTo(1.0);
            _src.CopyTo(_des, mat);
            mat.Dispose();
            return _des;
        }

        public static Point3d CalcFitLineFixedK(List<Point2d> _listPts, double _Kx, double _Ky)
        {
            Point3d result = default(Point3d);
            if (_listPts == null || _listPts.Count == 0)
            {
                return result;
            }

            double num = 0.0;
            for (int i = 0; i < _listPts.Count; i++)
            {
                num += _Kx * _listPts[i].X + _Ky * _listPts[i].Y;
            }

            double num2 = num / (double)_listPts.Count;
            result.Z = Math.Atan((0.0 - _Kx) / _Ky);
            if (Math.Abs(result.Z) < Math.PI / 4.0)
            {
                result.X = 0.0;
                result.Y = num2 / _Ky;
            }
            else
            {
                result.Y = 0.0;
                result.X = num2 / _Kx;
            }

            return result;
        }

        public static double GetDistanceLine2Point(Point3d _line, Point2d _point)
        {
            Point2d point2d = new Point2d(Math.Cos(_line.Z), Math.Sin(_line.Z));
            Point2d p = new Point2d(_point.X - _line.X, _point.Y - _line.Y);
            return Math.Abs(point2d.CrossProduct(p));
        }

        public static double GetRMS(List<Point2d> _listPts, double _Kx, double _Ky)
        {
            if (_listPts == null || _listPts.Count == 0)
            {
                return 0.0;
            }

            double num = 0.0;
            for (int i = 0; i < _listPts.Count; i++)
            {
                num += GetDistanceLine2Point(CalcFitLineFixedK(_listPts, _Kx, _Ky), _listPts[i]);
            }

            return Math.Sqrt(num / (double)_listPts.Count);
        }
    }
}
