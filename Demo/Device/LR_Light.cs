using Demo.Page;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Device
{
    class LR_Light
    {
        /// <summary>
        /// Control Light by Serial Port
        /// </summary>
        #region Singleton
        private static LR_Light _instance = null;
        private static readonly object olock = new object();
        public bool IsConnected = false;
        public static LR_Light Instance
        {
            get
            {
                lock (olock)
                {
                    if (_instance == null)
                    {
                        _instance = new LR_Light();
                    }
                }
                return _instance;
            }
        }
        #endregion
        SerialPort port;
        public string StrData { get => strData; set => strData = value; }

        private string strData = "";
        public bool Connect()
        {
            if (port == null)
            {
                //Setting para of Port
                port = new SerialPort();
                port.PortName = Globals.SettingParameter.Light_COM.ToString();
                port.BaudRate = 57600;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.DataBits = 8;
            }

            if (port.IsOpen)
            {
                return true;
            }
            try
            {
                port.Open();
                IsConnected = true;
                port.DataReceived += Port_DataReceived;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Serial Port : " + ex.ToString());
                return false;
            }
        }
        public bool Disconnect()
        {
            if (port.IsOpen && port!= null)
            {
                port.Close();
                return true;
            }
            return false;
        }
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            StrData = port.ReadLine();
        }
        public bool LightOn(string Sensitive)
        {
            lock (olock)
            {
                string ret = "";
                StrData = "";
                port.DiscardInBuffer();
                port.DiscardOutBuffer();
                //cmd send to scanner
                byte[] sentData = { 0x02, 0xF4, 0x03 };
                //Send Light sensitive
                var LightSensitive = $"IDC,SPL,1,{Sensitive},\r\n";
                //Turn on Light
                var cmdOn = "IDC,OPS,1,\r\n";
                //write down cmd to scanner
                port.Write(LightSensitive);
                Thread.Sleep(10);
                port.Write(cmdOn);
                return true;
            }
        }
        public bool LightOff()
        {
            lock (olock)
            {
                StrData = "";
                port.DiscardInBuffer();
                port.DiscardOutBuffer();
                //cmd send to scanner
                byte[] sentData = { 0x02, 0xF4, 0x03 };
                //Turn off Light
                var cmdOn = "IDC,CLS,1,\r\n";
                //write cmd to scanner
                port.Write(cmdOn);
                return true;
            }
        }
    }
}
