using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using wadbsrv.ApiRequests;
using washared;

namespace wadbsrv
{
    public class SqlServer: NetworkInterface, IDisposable
    {
        public override Network Network { get => base.Network; }
        public override SslStream SslStream { get => base.SslStream; }
        private readonly NetworkStream networkStream;
        private readonly Socket socket;

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None || MainServer.Config.SuppressCertificateErrors)
            {
                return true;
            }

            Debug.WriteLine("Certificate error: {0}", sslPolicyErrors);
            return false;
        }

        private SqlServer(Socket socket)
        {
            this.socket = socket;
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveTime, 10);
            socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.TcpKeepAliveInterval, 5);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.TcpKeepAliveRetryCount, 6);
            networkStream = new NetworkStream(socket);
            SslStream = new SslStream(networkStream, false, new RemoteCertificateValidationCallback(ValidateServerCertificate));
            SslStream.AuthenticateAsServer(MainServer.ServerCertificate, true, System.Security.Authentication.SslProtocols.None, true);
            Network = new Network(this);
        }
#nullable enable
        public static void Create(Socket? socket)
        {
            if (socket == null)
            {
                MainServer.ClientCount--;
                return;
            }
            SqlServer server = new SqlServer(socket);
            server.Serve();
        }

        private void Serve()
        {
            Console.WriteLine("Client connected.");
            using PacketParser parser = new PacketParser(this)
            {
                PacketActionCallback = PacketActionCallback,
                UseMultiThreading = true,
                ReleaseResources = false,
                Interactive = false
            };
            try
            {
                parser.BeginParse();
            }
            catch (ConnectionDroppedException)
            {
                Debug.WriteLine("Connection dropped.");
            }
            parser.Dispose();
            Dispose();
            Console.WriteLine("Client disconnected.");
        }

        private void PacketActionCallback(byte[] packet)
        {
            Debug.WriteLine("PacketActionCallback!");
            string json = Encoding.UTF8.GetString(packet);
            PackedApiRequest packedApiRequest = JsonConvert.DeserializeObject<PackedApiRequest>(json);
            ApiRequest apiRequest = packedApiRequest.Unpack();
            apiRequest.Process(this);
        }

        public void Dispose()
        {
            MainServer.ClientCount--;
            try
            {
                SslStream.Close();
                SslStream.Dispose();
            }
            catch (ObjectDisposedException) { }
            try
            {
                networkStream.Close();
                networkStream.Dispose();
            }
            catch (ObjectDisposedException) { }
            try
            {
                if (socket.Connected)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Disconnect(false);
                }
                socket.Close();
            }
            catch (ObjectDisposedException) { }
        }
    }
}
