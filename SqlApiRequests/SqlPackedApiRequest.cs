using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.SqlApiRequests
{
    /// <summary>
    /// SQL API Request wrapper class. Can hold any SQL API Request.
    /// </summary>
    public class SqlPackedApiRequest
    {
        public readonly SqlRequestId SqlRequestId;
        public readonly string Query;
        public readonly int ExpectedColumns;

        [Obsolete("Only used for JSON parsing. Use SqlPackedApiRequest.Pack() instead.")]
        public SqlPackedApiRequest(SqlRequestId sqlRequestId, string query, int expectedColumns)
        {
            SqlRequestId = sqlRequestId;
            Query = query;
            ExpectedColumns = expectedColumns;
        }

        public static SqlPackedApiRequest Pack(SqlApiRequestBase baseRequest)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return new SqlPackedApiRequest(baseRequest.RequestId, baseRequest.Query, baseRequest.ExpectedColumns);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public SqlApiRequest Unpack()
        {
            return SqlRequestId switch
            {
                SqlRequestId.Get2DArray => SqlGet2DArrayRequest.Create(this),
                SqlRequestId.GetDataArray => SqlGetDataArrayRequest.Create(this),
                SqlRequestId.GetSingleOrDefault => SqlGetSingleOrDefaultRequest.Create(this),
                SqlRequestId.ModifyData => SqlModifyDataRequest.Create(this),
                _ => null
            };
        }
    }
    public enum SqlRequestId
    {
        Get2DArray = 0,
        GetDataArray = 1,
        GetSingleOrDefault = 2,
        ModifyData = 3
    }
}
