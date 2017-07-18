using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusTicketBookingSystem.Models
{
    public class BusVehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Class { get; set; }
        public IEnumerable<SelectListItem> Classes { get; set; }

        public int Capacity { get; set; }

        [UIHint("Currency")]
        public int Fare { get; set; }

        [DisplayName("Departure Time")]
        public string DepartureTime { get; set; }
        //public IEnumerable<SelectListItem> DepartureTimes { get; set; }

        public int RouteID { get; set; }
        public string RouteName { get; set; }
        public RouteModel Route { get; set; }
        // Buat dropdown list yang akan dipake di controller dan view
        public IEnumerable<SelectListItem> Routes { get; set; }
        public BusVehicleModel()
        {
            Routes = new List<SelectListItem>();
        }
    }
}