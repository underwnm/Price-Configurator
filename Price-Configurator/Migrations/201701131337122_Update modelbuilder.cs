namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatemodelbuilder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceQuotes", "ProductId", "dbo.Products");
            AddForeignKey("dbo.PriceQuotes", "ProductId", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceQuotes", "ProductId", "dbo.Products");
            AddForeignKey("dbo.PriceQuotes", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
