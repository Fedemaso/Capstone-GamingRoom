namespace GamingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventRequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventRequests",
                c => new
                    {
                        EventRequestId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        ProposedDate = c.DateTime(nullable: false),
                        VenueProposal = c.String(),
                        ProposedTicketsAvailable = c.Int(nullable: false),
                        ProposedTicketPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.EventRequestId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EventRequests");
        }
    }
}
