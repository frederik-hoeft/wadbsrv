using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.SqlApiResponses
{
    public class SqlSerializedApiResponse
    {
        public readonly SqlResponseId SqlResponseId;
        public readonly string Json;
        [Obsolete("Only used for JSON parsing. Use SqlSerializedApiResponse.Create() instead.")]
        public SqlSerializedApiResponse(SqlResponseId sqlResponseId, string json)
        {
            SqlResponseId = sqlResponseId;
            Json = json;
        }

        public static SqlSerializedApiResponse Create(SqlApiResponse apiResponse)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return new SqlSerializedApiResponse(apiResponse.ResponseId, JsonConvert.SerializeObject(apiResponse));
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public SqlApiResponse Deserialize()
        {
            return SqlResponseId switch
            {
                SqlResponseId.Get2DArray => JsonConvert.DeserializeObject<SqlGet2DArrayResponse>(Json),
                SqlResponseId.GetDataArray => JsonConvert.DeserializeObject<SqlGetDataArrayResponse>(Json),
                SqlResponseId.GetSingleOrDefault => JsonConvert.DeserializeObject<SqlGetSingleOrDefaultResponse>(Json),
                SqlResponseId.ModifyData => JsonConvert.DeserializeObject<SqlModifyDataResponse>(Json),
                _ => null
            };
        }
    }
    public enum SqlResponseId
    {
        Get2DArray = 0,
        GetDataArray = 1,
        GetSingleOrDefault = 2,
        ModifyData = 3
    }
}
