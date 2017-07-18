using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusTicketBookingSystem.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public int TotalSeat { get; set; }
        public string SeatNumber { get; set; }

        public DateTime PurchasedOn { get; set; }
        public int PassengerID { get; set; }

        public int BusID { get; set; }
        [Display(Name = "Bus Name")]
        public string BusName { get; set; }
        [Display(Name = "Bus Class")]
        public string BusClass { get; set; }
        public string BusOrigin { get; set; }
        public string BusDestination { get; set; }


        [Display(Name = "Departure time")]
        [Required]
        public string DepartureTime { get; set; }

        [Display(Name = "Total Amount")]
        [UIHint("Currency")]
        public int TotalAmount { get; set; }

        [Display(Name = "Status")]
        [UIHint("ConfirmationStatus")]
        public bool IsConfirmed { get; set; }
    }
}