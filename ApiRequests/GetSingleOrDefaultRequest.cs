using System;
using System.Collections.Generic;
using System.Text;

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

        public override void Process(SqlClient client)
        {
            throw new NotImplementedException();
        }
    }
}
