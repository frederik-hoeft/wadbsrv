using System;
using System.Collections.Generic;
using System.Text;
using washared.DatabaseServer.ApiResponses;
using wadbsrv.Database;
using washared.DatabaseServer;

namespace wadbsrv.ApiRequests
{
    public class SqlModifyDataRequest : ApiRequest
    {
        public SqlModifyDataRequest(RequestId requestId, string query)
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
            int result = await DatabaseManager.ModifyData(Query);
            SqlModifyDataResponse response = SqlModifyDataResponse.Create(result);
            SerializedApiResponse serializedApiResponse = SerializedApiResponse.Create(response);
            string data = serializedApiResponse.Serialize();
            server.Network.Send(data);
        }
    }
}
