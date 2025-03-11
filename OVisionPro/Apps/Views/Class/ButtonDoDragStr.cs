using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OVisionPro
{
    public class DoDragDropButtonStr : ToolStripButton
    {
        public string blockID = "";
        public string shapeID = "";
        public DoDragDropButtonStr()
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
                string idShape = (shapeID == "") ? Guid.NewGuid().ToString() : shapeID;
                // Tạo một Dictionary để gửi
                // Sử dụng DataObject để gửi dữ liệu
                DataObject data = new DataObject();
                data.SetData("tempDrag", $"{blockID}|||{idShape}");  // Gửi Dictionary với key là "Dictionary"

                // Bắt đầu kéo dữ liệu
                DoDragDrop(data, DragDropEffects.Copy);
            }
        }
    }
}
