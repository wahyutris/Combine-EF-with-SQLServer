namespace BusTicketBookingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PassengerModels", "BankName", c => c.String());
            AddColumn("dbo.PassengerModels", "BankAccountNumber", c => c.String());
            AlterColumn("dbo.PassengerModels", "PhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PassengerModels", "PhoneNumber", c => c.Long(nullable: false));
            DropColumn("dbo.PassengerModels", "BankAccountNumber");
            DropColumn("dbo.PassengerModels", "BankName");
        }
    }
}
