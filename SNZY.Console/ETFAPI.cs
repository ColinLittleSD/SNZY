using RestSharp;
using System;
using System.Net;

namespace SNZY.Console
{
    public class ETFAPI
    {
        private string AuthorizationKey = "";

        public ETFAPI(string authorizationKey)
        {
            AuthorizationKey = authorizationKey;
        }

        public (string responseContent, string errorMessage) GetAllETF()
        {
            string responseContent = "";
            string errorMessage = "";

            try
            {
                var client = new RestClient("https://localhost:44389/api/ETF");
                client.Timeout = -1;
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
    }
}
