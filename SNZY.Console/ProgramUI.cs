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
        private static string AuthorizationKey = "btXey_MOdXIFTQMH2J81Cx-8ORrPc5e4h-aNQYtb9FciYs_PSXxz9cxOJJmt7wxqIV6OkPF9R1hQM4oQ1f65SScx_w8Q9z5H0rqpDEHqYMYRF7e1EHJ3ImFzNBlBJWSxTpanWKfcBBk3n095OE5CeF5vqcNPkKFc8QQsHJQTph1AslP2Lz_6EHwnVaDG_lME1A1cRjVcpOZs3fnj3RtnWXidoJmEw9MY8g4iYknUqGqSb37HQrG6bhyBvqtJuuFc95Se1meRl5Dx3aGh66F5XODiPH8Fk-i5NO6slRD5_dlYR7RniCEqky3gde1UmHInorOBNk4ERoCdwTPOITBly0GCG4zuEIalUaZ51-Qbqfrwr9ZP3leLx-6cHtIiZ2QvoqM-g_KQBB5VEjYENq9qfC9_WcEm25dQIWWtjNJECa65bfIwOSujJ19yKEklcbFFoytaqDdxAoGaZVawezIibz2iBSEd397fid7WEjSP4Go";
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

            Console.WriteLine($"{"StockId",narrowPaddingLength} {"Name",namePaddingLength}  {"Ticker",narrowPaddingLength}");

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

            Console.WriteLine($"{"ETFId",narrowPaddingLength}  {"Name",namePaddingLength} {"Ticker",narrowPaddingLength}");
            
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

                Console.WriteLine("Portfolio menu: \n" +
                    "1. View stocks\n" +
                    "2. View ETFs\n" +
                    "3. Post a stock\n" +
                    "4. Post an ETF\n" +
                    "5. Delete a stock\n" +
                    "6. Delete an ETF\n" +
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
            Console.WriteLine("Enter Stock id: ");

            int input_StockId = 0;

            if (int.TryParse(Console.ReadLine(), out input_StockId))
            {
                portfolioAPI.PostAStockToMyPortfolio(1, input_StockId); ;
            }
            else
            {
                Console.WriteLine("\nInvalid input. StockId should be numerical");
                Console.WriteLine("Press any key to continue portfolio menu.");
                Console.ReadLine();
                return;
            }

        }

        //4. Post an etf
        private void PostETFToPortfolio()
        {
            Console.WriteLine("Enter ETF id: ");
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
            Console.WriteLine("Enter Stock id: ");

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
            Console.WriteLine("Enter ETF id: ");

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
            Console.WriteLine("Enter ticker: ");

            string input_ticker = Console.ReadLine();
            var shareAPI = new ShareAPI();
            double price = shareAPI.GetSharePrice(input_ticker);
            Console.WriteLine(price);
            Console.ReadLine();

        }
            
        private void ShowDetails()
        {
            Console.WriteLine("Enter ticker: ");

            string input_ticker = Console.ReadLine();
            var shareAPI = new ShareAPI();
            var result = shareAPI.GetShareInfo(input_ticker);
            Console.WriteLine($"Time: {result.datetime} \n" +
                $"Open: {result.open} \n" +
                $"Volume: {result.volume} \n" +
                $"Low: {result.low} \n" +
                $"High: {result.high}");
            Console.ReadLine(); 
        }
    }
}
