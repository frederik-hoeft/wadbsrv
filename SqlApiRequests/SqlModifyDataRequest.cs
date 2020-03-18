using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.SqlApiRequests
{
    public class SqlModifyDataRequest : SqlApiRequest
    {
        public SqlModifyDataRequest(SqlRequestId requestId, string query)
        {
            RequestId = requestId;
            Query = query;
            ExpectedColumns = 0;
        }

        private SqlModifyDataRequest(SqlPackedApiRequest packedRequest)
        {
            RequestId = packedRequest.SqlRequestId;
            Query = packedRequest.Query;
            ExpectedColumns = 0;
        }

        public static SqlModifyDataRequest Create(SqlPackedApiRequest packedRequest)
        {
            return new SqlModifyDataRequest(packedRequest);
        }

        public override void Process(SqlClient client)
        {
            throw new NotImplementedException();
        }
    }
}
