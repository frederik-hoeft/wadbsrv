﻿using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.ApiRequests
{
    /// <summary>
    /// SQL API Request wrapper class. Can hold any SQL API Request.
    /// </summary>
    public class PackedApiRequest
    {
        public readonly RequestId RequestId;
        public readonly string Query;
        public readonly int ExpectedColumns;

        [Obsolete("Only used for JSON parsing. Use PackedApiRequest.Pack() instead.")]
        public PackedApiRequest(RequestId sqlRequestId, string query, int expectedColumns)
        {
            RequestId = sqlRequestId;
            Query = query;
            ExpectedColumns = expectedColumns;
        }

        public static PackedApiRequest Pack(ApiRequestBase baseRequest)
        {
#pragma warning disable CS0618 // Type or member is obsolete
            return new PackedApiRequest(baseRequest.RequestId, baseRequest.Query, baseRequest.ExpectedColumns);
#pragma warning restore CS0618 // Type or member is obsolete
        }

        public ApiRequest Unpack()
        {
            return RequestId switch
            {
                RequestId.Get2DArray => Get2DArrayRequest.Create(this),
                RequestId.GetDataArray => GetDataArrayRequest.Create(this),
                RequestId.GetSingleOrDefault => GetSingleOrDefaultRequest.Create(this),
                RequestId.ModifyData => ModifyDataRequest.Create(this),
                _ => null
            };
        }
    }
    public enum RequestId
    {
        Get2DArray = 0,
        GetDataArray = 1,
        GetSingleOrDefault = 2,
        ModifyData = 3
    }
}
