using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.ApiResponses
{
    public class GetDataArrayResponse : ApiResponse
    {
        public readonly string[] Result;
        [Obsolete("Only used for JSON parsing. Use GetDataArrayResponse.Create() instead.")]
        public GetDataArrayResponse(ResponseId responseId, string[] result)
        {
            ResponseId = responseId;
            Result = result;
        }

        public static GetDataArrayResponse Create(string[] result)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return new GetDataArrayResponse(ResponseId.GetDataArray, result);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
