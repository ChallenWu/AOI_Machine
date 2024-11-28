using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OVisionPro
{
    public class DragDropTextBox : ToolStripTextBox
    {
        // Danh sách phần mở rộng tệp được phép (nếu cần)
        public string[] AllowedExtensions { get; set; } = new[] { ".png", ".jpg", ".bmp"};
        public DragDropTextBox()
        {
            // Cho phép kéo-thả
            this.TextBox.AllowDrop = true;

            // Gắn sự kiện kéo-thả
            this.TextBox.DragEnter += TextBox_DragEnter;
            this.TextBox.DragDrop += TextBox_DragDrop;
        }

        private void TextBox_DragEnter(object sender, DragEventArgs e)
        {
            // Kiểm tra nếu dữ liệu là tệp
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Nếu có danh sách phần mở rộng, kiểm tra file hợp lệ
                if (AllowedExtensions.Length > 0)
                {
                    var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (files.Any(file => AllowedExtensions.Contains(Path.GetExtension(file).ToLower())))
                    {
                        e.Effect = DragDropEffects.Copy;
                        return;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }

            e.Effect = DragDropEffects.None;
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

                // Hiển thị danh sách file trong ToolStripTextBox
                this.Text = string.Join(Environment.NewLine, files);
            }
        }
    }
}
