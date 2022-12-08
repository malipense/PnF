using System.Net.Sockets;
using System.Net;
using System.Text;
using System;

namespace P_n_F
{
    public class ReadPort
    {
        private const int MAXCONNECTIONS = 10;
        private const int BUFFERSIZE = 2048;
        public void Listen(object port)
        {
            IPAddress ipAddress = IPAddress.Loopback;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, (int)port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(localEndPoint);
            socket.Listen(MAXCONNECTIONS);

            while(true)
            {
                Socket handler = socket.Accept();
                byte[] buffer = new byte[BUFFERSIZE];
                int bytesReceived = handler.Receive(buffer, SocketFlags.None);

                var data = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                System.Console.WriteLine($"{handler.RemoteEndPoint} - sent the message: {data}");

                Dispose(ref handler, ref buffer, ref bytesReceived);
            }
        }

        private void Dispose(ref Socket handler, ref byte[]buffer, ref int bytesReceived)
        {
            handler.Close();
            buffer = Array.Empty<byte>();
            bytesReceived = 0;
        }
    }
}
