using OVisionPro.Services.ImageProcessing;
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
using OVisionPro.Services._01._Core.ShapesPics;

namespace OVisionPro
{
    public partial class DialogSettingWidgetToolBase : FormUpdatePicCore
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

        public XObjectCVParamsBasicTool paramObj = new XObjectCVParamsBasicTool();

        public DialogSettingWidgetToolBase()
        {
            InitializeComponent();
        }


        public override void updateImg(string winname, OpenCvSharp.Mat mat)
        {
            if (!OBasicAlgorithm.IsMat(mat))
                return;
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = null;
            pictureBox1.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
            InitializeScale();
        }
        public DialogSettingWidgetToolBase(int X, int Y, string ToolID="", string ToolName = "", string ToolParent = "", string ImgPath="", int TaskRun=0)
        {
            StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
            InitializeScale();
            picImgDisplay = pictureBox1;
            toolID = ToolID;
            toolName = ToolName;
            toolParent = ToolParent;
            imgPath = ImgPath;
            taskIDRun = TaskRun;
            Dictionary<string, object> keyValuePairs = ConvertDictToNestDict(XParameterManager.Instance.ucBlockBasicControllers[toolID]);
            NestedDictionaryWrapper wrapper = new NestedDictionaryWrapper(keyValuePairs);
            paramObj.Parameters = wrapper;
            paramObj.posX = X;
            paramObj.posY = Y;
            paramObj.toolID = toolID;
            paramObj.toolName = toolName;
            paramObj.toolParent = toolParent;
            propertyGrid1.SelectedObject = paramObj;
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
        }

        private void DialogSettingWidgetToolBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            cvX.UnRegisterActionImshow(toolID);
            this.Dispose();
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
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
                //ShapeEditor.Instance.LoadCoordinerImgToPic(typeP);
                pictureBox1.Refresh();
            }
            propertyGrid1.Refresh();
        }

        private void btnSaveConf_Click(object sender, EventArgs e)
        {
            XParameterManager.Instance.SaveConfig();
            XParameterManager.Instance.LoadConfig();
            propertyGrid1.Refresh();
        }

        private void btnLoadConf_Click(object sender, EventArgs e)
        {
            XParameterManager.Instance.LoadConfig();
            propertyGrid1.Refresh();

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (imgPath == "")  {  MessageBox.Show("CHUA CHON ANH TEST");  return; }
            XVisionManager.Instance.StartWorkInTaskDebug(taskIDRun);
        }

        private void btnCreateROI_Click(object sender, EventArgs e)
        {

        }
    }
}
