using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BusTicketBookingSystem.Models
{
    public class PassengerModel
    {
        public int Id { get; set; }

        public string UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long PhoneNumber { get; set; }
    }
}