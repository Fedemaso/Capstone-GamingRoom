namespace GamingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLastUpdatedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "LastUpdated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "LastUpdated");
        }
    }
}
