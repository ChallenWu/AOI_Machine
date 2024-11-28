using System;
using System.Collections.Generic;
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
public class RoundedPanelDragBasic : Panel
{
    private Dictionary<string, object> dictComponents = new Dictionary<string, object>();
    private int cornerRadius = 5;
    private Color borderColor = Color.Blue;
    private int borderThickness = 5;
    private Panel draggablePanel;
    private bool isDragging = false;
    private Point lastCursor;
    private Point lastControl;

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

    public RoundedPanelDragBasic()
    {
        MakeMainPanel();
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
    private void DraggablePanel_MouseDown(object sender, MouseEventArgs e)
    {
        isDragging = true;
        lastCursor = Cursor.Position;
        lastControl = this.Location;
    }

    private void DraggablePanel_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDragging)
        {
            var diff = new Point(Cursor.Position.X - lastCursor.X, Cursor.Position.Y - lastCursor.Y);
            this.Location = new Point(lastControl.X + diff.X, lastControl.Y + diff.Y);
        }
    }

    private void DraggablePanel_MouseUp(object sender, MouseEventArgs e)
    {
        isDragging = false;
    }

    // Đảm bảo rằng khi bạn thêm điều khiển vào panel, chúng sẽ không che viền
    public void AddControlToPanel(Control control)
    {
        this.Controls.Add(control);
        control.BringToFront(); // Đảm bảo điều khiển này nằm trên cùng
        this.Invalidate(); // Tái vẽ panel để đảm bảo viền luôn hiển thị
    }

    public void MakeMainPanel()
    {
        //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoundedPanelDragBasic));
        RoundedPanel pnfooter = new RoundedPanel();
        RoundedPanel pnheader = new RoundedPanel();
        System.Windows.Forms.Label lblL = new System.Windows.Forms.Label();
        System.Windows.Forms.Label lblR = new System.Windows.Forms.Label();
        this.SuspendLayout();
        pnheader.SuspendLayout();
        dictComponents.Add("2", pnfooter);
        dictComponents.Add("3", pnheader);
        dictComponents.Add("4", lblL);
        dictComponents.Add("5", lblR);

        draggablePanel = pnheader;
        // 
        // roundedPanelDragBasic1
        // 
        this.BackColor = System.Drawing.Color.Gainsboro;
        this.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.BorderThickness = 5;
        this.Controls.Add(pnfooter);
        this.Controls.Add(pnheader);
        this.CornerRadius = 5;
        this.Location = new System.Drawing.Point(390, 159);
        //this.Name = "roundedPanelDragBasic11";
        this.Size = new System.Drawing.Size(238, 214);
        this.TabIndex = 2;
        // 
        // pnfooter
        // 
        pnfooter.BorderColor = System.Drawing.Color.Black;
        pnfooter.BorderThickness = 5;
        pnfooter.CornerRadius = 5;
        pnfooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        pnfooter.Location = new System.Drawing.Point(0, 178);
        //pnfooter.Name = "pnfooter1";
        pnfooter.Size = new System.Drawing.Size(238, 36);
        pnfooter.TabIndex = 1;
        // 
        // pnheader
        // 
        pnheader.BackColor = System.Drawing.Color.Silver;
        pnheader.BorderColor = System.Drawing.Color.Black;
        pnheader.BorderThickness = 5;
        pnheader.Controls.Add(lblL);
        pnheader.Controls.Add(lblR);
        pnheader.CornerRadius = 5;
        pnheader.Dock = System.Windows.Forms.DockStyle.Top;
        pnheader.Location = new System.Drawing.Point(0, 0);
        //pnheader.Name = "pnheader1";
        pnheader.Size = new System.Drawing.Size(238, 40);
        pnheader.TabIndex = 0;
        // 
        // lblL
        // 
        lblL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Left)));
        lblL.AutoSize = true;
        lblL.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //lblL.Image = ((System.Drawing.Image)(resources.GetObject("lblL.Image")));
        lblL.Location = new System.Drawing.Point(11, 14);
        lblL.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        //lblL.Name = "lblL";
        lblL.Size = new System.Drawing.Size(13, 13);
        lblL.TabIndex = 5;
        lblL.Text = " + ";
        // 
        // lblR
        // 
        lblR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
        | System.Windows.Forms.AnchorStyles.Right)));
        lblR.AutoSize = true;
        lblR.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //lblR.Image = ((System.Drawing.Image)(resources.GetObject("lblR.Image")));
        lblR.Location = new System.Drawing.Point(214, 14);
        lblR.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        //lblR.Name = "lblR";
        lblR.Size = new System.Drawing.Size(13, 13);
        lblR.TabIndex = 4;
        lblR.Text = " + ";


        pnheader.MouseDown += new MouseEventHandler(DraggablePanel_MouseDown);
        pnheader.MouseMove += new MouseEventHandler(DraggablePanel_MouseMove);
        pnheader.MouseUp += new MouseEventHandler(DraggablePanel_MouseUp);
        pnheader.Paint += new PaintEventHandler(DrawTitle);
        // 
        // Form1
        // 
        this.ResumeLayout(false);
        pnheader.ResumeLayout(false);
        pnheader.PerformLayout();
    }
    private string titleText = "My Custom Title";

    private void DrawTitle(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;

        // Thiết lập font và màu sắc
        Font font = new Font("Arial", 12, FontStyle.Bold);
        Brush brush = new SolidBrush(Color.Black);

        // Vẽ text ở vị trí phía trên cùng bên trái của UserControl
        g.DrawString(titleText, font, brush, new PointF(-100, 10));

        // Giải phóng tài nguyên
        font.Dispose();
        brush.Dispose();
    }

    // Tùy chọn: Thêm thuộc tính để cho phép thay đổi title
    public string TitleText
    {
        get { return titleText; }
        set
        {
            titleText = value;
            this.Invalidate(); // Yêu cầu vẽ lại UserControl khi thay đổi title
        }
    }

    private void pnHeaderBlock_Paint(object sender, PaintEventArgs e)
    {
        Control control = sender as Control;
        control?.BringToFront();
    }
}