namespace BusTicketBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class details : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PassengerModels", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PassengerModels", "UserName");
        }
    }
}
