using System;
using System.Collections.Generic;
using System.Text;

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

        public override void Process(SqlClient client)
        {
            throw new NotImplementedException();
        }
    }
}
