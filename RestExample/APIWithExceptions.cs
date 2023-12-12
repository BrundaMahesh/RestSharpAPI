using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RestExample
{
    public class APIWithExceptions
    {
        string baseUrl = "https://reqres.in/api/";
        public void GetSingleUser()
        { 
            var client = new RestClient(baseUrl);
            var request = new RestRequest("users/78", Method.Get);
            var response = client.Execute(request);

            if(!response.IsSuccessful)
            {
                try
                {
                    var errorDetails = JsonConvert.DeserializeObject
                        <ErrorResponse>(response.Content);
                    if(errorDetails!=null)
                    {
                        Console.WriteLine($"API Error: {errorDetails.Error}");
                    }
                }
                catch(JsonException)
                {
                    Console.WriteLine("Failed to deserialize error response");
                }
            }
            else
            {
                Console.WriteLine("Successful Response:");
                Console.WriteLine(response.Content);
            }

        }
    }
}
