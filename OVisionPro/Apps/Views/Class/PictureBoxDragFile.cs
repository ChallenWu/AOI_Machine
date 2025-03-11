using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OVisionPro
{
    public class DragDropPictureBox : PictureBox
    {
        public string imgFilePath = "";
        public static Action<string> ActionUpdateimgFilePath;
        // Khởi tạo constructor
        public DragDropPictureBox()
        {
            // Cho phép kéo và thả
            this.AllowDrop = true;

            // Đăng ký sự kiện kéo-thả
            this.DragEnter += DragDropPictureBox_DragEnter;
            this.DragDrop += DragDropPictureBox_DragDrop;
        }

        // Kiểm tra xem file có phải là ảnh không
        private bool IsImageFile(string filePath)
        {
            string[] validExtensions = { ".jpg", ".jpeg", ".png", ".bmp"};
            string extension = Path.GetExtension(filePath).ToLower();
            return Array.Exists(validExtensions, ext => ext == extension);
        }

        // Xử lý sự kiện DragEnter khi kéo vào PictureBox
        private void DragDropPictureBox_DragEnter(object sender, DragEventArgs e)
        {
            // Kiểm tra dữ liệu thả có phải là tệp không
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Kiểm tra nếu tệp đầu tiên là ảnh hợp lệ
                if (files.Length > 0 && IsImageFile(files[0]))
                {
                    e.Effect = DragDropEffects.Copy; // Cho phép thả ảnh vào
                }
                else
                {
                    e.Effect = DragDropEffects.None; // Không cho phép thả nếu không phải ảnh
                }
            }
        }

        // Xử lý sự kiện DragDrop khi thả vào PictureBox
        private void DragDropPictureBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);

                // Nếu tệp là ảnh hợp lệ
                if (files.Length > 0 && IsImageFile(files[0]))
                {
                    imgFilePath = files[0];
                    try
                    {
                        // Hiển thị ảnh trong PictureBox
                        this.Image = Image.FromFile(imgFilePath);
                        ActionUpdateimgFilePath?.Invoke(imgFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Không thể mở file ảnh: " + ex.Message);
                    }
                }
            }
        }
    }
}
