using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OVisionPro.Services._01._Core.ShapesPics;
using OVisionPro.Services.ImageProcessing;
using static OVisionPro.visionGlob;

namespace OVisionPro.Apps.Views.XWidgets
{
    public partial class DialogSettingWidgetROIs : FormUpdatePicCore
    {
        public string roiSelectID = "";
        public int selectedTaskIDRun = 0;
        // tọa độ khoảng trắng.
        int WhiteoffsetX = 0;
        int WhiteoffsetY = 0;
        // tọa độ thực tế của ảnh sau mỗi lần update picture của ảnh.
        int displayWidthReal = 0;
        int displayHeightReal = 0;
        // tỉ lệ zoom img so với picture
        float zoomScaleX = 1f;
        float zoomScaleY = 1f;
        float zoomScale = 1f;
        public string funcID = "";
        public int taskID = 0;
        public Mat matOrg;
        public Mat matRes;
        public ShapesPicROIs shapesIntance = new ShapesPicROIs();
        public XObjectCVParameters paramObj = new XObjectCVParameters();


        public DialogSettingWidgetROIs(string functionID, int TaskID) 
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            funcID = functionID;
            taskID = TaskID;
            propertyGrid1.SelectedObject = XParameterManager.Instance.propertyGridData;
            propertyGrid1.Refresh();

            shapesIntance.funcDraw = functionID;
            shapesIntance.usingGroupKey = functionID;
            shapesIntance.pictureDrawing = pictureBox1;

