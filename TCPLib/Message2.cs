using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPLib
{
    public class Message2 : EventArgs
    {
        private Socket _Scoket;
        private System.Text.Encoding _encoder = null;
        private byte _writeLineDelimiter;
        private bool _autoTrim = false;
        internal Message2(byte[] data, Socket tcpClient, System.Text.Encoding stringEncoder, byte lineDelimiter)
        {
            Data = data;
            _Scoket = tcpClient;
            _encoder = stringEncoder;
            _writeLineDelimiter = lineDelimiter;
        }

        internal Message2(byte[] data, Socket  tcpClient, System.Text.Encoding stringEncoder, byte lineDelimiter, bool autoTrim)
        {
            Data = data;
            _Scoket = tcpClient;
            _encoder = stringEncoder;
            _writeLineDelimiter = lineDelimiter;
            _autoTrim = autoTrim;
        }

        public byte[] Data { get; private set; }
        public string MessageString
        {
            get
            {
                if (_autoTrim)
                {
                    return _encoder.GetString(Data).Trim();
                }

                return _encoder.GetString(Data);
            }
        }

        public void Reply(byte[] data)
        {
            _Scoket.Send(data);
          //  _Scoket.Send (data, 0, data.Length);
        }

        public void Reply(string data)
        {
            if (string.IsNullOrEmpty(data)) { return; }
            Reply(_encoder.GetBytes(data));
        }

        public void ReplyLine(string data)
        {
            if (string.IsNullOrEmpty(data)) { return; }
            if (data.LastOrDefault() != _writeLineDelimiter)
            {
                Reply(data + _encoder.GetString(new byte[] { _writeLineDelimiter }));
            }
            else
            {
                Reply(data);
            }
        }
    }
}
