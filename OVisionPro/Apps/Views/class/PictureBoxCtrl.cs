using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Shapes;
using YamlDotNet.Core.Tokens;
using System.Reflection;
using System.Threading;

namespace OVisionPro
{
    public class ShapeEditor
    {
        public PictureBox pictureDrawing;

        // Dictionary cấp 2: GroupKey -> (ShapeID -> Shape)
        private Dictionary<string, Dictionary<string, Shape>> shapes = new Dictionary<string, Dictionary<string, Shape>>();

        private Shape selectedShape = null;
        private string selectedGroupKey = null;

        private bool isDraggingCorner = false;
        private bool isDraggingShape = false;
        private string draggingCorner = null;
        private PointF previousMousePosition;
        // zoom info
        public int WhiteoffsetX = 0;
        public int WhiteoffsetY = 0;
        public int displayWidthReal = 0;
        public int displayHeightReal = 0;
        public float zoomScaleX = 1f;
        public float zoomScaleY = 1f;
        public float zoomScale = 1f;
        public bool updatePropertyFlag = true;

        private static readonly ShapeEditor instance = new ShapeEditor();

        public static ShapeEditor Instance => instance;

        public event Action ShapesUpdated;
        public void LoadDrawFromConfig()
        {

        }

        private Point ConvertImgResizeToOrgCoordinate(Point originalPoint)
        {
            System.Drawing.Rectangle imageRect = GetImageRectangle(pictureDrawing);
            float scaleX = (float)imageRect.Width / pictureDrawing.Image.Width;
            float scaleY = (float)imageRect.Height / pictureDrawing.Image.Height;

            return new System.Drawing.Point(
                (int)(originalPoint.X * scaleX + imageRect.X),
                (int)(originalPoint.Y * scaleY + imageRect.Y)
            );
        }

