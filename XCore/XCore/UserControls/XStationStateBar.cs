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
                    button1.Text = MultiLanguage.GetMessage("ESTOP PRESS")+">>>";
                    break;
                case XStationState.ALARM:
                    button1.Text = MultiLanguage.GetMessage("ERROR")+">>>"+MultiLanguage.GetMessage("WAIT RESET ERROR");
                    break;
                case XStationState.PAUSE:
                    button1.Text = MultiLanguage.GetMessage("PAUSE")+">>>"+MultiLanguage.GetMessage("WAIT RUN");
                    break;
                case XStationState.RESETING:
                    button1.Text = MultiLanguage.GetMessage("RESETTING")+">>>";
                    break;
                case XStationState.RUNNING:
                    button1.Text = MultiLanguage.GetMessage("RUNNING")+">>>";
                    break;
                case XStationState.STOP:
                    button1.Text = MultiLanguage.GetMessage("STOP")+">>>"+MultiLanguage.GetMessage("WAIT RUN");
                    break;
                case XStationState.WAITRESET:
                    button1.Text = ">>>"+MultiLanguage.GetMessage("WAIT RESET");
                    break;
                case XStationState.WAITRUN:
                    button1.Text = ">>>"+MultiLanguage.GetMessage("WAIT RUN");
                    break;
            }
        }
        
    }
}
