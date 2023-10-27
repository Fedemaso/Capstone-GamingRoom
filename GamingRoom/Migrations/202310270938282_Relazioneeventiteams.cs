namespace GamingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Relazioneeventiteams : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teams", "Events_EventID", "dbo.Events");
            DropIndex("dbo.Teams", new[] { "Events_EventID" });
            CreateTable(
                "dbo.TeamsEvents",
                c => new
                    {
                        Teams_TeamID = c.Int(nullable: false),
                        Events_EventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teams_TeamID, t.Events_EventID })
                .ForeignKey("dbo.Teams", t => t.Teams_TeamID, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Events_EventID, cascadeDelete: true)
                .Index(t => t.Teams_TeamID)
                .Index(t => t.Events_EventID);
            
            DropColumn("dbo.Teams", "Events_EventID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "Events_EventID", c => c.Int());
            DropForeignKey("dbo.TeamsEvents", "Events_EventID", "dbo.Events");
            DropForeignKey("dbo.TeamsEvents", "Teams_TeamID", "dbo.Teams");
            DropIndex("dbo.TeamsEvents", new[] { "Events_EventID" });
            DropIndex("dbo.TeamsEvents", new[] { "Teams_TeamID" });
            DropTable("dbo.TeamsEvents");
            CreateIndex("dbo.Teams", "Events_EventID");
            AddForeignKey("dbo.Teams", "Events_EventID", "dbo.Events", "EventID");
        }
    }
}
