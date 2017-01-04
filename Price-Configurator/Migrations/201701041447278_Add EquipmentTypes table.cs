namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEquipmentTypestable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EquipmentGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentGroups", t => t.EquipmentGroupId, cascadeDelete: true)
                .Index(t => t.EquipmentGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentTypes", "EquipmentGroupId", "dbo.EquipmentGroups");
            DropIndex("dbo.EquipmentTypes", new[] { "EquipmentGroupId" });
            DropTable("dbo.EquipmentTypes");
        }
    }
}
