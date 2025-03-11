using OpenCvSharp;
using OVisionPro.Apps.Views.XWidgets;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro
{
    public partial class ucVisionPage : UserControl
    {
        private int taskId = 1;
        public static PictureBox pRes1, pRes2, pOrg1, pOrg2;
        public static Label lbltime, lblAOI;
        private Stopwatch stopwatch = new Stopwatch();
        public int TaskId
        {
            get { return this.taskId; }
            set
            {
                this.taskId = value;
            }
        }

        public readonly static ucVisionPage instance = new ucVisionPage();
        public ucVisionPage Instance
        {
            get { return instance; }
        }

        public ucVisionPage()
        {
            InitializeComponent();
            XVisionManager.Instance.OnUpdateResultUI += UpdateCicleTimeDurationLbalel;
            //cvX.onUpdateInFrameToUIAction += UpdatePicInput;
            //cvX.onUpdateOutFrameToUIAction += UpdatePicOutput;
            pRes1 = picBoxRes1;
            pRes2 = picBoxRes2;
            pOrg1 = picBoxOrg1;
            pOrg2 = picBoxOrg2;
            lbltime = lblCicleTime;
            lblAOI = lblResultAOI;
            // Tạo một đối tượng Stopwatch
        }

        public void UpdatePicInput(int stepID, Mat frame)
        {
            if (stepID == 0)
            {
                stopwatch.Reset();
                stopwatch.Start();
            }
            try
            {

                if (frame == null) return;
                if (stepID == 0)
                {
                    pOrg1?.Image.Dispose();
                    pOrg1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                }
                else if (stepID == 1)
                {
                    pOrg2?.Image.Dispose();
                    pOrg2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                }
            }
            catch (Exception e)
            {
                Log.Information("UpdatePicInput: " + e.ToString());
            }
        }

        public void UpdatePicOutput(int stepID, Mat frame)
        {
            if (stepID == 0)
            {
                stopwatch.Stop();
            }
            try
            {
                if (lbltime.InvokeRequired)
                {
                    lbltime.Invoke(new Action(() =>
                    {
                        lbltime.Text = $"CT: {(float)stopwatch.ElapsedMilliseconds / 1000} S";
                    }));
                }
                else
                {
                    lbltime.Text = $"CT: {(float)stopwatch.ElapsedMilliseconds / 1000} S";
                }
                if (frame == null) return;
                if (frame.Width <= 0 || frame.Height <= 0) return;
                if (stepID == 0)
                {
                    pRes1?.Image.Dispose();
                    pRes1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                }
                else if (stepID == 1)
                {
                    pRes2?.Image.Dispose();
                    pRes2.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                }
            }
            catch (Exception e)
            {
                Log.Information("UpdatePicOutput: " + e.ToString());
            }
        }

        public void UpdateCicleTimeDurationLbalel(string resAOI)
        {
            try
            {
                System.Drawing.Color colorRes = (resAOI != "OK") ? System.Drawing.Color.Red : System.Drawing.Color.Green;
                if (lblAOI.InvokeRequired)
                {
                    lblAOI.Invoke(new Action(() =>
                    {
                        lblAOI.Text = $"{resAOI}";
                        lblAOI.BackColor = colorRes;
                    }));
                }
                else
                {
                    lblAOI.Text = $"{resAOI}";
                    lblAOI.BackColor = colorRes;
                }
            }
            catch (Exception ex)
            {

                Log.Information("UpdateCicleTimeDurationLbalel: " + ex.ToString());
            }
        }

        public void InitializeStreamCamera()
        {

        }

        public Bitmap MatToBitmap(Mat mat)
        {
            // Convert từ Mat sang Bitmap
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                /// case 1
                using (var dialog = new DialogSettingWidgetHome())
                {
                    dialog.StartPosition = FormStartPosition.CenterParent;  // Đặt vị trí dialog ở trung tâm của form cha
                    dialog.ShowDialog();              // Hiển thị dialog theo kiểu modal
                }  // Tài nguyên sẽ tự động được giải phóng khi thoát khỏi khối using

                /// case 2
                //DialogSettingWidget.Instance.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[btnclick1] ex: {ex}");
            }
        }
        public void EnableConfigButton()
        {
            btnStartConfig.Visible = true;
        }
        public void DisableConfigButton()
        {
            btnStartConfig.Visible = false;
        }
    }
}
