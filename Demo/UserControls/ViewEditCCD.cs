using Demo;
using Demo.Page;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionTools;
using VisionTools.Tools.ImageFile;
using VisionTools.Tools.TotalGraphic;
using XCore;

namespace PasteLabelMachine.Controls
{
    public partial class ViewEditCCD : UserControl
    {
        public ViewEditCCD()
        {
            InitializeComponent();
        }
        private VisionTools.Process process;

        public VisionTools.Process Process
        {
            get { return process; }
            set 
            { 
                process = value;

            }
        }

        private void btn_EditTool_Click(object sender, EventArgs e)
        {
            Edit_Vision.Instance.Process = process;
            Edit_Vision.Instance.ShowDialog (); 
        }

        private void cb_Tool_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string SelectedItem = cb_Tool.SelectedItem.ToString();
                if(Globals.ListProcess.Exists(x=>x.Name == SelectedItem))
                {
                    Process = Globals.ListProcess.Find(x => x.Name == SelectedItem);
                    Edit_Vision.Instance.Process = Process;
                }
            }
            catch (Exception ex)
            {
                frmMessageBox.Show(EMessageIcon.Error,"Change Vision Job File Fail " + ex.Message);
            }
        }

        private void btn_EditWithCurentImage_Click(object sender, EventArgs e)
        {
            Edit_Vision.Instance.Process = process;
            if(process.Tools.Exists(x=>x.Name == "Image Load 0"))
            {
                ImageLoad imageLoad = process.Tools.Find(x=>x.Name == "Image Load 0") as ImageLoad;
                imageLoad.InImage = new VisionTools.Image(displayViewInteract1.Image.Mat.Clone());
                //imageLoad.Run();
            }
            //Edit_Vision.Instance.Show();
        }

        private void btn_RunTool_Click(object sender, EventArgs e)
        {
            try
            {
                Edit_Vision.Instance.processCreatorUI1.RunTool();
                OutputImage outputImage = process.Tools.Find(x => x.Name == "OutputImage 0") as OutputImage;
                if (outputImage.ImageList[0].Image.Width == 0 || outputImage.ImageList[0].Image.Height == 0)
                {
                    BzMessagebox.Show("input image is null");
                    return;
                }
                displayViewInteract1.Image = outputImage.ImageList[0].Image;
                displayViewInteract1.NotifyDrawing = Edit_Vision.Instance.processCreatorUI1.GetOutputGraphic("OutputImage 0");
            }
            catch (Exception)
            {
                MessageBox.Show("Run Tool Fail");
            }


        }
    }
}
