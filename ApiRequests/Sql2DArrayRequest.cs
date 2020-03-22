using System;
using System.Collections.Generic;
using System.Text;
using wadbsrv.Database;
using washared.DatabaseServer;
using washared.DatabaseServer.ApiResponses;

namespace wadbsrv.ApiRequests
{
    public class Sql2DArrayRequest : ApiRequest
    {
        public Sql2DArrayRequest(SqlRequestId requestId, string query, int expectedColumns)
        {
            RequestId = requestId;
            Query = query;
            ExpectedColumns = expectedColumns;
        }

        private Sql2DArrayRequest(PackedApiRequest packedRequest)
        {
            RequestId = packedRequest.RequestId;
            Query = packedRequest.Query;
            ExpectedColumns = packedRequest.ExpectedColumns;
        }

        public static Sql2DArrayRequest Create(PackedApiRequest packedRequest)
        {
            return new Sql2DArrayRequest(packedRequest);
        }

        public override async void Process(SqlServer server)
        {
            SqlPacket packet = await DatabaseManager.GetDataAs2DArray(Query, ExpectedColumns);
            ApiResponse response;
            if (packet.Success)
            {
                string[][] result = (string[][])packet.Data;
                response = Sql2DArrayResponse.Create(result);
            }
            else
            {
                response = SqlErrorResponse.Create(packet.ErrorMessage);
            }
            SerializedApiResponse serializedApiResponse = SerializedApiResponse.Create(response);
            string data = serializedApiResponse.Serialize();
            server.Network.Send(data);
        }
    }
}