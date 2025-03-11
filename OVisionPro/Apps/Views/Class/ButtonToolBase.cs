using OVisionPro.Apps.Views.XWidgets;
using OVisionPro.Services._01._Core.ShapesPics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace OVisionPro.Apps.Views.Class
{
    public class ButtonToolBase: Button
    {
        private bool imshowStatus = false;
        private ToolStripComboBox taskIDCBB;
        // Cách để truy cập thuộc tính Name từ lớp cơ sở
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        public Color bgColor { get; set; }
        public int taskDebugging=0;

        private void onClicked(object sender, EventArgs e)
        {
            if (!imshowStatus)
            {
                imshowStatus = true;
                string[] res_cv_func = Name.Split(new string[] { "btn_" }, StringSplitOptions.None);
                using (var dia = new DialogSettingWidgetROIs(res_cv_func[1], int.Parse(taskIDCBB.Text)))
                {
                    imshowStatus = false;
                    cvX.RegisterActionImshow(res_cv_func[1], dia);
                    dia.ShowDialog(); 
                }

            }
            //imshowStatus = !imshowStatus;
            //if (imshowStatus) { bgColor = Color.Green; }
            //else { bgColor = Color.WhiteSmoke; }
            //BackColor = bgColor;
        }
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    taskIDCBB.Dispose();
                    // Lặp qua tất cả các controls trong form và dispose từng cái
                    foreach (Control ctrl in this.Controls) { ctrl.Dispose(); }
                }
                GC.Collect();  // Yêu cầu Garbage Collector thu hồi bộ nhớ ngay lập tức
                //GC.WaitForFullGCComplete();
                //this.FormClosed -= DialogSettingWidget_FormClosed;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[Dispose] ex: {ex}");
            }
            base.Dispose(disposing);
        }
        public void AddEvent()
        {
            Click += onClicked;
        }
        public ButtonToolBase(ToolStripComboBox TaskIDCBB) 
        {
            AutoSize = true;
            taskIDCBB = TaskIDCBB;
            Dock = System.Windows.Forms.DockStyle.Top;
            MinimumSize = new System.Drawing.Size(0, 50);
            Size = new System.Drawing.Size(305, 50);
            UseVisualStyleBackColor = true;
            AddEvent(); 
        }
    }
}
