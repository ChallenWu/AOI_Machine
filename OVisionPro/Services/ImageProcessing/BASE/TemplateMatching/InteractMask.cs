using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.Serialization;
using OpenCvSharp;
using OVisionPro.Services.ImageProcessing;
namespace OVisionPro.Services.ImageProcessing.BASE.TemplateMatching
{
    public class InteractMask : IDisposable
    {
        private int height;
        private int width;

        private int SelectionSize;
        private bool IsPositionChange;
        public Mat TransformMat;

        [NonSerialized]
        private int penSize; // Độ dày của bút vẽ trên mask

        [NonSerialized]
        private Mat maskMat; // Mat để lưu trữ mask

        [NonSerialized]
        private Mat overlayMat; // Mat cho hình ảnh overlay, thường được dùng để render

        [NonSerialized]
        private bool isDrawingOnMask; // Cờ kiểm tra xem mask có đang được vẽ hay không

        [NonSerialized]
        private bool shouldDrawMask; // Cờ kiểm tra xem mask có cần được vẽ hay không

        [NonSerialized]
        private bool shouldDisplayMask; // Cờ kiểm tra xem mask có cần được hiển thị hay không

        [NonSerialized]
        private PointF lastMousePosition; // Vị trí chuột cuối cùng trên mask

        public int PenSize
        {
            get { return penSize; }
            set { penSize = value; }
        }

        public bool IsDrawingOnMask
        {
            get { return isDrawingOnMask; }
            set { isDrawingOnMask = value; }
        }

        public bool ShouldDrawMask
        {
            get { return shouldDrawMask; }
            set { shouldDrawMask = value; }
        }

        public bool ShouldDisplayMask
        {
            get { return shouldDisplayMask; }
            set { shouldDisplayMask = value; }
        }

        public Mat MaskMat => maskMat;

        public bool IsDisposed
        {
            get
            {
                return maskMat == null || overlayMat == null || maskMat.IsDisposed || overlayMat.IsDisposed;
            }
        }

        public InteractMask(int width, int height)
        {
            this.height = height;
            this.width = width;
            ShouldDrawMask = true;
            IsDrawingOnMask = true;
            ShouldDisplayMask = true;
            maskMat = new Mat(height, width, MatType.CV_8UC1, new Scalar(255.0));
            overlayMat = new Mat(height, width, MatType.CV_8UC4, new Scalar(0.0, 0.0, 0.0, 0.0));
        }

        public override string ToString()
        {
            return "Mask";
        }

        public void ResetMask()
        {
            Dispose();
            maskMat = new Mat(height, width, MatType.CV_8UC1, new Scalar(255.0));
            overlayMat = new Mat(height, width, MatType.CV_8UC4, new Scalar(0.0, 0.0, 0.0, 0.0));
        }

        public bool FindPoint(PointF mouseLocation)
        {
            if (!IsPositionChange || !ShouldDrawMask)
            {
                return false;
            }

            if (lastMousePosition == new PointF(-10000, -10000))
            {
                lastMousePosition = mouseLocation;
                return true;
            }

            Point2d point2d1 = OBasicAlgorithm.FixtureToImage2D(new OpenCvSharp.Point((int)Math.Round(lastMousePosition.X), (int)Math.Round(lastMousePosition.Y)), TransformMat);
            Point2d point2d2 = OBasicAlgorithm.FixtureToImage2D(new OpenCvSharp.Point((int)Math.Round(mouseLocation.X), (int)Math.Round(mouseLocation.Y)), TransformMat);
            Scalar color = (!IsDrawingOnMask) ? new Scalar(255.0) : new Scalar(0.0);
            if (penSize == 0)
            {
                penSize = SelectionSize;
            }

            maskMat.Line((int)point2d1.X, (int)point2d1.Y, (int)point2d2.X, (int)point2d2.Y, color, penSize);
            lastMousePosition = mouseLocation;
            return true;
        }
        public void SelectPoint()
        {
            if (!IsPositionChange && ShouldDrawMask)
            {
                IsPositionChange = true;
                lastMousePosition = new PointF(-10000f, -10000f);
            }
        }

        public void ResetSelectedPoint()
        {
            IsPositionChange = false;
        }

        public void Draw(Graphics gdi)
        {
            if (ShouldDisplayMask && overlayMat != null && !overlayMat.IsDisposed && overlayMat.Width != 0 && maskMat != null && !maskMat.IsDisposed && maskMat.Width != 0)
            {
                Mat mat = new Mat();
                OpenCvSharp.Size size = new OpenCvSharp.Size(maskMat.Width / 5, maskMat.Height / 5);
                Mat maskResized = new Mat(size, MatType.CV_8UC1, 0.0);
                Mat mergedMat = new Mat();
                Cv2.Resize(255.0 - maskMat, mat, size);
                Cv2.Merge(new Mat[4] { mat, mat, maskResized, mat * 0.3 }, mergedMat);
                Cv2.Resize(mergedMat, overlayMat, overlayMat.Size());
                Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(overlayMat);
                gdi.DrawImageUnscaled(bitmap, 0, 0);
                mat.Dispose();
                maskResized.Dispose();
                mergedMat.Dispose();
                bitmap.Dispose();
            }
        }

        public bool IsPossible(OpenCvSharp.Mat inputImage)
        {
            if (inputImage == null || inputImage.Width == 0 || inputImage.Height == 0)
            {
                return false;
            }

            if (MaskMat.Width != inputImage.Width || MaskMat.Height != inputImage.Height)
            {
                return false;
            }

            return true;
        }

        protected InteractMask(SerializationInfo info, StreamingContext context)
        {
            if (overlayMat == null || overlayMat.IsDisposed || overlayMat.Width == 0)
            {
                overlayMat = new Mat(height, width, MatType.CV_8UC4, new Scalar(0.0, 0.0, 0.0, 0.0));
            }
        }

        public void Dispose()
        {
            if (maskMat != null && !maskMat.IsDisposed)
            {
                maskMat.Dispose();
            }

            if (overlayMat != null && !overlayMat.IsDisposed)
            {
                overlayMat.Dispose();
            }
        }

        void IDisposable.Dispose()
        {
            // ILSpy generated this explicit interface implementation from .override directive in Dispose
            this.Dispose();
        }

        public InteractMask Clone()
        {
            if (maskMat == null)
            {
                return null;
            }

            Mat clonedMaskMat = maskMat.Clone();
            Mat clonedOverlayMat = overlayMat.Clone();
            return new InteractMask(width, height)
            {
                maskMat = clonedMaskMat,
                overlayMat = clonedOverlayMat
            };
        }
    }
}
