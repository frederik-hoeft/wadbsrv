﻿using wadbsrv.Database;
using washared.DatabaseServer;
using washared.DatabaseServer.ApiResponses;

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
            SqlPacket packet = await DatabaseManager.GetSingleOrDefault(Query);
            ApiResponse response;
            if (packet.Success)
            {
                string result = (string)packet.Data;
                response = SqlSingleOrDefaultResponse.Create(result);
            }
            else
            {
                response = SqlErrorResponse.Create(packet.ErrorMessage);
            }
            SerializedSqlApiResponse serializedApiResponse = SerializedSqlApiResponse.Create(response);
            string data = serializedApiResponse.Serialize();
            server.Network.Send(data);
        }
    }
}