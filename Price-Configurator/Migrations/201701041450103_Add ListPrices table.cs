namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddListPricestable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ListPrices");
        }
    }
}
