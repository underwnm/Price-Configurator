namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Allownullequipmenttype : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.EquipmentTypes", new[] { "EquipmentGroupId" });
            AlterColumn("dbo.EquipmentTypes", "EquipmentGroupId", c => c.Int());
            CreateIndex("dbo.EquipmentTypes", "EquipmentGroupId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.EquipmentTypes", new[] { "EquipmentGroupId" });
            AlterColumn("dbo.EquipmentTypes", "EquipmentGroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.EquipmentTypes", "EquipmentGroupId");
        }
    }
}
