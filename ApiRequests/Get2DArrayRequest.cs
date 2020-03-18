using System;
using System.Collections.Generic;
using System.Text;
using wadbsrv.Database;
using wadbsrv.ApiResponses;

namespace wadbsrv.ApiRequests
{
    public class Get2DArrayRequest : ApiRequest
    {
        public Get2DArrayRequest(RequestId requestId, string query, int expectedColumns)
        {
            RequestId = requestId;
            Query = query;
            ExpectedColumns = expectedColumns;
        }

        private Get2DArrayRequest(PackedApiRequest packedRequest)
        {
            RequestId = packedRequest.RequestId;
            Query = packedRequest.Query;
            ExpectedColumns = packedRequest.ExpectedColumns;
        }

        public static Get2DArrayRequest Create(PackedApiRequest packedRequest)
        {
            return new Get2DArrayRequest(packedRequest);
        }

        public override async void Process(SqlServer server)
        {
            string[][] result = await DatabaseManager.GetDataAs2DArray(Query, ExpectedColumns);
            Get2DArrayResponse response = Get2DArrayResponse.Create(result);
            SerializedApiResponse serializedApiResponse = SerializedApiResponse.Create(response);
            string data = serializedApiResponse.Serialize();
            server.Network.Send(data);
        }
    }
}