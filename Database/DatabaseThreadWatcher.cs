using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace wadbsrv.Database
{
    public class DatabaseThreadWatcher : IDisposable
    {
        private static readonly Mutex mutex = new Mutex();
        public DatabaseThreadWatcher()
        {
            mutex.WaitOne();
        }
        public void Dispose()
        {
            mutex.ReleaseMutex();
        }
    }
}
