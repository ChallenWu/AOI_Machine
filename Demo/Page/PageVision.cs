using BoTech;
using Demo.UserControls;
using Models;
using NPOI.HSSF.Record.PivotTable;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionTools;
using VisionTools.Tools;
using VisionTools.Tools.ImageFile;
using VisionTools.Tools.TotalGraphic;
namespace Demo.Page
{
    public partial class PageVision : UserControlBase
    {
        //public static ImageToolC9200 C9200;
        public PageVision()
        {
            InitializeComponent();           
            VisionModel();
        }

        public void VisionModel()
        {
           
        }
        static PageVision instance;
        public static PageVision Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                    instance = new PageVision();
                return instance;
            }
        }
    }
}
