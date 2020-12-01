using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Console
{
    public class ETFAPI
    {
        private string AuthorizationKey = "";

        public ETFAPI(string authorizationKey)
        {
            AuthorizationKey = authorizationKey;
        }

        public string GetAllETF()
        {
            var client = new RestClient("https://localhost:44389/api/ETF");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {AuthorizationKey}");
            IRestResponse response = client.Execute(request);

            return response.Content;
        }
    }
}
