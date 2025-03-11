using OpenCvSharp;
using OVisionPro.Services.ImageProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Core.Tokens;
using OVisionPro.Services._01._Core.ShapesPics;

namespace OVisionPro
{
    public partial class DialogSettingWidgetToolTrainTemp : FormUpdatePicCore
    {
        public string roiSelectID = "";
        public int WhiteoffsetX = 0;
        public int WhiteoffsetY = 0;
        public int displayWidthReal = 0;
        public int displayHeightReal = 0;
        public float zoomScaleX = 1f;
        public float zoomScaleY = 1f;
        public float zoomScale = 1f;
        public string toolID = "";
        public string toolName = "";
        public string toolParent = "";
        public string imgPath = "";
        public int taskIDRun = 0;
        public int shapeLimit = 1;
        public int shapeStore = 1;
        public bool trainning = false;
        private PointF MouseCoordinate = new PointF(0, 0);
        public OpenCvSharp.Mat frameInput;
        public OpenCvSharp.Mat frameResult;
        public OpenCvSharp.Mat framePattern;
        public XObjectCVParamsBasicTool paramObj = new XObjectCVParamsBasicTool();
        public ShapeEditorBase shapeInstance = new ShapeEditorBase();
        NestedDictionaryWrapper wrapper;
        Dictionary<string, object> keyValuePairs;
        public DialogSettingWidgetToolTrainTemp()
        {
            InitializeComponent();
        }

        public bool ShowPatternImg()
        {
            if (XImgPatternManager.Instance.ImgMatPatternData.ContainsKey(toolID) && XImgPatternManager.Instance.ImgMatPatternData[toolID] != null && OBasicAlgorithm.IsMat(XImgPatternManager.Instance.ImgMatPatternData[toolID]))
            {
                framePattern?.Dispose();
                framePattern = XImgPatternManager.Instance.ImgMatPatternData[toolID];
                picBoxImgPattern.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(framePattern);
                return true;
            }
            return false;
        }

        public DialogSettingWidgetToolTrainTemp(int X, int Y, string ToolID = "", string ToolName = "", string ToolParent = "", string ImgPath = "", int TaskRun = 0)
        {
            StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
            InitializeScale();
            picImgDisplay = picBoxImgDisplay;
            toolID = ToolID;
            toolName = ToolName;
            toolParent = ToolParent;
            imgPath = ImgPath;
            taskIDRun = TaskRun;
            keyValuePairs = ConvertDictToNestDict(XParameterManager.Instance.ucBlockBasicControllers[toolID]);
            wrapper = new NestedDictionaryWrapper(keyValuePairs);
            paramObj.Parameters = wrapper;
            paramObj.posX = X;
            paramObj.posY = Y;
            paramObj.toolID = toolID;
            paramObj.toolName = toolName;
            paramObj.toolParent = toolParent;
            btnAdddodrag.blockID = toolParent;
            btnAdddodrag.shapeID = toolID;
            ShowPatternImg();
            propertyGrid1.SelectedObject = paramObj;
            shapeInstance.pictureDrawing = picBoxImgDisplay;
            shapeInstance.ShapesUpdated += () => IUpdateInvalidate(); // Để vẽ lại khi có thay đổi
            shapeInstance.ShapesAdded += () => IADDFromROI(); // Để thêm selectROI
            picBoxImgDisplay.MouseDown += (sender, e) => shapeInstance.OnMouseDown(e.Location);
            picBoxImgDisplay.MouseMove += (sender, e) => shapeInstance.OnMouseMove(e.Location);
            picBoxImgDisplay.MouseUp += (sender, e) => shapeInstance.OnMouseUp();
            picBoxImgDisplay.DragDrop += (sender, e) => shapeInstance.DropArea_Drop(sender, e);
            picBoxImgDisplay.DragEnter += (sender, e) => shapeInstance.DropArea_DragEnter(sender, e);
            picBoxImgDisplay.AllowDrop = true; // bataj cho phep drop
        }

