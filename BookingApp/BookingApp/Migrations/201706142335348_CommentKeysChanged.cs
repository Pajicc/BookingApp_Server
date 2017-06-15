namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentKeysChanged : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "AppUsersId", newName: "AppUserId");
            RenameIndex(table: "dbo.Comments", name: "IX_AppUsersId", newName: "IX_AppUserId");
            DropPrimaryKey("dbo.Comments");
            AddPrimaryKey("dbo.Comments", new[] { "AccomodationId", "AppUserId" });
            DropColumn("dbo.Comments", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Comments");
            AddPrimaryKey("dbo.Comments", "Id");
            RenameIndex(table: "dbo.Comments", name: "IX_AppUserId", newName: "IX_AppUsersId");
            RenameColumn(table: "dbo.Comments", name: "AppUserId", newName: "AppUsersId");
        }
    }
}
