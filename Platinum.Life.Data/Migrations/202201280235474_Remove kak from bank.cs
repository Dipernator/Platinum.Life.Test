namespace Platinum.Life.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removekakfrombank : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.BankDetails", "PaymentRequisitionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BankDetails", "PaymentRequisitionId", c => c.Int(nullable: false));
        }
    }
}
