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
    class ICW_Scanner
    {
        /// <summary>
        /// using to connect ICW Scanner by Serial Port
        /// </summary>
        #region 单例
        private static ICW_Scanner _instance = null;
        private static readonly object olock = new object();
        public bool IsConnected = false;
        public static ICW_Scanner Instance
        {
            get
            {
                lock (olock)
                {
                    if (_instance == null)
                    {
                        _instance = new ICW_Scanner();
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

                port = new SerialPort();
                port.PortName = "COM3";
                port.BaudRate = 115200;
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
                MessageBox.Show("Not connect Serial Port");
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
            DebugDlg.Instance.UpdateUIText(StrData);
        }
        public string ReadQR()
        {
            lock(olock)
            {
                string ret = "";
                StrData = "";
                port.DiscardInBuffer();
                port.DiscardOutBuffer();
                //cmd send to scanner
                byte[] sentData = { 0x02, 0xF4, 0x03 };
                //write cmd to scanner
                port.Write(sentData, 0, sentData.Length);

                DateTime dt = DateTime.Now;
                TimeSpan ts;
                int timeOutMs = 5 * 1000;

                while (true)
                {
                    ret = StrData;
                    ts = new TimeSpan(DateTime.Now.Ticks - dt.Ticks);
                    if (ts.TotalMilliseconds > timeOutMs)
                    {
                        return ret;
                    }
                    if (ret != "")
                    {
                        return ret;
                    }
                    System.Threading.Thread.Sleep(10);
                }
            
            }
        } 
    }
}
