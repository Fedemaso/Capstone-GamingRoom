namespace GamingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BusinessDetails",
                c => new
                    {
                        BusinessID = c.Int(nullable: false),
                        BusinessName = c.String(nullable: false, maxLength: 255),
                        Address = c.String(maxLength: 500),
                        PhoneNumber = c.String(maxLength: 50),
                        Website = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.BusinessID)
                .ForeignKey("dbo.Users", t => t.BusinessID)
                .Index(t => t.BusinessID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 255),
                        Role = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        VenueID = c.Int(),
                        TicketsAvailable = c.Int(),
                        TicketsSold = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedBy = c.Int(),
                    })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.Venues", t => t.VenueID)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .Index(t => t.VenueID)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.EventTitles",
                c => new
                    {
                        EventTitleID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(),
                        TitleID = c.Int(),
                    })
                .PrimaryKey(t => t.EventTitleID)
                .ForeignKey("dbo.Events", t => t.EventID)
                .ForeignKey("dbo.Titles", t => t.TitleID)
                .Index(t => t.EventID)
                .Index(t => t.TitleID);
            
            CreateTable(
                "dbo.Titles",
                c => new
                    {
                        TitleID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        ProductionHouse = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.TitleID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        EventID = c.Int(),
                        UserID = c.Int(),
                        PurchaseDate = c.DateTime(),
                        Price = c.Decimal(nullable: false, precision: 10, scale: 2),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.Events", t => t.EventID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.EventID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Venues",
                c => new
                    {
                        VenueID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Address = c.String(nullable: false, maxLength: 500),
                        Capacity = c.Int(),
                    })
                .PrimaryKey(t => t.VenueID);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Surname = c.String(nullable: false, maxLength: 255),
                        Nickname = c.String(maxLength: 255),
                        TeamID = c.Int(),
                        CreatedBy = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerID)
                .ForeignKey("dbo.Teams", t => t.TeamID)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .Index(t => t.TeamID)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        CreatedBy = c.Int(),
                    })
                .PrimaryKey(t => t.TeamID)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .Index(t => t.CreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Players", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Players", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Events", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Events", "VenueID", "dbo.Venues");
            DropForeignKey("dbo.Tickets", "UserID", "dbo.Users");
            DropForeignKey("dbo.Tickets", "EventID", "dbo.Events");
            DropForeignKey("dbo.EventTitles", "TitleID", "dbo.Titles");
            DropForeignKey("dbo.EventTitles", "EventID", "dbo.Events");
            DropForeignKey("dbo.BusinessDetails", "BusinessID", "dbo.Users");
            DropIndex("dbo.Teams", new[] { "CreatedBy" });
            DropIndex("dbo.Players", new[] { "CreatedBy" });
            DropIndex("dbo.Players", new[] { "TeamID" });
            DropIndex("dbo.Tickets", new[] { "UserID" });
            DropIndex("dbo.Tickets", new[] { "EventID" });
            DropIndex("dbo.EventTitles", new[] { "TitleID" });
            DropIndex("dbo.EventTitles", new[] { "EventID" });
            DropIndex("dbo.Events", new[] { "CreatedBy" });
            DropIndex("dbo.Events", new[] { "VenueID" });
            DropIndex("dbo.BusinessDetails", new[] { "BusinessID" });
            DropTable("dbo.Teams");
            DropTable("dbo.Players");
            DropTable("dbo.Venues");
            DropTable("dbo.Tickets");
            DropTable("dbo.Titles");
            DropTable("dbo.EventTitles");
            DropTable("dbo.Events");
            DropTable("dbo.Users");
            DropTable("dbo.BusinessDetails");
        }
    }
}
