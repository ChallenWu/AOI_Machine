using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using TCPLib;
using System.Diagnostics;
using System.Threading;
using Demo;
using System.Windows.Forms;

namespace HB_IWatch
{
    sealed class KeyenceService
    {
        public SimpleTcpClient simpleTcpClient;
        private readonly static KeyenceService instance = new KeyenceService();

        private KeyenceService()
        {
            simpleTcpClient = new SimpleTcpClient();
            simpleTcpClient.DataReceived += simpleTcpClient_DataReceived;
            RecData = null;
            RecDatas = null;
        }

        public static KeyenceService Instance 
        {
            get { return instance; }
        }

        public void Connect()
        {
            //try
            //{
            //  simpleTcpClient.Connect(Globals.SettingParameter.CCD_IP, Globals.SettingParameter.CCD_Port);
            //}
            //catch
            //{

            //}
        }

        public void DisConnect()
        {
            simpleTcpClient.Disconnect();
        }

        public bool Connected
        {
            get { return this.simpleTcpClient.Connected; }
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
                if (simpleTcpClient.Connected == false)
                {

                    return;
                }

                RecData = null;
                RecDatas = null;
                are_DataRecerveDone = new AutoResetEvent(false);
                simpleTcpClient.Write(cmd + "\r");
            }
        }

        private void simpleTcpClient_DataReceived(object sender, TCPLib.Message message)
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
