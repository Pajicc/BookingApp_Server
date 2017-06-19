namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoomResCanceledAndAvgGradeDouble : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomReservations", "Canceled", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Accomodations", "AverageGrade", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accomodations", "AverageGrade", c => c.Single(nullable: false));
            DropColumn("dbo.RoomReservations", "Canceled");
        }
    }
}
