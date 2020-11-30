using Newtonsoft.Json;
using RestSharp;
using SNZY.Models;
using SNZY.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Services
{
    public class ShareAPIService
    {

        public ShareAPIService()
        {

        }


        public async Task<ShareInfoModel> GetStockInfo(string ticker)
        {
            string apiKey = "a6ae3f3429144b7fa3160c590b1c81b1";

            var client = new RestClient($"https://api.twelvedata.com/time_series?symbol={ticker}&interval=1min&apikey={apiKey}&source=docs");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            var myDeserializedClass = JsonConvert.DeserializeObject<ShareInfoModel>(response.Content);

            return myDeserializedClass;
        }


        public async Task<SharePriceModel> GetStockPrice(string ticker)
        {
            string apiKey = "a6ae3f3429144b7fa3160c590b1c81b1";

            var client = new RestClient($"https://api.twelvedata.com/price?symbol={ticker}&apikey={apiKey}&source=docs");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);

            var myDeserializedClass = JsonConvert.DeserializeObject<SharePriceModel>(response.Content);

            return myDeserializedClass;
        }

        public double GetStockPriceWithOutAsync(string ticker)
        {
            string apiKey = "a6ae3f3429144b7fa3160c590b1c81b1";

            var client = new RestClient($"https://api.twelvedata.com/price?symbol={ticker}&apikey={apiKey}&source=docs");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            var myDeserializedClass = JsonConvert.DeserializeObject<SharePriceModel>(response.Content);
            double priceInDouble = Convert.ToDouble(myDeserializedClass.price);
            return priceInDouble;
        }
    }
}
