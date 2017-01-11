namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addnametoequipmentrule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EquipmentRules", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EquipmentRules", "Name");
        }
    }
}
