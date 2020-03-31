using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using wadbsrv.Database;

namespace wadbsrv
{
    public static class MainServer
    {
        public static WadbsrvConfig Config;
        public static int ClientCount = 0;
        public static X509Certificate2 ServerCertificate;

        public static void Run()
        {
            IPAddress ipAddress = IPAddress.Parse(Config.LocalIpAddress);
            if (ipAddress == null)
            {
                Console.WriteLine("Unable to resolve IP address.");
                return;
            }
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Config.LocalPort);
            using Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(localEndPoint);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            socket.Listen(16);
            Console.WriteLine("MainServer started at " + ipAddress.ToString() + ":" + Config.LocalPort.ToString()); ;
            while (true)
            {
                Socket clientSocket = socket.Accept();
                Console.WriteLine("Client connecting ...");
                ClientCount++;
                new Thread(() => SqlServer.Create(clientSocket)).Start();
            }
        }

        public static void LoadConfig()
        {
            string config = File.ReadAllText("wadbsrv.config.json");
            Config = JsonConvert.DeserializeObject<WadbsrvConfig>(config);
            ServerCertificate = new X509Certificate2(Config.PfxCertificatePath, Config.PfxPassword);
            DatabaseManager.connectionString = $"Data Source={Config.SQLiteDatabasePath}";
        }
    }
}