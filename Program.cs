using Newtonsoft.Json;
using System;
using System.Threading;
using wadbsrv.SqlApiRequests;
using wadbsrv.SqlApiResponses;

namespace wadbsrv
{
    class Program
    {
        static void Main(string[] args)
        {
            string[][] result = new string[][] { new string[] { "lorem", "ipsum" }, new string[] { "dolor", "sit", "amet" } };
            SqlGet2DArrayResponse sqlGet2DArrayResponse = SqlGet2DArrayResponse.Create(result);
            SqlSerializedApiResponse serializedApiResponse = SqlSerializedApiResponse.Create(sqlGet2DArrayResponse);
            string json = serializedApiResponse.Serialize();
            SqlSerializedApiResponse serializedApiResponse1 = JsonConvert.DeserializeObject<SqlSerializedApiResponse>(json);
            SqlApiResponse sqlApiResponse = serializedApiResponse1.Deserialize();
            SqlGet2DArrayResponse finalResponse = (SqlGet2DArrayResponse)sqlApiResponse;
            Console.WriteLine("asdf");
        }
    }
}
