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
using YamlDotNet.Core.Tokens;

namespace OVisionPro.Services._01._Core.ShapesPics
{
    public class ShapesBase
    {
        // Dictionary cấp 2: GroupKey -> (ShapeID -> Shape)
        public Dictionary<string, Dictionary<string, Shape>> shapes = new Dictionary<string, Dictionary<string, Shape>>();
        public Action<string, string> OnActions;
        public ShapesBase() {
        }

        private static readonly ShapesBase instance = new ShapesBase();

        public static ShapesBase Instance => instance;

    }

    public class ShapesPicROIs
    {
        public ShapesPicROIs()
        {
            XParameterManager.Instance.onAddNewShapeROI += AddNewShape;
        }
        public ShapesPicROIs(string functionID) 
        {
            XParameterManager.Instance.onAddNewShapeROI += AddNewShape;
        }

        ~ShapesPicROIs()
        {
            XParameterManager.Instance.onAddNewShapeROI -= AddNewShape;
        }
        public bool mouseUpFlag = false;
        public bool mouseDownFlag = true;
        public PictureBox pictureDrawing;
        public string txtPathImgControl;
        public Shape selectedShape = null;
        public string selectedGroupKey = null;

        public bool isDraggingCorner = false;
        public bool isDraggingShape = false;
        public string draggingCorner = null;
        public PointF previousMousePosition;
        public PointF MouseCoordinate = new PointF(0, 0);

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
        public int selectedTaskIDRun = 0;
        public string funcDraw = "";
        public string usingGroupKey = "";

        //private static readonly ShapeEditor instance = new ShapeEditor();

        //public static ShapeEditor Instance => instance;

        public event Action ShapesUpdated; // draw again shapes.
        public event Action ShapesAdded;
        public event Action shapesDeleted;

