using System;
using System.Net.Sockets;
using System.Text;

namespace Diiage2020.CSharpGame.Client.Entities
{
    // State object for receiving data from remote device.
    public class StateObject
    {
        // Size of receive buffer.  
        public const int BufferSize = 9999;

        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];

        // Received data string.
        public StringBuilder sb = new StringBuilder();

        // Client socket.
        public Socket workSocket = null;
    }
}
