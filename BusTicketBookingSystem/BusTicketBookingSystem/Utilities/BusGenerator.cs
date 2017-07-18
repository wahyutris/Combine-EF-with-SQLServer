using BusTicketBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketBookingSystem.Utilities
{
    public class BusGenerator
    {
        public static BusVehicleModel GenerateBus(RouteModel route, DateTime departureTime, string busName, string busClass)
        {
            return new BusVehicleModel()
            {
                Name = busName,
                Class = busClass,
                Route = route,
                DepartureTime = departureTime.ToString()
            };
        }
    }
}