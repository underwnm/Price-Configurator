namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEquipmentTypestohavenullables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EquipmentTypes", "EquipmentGroupId", "dbo.EquipmentGroups");
            DropIndex("dbo.EquipmentTypes", new[] { "EquipmentGroupId" });
            AlterColumn("dbo.EquipmentTypes", "EquipmentGroupId", c => c.Int());
            CreateIndex("dbo.EquipmentTypes", "EquipmentGroupId");
            AddForeignKey("dbo.EquipmentTypes", "EquipmentGroupId", "dbo.EquipmentGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentTypes", "EquipmentGroupId", "dbo.EquipmentGroups");
            DropIndex("dbo.EquipmentTypes", new[] { "EquipmentGroupId" });
            AlterColumn("dbo.EquipmentTypes", "EquipmentGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.EquipmentTypes", "EquipmentGroupId");
            AddForeignKey("dbo.EquipmentTypes", "EquipmentGroupId", "dbo.EquipmentGroups", "Id", cascadeDelete: true);
        }
    }
}
