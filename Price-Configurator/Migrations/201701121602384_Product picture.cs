namespace Price_Configurator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Productpicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PictureUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PictureUrl");
        }
    }
}
