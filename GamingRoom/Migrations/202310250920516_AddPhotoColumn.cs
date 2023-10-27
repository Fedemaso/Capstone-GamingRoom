namespace GamingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhotoColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Photo", c => c.String());
            AddColumn("dbo.Titles", "Photo", c => c.String());
            AddColumn("dbo.Venues", "Photo", c => c.String());
            AddColumn("dbo.Players", "Photo", c => c.String());
            AddColumn("dbo.Teams", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teams", "Photo");
            DropColumn("dbo.Players", "Photo");
            DropColumn("dbo.Venues", "Photo");
            DropColumn("dbo.Titles", "Photo");
            DropColumn("dbo.Events", "Photo");
        }
    }
}
