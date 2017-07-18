using BusTicketBookingSystem.Filters;
using BusTicketBookingSystem.Models;
using BusTicketBookingSystem.Models.Vehicle;
using BusTicketBookingSystem.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BusTicketBookingSystem.Controllers
{
    public class ReservationController : Controller
    {
        private OperationDataContext context = null;

        public ReservationController()
        {
            context = new OperationDataContext();
        }

        // GET: Reservation
        public ActionResult Index()
        {
            // create the placeList variable
            List<ReservationModel> reservationList = new List<ReservationModel>();

            // perform linq operation
            var query = from reservation in context.Reservations select reservation;

            // store the query to list
            var reservations = query.ToList();

            // looping through all items in roles
            foreach (var reservationItem in reservations)
            {
                reservationList.Add(new ReservationModel()
                {
                    Id = reservationItem.Id,
                    TotalSeat = reservationItem.TotalSeat,
                    SeatNumber = reservationItem.SeatNumber,
                    PurchasedOn = reservationItem.PurchasedOn,
                    PassengerID = reservationItem.PassengerID,
                    BusID = reservationItem.BusID,
                    TotalAmount = reservationItem.TotalAmount,
                    IsConfirmed = reservationItem.IsConfirmed
                });
            }

            return View(reservationList);
        }

        // GET: Reservation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Reservation/Create
        [HttpGet]
        [InitializeSearchBusForm]
        [AllowAnonymous]
        public ActionResult Create()
        {
            ReservationModel model = new ReservationModel();

            return View(model);
        }

        // GET: Reservation/Search
        [HttpGet]
        [InitializeSearchBusForm]
        [AllowAnonymous]
        public ActionResult Search(SearchBusViewModel busTicket)
        {
            ViewBag.Errors = new List<string>();

            if (ModelState.IsValid)
            {
                DateTime departureTime = DateTime.Parse(busTicket.DepartureTime)
                                                 .AddHours(busTicket.DepartureTimeHour);

                if (departureTime < DateTime.Now)
                {
                    ModelState.AddModelError("AlreadyDeparted", "The time of departure for your search has already passed.");
                    return View(nameof(this.Index), busTicket);
                }

                var busRoute = context.Routes
                                      .Where(r => r.Origin == busTicket.Departure)
                                      .Where(r => r.Destination == busTicket.Arrival)
                                      .SingleOrDefault();

                if (busRoute == null)
                {
                    ModelState.AddModelError("InvalidRoute", "Departure and arrival cities should be different.");
                    return View(nameof(this.Index), busTicket);
                }

                var bus = context.BusVehicles
                                 .Where(b => b.DepartureTime >= departureTime)
                                 .Where(b => b.RouteID == busRoute.Id)
                                 .OrderBy(b => b.DepartureTime)
                                 .Take(3)
                                 .Select(bv => new AvailableBusViewModel()
                                 {
                                     Id = bv.Id,
                                     Capacity = bv.Capacity,
                                     Class = bv.Class,
                                     Route = bv.Route,
                                     Fare = bv.Fare,
                                     DepartureTime = bv.DepartureTime,
                                     //PassengersCount = context.Reservations
                                     //                         .Where(r => r.BusID == bv.Id)
                                     //                         .Where(r => r.IsConfirmed)
                                     //                         .Select(r => r.TotalSeat)
                                     //                         .DefaultIfEmpty()
                                     //                         .Sum()
                                 })
                                .ToList();

                return View(bus);
            }

            return View();
        }

        // GET /ticket/purchase/{id}
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Purchase(int id)
        {
            var bus = context.BusVehicles
                             .Where(b=>b.Id==id)
                             .SingleOrDefault();

            var viewModel = new AvailableBusViewModel()
            {
                Id = bus.Id,
                Capacity = bus.Capacity,
                Class = bus.Class,
                Route = bus.Route,
                Fare = bus.Fare,
                DepartureTime = bus.DepartureTime,
            };

            return View(viewModel);
        }

        // POST /ticket/purchase
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase(PurchaseTicketViewModel ticket)
        {
            try
            {
                // TODO: Add insert logic here
                var currentUserId = User.Identity.GetUserId();
                var bus = context.BusVehicles
                                 .Where(b => b.Id == ticket.Id)
                                 .SingleOrDefault();

                if (bus == null)
                {
                    return new HttpStatusCodeResult(404);
                }

                var viewModel = new AvailableBusViewModel()
                {
                    Id = bus.Id,
                    Capacity = bus.Capacity,
                    Class = bus.Class,
                    Route = bus.Route,
                    Fare = bus.Fare,
                    DepartureTime = bus.DepartureTime,
                };

                if (bus.DepartureTime < DateTime.Now)
                {
                    ViewBag.Error = "This bus has already departed.";
                    return View(viewModel);
                }
                else if (bus.DepartureTime > DateTime.Now.AddDays(AppConstants.MAX_DAYS_BEFORE_RESERVATION))
                {
                    ViewBag.Error = "You can only purchase tickets as late as two weeks before departure.";
                    return View(viewModel);
                }

                ReservationModel model = new ReservationModel()
                {
                    TotalSeat = ticket.PassengersCount,
                    PurchasedOn = DateTime.Now,
                    PassengerID = context.Passengers
                                         .Where(u => u.UserID == currentUserId)
                                         .Select(p => p.Id)
                                         .SingleOrDefault(),
                    BusID = bus.Id,
                    TotalAmount = ticket.PassengersCount * bus.Fare,
                    IsConfirmed = false
                };

                Reservation reservation = new Reservation()
                {
                    //TotalSeat = model.TotalSeat,
                    //PurchasedOn = model.PurchasedOn,
                    //PassengerID = model.PassengerID,
                    //BusID = model.BusID,
                    //Date = model.DateId,
                    //TotalAmount = model.TotalAmount,
                    //IsConfirmed = model.IsConfirmed

                    TotalSeat = ticket.PassengersCount,
                    PurchasedOn = DateTime.Now,
                    PassengerID = context.Passengers
                                         .Where(u => u.UserID == currentUserId)
                                         .Select(p => p.Id)
                                         .SingleOrDefault(),
                    BusID = bus.Id,
                    TotalAmount = ticket.PassengersCount * bus.Fare,
                    IsConfirmed = false
                };

                context.Reservations.InsertOnSubmit(reservation);
                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }            
        }

        // GET /tickets
        [HttpGet]
        [Authorize]
        public ActionResult List()
        {
            string currentUserId = User.Identity.GetUserId();

            var query = from reservation in context.Reservations
                        join bus in context.BusVehicles
                        on reservation.BusID equals bus.Id
                        join passenger in context.Passengers
                        on reservation.PassengerID equals passenger.Id
                        where passenger.UserID.ToString() == currentUserId
                        where bus.DepartureTime > DateTime.Now
                        orderby bus.DepartureTime
                        select new ReservationModel
                        {
                            Id = reservation.Id,
                            TotalSeat = reservation.TotalSeat,
                            BusOrigin = bus.Route.Origin,
                            BusDestination = bus.Route.Destination,
                            BusName = bus.Name,
                            BusClass = bus.Class,
                            DepartureTime = bus.DepartureTime.ToString(),
                            TotalAmount = reservation.TotalAmount,
                            IsConfirmed = reservation.IsConfirmed
                        };

            var tickets = query.ToList();

            return View(tickets);
        }

        // GET /ticket/cancel/{id}
        [HttpGet]
        [Authorize]
        public ActionResult Cancel(int id)
        {
            var ticketToCancel = context.Reservations.Where(some => some.Id == id).Single<Reservation>();

            if (ticketToCancel == null ||
                ticketToCancel.Passenger.UserID.ToString() != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(404);
            }

            context.Reservations.DeleteOnSubmit(ticketToCancel);
            context.SubmitChanges();

            return RedirectToAction(nameof(this.List));
        }

        // AJAX
        // GET: /Ticket/IsCancellableWithoutFee/{id}
        [HttpGet]
        [Authorize]
        [AjaxOnly]
        public JsonResult IsCancellableWithoutFee(int ticketId)
        {
            var ticket = context.Reservations.Where(some => some.Id == ticketId).Single<Reservation>();

            if (ticket == null)
            {
                return Json("undefined", JsonRequestBehavior.AllowGet);
            }

            return Json(ticket.BusVehicle.DepartureTime.Subtract(DateTime.Now)
                   >= new TimeSpan(3, 0, 0, 0, 0) || !ticket.IsConfirmed, JsonRequestBehavior.AllowGet);
        }

        // GET: Reservation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reservation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reservation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reservation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
