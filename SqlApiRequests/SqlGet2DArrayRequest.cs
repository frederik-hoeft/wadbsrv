using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.SqlApiRequests
{
    public class SqlGet2DArrayRequest : SqlApiRequest
    {
        public SqlGet2DArrayRequest(SqlRequestId requestId, string query, int expectedColumns)
        {
            RequestId = requestId;
            Query = query;
            ExpectedColumns = expectedColumns;
        }

        private SqlGet2DArrayRequest(SqlPackedApiRequest packedRequest)
        {
            RequestId = packedRequest.SqlRequestId;
            Query = packedRequest.Query;
            ExpectedColumns = packedRequest.ExpectedColumns;
        }

        public static SqlGet2DArrayRequest Create(SqlPackedApiRequest packedRequest)
        {
            return new SqlGet2DArrayRequest(packedRequest);
        }

        public override void Process(SqlClient client)
        {
            throw new NotImplementedException();
        }
    }
}