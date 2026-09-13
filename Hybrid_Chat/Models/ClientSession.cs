using System;
using System.IO;
using System.Net.Sockets;
using System.Net.Security;

namespace Hybrid_Chat.Models
{
    public class ClientSession
    {
        public string SessionId { get; set; } = string.Empty;
        public string Username { get; set; } = "Guest";
        public string RemoteEndpoint { get; set; } = string.Empty;
        public TcpClient? Connection { get; set; }
        public SslStream? SecureStream { get; set; }
        public StreamWriter? Writer { get; set; }
        public object SendLock { get; } = new();
        public DateTime JoinTime { get; set; } = DateTime.Now;
        public DateTime LastKeepAlive { get; set; } = DateTime.Now;
    }
}