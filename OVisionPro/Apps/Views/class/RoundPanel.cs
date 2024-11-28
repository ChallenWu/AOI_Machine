using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//Thuộc tính BorderColor, BorderThickness, CornerRadius:

//BorderColor: Màu của viền.
//BorderThickness: Độ dày của viền.
//CornerRadius: Độ cong của góc, càng lớn thì góc càng tròn.
//Sử dụng GraphicsPath để tạo các góc tròn:

//GraphicsPath cho phép tạo các đường cong và hình dạng tuỳ chỉnh.
//AddArc được dùng để tạo các góc tròn cho mỗi góc của Panel.
//Sử dụng Pen để vẽ viền:

//Pen được thiết lập với BorderColor và BorderThickness để vẽ đường viền tròn quanh Panel.

////RoundedPanel roundedPanel = new RoundedPanel
////{
////    Size = new Size(200, 150),
////    Location = new Point(50, 50),
////    BorderColor = Color.Red,
////    BorderThickness = 3,
////    CornerRadius = 30
////};
public class RoundedPanel : Panel
{
    private int cornerRadius = 5;
    private Color borderColor = Color.Black;
    private int borderThickness = 5;

    public int CornerRadius
    {
        get { return cornerRadius; }
        set { cornerRadius = value; Invalidate(); }
    }

    public Color BorderColor
    {
        get { return borderColor; }
        set { borderColor = value; Invalidate(); }
    }

    public int BorderThickness
    {
        get { return borderThickness; }
        set { borderThickness = value; Invalidate(); }
    }

    public RoundedPanel()
    {
        this.DoubleBuffered = true; // Giúp loại bỏ nhấp nháy khi vẽ
    }

    // Vẽ viền tròn cho panel
    protected override void OnPaint(PaintEventArgs e)
    {

        Graphics g = e.Graphics;
        Rectangle rect = this.ClientRectangle;

        // Tạo GraphicsPath cho các góc tròn
        using (GraphicsPath path = new GraphicsPath())
        {
            int diameter = cornerRadius * 2;
            path.AddArc(rect.Left, rect.Top, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Top, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            // Cập nhật vùng để không có góc vuông
            this.Region = new Region(path);

            // Vẽ viền tròn
            using (Pen pen = new Pen(borderColor, borderThickness))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawPath(pen, path);
            }
        }
        base.OnPaint(e);
    }

    // Đảm bảo rằng khi bạn thêm điều khiển vào panel, chúng sẽ không che viền
    public void AddControlToPanel(Control control)
    {
        this.Controls.Add(control);
        control.BringToFront(); // Đảm bảo điều khiển này nằm trên cùng
        this.Invalidate(); // Tái vẽ panel để đảm bảo viền luôn hiển thị
    }
}