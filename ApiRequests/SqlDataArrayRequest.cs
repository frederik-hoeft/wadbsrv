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
        public SqlDataArrayRequest(RequestId requestId, string query, int expectedColumns)
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
            string[] result = await DatabaseManager.GetDataArray(Query, ExpectedColumns);
            SqlDataArrayResponse response = SqlDataArrayResponse.Create(result);
            SerializedApiResponse serializedApiResponse = SerializedApiResponse.Create(response);
            string data = serializedApiResponse.Serialize();
            if (server.Network == null)
            {
                throw new NullReferenceException();
            }
            server.Network.Send(data);
        }
    }
}
