namespace BusTicketBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelistreservation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationModels", "PaymentProof", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservationModels", "PaymentProof");
        }
    }
}
