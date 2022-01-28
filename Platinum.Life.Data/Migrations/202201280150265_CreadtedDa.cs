namespace Platinum.Life.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreadtedDa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attachements", "ModifiedDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.BankDetails", "ModifiedDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Departments", "ModifiedDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.PaymentRequisitions", "ModifiedDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Signatures", "ModifiedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Attachements", "CreateDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BankDetails", "CreateDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Departments", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Departments", "CreateDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.PaymentRequisitions", "CreateDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Signatures", "CreateDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Signatures", "CreateDateTime", c => c.DateTime());
            AlterColumn("dbo.PaymentRequisitions", "CreateDateTime", c => c.DateTime());
            AlterColumn("dbo.Departments", "CreateDateTime", c => c.DateTime());
            AlterColumn("dbo.Departments", "Name", c => c.String());
            AlterColumn("dbo.BankDetails", "CreateDateTime", c => c.DateTime());
            AlterColumn("dbo.Attachements", "CreateDateTime", c => c.DateTime());
            DropColumn("dbo.Signatures", "ModifiedDateTime");
            DropColumn("dbo.PaymentRequisitions", "ModifiedDateTime");
            DropColumn("dbo.Departments", "ModifiedDateTime");
            DropColumn("dbo.BankDetails", "ModifiedDateTime");
            DropColumn("dbo.Attachements", "ModifiedDateTime");
        }
    }
}