            XParameterManager.Instance.SelectedBlockID = functionID;
            XParameterManager.Instance.ChangeFunctionDebug(functionID);
            InitializeScale();
            shapesIntance.ShapesUpdated += () => IUpdateInvalidate(); // Để vẽ lại khi có thay đổi
            shapesIntance.ShapesAdded += () => IADDFromROI(); // Để thêm selectROI
            shapesIntance.shapesDeleted += () => IDELFromROI(); // Để thêm selectROI
            pictureBox1.MouseDoubleClick += (sender, e) => shapesIntance.OnMouseDBClick(e.Location);
            pictureBox1.MouseDown += (sender, e) => shapesIntance.OnMouseDown(e.Location);
            pictureBox1.MouseMove += (sender, e) => shapesIntance.OnMouseMove(e.Location);
            pictureBox1.MouseUp += (sender, e) => shapesIntance.OnMouseUp();
            pictureBox1.DragDrop += (sender, e) => shapesIntance.DropArea_Drop(sender, e);
            pictureBox1.DragEnter += (sender, e) => shapesIntance.DropArea_DragEnter(sender, e);
            btnDeleteROI.Click += (sender, e) => shapesIntance.DeleteSelectedShape();
            pictureBox1.AllowDrop = true; // bat cho phep drop
            shapesIntance.LoadToShapesFromConfig();
        }
        public Dictionary<string, object> ConvertDictToNestDict(Dictionary<string, Dictionary<string, object>> dictParams)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>() { };
            foreach (var keyVa in dictParams.Keys)
            {
                keyValuePairs.Add(keyVa, dictParams[keyVa]);
            }
            return keyValuePairs;
        }
        public void IUpdateInvalidate()
        {
            if (pictureBox1 == null)
                return;
            pictureBox1.Invalidate();
        }
        public void IADDFromROI()
        {
            if (propertyGrid1 == null || funcID == "") return;
            try
            {
                string FuncTaskID = funcID;
                XParameterManager.Instance.ChangeFunctionDebug(FuncTaskID);
                pictureBox1.Invalidate();
                propertyGrid1.Refresh();
            }
            catch (Exception ex) {
                logW.Ins.Except($"[ROIS][0] IADDFromROI: \n {ex.ToString()}");
            }
            propertyGrid1.Refresh();
        }
        public void IDELFromROI()
        {
            if (propertyGrid1 == null || funcID == "") return;
            try
            {
                string FuncTaskID = funcID;
                XParameterManager.Instance.ChangeFunctionDebug(FuncTaskID);
            }
            catch (Exception ex)
            {
                logW.Ins.Except($"[ROIS][1] IDELFromROI: \n {ex.ToString()}");
            }

            propertyGrid1.Refresh();
        }

        public override void updateImg(string winname, OpenCvSharp.Mat mat)
        {
            if (!OBasicAlgorithm.IsMat(mat))
                return;
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
            InitializeScale();
            shapesIntance.LoadCoordinerImgToPic("ALL");
        }
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)

                {
                    // dispose cvX
                    cvX.UnRegisterActionImshow(funcID);
                    cvX.onUpdateFrameToUIAction = null;
                    // dispose shapes.
                    Shapes.Instance.shapes.Clear();
                    //XParameterManager.Instance.propertyGridData.PropertyChanged -= LoadCBBFuncIDAction;
                    shapesIntance.ShapesUpdated -= () => IUpdateInvalidate(); // Để vẽ lại khi có thay đổi
                    shapesIntance.ShapesAdded -= () => IADDFromROI(); // Để thêm selectROI
                    shapesIntance.shapesDeleted -= () => IADDFromROI(); // Để thêm selectROI
                    shapesIntance.pictureDrawing = null;
                    // dispose picturebox main events.
                    pictureBox1.MouseDoubleClick -= (sender, e) => shapesIntance.OnMouseDBClick(e.Location);
                    pictureBox1.MouseDown -= (sender, e) => shapesIntance.OnMouseDown(e.Location);
                    pictureBox1.MouseMove -= (sender, e) => shapesIntance.OnMouseMove(e.Location);
                    pictureBox1.Paint -= (sender, e) => shapesIntance.OnPaint(e.Graphics);
                    pictureBox1.MouseUp -= (sender, e) => shapesIntance.OnMouseUp();
                    pictureBox1.DragDrop -= (sender, e) => shapesIntance.DropArea_Drop(sender, e);
                    pictureBox1.DragEnter -= (sender, e) => shapesIntance.DropArea_DragEnter(sender, e);
                    btnDeleteROI.Click -= (sender, e) => shapesIntance.DeleteSelectedShape();
                    // dispose picturebox1 main.
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Dispose();
                    pictureBox1.Image = null;
                    pictureBox1 = null;
                    // Giải phóng tài nguyên hình ảnh của PictureBox nếu có
                    propertyGrid1.SelectedObject = null;
                    propertyGrid1.Dispose();
                    toolStrip2.Dispose();
                    toolStripSeparator1.Dispose();
                    propertyGrid1 = null;
                    toolStrip2 = null;
                    toolStripSeparator1 = null;

                    pictureBox1 = null;
                    roiSelectID = null;
                    // Giải phóng tài nguyên của các điều khiển hoặc tài nguyên quản lý khác

                    if (components != null) { components.Dispose(); }
                    // Lặp qua tất cả các controls trong form và dispose từng cái
                    foreach (Control ctrl in this.Controls) { ctrl.Dispose(); }
                }
                GC.Collect();  // Yêu cầu Garbage Collector thu hồi bộ nhớ ngay lập tức
                //GC.WaitForFullGCComplete();
                //this.FormClosed -= DialogSettingWidget_FormClosed;
            }
            catch (Exception ex)
            {
                logW.Ins.Except($"[ROIS][3] DISPOSE: \n {ex.ToString()}");
            }
            base.Dispose(disposing);
        } 

        private Rectangle GetImageRectangle(PictureBox pictureBox)
        {
            var propertyInfo = pictureBox.GetType().GetProperty(
                "ImageRectangle",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
            return (Rectangle)propertyInfo.GetValue(pictureBox, null);
        }
        public void InitializeScale()
        {
            try
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

                shapesIntance.pictureDrawingHeigh = img.Height;
                shapesIntance.pictureDrawingWidth = img.Width;
                shapesIntance.rectImageWidth = rectImg.Width;
                shapesIntance.rectImageHeigh = rectImg.Height;
                shapesIntance.rectImageX = rectImg.X;
                shapesIntance.rectImageY = rectImg.Y;
                shapesIntance.zoomScaleX = zoomScaleX;
                shapesIntance.zoomScaleY = zoomScaleY;
                shapesIntance.zoomScale = zoomScale;
                shapesIntance.WhiteoffsetX = WhiteoffsetX;
                shapesIntance.WhiteoffsetY = WhiteoffsetY;
                shapesIntance.displayWidthReal = displayWidthReal;
                shapesIntance.displayHeightReal = displayHeightReal;
            }
            catch (Exception ex)
            {
                logW.Ins.Except($"[ROIS][4] InitializeScale: \n {ex.ToString()}");
            }
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            if (XVisionManager.Instance.ImgUrlTest == "") { MessageBox.Show("CHUA CHON ANH TEST"); return; }
            XVisionManager.Instance.StartWorkInTaskDebug(taskID);

        }

        private void btnLoadConf_Click_1(object sender, EventArgs e)
        {
            InitializeScale();
            XParameterManager.Instance.LoadConfig();
            shapesIntance.LoadCoordinerImgToPic("ALL");
            propertyGrid1.Refresh();
        }

        private void btnSaveConf_Click_1(object sender, EventArgs e)
        {
            XParameterManager.Instance.SaveConfig();
            XParameterManager.Instance.LoadConfig();
            propertyGrid1.Refresh();
        }

        private void propertyGrid1_PropertyValueChanged_1(object s, PropertyValueChangedEventArgs e)
        {
            string parentKLey = e.ChangedItem.PropertyDescriptor.Name;
            if (parentKLey.Contains("ROI1") || parentKLey.Contains("ROI2"))
            {
                /////// NOTE: cần thêm check 
                string[] res_cv_func = parentKLey.Split(new string[] { "_1_" }, StringSplitOptions.None); if (res_cv_func[1].Contains("1")) ;
                string typeP = "BR";
                if (res_cv_func[1].Contains("1"))
                {
                    typeP = "TL";
                }
                shapesIntance.LoadCoordinerImgToPic(typeP);
                pictureBox1.Refresh();
            }
            propertyGrid1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (propertyGrid1 == null) return;
            shapesIntance.OnPaint(e.Graphics);
            var s = Shapes.Instance.shapes[funcID];
            propertyGrid1.Refresh();
        }

        private void DialogSettingWidgetROIs_Resize(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null) return;
            InitializeScale();
            shapesIntance.LoadCoordinerImgToPic();
            pictureBox1.Refresh();
        }
    }
}
