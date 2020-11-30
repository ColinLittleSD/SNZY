using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY.Console
{
    public class PortfolioAPI
    {
        private string AuthorizationKey = "";

        public PortfolioAPI(string authorizationKey)
        {
            AuthorizationKey = authorizationKey;
        }

        public string GetAllPortfolio()
        {
            var client = new RestClient("https://localhost:44389/api/Portfolio");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {AuthorizationKey}");
            IRestResponse response = client.Execute(request);

            return response.Content;
        }

        public void PostAStockToMyPortfolio(int portfolioId, int stockId)
        {
            var client = new RestClient("https://localhost:44389/api/StockPortfolio/PostPortfolioStocks");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {AuthorizationKey}");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("PortfolioId", portfolioId);
            request.AddParameter("StockId", stockId);
            IRestResponse response = client.Execute(request);
        }

        public void DeleteAStockFromMyPortfolio(int stockId)
        {
            var client = new RestClient($"https://localhost:44389/api/StockPortfolio/RemovePortfolioStocks/{stockId}");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", $"Bearer {AuthorizationKey}");
            IRestResponse response = client.Execute(request);
        }

        public void PostAETFToMyPortfolio(int portfolioId, int etfId)
        {
            var client = new RestClient("https://localhost:44389/api/ETFPortfolio/PostPortfolioETFs");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {AuthorizationKey}");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("PortfolioId", portfolioId);
            request.AddParameter("ETFId", etfId);
            IRestResponse response = client.Execute(request);
        }

        public void DeleteETFFromMyPortfolio(int etfId)
        {
            var client = new RestClient($"https://localhost:44389/api/ETFPortfolio/RemovePortfolioETFs/{etfId}");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", $"Bearer {AuthorizationKey}");
            IRestResponse response = client.Execute(request);
        }
    }
}
