using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro
{
    public class FormUpdatePicCore : Form
    {
        public PictureBox picImgDisplay;
        public object lockObj = new object();
       
        public virtual void updateImg(string winname, OpenCvSharp.Mat mat)
        {
            if (picImgDisplay == null || picImgDisplay.Image == null || mat == null || mat.Width <= 0 || mat.Height <= 0) return;
            lock (lockObj) { picImgDisplay.Image?.Dispose(); picImgDisplay.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat); }
        }
    }
}
