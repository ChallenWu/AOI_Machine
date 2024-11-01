using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Demo.Device;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Demo
{
    /// <summary>
    /// Only use for 3E Frame McProtocol
    /// </summary>
    public enum DevideCode
    {
        SM = 0x91,
        SD = 0xA9,
        X = 0x9C,
        Y = 0x9D,
        M = 0x90,
        L = 0x92,
        F = 0x93,
        B = 0xA0,
        D = 0xA8,
        W = 0xB4,
        Z = 0xCC,
        R = 0xAF,
    }
    class SLMP
    {
        #region Instance
        private static SLMP _instance = null;
        private static readonly object olock = new object();
        public static SLMP Instance
        {
            get
            {
                lock (olock)
                {
                    if (_instance == null)
                    {
                        _instance = new SLMP();
                    }
                }

                return _instance;
            }
        }
        #endregion
        Thread thread;
        private static readonly object obj = new object();
        #region Property
        private Socket sock;

        //Thông số của socket
        private string ipAddress;
        private int portNo;
        //MC
        private int networkNo;
        private int pcNo;
        private int stationNo;

        public string IpAddress { get => ipAddress; set => ipAddress = value; }
        public int PortNo { get => portNo; set => portNo = value; }
        public int NetworkNo { get => networkNo; set => networkNo = value; }
        public int PcNo { get => pcNo; set => pcNo = value; }
        public int StationNo { get => stationNo; set => stationNo = value; }

        #endregion

        #region Method
        //Hàm dựng
        public SLMP()
        {
            this.portNo = int.Parse(Globals.SettingICT.PLC_Port);
            this.ipAddress = Globals.SettingICT.PLC_IP;
            this.networkNo = 0x00;
            this.pcNo = 0xff;
            this.stationNo = 0x00;
        }
        #endregion
        public int Open()
        {
            int result = -1;
            //Kiểm tra xem cổng sock đã khởi tạo chưa
            if (sock == null)
            {
                sock = new Socket(SocketType.Stream, ProtocolType.Tcp);
            }
            if (sock.Connected)
            {
                result = 0;
                return result;
            }
            try
            {
                sock.Connect(this.ipAddress, this.portNo);
                if (sock.Connected)
                {
                    thread = new Thread(new ThreadStart(AlwaysRun));
                    thread.IsBackground = true;
                    thread.Start();
                    result = 0;
                }
            }
            catch (Exception e)
            {

            }
            return result;
        }

        public short D100;
        public short D110;
        public short D200;
        public short D210;

        public void AlwaysRun()
        {
            while(true)
            {
                //ReadWord(DevideCode.D, 100, out D100);
                //ReadWord(DevideCode.D, 110, out D110);
                //ReadWord(DevideCode.D, 200, out D200);
                //ReadWord(DevideCode.D, 210, out D210);
                //Thread.Sleep(10);
            }
        }
        public int Open(string IP, int Port)
        {
            int result = -1;
            //Kiểm tra xem cổng sock đã khởi tạo chưa
            if (sock == null)
            {
                sock = new Socket(SocketType.Stream, ProtocolType.Tcp);
            }
            if (sock.Connected)
            {
                result = 0;
                return result;
            }
            try
            {
                sock.Connect(IP, Port);
                if (sock.Connected)
                {
                    result = 0;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return result;
        }
        public int Close()
        {
            int result = -1;
            if (sock != null)
            {
                result = 0;
                return result;
            }
            if (sock.Connected == false)
            {

                result = 0;
                return result;
            }
            sock.Disconnect(false);
            sock = null;
            result = 0;
            return result;
        }
        public int WriteWord(DevideCode _devCode, int _devNumber, short _shValue)
        {
            lock(obj)
            {
                //binary code
                int result = -1;
                if (sock == null)
                {
                    return result;
                }
                if (sock.Connected == false)
                {
                    return result;
                }
                //Chuẩn bị dữ liệu gửi xuống PLC
                List<byte> lstSendData = new List<byte>();
                //1.Sub header:5000  [2bytes]
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                //2.Access router 5 byte
                //2.1 Network number
                lstSendData.Add((byte)this.networkNo);
                //2.2 PC No
                lstSendData.Add((byte)this.pcNo);
                //2.3 Request IO No
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //2.4 Station No
                lstSendData.Add((byte)this.stationNo);

                //3.Request data lenght = [2bytes]
                //monitor timer + command + subcommand + request data
                lstSendData.Add(0x0E);
                lstSendData.Add(0x00);

                //4. Monitoring time : [2 byte] 9-10: 16x250 = 4s
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                //5.Request item
                //5.1 Command: Device Write
                lstSendData.Add(0x01);
                lstSendData.Add(0x14);
                //5.2.Sub Command: select Word  to write data
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);
                //5.3 Head Device No: Đọc thanh ghi số bao nhiêu : 3 byte
                int headNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    byte a = (byte)(headNumber >> 8 * i);
                    lstSendData.Add(a);
                }
                //5.4 Device Code
                lstSendData.Add((byte)_devCode);
                //5.5 Number of Device: Số lượng thanh ghi đọc: 1 thanh ghi
                int numberOfDevice = 1;
                for (int i = 0; i < 2; i++)
                {
                    byte a = (byte)(numberOfDevice >> 8 * i);
                    lstSendData.Add(a);
                }
                //5.6 Write Data: Ghi dữ liệu vào thanh ghi
                short value = _shValue;
                for (int i = 0; i < 2; i++)
                {
                    byte a = (byte)(value >> 8 * i);
                    lstSendData.Add(a);
                }
                //Tính số lượng byte gửi. Data Lenght
                int noOfDataByte = lstSendData.Count - 9;
                byte[] bytes = BitConverter.GetBytes(noOfDataByte);
                lstSendData[7] = bytes[0];
                lstSendData[8] = bytes[1];

                //Gửi đi
                sock.Send(lstSendData.ToArray());
                //Nhận về 
                byte[] rcvData = new byte[1024];
                sock.Receive(rcvData);

                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(rcvData);
                //Subheader
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("SLMP get error Sub Header");
                }
                lstRcv.RemoveRange(0, 2);
                //Check Acess Route
                //Network No
                if (lstRcv[0] != 0x00)
                {
                    throw new Exception("SLMP get error Network No");
                }
                lstRcv.RemoveRange(0, 1);
                //PC No
                if (lstRcv[0] != (byte)this.pcNo)
                {
                    throw new Exception("SMLP get error PC No");
                }
                lstRcv.RemoveRange(0, 1);
                //Module IO
                if (lstRcv[0] != 0xFF || lstRcv[1] != 0x03)
                {
                    throw new Exception("SLMP get error Module IO");
                }
                lstRcv.RemoveRange(0, 2);
                //Station No
                if (lstRcv[0] != 0x00)
                {
                    throw new Exception("SLMP get error Module IO");
                }
                lstRcv.RemoveRange(0, 1);
                //Reponse Data Length
                short dataLenght = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (dataLenght < 2)
                //Data Lenght = Endcode + Response Data
                // nếu < báo lỗi
                {
                    throw new Exception("SLMP get error Data Lenght");
                }
                lstRcv.RemoveRange(0, 2);
                //End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("SLMP get error: {0} {1}", lstRcv[1], lstRcv[0]));
                }
                lstRcv.RemoveRange(0, 2);
                result = 0;
                return result;
            }
            
        }
        public int WriteDoubleWord(DevideCode _devCode, int _devNumber, int _shValue)
        {
            int result = -1;
            if (sock == null)
            {
                return result;
            }
            if (sock.Connected == false)
            {
                return result;
            }
            //Chuẩn bị dữ liệu gửi xuống PLC
            List<byte> lstSendData = new List<byte>();
            //1.Sub header:5000
            lstSendData.Add(0x50);
            lstSendData.Add(0x00);

            //2.Access router 5 byte
            //2.1 Network number
            lstSendData.Add((byte)this.networkNo);
            //2.2 PC No
            lstSendData.Add((byte)this.pcNo);
            //2.3 Request IO No
            lstSendData.Add(0xFF);
            lstSendData.Add(0x03);
            //2.4 Station No
            lstSendData.Add((byte)this.stationNo);

            //3.Request data lenght
            lstSendData.Add(0x00);
            lstSendData.Add(0x00);

            //4. Monitoring time : 2 byte 9-10: 16x250 = 4s
            lstSendData.Add(0x00);
            lstSendData.Add(0x00);

            //5.Request item
            //5.1 Command: Device Write
            lstSendData.Add(0x01);
            lstSendData.Add(0x14);
            //5.2.Sub Command:
            lstSendData.Add(0x00);
            lstSendData.Add(0x00);
            //5.3 Head Device No: Đọc thanh ghi số bao nhiêu : 3 byte
            int headNumber = _devNumber;
            for (int i = 0; i < 3; i++)
            {
                byte a = (byte)(headNumber >> 8 * i);
                lstSendData.Add(a);
            }
            //5.4 Device Code
            lstSendData.Add((byte)_devCode);
            //5.5 Number of Device: Số lượng thanh ghi đọc: 1 thanh ghi
            int numberOfDevice = 2;
            for (int i = 0; i < 2; i++)
            {
                byte a = (byte)(numberOfDevice >> 8 * i);
                lstSendData.Add(a);
            }
            //lstSendData.Add(0x01);
            //5.6 Write Data: Ghi dữ liệu vào thanh ghi
            int value = _shValue;
            for (int i = 0; i < 4; i++)
            {
                byte a = (byte)(value >> 8 * i);
                lstSendData.Add(a);
            }
            //Tính số lượng byte gửi
            int noOfDataByte = lstSendData.Count - 9;
            byte[] bytes = BitConverter.GetBytes(noOfDataByte);
            lstSendData[7] = bytes[0];
            lstSendData[8] = bytes[1];

            //Gửi đi
            sock.Send(lstSendData.ToArray());
            //Nhận về 
            byte[] rcvData = new byte[1024];
            sock.Receive(rcvData);

            List<byte> lstRcv = new List<byte>();
            lstRcv.AddRange(rcvData);
            //Subheader
            if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
            {
                throw new Exception("SLMP get error Sub Header");
            }
            lstRcv.RemoveRange(0, 2);
            //Check Acess Route
            //Network No
            if (lstRcv[0] != (byte)this.networkNo)
            {
                throw new Exception("SLMP get error Network No");
            }
            lstRcv.RemoveRange(0, 1);
            //PC No
            if (lstRcv[0] != (byte)this.pcNo)
            {
                throw new Exception("SMLP get error PC No");
            }
            lstRcv.RemoveRange(0, 1);
            //Module IO
            if (lstRcv[0] != 0xFF || lstRcv[1] != 0x03)
            {
                throw new Exception("SLMP get error Module IO");
            }
            lstRcv.RemoveRange(0, 2);
            //Station No
            if (lstRcv[0] != (byte)this.stationNo)
            {
                throw new Exception("SLMP get error Module IO");
            }
            lstRcv.RemoveRange(0, 1);

            //Reponse Data Length
            // Voi write data, chi tra ve 2
            short dataLenght = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
            if (dataLenght < 2)
            {
                throw new Exception("SLMP get error Data Lenght");
            }
            lstRcv.RemoveRange(0, 2);
            //End Code
            if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
            {
                throw new Exception(string.Format("SLMP get error: {0} {1}", lstRcv[1], lstRcv[0]));
            }
            lstRcv.RemoveRange(0, 2);
            result = 0;
            return result;
        }
        public int ReadWord(DevideCode _devCode, int _devNumber, out short _shValue)
        {
            lock (obj)
            {
                int result = -1;
                _shValue = 0;
                if (sock == null)
                {
                    return result;
                }
                if (sock.Connected == false)
                {
                    return result;
                }
                //Chuẩn bị dữ liệu gửi xuống PLC
                List<byte> lstSendData = new List<byte>();
                //1.Sub header:5000
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                //2.Access router 5 byte
                //2.1 Network number
                lstSendData.Add((byte)this.networkNo);
                //2.2 PC No
                lstSendData.Add((byte)this.pcNo);
                //2.3 Request IO No
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //2.4 Station No
                lstSendData.Add((byte)this.stationNo);

                //3.Request data lenght
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                //4. Monitoring time : 2 byte 9-10: 16x250 = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);

                //5.Request item
                //5.1 Command: Lệnh đọc dữ liệu 0401
                lstSendData.Add(0x01);
                lstSendData.Add(0x04);
                //5.2.Sub Command:
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);
                //5.3 Head Device No: Đọc thanh ghi số bao nhiêu : 3 byte
                int headNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    byte a = (byte)(headNumber >> 8 * i);
                    lstSendData.Add(a);
                }
                //5.4 Device Code
                lstSendData.Add((byte)_devCode);
                //5.5 Number of Device: Số lượng thanh ghi đọc: 1 thanh ghi
                int numberOfDevice = 1;
                for (int i = 0; i < 2; i++)
                {
                    byte a = (byte)(numberOfDevice >> 8 * i);
                    lstSendData.Add(a);
                }

                //Tính số lượng byte gửi
                int noOfDataByte = lstSendData.Count - 9;
                byte[] bytes = BitConverter.GetBytes(noOfDataByte);
                lstSendData[7] = bytes[0];
                lstSendData[8] = bytes[1];

                //Gửi đi
                sock.Send(lstSendData.ToArray());


                //Nhận về 
                byte[] rcvData = new byte[1024];
                sock.Receive(rcvData);

                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(rcvData);
                //Subheader
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("SLMP get error Sub Header");
                }
                lstRcv.RemoveRange(0, 2);
                //Check Acess Route
                //Network No
                if (lstRcv[0] != (byte)this.networkNo)
                {
                    throw new Exception("SLMP get error Network No");
                }
                lstRcv.RemoveRange(0, 1);
                //PC No
                if (lstRcv[0] != (byte)this.pcNo)
                {
                    throw new Exception("SMLP get error PC No");
                }
                lstRcv.RemoveRange(0, 1);
                //Module IO
                if (lstRcv[0] != 0xFF || lstRcv[1] != 0x03)
                {
                    throw new Exception("SLMP get error Module IO");
                }
                lstRcv.RemoveRange(0, 2);
                //Station No
                if (lstRcv[0] != (byte)this.stationNo)
                {
                    throw new Exception("SLMP get error Module IO");
                }
                lstRcv.RemoveRange(0, 1);
                //Reponse Data Length
                short dataLenght = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (dataLenght < 4)
                {
                    throw new Exception("SLMP get error Data Lenght");
                }
                lstRcv.RemoveRange(0, 2);
                //End Code
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("SLMP get error: {0} {1}", lstRcv[1], lstRcv[0]));
                }
                lstRcv.RemoveRange(0, 2);
                //Lấy dữ liệu
                _shValue = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1], 0 }, 0);
                result = 0;
                return result;
            }
            
        }
        public int ReadDoubleWord(DevideCode _devCode, int _devNumber, out short _shValue)
        {
            int result = -1;
            _shValue = 0;
            if (sock == null)
            {
                return result;
            }
            if (sock.Connected == false)
            {
                return result;
            }
            //Chuẩn bị dữ liệu gửi xuống PLC
            List<byte> lstSendData = new List<byte>();
            //1.Sub header:5000
            lstSendData.Add(0x50);
            lstSendData.Add(0x00);

            //2.Access router 5 byte
            //2.1 Network number
            lstSendData.Add((byte)this.networkNo);
            //2.2 PC No
            lstSendData.Add((byte)this.pcNo);
            //2.3 Request IO No
            lstSendData.Add(0xFF);
            lstSendData.Add(0x03);
            //2.4 Station No
            lstSendData.Add((byte)this.stationNo);

            //3.Request data lenght
            lstSendData.Add(0x00);
            lstSendData.Add(0x00);

            //4. Monitoring time : 2 byte 9-10: 16x250 = 4s
            lstSendData.Add(0x10);
            lstSendData.Add(0x00);

            //5.Request item
            //5.1 Command: Lệnh đọc dữ liệu 0401
            lstSendData.Add(0x01);
            lstSendData.Add(0x04);
            //5.2.Sub Command:
            lstSendData.Add(0x00);
            lstSendData.Add(0x00);
            //5.3 Head Device No: Đọc thanh ghi số bao nhiêu : 3 byte
            int headNumber = _devNumber;
            for (int i = 0; i < 3; i++)
            {
                byte a = (byte)(headNumber >> 8 * i);
                lstSendData.Add(a);
            }
            //5.4 Device Code
            lstSendData.Add((byte)_devCode);
            //5.5 Number of Device: Số lượng thanh ghi đọc: 1 thanh ghi
            int numberOfDevice = 2;
            for (int i = 0; i < 2; i++)
            {
                byte a = (byte)(numberOfDevice >> 8 * i);
                lstSendData.Add(a);
            }

            //Tính số lượng byte gửi
            int noOfDataByte = lstSendData.Count - 9;
            byte[] bytes = BitConverter.GetBytes(noOfDataByte);
            lstSendData[7] = bytes[0];
            lstSendData[8] = bytes[1];

            //Gửi đi
            sock.Send(lstSendData.ToArray());
            //Nhận về 
            byte[] rcvData = new byte[1024];
            sock.Receive(rcvData);

            List<byte> lstRcv = new List<byte>();
            lstRcv.AddRange(rcvData);
            //Subheader
            if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
            {
                throw new Exception("SLMP get error Sub Header");
            }
            lstRcv.RemoveRange(0, 2);
            //Check Acess Route
            //Network No
            if (lstRcv[0] != (byte)this.networkNo)
            {
                throw new Exception("SLMP get error Network No");
            }
            lstRcv.RemoveRange(0, 1);
            //PC No
            if (lstRcv[0] != (byte)this.pcNo)
            {
                throw new Exception("SMLP get error PC No");
            }
            lstRcv.RemoveRange(0, 1);

            //Module IO
            if (lstRcv[0] != 0xFF || lstRcv[1] != 0x03)
            {
                throw new Exception("SLMP get error Module IO");
            }
            lstRcv.RemoveRange(0, 2);

            //Station No
            if (lstRcv[0] != (byte)this.stationNo)
            {
                throw new Exception("SLMP get error Module IO");
            }
            lstRcv.RemoveRange(0, 1);

            //Reponse Data Length
            short dataLenght = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
            if (dataLenght < 6) //4 bytes //Double words
            {
                throw new Exception("SLMP get error Data Lenght");
            }
            lstRcv.RemoveRange(0, 2);
            //End Code
            if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
            {
                throw new Exception(string.Format("SLMP get error: {0} {1}", lstRcv[1], lstRcv[0]));
            }
            lstRcv.RemoveRange(0, 2);
            //Lấy dữ liệu
            _shValue = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1], lstRcv[2], lstRcv[3] }, 0);
            result = 0;
            return result;
        }
        public int ReadMutilWord(DevideCode _devCode, int _devNumber, int count, out List<short> _lstValue)
        {
            int result = -1;
            _lstValue = new List<short>();
            if (count <= 0)
            {
                return result;
            }
            if (sock == null)
            {
                return result;
            }
            if (sock.Connected == false)
            {
                return result;
            }
            //Chuẩn bị dữ liệu gửi xuống PLC
            List<byte> lstSendData = new List<byte>();
            //1.Sub header:5000
            lstSendData.Add(0x50);
            lstSendData.Add(0x00);

            //2.Access router 5 byte
            //2.1 Network number
            lstSendData.Add((byte)this.networkNo);
            //2.2 PC No
            lstSendData.Add((byte)this.pcNo);
            //2.3 Request IO No
            lstSendData.Add(0xFF);
            lstSendData.Add(0x03);
            //2.4 Station No
            lstSendData.Add((byte)this.stationNo);

            //3.Request data lenght
            lstSendData.Add(0x00);
            lstSendData.Add(0x00);

            //4. Monitoring time : 2 byte 9-10: 16x250 = 4s
            lstSendData.Add(0x10);
            lstSendData.Add(0x00);

            //5.Request item
            //5.1 Command: Lệnh đọc dữ liệu 0401
            lstSendData.Add(0x01);
            lstSendData.Add(0x04);
            //5.2.Sub Command:
            lstSendData.Add(0x00);
            lstSendData.Add(0x00);
            //5.3 Head Device No: Đọc thanh ghi số bao nhiêu : 3 byte
            int headNumber = _devNumber;
            for (int i = 0; i < 3; i++)
            {
                byte a = (byte)(headNumber >> 8 * i);
                lstSendData.Add(a);
            }
            //5.4 Device Code
            lstSendData.Add((byte)_devCode);
            //5.5 Number of Device: Số lượng thanh ghi đọc: 1 thanh ghi
            int numberOfDevice = count;
            for (int i = 0; i < 2; i++)
            {
                byte a = (byte)(numberOfDevice >> 8 * i);
                lstSendData.Add(a);
            }

            //Tính số lượng byte gửi
            int noOfDataByte = lstSendData.Count - 9;
            byte[] bytes = BitConverter.GetBytes(noOfDataByte);
            lstSendData[7] = bytes[0];
            lstSendData[8] = bytes[1];

            //Gửi đi
            sock.Send(lstSendData.ToArray());
            //Nhận về 
            byte[] rcvData = new byte[1024];
            sock.Receive(rcvData);

            List<byte> lstRcv = new List<byte>();
            lstRcv.AddRange(rcvData);
            //Subheader
            if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
            {
                throw new Exception("SLMP get error Sub Header");
            }
            lstRcv.RemoveRange(0, 2);
            //Check Acess Route
            //Network No
            if (lstRcv[0] != (byte)this.networkNo)
            {
                throw new Exception("SLMP get error Network No");
            }
            lstRcv.RemoveRange(0, 1);
            //PC No
            if (lstRcv[0] != (byte)this.pcNo)
            {
                throw new Exception("SMLP get error PC No");
            }
            lstRcv.RemoveRange(0, 1);

            //Module IO
            if (lstRcv[0] != 0xFF || lstRcv[1] != 0x03)
            {
                throw new Exception("SLMP get error Module IO");
            }
            lstRcv.RemoveRange(0, 2);

            //Station No
            if (lstRcv[0] != (byte)this.stationNo)
            {
                throw new Exception("SLMP get error Module IO");
            }
            lstRcv.RemoveRange(0, 1);

            //Reponse Data Length
            short dataLenght = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
            if (dataLenght < 6) //4 bytes //Double words
            {
                throw new Exception("SLMP get error Data Lenght");
            }
            lstRcv.RemoveRange(0, 2);
            //End Code
            if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
            {
                throw new Exception(string.Format("SLMP get error: {0} {1}", lstRcv[1], lstRcv[0]));
            }
            lstRcv.RemoveRange(0, 2);
            //Lấy dữ liệu
            for (int i = 0; i < count; i++)
            {
                _lstValue.Add(BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0));
                lstRcv.RemoveRange(0, 2);
            }

            result = 0;
            return result;
        }
        public int WriteBit(DevideCode _devCode, int _devNumber, bool _bValue)
        {
            lock(obj)
            {
                int result = -1;
                if (sock == null)
                {
                    return result;
                }
                if (sock.Connected == false)
                {
                    return result;
                }
                //Chuẩn bị dữ liệu gửi xuống PLC
                List<byte> lstSendData = new List<byte>();
                //1.Sub header:5000
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);
                //2.Access router 5 byte
                //2.1 Network number
                lstSendData.Add((byte)this.networkNo);
                //2.2 PC No
                lstSendData.Add((byte)this.pcNo);
                //2.3 Request IO No
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //2.4 Station No
                lstSendData.Add((byte)this.stationNo);
                //3.Request data lenght
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                //4. Monitoring time : 2 byte 9-10: 16x250 = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);

                //5.Request item
                //5.1 Command: Device Write
                lstSendData.Add(0x01);
                lstSendData.Add(0x14);
                //5.2.Sub Command:
                lstSendData.Add(0x01);
                lstSendData.Add(0x00);
                //5.3 Head Device No: Đọc thanh ghi số bao nhiêu : 3 byte
                int headNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    byte a = (byte)(headNumber >> 8 * i);
                    lstSendData.Add(a);
                }
                //5.4 Device Code
                lstSendData.Add((byte)_devCode);
                //5.5 Number of Device: Số lượng thanh ghi đọc: 1 thanh ghi
                int numberOfDevice = 1;
                for (int i = 0; i < 2; i++)
                {
                    byte a = (byte)(numberOfDevice >> 8 * i);
                    lstSendData.Add(a);
                }
                //5.6 Write Data: Ghi dữ liệu vào thanh ghi
                if (_bValue)
                {
                    lstSendData.Add(0x10);
                }
                else
                {
                    lstSendData.Add(0x00);
                }
                //Tính số lượng byte gửi
                int noOfDataByte = lstSendData.Count - 9;
                byte[] bytes = BitConverter.GetBytes(noOfDataByte);
                lstSendData[7] = bytes[0];
                lstSendData[8] = bytes[1];

                //Gửi đi
                sock.Send(lstSendData.ToArray());
                //Nhận về 
                byte[] rcvData = new byte[1024];
                sock.Receive(rcvData);

                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(rcvData);
                //Subheader
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("SLMP get error Sub Header");
                }
                lstRcv.RemoveRange(0, 2);
                //Check Acess Route
                //Network No
                if (lstRcv[0] != (byte)this.networkNo)
                {
                    throw new Exception("SLMP get error Network No");
                }
                lstRcv.RemoveRange(0, 1);
                //PC No
                if (lstRcv[0] != (byte)this.pcNo)
                {
                    throw new Exception("SMLP get error PC No");
                }
                lstRcv.RemoveRange(0, 1);
                //Module IO
                if (lstRcv[0] != 0xFF || lstRcv[1] != 0x03)
                {
                    throw new Exception("SLMP get error Module IO");
                }
                lstRcv.RemoveRange(0, 2);
                //Station No
                if (lstRcv[0] != (byte)this.stationNo)
                {
                    throw new Exception("SLMP get error Module IO");
                }
                lstRcv.RemoveRange(0, 1);
                //Reponse Data Length
                short dataLenght = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (dataLenght < 2)
                //Data Lenght = Endcode + Response Data
                // nếu <2 báo lỗi
                {
                    throw new Exception("SLMP get error Data Lenght");
                }
                lstRcv.RemoveRange(0, 2);
                //End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("SLMP get error: {0} {1}", lstRcv[1], lstRcv[0]));
                }
                lstRcv.RemoveRange(0, 2);
                result = 0;
                return result;
            }    
            
        }
        public int ReadBit(DevideCode _devCode, int _devNumber, bool _bValue)
        {
            lock(obj)
            {
                int result = -1;
                if (sock == null)
                {
                    return result;
                }
                if (sock.Connected == false)
                {
                    return result;
                }
                //Chuẩn bị dữ liệu gửi xuống PLC
                List<byte> lstSendData = new List<byte>();
                //1.Sub header:5000
                lstSendData.Add(0x50);
                lstSendData.Add(0x00);

                //2.Access router 5 byte
                //2.1 Network number
                lstSendData.Add((byte)this.networkNo);
                //2.2 PC No
                lstSendData.Add((byte)this.pcNo);
                //2.3 Request IO No
                lstSendData.Add(0xFF);
                lstSendData.Add(0x03);
                //2.4 Station No
                lstSendData.Add((byte)this.stationNo);

                //3.Request data lenght
                lstSendData.Add(0x00);
                lstSendData.Add(0x00);

                //4. Monitoring time : 2 byte 9-10: 16x250 = 4s
                lstSendData.Add(0x10);
                lstSendData.Add(0x00);

                //5.Request item
                //5.1 Command: Device Write
                lstSendData.Add(0x01);
                lstSendData.Add(0x14);
                //5.2.Sub Command:
                lstSendData.Add(0x01);
                lstSendData.Add(0x00);
                //5.3 Head Device No: Đọc thanh ghi số bao nhiêu : 3 byte
                int headNumber = _devNumber;
                for (int i = 0; i < 3; i++)
                {
                    byte a = (byte)(headNumber >> 8 * i);
                    lstSendData.Add(a);
                }
                //5.4 Device Code
                lstSendData.Add((byte)_devCode);
                //5.5 Number of Device: Số lượng thanh ghi đọc: 1 thanh ghi
                int numberOfDevice = 1;
                for (int i = 0; i < 2; i++)
                {
                    byte a = (byte)(numberOfDevice >> 8 * i);
                    lstSendData.Add(a);
                }
                //5.6 Write Data: Ghi dữ liệu vào thanh ghi
                if (_bValue)
                {
                    lstSendData.Add(0x10);
                }
                else
                {
                    lstSendData.Add(0x00);
                }
                //Tính số lượng byte gửi
                int noOfDataByte = lstSendData.Count - 9;
                byte[] bytes = BitConverter.GetBytes(noOfDataByte);
                lstSendData[7] = bytes[0];
                lstSendData[8] = bytes[1];

                //Gửi đi
                sock.Send(lstSendData.ToArray());
                //Nhận về 
                byte[] rcvData = new byte[1024];
                sock.Receive(rcvData);

                List<byte> lstRcv = new List<byte>();
                lstRcv.AddRange(rcvData);
                //Subheader
                if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                {
                    throw new Exception("SLMP get error Sub Header");
                }
                lstRcv.RemoveRange(0, 2);
                //Check Acess Route
                //Network No
                if (lstRcv[0] != (byte)this.networkNo)
                {
                    throw new Exception("SLMP get error Network No");
                }
                lstRcv.RemoveRange(0, 1);
                //PC No
                if (lstRcv[0] != (byte)this.pcNo)
                {
                    throw new Exception("SMLP get error PC No");
                }
                lstRcv.RemoveRange(0, 1);
                //Module IO
                if (lstRcv[0] != 0xFF || lstRcv[1] != 0x03)
                {
                    throw new Exception("SLMP get error Module IO");
                }
                lstRcv.RemoveRange(0, 2);
                //Station No
                if (lstRcv[0] != (byte)this.stationNo)
                {
                    throw new Exception("SLMP get error Module IO");
                }
                lstRcv.RemoveRange(0, 1);
                //Reponse Data Length
                short dataLenght = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                if (dataLenght < 2)
                //Data Lenght = Endcode + Response Data
                // nếu <4 báo lỗi
                {
                    throw new Exception("SLMP get error Data Lenght");
                }
                lstRcv.RemoveRange(0, 2);
                //End Code: 2 byte
                if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                {
                    throw new Exception(string.Format("SLMP get error: {0} {1}", lstRcv[1], lstRcv[0]));
                }
                lstRcv.RemoveRange(0, 2);
                result = 0;
                return result;
            }
           
        }
        public int ReadMultiBit(DevideCode _devCode, int _devNumber, int count, out List<bool> _lstValue)
        {
            lock (this)
            {
                    int result = -1;
                    _lstValue = new List<bool>();
                    if (sock == null)
                    {
                        return result;
                    }
                    if (sock.Connected == false)
                    {
                        return result;
                    }
                    //Chuẩn bị dữ liệu gửi xuống PLC
                    List<byte> lstSendData = new List<byte>();
                    //1.Sub header:5000
                    lstSendData.Add(0x50);
                    lstSendData.Add(0x00);

                    //2.Access router 5 byte
                    //2.1 Network number
                    lstSendData.Add((byte)this.networkNo);
                    //2.2 PC No
                    lstSendData.Add((byte)this.pcNo);
                    //2.3 Request IO No
                    lstSendData.Add(0xFF);
                    lstSendData.Add(0x03);
                    //2.4 Station No
                    lstSendData.Add((byte)this.stationNo);

                    //3.Request data lenght
                    lstSendData.Add(0x00);
                    lstSendData.Add(0x00);

                    //4. Monitoring time : 2 byte 9-10: 16x250 = 4s
                    lstSendData.Add(0x10);
                    lstSendData.Add(0x00);

                    //5.Request item
                    //5.1 Command: Device Write
                    lstSendData.Add(0x01);
                    lstSendData.Add(0x14);
                    //5.2.Sub Command:
                    lstSendData.Add(0x01);
                    lstSendData.Add(0x00);
                    //5.3 Head Device No: Đọc thanh ghi số bao nhiêu : 3 byte
                    int headNumber = _devNumber;
                    for (int i = 0; i < 3; i++)
                    {
                        byte a = (byte)(headNumber >> 8 * i);
                        lstSendData.Add(a);
                    }
                    //5.4 Device Code
                    lstSendData.Add((byte)_devCode);
                    //5.5 Number of Device: Đọc bao nhiêu bit
                    int numberOfDevice = count;
                    for (int i = 0; i < 2; i++)
                    {
                        byte a = (byte)(numberOfDevice >> 8 * i);
                        lstSendData.Add(a);
                    }

                    //Tính số lượng byte gửi
                    int noOfDataByte = lstSendData.Count - 9;
                    byte[] bytes = BitConverter.GetBytes(noOfDataByte);
                    lstSendData[7] = bytes[0];
                    lstSendData[8] = bytes[1];

                    //Gửi đi
                    sock.Send(lstSendData.ToArray());
                    //Nhận về 
                    byte[] rcvData = new byte[1024];
                    sock.Receive(rcvData);

                    List<byte> lstRcv = new List<byte>();
                    lstRcv.AddRange(rcvData);
                    //Subheader
                    if (lstRcv[0] != 0xD0 || lstRcv[1] != 0x00)
                    {
                        throw new Exception("SLMP get error Sub Header");
                    }
                    lstRcv.RemoveRange(0, 2);
                    //Check Acess Route
                    //Network No
                    if (lstRcv[0] != (byte)this.networkNo)
                    {
                        throw new Exception("SLMP get error Network No");
                    }
                    lstRcv.RemoveRange(0, 1);
                    //PC No
                    if (lstRcv[0] != (byte)this.pcNo)
                    {
                        throw new Exception("SMLP get error PC No");
                    }
                    lstRcv.RemoveRange(0, 1);
                    //Module IO
                    if (lstRcv[0] != 0xFF || lstRcv[1] != 0x03)
                    {
                        throw new Exception("SLMP get error Module IO");
                    }
                    lstRcv.RemoveRange(0, 2);
                    //Station No
                    if (lstRcv[0] != (byte)this.stationNo)
                    {
                        throw new Exception("SLMP get error Module IO");
                    }
                    lstRcv.RemoveRange(0, 1);
                    //Reponse Data Length
                    short dataLenght = BitConverter.ToInt16(new byte[] { lstRcv[0], lstRcv[1] }, 0);
                    if (dataLenght < 2)
                    //Data Lenght = Endcode + Response Data
                    {
                        throw new Exception("SLMP get error Data Lenght");
                    }
                    lstRcv.RemoveRange(0, 2);
                    //End Code: 2 byte
                    if (lstRcv[0] != 0x00 || lstRcv[1] != 0x00)
                    {
                        throw new Exception(string.Format("SLMP get error: {0} {1}", lstRcv[1], lstRcv[0]));
                    }
                    lstRcv.RemoveRange(0, 2);
                    //Lấy dữ liệu
                    int byteCount = count / 2 + count % 2;

                    for (int i = 0; i < byteCount; i++)
                    {
                        //0x10 >> 4bit kết quả được nhận sẽ là 0x01
                        int a = lstRcv[i] >> 4;
                        if (a == 0)
                        {
                            _lstValue.Add(false);
                        }
                        else
                        {
                            _lstValue.Add(true);
                        }
                        //Ví dụ 0x11 & 0x0f
                        // 0x11 = 0001 0001
                        // 0x0f = 0000 1111
                        // KQ   = 0000 0001
                        if (_lstValue.Count >= count)
                        {
                            break;
                        }
                        int b = lstRcv[i] & 0x0F;
                        if (b == 0)
                        {
                            _lstValue.Add(false);
                        }
                        else
                        {
                            _lstValue.Add(true);
                        }
                    }
                    result = 0;
                    return result;
               
            }
            
        }
    }
}
