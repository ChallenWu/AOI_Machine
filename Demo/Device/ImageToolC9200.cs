using OpenCvSharp;
using OVisionPro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Device
{
    class ImageToolC9200 : ImageToolBase
    {
        public enum FilterTools
        {
            findCircles,
            findLines,
            AOI1,
            AOI2,
            AOI3,
            AOI4,
            AOI5,
            AOI6,
            AOI7,
            AOI8,
            AOI9,
            AOI10,
            AOI11,
            AOI12,
            AOI13,
            AOI14,
            AOI15,
            AOI16,
            AOI17,
            Default,
        }

        public void ExecStep1(Mat frame)
        {
            timeStart = DateTime.Now.Millisecond;
            FilterTools filterTool = FilterTools.AOI1;
            resultTools.Clear();
            switch (filterTool)
            {
                case FilterTools.AOI1:
                    AlignImage(frame);
                    //goto case FilterTools.AOI2;
                    break;
                case FilterTools.AOI2:
                    AOI2(frame);
                    goto case FilterTools.AOI3;
                case FilterTools.AOI3:
                    AOI3(frame);
                    goto case FilterTools.AOI4;
                case FilterTools.AOI4:
                    AOI4(frame);
                    goto case FilterTools.AOI5;
                case FilterTools.AOI5:
                    AOI5(frame);
                    goto case FilterTools.AOI6;
                case FilterTools.AOI6:
                    AOI6(frame);
                    goto case FilterTools.AOI7;
                case FilterTools.AOI7:
                    AOI7(frame);
                    goto case FilterTools.AOI8;
                case FilterTools.AOI8:
                    AOI8(frame);
                    goto case FilterTools.AOI9;
                case FilterTools.AOI9:
                    AOI9(frame);
                    goto case FilterTools.AOI10;
                case FilterTools.AOI10:
                    AOI10(frame);
                    goto case FilterTools.AOI11;
                case FilterTools.AOI11:
                    AOI11(frame);
                    goto case FilterTools.AOI12;
                case FilterTools.AOI12:
                    AOI12(frame);
                    goto case FilterTools.AOI13;
                case FilterTools.AOI13:
                    AOI13(frame);
                    goto case FilterTools.AOI14;
                case FilterTools.AOI14:
                    AOI14(frame);
                    goto case FilterTools.AOI15;
                case FilterTools.AOI15:
                    AOI15(frame);
                    goto case FilterTools.AOI16;
                case FilterTools.AOI16:
                    AOI16(frame);
                    goto case FilterTools.AOI17;
                case FilterTools.AOI17:
                    AOI17(frame);
                    goto case FilterTools.Default;
                case FilterTools.findLines:
                    break;
                case FilterTools.Default:
                    break;
            }
        }

        public void ExecStep2(Mat frame)
        {
            FilterTools filterTool = FilterTools.findCircles;
            switch (filterTool)
            {
                case FilterTools.findCircles:
                    Dictionary<string, object> resultFindCircles = new Dictionary<string, object>();
                    resultTools.Add(nameof(FilterTools.findCircles), resultFindCircles);
                    goto case FilterTools.findLines;
                case FilterTools.findLines:
                    Dictionary<string, object> resultFindLines = new Dictionary<string, object>();
                    resultTools.Add(nameof(FilterTools.findCircles), resultFindLines);
                    break;
            }
            timeStart = DateTime.Now.Millisecond;
            cycleTime = timeStart - timeEnd;
        }


        //public Tuple<int , int , int, int, float> AlignImage(Mat frame)
        //{

        //    if (frame == null) return null;
        //    Mat dst = new Mat();
        //    var ax = cvX.SelectROI(blockID: "AlignImage", cvID: "MainROI");
        //    int mRoix1 = ax.Item1;
        //    int mRoiy1 = ax.Item2;
        //    int mRoix2 = ax.Item3;
        //    int mRoiy2 = ax.Item4;
        //    //Rect roi = new Rect(mRoix1, mRoiy1, mRoix2 - mRoix1, mRoiy2 - mRoiy1);

        //    //Mat roiCheck =  frame.Clone(roi);
        //    //Cv2.ImShow("1111", roiCheck);

        //    cvX.Threshold(frame, dst, blockID: "AlignImage", cvID: "0");
        //    //roiCheck.Dispose();
        //    //dst.Dispose();
        //    return new Tuple<int, int, int, int, float> (1, 1, 1, 1, 20);
        //}

        public Dictionary<string, object> AlignImage(Mat frame)
        {
            if (frame == null) return new Dictionary<string, object>();
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            using (Mat dst = new Mat())
            {
                // Vẽ hình vuông (với vị trí và kích thước cụ thể)=
                Scalar color = new Scalar(0, 0, 255); // Màu đỏ (BGR)
                int thickness = 20; // Độ dày viền
                // Vẽ hình vuông

                // Hiển thị hình ảnh
                Cv2.WaitKey(1); // Đợi người dùng nhấn phím để đóng cửa sổ
                cvX.Threshold(frame, dst, blockID: "AlignImage", cvID: "0");
                var ax = cvX.SelectROI(blockID: "AlignImage", cvID: "MainROI");
                int x1 = ax.Item1;
                int y1 = ax.Item2;
                int x2 = ax.Item3;
                int y2 = ax.Item4;
                OpenCvSharp.Point topLeft = new OpenCvSharp.Point(x1, y1); // Tọa độ góc trên trái
                OpenCvSharp.Point bottomRight = new OpenCvSharp.Point(x2, y2); // Tọa độ góc dưới phải
                Cv2.Rectangle(dst, topLeft, bottomRight, color, thickness);
                cvX.ImShow(funcID: "AlignImage", "DISPLAY", frame);
            }
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }

        public Dictionary<string, object> AOI2(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI2", cvID: "0");
            cvX.ImShow(funcID: "AOI2", "DISPLAY", dst);
            dst.Dispose();

            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }

        public Dictionary<string, object> AOI3(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI3", cvID: "0");
            cvX.ImShow(funcID: "AOI3", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }

        public Dictionary<string, object> AOI4(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI4", cvID: "0");
            cvX.ImShow(funcID: "AOI4", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI5(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI5", cvID: "0");
            cvX.ImShow(funcID: "AOI5", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI6(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI6", cvID: "0");
            cvX.ImShow(funcID: "AOI6", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI7(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI7", cvID: "0");
            cvX.ImShow(funcID: "AOI7", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI8(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI8", cvID: "0");
            cvX.ImShow(funcID: "AOI8", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI9(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI9", cvID: "0");
            cvX.ImShow(funcID: "AOI9", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI10(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI10", cvID: "0");
            cvX.ImShow(funcID: "AOI10", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI11(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI11", cvID: "0");
            cvX.ImShow(funcID: "AOI11", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI12(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI12", cvID: "0");
            cvX.ImShow(funcID: "AOI12", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI13(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI13", cvID: "0");
            cvX.ImShow(funcID: "AOI13", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI14(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI14", cvID: "0");
            cvX.ImShow(funcID: "AOI14", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI15(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI15", cvID: "0");
            cvX.ImShow(funcID: "AOI15", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI16(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI16", cvID: "0");
            cvX.ImShow(funcID: "AOI16", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
        public Dictionary<string, object> AOI17(Mat frame)
        {
            Dictionary<string, object> resultTool = new Dictionary<string, object>();
            Mat dst = new Mat();
            cvX.Threshold(frame, dst, blockID: "AOI17", cvID: "0");
            cvX.ImShow(funcID: "AOI17", "DISPLAY", dst);

            dst.Dispose();
            // Đợi người dùng bấm phím bất kỳ và đóng tất cả cửa sổ
            Cv2.WaitKey(1);
            return resultTool;
        }
    }
}
