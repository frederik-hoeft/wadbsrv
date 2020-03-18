using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.SqlApiRequests
{
    /// <summary>
    /// Base class for specific SQL API Request implementations
    /// </summary>
    public abstract class SqlApiRequest : SqlApiRequestBase
    {
        public abstract void Process(SqlClient client);
    }
}
