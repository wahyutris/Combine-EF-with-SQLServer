namespace BusTicketBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listPurchased2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationModels", "BusName", c => c.String());
            AddColumn("dbo.ReservationModels", "BusClass", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservationModels", "BusClass");
            DropColumn("dbo.ReservationModels", "BusName");
        }
    }
}
