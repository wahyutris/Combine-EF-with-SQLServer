using BusTicketBookingSystem.Utilities;
using System.Web;
using System.Web.Optimization;

namespace BusTicketBookingSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            var vendorScriptsBundle = new ScriptBundle("~/bundles/vendor").Include(
                "~/bower_components/jquery/dist/jquery.min.js",
                "~/bower_components/jquery.validate/jquery.validate.js",
                "~/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js",
                "~/bower_components/moment/min/moment-with-locales.min.js",
                "~/bower_components/eonasdan-bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js",
                "~/bower_components/bootstrap/dist/js/bootstrap.min.js");

            vendorScriptsBundle.Orderer = new KeepAsDeclaredBundleOrderer();
            bundles.Add(vendorScriptsBundle);

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/App/Search/purchase.js",
                "~/Scripts/App/Search/search.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/bower_components/bootswatch-dist/css/bootstrap.min.css",
                //"~/Content/bootstrap.css",
                "~/bower_components/eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css",
                "~/Content/Site.css"
            ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
