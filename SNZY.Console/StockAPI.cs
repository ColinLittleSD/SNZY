using Newtonsoft.Json;
using RestSharp;
using SNZY.Models;
using SNZY.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Console
{
    public class StockAPI
    {
        private string AuthorizationKey = "";

        public StockAPI(string authorizationKey)
        {
            AuthorizationKey = authorizationKey;
        }

        public string GetAllStocks()
        {
            var client = new RestClient("https://localhost:44389/api/Stock");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {AuthorizationKey}");
            IRestResponse response = client.Execute(request);

            return response.Content;
        }
    }
}
