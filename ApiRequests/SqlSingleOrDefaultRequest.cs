using System;
using System.Collections.Generic;
using System.Text;
using washared.DatabaseServer.ApiResponses;
using wadbsrv.Database;
using washared.DatabaseServer;

namespace wadbsrv.ApiRequests
{
    public class SqlSingleOrDefaultRequest : ApiRequest
    {
        public SqlSingleOrDefaultRequest(SqlRequestId requestId, string query, int expectedColumns)
        {
            RequestId = requestId;
            Query = query;
            ExpectedColumns = expectedColumns;
        }

        private SqlSingleOrDefaultRequest(PackedApiRequest packedRequest)
        {
            RequestId = packedRequest.RequestId;
            Query = packedRequest.Query;
            ExpectedColumns = packedRequest.ExpectedColumns;
        }

        public static SqlSingleOrDefaultRequest Create(PackedApiRequest packedRequest)
        {
            return new SqlSingleOrDefaultRequest(packedRequest);
        }

        public override async void Process(SqlServer server)
        {
            string result = await DatabaseManager.GetSingleOrDefault(Query);
            SqlSingleOrDefaultResponse response = SqlSingleOrDefaultResponse.Create(result);
            SerializedApiResponse serializedApiResponse = SerializedApiResponse.Create(response);
            string data = serializedApiResponse.Serialize();
            server.Network.Send(data);
        }
    }
}
