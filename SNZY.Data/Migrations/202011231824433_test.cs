namespace SNZY.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ETF", t => t.ETFId, cascadeDelete: true)
                .ForeignKey("dbo.Stock", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.ETFId);
            
            CreateTable(
                "dbo.ETF",
                c => new
                    {
                        ETFId = c.Int(nullable: false, identity: true),
                        AuthorId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Ticker = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Portfolio_PortfolioID = c.Int(),
                    })
                .PrimaryKey(t => t.ETFId)
                .ForeignKey("dbo.Portfolio", t => t.Portfolio_PortfolioID)
                .Index(t => t.Portfolio_PortfolioID);
            
            CreateTable(
                "dbo.Portfolio",
                c => new
                    {
                        PortfolioID = c.Int(nullable: false, identity: true),
                        AuthorId = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PortfolioID);
            
            AddColumn("dbo.Stock", "Portfolio_PortfolioID", c => c.Int());
            CreateIndex("dbo.Stock", "Portfolio_PortfolioID");
            AddForeignKey("dbo.Stock", "Portfolio_PortfolioID", "dbo.Portfolio", "PortfolioID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stock", "Portfolio_PortfolioID", "dbo.Portfolio");
            DropForeignKey("dbo.ETF", "Portfolio_PortfolioID", "dbo.Portfolio");
            DropForeignKey("dbo.ETF_Stock", "StockId", "dbo.Stock");
            DropForeignKey("dbo.ETF_Stock", "ETFId", "dbo.ETF");
            DropIndex("dbo.Stock", new[] { "Portfolio_PortfolioID" });
            DropIndex("dbo.ETF", new[] { "Portfolio_PortfolioID" });
            DropIndex("dbo.ETF_Stock", new[] { "ETFId" });
            DropIndex("dbo.ETF_Stock", new[] { "StockId" });
            DropColumn("dbo.Stock", "Portfolio_PortfolioID");
            DropTable("dbo.Portfolio");
            DropTable("dbo.ETF");
            DropTable("dbo.ETF_Stock");
        }
    }
}
