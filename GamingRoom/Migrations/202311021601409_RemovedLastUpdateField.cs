namespace GamingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedLastUpdateField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "LastUpdated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "LastUpdated", c => c.DateTime(nullable: false));
        }
    }
}
