using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace wadbsrv.ApiRequests
{
    /// <summary>
    /// Base class for all SQL API Requests
    /// </summary>
    public class ApiRequestBase
    {
        public RequestId RequestId;
        public string Query;
        public int ExpectedColumns;
    }
}
