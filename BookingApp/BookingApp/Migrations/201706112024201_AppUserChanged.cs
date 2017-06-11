namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppUserChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppUsers", "Name", c => c.String(maxLength: 50));
            AddColumn("dbo.AppUsers", "Lastname", c => c.String(maxLength: 50));
            DropColumn("dbo.AppUsers", "Username");
            DropColumn("dbo.AppUsers", "Email");
            DropColumn("dbo.AppUsers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppUsers", "Password", c => c.String(maxLength: 50));
            AddColumn("dbo.AppUsers", "Email", c => c.String(maxLength: 50));
            AddColumn("dbo.AppUsers", "Username", c => c.String(maxLength: 50));
            DropColumn("dbo.AppUsers", "Lastname");
            DropColumn("dbo.AppUsers", "Name");
        }
    }
}
