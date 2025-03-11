using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Demo.Device
{
    class ScannerComm
    {
        #region initial variables
        private const int read_timeout = 1000;
        private SerialPort serialPort;
        private byte[] readBuf = new byte[1];
        // private byte[] readBuf = new byte[1];
        private volatile bool isReading = false;
        private volatile List<byte> readingBuf;
        public bool Connected = false;
        //Tao su kien
        public delegate void RxDataHandler(byte rx);
        public event RxDataHandler DataReceived;

        public string ret { get; set; }
        #endregion

        #region 单例
        private static ScannerComm _instance = null;
        private static readonly object olock = new object();
        public static ScannerComm Instance
        {
            get
            {
                lock (olock)
                {
                    if (_instance == null)
                    {
                        _instance = new ScannerComm("1111",null);
                    }
                }

                return _instance;
            }
        }
        #endregion

        public ScannerComm(string comPort, RxDataHandler callback)
        {
            try
            {
                this.serialPort = new SerialPort(comPort);
                serialPort.BaudRate = 115200;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Parity = Parity.None;
                this.DataReceived = callback;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void Start()
        {
            try
            {
                if (!this.serialPort.IsOpen)
                {
                    serialPort.Open();
                    Connected = true;
                    this.serialPort.BaseStream.BeginRead(this.readBuf, 0, 1, new AsyncCallback(this.readCallback), this.serialPort);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }

        private void readCallback(IAsyncResult ar)
        {
            try
            {
                var port = (SerialPort)ar.AsyncState;
                if (!port.IsOpen)
                {
                    return;
                }
                int rxCnt = port.BaseStream.EndRead(ar);
                if (rxCnt == 1)
                {
                    byte rx = this.readBuf[0];

                    //update reading buffer
                    if (isReading)
                    {
                        //Neu ket thuc chuoi la 0x0d                        
                        if (rx == 0x0A)
                        {
                            isReading = false;// n
                        }
                        else
                        {
                            this.readingBuf.Add(rx);
                        }
                    }
                    if (this.DataReceived != null)
                    {
                        this.DataReceived(rx);
                    }
                }
                //Continue reading
                port.BaseStream.BeginRead(this.readBuf, 0, 1, new AsyncCallback(this.readCallback), port);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        public void Stop()
        {
            try
            {
                if (this.serialPort != null && this.serialPort.IsOpen)
                {
                    Connected = false;
                    this.serialPort.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public string ReadQR()
        {
           try
            {
                ret = "";
                isReading = true;
                this.readingBuf = new List<byte>();                
                byte[] cmd = { 0x02, 0xF4, 0x03 };
                this.serialPort.Write(cmd, 0, cmd.Length);

                for (int i = 0; i < read_timeout / 10; i++)
                {
                    if (!isReading)
                    {
                        break;
                    }
                    Thread.Sleep(10);
                }
                if (!isReading)
                {
                    ret = Encoding.UTF8.GetString(readingBuf.ToArray());
                }
                else
                {
                    //finish reading
                    isReading = true;
                    for (int i = 0; i < read_timeout / 10; i++)
                    {
                        if (!isReading)
                        {
                            break;
                        }
                        Thread.Sleep(10);
                    }
                    if (!isReading)
                    {
                        ret = ASCIIEncoding.ASCII.GetString(this.readingBuf.ToArray());
                    }
                }
                // Check error:
                if (ret == null)
                {
                    ret = "";
                }
                return ret;
            }
            catch(Exception e)
            {
                return "The Port is Closed";
            }
        }
    }
}
