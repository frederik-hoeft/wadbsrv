using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.SqlApiResponses
{
    public class SqlModifyDataResponse : SqlApiResponse
    {
        public readonly int Result;
        [Obsolete("Only used for JSON parsing. Use SqlModifyDataResponse.Create() instead.")]
        public SqlModifyDataResponse(SqlResponseId responseId, int result)
        {
            ResponseId = responseId;
            Result = result;
        }

        public static SqlModifyDataResponse Create(int result)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return new SqlModifyDataResponse(SqlResponseId.ModifyData, result);
#pragma warning restore CS0618 // Type or member is obsolete
        }
    }
}
