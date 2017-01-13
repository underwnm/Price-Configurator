namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adddatetimetopricequote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PriceQuotes", "DateCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PriceQuotes", "DateCreated");
        }
    }
}
