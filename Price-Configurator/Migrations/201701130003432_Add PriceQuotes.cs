namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceQuotes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Checked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceQuotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(nullable: false),
                        EquipmentId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        ListPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EquipmentTypeId = c.Int(nullable: false),
                        PictureUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentTypes", t => t.EquipmentTypeId, cascadeDelete: true)
                .Index(t => t.EquipmentTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceQuotes", "EquipmentTypeId", "dbo.EquipmentTypes");
            DropIndex("dbo.PriceQuotes", new[] { "EquipmentTypeId" });
            DropTable("dbo.PriceQuotes");
            DropTable("dbo.CheckModels");
        }
    }
}
