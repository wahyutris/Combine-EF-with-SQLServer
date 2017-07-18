using BusTicketBookingSystem.Models;
using BusTicketBookingSystem.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusTicketBookingSystem.Filters
{
    public class InitializeSearchBusFormAttribute : ActionFilterAttribute
    {
        private OperationDataContext context = new OperationDataContext();

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // Get list cities and hours
            var cities = context.Places.Distinct().ToList();
            var hours = AppConstants.HOURS;

            filterContext.Controller.ViewBag.Hours = hours;
            filterContext.Controller.ViewBag.Cities = cities;
            filterContext.Controller.ViewBag.CitiesJson = JsonConvert.SerializeObject(
                cities.Select(c => c.Name).ToArray());

            base.OnResultExecuting(filterContext);
        }
    }
}