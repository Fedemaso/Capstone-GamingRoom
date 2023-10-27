namespace GamingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificaEvents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "Events_EventID", c => c.Int());
            CreateIndex("dbo.Teams", "Events_EventID");
            AddForeignKey("dbo.Teams", "Events_EventID", "dbo.Events", "EventID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "Events_EventID", "dbo.Events");
            DropIndex("dbo.Teams", new[] { "Events_EventID" });
            DropColumn("dbo.Teams", "Events_EventID");
        }
    }
}
