using BusTicketBookingSystem.Models;
using BusTicketBookingSystem.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(BusTicketBookingSystem.Startup))]
namespace BusTicketBookingSystem
{
    public partial class Startup
    {
        private static Random rand = new Random();

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
            BusVehicleSeed();
        }

        // In this method we will create default User roles and Admin user for login    
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "Admin@Admin.com";

                string userPWD = "Adm!n94";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating User role     
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }

        private void BusVehicleSeed()
        {
            OperationDataContext context = new OperationDataContext();

            var bandung = new PlaceModel { Name = "Bandung" };
            var yogyakarta = new PlaceModel { Name = "Yogyakarta" };
            var curup = new PlaceModel { Name = "Curup" };
            var surabaya = new PlaceModel { Name = "Surabaya" };
            var denpasar = new PlaceModel { Name = "Denpasar" };

            var places = new List<PlaceModel>()
            {
                bandung, yogyakarta, curup, surabaya, denpasar
            };

            List<RouteModel> busRoutes = new List<RouteModel>();

            // Generate list of busRoute
            foreach (var origin in places)
            {
                foreach (var destination in places)
                {
                    if (origin != destination)
                        busRoutes.Add(new RouteModel { OriginName = origin.Name, DestinationName = destination.Name });
                }
            }

            if (context.Places.Count() == 0)
            {
                // Insert all places above to database
                foreach (var item in places)
                {
                    Place place = new Place()
                    {
                        Name = item.Name
                    };

                    context.Places.InsertOnSubmit(place);
                    context.SubmitChanges();
                }

                // Insert all busRoute to database
                foreach (var item in busRoutes)
                {
                    Route route = new Route()
                    {
                        Origin = item.OriginName,
                        Destination = item.DestinationName
                    };

                    context.Routes.InsertOnSubmit(route);
                    context.SubmitChanges();
                }
            }

            if (context.BusVehicles.Count() == 0)
            {
                var dates = DateTimeGenerator.GetDaysInRange(DateTime.Today, DateTime.Today.AddDays(60));
                var busses = new List<BusVehicleModel>();

                var busNames = new List<string>(){
                    "Primajasa", "MGI", "Budiman", "Putra Raflesia", "Lorena", "Haryanto"
                };
                var busClasses = AppConstants.BusClasses;

                foreach (DateTime date in dates)
                {
                    //var tes = date; 
                    for (double i = 0; i < 24; i += 2)
                    {
                        foreach (var route in busRoutes)
                        {
                            int randName = rand.Next(busNames.Count);
                            int randClass = rand.Next(busClasses.Count);

                            busses.Add(BusGenerator.GenerateBus(route,
                                        date.AddHours(rand.Next(0, 24)).AddMinutes(rand.Next(0, 60)),
                                        busNames[randName],
                                        busClasses[randClass]));
                        }
                    }
                }

                // Insert all busvehicle to database
                // Belum bisa, masih ada errornya
                foreach (var item in busses)
                {
                    BusVehicle busVehicle = new BusVehicle()
                    {
                        Name = item.Name,
                        Class = item.Class,
                        DepartureTime = DateTime.Parse(item.DepartureTime),
                        RouteID = item.RouteID
                    };

                    //context.BusVehicles.InsertOnSubmit(busVehicle);
                    //context.SubmitChanges();
                }
            }
        }
    }
}
