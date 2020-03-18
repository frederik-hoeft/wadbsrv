using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.ApiRequests
{
    /// <summary>
    /// Base class for specific SQL API Request implementations
    /// </summary>
    public abstract class ApiRequest : ApiRequestBase
    {
        public abstract void Process(SqlServer server);
    }
}
