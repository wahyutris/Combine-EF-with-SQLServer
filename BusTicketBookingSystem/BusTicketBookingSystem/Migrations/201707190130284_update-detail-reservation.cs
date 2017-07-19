namespace BusTicketBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedetailreservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationModels", "PassengerName", c => c.String());
            AddColumn("dbo.ReservationModels", "PhoneNumber", c => c.String());
            AddColumn("dbo.ReservationModels", "BankName", c => c.String());
            AddColumn("dbo.ReservationModels", "BankAccount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservationModels", "BankAccount");
            DropColumn("dbo.ReservationModels", "BankName");
            DropColumn("dbo.ReservationModels", "PhoneNumber");
            DropColumn("dbo.ReservationModels", "PassengerName");
        }
    }
}
