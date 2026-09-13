using System;

namespace Hybrid_Chat.Models
{
    public enum PacketType
    {
        JOIN,       // TCP: Client joining server
        LEAVE,      // TCP: Client leaving server
        MSG,        // TCP: Text chat message broadcast
        SYS,        // TCP: Server notification
        PING,       // TCP: Keepalive ping
        PONG,       // TCP: Keepalive pong
        TYPING      // UDP: Unreliable typing status beacon
    }

    public class NetworkPacket
    { 
        public PacketType Type { get; set; }
        public string Sender { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// Encodes frame into wire-format string: TYPE|SENDER|PAYLOAD
        /// </summary>
        public string Serialize()
        {
            return $"{(int)Type}|{Escape(Sender)}|{Escape(Payload)}";
        }

        /// <summary>
        /// Decodes wire-format string into structured packet object
        /// </summary>
        public static NetworkPacket Deserialize(string packetStr)
        {
            var parts = packetStr.Split('|');
            if (parts.Length < 3)
            {
                return new NetworkPacket { Type = PacketType.SYS, Sender = "System", Payload = packetStr };
            }

            int typeInt = int.TryParse(parts[0], out var res) ? res : 3;
            return new NetworkPacket
            {
                Type = (PacketType)typeInt,
                Sender = Unescape(parts[1]),
                Payload = Unescape(parts[2]),
                Timestamp = DateTime.Now
            };
        }

        private static string Escape(string input) => input.Replace("|", "\\pipe").Replace("\n", "\\n");
        private static string Unescape(string input) => input.Replace("\\pipe", "|").Replace("\\n", "\n");
    }
}