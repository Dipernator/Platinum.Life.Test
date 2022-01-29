namespace Platinum.Life.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveAtt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentRequisitions", "Attachment_Id", "dbo.Attachments");
            DropIndex("dbo.PaymentRequisitions", new[] { "Attachment_Id" });
            AddColumn("dbo.Attachments", "PaymentRequisitionId", c => c.Int(nullable: false));
            DropColumn("dbo.PaymentRequisitions", "Attachment_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentRequisitions", "Attachment_Id", c => c.Int());
            DropColumn("dbo.Attachments", "PaymentRequisitionId");
            CreateIndex("dbo.PaymentRequisitions", "Attachment_Id");
            AddForeignKey("dbo.PaymentRequisitions", "Attachment_Id", "dbo.Attachments", "Id");
        }
    }
}
