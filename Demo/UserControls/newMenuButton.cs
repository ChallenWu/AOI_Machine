using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.UserControls
{
    public partial class newMenuButton : UserControlBase
    {

        public event EventHandler OnClicked;
        private Color colorSelected = Color.FromArgb(0x66, 0xD9, 0x38);
        private Color colorUnselected = Color.FromArgb(0xEA, 0xEA, 0xEB);
        private bool selected;

        public newMenuButton()
        {
            InitializeComponent();
            this.BackColor = colorUnselected;
            this.Click += MenuButton_Click;
            this.label1.Click += MenuButton_Click;
            this.pictureBox1.Click += MenuButton_Click;
        }
        private void MenuButton_Click(object sender, EventArgs e)
        {
            Selected = !this.selected;
            ///Luôn truyền `this` là `newMenuButton` làm `sender`
            OnClicked?.Invoke(this, e);

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


        private Image imageShow;
        public Image ImageShow
        {
            get
            {
                return imageShow;
            }
            set
            {
                imageShow = value;
                pictureBox1.Image = imageShow;
            }
        }
        private string textButtton;
        public string TextButtton
        {
            get => textButtton;  
            set
                {
                textButtton = value;
                label1.Text = textButtton;
            } 
        }

    }

}
