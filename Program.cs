using Newtonsoft.Json;
using System;
using System.Threading;
using wadbsrv.ApiRequests;
using washared.DatabaseServer;
using washared.DatabaseServer.ApiResponses;

namespace wadbsrv
{
    class Program
    {
        static void Main(string[] args)
        {
            MainServer.Run();
        }
    }
}
