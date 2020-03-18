using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.SqlApiResponses
{
    public class SqlGet2DArrayResponse : SqlApiResponse
    {
        public readonly string[][] Result;
        [Obsolete("Only used for JSON parsing. Use SqlGet2DArrayResponse.Create() instead.")]
        public SqlGet2DArrayResponse(SqlResponseId responseId, string[][] result)
        {
            ResponseId = responseId;
            Result = result;
        }

        public static SqlGet2DArrayResponse Create(string[][] result)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return new SqlGet2DArrayResponse(SqlResponseId.Get2DArray, result);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
