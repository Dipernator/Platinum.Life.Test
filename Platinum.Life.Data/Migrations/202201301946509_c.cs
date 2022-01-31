namespace Platinum.Life.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class c : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BankDetails", "AccountNumber", c => c.String(nullable: false));
            AlterColumn("dbo.BankDetails", "AccountHolder", c => c.String(nullable: false));
            AlterColumn("dbo.BankDetails", "Bank", c => c.String(nullable: false));
            AlterColumn("dbo.BankDetails", "BranchCode", c => c.String(nullable: false));
            AlterColumn("dbo.PaymentRequisitions", "UserId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentRequisitions", "UserId", c => c.String());
            AlterColumn("dbo.BankDetails", "BranchCode", c => c.String());
            AlterColumn("dbo.BankDetails", "Bank", c => c.String());
            AlterColumn("dbo.BankDetails", "AccountHolder", c => c.String());
            AlterColumn("dbo.BankDetails", "AccountNumber", c => c.String());
        }
    }
}
