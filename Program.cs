using System.Diagnostics;
using wadbsrv.Database;

namespace wadbsrv
{
    internal static class Program
    {
        private static void Main()
        {
            Debug.WriteLine("Started main!");
            MainServer.LoadConfig();
            MainServer.Run();
        }
    }
}