using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XCore
{
    public partial class XStationStateBar : UserControl
    {
        private Color Red = Color.FromArgb(0xC8, 0x25, 0x06);
        private Color Green = Color.FromArgb(0xAE, 0xDA, 0x97);
        private int stationId = 1;
        public XStationStateBar()
        {
            InitializeComponent();
        }
        public int StationId
        {
            get { return stationId; }
            set
            {
                stationId = value;
                if (XStationManager.Instance.FindStationById(stationId) == null)
                {
                    return;
                }
                XStationManager.Instance.Stations[stationId].RedLightON += new Action(BeginRedOn);
                XStationManager.Instance.Stations[stationId].OrangeLightON += new Action(BeginOrangeOn);
                XStationManager.Instance.Stations[stationId].GreenLightON += new Action(BeginGreenOn);
                XStationManager.Instance.Stations[stationId].AllLightsOFF += new Action(BeginOff);
                XStationManager.Instance.Stations[stationId].OnStationStateChanged += new Action<XStationState>(BeginChangeText);
            }
        }

        private void BeginRedOn()
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action(RedOn));
                }
            }
            catch
            {

            }
        }

        private void BeginOrangeOn()
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action(OrangeOn));
                }
            }
            catch
            {

            }
        }

        private void BeginGreenOn()
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action(GreenOn));
                }
            }
            catch
            {

            }
        }

        private void BeginOff()
        {
            try
            {
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action(Off));
                }
            }
            catch
            {

            }
        }

        private void BeginChangeText(XStationState sts)
        {
            try
            {
                if (this.IsHandleCreated && (!this.IsDisposed))
                {
                    this.BeginInvoke(new Action<XStationState>(ChangeText), new object[] { sts });
                }
            }
            catch
            {

            }
        }

        private void RedOn()
        {
            this.button1.BackColor = Red;
        }

        private void OrangeOn()
        {
            this.button1.BackColor = Color.Orange;
        }

        private void GreenOn()
        {
            this.button1.BackColor = Green;
        }

        private void Off()
        {
            this.button1.BackColor = Color.White;
        }

        private void ChangeText(XStationState sts)
        {
            switch (sts)
            { 
                case XStationState.ESTOP:
                    button1.Text = MultiLanguage.GetMessage("急停按下")+">>>";
                    break;
                case XStationState.ALARM:
                    button1.Text = MultiLanguage.GetMessage("发现报警")+">>>"+MultiLanguage.GetMessage("等待复位");
                    break;
                case XStationState.PAUSE:
                    button1.Text = MultiLanguage.GetMessage("暂停中")+">>>"+MultiLanguage.GetMessage("等待运行");
                    break;
                case XStationState.RESETING:
                    button1.Text = MultiLanguage.GetMessage("复位中")+">>>";
                    break;
                case XStationState.RUNNING:
                    button1.Text = MultiLanguage.GetMessage("运行中")+">>>";
                    break;
                case XStationState.STOP:
                    button1.Text = MultiLanguage.GetMessage("停止")+">>>"+MultiLanguage.GetMessage("等待复位");
                    break;
                case XStationState.WAITRESET:
                    button1.Text = ">>>"+MultiLanguage.GetMessage("等待复位");
                    break;
                case XStationState.WAITRUN:
                    button1.Text = ">>>"+MultiLanguage.GetMessage("等待运行");
                    break;
            }
        }
        
    }
}
