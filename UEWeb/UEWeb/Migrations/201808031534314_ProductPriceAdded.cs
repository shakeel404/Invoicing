namespace UEWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductPriceAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price", c => c.Double(nullable: false,defaultValue:0.00));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Price");
        }
    }
}
