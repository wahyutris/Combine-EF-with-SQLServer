using System;
using System.ComponentModel.DataAnnotations;

namespace BusTicketBookingSystem.Models.Vehicle
{
    public class AvailableBusViewModel
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public string Class { get; set; }

        virtual public Route Route { get; set; }

        [Display(Name = "Departure Time")]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Passenger Count")]
        //[Range(0, AppConstants.TRAIN_CAPACITY_BUSINESS)]
        public int PassengersCount { get; set; }

        [UIHint("Currency")]
        public int Fare { get; set; }
    }
}