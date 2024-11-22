using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCore.UserControls
{
    public partial class XPositionModelChange : UserControl
    {

        private int taskId = 1;
        public XPositionModelChange()
        {
            InitializeComponent();
        }
        public int TaskId
        {
            get { return this.taskId; }
            set
            {
                this.taskId = value;
                if (XTaskManager.Instance.FindTaskById(taskId) != null)
                {
                    // this.toolStripStatusLabel1.Text = XTaskManager.Instance.FindTaskById(taskId).Name;
                    XModelManager.Instance.modelname_realtime.CollectionChanged += update_cbb_CollectionChanged;
                    update_cbb();
                }
            }
        }

        private void update_cbb_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // Cập nhật ComboBox khi danh sách thay đổi
            update_cbb();
        }

        public void update_cbb()
        {
            cbbModelName.Items.Clear();
            foreach (string modelName in XModelManager.Instance.nameMap.Keys)
            {
                cbbModelName.Items.Add(modelName);
            }
            if(XModelManager.Instance.nameMap.Keys.Count > 0)
            {
                cbbModelName.SelectedIndex = 0;
            }    

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
