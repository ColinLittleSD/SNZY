using Newtonsoft.Json.Linq;
using RestSharp;
using SNZY.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY_Console
{
    public class ProgramUI
    {
        //Change this to your authorization key
        private static string AuthorizationKey = "2QiJerB1o_6KKr1h17wll8dQo1gARyNwNllff5JZX20Rd3YzR4P2b3ur_15kQq6G-YUduceBq1drqw5I5NaijDIJsVORKkOLmOmE0uM_4t_VQp_6InueUc0PL-ipKjEd-Ls41I7AxPPc7udmCvLHRkvTEYTbAcjeBuJfsuuIllGWM-oJq3D9J42CopR-kAooJSjt1yxzk_gkoyjpLJHO8C_eWfzO7LIDBxTA0N7vF_sqj9lOU72DboTqRwHLxjJ1UMLUgw9DvT1sIydwgF72jlutiSvPCj7vT2SU5U3RUZMIhRvLh130O5z53jefdinBVijKFcKhEXRo198EI-GOTYYxshB3TUwgA7Uk2YBeXEWPfm7MpAETOd3m7TOSVD23yNBwXpOi3WbBRp6zcD9m1CZhyGU-EHfjBvI5Ss0LaPZLxV0kcvbyv7ioajhXVHVWRus12gI7Cc0opC2CSO6R-MUuCWLvVAVLv1YThoUyjkmXAN8YOBDqb-ZKvo2EJHe8";
        private readonly PortfolioAPI portfolioAPI = new PortfolioAPI(AuthorizationKey);
        private readonly StockAPI stockAPI = new StockAPI(AuthorizationKey);
        private readonly ETFAPI etfAPI = new ETFAPI(AuthorizationKey);

        public void Run()
        {
            Menu();
        }

        //Main menu
        private void Menu()
        {
            bool continueToRun = true;

            while (continueToRun)
            {
                Console.Clear();

                Console.WriteLine("Welcome to SNZY\n" +
                    " \n" +
                    "1. Show all Stocks\n" +
                    "2. Show all ETFs\n" +
                    "3. Show My Portfolio\n" +
                    "4. Exit\n");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowAllStocks();
                        break;
                    case "2":
                        ShowAllETFS();
                        break;
                    case "3":
                        ShowPortfolio();
                        break;
                    case "4":
                        continueToRun = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice.\n");
                        Console.WriteLine("Press any key to return to main menu.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        //1. Show all stocks
        private void ShowAllStocks()
        {
            Console.Clear();
            string loading = "Loading...";
            Console.WriteLine(loading);

            var result = stockAPI.GetAllStocks();
            string listOfStockInJSON = result.responseContent;
            string errorMessage = result.errorMessage;

            //if there is an error message
            if (errorMessage != "")
            {
                Console.Clear();
                Console.WriteLine($"\n{errorMessage}");
                Console.WriteLine("\nPress any key to return to main menu");
                Console.ReadLine();
                return;

            }

            Console.Clear();
            Console.WriteLine("These are some examples of Stocks and their Tickers: \n");

            JArray resultArray = JArray.Parse(listOfStockInJSON);

            const int narrowPaddingLength = -10;
            const int namePaddingLength = -30;

            Console.WriteLine($"{"Stock ID",narrowPaddingLength} {"Name",namePaddingLength}  {"Ticker",narrowPaddingLength}");

            foreach (var item in resultArray)
            {
                Console.WriteLine($"{item["StockId"],narrowPaddingLength} {item["StockName"],namePaddingLength}  {item["Ticker"],narrowPaddingLength}");
            }

            DetailsMenu();

        }

        //2. Show all ETF
        private void ShowAllETFS()
        {
            Console.Clear();
            string loading = "Loading...";
            Console.WriteLine(loading);

            var result = etfAPI.GetAllETF();
            string listOfETFsInJSON = result.responseContent;
            string errorMessage = result.errorMessage;

            //if there is an error message
            if (errorMessage != "")
            {
                Console.Clear();
                Console.WriteLine($"\n{errorMessage}");
                Console.WriteLine("\nPress any key to return to main menu");
                Console.ReadLine();
                return;

            }

            Console.Clear();
            Console.WriteLine("These are some examples of ETFs and their Tickers: \n");

            const int narrowPaddingLength = -10;
            const int namePaddingLength = -40;

            JArray resultArray = JArray.Parse(listOfETFsInJSON);

            Console.WriteLine($"{"ETF ID",narrowPaddingLength} {"Name",namePaddingLength} {"Ticker",narrowPaddingLength} { "Holdings",narrowPaddingLength}");

            List<string> listOfStockNames = new List<string>();

            foreach (var item in resultArray)
            {
                var HoldingsList = item["Holdings"];
                foreach(var stock in HoldingsList)
                {
                    listOfStockNames.Add((string) stock["StockName"]);
                }
                Console.WriteLine($"{item["ETFId"],narrowPaddingLength} {item["Name"],namePaddingLength} {item["Ticker"],narrowPaddingLength} {String.Join(", ", listOfStockNames), narrowPaddingLength}");
                listOfStockNames.Clear();
            }

            DetailsMenu();

        }

        //3. Show my portfolio
        private void ShowPortfolio()
        {
            Console.Clear();
            string loading = "Loading...";
            Console.WriteLine(loading);

            var result = portfolioAPI.GetAllPortfolio();
            string listOfPortfoliosInJSON = result.responseContent;
            string errorMessage = result.errorMessage;

            //if there is an error message
            if (errorMessage != "")
            {
                Console.Clear();
                Console.WriteLine($"\n{errorMessage}");
                Console.WriteLine("\nPress any key to return to main menu");
                Console.ReadLine();
                return;

            }
            
            JArray resultArray = JArray.Parse(listOfPortfoliosInJSON);

            string portfolioName = (string)resultArray[0]["Name"];

            PortfolioMenu(portfolioName);
        }

        //Portfolio menu
        private void PortfolioMenu(string portfolioName)
        {
            bool continueToRun = true;

            while (continueToRun)
            {
                Console.Clear();
                Console.WriteLine($"{portfolioName}\n");

                Console.WriteLine("Portfolio Menu: \n" +
                    "1. View stocks\n" +
                    "2. View ETFs\n" +
                    "3. Post A Stock\n" +
                    "4. Post An ETF\n" +
                    "5. Remove a Stock\n" +
                    "6. Remove an ETF\n" +
                    "7. Back to main menu\n");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ViewStockFromPortfolio();
                        break;
                    case "2":
                        ViewETFFromPortfolio();
                        break;
                    case "3":
                        PostStockToPortfolio();
                        break;
                    case "4":
                        PostETFToPortfolio();
                        break;
                    case "5":
                        DeleteStockFromPortfolio();
                        break;
                    case "6":
                        DeleteETFFromPortfolio();
                        break;
                    case "7":
                        continueToRun = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice.\n");
                        Console.WriteLine("Press any key to return to portfolio menu");
                        Console.ReadLine();
                        break;
                }
            }
        }

        //1. View stocks
        private void ViewStockFromPortfolio()
        {
            Console.Clear();
            string loading = "Loading...";
            Console.WriteLine(loading);

            var result = portfolioAPI.GetAllPortfolio();
            string listOfPortfoliosInJSON = result.responseContent;
            string errorMessage = result.errorMessage;

            //if there is an error message
            if (errorMessage != "")
            {
                Console.Clear();
                Console.WriteLine($"\n{errorMessage}");
                Console.WriteLine("\nPress any key to return to main menu");
                Console.ReadLine();
                return;

            }

            Console.Clear();

            JArray resultArray = JArray.Parse(listOfPortfoliosInJSON);

            Console.WriteLine("Porfolio Stocks");
            DisplayStockHelper((JArray)resultArray[0]["StocksInPortfolio"]);

            Console.WriteLine("\nPress any key to return to portfolio menu");
            Console.ReadLine();
            return;
        }

        //2. View ETFs
        private void ViewETFFromPortfolio()
        {
            Console.Clear();
            string loading = "Loading...";
            Console.WriteLine(loading);

            var result = portfolioAPI.GetAllPortfolio();
            string listOfPortfoliosInJSON = result.responseContent;
            string errorMessage = result.errorMessage;

            //if there is an error message
            if (errorMessage != "")
            {
                Console.Clear();
                Console.WriteLine($"\n{errorMessage}");
                Console.WriteLine("\nPress any key to return to main menu");
                Console.ReadLine();
                return;

            }

            Console.Clear();

            JArray resultArray = JArray.Parse(listOfPortfoliosInJSON);

            Console.WriteLine("Portfolio ETFs");
            DisplayETFHelper((JArray)resultArray[0]["ETFsInPortfolio"]);

            Console.WriteLine("\nPress any key to return to portfolio menu");
            Console.ReadLine();
            return;
        }

        //3. Post a stock
        private void PostStockToPortfolio()
        {
            Console.Write("Enter Stock ID: ");

            int input_StockId = 0;

            if (int.TryParse(Console.ReadLine(), out input_StockId))
            {
                string errorMessage = portfolioAPI.PostAStockToMyPortfolio(1, input_StockId);

                //if there is an error message
                if (errorMessage != "")
                {
                    Console.WriteLine($"\n{errorMessage}");
                    Console.WriteLine("\nPress any key to return to main menu");
                    Console.ReadLine();
                    return;
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input. Stock ID should be numerical");
                Console.WriteLine("Press any key to continue portfolio menu.");
                Console.ReadLine();
                return;
            }

        }

        //4. Post an etf
        private void PostETFToPortfolio()
        {
            Console.Write("Enter ETF ID: ");
            int input_ETFId = 0;

            if (int.TryParse(Console.ReadLine(), out input_ETFId))
            {
                string errorMessage = portfolioAPI.PostAETFToMyPortfolio(1, input_ETFId);

                //if there is an error message
                if (errorMessage != "")
                {
                    Console.WriteLine($"\n{errorMessage}");
                    Console.WriteLine("\nPress any key to return to main menu");
                    Console.ReadLine();
                    return;
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input. ETFId should be numerical");
                Console.WriteLine("Press any key to continue portfolio menu.");
                Console.ReadLine();
                return;
            }
        }

        //5. Delete a stock
        private void DeleteStockFromPortfolio()
        {
            Console.Write("Enter Stock ID: ");

            int input_StockId = 0;

            if (int.TryParse(Console.ReadLine(), out input_StockId))
            {
                string errorMessage = portfolioAPI.DeleteAStockFromMyPortfolio(input_StockId);

                //if there is an error message
                if (errorMessage != "")
                {
                    Console.WriteLine($"\n{errorMessage}");
                    Console.WriteLine("\nPress any key to return to main menu");
                    Console.ReadLine();
                    return;
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input. StockId should be numerical");
                Console.WriteLine("Press any key to continue portfolio menu.");
                Console.ReadLine();
                return;
            }
        }

        //6. Delete an ETF
        private void DeleteETFFromPortfolio()
        {
            Console.Write("Enter ETF ID: ");

            int input_ETFId = 0;

            if (int.TryParse(Console.ReadLine(), out input_ETFId))
            {
                string errorMessage = portfolioAPI.DeleteETFFromMyPortfolio(input_ETFId);

                //if there is an error message
                if (errorMessage != "")
                {
                    Console.WriteLine($"{errorMessage}");
                    Console.WriteLine("\nPress any key to return to main menu");
                    Console.ReadLine();
                    return;
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input. ETFId should be numerical");
                Console.WriteLine("Press any key to continue portfolio menu.");
                Console.ReadLine();
                return;
            }
        }

        private void DisplayStockHelper(JArray resultArray)
        {
            const int narrowPaddingLength = -10;
            const int namePaddingLength = -30;

            Console.WriteLine($"{"StockId",narrowPaddingLength} {"Name",namePaddingLength}  {"Ticker",narrowPaddingLength}  {"Price",narrowPaddingLength} ");

            foreach (var item in resultArray)
            {
                Console.WriteLine($"{item["StockId"],narrowPaddingLength} {item["StockName"],namePaddingLength}  {item["Ticker"],narrowPaddingLength}  {item["Price"],narrowPaddingLength} ");
            }
        }

        private void DisplayETFHelper(JArray resultArray)
        {
            const int narrowPaddingLength = -10;
            const int namePaddingLength = -30;

            Console.WriteLine($"{"ETF ID",narrowPaddingLength} {"Name",namePaddingLength} {"Ticker",narrowPaddingLength} { "Holdings",narrowPaddingLength}");

            List<string> listOfStockNames = new List<string>();

            foreach (var item in resultArray)
            {
                var HoldingsList = item["Holdings"];
                foreach (var stock in HoldingsList)
                {
                    listOfStockNames.Add((string)stock["StockName"]);
                }
                Console.WriteLine($"{item["ETFId"],narrowPaddingLength} {item["Name"],namePaddingLength} {item["Ticker"],narrowPaddingLength} {String.Join(", ", listOfStockNames),narrowPaddingLength}");
                listOfStockNames.Clear();
            }
        }

        private void DetailsMenu()
        {
            bool continueToRun = true;

            while (continueToRun)
            {

                Console.WriteLine("");
                Console.WriteLine("What would you like to see?\n" +
                    "1. Show Price\n" +
                    "2. Show Details\n" +
                    "3. Go back\n");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ShowPrice();
                        break;
                    case "2":
                        ShowDetails();
                        break;
                    case "3":
                        continueToRun = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid choice.\n");
                        Console.WriteLine("Press any key to return to main menu.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowPrice()
        {
            Console.Clear();

            Console.Write("Enter Ticker: ");
            string input_ticker = Console.ReadLine();

            var shareAPI = new ShareAPI();
            var stockResult = shareAPI.GetSharePrice(input_ticker);
            double price = stockResult.price;
            string errorMessage = stockResult.errorMessage;

            if(errorMessage != "")
            {
                Console.WriteLine($"\n{errorMessage}");
                return;

            }
            Console.WriteLine("");
            Console.WriteLine($"Current Price: ${price}");
        }

        private void ShowDetails()
        {
            Console.Clear();

            Console.Write("Enter Ticker: ");
            string input_ticker = Console.ReadLine();

            var shareAPI = new ShareAPI();
            var etfResult = shareAPI.GetShareInfo(input_ticker);
            var etfValue = etfResult.values;
            string errorMessage = etfResult.errorMessage;

            if (errorMessage != "")
            {
                Console.WriteLine($"\n{errorMessage}");
                return;

            }

            double open = double.Parse(etfValue.open);
            double close = double.Parse(etfValue.close);

            Console.WriteLine("");
            Console.WriteLine($"Time: {etfValue.datetime} \n" +
                $"Open: ${etfValue.open} \n" +
                $"Volume: {etfValue.volume} \n" +
                $"Low: ${etfValue.low} \n" +
                $"High: ${etfValue.high}\n" +
                $"Percent Change: {(close - open) / close * 100}%");
        }
    }
}