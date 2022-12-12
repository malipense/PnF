using System.Net.Sockets;
using System.Net;
using System.Text;
using System;

namespace P_n_F
{
    public class ReadPort
    {
        private const int MAXQUEUESIZE = 10;
        private const int BUFFERSIZE = 2048;
        public void Listen(object port)
        {
            IPAddress ipAddress = IPAddress.Loopback;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, (int)port);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(localEndPoint);
            socket.Listen(MAXQUEUESIZE);

            while(true)
            {
                Socket pair = socket.Accept();
                byte[] buffer = new byte[BUFFERSIZE];
                int bytesReceived = pair.Receive(buffer, SocketFlags.None);

                var payload = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                System.Console.WriteLine($"{pair.RemoteEndPoint} - sent the message: {payload}");

                Dispose(ref pair, ref buffer, ref bytesReceived);
            }
        }

        private void Dispose(ref Socket pair, ref byte[]buffer, ref int bytesReceived)
        {
            pair.Shutdown(SocketShutdown.Both);
            pair.Close();
            buffer = Array.Empty<byte>();
            bytesReceived = 0;
        }
    }
}
