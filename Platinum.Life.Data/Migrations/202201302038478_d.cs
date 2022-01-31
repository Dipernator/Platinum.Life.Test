namespace Platinum.Life.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentRequisitions", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentRequisitions", "UserId", c => c.String(nullable: false));
        }
    }
}
