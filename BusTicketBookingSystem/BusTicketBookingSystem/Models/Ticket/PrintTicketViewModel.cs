using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusTicketBookingSystem.Models.Ticket
{
    public class PrintTicketViewModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }

        [Display(Name = "Date")]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Date")]
        public string BusName { get; set; }

        [UIHint("PassengerClass")]
        public string BusClass { get; set; }

        [Range(1, 10)]
        public int PassengersCount { get; set; }

        public DateTime PurchasedOn { get; set; } = DateTime.Now;

        [UIHint("Currency")]
        public int Price { get; set; }
    }
}