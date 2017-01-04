namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEquipmentRulesandProductEquipmentRulestables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EquipmentRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentEquipmentId = c.Int(nullable: false),
                        ChildEquipmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipments", t => t.ChildEquipmentId)
                .ForeignKey("dbo.Equipments", t => t.ParentEquipmentId)
                .Index(t => t.ParentEquipmentId)
                .Index(t => t.ChildEquipmentId);
            
            CreateTable(
                "dbo.ProductEquipmentRules",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        EquipmentRuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.EquipmentRuleId })
                .ForeignKey("dbo.EquipmentRules", t => t.EquipmentRuleId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.EquipmentRuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductEquipmentRules", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductEquipmentRules", "EquipmentRuleId", "dbo.EquipmentRules");
            DropForeignKey("dbo.EquipmentRules", "ParentEquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.EquipmentRules", "ChildEquipmentId", "dbo.Equipments");
            DropIndex("dbo.ProductEquipmentRules", new[] { "EquipmentRuleId" });
            DropIndex("dbo.ProductEquipmentRules", new[] { "ProductId" });
            DropIndex("dbo.EquipmentRules", new[] { "ChildEquipmentId" });
            DropIndex("dbo.EquipmentRules", new[] { "ParentEquipmentId" });
            DropTable("dbo.ProductEquipmentRules");
            DropTable("dbo.EquipmentRules");
        }
    }
}
