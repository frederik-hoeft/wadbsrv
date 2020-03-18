﻿using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.ApiResponses
{
    public class ModifyDataResponse : ApiResponse
    {
        public readonly int Result;
        [Obsolete("Only used for JSON parsing. Use ModifyDataResponse.Create() instead.")]
        public ModifyDataResponse(ResponseId responseId, int result, bool success)
        {
            ResponseId = responseId;
            Result = result;
            Success = success;
        }
        // TODO: figure out what that int even indicates
        public static ModifyDataResponse Create(int result)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return new ModifyDataResponse(ResponseId.ModifyData, result, true);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
