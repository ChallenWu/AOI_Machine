using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro
{
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
        }
        public void Settext(string msg)
        {
            txtMsg.Text = msg;
        }

        public CustomMessageBox(String message)
        {
            InitializeComponent();
            txtMsg.Text = message.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
