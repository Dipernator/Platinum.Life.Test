namespace Platinum.Life.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        CreateDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BankDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentRequisitionId = c.Int(nullable: false),
                        AccountNumber = c.String(),
                        AccountHolder = c.String(),
                        Bank = c.String(),
                        BranchCode = c.String(),
                        CreateDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentRequisitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        DateOfInvoice = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        CreateDateTime = c.DateTime(),
                        BankDetails_Id = c.Int(),
                        Signature_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankDetails", t => t.BankDetails_Id)
                .ForeignKey("dbo.Signatures", t => t.Signature_Id)
                .Index(t => t.BankDetails_Id)
                .Index(t => t.Signature_Id);
            
            CreateTable(
                "dbo.Signatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Url = c.String(),
                        CreateDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentRequisitions", "Signature_Id", "dbo.Signatures");
            DropForeignKey("dbo.PaymentRequisitions", "BankDetails_Id", "dbo.BankDetails");
            DropIndex("dbo.PaymentRequisitions", new[] { "Signature_Id" });
            DropIndex("dbo.PaymentRequisitions", new[] { "BankDetails_Id" });
            DropTable("dbo.Signatures");
            DropTable("dbo.PaymentRequisitions");
            DropTable("dbo.Departments");
            DropTable("dbo.BankDetails");
            DropTable("dbo.Attachements");
        }
    }
}
