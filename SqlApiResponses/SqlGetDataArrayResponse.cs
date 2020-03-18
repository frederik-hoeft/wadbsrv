using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.SqlApiResponses
{
    public class SqlGetDataArrayResponse : SqlApiResponse
    {
        public readonly string[] Result;
        [Obsolete("Only used for JSON parsing. Use SqlGetDataArrayResponse.Create() instead.")]
        public SqlGetDataArrayResponse(SqlResponseId responseId, string[] result)
        {
            ResponseId = responseId;
            Result = result;
        }

        public static SqlGetDataArrayResponse Create(string[] result)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return new SqlGetDataArrayResponse(SqlResponseId.GetDataArray, result);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
