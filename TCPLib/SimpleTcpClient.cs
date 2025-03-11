using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;


namespace TCPLib
{
    public class SimpleTcpClient : IDisposable
    {
        private Socket _socket = null;
        private byte[] buffer = new byte[2048];
        public SimpleTcpClient()
        {
            
            //StringEncoder = System.Text.Encoding.UTF8;
            //ReadLoopIntervalMs = 10;
            //Delimiter = 0x13;

            //this.DataReceived += SimpleTcpClient_DataReceived;
        }

        private Thread _rxThread = null;
        private List<byte> _queuedMsg = new List<byte>();
        public byte Delimiter { get; set; }
        public System.Text.Encoding StringEncoder { get; set; }
        private TcpClient _client = null;

        public event EventHandler<Message> DelimiterDataReceived;
        public event EventHandler<Message> DataReceived;
        public event EventHandler NotifyDisconnect;

        internal bool QueueStop { get; set; }
        internal int ReadLoopIntervalMs { get; set; }
        public bool AutoTrimStrings { get; set; }

        public string IpAddress;
        public int Iport;
        Ping pingSender = new Ping();
        PingOptions options = new PingOptions();

        private bool _isConnect=false;

        private AutoResetEvent _timeoutObject;
        public SimpleTcpClient Connect(string hostNameOrIpAddress, int port)
        {
            _timeoutObject = new AutoResetEvent(false);
            Iport = port;
            IpAddress = hostNameOrIpAddress;

            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //_socket.Connect(hostNameOrIpAddress, port);
                _socket.BeginConnect(hostNameOrIpAddress, port, new AsyncCallback(CallBackConnect), _socket);
                if (!_timeoutObject.WaitOne(2000, false))
                {
                    throw(new Exception());
                }
                _socket.BeginReceive(buffer, 0, 2048, SocketFlags.None, AsyncCallback, this);
                _isConnect = true;
            }
            catch ( Exception ex)
            {
                string msg = ex.Message;
                Disconnect();

            }
            return this;
        }

        private void CallBackConnect(IAsyncResult asyncresult)
        {
            _timeoutObject.Set();
        }

        public void AsyncCallback(IAsyncResult ar)
        {
            try
            {
                int recvcount= this._socket.EndReceive(ar);
               if (recvcount > 0)
               {
                   string recvmsg = Encoding.ASCII.GetString(buffer);
                   if (DataReceived != null)
                   {
                       Message m = new Message(buffer, _client, StringEncoder, Delimiter, AutoTrimStrings);
                       mReply = m;
                       DataReceived(this, m);
                       buffer = new byte[2048];
                       _socket.BeginReceive(buffer, 0, 2048, SocketFlags.None, AsyncCallback, this);
                   }
               }
            }
            catch
            {
                Disconnect();
            }
           
        }

        private void StartRxThread()
        {
            if (_rxThread != null) { return; }

            _rxThread = new Thread(ListenerLoop);
            _rxThread.IsBackground = true;
            _rxThread.Start();
        }

        public SimpleTcpClient Disconnect()
        {
            try
            {
                _isConnect = false;
                if (_socket == null) { return this; }
                this._socket.Disconnect(true);
                _socket.Close();
                _socket = null;
                
                if (NotifyDisconnect != null)
                    NotifyDisconnect(this,new EventArgs());

            }
            catch (Exception)
            {
                if (NotifyDisconnect != null)
                    NotifyDisconnect(this, new EventArgs());
            }
            return this;
        }

        public bool Connected
        {
            get
            {
                return _isConnect;
            }
        }



        public void ReConnect()
        {
            try
            {
                //Disconnect();
                //_client = new TcpClient();
                //_client.Connect(IpAddress, Iport);
                //StartRxThread();
            }
            catch (Exception)
            {
            }

        }

        public TcpClient TcpClient { get { return _client; } }

        private void ListenerLoop(object state)
        {
            while (!QueueStop)
            {
                try
                {
                    RunLoopStep();
                }
                catch
                {
                }
                System.Threading.Thread.Sleep(ReadLoopIntervalMs);
            }

            _rxThread = null;
        }

