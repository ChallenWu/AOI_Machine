using OpenCvSharp;
using OVisionPro.Apps.Views.UserControls;
using System;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace OVisionPro
{
    public partial class DialogSettingWidget : Form
    {
        public PictureBox PicInstance;
        public string roiSelectID = "";
        int WhiteoffsetX = 0;
        int WhiteoffsetY = 0;
        int displayWidthReal = 0;
        int displayHeightReal = 0;
        float zoomScaleX = 1f;
        float zoomScaleY = 1f;
        float zoomScale = 1f;
        public Image currentImg;

        public DialogSettingWidget()
        {
            XVisionManager.Instance.VisionRunMode = Globals.RunMode.debug;
            InitializeComponent();
            LoadCBBFuncID();
            LoadCBBTasksID();
            InitializeScale();
            //this.txtBrowserIgFile = new DragDropTextBox
            //{
            //    AllowedExtensions = new[] { ".txt", ".csv" }, // Chỉ cho phép tệp .txt và .csv
            //    Multiline = true,
            //    Dock = DockStyle.Fill
            //};
            StartPosition = FormStartPosition.CenterParent;
            PicInstance = pictureBox1;
            ShapeEditor.Instance.ShapesUpdated += () => IInvalidate(); // Để vẽ lại khi có thay đổi
            ShapeEditor.Instance.pictureDrawing = PicInstance;
            PicInstance.MouseDown += (sender, e) => ShapeEditor.Instance.OnMouseDown(e.Location);
            PicInstance.MouseMove += (sender, e) => ShapeEditor.Instance.OnMouseMove(e.Location);
            PicInstance.MouseUp += (sender, e) => ShapeEditor.Instance.OnMouseUp();

            //btnDeleteROI.Click += (sender, e) => ShapeEditor.Instance.DeleteSelectedShape();
            //btnCreateROI.Click += (sender, e) => ShapeEditor.Instance.AddNewShape(new System.Drawing.Point(50, 50), new System.Drawing.Point(200, 150));

            
            propertyGrid1.SelectedObject = XParameterManager.Instance.propertyGridData;

            cvX.onUpdateFrameToUIAction += LoadPicturePropertyGrid;
            XParameterManager.Instance.onUpdateFuncIDToUIAction += LoadCBBFuncID;
        }

        public void LoadCBBTasksID()
        {
            cbbModelTasks.Items.Clear();
            foreach (var key in XVisionManager.Instance.TaskRuns[XVisionManager.Instance.GetSelectModelRun()].Keys)
            {
                cbbModelTasks.Items.Add(key);
            }
            cbbModelTasks.SelectedIndex = 0;
        }

        public void LoadCBBFuncID(string funcID=null)
        {
            cbbFuncID.Items.Clear();
            foreach (var key in XParameterManager.Instance.ucBlockBasicControllers.Keys)
            {
                cbbFuncID.Items.Add(key);
            }
            cbbFuncID.SelectedIndex = 0;
            this.Update();
            //cbbFuncID.EndUpdate();
        }

        public void IInvalidate()
        {
            if (PicInstance == null)
                return;
            PicInstance.Invalidate();
        }
        public void IOnPaint(Graphics g)
        {
            if (PicInstance == null)
                return;
            ShapeEditor.Instance.OnPaint(g);
        }
        private void DialogSettingWidget_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        public Bitmap MatToBitmap(Mat mat)
        {
            // Convert từ Mat sang Bitmap

            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
        }
        private readonly object lockObject = new object();

        public void LoadPicturePropertyGrid(OpenCvSharp.Mat frame)
        {
            cvX.onUpdateFrameToUIAction -= LoadPicturePropertyGrid;
            if (frame == null) return;

            // Lock để ngăn chặn các luồng khác đồng thời truy cập
            lock (lockObject)
            {
                try
                {

                    // Lấy kích thước từ Mat
                    int matWidth = frame.Width;
                    int matHeight = frame.Height;
                    // Chuyển đổi từ Mat sang Bitmap
                    Bitmap bitmapImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                    // Lấy kích thước từ Bitmap
                    int bitmapWidth = bitmapImage.Width;
                    int bitmapHeight = bitmapImage.Height;
                    //MessageBox.Show($"Kích thước ảnh từ Bitmap: {bitmapWidth}x{bitmapHeight}");

                    // Kiểm tra nếu kích thước không khớp
                    //if (matWidth != bitmapWidth || matHeight != bitmapHeight)
                    //{
                    //    MessageBox.Show("Kích thước Mat và Bitmap không khớp.");
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Kích thước Mat và Bitmap khớp nhau.");
                    //}
                    // Xóa hình ảnh cũ nếu có
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Image = bitmapImage;
                    currentImg = pictureBox1.Image;
                    InitializeScale();
                    ShapeEditor.Instance.ConvertCoordinerImgToPic("ALL");

                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"Error updating PictureBox: {ex.Message}");
                }
            }
            cvX.onUpdateFrameToUIAction += LoadPicturePropertyGrid;
        }

        protected override void Dispose(bool disposing)
        {
            try
            {

                if (disposing)
                {

                    PicInstance.Paint -= (sender, e) => ShapeEditor.Instance.OnPaint(e.Graphics);
                    PicInstance.MouseDown -= (sender, e) => ShapeEditor.Instance.OnMouseDown(e.Location);
                    PicInstance.MouseMove -= (sender, e) => ShapeEditor.Instance.OnMouseMove(e.Location);
                    PicInstance.MouseUp -= (sender, e) => ShapeEditor.Instance.OnMouseUp();
                    btnDeleteROI.Click -= (sender, e) => ShapeEditor.Instance.DeleteSelectedShape();
                    //btnCreateROI.Click -= (sender, e) => ShapeEditor.Instance.AddNewShape(new System.Drawing.Point(50, 50), new System.Drawing.Point(200, 150));

                    // Giải phóng tài nguyên hình ảnh của PictureBox nếu có
                    pictureBox1.Image?.Dispose();
                    pictureMainCCD.Image?.Dispose();
                    pictureBox1.Dispose();
                    pictureMainCCD.Dispose();
                    propertyGrid1.Dispose();
                    customPropertyGrid1.Dispose();
                    toolStrip2.Dispose();
                    toolStrip1.Dispose();
                    toolStripSeparator1.Dispose();
                    splitContainer1.Dispose();

                    pictureBox1.Image = null;
                    pictureMainCCD.Image = null;
                    pictureBox1 = null;
                    pictureMainCCD = null;
                    propertyGrid1 = null;
                    customPropertyGrid1 = null;
                    toolStrip2 = null;
                    toolStrip1 = null;
                    toolStripSeparator1 = null;
                    splitContainer1 = null;


                    PicInstance = null;
                    roiSelectID = null;
                    cvX.onUpdateFrameToUIAction = null;

                    checkBox1.Dispose();
                    checkBox2.Dispose();
                    checkBox3.Dispose();
                    // Giải phóng tài nguyên của các điều khiển hoặc tài nguyên quản lý khác

                    if (components != null)
                    {
                        components.Dispose();
                    }
                    // Lặp qua tất cả các controls trong form và dispose từng cái
                    foreach (Control ctrl in this.Controls)
                    {
                        ctrl.Dispose();
                    }
                }
                GC.Collect();  // Yêu cầu Garbage Collector thu hồi bộ nhớ ngay lập tức
                //GC.WaitForFullGCComplete();
                //this.FormClosed -= DialogSettingWidget_FormClosed;
            }
            catch
            {
            }
            base.Dispose(disposing);
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            string parentKLey = e.ChangedItem.PropertyDescriptor.Name;
            if (parentKLey.Contains("SelectROI"))
            {
                string[] res_cv_func = parentKLey.Split(new string[] { "_1_" }, StringSplitOptions.None);if (res_cv_func[1].Contains("1")) ;
                string typeP = "BR";
                if (res_cv_func[1].Contains("1"))
                {
                    typeP = "TL";
                }
                ShapeEditor.Instance.ConvertCoordinerImgToPic(typeP);
                pictureBox1.Refresh();
            }
            propertyGrid1.Refresh();
        }// Đặt trong hàm khởi tạo hoặc Main

        public void InitializeScale()
        {
            // Lấy ảnh gốc từ PictureBox
            Image img = pictureBox1.Image;
            if (img == null) return;
            Rectangle rectImg = GetImageRectangle(pictureBox1);
            // Kích thước của ảnh gốc
            int imgWidth = rectImg.Width;
            int imgHeight = rectImg.Height;
            //MessageBox.Show($"{imgWidth}-{imgHeight}      {img.Width}-{img.Height}");

            // Kích thước của PictureBox
            int pbWidth = pictureBox1.Width;
            int pbHeight = pictureBox1.Height;

            // Tính tỷ lệ phóng to/thu nhỏ (scale)
            zoomScaleX = (float)pbWidth / imgWidth;
            zoomScaleY = (float)pbHeight / imgHeight;

            // Chọn tỷ lệ nhỏ hơn để đảm bảo ảnh vừa với PictureBox
            zoomScale = Math.Min(zoomScaleX, zoomScaleY);

            // Tính kích thước thực tế của ảnh hiển thị trong PictureBox
            displayWidthReal = (int)(imgWidth * zoomScale);
            displayHeightReal = (int)(imgHeight * zoomScale);

            // Tính khoảng trắng (padding) xung quanh ảnh
            WhiteoffsetX = (pbWidth - displayWidthReal) / 2;
            WhiteoffsetY = (pbHeight - displayHeightReal) / 2;


            ShapeEditor.Instance.zoomScaleX = zoomScaleX;
            ShapeEditor.Instance.zoomScaleY = zoomScaleY;
            ShapeEditor.Instance.zoomScale = zoomScale;
            ShapeEditor.Instance.WhiteoffsetX = WhiteoffsetX;
            ShapeEditor.Instance.WhiteoffsetY = WhiteoffsetY;
            ShapeEditor.Instance.displayWidthReal = displayWidthReal;
            ShapeEditor.Instance.displayHeightReal = displayHeightReal;
        }

        private void MouseEventArgs(object sender, MouseEventArgs e)
        {
            try
            {
                if (pictureBox1.Image == null) return;
                // Vẽ điểm highlight tại vị trí mới
                InitializeScale();
                // Lấy tọa độ chuột trong PictureBox
                int mouseX = e.X;
                int mouseY = e.Y;

                ///////////////////////////////////////////////////
                // Hiển thị tỷ lệ zoom trong tiêu đề form
                this.Text = $"Model: {XVisionManager.Instance.GetSelectModelRun()} Tỷ lệ zoom: {zoomScale * 100}%";
                //// Lấy tọa độ trên ảnh gốc
                //int imgX1 = (int)(e.X * zoomScaleX);
                //int imgY1 = (int)(e.Y * zoomScaleY);
                // Hiển thị tọa độ trên ảnh gốc
                ////////////////////////////////////////////////////

                // Kiểm tra xem tọa độ chuột có nằm trong vùng hiển thị của ảnh không
                if (mouseX >= WhiteoffsetX && mouseX <= WhiteoffsetX + displayWidthReal && mouseY >= WhiteoffsetY && mouseY <= WhiteoffsetY + displayHeightReal)
                {
                    // Điểm nằm trong ảnh, chuyển đổi sang tọa độ của ảnh gốc
                    int imgX = (int)((mouseX - WhiteoffsetX) / zoomScale);
                    int imgY = (int)((mouseY - WhiteoffsetY) / zoomScale);
                    this.Text += $" | Tọa độ ảnh gốc: X = {imgX}, Y = {imgY}";
                    //MessageBox.Show($"Điểm nằm trong ảnh tại tọa độ gốc: ({imgX}, {imgY}) -- {mouseX}, {mouseY}");
                }
                else
                {
                    // Điểm nằm ngoài vùng ảnh (khoảng trắng)
                    //MessageBox.Show("Điểm nằm ngoài ảnh (khoảng trắng).");
                }
            }
            catch
            {

            }
        }

        private System.Drawing.Point ConvertImgResizeToOrgCoordinate(System.Drawing.Point originalPoint)
        {
            Rectangle imageRect = GetImageRectangle(pictureBox1);
            float scaleX = (float)imageRect.Width / pictureBox1.Image.Width;
            float scaleY = (float)imageRect.Height / pictureBox1.Image.Height;

            return new System.Drawing.Point(
                (int)(originalPoint.X * scaleX + imageRect.X),
                (int)(originalPoint.Y * scaleY + imageRect.Y)
            );
        }
        private Rectangle GetImageRectangle(PictureBox pictureBox)
        {
            var propertyInfo = pictureBox.GetType().GetProperty(
                "ImageRectangle",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
            return (Rectangle)propertyInfo.GetValue(pictureBox, null);
        }

        private void btnStartDebug_Click(object sender, EventArgs e)
        {
            XVisionManager.Instance.StartWorkInTaskDebug(XVisionManager.Instance.GetSelectModelRun(), int.Parse(cbbModelTasks.Text), txtDragTxtBox.Text);
        }

        private void btnSaveCVConfig_Click(object sender, EventArgs e)
        {
            XParameterManager.Instance.SaveConfig();
            propertyGrid1.Refresh();
        }

        private void DialogSettingWidget_FormClosed_1(object sender, FormClosedEventArgs e)
        {
            XVisionManager.Instance.VisionRunMode = Globals.RunMode.production;
        }

        private void cbbFuncID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FuncID = cbbFuncID.Text;
            XVisionManager.Instance.SetSelectFuncID(FuncID);
            XParameterManager.Instance.ChangeFunctionDebug(FuncID);
            propertyGrid1.Refresh();
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            XParameterManager.Instance.LoadConfig();
            var X = XParameterManager.Instance.ucBlockBasicControllers;
            propertyGrid1.Refresh();
        }

        private void DialogSettingWidget_Resize(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            InitializeScale();
            //Rectangle abc = GetImageRectangle(pictureBox1);
            // Tính tỷ lệ zoom của ảnh so với kích thước PictureBox
            //zoomScaleX = (float)abc.Width / pictureBox1.Width;
            //zoomScaleY = (float)abc.Height / pictureBox1.Height;

            //// Tỷ lệ zoom tổng quát (lấy tỷ lệ lớn nhất giữa chiều rộng và chiều cao)
            //zoomScale = Math.Min(zoomScaleX, zoomScaleY);
            //ShapeEditor.Instance.zoomScale = zoomScale;
            //ShapeEditor.Instance.zoomScaleX = zoomScaleX;
            //ShapeEditor.Instance.zoomScaleY = zoomScaleY;

            ShapeEditor.Instance.ConvertCoordinerImgToPic();
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            ShapeEditor.Instance.OnPaint(e.Graphics);
            propertyGrid1.Refresh();
        }
    }
}
