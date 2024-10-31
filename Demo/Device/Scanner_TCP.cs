using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TCPLib;

namespace Demo.Device
{
    sealed class Scanner_TCP
    {
        public SimpleTcpClient simpleTcpClient1;
        private readonly static Scanner_TCP instance = new Scanner_TCP();

        private Scanner_TCP()
        {
            simpleTcpClient1 = new SimpleTcpClient();
            simpleTcpClient1.DataReceived += SimpleTcpClient1_DataReceived;
            RecData = null;
            RecDatas = null;
        }


        public static Scanner_TCP Instance
        {
            get { return instance; }
        }

        public void Connect()
        {
            try
            {
                simpleTcpClient1.Connect(Globals.SettingICT.ScanLead_IP, Globals.SettingICT.ScanLead_Port);
            }
            catch
            {

            }
        }

        public void DisConnect()
        {
            simpleTcpClient1.Disconnect();
        }

        public bool Connected
        {
            get { return this.simpleTcpClient1.Connected; }
        }

        public string RecData
        {
            get;
            set;
        }

        public string[] RecDatas
        {
            get;
            set;
        }

        public AutoResetEvent are_DataRecerveDone = new AutoResetEvent(false);

        private object mutex = new object();
        public void WriteCmd(byte[] cmd, ushort duration)
        {
            lock (mutex)
            {
                if (duration > 0)
                {
                    Thread.Sleep(duration);
                }
                if (simpleTcpClient1.Connected == false)
                {

                    return;
                }

                RecData = null;
                RecDatas = null;
                are_DataRecerveDone = new AutoResetEvent(false);
                simpleTcpClient1.Write(cmd);
            }
        }

        private void SimpleTcpClient1_DataReceived(object sender, TCPLib.Message message)
        {
            string result = message.MessageString;

            try
            {
                int pos = result.IndexOf("\r");
                result = result.Substring(0, pos);
                if (result.Contains(','))
                {
                    RecDatas = result.Split(',');
                    RecData = result;
                }
                else
                {
                    RecData = result;
                }

                are_DataRecerveDone.Set();
            }
            catch
            {
                RecData = null;
                RecDatas = null;
            }

        }
    }
}
