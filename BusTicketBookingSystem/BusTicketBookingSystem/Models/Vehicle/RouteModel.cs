using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusTicketBookingSystem.Models
{
    public class RouteModel
    {
        public int Id { get; set; }

        [DisplayName("Origin")]
        //public int OriginID { get; set; }
        public string OriginName { get; set; }
        // Buat dropdown list yang akan dipake di controller dan view
        public IEnumerable<SelectListItem> Origins { get; set; }

        [DisplayName("Destination")]
        //public int DestinationID { get; set; }
        public string DestinationName { get; set; }
        // Buat dropdown list yang akan dipake di controller dan view
        public IEnumerable<SelectListItem> Destinations { get; set; }

        public RouteModel()
        {
            Origins = new List<SelectListItem>();
            Destinations = new List<SelectListItem>();
        }
    }
}