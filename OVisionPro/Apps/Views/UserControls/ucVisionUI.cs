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
    public partial class VisionUI : UserControl
    {
        public VisionUI()
        {
            InitializeComponent();
        }
        private int taskId = 1;
        public int TaskId
        {
            get { return this.taskId; }
            set
            {
                this.taskId = value;
            }
        }


        private void timerStreamCCD_Tick(object sender, EventArgs e)
        {
            //using (var frame = new OpenCvSharp.Mat())
            //{
            //    //if (XCCDManager.Read(frame)) // Lấy khung hình từ camera
            //    //{
            //    //    pictureMainCCD.Image = MatToBitmap(frame); // Cập nhật vào PictureBox
            //    //}
            //}
        }

        //private void tabVSBZ0010_Paint(object sender, PaintEventArgs e)
        //{
        //    // Lấy Graphics object
        //    Graphics g = e.Graphics;

        //    // Lấy tọa độ của Button1 và Button2
        //    Point start = new Point(ucBlockBasic1..Right, button1.Top + button1.Height / 2);
        //    Point end = new Point(button2.Left, button2.Top + button2.Height / 2);

        //    // Vẽ mũi tên
        //    using (Pen pen = new Pen(Color.Black, 2))
        //    {
        //        pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
        //        g.DrawLine(pen, start, end);
        //    }
        //}
    }
}
