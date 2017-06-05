using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookingApp.Models
{
    public class Country
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public double Code { get; set; }
        public IList<Region> Regions { get; set; }
    }
}