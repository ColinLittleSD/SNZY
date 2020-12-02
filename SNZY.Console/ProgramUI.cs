using Newtonsoft.Json.Linq;
using RestSharp;
using SNZY.Console;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNZY_Console
{
    public class ProgramUI
    {
        //Change this to your authorization key
        private static string AuthorizationKey = "lhERvwylWQNfBL5jZ6v9PepDAAbn7GMs4tdd34Gv2K_pQtp4WdYuGBtBROoBQDyRypARreiRhu5xObNt8OSReyHTYrReVD6lii3IlnUwISHNcJ355RJW-vz151qq9bQo-0BFJZzPwMP-OhyJ5y8fso6IIccDTYFALCzr9fxfBgLCW6lwUTg9lbARxF4LUEu5Rec4P7N20s9tPEggHHOCO-lCJUmulFbLsfMGYdVo2rDmLZ7wKf51EpnliBspYjcokuspL7R1AUpe3HcHMV0ObcOCe8_4zb0z2Mf9NFMwoQc8zvnEsHMj0Kgb20nlM1Bcd9rjY82NorGrx2QUxSPwxA-CFXuRJpAkj0zp8xSINzWKPV5Eh7jPvKuDOefJafsaMrE_WGhoCzC-EbxgX5OttBx62qUdOsWbXfL1IzE3v8cqq3z5wUWGRBhYmZS56rhYvQC1ASup10CVzTAwjdVZEF6om3c1G4qc9L2nUhRWquM";
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
            Console.WriteLine("");
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

            Console.WriteLine($"{"ETF ID",narrowPaddingLength}  {"Name",namePaddingLength} {"Ticker",narrowPaddingLength}");
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

            Console.WriteLine("Portfolio Stocks");
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

            Console.WriteLine($"{"Stock ID",narrowPaddingLength} {"Name",namePaddingLength}  {"Ticker",narrowPaddingLength}");

            foreach (var item in resultArray)
            {
                Console.WriteLine($"{item["StockId"],narrowPaddingLength} {item["StockName"],namePaddingLength}  {item["Ticker"],narrowPaddingLength}");
            }
        }

        private void DisplayETFHelper(JArray resultArray)
        {
            const int narrowPaddingLength = -10;
            const int namePaddingLength = -30;

            Console.WriteLine($"{"ETF ID",narrowPaddingLength} {"Name",namePaddingLength} {"Ticker",narrowPaddingLength}");

            List<string> listOfETFNames = new List<string>();

            foreach (var item in resultArray)
            {
  
                Console.WriteLine($"{item["ETFId"],narrowPaddingLength} {item["ETFName"],namePaddingLength} {item["Ticker"],narrowPaddingLength}");
                listOfETFNames.Clear();
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
            string input_ticker = (Console.ReadLine()).ToUpper();
            var shareAPI = new ShareAPI();
            var priceResult = shareAPI.GetSharePrice(input_ticker);
            var infoResult = shareAPI.GetShareInfo(input_ticker);
            double price = priceResult.price;
            var values = infoResult.values;
            
            string errorMessage = priceResult.errorMessage;

            if (errorMessage != "")
            {
                Console.WriteLine($"\n{errorMessage}");
                return;

            }
            double open = double.Parse(values.open);
            Console.Clear();
            Console.WriteLine($" \n" +
                $"{ input_ticker}");

            if (price > open)
            {
                Console.WriteLine($"Current Price: ${Math.Round(price, 2)} \n" +
                    $"The price of {input_ticker} has gone up since the open this morning!", Console.ForegroundColor = ConsoleColor.Green);

            }
            else
            {
                Console.WriteLine($"Current Price: ${Math.Round(price, 2)} \n" +
                    $"The price of {input_ticker} has gone down since the open this morning.", Console.ForegroundColor = ConsoleColor.Red);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();
        }

        private void ShowDetails()
        {
            Console.Clear();
            Console.Write("Enter Ticker: ");
            string input_ticker = (Console.ReadLine()).ToUpper();
            var shareAPI = new ShareAPI();
            var infoResult = shareAPI.GetShareInfo(input_ticker);
            var values = infoResult.values;
            string errorMessage = infoResult.errorMessage;

            if (errorMessage != "")
            {
                Console.WriteLine($"\n{errorMessage}");
                return;
            }

            double open = double.Parse(values.open);
            double close = double.Parse(values.close);
            double low = double.Parse(values.low);
            double high = double.Parse(values.high);
            DateTime dateTime = DateTime.Parse(values.datetime);
            double percentChange = (close - open) / close * 100;

            Console.Clear();
            Console.WriteLine("");

            Console.WriteLine($"{input_ticker} \n" +
                $"Time: {dateTime.ToString("dddd, MM/dd/yyy HH:mm tt")} \n" +
                $"Open: ${Math.Round(open, 2)} \n" +
                $"Volume: {values.volume}/shares per minute \n" +
                $"Low: ${Math.Round(low, 2)} \n" +
                $"High: ${Math.Round(high, 2)}");

            if (percentChange < 0)
            {
                Console.WriteLine($"{input_ticker} has gone down {Math.Round(percentChange, 4)}% since the open this morning.", Console.ForegroundColor = ConsoleColor.Red);
            }
            else
            {
                Console.WriteLine($"{input_ticker} has gone up {Math.Round(percentChange, 4)}% since the open this morning!", Console.ForegroundColor = ConsoleColor.Green);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadLine();
        }
    }
}
