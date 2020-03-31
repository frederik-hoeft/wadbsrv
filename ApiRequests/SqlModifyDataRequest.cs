using wadbsrv.Database;
using washared.DatabaseServer;
using washared.DatabaseServer.ApiResponses;

namespace wadbsrv.ApiRequests
{
    public class SqlModifyDataRequest : ApiRequest
    {
        public SqlModifyDataRequest(SqlRequestId requestId, string query)
        {
            RequestId = requestId;
            Query = query;
            ExpectedColumns = 0;
        }

        private SqlModifyDataRequest(PackedApiRequest packedRequest)
        {
            RequestId = packedRequest.RequestId;
            Query = packedRequest.Query;
            ExpectedColumns = 0;
        }

        public static SqlModifyDataRequest Create(PackedApiRequest packedRequest)
        {
            return new SqlModifyDataRequest(packedRequest);
        }

        public override async void Process(SqlServer server)
        {
            SqlPacket packet = await DatabaseManager.ModifyData(Query);
            ApiResponse response;
            if (packet.Success)
            {
                int result = (int)packet.Data;
                response = SqlModifyDataResponse.Create(result);
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