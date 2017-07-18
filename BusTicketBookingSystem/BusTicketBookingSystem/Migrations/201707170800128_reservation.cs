namespace BusTicketBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReservationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalSeat = c.Int(nullable: false),
                        SeatNumber = c.String(),
                        TimeStamp = c.String(),
                        PassengerID = c.Int(nullable: false),
                        BusID = c.Int(nullable: false),
                        BusOrigin = c.String(),
                        BusDestination = c.String(),
                        DepartureTime = c.String(nullable: false),
                        DepartureTimeHour = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        TotalAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReservationModels");
        }
    }
}
