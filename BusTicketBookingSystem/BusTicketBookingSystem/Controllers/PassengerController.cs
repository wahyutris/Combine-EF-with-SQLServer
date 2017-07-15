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

        // For collection query in aspnetusers EF database
        // for reference : https://stackoverflow.com/questions/20925822/asp-net-mvc-5-identity-how-to-get-current-applicationuser
        private ApplicationUser GetInfoUser(string id)
        {
            return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);
        }

        // GET: Passenger
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
                    PhoneNumber = passengerItem.PhoneNumber
                });
            }

            return View(passengerList);
        }

        // GET: Passenger/Details/5
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
                    PhoneNumber = some.PhoneNumber
                }).SingleOrDefault();

            return View(model);
        }

        // GET: Passenger/Create
        public ActionResult Create()
        {
            // Get current user email           
            ViewBag.currentEmail = GetInfoUser(User.Identity.GetUserId()).Email;

            return View();
        } 

        // POST: Passenger/Create
        [HttpPost]
        public ActionResult Create(PassengerModel model)
        {
            try
            {
                // TODO: Add insert logic here
                // Get current user email 
                //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                Passenger passenger = new Passenger()
                {
                    UserID = User.Identity.GetUserId(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = GetInfoUser(User.Identity.GetUserId()).Email,
                    PhoneNumber = model.PhoneNumber
                };

                context.Passengers.InsertOnSubmit(passenger);
                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Passenger/Edit/5
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
                    PhoneNumber = some.PhoneNumber
                }).SingleOrDefault();

            ViewBag.currentEmail = GetInfoUser(User.Identity.GetUserId()).Email;

            return View(model);
        }

        // POST: Passenger/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PassengerModel model)
        {
            try
            {
                // TODO: Add update logic here
                Passenger passenger = context.Passengers.Where(some => some.Id == model.Id).Single<Passenger>();
                passenger.FirstName = model.FirstName;
                passenger.LastName = model.LastName;
                passenger.PhoneNumber = model.PhoneNumber;

                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Passenger/Delete/5
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
                    PhoneNumber = some.PhoneNumber
                }).SingleOrDefault();

            return View(model);
        }

        // POST: Passenger/Delete/5
        [HttpPost]
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
    }
}
