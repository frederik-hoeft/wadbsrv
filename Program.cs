using System.Diagnostics;
using wadbsrv.Database;

namespace wadbsrv
{
    internal class Program
    {
        private static void Main()
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