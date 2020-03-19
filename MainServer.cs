using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace wadbsrv
{
    public static class MainServer
    {
        public static int ClientCount = 0;
        public static readonly int Port = 11000;
        private const string certFileName = @"L:\Programming\C#\wa_backend\wamsrv\bin\Debug\netcoreapp3.1\cert.pem";
        public static X509Certificate ServerCertificate;

        public static void Run()
        {
            ServerCertificate = X509Certificate.CreateFromCertFile(certFileName);
            IPAddress ipAddress = washared.Extensions.GetLocalIPAddress();
            if (ipAddress == null)
            {
                Console.WriteLine("Unable to resolve IP address.");
                return;
            }
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Port);
            using Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(localEndPoint);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            socket.Listen(16);
            Console.WriteLine("MainServer started at " + ipAddress.ToString() + ":" + Port.ToString()); ;
            while (true)
            {
                Socket clientSocket = socket.Accept();
                ClientCount++;
                new Thread(() => SqlServer.Create(clientSocket)).Start();
            }
        }
    }
}
