namespace SNZY.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatestoproject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ETF_Stock", "AuthorId", c => c.Guid(nullable: false));
            DropColumn("dbo.ETF_Stock", "AuthorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ETF_Stock", "AuthorId", c => c.Guid(nullable: false));
            DropColumn("dbo.ETF_Stock", "AuthorId");
        }
    }
}
