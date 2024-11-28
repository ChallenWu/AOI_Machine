using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro
{
    public partial class ucVisionPage : UserControl
    {
        private int taskId = 1;
        public int TaskId
        {
            get { return this.taskId; }
            set
            {
                this.taskId = value;
            }
        }

        private readonly static ucVisionPage instance = new ucVisionPage();
        public ucVisionPage Instance
        {
            get { return instance; }
        }

        public ucVisionPage()
        {
            InitializeComponent();
            XVisionManager.Instance.OnUpdateCCTimeUI += UpdateCicleTimeDurationLbalel;
        }

        public void UpdateCicleTimeDurationLbalel()
        {
            string dur = Math.Round(XVisionManager.Instance.CCTime.TotalSeconds, 2).ToString();
            if (lblCicleTime.InvokeRequired)
            {
                lblCicleTime.Invoke(new Action(() =>
                {
                    lblCicleTime.Text = $"CT: {dur} S";
                }));
            }
            else
            {
                lblCicleTime.Text = $"CT: {dur} S";
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

        public void update_picBoxOrg1(Mat frame)
        {
            picBoxOrg1.Image = MatToBitmap(frame);
        }

        public void update_picBoxOrg2(Mat frame)
        {
            picBoxOrg2.Image = MatToBitmap(frame);
        }
        public void update_picBoxRes1(Mat frame)
        {
            picBoxRes1.Image = MatToBitmap(frame);
        }
        public void update_picBoxRes2(Mat frame)
        {
            picBoxRes2.Image = MatToBitmap(frame);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        //DialogSettingWidget Newfrm = new DialogSettingWidget();
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                /// case 1
                using (var dialog = new DialogSettingWidget())
                {
                    dialog.StartPosition = FormStartPosition.CenterParent;  // Đặt vị trí dialog ở trung tâm của form cha
                    dialog.ShowDialog();              // Hiển thị dialog theo kiểu modal
                }  // Tài nguyên sẽ tự động được giải phóng khi thoát khỏi khối using

                /// case 2
                //DialogSettingWidget.Instance.ShowDialog();
            } catch
            {

            }
        }
    }
}
