using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro.Apps.Views.XWidgets
{
    public partial class DialogSettingWidgetLiveCCD : Form
    {
        bool liveNDFlag = false;
        bool living = false;
        bool isrunning = false;
        public DialogSettingWidgetLiveCCD()
        {
            XCCD.Instance.HIK1.postBitmap += UpdateBitMapImg;
            InitializeComponent();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap bitmap = XCCD.Instance.HIK1.frameBitmap;
            if (bitmap == null && !liveNDFlag)
            {
                string msg = "CCD MAT KET NOI.";
                using (var cusdialog = new CustomMessageBox(msg))
                {
                    logW.Ins.error($"[LIVECCD] {msg}");
                    liveNDFlag = true;
                    cusdialog.ShowDialog();
                }
            }
            if (pictureBox1 != null && pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }
            pictureBox1.Image = bitmap;
            bitmap.Dispose();
        }

        public void UpdateBitMapImg(Bitmap bitmap)
        {
            XCCD.Instance.HIK1.postBitmap -= UpdateBitMapImg;
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.Invoke(new Action(() =>
                {
                    if (pictureBox1 != null && pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }
                    pictureBox1.Image = bitmap;
                }));
            }
            else
            {
                if (pictureBox1 != null && pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }
                pictureBox1.Image = bitmap;
            }
            XCCD.Instance.HIK1.postBitmap += UpdateBitMapImg;
        }

        private void btnpause_Click(object sender, EventArgs e)
        {
            XCCD.Instance.HIK1.PauseLiveCCD();
        }

        private void btnLive_Click(object sender, EventArgs e)
        {
            XCCD.Instance.HIK1.StartLiveCCD();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                // Đặt các thuộc tính của hộp thoại
                saveFileDialog.Title = "Chọn vị trí và tên file để lưu";
                saveFileDialog.Filter = "Image Files (*.bmp)|*.bmp|PNG Files (*.png)|*.png|JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg"; // Loại file có thể lưu
                saveFileDialog.DefaultExt = "bmp"; // Định dạng mặc định là txt
                saveFileDialog.AddExtension = true; // Thêm phần mở rộng tự động nếu không có
                saveFileDialog.InitialDirectory = $"{visionGlob.ccdTemplatePatternsPath}"; // Đặt thư mục mặc định
                saveFileDialog.FileName = "ImageCapture.bmp";

                // Hiển thị hộp thoại và kiểm tra xem người dùng có chọn file không
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy đường dẫn tệp đã chọn
                    string filePath = saveFileDialog.FileName;
                    Log.Information("Tệp sẽ được lưu tại: " + filePath);
                    // Lưu hình ảnh vào đường dẫn
                    OpenCvSharp.Mat mat = XCCD.Instance.HIK1.frame;
                    if (mat == null || mat.Width <= 0) {
                        using (var dia = new CustomMessageBox("LUU ANH THAT BAI. LOI CCD."))
                        {
                            dia.ShowDialog();
                            dia.Dispose();
                        }
                    } else
                    {
                        mat.SaveImage(filePath);
                        Log.Information("LUU ANH THANH CONG: " + filePath);
                    }
                }
            }
        }

        private void DialogSettingWidgetLiveCCD_FormClosed(object sender, FormClosedEventArgs e)
        {
            XCCD.Instance.HIK1.PauseLiveCCD();
            XCCD.Instance.HIK1.postBitmap -= UpdateBitMapImg;
            XCCD.Instance.HIK1.ReleaseFrame();
            XCCD.Instance.HIK1.ReleaseFrameBitmap();
            pictureBox1.Image?.Dispose();
            pictureBox1?.Dispose();
            pictureBox1 = null;
            this.Dispose();
        }

        private void DialogSettingWidgetLiveCCD_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void DialogSettingWidgetLiveCCD_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void DialogSettingWidgetLiveCCD_MouseEnter(object sender, EventArgs e)
        {

        }

        private void DialogSettingWidgetLiveCCD_MouseLeave(object sender, EventArgs e)
        {

        }
    }
}
