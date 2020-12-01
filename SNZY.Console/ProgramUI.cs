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
        private static string AuthorizationKey = "bfoDf_5Nnrkk7Cc_B5xtG74FdhFTnPhOCEk4UQuvHdkKABdaFa_HUTOWKlMeuTE8Qf7k3sV-qY8BmaGgp9JInYfRyeUdU56ku35UhKJCr3Fch_7PKIdqnCVJ461N4algV7RSSw8CbuxM2iWWDxAma_2JF4W-DX4CHH78xN1ndc6ONHr1HExHAeHDIbdbf7vKoCygCR3W1rTi0rcL5CDCBpx7enjWKJntGUfdSZMRtOJ8PCDpDQNhOiOvorIZ3FMRyQdRXhTVtfBqDGo7oBY1R2nf3GDRsaQSlJOXd-sGf_oC_tKdQcjbjasaHB3JU9-3lT8-KtRpGQwlH5p3C6lXdfBiqwpVq2c-2SR6QpfLQttSlb8zcrMrUJuk_ZAfBjtPUka3X8d7Ebe-533SXcK496Elzu98kjl4QXzUxFELL59I09zNTEQ0HxW7bIakCdyb1vBzx3zteosRVvPeJ1L4s4nqTMqYqI8gfS6QGoKB9PM7qjunpNmkXXfngz0Jvrf1";
        private readonly PortfolioAPI portfolioAPI = new PortfolioAPI(AuthorizationKey);
        private readonly StockAPI stockAPI = new StockAPI(AuthorizationKey);
        private readonly ETFAPI etfAPI = new ETFAPI(AuthorizationKey);

        public void Run()
        {
            Menu();
        }
        
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
            Console.WriteLine("Stocks: \n");

            var result = stockAPI.GetAllStocks();
            JArray resultArray = JArray.Parse(result);

            const int narrowPaddingLength = -10;
            const int namePaddingLength = -30;

            Console.WriteLine($"{"Stock ID",narrowPaddingLength} {"Name",namePaddingLength}  {"Ticker",narrowPaddingLength}");

            foreach (var item in resultArray)
            {
                Console.WriteLine($"{item["StockId"],narrowPaddingLength} {item["StockName"],namePaddingLength}  {item["Ticker"],narrowPaddingLength}");
            }

            DetailsMenu();

            Console.WriteLine("\nPress any key to return to main menu.");
            Console.ReadLine();
        }

        //2. Show all ETF
        private void ShowAllETFS()
        {
            Console.Clear();
            Console.WriteLine("ETFs: \n");

            const int narrowPaddingLength = -10;
            const int namePaddingLength = -40;

            var result = etfAPI.GetAllETF();
            JArray resultArray = JArray.Parse(result);

            Console.WriteLine($"{"ETF ID",narrowPaddingLength}  {"Name",namePaddingLength} {"Ticker",narrowPaddingLength}");
            
            foreach(var item in resultArray)
            {
                var HoldingsList = item["Holdings"];
                Console.WriteLine($"{item["ETFId"],narrowPaddingLength} {item["Name"],namePaddingLength} {item["Ticker"],narrowPaddingLength} ");
            }

            DetailsMenu();

            Console.WriteLine("\nPress any key to return to main menu.");
            Console.ReadLine();
        }

        //3. Show my portfolio
        private void ShowPortfolio()
        {
            var result = portfolioAPI.GetAllPortfolio();
            JArray resultArray = JArray.Parse(result);

            string portfolioName = (string)resultArray[0]["Name"];

            PortfolioMenu(portfolioName);
        }

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
            var result = portfolioAPI.GetAllPortfolio();
            JArray resultArray = JArray.Parse(result);

            Console.WriteLine("Porfolio Stocks");
            DisplayStockHelper((JArray)resultArray[0]["StocksInPortfolio"]);

            Console.WriteLine("\nPress any key to return to portfolio menu");
            Console.ReadLine();
            return;
        }

        //2. View ETFs
        private void ViewETFFromPortfolio()
        {
            var result = portfolioAPI.GetAllPortfolio();
            JArray resultArray = JArray.Parse(result);
            
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
                portfolioAPI.PostAStockToMyPortfolio(1, input_StockId); ;
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
                portfolioAPI.PostAETFToMyPortfolio(1, input_ETFId);
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
                portfolioAPI.DeleteAStockFromMyPortfolio(input_StockId);
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
                portfolioAPI.DeleteETFFromMyPortfolio(input_ETFId);
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

            Console.WriteLine($"{"ETFId",narrowPaddingLength} {"Name",namePaddingLength} {"Ticker",narrowPaddingLength} {"Price",narrowPaddingLength} {"Holding",narrowPaddingLength}");

            foreach (var item in resultArray)
            {
                var HoldingsList = item["Holdings"];
                Console.WriteLine($" {item["ETFName"],namePaddingLength}{item["Ticker"],narrowPaddingLength}");
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
                    "3. Exit\n");

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
            double price = shareAPI.GetSharePrice(input_ticker);
            Console.WriteLine("");
            Console.WriteLine($"Current Price: ${price}");
            Console.ReadLine();
        }
            
        private void ShowDetails()
        {
            Console.Clear();
            Console.Write("Enter Ticker: ");

            string input_ticker = Console.ReadLine();
            var shareAPI = new ShareAPI();
            var result = shareAPI.GetShareInfo(input_ticker);

            double open = double.Parse(result.open);
            double close = double.Parse(result.close);

            Console.WriteLine("");
            Console.WriteLine($"Time: {result.datetime} \n" +
                $"Open: ${result.open} \n" +
                $"Volume: {result.volume} \n" +
                $"Low: ${result.low} \n" +
                $"High: ${result.high}\n" +
                $"Percent Change: {(close - open) / close * 100}%");
            Console.ReadLine(); 
        }
    }
}
