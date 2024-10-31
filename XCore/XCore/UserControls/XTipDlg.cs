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
    public partial class XTipDlg : Form
    {
        public string output;
        public string m_Tip;
        public XTipDlg(string str)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;    
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.m_Tip = str;
            this.Text = m_Tip;
            textBox1.Text = m_Tip;
            textBox1.SelectAll();
            this.AcceptButton = button1;
            this.output = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.output = textBox1.Text;
            if (output == m_Tip)
            {
                this.Close();
                return;
            }
            else
            {
                if (output.Contains(','))
                {
                    string[] input = output.Split(',');

                    input[0].Trim();
                    if (input.Length != 2 || input[0] == "")
                    {
                        BzMessagebox.Show(MultiLanguage.GetMessage("输入格式不正确") + ","
                            + MultiLanguage.GetMessage("请重新输入") + "!");//"输入格式不正确，请重新输入!");
                        return;
                    }                   
                }
                else
                {
                    output.Trim();
                    if (output == "")
                    {
                        BzMessagebox.Show(MultiLanguage.GetMessage("输入格式不正确") + ","
                            + MultiLanguage.GetMessage("请重新输入") + "!");//"输入格式不正确，请重新输入!");
                        return;
                    } 
                }
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
