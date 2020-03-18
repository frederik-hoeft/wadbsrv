using System;
using System.Collections.Generic;
using System.Text;

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

        public override void Process(SqlClient client)
        {
            throw new NotImplementedException();
        }
    }
}
