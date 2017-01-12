namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveListPricetable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipments", "ListPriceId", "dbo.ListPrices");
            DropIndex("dbo.Equipments", new[] { "ListPriceId" });
            AddColumn("dbo.Equipments", "ListPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "ListPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Equipments", "ListPriceId");
            DropTable("dbo.ListPrices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ListPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Equipments", "ListPriceId", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "ListPrice");
            DropColumn("dbo.Equipments", "ListPrice");
            CreateIndex("dbo.Equipments", "ListPriceId");
            AddForeignKey("dbo.Equipments", "ListPriceId", "dbo.ListPrices", "Id");
        }
    }
}
