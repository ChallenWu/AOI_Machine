using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionTools;

namespace Demo.Page
{
    public partial class Edit_Vision : Form
    {
        public Edit_Vision()
        {
            InitializeComponent();
            
        }
        private Process process;

        public Process Process
        {
            get { return process; }
            set
            {
                process = value;
                processCreatorUI1.ProcessDisplay.Process = process;
                if (process != null)
                {
                    processCreatorUI1.ProcessName.Text = process.Path;
                }
            }
        }
        private static Edit_Vision instance;
        public static Edit_Vision Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new Edit_Vision();
                return instance;
            }
        }
    }
}
