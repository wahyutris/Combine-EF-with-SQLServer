using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTicketBookingSystem.Utilities
{
    public class AppConstants
    {
        public const int BUS_CAPACITY = 50;

        public const int MAX_DAYS_BEFORE_RESERVATION = 13;

        //public static List<string> HOURS = new List<string> {
        //                                        "00:00",
        //                                        "01:00",
        //                                        "02:00",
        //                                        "03:00",
        //                                        "04:00",
        //                                        "05:00",
        //                                        "06:00",
        //                                        "07:00",
        //                                        "08:00",
        //                                        "09:00",
        //                                        "10:00",
        //                                        "11:00",
        //                                        "12:00",
        //                                        "13:00",
        //                                        "14:00",
        //                                        "15:00",
        //                                        "16:00",
        //                                        "17:00",
        //                                        "18:00",
        //                                        "19:00",
        //                                        "20:00",
        //                                        "21:00",
        //                                        "22:00",
        //                                        "23:00",
        //                                   };

        public static Dictionary<string, int> HOURS =
            new Dictionary<string, int> {
                { "00:00", 0 },
                { "01:00", 1 },
                { "02:00", 2 },
                { "03:00", 3 },
                { "04:00", 4 },
                { "05:00", 5 },
                { "06:00", 6 },
                { "07:00", 7 },
                { "08:00", 8 },
                { "09:00", 9 },
                { "10:00", 10 },
                { "11:00", 11 },
                { "12:00", 12 },
                { "13:00", 13 },
                { "14:00", 14 },
                { "15:00", 15 },
                { "16:00", 16 },
                { "17:00", 17 },
                { "18:00", 18 },
                { "19:00", 19 },
                { "20:00", 20 },
                { "21:00", 21 },
                { "22:00", 22 },
                { "23:00", 23 }
            };

        public static List<string> BusClasses = new List<string> {
                                                "Economy",
                                                "Economy AC",
                                                "Bussiness AC",
                                                "Executive",
                                                "Big Top",
                                           };
    }
}