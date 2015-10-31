using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace ABConsole
{
    public class RestHelper
    {
        public static IRestResponse PostBasicAuthenication(string baseUrl, string resourceUrl, string userName, string password, string json)
        {
            var client = new RestClient(baseUrl);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);
            var request = new RestRequest(resourceUrl, Method.POST);
            request.AddParameter("text/json", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            return client.Execute(request);
        }
        public static IRestResponse Post(string baseUrl, string resourceUrl, string json)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resourceUrl, Method.POST);
            request.AddParameter("seria", json);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("A", "foo");
            request.AddParameter("B", "bar");
            return client.Execute(request);
        }
        public static string ConvertObjectToJason<T>(T arg)
        {
            return JsonConvert.SerializeObject(arg);
        }
        public static BL.OrderBO.JsonResponse  GetResponseObject(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<BL.OrderBO.JsonResponse>(response.Content);
        }
    }
}
