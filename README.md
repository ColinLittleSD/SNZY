# SNZY

## SNZY Stock Information Application
### Purpose
SNZY is a way to be more connected to the ever changing information in the stock market. We wanted to be able to create a way to research stocks, and let the user gain information to make informed decisions on the stock market. This API uses outside API's to get real time information on stocks, as well as ETFs.

### Concept
We use a console to display the information you are searching for. By using outside API's, the user can input tickers to get both current price of stock, and other important details to consider when making decisions on the market.

### Directions to Run Application
In order to run our project, you will need an authorization key provided by Postman. You will have to insert that authorization into the ProgramUI.cs file, on line 16 within the SNZY_Console project. Set your startup projects to both SNZY.WebApi (start without debugging) and SNZY_Console (start), by right clicking on the solution and going to properties. We have already provided API keys from the outside API's, there is no need to change those values.

Once you have the authorization key, you can POST stocks and ETFs to Postman, as well as creating a portfolio to add stocks and ETFs to. To organize your requests, create 3 folders: Stocks, ETFs and Portfolio.

#### The Stocks Requests, Routes, and Keys are as follows:
* Post Stock - /api/Stock - StockName / Ticker
* Get Stock - /api/Stock/
* Get Stock By Ticker (will return REAL-TIME data on the stock) - /api/Stock/GetByTicker?ticker={ticker}


#### The ETF Requests, Routes, and Keys are as follows:
* Post ETF - /api/ETF/ - Name / Ticker
* Get ETF - /api/ETF/


#### The Portfolio Requests, Routes, and Keys are as follows:
* Post Portfolio - /api/Portfolio - Name
* Get Portfolio - /api/Portfolio
* Post Stock In Portfolio - /api/StockPortfolio/PostPortfolioStocks - StockId / PortfolioId
* Get Stocks In Portfolio - /api/StockPortfolio/GetPortfolioStocks
* Post ETFs in Portfolio - /api/ETFPortfolio/PostPortfolioETFs - ETFId / PortfolioId
* Get ETFs in Portfolio - /api/ETFPortfolio/GetPortfolioETFs
* Remove Stocks from Portfolio - api/StockPortfolio/RemovePortfolioStocks/{id}
* Remove ETFs from Portfolio - api/ETFPortfolio/RemovePortfolioETFs/{id}

#### The Console
The console is pretty straight forward! Play around with it to discover the services it provides. You can type in any ticker for a stock or ETF to get the prices and details. This will allow you to see real time data, the important information for a trader.
### Future Plans
We would like to expand the functionality of the console, by allowing the user to have more power to control their portfolio. Using parameters, buy/hold/sell suggestions can be added for each ticker searched.

## Resource Links
### SNZY GitHub Repository
https://github.com/ColinLittleSD/SNZY

### SNZY Tello Board
https://trello.com/b/MWrTnYG6/snzy

### Stock Information and Price Data API
https://twelvedata.com/
