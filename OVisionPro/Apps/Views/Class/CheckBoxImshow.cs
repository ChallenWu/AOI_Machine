using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro
{
    public class CheckBoxImshow : CheckBox
    {
        private bool imshowStatus;
        public bool ImshowStatus
        {
            get { return imshowStatus; }
            set { imshowStatus = Checked; }
        }

        // Cách để truy cập thuộc tính Name từ lớp cơ sở
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
    }
}
