using System;
using System.Collections.Generic;
using System.Text;
using washared.DatabaseServer.ApiResponses;
using wadbsrv.Database;
using washared.DatabaseServer;

namespace wadbsrv.ApiRequests
{
    public class SqlDataArrayRequest : ApiRequest
    {
        public SqlDataArrayRequest(SqlRequestId requestId, string query, int expectedColumns)
        {
            RequestId = requestId;
            Query = query;
            ExpectedColumns = expectedColumns;
        }

        private SqlDataArrayRequest(PackedApiRequest packedRequest)
        {
            RequestId = packedRequest.RequestId;
            Query = packedRequest.Query;
            ExpectedColumns = packedRequest.ExpectedColumns;
        }

        public static SqlDataArrayRequest Create(PackedApiRequest packedRequest)
        {
            return new SqlDataArrayRequest(packedRequest);
        }

        public override async void Process(SqlServer server)
        {
            SqlPacket packet = await DatabaseManager.GetDataArray(Query, ExpectedColumns);
            ApiResponse response;
            if (packet.Success)
            {
                string[] result = (string[])packet.Data;
                response = SqlDataArrayResponse.Create(result);
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
