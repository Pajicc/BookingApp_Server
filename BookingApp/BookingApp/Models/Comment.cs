using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public double Grade { get; set; }

        [StringLength(50)]
        public string Text { get; set; }

        [ForeignKey("Accomodation")]
        public int AccomodationId { get; set; }
        public virtual Accomodation Accomodation { get; set; }

        [ForeignKey("AppUsers")]
        public int AppUsersId { get; set; }
        public virtual AppUser AppUsers { get; set; }
    }
}