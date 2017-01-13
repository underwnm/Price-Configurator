namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePriceQuoteTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceQuotes", "EquipmentTypeId", "dbo.EquipmentTypes");
            DropIndex("dbo.PriceQuotes", new[] { "EquipmentTypeId" });
            AddColumn("dbo.PriceQuotes", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.PriceQuotes", "ProductId");
            AddForeignKey("dbo.PriceQuotes", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.PriceQuotes", "EquipmentTypeId");
            DropColumn("dbo.PriceQuotes", "PictureUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PriceQuotes", "PictureUrl", c => c.String());
            AddColumn("dbo.PriceQuotes", "EquipmentTypeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PriceQuotes", "ProductId", "dbo.Products");
            DropIndex("dbo.PriceQuotes", new[] { "ProductId" });
            DropColumn("dbo.PriceQuotes", "ProductId");
            CreateIndex("dbo.PriceQuotes", "EquipmentTypeId");
            AddForeignKey("dbo.PriceQuotes", "EquipmentTypeId", "dbo.EquipmentTypes", "Id", cascadeDelete: true);
        }
    }
}
