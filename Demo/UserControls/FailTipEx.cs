using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using XCore;
using Demo;

namespace HB_IWatch
{
    public partial class FailTipEx : Form
    {
        private Color Red = Color.FromArgb(0xC8, 0x25, 0x06);
        private Color Green = Color.FromArgb(0xAE, 0xDA, 0x97);
        private AutoResetEvent _are = new AutoResetEvent(false);
        private static int index = 0;
        private System.Timers.Timer m_Timer = null;
        //private int CallBackDiId = -1;

        private CallbackAction CallbackAct = null;

        public FailTipEx(string message, bool IsCancelVisable = true, bool IsIgnoreVisible = false, CallbackAction callBackAct = null)//int callBackDiId = -1)
        {
            index++;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.button1.BackColor = Green;
            this.BackColor = Red;
            this.richTextBox1.Text = message;

            this.button2.Visible = IsCancelVisable;
            this.button3.Visible = IsIgnoreVisible;

            if (Globals.SettingOption.是否开启蜂鸣器)
            {
                //if (XDevice.Instance.FindDoById((int)DoId.蜂鸣).STS == false)
                {
                    XDevice.Instance.FindDoById((int)DoId.蜂鸣).SetDo(DOSTSTYPE.HIGH);
                    Thread.Sleep(500);
                    XDevice.Instance.FindDoById((int)DoId.蜂鸣).SetDo(DOSTSTYPE.LOW);
                }
            }

            //CallBackDiId = callBackDiId;
            //if (CallBackDiId >= 0)
            CallbackAct = callBackAct;
            if (callBackAct != null)
            {
                m_Timer = new System.Timers.Timer();
                m_Timer.Interval = 300;
                m_Timer.Elapsed += m_Timer_Elapsed;
                m_Timer.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(button1_Click), new object[] { sender, e });
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this._are.Set();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._are.Set();
            this.Close();
        }

        public bool WaitOne()
        {
            return _are.WaitOne();
        }

        public void SetSubmitText(string text)
        {
            this.button1.Text = text;
        }

        public void SetCancelText(string text)
        {
            this.button2.Text = text;
        }

        public void SetIgnoreText(string text)
        {
            this.button3.Text = text;
        }

        public void SelectAll()
        {
            this.richTextBox1.SelectAll();
        }

        public string GetText()
        {
            return this.richTextBox1.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this._are.Set();
            this.Close();
        }

        private void m_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //DISTSTYPE sts = 0;
            //if (XDevice.Instance.FindDiById(CallBackDiId).GetDi(ref sts) == 0)
            //{
            //    if (sts == DISTSTYPE.HIGH)
            //    {
            //        if (InvokeRequired)
            //        {
            //            BeginInvoke(new EventHandler(button1_Click), new object[] { sender, e });
            //            return;
            //        }
            //    }
            //}
            try
            {
                if (CallbackAct != null)
                {
                    if (CallbackAct())
                    {
                        button1_Click(null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                throw (new Exception("FailTipEx Trigger Exception:" + ex.Message));
            }

        }
    }
}
