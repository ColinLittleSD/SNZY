# SNZY

## SNZY Stock Information Application
### Purpose


SNZY is a way to be more connected to the ever changing information in the stock market. We wanted to be able to create a way to research stocks, and let the user gain information to make informed decisions on the stock market. This API uses outside API's to get real time information on stocks, as well as ETFs.

### Concept
We use a console to display the information you are searching for. By using outside API's, the user can input tickers to get both current price of stock, and other important details to consider when making decisions on the market.

### Directions to Run Application
In order to run our project, you will need an authorization key provided by Postman. You will have to insert that authorization into the ProgramUI, on line 15 within the SNZY_Console project. Set your startup projects to both SNZY.WebApi (start without debugging) and SNZY_Console (start), by right clicking on the solution and going to properties. We have already provided API keys to our outside API's, no need to change those values. If for whatever reason the key disappears, here is the apiKey "a6ae3f3429144b7fa3160c590b1c81b1". You will have to add this to ShareAPI.cs within the SNZY_Console project, StockPortfolioListItem.cs within SNZY.Models\StockPortfolio, ETFAPIService and ShareApiService within SNZY.Service project.


### Future Plans
We would like to expand the functionality of the console, by allowing the user to have more power to control their portfolio. Using parameters, buy/hold/sell suggestions can be added for each ticker searched.

## Resource Links
### SNZY GitHub Repository
https://github.com/ColinLittleSD/SNZY

### SNZY Tello Board
https://trello.com/b/MWrTnYG6/snzy

### Stock Information and Price Data API
https://twelvedata.com/
