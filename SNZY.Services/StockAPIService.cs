using Newtonsoft.Json;
using RestSharp;
using SNZY.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Services
{
    public class StockAPIService
    {

        public StockAPIService()
        {

        }

        public string CallAPI()
        {
            string apiKey = "a6ae3f3429144b7fa3160c590b1c81b1";

            List<string> listOfTickers = new List<string>() { "AMD", "TSLA", "AAPL", "TWTR", "NFLX" };

            List<StockAPIModel> listOfStocks = new List<StockAPIModel>();

            foreach (string ticker in listOfTickers)
            {
                var client = new RestClient($"https://api.twelvedata.com/time_series?symbol={ticker}&interval=1min&apikey={apiKey}&source=docs");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                var myDeserializedClass = JsonConvert.DeserializeObject<StockAPIModel>(response.Content);

                listOfStocks.Add(myDeserializedClass);
            }

            var listOfStocksInJson = JsonConvert.SerializeObject(listOfStocks);

            return listOfStocksInJson;
        }


    }
}
