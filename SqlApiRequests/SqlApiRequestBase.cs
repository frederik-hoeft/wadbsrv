using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace wadbsrv.SqlApiRequests
{
    /// <summary>
    /// Base class for all SQL API Requests
    /// </summary>
    public class SqlApiRequestBase
    {
        public SqlRequestId RequestId;
        public string Query;
        public int ExpectedColumns;
    }
}
