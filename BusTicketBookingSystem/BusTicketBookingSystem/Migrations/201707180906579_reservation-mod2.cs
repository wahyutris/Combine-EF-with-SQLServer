namespace BusTicketBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservationmod2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ReservationModels", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReservationModels", "Date", c => c.DateTime(nullable: false));
        }
    }
}
