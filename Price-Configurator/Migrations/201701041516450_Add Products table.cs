namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ProductModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductModels", t => t.ProductModelId, cascadeDelete: true)
                .Index(t => t.ProductModelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductModelId", "dbo.ProductModels");
            DropIndex("dbo.Products", new[] { "ProductModelId" });
            DropTable("dbo.Products");
        }
    }
}
