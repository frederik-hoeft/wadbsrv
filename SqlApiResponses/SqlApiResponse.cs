using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv.SqlApiResponses
{
    public abstract class SqlApiResponse
    {
        public SqlResponseId ResponseId;

        /// <summary>
        /// Serializes the current object to a Json string.
        /// </summary>
        /// <returns>The current object as a JSON string.</returns>
        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
