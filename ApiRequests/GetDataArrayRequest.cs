using System;
using System.Collections.Generic;
using System.Text;
using washared.DatabaseServer.ApiResponses;
using wadbsrv.Database;
using washared.DatabaseServer;

namespace wadbsrv.ApiRequests
{
    public class GetDataArrayRequest : ApiRequest
    {
        public GetDataArrayRequest(RequestId requestId, string query, int expectedColumns)
        {
            RequestId = requestId;
            Query = query;
            ExpectedColumns = expectedColumns;
        }

        private GetDataArrayRequest(PackedApiRequest packedRequest)
        {
            RequestId = packedRequest.RequestId;
            Query = packedRequest.Query;
            ExpectedColumns = packedRequest.ExpectedColumns;
        }

        public static GetDataArrayRequest Create(PackedApiRequest packedRequest)
        {
            return new GetDataArrayRequest(packedRequest);
        }

        public override async void Process(SqlServer server)
        {
            string[] result = await DatabaseManager.GetDataArray(Query, ExpectedColumns);
            GetDataArrayResponse response = GetDataArrayResponse.Create(result);
            SerializedApiResponse serializedApiResponse = SerializedApiResponse.Create(response);
            string data = serializedApiResponse.Serialize();
            server.Network.Send(data);
        }
    }
}
