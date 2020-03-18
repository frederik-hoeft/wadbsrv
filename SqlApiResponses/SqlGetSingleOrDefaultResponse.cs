using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.SqlApiResponses
{
    public class SqlGetSingleOrDefaultResponse : SqlApiResponse
    {
        public readonly string Result;
        public readonly bool Success;
        [Obsolete("Only used for JSON parsing. Use SqlGetSingleOrDefaultResponse.Create() instead.")]
        public SqlGetSingleOrDefaultResponse(SqlResponseId responseId, string result, bool success)
        {
            ResponseId = responseId;
            Result = result;
            Success = success;
        }

        public static SqlGetSingleOrDefaultResponse Create(string result)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return new SqlGetSingleOrDefaultResponse(SqlResponseId.GetSingleOrDefault, result, !result.Equals(string.Empty));
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
