using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [Required]
        [Phone]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        public string BankName { get; set; }
        public IEnumerable<SelectListItem> Banks { get; set; }
        public PassengerModel()
        {
            Banks = new List<SelectListItem>();
        }
        public string BankAccountNumber { get; set; }
    }
}