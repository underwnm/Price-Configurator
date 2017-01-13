namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refactorpricequote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckModels", "ProductId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CheckModels", "ProductId");
        }
    }
}
