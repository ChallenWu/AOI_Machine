using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XCore
{
    public partial class BzMessagebox : Form
    {
        public BzMessagebox(string msg, string formStr, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon error)
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            ShowForm(msg, formStr, buttons, error);
        }

        private void ShowForm(string msg, string formStr, System.Windows.Forms.MessageBoxButtons buttons, System.Windows.Forms.MessageBoxIcon error)
        {
            this.Text = formStr;

            buttonOK.Text = "Confirm";
            buttonCancel.Text = "Cancel";

            if (buttons == MessageBoxButtons.OK)
            {
                buttonOK.Visible = true;
                buttonCancel.Visible = false;
                buttonOK.Left = this.Width / 2 - buttonOK.Width / 2;
            }
            else
            {
                buttonOK.Visible = true;
                buttonCancel.Visible = true;
            }

            if(error == MessageBoxIcon.Question)
            {
                pictureBox1.Image = global::XCore.Properties.Resources.question;

            }
            else if (error == MessageBoxIcon.Warning)
            {
                pictureBox1.Image = global::XCore.Properties.Resources.info;

            }
            else if (error == MessageBoxIcon.Error)
            {
                pictureBox1.Image = global::XCore.Properties.Resources.error;
            }

            richTextBox1.Text = msg;

        }

        public static DialogResult Show(IWin32Window owner, string msg, string formStr = "错误", System.Windows.Forms.MessageBoxButtons buttons = MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon error = System.Windows.Forms.MessageBoxIcon.Error)
        {
            BzMessagebox messageBox = new BzMessagebox(msg, MultiLanguage.GetMessage(formStr), buttons, error);
            messageBox.Owner = owner as Form;
            messageBox.ShowDialog();
            return messageBox.DialogResult;
        }

        public static DialogResult Show(string msg, string formStr = "错误", System.Windows.Forms.MessageBoxButtons buttons = MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon error = System.Windows.Forms.MessageBoxIcon.Error)
        {
            BzMessagebox messageBox = new BzMessagebox(msg, MultiLanguage.GetMessage(formStr), buttons, error);
            messageBox.ShowDialog();
            return messageBox.DialogResult;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
