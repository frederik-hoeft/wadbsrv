using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.SqlApiRequests
{
    public class SqlGetDataArrayRequest : SqlApiRequest
    {
        public SqlGetDataArrayRequest(SqlRequestId requestId, string query, int expectedColumns)
        {
            RequestId = requestId;
            Query = query;
            ExpectedColumns = expectedColumns;
        }

        private SqlGetDataArrayRequest(SqlPackedApiRequest packedRequest)
        {
            RequestId = packedRequest.SqlRequestId;
            Query = packedRequest.Query;
            ExpectedColumns = packedRequest.ExpectedColumns;
        }

        public static SqlGetDataArrayRequest Create(SqlPackedApiRequest packedRequest)
        {
            return new SqlGetDataArrayRequest(packedRequest);
        }

        public override void Process(SqlClient client)
        {
            throw new NotImplementedException();
        }
    }
}
