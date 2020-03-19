using System;
using System.Collections.Generic;
using System.Text;
using washared.DatabaseServer.ApiResponses;
using wadbsrv.Database;
using washared.DatabaseServer;

namespace wadbsrv.ApiRequests
{
    public class ModifyDataRequest : ApiRequest
    {
        public ModifyDataRequest(RequestId requestId, string query)
        {
            RequestId = requestId;
            Query = query;
            ExpectedColumns = 0;
        }

        private ModifyDataRequest(PackedApiRequest packedRequest)
        {
            RequestId = packedRequest.RequestId;
            Query = packedRequest.Query;
            ExpectedColumns = 0;
        }

        public static ModifyDataRequest Create(PackedApiRequest packedRequest)
        {
            return new ModifyDataRequest(packedRequest);
        }

        public override async void Process(SqlServer server)
        {
            int result = await DatabaseManager.ModifyData(Query);
            ModifyDataResponse response = ModifyDataResponse.Create(result);
            SerializedApiResponse serializedApiResponse = SerializedApiResponse.Create(response);
            string data = serializedApiResponse.Serialize();
            server.Network.Send(data);
        }
    }
}
