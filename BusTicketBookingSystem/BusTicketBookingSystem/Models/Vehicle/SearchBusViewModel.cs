using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusTicketBookingSystem.Models.Vehicle
{
    public class SearchBusViewModel
    {
        [Display(Name = "Departure")]
        [Required]
        public string Departure { get; set; }

        [Display(Name = "Arrival")]
        [Required]
        public string Arrival { get; set; }

        [Display(Name = "Departure time")]
        [Required]
        public string DepartureTime { get; set; }

        [Required]
        public int DepartureTimeHour { get; set; }
    }
}