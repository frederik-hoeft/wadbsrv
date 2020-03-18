using System;
using System.Collections.Generic;
using System.Text;
using wadbsrv.Database;
using wadbsrv.SqlApiResponses;

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

        public override async void Process(SqlClient client)
        {
            string[][] result = await DatabaseManager.GetDataAs2DArray(Query, ExpectedColumns);
            SqlGet2DArrayResponse get2DArrayResponse = SqlGet2DArrayResponse.Create(result);
            SqlSerializedApiResponse serializedApiResponse = SqlSerializedApiResponse.Create(get2DArrayResponse);
        }
    }
}