using AutoStudio.Forms;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XCore;

namespace Demo.Device
{
     class PublicPLCData
    {
        #region 单例
        private static PublicPLCData _instance = null;
        private static readonly object olock = new object();
        public static PublicPLCData Instance
        {
            get
            {
                lock (olock)
                {
                    if (_instance == null)
                    {
                        _instance = new PublicPLCData();
                    }
                }

                return _instance;
            }
        }
        #endregion

        public ModbusApiH5U ModbusApiH5U = new ModbusApiH5U();
        public const int triggerRegister_D = 10;
        public bool scanSN = true;
        public const int result_D = 12;
        public int result { get; set; }
        Thread _thread;
        public void Start()
        {
            Stop();
            _thread = new Thread(new ThreadStart(ThreadGetPlcData));
            _thread.IsBackground = true;    
            _thread.Start();
        }
        public void Stop()
        {
            if (_thread != null)
            {
                _thread.Abort();
            }
        }
        public void ThreadGetPlcData()
        {
            while (true)
            { if(!GetPlcData())
                {
                    return;
                }
                Thread.Sleep(1000);
            }
        }
        private bool GetPlcData()
        {
            if(Connect() != true)
            {
                BzMessagebox.Show("Connect Error");
            }
            else
            {
                 result = ModbusApiH5U.ReadValueInt16(SoftElemType.REGI_H5U_D, (int)triggerRegister_D).Result;
            }
            return true;
        }
        bool Connect()
        {
            try
            {
                if(ModbusApiH5U.IsConnected)
                {
                    return true;
                }

                if (ModbusApiH5U.PLCConnect(Globals.SettingICT.PLC_IP, 1))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
