﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.ApiResponses
{
    public class SerializedApiResponse
    {
        public readonly ResponseId ResponseId;
        public readonly string Json;
        [Obsolete("Only used for JSON parsing. Use SerializedApiResponse.Create() instead.")]
        public SerializedApiResponse(ResponseId sqlResponseId, string json)
        {
            ResponseId = sqlResponseId;
            Json = json;
        }

        public static SerializedApiResponse Create(ApiResponse apiResponse)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return new SerializedApiResponse(apiResponse.ResponseId, JsonConvert.SerializeObject(apiResponse));
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public ApiResponse Deserialize()
        {
            return ResponseId switch
            {
                ResponseId.Get2DArray => JsonConvert.DeserializeObject<Get2DArrayResponse>(Json),
                ResponseId.GetDataArray => JsonConvert.DeserializeObject<GetDataArrayResponse>(Json),
                ResponseId.GetSingleOrDefault => JsonConvert.DeserializeObject<GetSingleOrDefaultResponse>(Json),
                ResponseId.ModifyData => JsonConvert.DeserializeObject<ModifyDataResponse>(Json),
                _ => null
            };
        }
    }
    public enum ResponseId
    {
        Get2DArray = 0,
        GetDataArray = 1,
        GetSingleOrDefault = 2,
        ModifyData = 3
    }
}
