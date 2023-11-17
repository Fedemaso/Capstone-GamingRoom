namespace GamingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AggiuntaImmagineEventRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventRequests", "ImageFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventRequests", "ImageFileName");
        }
    }
}
