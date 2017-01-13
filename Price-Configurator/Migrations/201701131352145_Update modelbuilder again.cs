namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatemodelbuilderagain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CheckModels", "PriceQuote_Id", "dbo.PriceQuotes");
            DropIndex("dbo.CheckModels", new[] { "PriceQuote_Id" });
            RenameColumn(table: "dbo.CheckModels", name: "PriceQuote_Id", newName: "PriceQuoteId");
            AlterColumn("dbo.CheckModels", "PriceQuoteId", c => c.Int(nullable: false));
            CreateIndex("dbo.CheckModels", "PriceQuoteId");
            AddForeignKey("dbo.CheckModels", "PriceQuoteId", "dbo.PriceQuotes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckModels", "PriceQuoteId", "dbo.PriceQuotes");
            DropIndex("dbo.CheckModels", new[] { "PriceQuoteId" });
            AlterColumn("dbo.CheckModels", "PriceQuoteId", c => c.Int());
            RenameColumn(table: "dbo.CheckModels", name: "PriceQuoteId", newName: "PriceQuote_Id");
            CreateIndex("dbo.CheckModels", "PriceQuote_Id");
            AddForeignKey("dbo.CheckModels", "PriceQuote_Id", "dbo.PriceQuotes", "Id");
        }
    }
}
