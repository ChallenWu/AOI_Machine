using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPOI.SS.Formula.Functions;
using System.IO;
using System.Xml.Serialization;

namespace XCore
{
    public partial class XSettingGrid : UserControl
    {
        private int id = 1;
        private bool locked = false;

        public event EventHandler SaveOkEventHandler;
        public XSettingGrid()
        {
            InitializeComponent();
        }
        public bool Locked
        {
            get { return this.locked; }
            set { this.locked = value; }
        }
        public int Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
                this.propertyGrid1.SelectedObject = XSettingManager.Instance.FindSettingById(this.id);
                if (XSettingManager.Instance.FindSettingById(this.id) != null)
                {
                    this.toolStripLabel1.Text = XSettingManager.Instance.FindSettingById(this.id).Name;
                }
            }
        }
        public void SelectedObject<T>(T Object)
        {
            this.propertyGrid1.SelectedObject = Object;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }
            if (XMachine.Instance.MachineMode != MachineModeType.Engineering)
            {
                MessageBox.Show("Engineer mới có quyền lưu cài đặt\r\nĐăng nhập với quyền Engineer", "Fail");
                return;
            }
            if (MessageBox.Show(this, "Save[" + XSettingManager.Instance.FindSettingById(this.id).Name + "]？", "Confirm",
                  MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                XSettingManager.Instance.FindSettingById(this.id).SaveSetting();
                MessageBox.Show("Save new Setting");
                if (SaveOkEventHandler != null)
                    SaveOkEventHandler(sender, e);
            }
        }
    }
}
