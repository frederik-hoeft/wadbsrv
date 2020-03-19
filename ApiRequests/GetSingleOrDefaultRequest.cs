using System;
using System.Collections.Generic;
using System.Text;
using washared.DatabaseServer.ApiResponses;
using wadbsrv.Database;
using washared.DatabaseServer;

namespace wadbsrv.ApiRequests
{
    public class GetSingleOrDefaultRequest : ApiRequest
    {
        public GetSingleOrDefaultRequest(RequestId requestId, string query, int expectedColumns)
        {
            RequestId = requestId;
            Query = query;
            ExpectedColumns = expectedColumns;
        }

        private GetSingleOrDefaultRequest(PackedApiRequest packedRequest)
        {
            RequestId = packedRequest.RequestId;
            Query = packedRequest.Query;
            ExpectedColumns = packedRequest.ExpectedColumns;
        }

        public static GetSingleOrDefaultRequest Create(PackedApiRequest packedRequest)
        {
            return new GetSingleOrDefaultRequest(packedRequest);
        }

        public override async void Process(SqlServer server)
        {
            string result = await DatabaseManager.GetSingleOrDefault(Query);
            GetSingleOrDefaultResponse response = GetSingleOrDefaultResponse.Create(result);
            SerializedApiResponse serializedApiResponse = SerializedApiResponse.Create(response);
            string data = serializedApiResponse.Serialize();
            server.Network.Send(data);
        }
    }
}
