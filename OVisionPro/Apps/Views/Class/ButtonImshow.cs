using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro.Apps.Views.Class
{
    public class ButtonImshow : Button
    {
        private bool imshowStatus=false;
        // Cách để truy cập thuộc tính Name từ lớp cơ sở
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        private void onClicked(object sender, EventArgs e)
        {
            imshowStatus = !imshowStatus;
            if (imshowStatus)
            {
                BackColor = Color.Green;
            }
            else
            {
                BackColor = Color.Gray;
            }
        }
        public void AddEvent()
        {
            Click += onClicked;
        }
        public ButtonImshow() { AddEvent(); }
    }
}
