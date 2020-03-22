using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading;
using wadbsrv.ApiRequests;
using wadbsrv.Database;
using washared.DatabaseServer;
using washared.DatabaseServer.ApiResponses;

namespace wadbsrv
{
    class Program
    {
        static void Main()
        {
            Debug.WriteLine("Started main!");
            MainServer.LoadConfig();
            MainServer.Run();
        }

        public static async void asdf()
        {
            _ = await DatabaseManager.ModifyData("INSERT INTO Tbl_user (password, hid, email) VALUES (\'asdf\',\'asdf\',\'asdf\');");
        }
    } 
}
