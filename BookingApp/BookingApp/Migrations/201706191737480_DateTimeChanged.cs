namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoomReservations", "StartDate", c => c.DateTime());
            AlterColumn("dbo.RoomReservations", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoomReservations", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.RoomReservations", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
