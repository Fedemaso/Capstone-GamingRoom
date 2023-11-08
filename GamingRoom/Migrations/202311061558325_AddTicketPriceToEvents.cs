namespace GamingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketPriceToEvents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "TicketPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "TicketPrice");
        }
    }
}
