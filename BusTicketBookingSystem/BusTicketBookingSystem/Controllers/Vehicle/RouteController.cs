using BusTicketBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusTicketBookingSystem.Controllers.Vehicle
{
    public class RouteController : Controller
    {
        private OperationDataContext context = null;

        public RouteController()
        {
            context = new OperationDataContext();
        }

        // GET: Route
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            // create the userRoleList variable
            List<RouteModel> routeList = new List<RouteModel>();

            // perform linq operation
            var query = from route in context.Routes                        
                        select new RouteModel
                        {
                            Id = route.Id,
                            OriginName = route.Origin,
                            DestinationName = route.Destination
                        };

            // store the query to list
            routeList = query.ToList();

            return View(routeList);
        }

        // GET: Route/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Route/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            RouteModel model = new RouteModel();
            PrepareRoute(model);

            return View(model);
        }

        // POST: Route/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(RouteModel model)
        {
            try
            {
                // TODO: Add insert logic here
                Route route = new Route()
                {
                    Origin = model.OriginName,
                    Destination = model.DestinationName
                };

                context.Routes.InsertOnSubmit(route);
                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                model = new RouteModel();
                PrepareRoute(model);

                return View(model);
            }
        }

        // GET: Route/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            RouteModel model = context.Routes.Where(some => some.Id == id).Select(
                some => new RouteModel()
                {
                    OriginName = some.Origin,
                    DestinationName = some.Destination
                }).SingleOrDefault();

            PrepareRoute(model);
            return View(model);
        }

        // POST: Route/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, RouteModel model)
        {
            try
            {
                // TODO: Add update logic here
                Route route = context.Routes.Where(some => some.Id == model.Id).Single<Route>();
                route.Origin = model.OriginName;
                route.Destination = model.DestinationName;
                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Route/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Route route = context.Routes.Where(some => some.Id == id).Single<Route>();

            context.Routes.DeleteOnSubmit(route);
            context.SubmitChanges();

            return RedirectToAction("Index");
        }

        private void PrepareRoute(RouteModel model)
        {
            model.Origins = context.Places.AsQueryable<Place>().Select(x =>
                            new SelectListItem()
                            {
                                Text = x.Name,
                                Value = x.Name.ToString()
                            });

            model.Destinations = context.Places.AsQueryable<Place>().Select(x =>
                                 new SelectListItem()
                                 {
                                     Text = x.Name,
                                     Value = x.Name.ToString()
                                 });
        }
    }
}
