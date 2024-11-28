using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Shapes;

namespace OVisionPro.Apps.Views.UserControls
{
    public partial class ucPictureBox : UserControl
    {
        public PictureBox PicInstance;
        public ShapeEditor ShapeInstance;
        public string roiSelectID = "";

        private Dictionary<string, Shape> shapes = new Dictionary<string, Shape>(); // Danh sách các hình
        private Shape selectedShape = null;             // Hình được chọn
        private bool isDraggingCorner = false;
        private bool isDraggingShape = false;
        private int draggingCorner = -1;
        private Point previousMousePosition;

        public ucPictureBox()
        {
            InitializeComponent();
            PicInstance = pictureDebug;
            ShapeInstance = new ShapeEditor();
            ShapeInstance.ShapesUpdated += () => PicInstance.Invalidate(); // Để vẽ lại khi có thay đổi
            PicInstance.Paint += (sender, e) => ShapeInstance.OnPaint(e.Graphics);
            PicInstance.MouseDown += (sender, e) => ShapeInstance.OnMouseDown(e.Location);
            PicInstance.MouseMove += (sender, e) => ShapeInstance.OnMouseMove(e.Location);
            PicInstance.MouseUp += (sender, e) => ShapeInstance.OnMouseUp();
            btnDeleteROI.Click += (sender, e) => ShapeInstance.DeleteSelectedShape();
            //btnCreateROI.Click += (sender, e) => ShapeInstance.AddNewShape(new Point(50, 50), new Point(200, 150));


        }
        public void LoadPointFromVision(string pointID, Point p1, Point p2)
        {
            //ShapeInstance.AddNewShape("", p1, p2);
        }
        private void AddNewShape(string shapeID)
        {
            //Shape sShape = new Shape ;
            //// Thêm một hình mới vào danh sách
            //shapes.Add(shapeID, neư);
            //pictureBox.Invalidate(); // Vẽ lại
        }

        private void pictureDebug_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pictureDebug_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pictureDebug_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void pictureDebug_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDeleteROI_Click(object sender, EventArgs e)
        {

        }

        private void btnCreateROI_Click(object sender, EventArgs e)
        {

        }
    }
}
