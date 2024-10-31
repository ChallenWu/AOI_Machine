using Demo.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class MenuButton : UserControlBase
    {
        public event EventHandler OnClicked;
        //private Color colorSelected = Color.FromArgb(0xAE, 0xDA, 0x97);
        private Color colorSelected = Color.FromArgb(0x66, 0xD9, 0x38);
        private Color colorUnselected = Color.FromArgb(0xEA, 0xEA, 0xEB);
        //private Color colorUnselected = Color.FromArgb(0x6F, 0x79, 0x80);
        private bool selected;
        public MenuButton()
        {
            InitializeComponent();
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.BackColor = colorUnselected;
            this.BackgroundImageLayout = ImageLayout.Center;
            this.Click += new EventHandler(MenuButton_Click);
        }

        public void SetBackColor(Color colorSelected, Color colorUnselected)
        {
            this.colorSelected = colorSelected;
            this.colorUnselected = colorUnselected;
        }
        public void IsSelect(bool isSelect)
        {
            Selected = isSelect;

        }
        public bool Selected
        {
            get { return this.selected; }
            set
            {
                if (value == selected)
                {
                    return;
                }
                this.selected = value;

                this.BackColor = selected ? colorSelected : colorUnselected;
            }
        }
        private void MenuButton_Click(object sender, EventArgs e)
        {
            Selected = !this.selected;
            if (OnClicked != null)
            {
                OnClicked(sender, e);
            }

        }
    }
}
