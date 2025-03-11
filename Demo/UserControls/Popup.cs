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
using Sunny.UI;
using TCPLib;

namespace HB_IWatch
{
    public partial class Popup : Form
    {
        private System.Windows.Forms.Timer timer;
        private Label messageLabel;

        public Popup(string message, int timeout = 5000)
        {
            InitializeComponent();

            this.Size = new System.Drawing.Size(300, 150);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;

            // Thêm nhãn hiển thị thông báo
            messageLabel = new Label()
            {
                Text = message,
                AutoSize = false,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            this.Controls.Add(messageLabel);

            // Thiết lập Timer
            timer = new System.Windows.Forms.Timer();
            timer.Interval = timeout; 
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                this.Close();
            };
            timer.Start();
        }
    }
}
