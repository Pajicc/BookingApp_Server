namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDBModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accomodations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 50),
                        Address = c.String(maxLength: 50),
                        AverageGrade = c.Single(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longtitude = c.Double(nullable: false),
                        ImageURL = c.String(maxLength: 50),
                        Approved = c.Boolean(nullable: false),
                        PlaceId = c.Int(nullable: false),
                        AppUserId = c.Int(nullable: false),
                        AccomodationTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccomodationTypes", t => t.AccomodationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.AppUserId, cascadeDelete: true)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId)
                .Index(t => t.AppUserId)
                .Index(t => t.AccomodationTypeId);
            
            CreateTable(
                "dbo.AccomodationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Grade = c.Double(nullable: false),
                        Text = c.String(maxLength: 50),
                        AccomodationId = c.Int(nullable: false),
                        AppUsersId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationId, cascadeDelete: false)
                .ForeignKey("dbo.AppUsers", t => t.AppUsersId, cascadeDelete: true)
                .Index(t => t.AccomodationId)
                .Index(t => t.AppUsersId);
            
            CreateTable(
                "dbo.RoomReservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNumber = c.Int(nullable: false),
                        BedCount = c.Int(nullable: false),
                        Description = c.String(maxLength: 50),
                        PricePerNight = c.Double(nullable: false),
                        AccomodationId = c.Int(nullable: false),
                        RoomReservations_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accomodations", t => t.AccomodationId, cascadeDelete: true)
                .ForeignKey("dbo.RoomReservations", t => t.RoomReservations_Id)
                .Index(t => t.AccomodationId)
                .Index(t => t.RoomReservations_Id);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        RegionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Regions", t => t.RegionId, cascadeDelete: true)
                .Index(t => t.RegionId);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Code = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoomReservationsAppUsers",
                c => new
                    {
                        RoomReservations_Id = c.Int(nullable: false),
                        AppUser_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoomReservations_Id, t.AppUser_Id })
                .ForeignKey("dbo.RoomReservations", t => t.RoomReservations_Id, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id, cascadeDelete: true)
                .Index(t => t.RoomReservations_Id)
                .Index(t => t.AppUser_Id);
            
            AddColumn("dbo.AppUsers", "Username", c => c.String(maxLength: 50));
            AddColumn("dbo.AppUsers", "Email", c => c.String(maxLength: 50));
            AddColumn("dbo.AppUsers", "Password", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accomodations", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "RegionId", "dbo.Regions");
            DropForeignKey("dbo.Regions", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Accomodations", "AppUserId", "dbo.AppUsers");
            DropForeignKey("dbo.Rooms", "RoomReservations_Id", "dbo.RoomReservations");
            DropForeignKey("dbo.Rooms", "AccomodationId", "dbo.Accomodations");
            DropForeignKey("dbo.RoomReservationsAppUsers", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.RoomReservationsAppUsers", "RoomReservations_Id", "dbo.RoomReservations");
            DropForeignKey("dbo.Comments", "AppUsersId", "dbo.AppUsers");
            DropForeignKey("dbo.Comments", "AccomodationId", "dbo.Accomodations");
            DropForeignKey("dbo.Accomodations", "AccomodationTypeId", "dbo.AccomodationTypes");
            DropIndex("dbo.RoomReservationsAppUsers", new[] { "AppUser_Id" });
            DropIndex("dbo.RoomReservationsAppUsers", new[] { "RoomReservations_Id" });
            DropIndex("dbo.Regions", new[] { "CountryId" });
            DropIndex("dbo.Places", new[] { "RegionId" });
            DropIndex("dbo.Rooms", new[] { "RoomReservations_Id" });
            DropIndex("dbo.Rooms", new[] { "AccomodationId" });
            DropIndex("dbo.Comments", new[] { "AppUsersId" });
            DropIndex("dbo.Comments", new[] { "AccomodationId" });
            DropIndex("dbo.Accomodations", new[] { "AccomodationTypeId" });
            DropIndex("dbo.Accomodations", new[] { "AppUserId" });
            DropIndex("dbo.Accomodations", new[] { "PlaceId" });
            DropColumn("dbo.AppUsers", "Password");
            DropColumn("dbo.AppUsers", "Email");
            DropColumn("dbo.AppUsers", "Username");
            DropTable("dbo.RoomReservationsAppUsers");
            DropTable("dbo.Countries");
            DropTable("dbo.Regions");
            DropTable("dbo.Places");
            DropTable("dbo.Rooms");
            DropTable("dbo.RoomReservations");
            DropTable("dbo.Comments");
            DropTable("dbo.AccomodationTypes");
            DropTable("dbo.Accomodations");
        }
    }
}
