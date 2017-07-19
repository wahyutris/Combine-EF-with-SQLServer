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
        [Display(Name = "Seat reserved")]
        public int TotalSeat { get; set; }
        public string SeatNumber { get; set; }

        public DateTime PurchasedOn { get; set; }

        [Display(Name = "Passenger ID")]
        public int PassengerID { get; set; }
        [Display(Name = "Passenger Name")]
        public string PassengerName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Bank")]
        public string BankName { get; set; }
        [Display(Name = "Account")]
        public string BankAccount { get; set; }

        public int BusID { get; set; }
        [Display(Name = "Bus Name")]
        public string BusName { get; set; }
        [Display(Name = "Bus Class")]
        public string BusClass { get; set; }
        [Display(Name = "Departure")]
        public string BusOrigin { get; set; }
        [Display(Name = "Arrival")]
        public string BusDestination { get; set; }
        [Display(Name = "Capacity")]
        public int BusCapacity { get; set; }


        [Display(Name = "Departure time")]
        [Required]
        public string DepartureTime { get; set; }

        [Display(Name = "Total Amount")]
        [UIHint("Currency")]
        public int TotalAmount { get; set; }

        [Display(Name = "Status")]
        [UIHint("ConfirmationStatus")]
        public bool IsConfirmed { get; set; }

        [Display(Name = "Payment Proof")]
        public string PaymentProof { get; set; }
    }
}