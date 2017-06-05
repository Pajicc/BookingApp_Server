using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class AccomodationType
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        public IList<Accomodation> Accomodations { get; set; }
    }
}