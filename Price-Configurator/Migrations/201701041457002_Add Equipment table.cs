namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEquipmenttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        ListPriceId = c.Int(nullable: false),
                        EquipmentTypeId = c.Int(nullable: false),
                        PictureUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentTypes", t => t.EquipmentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ListPrices", t => t.ListPriceId, cascadeDelete: true)
                .Index(t => t.ListPriceId)
                .Index(t => t.EquipmentTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipments", "ListPriceId", "dbo.ListPrices");
            DropForeignKey("dbo.Equipments", "EquipmentTypeId", "dbo.EquipmentTypes");
            DropIndex("dbo.Equipments", new[] { "EquipmentTypeId" });
            DropIndex("dbo.Equipments", new[] { "ListPriceId" });
            DropTable("dbo.Equipments");
        }
    }
}
