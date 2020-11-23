namespace SNZY.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedETFStockList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stock", "ETF_ETFId", c => c.Int());
            CreateIndex("dbo.Stock", "ETF_ETFId");
            AddForeignKey("dbo.Stock", "ETF_ETFId", "dbo.ETF", "ETFId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stock", "ETF_ETFId", "dbo.ETF");
            DropIndex("dbo.Stock", new[] { "ETF_ETFId" });
            DropColumn("dbo.Stock", "ETF_ETFId");
        }
    }
}
