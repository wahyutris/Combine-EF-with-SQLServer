namespace BusTicketBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listPurchased : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ReservationModels", "DepartureTimeHour");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReservationModels", "DepartureTimeHour", c => c.Int(nullable: false));
        }
    }
}
