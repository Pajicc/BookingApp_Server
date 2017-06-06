using BookingApp.Models;

namespace BookingApp.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookingApp.Models.BAContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookingApp.Models.BAContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.AccomodationTypes.AddOrUpdate(
              a => a.Name,
              new AccomodationType { Name = "Hotel"},
              new AccomodationType { Name = "House" }
            );

            context.Countries.AddOrUpdate(
            c => c.Name,
            new Country { Name = "Germany" }
            );

            context.AppUsers.AddOrUpdate(
            au => au.Username,
            new AppUser { Username = "user1", Password = "user1", Email = "user1@live.com", FullName = 1 },
            new AppUser { Username = "user2", Password = "user2", Email = "user2@live.com",FullName = 4 },
            new AppUser { Username = "user3", Password = "user3", Email = "user3@live.com",FullName = 3 },
            new AppUser { Username = "user4", Password = "user4", Email = "user4@live.com",FullName = 2},
            new AppUser { Username = "owner1", Password = "owner1", Email = "owner1@live.com", FullName = 2 },
             new AppUser { Username = "owner2", Password = "owner2", Email = "owner2@live.com", FullName = 2 },
             new AppUser { Username = "admin", Password = "admin", Email = "admin@live.com", FullName = 2 }
            );

            context.SaveChanges();

            context.Regions.AddOrUpdate(
            r => r.Name,
            new Region { Name = "East Germany", CountryId = 1 },
            new Region { Name = "West Germany", CountryId = 1}
            );

            context.SaveChanges();

            context.Places.AddOrUpdate(
            p => p.Name,
            new Place { Name = "Berlin", RegionId = 1 },
            new Place { Name = "Frankfurt", RegionId = 2 }
            );

            context.SaveChanges();

            context.Accomodations.AddOrUpdate(
            a => a.Name,
            new Accomodation { Name = "Hotel Park", AverageGrade = 5, Address = "Main Street", PlaceId = 1, Longtitude = 24.600, Latitude = 25.880, Description = "Hotel", ImageURL = "c:/users/img1.jpg", AppUserId = 5, AccomodationTypeId = 1, Approved = true },
           new Accomodation { Name = "Green House", AverageGrade = 4, Address = "5th Street", PlaceId = 2, Longtitude = 34.600, Latitude = 45.880, Description = "House", ImageURL = "c:/users/img2.jpg", AppUserId = 6, AccomodationTypeId = 2, Approved = true }
            );

           context.SaveChanges();

           context.Comments.AddOrUpdate(
           c => c.Text,
           new Comment { Text = "Great place", Grade = 4, AppUsersId = 1, AccomodationId = 1 },
           new Comment { Text = "Ok", Grade = 3, AppUsersId = 2, AccomodationId = 2 },
           new Comment { Text = "Very bad", Grade = 1, AppUsersId = 3, AccomodationId = 1 },
           new Comment { Text = "Very nice", Grade = 5, AppUsersId = 4, AccomodationId = 2 }
           );

            context.Rooms.AddOrUpdate(
            r => r.RoomNumber,
            new Room { Description = "Room1", BedCount = 3, PricePerNight = 1500, AccomodationId = 1, RoomNumber = 1 },
            new Room { Description = "Room2", BedCount = 2, PricePerNight = 1000, AccomodationId = 2, RoomNumber = 2 }
            );

            context.SaveChanges();

            context.RoomReservations.AddOrUpdate(
            r => r.StartDate,
            new RoomReservations { StartDate = DateTime.Now, EndDate = DateTime.Today, AppUserId = 1, RoomId = 1, Timestamp = DateTime.Now}
            );

            context.SaveChanges();

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Manager"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Manager" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "AppUser"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppUser"};

                manager.Create(role);
            }

            var userStore = new UserStore<BAIdentityUser>(context);
            var userManager = new UserManager<BAIdentityUser>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var user = new BAIdentityUser() { Id = "admin", UserName = "admin", Email = "admin@yahoo.com", PasswordHash = BAIdentityUser.HashPassword("admin"), appUserId = 7};
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
