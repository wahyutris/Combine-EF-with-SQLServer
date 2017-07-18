using BusTicketBookingSystem.Models;
using BusTicketBookingSystem.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusTicketBookingSystem.Controllers.Vehicle
{
    public class BusVehicleController : Controller
    {
        private OperationDataContext context = null;

        public BusVehicleController()
        {
            context = new OperationDataContext();
        }

        // GET: BusVehicle
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            // create the BusList variable
            List<BusVehicleModel> busVehicleList = new List<BusVehicleModel>();

            // perform linq operation
            var query = from bus in context.BusVehicles
                        join route in context.Routes
                        on bus.RouteID equals route.Id
                        select new BusVehicleModel
                        {
                            Id = bus.Id,
                            Name = bus.Name,
                            RouteName = route.Origin + " ==> " + route.Destination,
                            Class = bus.Class,
                            Capacity = (int)bus.Capacity,
                            Fare = (int)bus.Fare,
                            DepartureTime = bus.DepartureTime.ToString()
                        };

            // store the query to list
            busVehicleList = query.ToList();

            return View(busVehicleList);
        }

        // GET: BusVehicle/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BusVehicle/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            BusVehicleModel model = new BusVehicleModel();
            // Preparing route for dropdown list
            PrepareBusVehicle(model);

            // Let's get all states that we need for a DropDownList
            var classes = AppConstants.BusClasses;

            // Create a list of SelectListItems so these can be rendered on the page
            model.Classes = GetSelectListItems(classes);

            return View(model);
        }

        // POST: BusVehicle/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(BusVehicleModel model)
        {
            try
            {
                // TODO: Add insert logic here

                // Get all states again
                var times = AppConstants.HOURS;
                var classes = AppConstants.BusClasses;

                // Set these states on the model. We need to do this because
                // only the selected value from the DropDownList is posted back, not the whole
                // list of states.
                //model.DepartureTimes = GetSelectListItems(times.Keys);
                //ViewBag.Hours = times;
                model.Classes = GetSelectListItems(classes);

                BusVehicle busVehicle = new BusVehicle()
                {
                    Name = model.Name,
                    Class = model.Class,
                    Capacity = model.Capacity,
                    Fare = model.Fare,
                    DepartureTime = DateTime.Parse(model.DepartureTime),
                    RouteID = model.RouteID
                };

                context.BusVehicles.InsertOnSubmit(busVehicle);
                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                model = new BusVehicleModel();
                PrepareBusVehicle(model);

                // Create a list of SelectListItems so these can be rendered on the page
                //model.DepartureTimes = GetSelectListItems(AppConstants.HOURS);
                //ViewBag.Hours = AppConstants.HOURS;
                model.Classes = GetSelectListItems(AppConstants.BusClasses);

                return View(model);
            }
        }

        // GET: BusVehicle/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            BusVehicleModel model = context.BusVehicles.Where(some => some.Id == id).Select(
                some => new BusVehicleModel()
                {
                    Name = some.Name,
                    Class = some.Class,
                    Capacity = (int)some.Capacity,
                    Fare = (int)some.Fare,
                    DepartureTime = some.DepartureTime.ToString(),
                    RouteID = some.RouteID
                }).SingleOrDefault();

            // Preparing route for dropdown list
            PrepareBusVehicle(model);

            // Let's get all states that we need for a DropDownList
            var classes = AppConstants.BusClasses;

            // Create a list of SelectListItems so these can be rendered on the page
            model.Classes = GetSelectListItems(classes);

            return View(model);
        }

        // POST: BusVehicle/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, BusVehicleModel model)
        {
            try
            {
                // TODO: Add update logic here
                // Get all states again
                var classes = AppConstants.BusClasses;

                // Set these states on the model. We need to do this because
                // only the selected value from the DropDownList is posted back, not the whole
                // list of states.
                model.Classes = GetSelectListItems(classes);

                BusVehicle busVehicle = context.BusVehicles.Where(some => some.Id == model.Id).Single<BusVehicle>();
                busVehicle.Name = model.Name;
                busVehicle.Class = model.Class;
                busVehicle.Capacity = model.Capacity;
                busVehicle.Fare = model.Fare;
                busVehicle.DepartureTime = DateTime.Parse(model.DepartureTime);
                busVehicle.RouteID = model.RouteID;

                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BusVehicle/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            BusVehicleModel model = context.BusVehicles.Where(some => some.Id == id).Select(
                some => new BusVehicleModel()
                {
                    Id = some.Id,
                    Name = some.Name,
                    RouteID = some.RouteID,
                    Class = some.Class,
                    Capacity = (int)some.Capacity,
                    Fare = (int)some.Fare,
                    DepartureTime = some.DepartureTime.ToString()
                }).SingleOrDefault();

            PrepareBusVehicle(model);

            return View(model);
        }

        // POST: BusVehicle/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id, BusVehicleModel model)
        {
            try
            {
                // TODO: Add delete logic here
                BusVehicle busVehicle = context.BusVehicles.Where(some => some.Id == model.Id).Single<BusVehicle>();

                context.BusVehicles.DeleteOnSubmit(busVehicle);
                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // This is one of the most important parts in the whole example.
        // This function takes a list of strings and returns a list of SelectListItem objects.
        // These objects are going to be used later in the SignUp.html template to render the
        // DropDownList.
        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }

        private void PrepareBusVehicle(BusVehicleModel model)
        {
            model.Routes = context.Routes.AsQueryable<Route>().Select(x =>
                          new SelectListItem()
                          {
                              Text = x.Origin + " ==> " + x.Destination,
                              Value = x.Id.ToString()
                          });
        }
    }
}
