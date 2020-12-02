using Newtonsoft.Json;
using RestSharp;
using SNZY.Models.APIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Models.StockPortfolio
{
    public class StockPortfolioListItem
    {
        public int StockId { get; set; }
        public string StockName { get; set; }
        public string Ticker { get; set; }
        //public ShareInfoModel.Value Model { get { return GetStockInfo(Ticker); } }
        //public double Price { get { return GetStockPriceWithOutAsync(Ticker); } }
        //public string Open { get { return Model.values[0].open; } }

        //private static ShareInfoModel.Value GetStockInfo(string ticker)
        //{
        //    string apiKey = "56885b95ab374bfba5284bf37ba5c869";

        //    var client = new RestClient($"https://api.twelvedata.com/time_series?symbol={ticker}&interval=1min&apikey={apiKey}&source=docs");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.GET);
        //    IRestResponse response = client.Execute(request);
        //    //Just return item [0]
        //    var myDeserializedClass = JsonConvert.DeserializeObject<ShareInfoModel>(response.Content);

        //    return myDeserializedClass.values[0];
        //}

        //public double GetStockPriceWithOutAsync(string ticker)
        //{
        //    string apiKey = "56885b95ab374bfba5284bf37ba5c869";

        //    var client = new RestClient($"https://api.twelvedata.com/price?symbol={ticker}&apikey={apiKey}&source=docs");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.GET);
        //    IRestResponse response = client.Execute(request);

        //    var myDeserializedClass = JsonConvert.DeserializeObject<SharePriceModel>(response.Content);
        //    double priceInDouble = Convert.ToDouble(myDeserializedClass.price);
        //    return priceInDouble;
        //}
    }
}