        private System.Drawing.Rectangle GetImageRectangle(PictureBox pictureBox)
        {
            var propertyInfo = pictureBox.GetType().GetProperty(
                "ImageRectangle",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
            return (System.Drawing.Rectangle)propertyInfo.GetValue(pictureBox, null);
        }

        public void ConvertCoordinerPicToImage(string groupKey, string shapeID)
        {
            string[] key2 = shapeID.Split(new string[] { "?!" }, StringSplitOptions.None);
            Shape shape = shapes[groupKey][shapeID];
            PointF TL = ConvertPicImageToOrgImage(ConvertPicBoxToPicImage(shape.TLPoint));
            PointF BR = ConvertPicImageToOrgImage(ConvertPicBoxToPicImage(shape.BRPoint));
            XParameterManager.Instance.SetUcBlockBasicData(groupKey, key2[0], "x2", (int)(BR.X));
            XParameterManager.Instance.SetUcBlockBasicData(groupKey, key2[0], "y2", (int)(BR.Y));
            XParameterManager.Instance.SetUcBlockBasicData(groupKey, key2[0], "x1", (int)(TL.X));
            XParameterManager.Instance.SetUcBlockBasicData(groupKey, key2[0], "y1", (int)(TL.Y));
        }

        public PointF ConvertOrgImageToPicImage(PointF orgPoint)
        {
            // chuyển tọa đôj ảnh ban đầu sang toaj độ ảnh sau khi resize ( Note: cả 2 đều là tọa độ của picture.Image)
            // Lay toa do cua image ban dau

            System.Drawing.Rectangle imageRect = GetImageRectangle(pictureDrawing);
            float scaleX = (float)imageRect.Width / pictureDrawing.Image.Width;
            float scaleY = (float)imageRect.Height / pictureDrawing.Image.Height;
            float scale = Math.Min(scaleX, scaleY);
            PointF s02 = new PointF(
                (float)(scale * (orgPoint.X + WhiteoffsetX - imageRect.X)),
                (float)(scale * (orgPoint.Y + WhiteoffsetY - imageRect.Y))
            );
            return s02;
        }

        public PointF ConvertPicImageToOrgImage(PointF orgPoint)
        {
            /// chuyển tọa đôj ảnh ban đầu sang toaj độ ảnh sau khi resize ( Note: cả 2 đều là tọa độ của picture.Image)
            /// Vi thong thuong anh Img sau khi resize se nho hon Image truoc do.
            /// Lay toa do cua image ban dau
            System.Drawing.Rectangle imageRect = GetImageRectangle(pictureDrawing);

            float scaleX = (float)imageRect.Width / pictureDrawing.Image.Width;
            float scaleY = (float)imageRect.Height / pictureDrawing.Image.Height;
            float scale = Math.Min(scaleX, scaleY);
            PointF s01 = new PointF(
                (float)( orgPoint.X / scale + imageRect.X - WhiteoffsetX),
                (float)( orgPoint.Y / scale + imageRect.Y - WhiteoffsetY)
            );
            //PointF s02 = new PointF(
            //    (float)(scale * (s01.X + WhiteoffsetX - imageRect.X)),
            //    (float)(scale * (s01.Y + WhiteoffsetY - imageRect.Y))
            //);
            //PointF s03 = new PointF(
            //    (float)(s02.X / scale + imageRect.X - WhiteoffsetX),
            //    (float)(s02.Y / scale + imageRect.Y - WhiteoffsetY)
            //);
            return s01;
        }

        public PointF ConvertPicImageToPicBox(PointF picImagePoint)
        {

            float imgX = (float)(picImagePoint.X* zoomScale + WhiteoffsetX);
            float imgY = (float)(picImagePoint.Y* zoomScale + WhiteoffsetY);
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

        public void ConvertCoordinerImgToPic(string pointName="ALL")
        {

            if (pictureDrawing == null) return;
            updatePropertyFlag = false;
            foreach (string key in shapes.Keys)
            {
                Dictionary<string, Shape> group = shapes[key];
                foreach (string key1 in group.Keys)
                {
                    string[] key2 = key1.Split(new string[] { "?!" }, StringSplitOptions.None);
                    float x1 = (float)XParameterManager.Instance.GetUcBlockBasicData(key, key2[0], "x1", 0);
                    float y1 = (float)XParameterManager.Instance.GetUcBlockBasicData(key, key2[0], "y1", 0);
                    float x2 = (float)XParameterManager.Instance.GetUcBlockBasicData(key, key2[0], "x2", 1);
                    float y2 = (float)XParameterManager.Instance.GetUcBlockBasicData(key, key2[0], "y2", 1);
                    if (pointName == "TL")
                    {
                        PointF TLPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x1, y1)));
                        shapes[key][key1].TLPoint = TLPoint;
                    }
                    else if (pointName == "BR")
                    {
                        PointF BRPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x2, y2)));
                        shapes[key][key1].BRPoint = BRPoint;
                    }
                    else
                    {
                        PointF TLPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x1, y1)));
                        shapes[key][key1].TLPoint = TLPoint;
                        PointF BRPoint = ConvertPicImageToPicBox(ConvertOrgImageToPicImage(new PointF(x2, y2)));
                        shapes[key][key1].BRPoint = BRPoint;
                    }

                    //Point TRPoint = ConvertPicImageToPicBox(new Point(TLPoint.X, BRPoint.Y));
                    //Point BLPoint = ConvertPicImageToPicBox(new Point(BRPoint.X, TLPoint.Y));
                    //shapes[key][key1].TRPoint = TRPoint;
                    //shapes[key][key1].BLPoint = BLPoint;
                }
            }
        }

        public void AddNewShape(string groupKey, string shapeID)
        {
            try
            {
                if (!shapes.ContainsKey(groupKey))
                {
                    shapes[groupKey] = new Dictionary<string, Shape>();
                }
                //var pointDetail = XParameterManager.Instance.ucBlockBasicControllers[groupKey][shapeID];
                //foreach (var key in pointDetail.Keys) { 
                //    var x1 = pointDetail[key];
                //}

                //Point TLPoint = new Point(x1, y1);
                //Point BRPoint = new Point(x2, y2);
                if (!shapes.ContainsKey(groupKey))
                {
                    shapes[groupKey] = new Dictionary<string, Shape>();
                }

                string shapeId = shapeID + "?!" + Guid.NewGuid().ToString();
                Point TLPoint = new Point(0, 0);
                Point BRPoint = new Point(0, 0);
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

                shapes[groupKey][shapeId] = newShape;
                this.ShapesUpdated?.Invoke();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void DeleteSelectedShape()
        {
            if (selectedShape != null && selectedGroupKey != null)
            {
                var group = shapes[selectedGroupKey];
                string shapeId = group.FirstOrDefault(x => x.Value == selectedShape).Key;
                if (shapeId != null)
                {
                    group.Remove(shapeId);
                    if (!group.Any())
                    {
                        shapes.Remove(selectedGroupKey); // Xóa nhóm nếu trống
                    }

                    selectedShape = null;
                    selectedGroupKey = null;
                    this.ShapesUpdated?.Invoke();
                }
            }
        }

        public void OnPaint(Graphics g)
        {
            try
            {
                if (pictureDrawing.Image == null || pictureDrawing == null || g == null)
                {
                    return;
                }

                foreach (string key in shapes.Keys)
                {
                    Dictionary<string, Shape>  group = shapes[key];
                    foreach (string key1 in group.Keys)
                    {
                        Shape shape = group[key1];
                        
                        PointF TL = shape.TLPoint;
                        PointF BR = shape.BRPoint;
                        PointF TR = new PointF(shape.BRPoint.X, shape.TLPoint.Y);
                        PointF BL = new PointF(shape.TLPoint.X, shape.BRPoint.Y);

                        float xMax = Math.Max(TL.X, BR.X);
                        float yMax = Math.Max(TL.Y, BR.Y);
                        float xMin = Math.Min(TL.X, BR.X);
                        float yMin = Math.Min(TL.Y, BR.Y);

                        if (xMax == xMin || yMax == yMin)
                        {
                        }
                        else
                        {
                            TL = new PointF(xMin, yMin);
                            TR = new PointF(xMax, yMin);
                            BR = new PointF(xMax, yMax);
                            BL = new PointF(xMin, yMax);
                        }

                        shapes[key][key1].TLPoint = TL;
                        shapes[key][key1].TRPoint = TR;
                        shapes[key][key1].BRPoint = BR;
                        shapes[key][key1].BLPoint = BL;
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
                        DrawTextAtTopLeft(g, TL, $"X:{TL.X}_Y: {TL.Y} - {key1}");
                        
                        ConvertCoordinerPicToImage(key, key1);
                    }
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
                Brush brush = Brushes.Black;
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
            catch
            {
            }
        }

        public void OnMouseDown(PointF mouseLocation)
        {
            try
            {

                // Nếu không chọn hình nào, bỏ chọn toàn bộ
                foreach (var group in shapes.Values)
                {
                    foreach (var shape in group.Values)
                    {
                        shape.IsSelected = false;
                    }
                }
                foreach (var groupEntry in shapes)
                {
                    var groupKey = groupEntry.Key;
                    var group = groupEntry.Value;

                    selectedShape = group.Values.FirstOrDefault(shape =>
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
                            isDraggingCorner = true;
                            return;
                        }

                        isDraggingShape = true;
                        previousMousePosition = mouseLocation;
                        return;
                    }
                }

            }
            catch
            {
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
            catch
            {
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
            return Math.Abs(p.X - corner.X) <= 4 && Math.Abs(p.Y - corner.Y) <= 4;
        }

        private bool IsPointInShape(PointF p, Shape shape)
        {
            float minX = Math.Min(shape.TLPoint.X, shape.BRPoint.X);
            float maxX = Math.Max(shape.TLPoint.X, shape.BRPoint.X);
            float minY = Math.Min(shape.TLPoint.Y, shape.BRPoint.Y);
            float maxY = Math.Max(shape.TLPoint.Y, shape.BRPoint.Y);

            return p.X >= minX && p.X <= maxX && p.Y >= minY && p.Y <= maxY;
        }
    }
    public class Shape
    {
        public PointF TLPoint { get; set; }
        public PointF BRPoint { get; set; }
        public PointF TRPoint { get; set; }
        public PointF BLPoint { get; set; }
        public bool IsSelected { get; set; }
    }
}