        public Rectangle GetImageRectangle(PictureBox pictureBox)
        {
            var propertyInfo = pictureBox.GetType().GetProperty(
                "ImageRectangle",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
            return (Rectangle)propertyInfo.GetValue(pictureBox, null);
        }
        public Point ConvertImgResizeToOrgCoordinate(Point originalPoint)
        {
            float scaleX = (float)rectImageWidth / pictureDrawingWidth;
            float scaleY = (float)rectImageHeigh / pictureDrawingHeigh;

            return new Point(
                (int)(originalPoint.X * scaleX + rectImageX),
                (int)(originalPoint.Y * scaleY + rectImageY)
            );
        }
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
        public void LoadCoordinerImgToPicPer(string block, string cvid)
        {
            float x1 = (float)XParameterManager.Instance.GetUcBlockBasicData(block, cvid, "x1", 0);
            float y1 = (float)XParameterManager.Instance.GetUcBlockBasicData(block, cvid, "y1", 0);
            float x2 = (float)XParameterManager.Instance.GetUcBlockBasicData(block, cvid, "x2", 1);
            float y2 = (float)XParameterManager.Instance.GetUcBlockBasicData(block, cvid, "y2", 1);
            string blockName = XParameterManager.Instance.GetUcBlockBasicData(block, cvid, "BlockName", "NameDef");
            string blockParent = XParameterManager.Instance.GetUcBlockBasicData(block, cvid, "BlockParentKey", "ParentDef");
            Shapes.Instance.shapes[block][cvid].shapeName = blockName;
            Shapes.Instance.shapes[block][cvid].shapeParent = blockParent;
            PointF TLPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x1, y1)));
            Shapes.Instance.shapes[block][cvid].TLPoint = TLPoint;
            PointF BRPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x2, y2)));
            Shapes.Instance.shapes[block][cvid].BRPoint = BRPoint;
            this.ShapesUpdated?.Invoke();
        }
        public void LoadCoordinerPicToImage(string groupKey, string shapeID)
        {
            Shape shape = Shapes.Instance.shapes[groupKey][shapeID];
            PointF TL = ConvertPicImageToOrgImage(ConvertPicBoxToPicImage(shape.TLPoint));
            PointF BR = ConvertPicImageToOrgImage(ConvertPicBoxToPicImage(shape.BRPoint));
            XParameterManager.Instance.SetUcBlockBasicData(groupKey, shapeID, "x2", (int)(BR.X));
            XParameterManager.Instance.SetUcBlockBasicData(groupKey, shapeID, "y2", (int)(BR.Y));
            XParameterManager.Instance.SetUcBlockBasicData(groupKey, shapeID, "x1", (int)(TL.X));
            XParameterManager.Instance.SetUcBlockBasicData(groupKey, shapeID, "y1", (int)(TL.Y));
            if (shapeID.Contains("ROI2"))
            {
                float xT = (float)XParameterManager.Instance.GetUcBlockBasicData(groupKey, shapeID, "xT", 1);
                float yT = (float)XParameterManager.Instance.GetUcBlockBasicData(groupKey, shapeID, "yT", 1);
                XParameterManager.Instance.SetUcBlockBasicData(groupKey, shapeID, "w1", (int)(xT - TL.X));
                XParameterManager.Instance.SetUcBlockBasicData(groupKey, shapeID, "h1", (int)(yT - TL.Y));
                XParameterManager.Instance.SetUcBlockBasicData(groupKey, shapeID, "w2", (int)(BR.X - xT));
                XParameterManager.Instance.SetUcBlockBasicData(groupKey, shapeID, "h2", (int)(BR.Y - yT));
            }
            //XParameterManager.Instance.SetUcBlockBasicData(groupKey, shapeID, "BlockID", "");
        }
        public void AddNewShape(string groupKey, string shapeId)
        {
            try
            {
                if (groupKey != usingGroupKey) return;
                if (!Shapes.Instance.shapes.ContainsKey(groupKey))
                {
                    Shapes.Instance.shapes[groupKey] = new Dictionary<string, Shape>();
                }
                if (!Shapes.Instance.shapes.ContainsKey(groupKey))
                {
                    Shapes.Instance.shapes[groupKey] = new Dictionary<string, Shape>();
                }
                PointF TLPoint = MouseCoordinate;
                PointF BRPoint = MouseCoordinate;
                PointF TRPoint = new PointF(TLPoint.X, BRPoint.Y);
                PointF BLPoint = new PointF(BRPoint.X, TLPoint.Y);

                Shape newShape = new Shape
                {
                    shapeName = "Shape Name",
                    shapeId = shapeId,
                    shapeParent = groupKey,
                    TLPoint = TLPoint,
                    BRPoint = BRPoint,
                    TRPoint = TRPoint,
                    BLPoint = BLPoint,
                    IsSelected = false
                };

                Shapes.Instance.shapes[groupKey][shapeId] = newShape;
                LoadCoordinerPicToImage(groupKey, shapeId);
                LoadCoordinerImgToPicPer(groupKey, shapeId);
                //LoadCoordinerImgToPic("ALL");
                this.ShapesAdded?.Invoke();
            }
            catch (TargetInvocationException ex)
            {
                Log.Information("AddNewShape ex:" + ex.Message);
                if (ex.InnerException != null)
                {
                    Log.Information("AddNewShape ex:" + ex.InnerException.Message);
                }
            }
        }
        public void LoadToShapesFromConfig()
        {
            try
            {
                if (pictureDrawing == null) return;
                Shapes.Instance.shapes.Clear();
                foreach (string key in XParameterManager.Instance.ucBlockBasicControllers.Keys)
                {
                    if (!Shapes.Instance.shapes.Keys.Contains(key)) Shapes.Instance.shapes[key] = new Dictionary<string, Shape>();
                    foreach (string key1 in XParameterManager.Instance.ucBlockBasicControllers[key].Keys)
                    {
                        float x1 = (float)XParameterManager.Instance.GetUcBlockBasicData(key, key1, "x1", 0);
                        float y1 = (float)XParameterManager.Instance.GetUcBlockBasicData(key, key1, "y1", 0);
                        float x2 = (float)XParameterManager.Instance.GetUcBlockBasicData(key, key1, "x2", 1);
                        float y2 = (float)XParameterManager.Instance.GetUcBlockBasicData(key, key1, "y2", 1);
                        string blockName = XParameterManager.Instance.GetUcBlockBasicData(key, key1, "BlockName", "NameDef");
                        string blockParent = XParameterManager.Instance.GetUcBlockBasicData(key, key1, "BlockParentKey", "ParentDef");
                        if (key1.Contains("ROI1"))
                        {
                            if (!Shapes.Instance.shapes[key].Keys.Contains(key1)) Shapes.Instance.shapes[key][key1] = new Shape();
                            PointF TLPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x1, y1)));
                            Shapes.Instance.shapes[key][key1].TLPoint = TLPoint;
                            PointF BRPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x2, y2)));
                            Shapes.Instance.shapes[key][key1].BRPoint = BRPoint;
                            Shapes.Instance.shapes[key][key1].shapeParent = blockParent;
                            Shapes.Instance.shapes[key][key1].shapeName = blockName;
                            Shapes.Instance.shapes[key][key1].shapeId = key1;
                        }
                        if (key1.Contains("ROI2"))
                        {
                            if (!Shapes.Instance.shapes[key].Keys.Contains(key1)) Shapes.Instance.shapes[key][key1] = new Shape();
                            PointF TLPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x1, y1)));
                            PointF BRPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x2, y2)));
                            Shapes.Instance.shapes[key][key1].TLPoint = TLPoint;
                            Shapes.Instance.shapes[key][key1].BRPoint = BRPoint;
                            Shapes.Instance.shapes[key][key1].shapeParent = blockParent;
                            Shapes.Instance.shapes[key][key1].shapeName = blockName;
                            Shapes.Instance.shapes[key][key1].shapeId = key1;
                        }
                    }
                }
                LoadCoordinerImgToPic("ALL");
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
        public void AddNewShapeROI2(string groupKey, string shapeId)
        {
            try
            {
                if (!Shapes.Instance.shapes.ContainsKey(groupKey))
                {
                    Shapes.Instance.shapes[groupKey] = new Dictionary<string, Shape>();
                }
                if (!Shapes.Instance.shapes.ContainsKey(groupKey))
                {
                    Shapes.Instance.shapes[groupKey] = new Dictionary<string, Shape>();
                }
                PointF TLPoint = MouseCoordinate;
                PointF BRPoint = new PointF(MouseCoordinate.X + 20, MouseCoordinate.Y + 20);
                PointF TRPoint = new PointF(TLPoint.X, BRPoint.Y);
                PointF BLPoint = new PointF(BRPoint.X, TLPoint.Y);

                Shape newShape = new Shape
                {
                    shapeName = "Shape Name",
                    shapeId = shapeId,
                    shapeParent = groupKey,
                    TLPoint = TLPoint,
                    BRPoint = BRPoint,
                    TRPoint = TRPoint,
                    BLPoint = BLPoint,
                    IsSelected = false
                };

                Shapes.Instance.shapes[groupKey][shapeId] = newShape;
                LoadCoordinerImgToPic("ALL");
                //this.ShapesAdded?.Invoke();
                this.ShapesUpdated?.Invoke();
            }
            catch (TargetInvocationException ex)
            {
                Log.Information("Lỗi: " + ex.Message);
                if (ex.InnerException != null)
                {
                    logW.Ins.info($"[SHAPE][2] LOI BEN TRONG: \n {ex.ToString()}");
                }
            }
        }
        public void AddNewShape()
        {
            try
            {
                if (!Shapes.Instance.shapes.ContainsKey(usingGroupKey))
                {
                    Shapes.Instance.shapes[usingGroupKey] = new Dictionary<string, Shape>();
                }
                if (!Shapes.Instance.shapes.ContainsKey(usingGroupKey))
                {
                    Shapes.Instance.shapes[usingGroupKey] = new Dictionary<string, Shape>();
                }
                cvX.ROI1(usingGroupKey, Guid.NewGuid().ToString());
            }
            catch (Exception e)
            {
                logW.Ins.Except($"[SHAPE][1] ADD: \n {e.ToString()}");
            }
        }
        public void AddNewShape(int X, int Y)
        {
            try
            {
                if (!Shapes.Instance.shapes.ContainsKey(usingGroupKey))
                {
                    Shapes.Instance.shapes[usingGroupKey] = new Dictionary<string, Shape>();
                }
                //var pointDetail = XParameterManager.Instance.ucBlockBasicControllers[groupKey][shapeID];
                //foreach (var key in pointDetail.Keys) { 
                //    var x1 = pointDetail[key];
                //}

                //Point TLPoint = new Point(x1, y1);
                //Point BRPoint = new Point(x2, y2);
                if (!Shapes.Instance.shapes.ContainsKey(usingGroupKey))
                {
                    Shapes.Instance.shapes[usingGroupKey] = new Dictionary<string, Shape>();
                }
                Point TLPoint = new Point(X, Y);
                Point BRPoint = new Point(X + 20, Y + 20);
                Point TRPoint = new Point(TLPoint.X, BRPoint.Y);
                Point BLPoint = new Point(BRPoint.X, TLPoint.Y);

                Shape newShape = new Shape
                {
                    TLPoint = TLPoint,
                    BRPoint = BRPoint,
                    TRPoint = TRPoint,
                    BLPoint = BLPoint,
                    IsSelected = false
                };

                Shapes.Instance.shapes[usingGroupKey][usingGroupKey] = newShape;
                this.ShapesUpdated?.Invoke();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public void DeleteSelectedShape()
        {
            try
            {
                logW.Ins.info($"[SHAPE][0] DEL");
                if (selectedShape != null && selectedGroupKey != null)
                {

                    var group = Shapes.Instance.shapes[selectedGroupKey];
                    Shape shapeVal = group.FirstOrDefault(x => x.Value == selectedShape).Value;
                    string shapeId = group.FirstOrDefault(x => x.Value == selectedShape).Key;
                    if (shapeId != null)
                    {
                        XParameterManager.Instance.ucBlockBasicControllers.Remove(shapeId);
                        if (shapeVal.shapeParent != null && XParameterManager.Instance.ucBlockBasicControllers.ContainsKey(shapeVal.shapeParent))
                        {
                            XParameterManager.Instance.ucBlockBasicControllers[shapeVal.shapeParent].Remove(shapeId);
                        }
                        group.Remove(shapeId);
                        if (!group.Any())
                        {
                            Shapes.Instance.shapes.Remove(selectedGroupKey); // Xóa nhóm nếu trống
                        }

                        selectedShape = null;
                        selectedGroupKey = null;
                        this.shapesDeleted?.Invoke();
                    }
                }
            }
            catch (Exception ex)
            {
                logW.Ins.Except($"[SHAPE][0] DEL: \n {ex.ToString()}");
            }

        }
        public void OnPaint(Graphics g)
        {
            try
            {
                if (pictureDrawing == null || pictureDrawing.Image == null || g == null || !Shapes.Instance.shapes.Keys.Contains(funcDraw))
                {
                    return;
                }
                string key = funcDraw;
                Dictionary<string, Shape> group = Shapes.Instance.shapes[key];
                foreach (string key1 in group.Keys)
                {
                    Shape shape = group[key1];
                    //if (!shape.IsSelected) { continue; }
                    PointF TL = shape.TLPoint;
                    PointF BR = shape.BRPoint;
                    PointF TR = new PointF(shape.BRPoint.X, shape.TLPoint.Y);
                    PointF BL = new PointF(shape.TLPoint.X, shape.BRPoint.Y);
                    float xMax = Math.Max(TL.X, BR.X);
                    float yMax = Math.Max(TL.Y, BR.Y);
                    float xMin = Math.Min(TL.X, BR.X);
                    float yMin = Math.Min(TL.Y, BR.Y);
                    xMax = (xMax > rectImageWidth + WhiteoffsetX) ? rectImageWidth + WhiteoffsetX : xMax;
                    xMin = (xMin < WhiteoffsetX) ? WhiteoffsetX : xMin;
                    yMax = (yMax > rectImageHeigh + WhiteoffsetY) ? rectImageHeigh + WhiteoffsetY : yMax;
                    yMin = (yMin < WhiteoffsetY) ? WhiteoffsetY : yMin;
                    if (xMax == xMin || yMax == yMin) { }
                    else {
                        TL = new PointF(xMin, yMin);
                        TR = new PointF(xMax, yMin);
                        BR = new PointF(xMax, yMax);
                        BL = new PointF(xMin, yMax);
                    }

                    Shapes.Instance.shapes[key][key1].TLPoint = TL;
                    Shapes.Instance.shapes[key][key1].TRPoint = TR;
                    Shapes.Instance.shapes[key][key1].BRPoint = BR;
                    Shapes.Instance.shapes[key][key1].BLPoint = BL;
                    Pen pen = shape.IsSelected
                        ? new Pen(Color.Red, 2f) { DashStyle = DashStyle.Dash }
                        : new Pen(Color.Blue, 2f) { DashStyle = DashStyle.Dash };

                    g.DrawLine(pen, TL, BR);
                    g.DrawLine(pen, TR, BL);
                    g.DrawLine(pen, TL, TR);
                    g.DrawLine(pen, TR, BR);
                    g.DrawLine(pen, BR, BL);
                    g.DrawLine(pen, BL, TL);

                    DrawCorner(g, Brushes.Green, new Point((int)(shape.TLPoint.X), (int)(shape.TLPoint.Y)));
                    DrawCorner(g, Brushes.Green, new Point((int)(shape.BRPoint.X), (int)(shape.BRPoint.Y)));
                    DrawCorner(g, Brushes.Green, new Point((int)(TR.X), (int)(TR.Y)));
                    DrawCorner(g, Brushes.Green, new Point((int)(BL.X), (int)(BL.Y)));
                    string[] conntenShow = key1.Split(new string[] { "_0_" }, StringSplitOptions.None);
                    string subshow = (conntenShow.Length > 1) ? conntenShow[1]?.Substring(0, (conntenShow[1].Length > 5) ? 5 : conntenShow[1].Length) : key1;
                    string shapeName = Shapes.Instance.shapes[key][key1].shapeName;
                    DrawTextAtTopLeft(g, TL, $"{(int)TL.X}X{(int)TL.Y} - {shapeName} - {subshow}");
                    LoadCoordinerPicToImage(key, key1);
                }
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
        public void OnMouseDBClick(PointF mouseLocation)
        {
            try
            {
                if (selectedShape == null) return;
                if (selectedShape.shapeId == null) return;
                if (!XParameterManager.Instance.ucBlockBasicControllers.Keys.Contains(selectedShape.shapeId))
                {
                    return;
                }
                // Nếu không chọn hình nào, bỏ chọn toàn bộ
                foreach (var group1 in Shapes.Instance.shapes.Values)
                {
                    foreach (var shape in group1.Values)
                    {
                        shape.IsSelected = false;
                    }
                }
                var groupKey = funcDraw;
                var group = Shapes.Instance.shapes[funcDraw];

                selectedShape = group.Values.FirstOrDefault(shape =>
                    IsPointInCorner(mouseLocation, shape.TLPoint) ||
                    IsPointInCorner(mouseLocation, shape.BRPoint) ||
                    IsPointInCorner(mouseLocation, shape.TRPoint) ||
                    IsPointInCorner(mouseLocation, shape.BLPoint) ||
                    IsPointInShape(mouseLocation, shape));
                if (selectedShape != null && selectedShape.shapeId.Contains("ROI1"))
                {
                    if (XParameterManager.Instance.ucBlockBasicControllers.ContainsKey(selectedShape.shapeParent))
                    {
                        string roi1_blockID = XParameterManager.Instance.ucBlockBasicControllers[selectedShape.shapeParent][selectedShape.shapeId]["ROI1_1_BlockID"].ToString();
                        if (roi1_blockID.Contains("TemplateMatching") || roi1_blockID == "PerformPatternMatching")
                        {
                            var dialog = new DialogSettingWidgetToolTrainTemp((int)selectedShape.TLPoint.X, (int)selectedShape.TLPoint.Y, selectedShape.shapeId, selectedShape.shapeName, selectedShape.shapeParent, txtPathImgControl, selectedTaskIDRun);
                            dialog.StartPosition = FormStartPosition.CenterParent;
                            cvX.RegisterActionImshow(selectedShape.shapeId, dialog);
                            dialog.Show();
                        }
                        else
                        {
                            var dialog = new DialogSettingWidgetToolBase((int)selectedShape.TLPoint.X, (int)selectedShape.TLPoint.Y, selectedShape.shapeId, selectedShape.shapeName, selectedShape.shapeParent, txtPathImgControl, selectedTaskIDRun);
                            dialog.StartPosition = FormStartPosition.CenterParent;
                            cvX.RegisterActionImshow(selectedShape.shapeId, dialog);
                            dialog.Show();
                        }
                        return;
                    }
                }
                this.ShapesUpdated?.Invoke();
            }
            catch (Exception x)
            {
                MessageBox.Show($"[OnDoubleClick] ex: {x}");
            }
        }
        public void OnMouseDown(PointF mouseLocation)
        {
            try
            {   if (!mouseUpFlag) { return; }
                if (!Shapes.Instance.shapes.ContainsKey(funcDraw)) return;
                // Nếu không chọn hình nào, bỏ chọn toàn bộ
                foreach (var group1 in Shapes.Instance.shapes.Values) {
                    foreach (var shape in group1.Values) {
                        shape.IsSelected = false;
                    }
                }
                var group = Shapes.Instance.shapes[funcDraw];

                mouseUpFlag = false;
                mouseDownFlag = true;
                selectedShape = group.Values.FirstOrDefault(shape =>
                    IsPointInCorner(mouseLocation, shape.TLPoint) ||
                    IsPointInCorner(mouseLocation, shape.BRPoint) ||
                    IsPointInCorner(mouseLocation, shape.TRPoint) ||
                    IsPointInCorner(mouseLocation, shape.BLPoint) ||
                    IsPointInShape(mouseLocation, shape));

                if (selectedShape != null)
                {
                    selectedGroupKey = funcDraw;
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
            mouseUpFlag = true;
            mouseDownFlag = false;
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
            if (e.Data.GetDataPresent("Dictionary"))
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
                if (e.Data.GetDataPresent("Dictionary")) {
                    MouseCoordinate = pictureDrawing.PointToClient(new Point(e.X, e.Y));
                    AddNewShape();
                } else {
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
