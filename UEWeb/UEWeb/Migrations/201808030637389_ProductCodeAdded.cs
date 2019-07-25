namespace UEWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductCodeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Code", c => c.String(maxLength: 20));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Customers", "Address", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Customers", "Address", c => c.String(maxLength: 100));
            AlterColumn("dbo.Customers", "Name", c => c.String(maxLength: 30));
            DropColumn("dbo.Products", "Code");
        }
    }
}
