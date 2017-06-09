namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDbData : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AppUsers", "FullName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppUsers", "FullName", c => c.Int(nullable: false));
        }
    }
}
