namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomResNullableDates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoomReservations", "Timestamp", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoomReservations", "Timestamp", c => c.DateTime(nullable: false));
        }
    }
}
