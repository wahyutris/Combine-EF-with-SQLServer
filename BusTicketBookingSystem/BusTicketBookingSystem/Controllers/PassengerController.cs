using BusTicketBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace BusTicketBookingSystem.Controllers
{
    public class PassengerController : Controller
    {
        private OperationDataContext context = null;

        public PassengerController()
        {
            context = new OperationDataContext();           
        }

        // GET: Passenger
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            // create the placeList variable
            List<PassengerModel> passengerList = new List<PassengerModel>();

            // perform linq operation
            var query = from passenger in context.Passengers select passenger;

            // store the query to list
            var passengers = query.ToList();

            // looping through all items in roles
            foreach (var passengerItem in passengers)
            {
                passengerList.Add(new PassengerModel()
                {
                    Id = passengerItem.Id,
                    UserID = passengerItem.UserID,
                    UserName = GetInfoUser(passengerItem.UserID).UserName,
                    FirstName = passengerItem.FirstName,
                    LastName = passengerItem.LastName,
                    Email = GetInfoUser(passengerItem.UserID).Email,
                    PhoneNumber = passengerItem.PhoneNumber,
                    BankName = passengerItem.BankName,
                    BankAccountNumber = passengerItem.BankAccountNumber
                });
            }

            return View(passengerList);
        }

        [Authorize]
        public ActionResult Manage()
        {
            // create the placeList variable
            List<PassengerModel> passengerList = new List<PassengerModel>();

            // perform linq operation
            var query = from passenger in context.Passengers
                        where passenger.UserID == User.Identity.GetUserId()
                        select passenger;

            // store the query to list
            var passengers = query.ToList();

            // looping through all items in roles
            foreach (var passengerItem in passengers)
            {
                passengerList.Add(new PassengerModel()
                {
                    Id = passengerItem.Id,
                    UserID = passengerItem.UserID,
                    UserName = GetInfoUser(passengerItem.UserID).UserName,
                    FirstName = passengerItem.FirstName,
                    LastName = passengerItem.LastName,
                    Email = GetInfoUser(passengerItem.UserID).Email,
                    PhoneNumber = passengerItem.PhoneNumber,
                    BankName = passengerItem.BankName,
                    BankAccountNumber = passengerItem.BankAccountNumber
                });
            }

            return View(passengerList);
        }

        // GET: Passenger/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            PassengerModel model = context.Passengers.Where(some => some.Id == id).Select(
                some => new PassengerModel()
                {
                    UserID = some.UserID,
                    UserName = GetInfoUser(some.UserID).UserName,
                    FirstName = some.FirstName,
                    LastName = some.LastName,
                    Email = GetInfoUser(some.UserID).Email,
                    PhoneNumber = some.PhoneNumber,
                    BankName = some.BankName,
                    BankAccountNumber = some.BankAccountNumber
                }).SingleOrDefault();

            return View(model);
        }

        // GET: Passenger/Create
        [Authorize]
        public ActionResult Create()
        {
            PassengerModel model = new PassengerModel();

            PopulateBanks(model);

            // Get current user email           
            ViewBag.currentEmail = GetInfoUser(User.Identity.GetUserId()).Email;            

            return View(model);
        }

        // POST: Passenger/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(PassengerModel model)
        {
            try
            {
                // TODO: Add insert logic here

                PopulateBanks(model);

                Passenger passenger = new Passenger()
                {
                    UserID = User.Identity.GetUserId(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    BankName = model.BankName,
                    BankAccountNumber = model.BankAccountNumber
                };

                context.Passengers.InsertOnSubmit(passenger);
                context.SubmitChanges();

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch
            {
                return View();
            }
        }

        // GET: Passenger/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            PassengerModel model = context.Passengers.Where(some => some.Id == id).Select(
                some => new PassengerModel()
                {
                    UserID = some.UserID,
                    UserName = GetInfoUser(some.UserID).UserName,
                    FirstName = some.FirstName,
                    LastName = some.LastName,
                    Email = GetInfoUser(some.UserID).Email,
                    PhoneNumber = some.PhoneNumber,
                    BankName = some.BankName,
                    BankAccountNumber = some.BankAccountNumber
                }).SingleOrDefault();

            PopulateBanks(model);

            ViewBag.currentEmail = GetInfoUser(User.Identity.GetUserId()).Email;

            return View(model);
        }

        // POST: Passenger/Edit/5
        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, PassengerModel model)
        {
            try
            {
                // TODO: Add update logic here

                PopulateBanks(model);

                Passenger passenger = context.Passengers.Where(some => some.Id == model.Id).Single<Passenger>();
                passenger.FirstName = model.FirstName;
                passenger.LastName = model.LastName;
                passenger.PhoneNumber = model.PhoneNumber;
                passenger.BankName = model.BankName;
                passenger.BankAccountNumber = model.BankAccountNumber;

                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Passenger/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            PassengerModel model = context.Passengers.Where(some => some.Id == id).Select(
                some => new PassengerModel()
                {
                    Id = some.Id,
                    UserID = some.UserID,
                    UserName = GetInfoUser(some.UserID).UserName,
                    FirstName = some.FirstName,
                    LastName = some.LastName,
                    Email = GetInfoUser(some.UserID).Email,
                    PhoneNumber = some.PhoneNumber,
                    BankName = some.BankName,
                    BankAccountNumber = some.BankAccountNumber
                }).SingleOrDefault();

            return View(model);
        }

        // POST: Passenger/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete(int id, PassengerModel model)
        {
            try
            {
                // TODO: Add delete logic here
                Passenger passenger = context.Passengers.Where(some => some.Id == model.Id).Single<Passenger>();

                context.Passengers.DeleteOnSubmit(passenger);
                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // For collection query in aspnetusers EF database
        // for reference : https://stackoverflow.com/questions/20925822/asp-net-mvc-5-identity-how-to-get-current-applicationuser
        private ApplicationUser GetInfoUser(string id)
        {
            return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);
        }

        // For Bank Select List Item
        private IEnumerable<string> GetAllBanks()
        {
            return new List<string>
            {
                "BNI",
                "BNI Syariah",
                "BRI",
                "BRI Syariah",
                "Bank Mandiri",
                "Bank Mandiri Syariah",
                "Bank Muamalat",
                "BTN",
                "BCA",
                "Bank Permata",
                "CIMB Niaga",
            };
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

        private void PopulateBanks(PassengerModel model)
        {
            // Let's get all states that we need for a DropDownList
            var banks = GetAllBanks();

            // Create a list of SelectListItems so these can be rendered on the page
            model.Banks = GetSelectListItems(banks);
        }
    }
}
