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
        public double Grade { get; set; }

        [StringLength(50)]
        public string Text { get; set; }

        public Accomodation Accomodation { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Accomodation")]
        public int AccomodationId { get; set; }

        public AppUser AppUser { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }
    }
}