using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BusTicketBookingSystem.Startup))]
namespace BusTicketBookingSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
