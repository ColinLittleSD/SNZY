using RestSharp;
using System;
using System.Net;

namespace SNZY.Console
{
    public class PortfolioAPI
    {
        private string AuthorizationKey = "";

        public PortfolioAPI(string authorizationKey)
        {
            AuthorizationKey = authorizationKey;
        }

        public (string responseContent, string errorMessage) GetAllPortfolio()
        {
            string responseContent = "";
            string errorMessage = "";

            try
            {
                var client = new RestClient("https://localhost:44389/api/Portfolio");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", $"Bearer {AuthorizationKey}");
                IRestResponse response = client.Execute(request);


                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error Occured: {response.ErrorMessage}");
                }

                responseContent = response.Content;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return (responseContent, errorMessage);
        }

        public string PostAStockToMyPortfolio(int portfolioId, int stockId)
        {
            string errorMessage = "";

            try
            {
                var client = new RestClient("https://localhost:44389/api/StockPortfolio/PostPortfolioStocks");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Bearer {AuthorizationKey}");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("PortfolioId", portfolioId);
                request.AddParameter("StockId", stockId);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error Occured: {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return errorMessage;
        }

        public string DeleteAStockFromMyPortfolio(int stockId)
        {
            string errorMessage = "";

            try
            {
                var client = new RestClient($"https://localhost:44389/api/StockPortfolio/RemovePortfolioStocks/{stockId}");
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("Authorization", $"Bearer {AuthorizationKey}");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error Occured: {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return errorMessage;
        }

        public string PostAETFToMyPortfolio(int portfolioId, int etfId)
        {
            string errorMessage = "";

            try
            {

                var client = new RestClient("https://localhost:44389/api/ETFPortfolio/PostPortfolioETFs");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Bearer {AuthorizationKey}");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("PortfolioId", portfolioId);
                request.AddParameter("ETFId", etfId);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error Occured: {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return errorMessage;
        }

        public string DeleteETFFromMyPortfolio(int etfId)
        {
            string errorMessage = "";

            try
            {
                var client = new RestClient($"https://localhost:44389/api/ETFPortfolio/RemovePortfolioETFs/{etfId}");
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("Authorization", $"Bearer {AuthorizationKey}");
                IRestResponse response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error Occured: {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            return errorMessage;
        }
    }
}