        private void RunLoopStep()
        {
            if (_client == null) { return; }
            if (_client.Connected == false) { return; }


            var delimiter = this.Delimiter;
            var c = _client;

            int bytesAvailable = c.Available;
            if (bytesAvailable == 0)
            {
                System.Threading.Thread.Sleep(10);
                return;
            }

            List<byte> bytesReceived = new List<byte>();

            while (c.Available > 0 && c.Connected)
            {
                byte[] nextByte = new byte[1];
                c.Client.Receive(nextByte, 0, 1, SocketFlags.None);
                bytesReceived.AddRange(nextByte);
                if (nextByte[0] == delimiter)
                {
                    byte[] msg = _queuedMsg.ToArray();
                    _queuedMsg.Clear();
                    NotifyDelimiterMessageRx(c, msg);
                }
                else
                {
                    _queuedMsg.AddRange(nextByte);
                }
                Thread.Sleep(2);
            }

            if (bytesReceived.Count > 0)
            {
                NotifyEndTransmissionRx(c, bytesReceived.ToArray());
            }
        }

        private void JudgeAndReConnected(object state)
        {
            while (true)
            {
                if (_client == null || !_client.Connected)
                {
                    ReConnect();
                }
                try
                {

                    PingReply reply = pingSender.Send(IpAddress, 120, Encoding.ASCII.GetBytes("test"));//硬件断线检测

                    var ret = TcpClient.Client.Poll(100, SelectMode.SelectRead);//软件断线检测
                    if (ret || (reply.Status != IPStatus.Success))
                    {
                        if (TcpClient.Available == 0)
                        {
                            try
                            {
                                Disconnect();
                                _client = new TcpClient();
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Disconnect();
                    _client = new TcpClient();
                }
                Thread.Sleep(300);
            }

        }


        private void NotifyDelimiterMessageRx(TcpClient client, byte[] msg)
        {
            if (DelimiterDataReceived != null)
            {
                Message m = new Message(msg, client, StringEncoder, Delimiter, AutoTrimStrings);
                DelimiterDataReceived(this, m);
            }
        }

        private void NotifyEndTransmissionRx(TcpClient client, byte[] msg)
        {
            if (DataReceived != null)
            {
                Message m = new Message(msg, client, StringEncoder, Delimiter, AutoTrimStrings);
                DataReceived(this, m);
            }
        }

        public void Write(byte[] data)
        {
            try
            {
               // if (_client == null) { throw new Exception("Cannot send data to a null TcpClient (check to see if Connect was called)"); }
                //_client.GetStream().Write(data, 0, data.Length);
                _socket.Send(data, data.Length, SocketFlags.None);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Write(string data)
        {
            try
            {
                if (data == null) { return; }
                Write(Encoding.ASCII.GetBytes(data));
            }
            catch (Exception)
            {

            }

        }

        public void WriteLine(string data)
        {
            try
            {
                if (string.IsNullOrEmpty(data)) { return; }
                if (data.LastOrDefault() != Delimiter)
                {
                    Write(data + StringEncoder.GetString(new byte[] { Delimiter }));
                }
                else
                {
                    Write(data);
                }
            }
            catch (Exception)
            {

            }

        }

        Message mReply = null;
        public bool WriteAndGetReply(string cmd)
        {
            mReply = null;
            Write(cmd);
            return true;
        }

        public void Send(string cmd)
        {
            mReply = null;
            Write(cmd);
        }

        public bool GetReply(out string result, int timeout)
        {
            result = "";
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (true)
            {
                if (sw.ElapsedMilliseconds > timeout)
                {
                    return false;
                }
                if (mReply == null)
                {
                    System.Threading.Thread.Sleep(10);
                    continue;
                }
                else
                {
                    break;
                }
            }
            result = mReply.MessageString;
            return true;
        }

        public bool WriteAndGetReply(string cmd, out string result, int timeout)
        {
            result = "";
            mReply = null;
            Write(cmd);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (true)
            {
                if (sw.ElapsedMilliseconds > timeout)
                {
                    return false;
                }
                if (mReply == null)
                {
                    System.Threading.Thread.Sleep(10);
                    continue;
                }
                else
                {
                    break;
                }
            }
            result = mReply.MessageString;
            return true;
        }

        private void SimpleTcpClient_DataReceived(object sender, Message message)
        {
            mReply = message;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                QueueStop = true;
                if (_client != null)
                {
                    try
                    {
                        _client.Close();
                    }
                    catch { }
                    _client = null;
                }

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SimpleTcpClient() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}