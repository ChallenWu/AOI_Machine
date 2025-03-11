using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OVisionPro
{
    public class DoDragDropButton : ToolStripButton
    {
        public DoDragDropButton()
        {
            // Cho phép kéo-thả
            this.AllowDrop = true;

            // Gắn sự kiện kéo-thả
            this.MouseDown += btnDrag_MouseDown;
        }

        private void btnDrag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Tạo một Dictionary để gửi
                Dictionary<string, int> dataToSend = new Dictionary<string, int>
                {
                    { "key1", 10 },
                    { "key2", 20 },
                    { "key3", 30 }
                };

                // Sử dụng DataObject để gửi dữ liệu
                DataObject data = new DataObject();
                data.SetData("Dictionary", dataToSend);  // Gửi Dictionary với key là "Dictionary"

                // Bắt đầu kéo dữ liệu
                DoDragDrop(data, DragDropEffects.Copy);
            }
        }
    }
}
