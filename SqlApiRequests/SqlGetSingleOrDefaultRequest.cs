using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.SqlApiRequests
{
    public class SqlGetSingleOrDefaultRequest : SqlApiRequest
    {
        public SqlGetSingleOrDefaultRequest(SqlRequestId requestId, string query, int expectedColumns)
        {
            RequestId = requestId;
            Query = query;
            ExpectedColumns = expectedColumns;
        }

        private SqlGetSingleOrDefaultRequest(SqlPackedApiRequest packedRequest)
        {
            RequestId = packedRequest.SqlRequestId;
            Query = packedRequest.Query;
            ExpectedColumns = packedRequest.ExpectedColumns;
        }

        public static SqlGetSingleOrDefaultRequest Create(SqlPackedApiRequest packedRequest)
        {
            return new SqlGetSingleOrDefaultRequest(packedRequest);
        }

        public override void Process(SqlClient client)
        {
            throw new NotImplementedException();
        }
    }
}
