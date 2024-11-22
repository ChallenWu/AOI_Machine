using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro.Apps.Views.XWidgets
{
    public partial class createSpeedDraw : UserControl
    {
        public createSpeedDraw()
        {
            InitializeComponent();
        }
    }
}

public class SpeedometerForm : Form
{
    private Timer timer;
    private float speed = 0;  // Giá trị tốc độ (0 - 100)
    private const float maxSpeed = 100; // Tốc độ tối đa
    private const float minSpeed = 0;   // Tốc độ tối thiểu

    public SpeedometerForm()
    {
        // Thiết lập cửa sổ
        this.Text = "Speedometer";
        this.Size = new Size(300, 300);
        this.StartPosition = FormStartPosition.CenterScreen;

        // Thiết lập Timer
        timer = new Timer();
        timer.Interval = 100; // Cập nhật tốc độ mỗi 100ms (0.1 giây)
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        // Cập nhật tốc độ mỗi giây (hoặc bạn có thể thay đổi cách tính toán)
        speed += 1;
        if (speed > maxSpeed)
            speed = minSpeed;

        // Yêu cầu vẽ lại Form mỗi khi tốc độ thay đổi
        this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Lấy đối tượng Graphics
        Graphics g = e.Graphics;

        // Vẽ vòng tròn speedometer
        DrawSpeedometerFace(g);

        // Vẽ kim chỉ tốc độ
        DrawNeedle(g, speed);
    }

    private void DrawSpeedometerFace(Graphics g)
    {
        // Vẽ vòng tròn ngoài cùng (Speedometer face)
        Pen pen = new Pen(Color.Black, 5);
        g.DrawEllipse(pen, 50, 50, 200, 200);

        // Vẽ các số và phân chia
        Font font = new Font("Arial", 10);
        for (int i = 0; i <= 10; i++)
        {
            float angle = 180f * i / 10;
            float x = (float)(150 + 80 * Math.Cos(Math.PI * angle / 180));
            float y = (float)(150 + 80 * Math.Sin(Math.PI * angle / 180));
            g.DrawString((i * 10).ToString(), font, Brushes.Black, x, y);
        }
    }

    private void DrawNeedle(Graphics g, float speed)
    {
        // Vẽ kim chỉ tốc độ
        float angle = 180f * speed / maxSpeed; // Góc kim chỉ tốc độ tương ứng với giá trị tốc độ

        float needleLength = 80;
        float needleX = (float)(150 + needleLength * Math.Cos(Math.PI * angle / 180));
        float needleY = (float)(150 + needleLength * Math.Sin(Math.PI * angle / 180));

        Pen needlePen = new Pen(Color.Red, 3);
        g.DrawLine(needlePen, 150, 150, needleX, needleY);
    }

    public static void Main()
    {
        // Chạy ứng dụng
        Application.Run(new SpeedometerForm());
    }
}
