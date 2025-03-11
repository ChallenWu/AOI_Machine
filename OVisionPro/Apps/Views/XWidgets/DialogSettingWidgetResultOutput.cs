using OpenCvSharp;
using OVisionPro.Services.ImageProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace OVisionPro.Apps.Views.XWidgets
{
    public partial class DialogSettingWidgetResultOutput : Form
    {
        public bool outputShow = true;
        public OpenCvSharp.Mat inMat;
        public OpenCvSharp.Mat outMat;
        public DialogSettingWidgetResultOutput()
        {
            InitializeComponent();
            cvX.onUpdateOutFrameToUIAction += updateOutMat;
            cvX.onUpdateInFrameToUIAction += updateInMat;
            if (outputShow) { button1.Text = "OUTPUT"; }
            else {  button1.Text = "INPUT"; }
        }
        public void updateInMat(int winname, Mat mat)
        {
            if (OBasicAlgorithm.IsMat(mat))
            {
                inMat?.Dispose();
                inMat = mat.Clone();
            }
            if (!outputShow)
            {
                if (OBasicAlgorithm.IsMat(inMat))
                {
                    pictureBox2.Image?.Dispose();
                    pictureBox2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(inMat);
                }
                else { using (var dia = new CustomMessageBox("INPUT IMAGE KHONG CO")) { dia.ShowDialog(); } }
            }
        }
        public void updateOutMat(int winname, Mat mat)
        {
            if (OBasicAlgorithm.IsMat(mat))
            {
                outMat?.Dispose();
                outMat = mat.Clone();
            }

            if (outputShow)
            {
                if (OBasicAlgorithm.IsMat(outMat))
                {
                    pictureBox2.Image?.Dispose();
                    pictureBox2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outMat);
                }
                else { using (var dia = new CustomMessageBox("OUTPUT IMAGE KHONG CO")) { dia.ShowDialog(); } }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            outputShow = !outputShow;
            if (outputShow)
            {
                button1.Text = "OUTPUT";
                if (OBasicAlgorithm.IsMat(outMat))
                {
                    pictureBox2.Image?.Dispose();
                    pictureBox2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(outMat);
                }
                else { using (var dia = new CustomMessageBox("OUTPUT IMAGE KHONG CO")) { dia.ShowDialog(); } }
            }
            else
            {
                button1.Text = "INPUT";
                if (OBasicAlgorithm.IsMat(inMat))
                {
                    pictureBox2.Image?.Dispose();
                    pictureBox2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(inMat);
                }
                else { using (var dia = new CustomMessageBox("INPUT IMAGE KHONG CO")) { dia.ShowDialog(); } }
            }
        }

        private void DialogSettingWidgetResultOutput_FormClosed(object sender, FormClosedEventArgs e)
        {
            cvX.onUpdateOutFrameToUIAction -= updateOutMat;
            cvX.onUpdateInFrameToUIAction -= updateInMat;
            inMat?.Dispose();
            inMat = null;
            outMat?.Dispose();
            outMat = null;
        }
    }
}
