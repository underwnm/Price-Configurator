namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductsEquipmentjunctiontable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductEquipments",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        EquipmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.EquipmentId })
                .ForeignKey("dbo.Equipments", t => t.EquipmentId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.EquipmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductEquipments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductEquipments", "EquipmentId", "dbo.Equipments");
            DropIndex("dbo.ProductEquipments", new[] { "EquipmentId" });
            DropIndex("dbo.ProductEquipments", new[] { "ProductId" });
            DropTable("dbo.ProductEquipments");
        }
    }
}
