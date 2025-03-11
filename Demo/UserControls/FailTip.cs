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
using Sunny.UI;

namespace HB_IWatch
{
    public partial class FailTip : UIForm
    {
        private Color Red = Color.FromArgb(0xC8, 0x25, 0x06);
        private Color Green = Color.FromArgb(0xAE, 0xDA, 0x97);
        private AutoResetEvent _are = new AutoResetEvent(false);
        private System.Timers.Timer m_Timer = null;
        private bool unnaturalClosed = true;

        public FailTip(string message, bool IsCancelVisable = true, bool IsIgnoreVisible = false, int timeout = -1, bool IsokVisable = true)
        {
            InitializeComponent();
            var content = message.Split(':');
            if (message.Contains(XAlarmReporter.Instance.SystemAlarms[XAlarmId.DOOR_OPEN].Description))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Top = 100;
                this.Left = 550;
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.ControlBox = false;

            this.tbStartTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:f");
            this.button1.BackColor = Green;
            this.BackColor = Red;
            this.tbErrorCode.Text = content[0];
            this.richTextBox1.Text = content[1];
            this.richTextBox2.Text = content[2];
            this.button1.Visible = IsokVisable;
            this.button2.Visible = IsCancelVisable;
            this.button3.Visible = IsIgnoreVisible;

            //Bật còi, báo lỗi về plc
            //PC > PLC : ghi bit alarm.

            //if (Globals.SettingOption.是否开启蜂鸣器)
            //{
            //    //if (XDevice.Instance.FindDoById((int)DoId.蜂鸣).STS == false)
            //    {
            //        XDevice.Instance.FindDoById((int)DoId.蜂鸣).SetDo(DOSTSTYPE.HIGH);
            //        Thread.Sleep(500);
            //        XDevice.Instance.FindDoById((int)DoId.蜂鸣).SetDo(DOSTSTYPE.LOW);
            //    }
            //}

            if (timeout > 0)
            {
                m_Timer = new System.Timers.Timer();
                m_Timer.Interval = timeout;
                m_Timer.Elapsed += m_Timer_Elapsed;
                m_Timer.Start();
            }
        }

        //Tiếp tục
        private void button1_Click(object sender, EventArgs e)
        {
            //this.DialogResult = System.Windows.Forms.DialogResult.OK;
            //this.unnaturalClosed = false;
            //this._are.Set();
            //this.Close();
            SafeCloseDialog(DialogResult.OK);
        }

        //Cancel
        private void button2_Click(object sender, EventArgs e)
        {
            //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            //this.unnaturalClosed = false;
            //this._are.Set();
            //this.Close();
            SafeCloseDialog(DialogResult.Cancel);
        }

        public void SafeCloseDialog(DialogResult result)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    this.DialogResult = result; // Thiết lập kết quả
                    this.unnaturalClosed = false;
                    this._are.Set();
                    this.Close(); // Đóng dialog
                }));
            }
            else
            {
                this.DialogResult = result; // Thiết lập kết quả
                this.unnaturalClosed = false;
                this._are.Set();
                this.Close(); // Đóng dialog
            }
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
        public void SetBackColor(Color color)
        {
            this.BackColor = color;
        }
        public void SelectAll()
        {
            this.richTextBox1.SelectAll();
        }

        public string GetText()
        {
            return this.richTextBox1.Text;
        }
        //Next
        private void button3_Click(object sender, EventArgs e)
        {
            //this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            //this.unnaturalClosed = false;
            //this._are.Set();
            //this.Close();
            SafeCloseDialog(DialogResult.Ignore);
        }

        private void m_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new System.Timers.ElapsedEventHandler(m_Timer_Elapsed), new object[] { sender, e });
                    return;
                }
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                this.unnaturalClosed = false;
                this._are.Set();

                this.Close();
            }
            catch (Exception ex)
            {
                throw (new Exception("FailTip Trigger Exception:" + ex.Message));
            }
        }

        private void FailTip_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (unnaturalClosed)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this._are.Set();
                this.Close();
            }
        }

        private void FailTip_Shown(object sender, EventArgs e)
        {
            this.unnaturalClosed = true;
        }
    }
}
