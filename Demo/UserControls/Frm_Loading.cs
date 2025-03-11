using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VM_Pro
{
    public partial class Frm_Loading : Form
    {
        public Frm_Loading()
        {
            InitializeComponent();
            this.TopLevel = true;
        }

        /// <summary>
        /// Form Instance
        /// </summary>
        private static Frm_Loading _instance;
        internal static Frm_Loading Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Frm_Loading();
                return _instance;
            }
        }


        private void Frm_Loading_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void uiProgressIndicator1_Click(object sender, EventArgs e)
        {

        }
    }
}
