namespace BusTicketBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reservationedit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReservationModels", "BusCapacity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReservationModels", "BusCapacity");
        }
    }
}
