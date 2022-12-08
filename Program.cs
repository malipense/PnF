using System;
using System.Threading;

namespace P_n_F
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ReadPort readPort1 = new ReadPort();
            ReadPort readPort2 = new ReadPort();

            Thread thread = new Thread(readPort1.Listen);
            Thread thread2 = new Thread(readPort2.Listen);

            thread.Start(25000);
            thread2.Start(25001);
        }
    }
}
