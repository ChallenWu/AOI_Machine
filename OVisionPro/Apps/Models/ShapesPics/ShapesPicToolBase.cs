using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro.Services._01._Core.ShapesPics
{
    public class ShapeEditorBase
    {
        public PictureBox pictureDrawing;
        public DragDropTextBox txtPathImgControl;
        private Shape selectedShape = null;
        private string selectedGroupKey = null;

        private bool isDraggingCorner = false;
        private bool isDraggingShape = false;
        private string draggingCorner = null;
        private PointF previousMousePosition;

        private PointF MouseCoordinate = new PointF(0, 0);
        // zoom info
        public int WhiteoffsetX = 0;
        public int WhiteoffsetY = 0;
        public int displayWidthReal = 0;
        public int displayHeightReal = 0;
        public float zoomScaleX = 1f;
        public float zoomScaleY = 1f;
        public float zoomScale = 1f;
        public float pictureDrawingWidth = 1f;
        public float pictureDrawingHeigh = 1f;
        // image có độ sai lêhcj so với bitmap và picturebx 
        public float rectImageWidth = 1f;
        public float rectImageHeigh = 1f;
        public float rectImageX = 1f;
        public float rectImageY = 1f;
        public string funcDraw = "";
        public int selectedTaskIDRun = 0;
        public string usingGroupKey = "";

        public Dictionary<string, Shape> shapes = new Dictionary<string, Shape>();
        public Shape shapeActive;

        public event Action ShapesUpdated; // draw again shapes.
        public event Action ShapesAdded;
        public event Action shapesDeleted;
        public PointF ConvertOrgImageToPicImage(PointF orgPoint)
        {
            // chuyển tọa đôj ảnh ban đầu sang toaj độ ảnh sau khi resize ( Note: cả 2 đều là tọa độ của picture.Image)
            // Lay toa do cua image ban dau
            float scaleX = (float)rectImageWidth / pictureDrawingWidth;
            float scaleY = (float)rectImageHeigh / pictureDrawingHeigh;
            float scale = Math.Min(scaleX, scaleY);
            PointF s02 = new PointF(
                (float)(scale * (orgPoint.X + WhiteoffsetX - rectImageX)),
                (float)(scale * (orgPoint.Y + WhiteoffsetY - rectImageY))
            );
            return s02;
        }
        public PointF ConvertPicImageToOrgImage(PointF orgPoint)
        {
            /// chuyển tọa đôj ảnh ban đầu sang toaj độ ảnh sau khi resize ( Note: cả 2 đều là tọa độ của picture.Image)
            /// Vi thong thuong anh Img sau khi resize se nho hon Image truoc do.
            /// Lay toa do cua image ban dau
            float scaleX = (float)rectImageWidth / pictureDrawingWidth;
            float scaleY = (float)rectImageHeigh / pictureDrawingHeigh;
            float scale = Math.Min(scaleX, scaleY);
            PointF s01 = new PointF(
                (float)(orgPoint.X / scale + rectImageX - WhiteoffsetX),
                (float)(orgPoint.Y / scale + rectImageY - WhiteoffsetY)
            );
            return s01;
        }
        public PointF ConvertPicImageToPicBox(PointF picImagePoint)
        {

            float imgX = (float)(picImagePoint.X * zoomScale + WhiteoffsetX);
            float imgY = (float)(picImagePoint.Y * zoomScale + WhiteoffsetY);
            PointF p1 = new PointF(imgX, imgY);
            return p1;
        }
        public PointF ConvertPicBoxToPicImage(PointF picImagePoint)
        {
            float imgX = (float)((picImagePoint.X - WhiteoffsetX) / zoomScale);
            float imgY = (float)((picImagePoint.Y - WhiteoffsetY) / zoomScale);
            PointF p1 = new PointF(imgX, imgY);
            return p1;
        }
        public void LoadCoordinerImgToPic(string pointName = "ALL")
        {
            foreach (string key in Shapes.Instance.shapes.Keys)
            {
                Dictionary<string, Shape> group = Shapes.Instance.shapes[key];
                foreach (string key1 in group.Keys)
                {
                    float x1 = (float)XParameterManager.Instance.GetUcBlockBasicData(key, key1, "x1", 0);
                    float y1 = (float)XParameterManager.Instance.GetUcBlockBasicData(key, key1, "y1", 0);
                    float x2 = (float)XParameterManager.Instance.GetUcBlockBasicData(key, key1, "x2", 1);
                    float y2 = (float)XParameterManager.Instance.GetUcBlockBasicData(key, key1, "y2", 1);
                    string blockName = XParameterManager.Instance.GetUcBlockBasicData(key, key1, "BlockName", "NameDef");
                    string blockParent = XParameterManager.Instance.GetUcBlockBasicData(key, key1, "BlockParentKey", "ParentDef");
                    Shapes.Instance.shapes[key][key1].shapeName = blockName;
                    Shapes.Instance.shapes[key][key1].shapeParent = blockParent;
                    if (pointName == "TL")
                    {
                        PointF TLPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x1, y1)));
                        Shapes.Instance.shapes[key][key1].TLPoint = TLPoint;
                    }
                    else if (pointName == "BR")
                    {
                        PointF BRPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x2, y2)));
                        Shapes.Instance.shapes[key][key1].BRPoint = BRPoint;
                    }
                    else
                    {
                        PointF TLPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x1, y1)));
                        Shapes.Instance.shapes[key][key1].TLPoint = TLPoint;
                        PointF BRPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x2, y2)));
                        Shapes.Instance.shapes[key][key1].BRPoint = BRPoint;
                    }
                }
            }
            //this.ShapesUpdated?.Invoke();
        }
        public void AddNewShape(string shapeId, string shapeParent)
        {
            try
            {
                PointF TLPoint = MouseCoordinate;
                PointF BRPoint = MouseCoordinate;
                PointF TRPoint = new PointF(TLPoint.X, BRPoint.Y);
                PointF BLPoint = new PointF(BRPoint.X, TLPoint.Y);
                if (shapeActive == null) shapeActive = new Shape();
                shapeActive.shapeName = "Shape Name";
                shapeActive.shapeId = shapeId;
                shapeActive.shapeParent = shapeParent;
                shapeActive.TLPoint = TLPoint;
                shapeActive.BRPoint = BRPoint;
                shapeActive.TRPoint = TRPoint;
                shapeActive.BLPoint = BLPoint;
                shapeActive.IsSelected = false;
                shapes[shapeId] = shapeActive;
                this.ShapesUpdated?.Invoke();
            }
            catch (TargetInvocationException ex)
            {
                Log.Information("Lỗi: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Log.Information("Lỗi bên trong: " + ex.InnerException.Message);
                }
            }
        }
        public void OnPaint(Graphics g)
        {
            try
            {
                if (pictureDrawing == null || pictureDrawing.Image == null || g == null || shapeActive == null)
                {
                    return;
                }
                PointF TL = shapeActive.TLPoint;
                PointF BR = shapeActive.BRPoint;
                PointF TR = new PointF(shapeActive.BRPoint.X, shapeActive.TLPoint.Y);
                PointF BL = new PointF(shapeActive.TLPoint.X, shapeActive.BRPoint.Y);
                float xMax = Math.Max(TL.X, BR.X);
                float yMax = Math.Max(TL.Y, BR.Y);
                float xMin = Math.Min(TL.X, BR.X);
                float yMin = Math.Min(TL.Y, BR.Y);
                if (xMax == xMin || yMax == yMin) { }
                else
                {
                    TL = new PointF(xMin, yMin);
                    TR = new PointF(xMax, yMin);
                    BR = new PointF(xMax, yMax);
                    BL = new PointF(xMin, yMax);
                }

                shapeActive.TLPoint = TL;
                shapeActive.TRPoint = TR;
                shapeActive.BRPoint = BR;
                shapeActive.BLPoint = BL;
                Pen pen = shapeActive.IsSelected
                    ? new Pen(Color.Red, 2f) { DashStyle = DashStyle.Dash }
                    : new Pen(Color.Blue, 2f) { DashStyle = DashStyle.Dash };

                g.DrawLine(pen, TL, BR);
                g.DrawLine(pen, TR, BL);
                g.DrawLine(pen, TL, TR);
                g.DrawLine(pen, TR, BR);
                g.DrawLine(pen, BR, BL);
                g.DrawLine(pen, BL, TL);

                DrawCorner(g, Brushes.Green, new Point((int)(shapeActive.TLPoint.X), (int)(shapeActive.TLPoint.Y)));
                DrawCorner(g, Brushes.Green, new Point((int)(shapeActive.BRPoint.X), (int)(shapeActive.BRPoint.Y)));
                DrawCorner(g, Brushes.Green, new Point((int)(TR.X), (int)(TR.Y)));
                DrawCorner(g, Brushes.Green, new Point((int)(BL.X), (int)(BL.Y)));
                DrawTextAtTopLeft(g, TL, $"{(int)TL.X}X{(int)TL.Y} Patten");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void DrawTextAtTopLeft(Graphics g, PointF topLeft, string text)
        {
            try
            {
                Font font = new Font("Arial", 10f);
                Brush brush = Brushes.Red;
                SizeF textSize = g.MeasureString(text, font);
                g.DrawString(text, font, brush, topLeft.X + 5, (float)topLeft.Y - textSize.Height - 5f);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error drawing text: " + ex.Message);
            }
        }
        private void DrawCorner(Graphics g, Brush brush, Point corner)
        {
            try
            {
                if (pictureDrawing.Image != null && pictureDrawing != null)
                {
                    g.FillEllipse(brush, corner.X - 10, corner.Y - 10, 20, 20);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show($"[DrawCorner] ex: {x}");
            }
        }
        public void OnMouseDown(PointF mouseLocation)
        {
            try
            {
                if (shapes.ContainsKey(funcDraw)) return;
                // Nếu không chọn hình nào, bỏ chọn toàn bộ
                foreach (var shape in shapes.Values)
                {
                    shape.IsSelected = false;
                }
                var groupKey = funcDraw;

                selectedShape = shapes.Values.FirstOrDefault(shape =>
                    IsPointInCorner(mouseLocation, shape.TLPoint) ||
                    IsPointInCorner(mouseLocation, shape.BRPoint) ||
                    IsPointInCorner(mouseLocation, shape.TRPoint) ||
                    IsPointInCorner(mouseLocation, shape.BLPoint) ||
                    IsPointInShape(mouseLocation, shape));

                if (selectedShape != null)
                {
                    selectedGroupKey = groupKey;
                    selectedShape.IsSelected = true;

                    if (IsPointInCorner(mouseLocation, selectedShape.TLPoint)) draggingCorner = "TL";
                    if (IsPointInCorner(mouseLocation, selectedShape.BRPoint)) draggingCorner = "BR";
                    if (IsPointInCorner(mouseLocation, selectedShape.TRPoint)) draggingCorner = "TR";
                    if (IsPointInCorner(mouseLocation, selectedShape.BLPoint)) draggingCorner = "BL";

                    if (draggingCorner != null)
                    {
                        this.ShapesUpdated?.Invoke();
                        isDraggingCorner = true;
                        return;
                    }

                    isDraggingShape = true;
                    previousMousePosition = mouseLocation;
                    this.ShapesUpdated?.Invoke();
                    return;
                }
                this.ShapesUpdated?.Invoke();

            }
            catch (Exception x)
            {
                MessageBox.Show($"[onmouseDown] ex: {x}");
            }
        }
        public void OnMouseMove(PointF mouseLocation)
        {
            try
            {
                if (selectedShape == null)
                {
                    return;
                }

                if (isDraggingCorner)
                {
                    switch (draggingCorner)
                    {
                        case "TL":
                            selectedShape.TLPoint = mouseLocation;
                            break;
                        case "BR":
                            selectedShape.BRPoint = mouseLocation;
                            break;
                        case "TR":
                            selectedShape.BRPoint = new PointF(mouseLocation.X, selectedShape.BRPoint.Y);
                            selectedShape.TLPoint = new PointF(selectedShape.TLPoint.X, mouseLocation.Y);
                            break;
                        case "BL":
                            selectedShape.TLPoint = new PointF(mouseLocation.X, selectedShape.TLPoint.Y);
                            selectedShape.BRPoint = new PointF(selectedShape.BRPoint.X, mouseLocation.Y);
                            break;
                    }

                    this.ShapesUpdated?.Invoke();
                }
                else if (isDraggingShape)
                {
                    float dx = mouseLocation.X - previousMousePosition.X;
                    float dy = mouseLocation.Y - previousMousePosition.Y;

                    selectedShape.TLPoint = new PointF(selectedShape.TLPoint.X + dx, selectedShape.TLPoint.Y + dy);
                    selectedShape.BRPoint = new PointF(selectedShape.BRPoint.X + dx, selectedShape.BRPoint.Y + dy);

                    previousMousePosition = mouseLocation;
                    this.ShapesUpdated?.Invoke();
                }
            }
            catch (Exception x)
            {
                MessageBox.Show($"[onmousemove] ex: {x}");
            }
        }
        public void OnMouseUp()
        {
            isDraggingCorner = false;
            isDraggingShape = false;
            draggingCorner = null;
        }
        private bool IsPointInCorner(PointF p, PointF corner)
        {
            return Math.Abs(p.X - corner.X) <= 10 && Math.Abs(p.Y - corner.Y) <= 10;
        }
        private bool IsPointInShape(PointF p, Shape shape)
        {
            float minX = Math.Min(shape.TLPoint.X, shape.BRPoint.X);
            float maxX = Math.Max(shape.TLPoint.X, shape.BRPoint.X);
            float minY = Math.Min(shape.TLPoint.Y, shape.BRPoint.Y);
            float maxY = Math.Max(shape.TLPoint.Y, shape.BRPoint.Y);

            return p.X >= minX && p.X <= maxX && p.Y >= minY && p.Y <= maxY;
        }
        public void DropArea_DragEnter(object sender, DragEventArgs e)
        {
            // Kiểm tra nếu dữ liệu là Dictionary
            if (e.Data.GetDataPresent("tempDrag"))
            {
                e.Effect = DragDropEffects.Copy;  // Cho phép thả nếu đúng loại dữ liệu
            }
            else
            {
                e.Effect = DragDropEffects.None;  // Không cho phép thả nếu không đúng loại
            }
        }
        public void DropArea_Drop(object sender, DragEventArgs e)
        {
            try
            {
                // Kiểm tra nếu dữ liệu là Dictionary
                if (e.Data.GetDataPresent("tempDrag"))
                {
                    string dataV = e.Data.GetData("tempDrag").ToString();
                    string[] conntent = dataV.Split(new string[] { "|||" }, StringSplitOptions.None);
                    MouseCoordinate = pictureDrawing.PointToClient(new Point(e.X, e.Y));
                    AddNewShape(conntent[0], "");
                }
                else
                {
                    Log.Information("No valid data found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DropArea_Drop :", ex.ToString());
            }
        }
    }

}
