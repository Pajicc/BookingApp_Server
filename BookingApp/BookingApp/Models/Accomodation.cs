using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Accomodation
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
        public double AverageGrade { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }
        [StringLength(50)]
        public string ImageURL { get; set; }
        public bool Approved { get; set; }

        public IList<Room> Rooms { get; set; }
        public IList<Comment> Comments { get; set; }

        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public Place Place { get; set; }

        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        [ForeignKey("AccomodationType")]
        public int AccomodationTypeId { get; set; }
        public AccomodationType AccomodationType { get; set; }
    }
}