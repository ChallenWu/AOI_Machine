using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace OVisionPro
{
    public class CustomToolStripComboBox : ToolStripComboBox
    {
        public CustomToolStripComboBox()
        {
            // Đặt một số thuộc tính cơ bản
            this.DropDownStyle = ComboBoxStyle.DropDownList; // Chỉ cho phép chọn
            this.ComboBox.FlatStyle = FlatStyle.Standard;       // Phong cách phẳng để dễ tùy chỉnh
            this.ComboBox.DrawMode = DrawMode.OwnerDrawFixed; // Bật vẽ tùy chỉnh
            this.ComboBox.Width = 150;

            // Đăng ký sự kiện vẽ
            this.ComboBox.DrawItem += ComboBox_DrawItem;
            this.ComboBox.Paint += ComboBox_Paint;
        }

        // Sự kiện vẽ từng mục trong danh sách thả xuống
        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            // Vẽ nền
            e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Vẽ văn bản
            string text = this.Items[e.Index].ToString();
            e.Graphics.DrawString(text, e.Font, Brushes.Black, e.Bounds.X, e.Bounds.Y + 2);

            // Nếu mục được chọn, vẽ khung viền
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                using (Pen borderPen = new Pen(Color.Blue, 2))
                {
                    e.Graphics.DrawRectangle(borderPen, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
                }
            }
        }

        // Sự kiện vẽ khung ngoài của ComboBox
        private void ComboBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Vẽ vùng bo góc
            Rectangle rect = this.ComboBox.ClientRectangle;
            rect.Inflate(-2, -2); // Bên trong một chút để đẹp hơn

            GraphicsPath path = new GraphicsPath();
            int radius = 10; // Bán kính bo góc
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();

            // Vẽ nền làm nổi
            using (SolidBrush brush = new SolidBrush(Color.White))
            {
                g.FillPath(brush, path);
            }

            // Đổ bóng làm nổi
            using (Pen shadowPen = new Pen(Color.LightGray, 2))
            {
                g.DrawPath(shadowPen, path);
            }

            // Vẽ viền
            using (Pen borderPen = new Pen(Color.Gray, 2))
            {
                g.DrawPath(borderPen, path);
            }
        }
    }
    public class RoundedToolStripComboBox : ToolStripComboBox
    {
        public int BorderRadius { get; set; } = 10; // Độ bo góc
        public Color BorderColor { get; set; } = Color.Gray; // Màu viền
        public Color BackgroundColor { get; set; } = Color.White; // Màu nền
        public Color ShadowColor { get; set; } = Color.LightGray; // Màu bóng
        public int ShadowDepth { get; set; } = 4;  // Độ sâu đổ bóng

        public RoundedToolStripComboBox()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ComboBox.FlatStyle = FlatStyle.Flat;
            this.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            this.ComboBox.DrawItem += ComboBox_DrawItem;
            this.ComboBox.Paint += ComboBox_Paint;
        }

        // Sự kiện vẽ khung ngoài của ComboBox
        private void ComboBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Vẽ đổ bóng
            Rectangle rect = this.ComboBox.ClientRectangle;
            Rectangle shadowRect = new Rectangle(rect.X + ShadowDepth, rect.Y + ShadowDepth, rect.Width - ShadowDepth, rect.Height - ShadowDepth);
            using (SolidBrush shadowBrush = new SolidBrush(ShadowColor))
            {
                g.FillRoundedRectangle(shadowBrush, shadowRect, BorderRadius);
            }

            // Vẽ nền ComboBox
            using (SolidBrush backgroundBrush = new SolidBrush(BackgroundColor))
            {
                g.FillRoundedRectangle(backgroundBrush, rect, BorderRadius);
            }

            // Vẽ viền
            using (Pen borderPen = new Pen(BorderColor, 2))
            {
                g.DrawRoundedRectangle(borderPen, rect, BorderRadius);
            }
        }

        // Sự kiện vẽ từng mục trong ComboBox
        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Vẽ nền cho mục được chọn
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightBlue), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(BackgroundColor), e.Bounds);
            }

            // Vẽ văn bản
            string text = this.Items[e.Index].ToString();
            g.DrawString(text, e.Font, Brushes.Black, e.Bounds.X + 5, e.Bounds.Y + 2);

            e.DrawFocusRectangle();
        }
    }

    // Helper class for drawing rounded rectangles
    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics g, Brush brush, Rectangle bounds, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(bounds, cornerRadius))
            {
                g.FillPath(brush, path);
            }
        }

        public static void DrawRoundedRectangle(this Graphics g, Pen pen, Rectangle bounds, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(bounds, cornerRadius))
            {
                g.DrawPath(pen, path);
            }
        }

        private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = cornerRadius * 2;

            // Add rounded corners
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}
