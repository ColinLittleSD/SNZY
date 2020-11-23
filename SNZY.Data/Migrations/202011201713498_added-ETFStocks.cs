namespace SNZY.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedETFStocks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ETF_Stock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AutherId = c.Guid(nullable: false),
                        StockId = c.Int(nullable: false),
                        ETFId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Portfolio",
                c => new
                    {
                        PortfolioID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.PortfolioID);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        StockName = c.String(nullable: false),
                        Ticker = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        AverageVolume = c.Double(nullable: false),
                        MarketCap = c.Double(nullable: false),
                        AuthorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.StockId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Stock");
            DropTable("dbo.Portfolio");
            DropTable("dbo.ETF_Stock");
        }
    }
}
