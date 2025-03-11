using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OVisionPro
{
    public class DragDropTextBoxNorm : TextBox
    {
        // Danh sách phần mở rộng tệp được phép (nếu cần)
        public string[] AllowedExtensions { get; set; } = new[] { ".png", ".jpg", ".bmp" };

        public DragDropTextBoxNorm()
        {
            // Cho phép kéo-thả trên chính TextBox
            this.AllowDrop = true;
            // Đặt chiều rộng và chiều cao tối thiểu
            //this.MinimumSize = new Size(300, 45);  // Minimum width 300, minimum height 100

            // Đảm bảo AutoSize là false (tránh việc thay đổi kích thước tự động)
            this.AutoSize = false;
            // Gắn sự kiện kéo-thả
            this.DragEnter += TextBox_DragEnter;
            this.DragDrop += TextBox_DragDrop;
        }

        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            // Kiểm tra nếu dữ liệu là tệp
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Nếu có danh sách phần mở rộng, kiểm tra file hợp lệ
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (AllowedExtensions.Length > 0)
                {
                    if (files.Any(file => AllowedExtensions.Contains(Path.GetExtension(file).ToLower())))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void TextBox_DragDrop(object sender, DragEventArgs e)
        {
            // Lấy danh sách tệp được kéo vào
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Nếu có danh sách phần mở rộng, lọc file
                if (AllowedExtensions.Length > 0)
                {
                    files = files.Where(file => AllowedExtensions.Contains(Path.GetExtension(file).ToLower())).ToArray();
                }

                // Hiển thị danh sách file trong TextBox (có thể là các file hợp lệ)
                this.Text = string.Join(Environment.NewLine, files);
            }
        }
    }
}
