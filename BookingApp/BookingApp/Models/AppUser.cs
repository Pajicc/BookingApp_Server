using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Lastname { get; set; }

        public IList<Accomodation> Accomodations { get; set; }
        public IList<Comment> Comments { get; set; }
        public IList<RoomReservations> RoomReservations { get; set; }
    }
}