namespace BusTicketBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservationmod1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationModels", "PurchasedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ReservationModels", "IsConfirmed", c => c.Boolean(nullable: false));
            DropColumn("dbo.ReservationModels", "TimeStamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReservationModels", "TimeStamp", c => c.String());
            DropColumn("dbo.ReservationModels", "IsConfirmed");
            DropColumn("dbo.ReservationModels", "PurchasedOn");
        }
    }
}
