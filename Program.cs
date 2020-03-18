using Newtonsoft.Json;
using System;
using System.Threading;
using wadbsrv.ApiRequests;
using wadbsrv.ApiResponses;

namespace wadbsrv
{
    class Program
    {
        static void Main(string[] args)
        {
            string[][] result = new string[][] { new string[] { "lorem", "ipsum" }, new string[] { "dolor", "sit", "amet" } };
            Get2DArrayResponse sqlGet2DArrayResponse = Get2DArrayResponse.Create(result);
            SerializedApiResponse serializedApiResponse = SerializedApiResponse.Create(sqlGet2DArrayResponse);
            string json = serializedApiResponse.Serialize();
            SerializedApiResponse serializedApiResponse1 = JsonConvert.DeserializeObject<SerializedApiResponse>(json);
            ApiResponse sqlApiResponse = serializedApiResponse1.Deserialize();
            Get2DArrayResponse finalResponse = (Get2DArrayResponse)sqlApiResponse;
            Console.WriteLine("asdf");
        }
    }
}
