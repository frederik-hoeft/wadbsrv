﻿using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.ApiResponses
{
    public class GetSingleOrDefaultResponse : ApiResponse
    {
        public readonly string Result;
        public readonly bool Success;
        [Obsolete("Only used for JSON parsing. Use GetSingleOrDefaultResponse.Create() instead.")]
        public GetSingleOrDefaultResponse(ResponseId responseId, string result, bool success)
        {
            ResponseId = responseId;
            Result = result;
            Success = success;
        }

        public static GetSingleOrDefaultResponse Create(string result)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return new GetSingleOrDefaultResponse(ResponseId.GetSingleOrDefault, result, !result.Equals(string.Empty));
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
