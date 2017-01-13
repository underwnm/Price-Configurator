namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddtotalpricetoPriceQuote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceQuotes", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceQuotes", "TotalPrice");
        }
    }
}
