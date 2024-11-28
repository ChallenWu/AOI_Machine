using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OVisionPro
{
    public partial class ucPropertyGridBlockBasic : UserControl
    {
        public Dictionary<string, Dictionary<string, object>> myNestedDictionary; 
        private readonly static XParameterManager instance = new XParameterManager();
        public string funcID = "";
        private int taskId = 1;
        public int TaskId
        {
            get { return this.taskId; }
            set
            {
                cvX.onUpdateFrameToUIAction = LoadPicturePropertyGrid;
                this.taskId = value;
            }
        }
        //public static XParameterManager Instance
        //{
        //    get { return instance; }
        //}
        public Bitmap MatToBitmap(Mat mat)
        {
            // Convert từ Mat sang Bitmap
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
        }
        public Dictionary<string, object> LoadDataPropertyGrid(Dictionary<string, Dictionary<string, object>> dictParams)
        {
            Dictionary<string, object> keyValuePairs = new Dictionary<string, object>() { };
            foreach (var keyVa in dictParams.Keys)
            {
                keyValuePairs.Add(keyVa, dictParams[keyVa]);
            }
            return keyValuePairs;
        }
        public ucPropertyGridBlockBasic()
        {
            InitializeComponent();
            XObjectCVParameters obj = new XObjectCVParameters();
            XObjectCVParameters config = new XObjectCVParameters();

            myNestedDictionary = new Dictionary<string, Dictionary<string, object>>
            {
                {
                    "Threshold_0_k01", new Dictionary<string, object>
                    {
                        { "thresh", 125 },
                        { "maxVal", 255 }
                    }
                },
                {
                    "Threshold_0_k02", new Dictionary<string, object>
                    {
                        { "thresh", new Dictionary<string, object>
                            {
                                { "abc", "2s" }
                            }
                        },
                        { "maxVal", 255 }
                    }
                },
                {
                    "Threshold_0_k03", new Dictionary<string, object>
                    {
                        { "thresh", 125 },
                        { "maxVal", 255 }
                    }
                }
            };
            Dictionary<string, object> keyValuePairs = LoadDataPropertyGrid(myNestedDictionary);    
            NestedDictionaryWrapper wrapper = new NestedDictionaryWrapper(keyValuePairs);
            XObjectCVParameters dataProperty = new XObjectCVParameters()
            {
                posY = 1,
                posX = 11,
                Parameters=wrapper
            };


            //person.Address.Dictionary.
            propertyGrid1.SelectedObject = XParameterManager.Instance.propertyGridData;
            //propertyGrid1.SelectedObject = myNestedDictionary;
        }

        public void LoadPicturePropertyGrid(OpenCvSharp.Mat frame)
        {
            pictureBox1.Image = MatToBitmap(frame);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var sss = myNestedDictionary;
            propertyGrid1.Refresh();
        }
    }
    
}

