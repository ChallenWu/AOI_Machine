using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCPLib;
using System.Threading;

namespace Demo.Device
{
    sealed class Mid_Light_TCP
    {
        public SimpleTcpClient simpleTcpClient2;
        private readonly static Mid_Light_TCP instance = new Mid_Light_TCP();

        private Mid_Light_TCP()
        {
            simpleTcpClient2 = new SimpleTcpClient();
            simpleTcpClient2.DataReceived += SimpleTcpClient1_DataReceived;
            RecData = null;
            RecDatas = null;
        }


        public static Mid_Light_TCP Instance
        {
            get { return instance; }
        }

        public void Connect()
        {
            try
            {
                if (!simpleTcpClient2.Connected)
                {
                    simpleTcpClient2.Connect(Globals.SettingParameter.Light_IP, Globals.SettingParameter.Light_Port);
                    //simpleTcpClient2.Connect("192.168.1.100", 6000);
                }
            }
            catch
            {

            }
        }

        public void DisConnect()
        {
            simpleTcpClient2.Disconnect();
        }

        public bool Connected
        {
            get { return this.simpleTcpClient2.Connected; }
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

        public void WriteCmd(string cmd, ushort duration)
        {
            lock (mutex)
            {
                if (duration > 0)
                {
                    Thread.Sleep(duration);
                }
                if (simpleTcpClient2.Connected == false)
                {

                    return;
                }

                RecData = null;
                RecDatas = null;
                are_DataRecerveDone = new AutoResetEvent(false);
                simpleTcpClient2.Write(cmd);
                Thread.Sleep(10);
            }
        }
        public void WriteCmd(byte[] cmd, ushort duration)
        {
            lock (mutex)
            {
                if (duration > 0)
                {
                    Thread.Sleep(duration);
                }
                if (simpleTcpClient2.Connected == false)
                {

                    return;
                }

                RecData = null;
                RecDatas = null;
                are_DataRecerveDone = new AutoResetEvent(false);
                simpleTcpClient2.Write(cmd);
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
