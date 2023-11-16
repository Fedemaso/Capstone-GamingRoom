namespace GamingRoom.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeamRequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeamRequests",
                c => new
                    {
                        TeamRequestId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        ProposedPhoto = c.String(),
                    })
                .PrimaryKey(t => t.TeamRequestId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TeamRequests");
        }
    }
}
