using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace Demo.UserControls
{
    public partial class UserControlBase : UserControl
    {
        public UserControlBase()
        {
            InitializeComponent();
            this.Load += UserControlBase_Load;
        }

        public Rectangle OriginFormSize;
        float Ratio_Width, Ratio_Height;
        private void UserControlBase_Load(object sender, EventArgs e)
        {
            OriginFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            if (!DesignMode)
                this.Resize += UserControlBase_Resize;
        }
        int oldResizeWidth = 0;
        int oldResizeHeight = 0;
        private void UserControlBase_Resize(object sender, EventArgs e)
        {
            if (this.Width < 100 || this.Height < 100) return;
            if(this.Width == oldResizeWidth || this.Height == oldResizeHeight) return;
            Ratio_Width = this.Width / (float)OriginFormSize.Width;
            Ratio_Height = this.Height / (float)OriginFormSize.Height;
            AutoResize(this);
            OriginFormSize = new Rectangle(this.Location.X, this.Location.Y, this.Width, this.Height);
            oldResizeWidth = OriginFormSize.Width;
            oldResizeHeight = OriginFormSize.Height;

        }
        public void AutoResize(Control Maincontrol)
        {
            if (OriginFormSize.Width == 0 || OriginFormSize.Height == 0) return;
            foreach (Control control in Maincontrol.Controls)
            {
                float CurrentFont = float.Parse(Math.Round(control.Font.Size * (Math.Min(Ratio_Width, Ratio_Height)), 2).ToString());

                if (CurrentFont > 0)
                    control.Font = new Font(control.Font.Name, CurrentFont, control.Font.Style, control.Font.Unit);

                control.Left = Convert.ToInt32(control.Left * Ratio_Width);
                control.Top = Convert.ToInt32(control.Top * Ratio_Height);
                control.Width = Convert.ToInt32(control.Width * Ratio_Width);
                control.Height = Convert.ToInt32(control.Height * Ratio_Height);
                if (control.Controls.Count > 0)
                {
                    if (control is UserControl)
                    {
                    }
                    else
                        AutoResize(control);
                }

                control.Invalidate();
            }

        }
        public virtual void StartRefreshPage() { }
        public virtual void StopRefreshPage() { }
    }
}
