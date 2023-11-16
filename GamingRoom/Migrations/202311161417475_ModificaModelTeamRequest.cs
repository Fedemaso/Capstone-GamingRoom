namespace GamingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModificaModelTeamRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamRequests", "CreatedBy", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamRequests", "CreatedBy");
        }
    }
}
