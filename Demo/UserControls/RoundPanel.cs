using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo;
using HB_IWatch;

namespace Demo.UserControls
{
    public partial class RoundPanel : UserControlBase
    {
        private Color color = MyColor.None;
        public RoundPanel()
        {
            InitializeComponent();
            this.BackColor = color;
            this.BackgroundImageLayout = ImageLayout.Center;            
        }

        public void SetBackColor(Color color)
        {
            this.BackColor = color;
        }
    }
}
