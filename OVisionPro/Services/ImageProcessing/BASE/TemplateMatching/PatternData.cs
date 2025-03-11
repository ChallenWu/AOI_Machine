using OVisionPro.Services.ImageProcessing.BASE;
using System;
using OpenCvSharp;

namespace OVisionPro.Services.ImageProcessing.BASE.TemplateMatching
{
    public class PatternData : IDisposable
    {
        private Mat patternImage;
        private Point3f patternCP;
        private InteractMask mask;
        private Mat maskedPattern;
        private double borderColor;

        public Mat PatternImage
        {
            get => this.patternImage;
            set => this.patternImage = value;
        }

        public Point3f PatternCP
        {
            get => this.patternCP;
            set => this.patternCP = value;
        }

        public InteractMask Mask
        {
            get => this.mask;
            set => this.mask = value;
        }

        public Mat MaskedPattern
        {
            get => this.maskedPattern;
            set => this.maskedPattern = value;
        }

        public double BorderColor
        {
            get => this.borderColor;
            set => this.borderColor = value;
        }

        public PatternData()
        {
        }

        public PatternData(Mat pattern, Point3f center, InteractMask mask = null)
        {
            if (pattern == null)
                return;
            Mat image = new Mat(pattern.Rows, pattern.Cols, pattern.Type());
            if (pattern.Type() == MatType.CV_8UC3)
            {
                Cv2.CvtColor(pattern, image, ColorConversionCodes.RGB2GRAY);
            }
            else
            {
                if (!(pattern.Type() == MatType.CV_8UC1))
                    return;
                pattern.Clone();
            }
            this.patternImage?.Dispose();
            this.patternImage = pattern;
            this.patternCP = center;
            this.maskedPattern = this.patternImage.Clone();
            this.borderColor = ((double)this.patternImage.Mean() + (double)sbyte.MaxValue) % (double)byte.MaxValue;
            if (mask == null || mask.MaskMat.Width != this.PatternImage.Width || mask.MaskMat.Height != this.PatternImage.Height)
                this.mask = new InteractMask(pattern.Width, pattern.Height);
            else
                this.mask = mask.Clone();
        }

        public void MakeMaskedPattern()
        {
            if (this.PatternImage == null || this.Mask.MaskMat == null)
                return;
            if (this.MaskedPattern != null)
                this.MaskedPattern.Dispose();
            this.MaskedPattern = new Mat();
            this.PatternImage.CopyTo(this.MaskedPattern, Mask.MaskMat);
            Cv2.ScaleAdd(new Scalar((double)byte.MaxValue) - this.Mask.MaskMat, (double)this.MaskedPattern.Mean((InputArray)this.Mask.MaskMat) / (double)byte.MaxValue, (InputArray)this.MaskedPattern, (OutputArray)this.MaskedPattern);
        }

        public void ResetMaskedPattern()
        {
            if (this.patternImage == null || this.Mask.MaskMat == null)
                return;
            if (this.maskedPattern != null)
                this.maskedPattern.Dispose();
            this.maskedPattern = this.patternImage.Clone();
        }

        public void Dispose()
        {
            if (this.patternImage != null)
                this.patternImage.Dispose();
            if (this.mask == null)
                return;
            this.mask.Dispose();
        }
    }
}