        public void InitializeScale()
        {
            // Lấy ảnh gốc từ PictureBox
            Image img = picBoxImgDisplay.Image;
            if (img == null) return;
            Rectangle rectImg = GetImageRectangle(picBoxImgDisplay);
            // Kích thước của ảnh gốc
            int imgWidth = rectImg.Width;
            int imgHeight = rectImg.Height;
            //MessageBox.Show($"{imgWidth}-{imgHeight}      {img.Width}-{img.Height}");

            // Kích thước của PictureBox
            int pbWidth = picBoxImgDisplay.Width;
            int pbHeight = picBoxImgDisplay.Height;

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



            shapeInstance.pictureDrawingHeigh = img.Height;
            shapeInstance.pictureDrawingWidth = img.Width;
            shapeInstance.rectImageWidth = rectImg.Width;
            shapeInstance.rectImageHeigh = rectImg.Height;
            shapeInstance.rectImageX = rectImg.X;
            shapeInstance.rectImageY = rectImg.Y;
            shapeInstance.zoomScaleX = zoomScaleX;
            shapeInstance.zoomScaleY = zoomScaleY;
            shapeInstance.zoomScale = zoomScale;
            shapeInstance.WhiteoffsetX = WhiteoffsetX;
            shapeInstance.WhiteoffsetY = WhiteoffsetY;
            shapeInstance.displayWidthReal = displayWidthReal;
            shapeInstance.displayHeightReal = displayHeightReal;
        }
        public void IUpdateInvalidate()
        {
            if (picBoxImgDisplay == null)
                return;
            picBoxImgDisplay.Invalidate();
        }
        public void IADDFromROI()
        {
            propertyGrid1.Refresh();
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
        private Rectangle GetImageRectangle(PictureBox pictureBox)
        {
            var propertyInfo = pictureBox.GetType().GetProperty(
                "ImageRectangle",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
            return (Rectangle)propertyInfo.GetValue(pictureBox, null);
        }

        public override void updateImg(string winname, OpenCvSharp.Mat mat)
        {
            if (!OBasicAlgorithm.IsMat(mat))
                return;
            if (winname.Contains("ORG"))
            {
                frameInput?.Dispose();
                frameInput = mat.Clone();
                picBoxImgDisplay.Image?.Dispose();
                picBoxImgDisplay.Image = null;
                picBoxImgDisplay.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frameInput);
            }
            else
            {
                frameResult?.Dispose();
                frameResult = mat.Clone();
                picBoxImgDisplay.Image?.Dispose();
                picBoxImgDisplay.Image = null;
                picBoxImgDisplay.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frameResult);
            }
            InitializeScale();
        }
        public void DropArea_DragEnter(object sender, DragEventArgs e)
        {
            // Kiểm tra nếu dữ liệu là Dictionary
            if (e.Data.GetDataPresent("Dictionary"))
            {
                e.Effect = DragDropEffects.Copy;  // Cho phép thả nếu đúng loại dữ liệu
            }
            else
            {
                e.Effect = DragDropEffects.None;  // Không cho phép thả nếu không đúng loại
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (imgPath == "")
            {
                logW.Ins.warning($"[TRAIN][0] NO MODEL/ CHUA CHỌN MODEL");
                return;
            }
            btnShowImGTrain.Text = "TRAIN";
            btnShowImGTrain.BackColor = Color.Silver;
            if (OBasicAlgorithm.IsMat(frameResult))
            {
                picBoxImgDisplay.Image?.Dispose();
                picBoxImgDisplay.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frameResult);
            }
            XVisionManager.Instance.StartWorkInTaskDebug(taskIDRun);
        }

        private void btnLoadConf_Click(object sender, EventArgs e)
        {
            XParameterManager.Instance.LoadConfig();
            propertyGrid1.Refresh();
        }

        private void btnSaveConf_Click(object sender, EventArgs e)
        {
            XParameterManager.Instance.SaveConfig();
            XParameterManager.Instance.LoadConfig();
            propertyGrid1.Refresh();
        }

        private void DialogSettingWidgetToolTrainTemp_Paint(object sender, PaintEventArgs e)
        {
        }

