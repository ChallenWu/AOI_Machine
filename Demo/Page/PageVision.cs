//using AutoStudio.Core.Views.Regions;
using BoTech;
using Demo.UserControls;
using NPOI.HSSF.Record.PivotTable;
using OVisionPro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Page
{
    public partial class PageVision : UserControlBase
    {
        public PageVision()
        {
            InitializeComponent();
           
        }

        private static PageVision instance;
        public static PageVision Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new PageVision();
                return instance;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            XVisionManager.Instance.StartWorkInTask("Model1", 0);
        }
    }
}
