using BusTicketBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BusTicketBookingSystem.Controllers
{
    public class PlaceController : Controller
    {
        private OperationDataContext context = null;

        public PlaceController()
        {
            context = new OperationDataContext();
        }

        // GET: Place
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            // create the placeList variable
            List<PlaceModel> placeList = new List<PlaceModel>();

            // perform linq operation
            var query = from place in context.Places select place;

            // store the query to list
            var places = query.ToList();

            // looping through all items in roles
            foreach (var placeItem in places)
            {
                placeList.Add(new PlaceModel()
                {
                    Id = placeItem.Id,
                    Name = placeItem.Name
                });
            }

            return View(placeList);
        }

        // GET: Place/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Place/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(PlaceModel model)
        {
            try
            {
                // TODO: Add insert logic here
                Place place = new Place()
                {
                    Name = model.Name
                };

                context.Places.InsertOnSubmit(place);
                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Place/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            PlaceModel model = context.Places.Where(some => some.Id == id).Select(
                some => new PlaceModel()
                {
                    Name = some.Name
                }).SingleOrDefault();

            return View(model);
        }

        // POST: Place/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, PlaceModel model)
        {
            try
            {
                // TODO: Add update logic here
                Place place = context.Places.Where(some => some.Id == model.Id).Single<Place>();
                place.Name = model.Name;
                context.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Place/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {           
            Place place = context.Places.Where(some => some.Id == id).Single<Place>();

            context.Places.DeleteOnSubmit(place);
            context.SubmitChanges();                

            return RedirectToAction("Index");            
        }
    }
}