        private void picBoxImgDisplay_Paint(object sender, PaintEventArgs e)
        {
            if (propertyGrid1 == null) return;
            shapeInstance.OnPaint(e.Graphics);
            propertyGrid1.Refresh();
        }

        private void DialogSettingWidgetToolTrainTemp_FormClosed(object sender, FormClosedEventArgs e)
        {

            propertyGrid1.SelectedObject = null;
            keyValuePairs = null;
            wrapper = null;
            shapeInstance.ShapesUpdated -= () => IUpdateInvalidate(); // Để vẽ lại khi có thay đổi
            shapeInstance.ShapesAdded -= () => IADDFromROI(); // Để thêm selectROI

            frameInput?.Dispose();
            frameResult?.Dispose();

            picBoxImgDisplay.Image?.Dispose();
            picBoxImgPattern.Image?.Dispose();
            picBoxImgDisplay.Image = null;
            picBoxImgPattern.Image = null;
            picBoxImgDisplay = picBoxImgPattern = null;

            frameInput = frameResult = null;
            cvX.UnRegisterActionImshow(toolID);
        }

        private void btnTrainImg_Click(object sender, EventArgs e)
        {
            try
            {
                InitializeScale();
                if (shapeInstance.shapeActive == null) return;
                PointF TL = shapeInstance.ConvertPicImageToOrgImage(shapeInstance.ConvertPicBoxToPicImage(shapeInstance.shapeActive.TLPoint));
                PointF BR = shapeInstance.ConvertPicImageToOrgImage(shapeInstance.ConvertPicBoxToPicImage(shapeInstance.shapeActive.BRPoint));
                int mRoix1 = (int)TL.X;
                int mRoiy1 = (int)TL.Y;
                int mRoix2 = (int)BR.X;
                int mRoiy2 = (int)BR.Y;
                OpenCvSharp.Scalar curColor = OpenCvSharp.Scalar.Red;

                if (!OBasicAlgorithm.IsMat(frameInput)) return;
                Rect roi = new Rect(mRoix1, mRoiy1, mRoix2 - mRoix1, mRoiy2 - mRoiy1);
                Mat ROI = frameInput.Clone(roi);
                if (OBasicAlgorithm.IsMat(ROI))
                {
                    framePattern?.Dispose();
                    framePattern = ROI.Clone();
                    ROI.Dispose();
                    //Cv2.ImWrite("pattern.png", frameInput);
                    //Cv2.ImWrite("sourceImg.png", frameInput);
                    picBoxImgPattern.Image?.Dispose();
                    picBoxImgPattern.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(framePattern);
                    XImgPatternManager.Instance.ImgMatPatternData[toolID] = framePattern;
                    XImgPatternManager.Instance.ImgPatternSerialization();
                }
            }
            catch (Exception ex)
            {
                logW.Ins.Except($"[TRAIN][1] btnTrainImg_Click :{ex.ToString()}");
            }
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            propertyGrid1.Refresh();
        }

        private void btnShowImGTrain_Click(object sender, EventArgs e)
        {
            trainning = !trainning;
            if (trainning)
            {
                btnShowImGTrain.Text = "TRAINING";
                btnShowImGTrain.BackColor = Color.AliceBlue;
                picBoxImgDisplay.Image?.Dispose();
                if (OBasicAlgorithm.IsMat(frameInput))
                {
                    picBoxImgDisplay.Image?.Dispose();
                    picBoxImgDisplay.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frameInput);
                }
                else
                {
                    logW.Ins.warning($"[TRAIN][2] KHONG CO ANH DAU VAO. ");
                }
            }
            else
            {
                btnShowImGTrain.Text = "TRAIN";
                btnShowImGTrain.BackColor = Color.Silver;
                if (OBasicAlgorithm.IsMat(frameResult))
                {
                    picBoxImgDisplay.Image?.Dispose();
                    picBoxImgDisplay.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frameResult);
                }
                else
                {
                    logW.Ins.warning($"[TRAIN][2] KHONG CO ANH KET QUA.");
                }
            }
        }
    }
}
