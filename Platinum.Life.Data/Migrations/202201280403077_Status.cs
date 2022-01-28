namespace Platinum.Life.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Status : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentRequisitions", "StatusId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentRequisitions", "StatusId");
        }
    }
}
