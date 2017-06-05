namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedInitialModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomReservationsAppUsers", "RoomReservations_Id", "dbo.RoomReservations");
            DropForeignKey("dbo.RoomReservationsAppUsers", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Rooms", "RoomReservations_Id", "dbo.RoomReservations");
            DropIndex("dbo.Rooms", new[] { "RoomReservations_Id" });
            DropIndex("dbo.RoomReservationsAppUsers", new[] { "RoomReservations_Id" });
            DropIndex("dbo.RoomReservationsAppUsers", new[] { "AppUser_Id" });
            AddColumn("dbo.RoomReservations", "AppUserId", c => c.Int(nullable: false));
            AddColumn("dbo.RoomReservations", "RoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.RoomReservations", "AppUserId");
            CreateIndex("dbo.RoomReservations", "RoomId");
            AddForeignKey("dbo.RoomReservations", "AppUserId", "dbo.AppUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.RoomReservations", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
            DropColumn("dbo.Rooms", "RoomReservations_Id");
            DropTable("dbo.RoomReservationsAppUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoomReservationsAppUsers",
                c => new
                    {
                        RoomReservations_Id = c.Int(nullable: false),
                        AppUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoomReservations_Id, t.AppUser_Id });
            
            AddColumn("dbo.Rooms", "RoomReservations_Id", c => c.Int());
            DropForeignKey("dbo.RoomReservations", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.RoomReservations", "AppUserId", "dbo.AppUsers");
            DropIndex("dbo.RoomReservations", new[] { "RoomId" });
            DropIndex("dbo.RoomReservations", new[] { "AppUserId" });
            DropColumn("dbo.RoomReservations", "RoomId");
            DropColumn("dbo.RoomReservations", "AppUserId");
            CreateIndex("dbo.RoomReservationsAppUsers", "AppUser_Id");
            CreateIndex("dbo.RoomReservationsAppUsers", "RoomReservations_Id");
            CreateIndex("dbo.Rooms", "RoomReservations_Id");
            AddForeignKey("dbo.Rooms", "RoomReservations_Id", "dbo.RoomReservations", "Id");
            AddForeignKey("dbo.RoomReservationsAppUsers", "AppUser_Id", "dbo.AppUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.RoomReservationsAppUsers", "RoomReservations_Id", "dbo.RoomReservations", "Id", cascadeDelete: true);
        }
    }
}
