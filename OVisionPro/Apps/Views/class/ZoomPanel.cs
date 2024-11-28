using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class ZoomPanel : Panel
{
    private float zoomFactor = 1.1f;  // Tỷ lệ zoom ban đầu
    private PointF mousePosition;  // Vị trí con trỏ chuột khi zoom
    private bool isDragging = false;  // Biến kiểm tra xem có đang kéo chuột hay không
    private Point lastMousePosition;  // Vị trí chuột khi bắt đầu kéo
    public ZoomPanel()
    {
        this.AutoScroll = true;  // Cho phép cuộn khi các điều khiển con vượt ngoài kích thước Panel
        this.MouseWheel += ZoomablePanel_MouseWheel;  // Sự kiện cuộn chuột để zoom
        this.MouseMove += ZoomablePanel_MouseMove;  // Cập nhật vị trí con trỏ chuột
        this.MouseDown += ZoomablePanel_MouseDown;  // Sự kiện khi nhấn chuột để bắt đầu kéo
        this.MouseUp += ZoomablePanel_MouseUp;  // Sự kiện khi thả chuột để kết thúc kéo
    }

    // Xử lý sự kiện cuộn chuột để zoom in hoặc zoom out
    private void ZoomablePanel_MouseWheel(object sender, MouseEventArgs e)
    {
        // Lưu lại vị trí con trỏ chuột khi bắt đầu zoom
        mousePosition = this.PointToClient(Cursor.Position);

        // Điều chỉnh zoomFactor khi cuộn chuột lên hoặc xuống
        if (e.Delta > 0)
        {
            OutControls();
        }
        else
        {
            InControls();
        }

        // Gọi lại phương thức Invalidate để yêu cầu vẽ lại Panel
        this.Invalidate();
    }

    // Cập nhật kích thước và vị trí của các điều khiển con trong Panel
    private void OutControls()
    {
        this.BeginInvoke(new Action(() =>
        {
            // Cập nhật lại kích thước và vị trí của các điều khiển con trong Panel
            foreach (Control control in this.Controls)
            {
                // Tính toán lại kích thước của các điều khiển con và làm tròn về kiểu int
                control.Width = (int)Math.Round(control.Width / zoomFactor);
                control.Height = (int)Math.Round(control.Height / zoomFactor);

                // Tính toán lại vị trí của các điều khiển con và làm tròn về kiểu int
                control.Left = (int)Math.Round((control.Left + mousePosition.X) / zoomFactor) - (int)mousePosition.X;
                control.Top = (int)Math.Round((control.Top + mousePosition.Y) / zoomFactor) - (int)mousePosition.Y;
            }
        }));
    }
    private void InControls()
    {


        this.BeginInvoke(new Action(() =>
        {
            // Cập nhật lại kích thước và vị trí của các điều khiển con trong Panel
            foreach (Control control in this.Controls)
            {
                // Tính toán lại kích thước của các điều khiển con và làm tròn về kiểu int
                control.Width = (int)Math.Round(control.Width * zoomFactor);
                control.Height = (int)Math.Round(control.Height * zoomFactor);

                // Tính toán lại vị trí của các điều khiển con và làm tròn về kiểu int
                control.Left = (int)Math.Round((control.Left + mousePosition.X) * zoomFactor) - (int)mousePosition.X;
                control.Top = (int)Math.Round((control.Top + mousePosition.Y) * zoomFactor) - (int)mousePosition.Y;
            }
        }));


        
    }

    // Cập nhật vị trí của con trỏ chuột khi zoom
    private void ZoomablePanel_MouseMove(object sender, MouseEventArgs e)
    {

        this.BeginInvoke(new Action(() =>
        {
            mousePosition = this.PointToClient(Cursor.Position);

            // Nếu đang kéo chuột, di chuyển toàn bộ các điều khiển con
            if (isDragging)
            {
                // Tính toán sự di chuyển
                var deltaX = e.X - lastMousePosition.X;
                var deltaY = e.Y - lastMousePosition.Y;

                // Di chuyển các điều khiển con
                foreach (Control control in this.Controls)
                {
                    control.Left += deltaX;
                    control.Top += deltaY;
                }

                // Cập nhật vị trí chuột cuối cùng
                lastMousePosition = e.Location;
            }
        }));
    }

    // Xử lý sự kiện khi bắt đầu kéo chuột
    private void ZoomablePanel_MouseDown(object sender, MouseEventArgs e)
    {
        // Đánh dấu bắt đầu kéo chuột
        isDragging = true;
        lastMousePosition = e.Location;
    }

    // Xử lý sự kiện khi thả chuột để kết thúc kéo
    private void ZoomablePanel_MouseUp(object sender, MouseEventArgs e)
    {
        // Đánh dấu kết thúc kéo chuột
        isDragging = false;
    }
}
