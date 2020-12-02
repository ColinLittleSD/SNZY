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

Post Stock - https://localhost:{hostnumber}/api/Stock - StockName / Ticker
Get Stock - https://localhost:{hostnumber}/api/Stock/
Get Stock By Ticker (will return real-time data on the stock) - https://localhost:{hostnumber/api/Stock/GetByTicker?ticker={ticker}
Post Stock In Portfolio - https://localhost:{hostnumber}/api/StockPortfolio/PostPortfolioStocks - 
Get Stocks In Portfolio - https://localhost:{hostnumber}/api/StockPortfolio/GetPortfolioStocks

#### The ETF Requests and Routes are as follows:
Post ETF - https://localhost:{hostnumber}/api/ETF/ - Name / Ticker
Get ETF - https://localhost:{hostnumber}/api/ETF/
Post ETFs in Portfolio - https://localhost:{hostnumber}/api/ETFPortfolio/PostPortfolioETFs - ETFId / PortfolioId
Get ETFs in Portfolio - https://localhost:{hostnumber}/api/ETFPortfolio/GetPortfolioETFs

#### The Portfolio Requests and Routes are as follows:
Post Portfolio - https://localhost:{hostnumber}/api/Portfolio - Name
Get Portfolio - https://localhost:{hostnumber}/api/Portfolio



### Future Plans
We would like to expand the functionality of the console, by allowing the user to have more power to control their portfolio. Using parameters, buy/hold/sell suggestions can be added for each ticker searched.

## Resource Links
### SNZY GitHub Repository
https://github.com/ColinLittleSD/SNZY

### SNZY Tello Board
https://trello.com/b/MWrTnYG6/snzy

### Stock Information and Price Data API
https://twelvedata.com/
