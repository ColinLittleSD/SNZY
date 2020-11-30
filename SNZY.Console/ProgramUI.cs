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

        private void Menu()
        {
            bool continueToRun = true;

            while (continueToRun)
            {
                Console.Clear();

                Console.WriteLine("Welcome to SNZY\n" +
                    "1. Show all stocks\n" +
                    "2. Show all ETF\n" +
                    "3. Show portfolio\n" +
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

            Console.WriteLine("Name \t Ticker \t Price \t AverageVolumen \t MarketCap");

            foreach (var item in resultArray)
            {
                Console.WriteLine($"{item["StockId"]} {item["StockName"], 10} \t {item["Ticker"], 15} \t {item["Price"], 15} \t {item["averageVolume"], 15} \t {item["MarketCap"], 15}");
            }

            Console.WriteLine("\nPress any key to return to main menu.");
            Console.ReadLine();
        }

        //2. Show all ETF
        private void ShowAllETFS()
        {
            Console.Clear();
            Console.WriteLine("ETFs: \n");

            var result = etfAPI.GetAllETF();
            JArray resultArray = JArray.Parse(result);

            Console.WriteLine("ETFId \t Name \t Ticker \t Price \t Holding");
            
            foreach(var item in resultArray)
            {
                var HoldingsList = item["Holdings"];
                Console.WriteLine($"{item["ETFId"]} {item["Name"],10} {item["Price"], 5} {item["Ticker"],15} {HoldingsList[0]["StockName"]}");
            }

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
            Console.WriteLine("Name \t Ticker \t Price \t AverageVolumen \t MarketCap");

            foreach (var item in resultArray)
            {
                Console.WriteLine($"{item["StockId"]} {item["StockName"],10} \t {item["Ticker"],15} \t {item["Price"],15} \t {item["averageVolume"],15} \t {item["MarketCap"],15}");
            }
        }

        private void DisplayETFHelper(JArray resultArray)
        {
            Console.WriteLine("ETFId \t Name \t Ticker \t Price \t Holding");

            foreach (var item in resultArray)
            {
                var HoldingsList = item["Holdings"];
                Console.WriteLine($" {item["ETFName"],10}{item["Ticker"],15}");
            }
        }
    }
}
