 namespace GamingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelRole : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserCustomers", "Username", c => c.String(maxLength: 255));
            AlterColumn("dbo.UserCustomers", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.UserCustomers", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.UserCustomers", "Address", c => c.String(maxLength: 255));
            AlterColumn("dbo.UserCustomers", "ZipCode", c => c.String(maxLength: 10));
            AlterColumn("dbo.UserCustomers", "PhoneNumber", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserCustomers", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.UserCustomers", "ZipCode", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.UserCustomers", "Address", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.UserCustomers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.UserCustomers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.UserCustomers", "Username", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
