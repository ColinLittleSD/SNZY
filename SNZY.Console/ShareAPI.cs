using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SNZY.Models;
using SNZY.Models.APIModels;
using System;
using System.Net;

namespace SNZY.Console
{
    public class ShareAPI
    {
        public (double price, string errorMessage) GetSharePrice(string ticker)
        {
            SharePriceModel myDeserializedClass = null;
            double priceInDouble = 0;
            
            string apiKey = "a6ae3f3429144b7fa3160c590b1c81b1";
            string baseUri = $"https://api.twelvedata.com/price?symbol={ticker}&apikey={apiKey}&source=docs";

            var apiResult = CallShareAPI(baseUri);

            string responseContent = apiResult.responseContent;
            string errorMessage = apiResult.errorMessage;

            //if there is no error message
            if (errorMessage == "")
            {
                myDeserializedClass = JsonConvert.DeserializeObject<SharePriceModel>(responseContent);
                priceInDouble = Convert.ToDouble(myDeserializedClass.price);
            }

            return (priceInDouble, errorMessage);
        }


        public (ShareInfoModel.Value values, string errorMessage) GetShareInfo(string ticker)
        {
            ShareInfoModel myDeserializedClass = null;
            ShareInfoModel.Value values = null;

            string apiKey = "56885b95ab374bfba5284bf37ba5c869";
            string baseUri = $"https://api.twelvedata.com/time_series?symbol={ticker}&interval=1min&apikey={apiKey}&source=docs";

            var apiResult = CallShareAPI(baseUri);

            string responseContent = apiResult.responseContent;
            string errorMessage = apiResult.errorMessage;

            //if there is no error message
            if (errorMessage == "")
            {
                myDeserializedClass = JsonConvert.DeserializeObject<ShareInfoModel>(responseContent);
                values = myDeserializedClass.values[0];
            }

            return (values, errorMessage);
        }

        public (string responseContent, string errorMessage) CallShareAPI(string baseUri)
        {
            string responseContent = "";
            IRestResponse response = null;
            string errorMessage = "";

            try
            {
                var client = new RestClient(baseUri);
                var request = new RestRequest(Method.GET);
                response = client.Execute(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception($"Error Occured: {response.ErrorMessage}");
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

            //if there is no error message
            if (errorMessage == "")
            {
                var responseContentJObject = JObject.Parse(response.Content);
                var errorCode = responseContentJObject["code"];

                //If there is no error code
                if (errorCode == null)
                {
                    responseContent = response.Content;
                }
                else
                {
                    if ((string)errorCode == "400")
                    {
                        errorMessage = "Invalid ticker.";
                    }
                    else if ((string)errorCode == "429")
                    {
                        errorMessage = "API Key minute limit is reached.";
                    }
                    else
                    {
                        errorMessage = $"Some Error Occured: { errorCode} Code + Message {responseContentJObject["message"]}";
                    }
                }
            }

            return (responseContent, errorMessage);
        }
    }
}
