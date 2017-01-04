namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatecascadeondeletetofalse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipments", "EquipmentTypeId", "dbo.EquipmentTypes");
            DropForeignKey("dbo.Equipments", "ListPriceId", "dbo.ListPrices");
            DropForeignKey("dbo.Products", "ProductModelId", "dbo.ProductModels");
            DropForeignKey("dbo.ProductModels", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductEquipments", "EquipmentId", "dbo.Equipments");
            DropIndex("dbo.EquipmentTypes", new[] { "EquipmentGroupId" });
            AlterColumn("dbo.EquipmentTypes", "EquipmentGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.EquipmentTypes", "EquipmentGroupId");
            AddForeignKey("dbo.Equipments", "EquipmentTypeId", "dbo.EquipmentTypes", "Id");
            AddForeignKey("dbo.Equipments", "ListPriceId", "dbo.ListPrices", "Id");
            AddForeignKey("dbo.Products", "ProductModelId", "dbo.ProductModels", "Id");
            AddForeignKey("dbo.ProductModels", "ProductCategoryId", "dbo.ProductCategories", "Id");
            AddForeignKey("dbo.ProductEquipments", "EquipmentId", "dbo.Equipments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductEquipments", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.ProductModels", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.Products", "ProductModelId", "dbo.ProductModels");
            DropForeignKey("dbo.Equipments", "ListPriceId", "dbo.ListPrices");
            DropForeignKey("dbo.Equipments", "EquipmentTypeId", "dbo.EquipmentTypes");
            DropIndex("dbo.EquipmentTypes", new[] { "EquipmentGroupId" });
            AlterColumn("dbo.EquipmentTypes", "EquipmentGroupId", c => c.Int());
            CreateIndex("dbo.EquipmentTypes", "EquipmentGroupId");
            AddForeignKey("dbo.ProductEquipments", "EquipmentId", "dbo.Equipments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductModels", "ProductCategoryId", "dbo.ProductCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "ProductModelId", "dbo.ProductModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Equipments", "ListPriceId", "dbo.ListPrices", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Equipments", "EquipmentTypeId", "dbo.EquipmentTypes", "Id", cascadeDelete: true);
        }
    }
}
