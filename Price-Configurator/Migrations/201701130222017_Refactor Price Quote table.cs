namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorPriceQuotetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckModels", "PriceQuote_Id", c => c.Int());
            CreateIndex("dbo.CheckModels", "PriceQuote_Id");
            AddForeignKey("dbo.CheckModels", "PriceQuote_Id", "dbo.PriceQuotes", "Id");
            DropColumn("dbo.PriceQuotes", "EquipmentId");
            DropColumn("dbo.PriceQuotes", "Name");
            DropColumn("dbo.PriceQuotes", "Description");
            DropColumn("dbo.PriceQuotes", "ListPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PriceQuotes", "ListPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PriceQuotes", "Description", c => c.String());
            AddColumn("dbo.PriceQuotes", "Name", c => c.String(nullable: false));
            AddColumn("dbo.PriceQuotes", "EquipmentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CheckModels", "PriceQuote_Id", "dbo.PriceQuotes");
            DropIndex("dbo.CheckModels", new[] { "PriceQuote_Id" });
            DropColumn("dbo.CheckModels", "PriceQuote_Id");
        }
    }
}
