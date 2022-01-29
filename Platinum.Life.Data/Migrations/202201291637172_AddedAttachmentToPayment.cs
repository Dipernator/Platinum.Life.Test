namespace Platinum.Life.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAttachmentToPayment : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Attachements", newName: "Attachments");
            AddColumn("dbo.Attachments", "PaymentRequisition_Id", c => c.Int());
            CreateIndex("dbo.Attachments", "PaymentRequisition_Id");
            AddForeignKey("dbo.Attachments", "PaymentRequisition_Id", "dbo.PaymentRequisitions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachments", "PaymentRequisition_Id", "dbo.PaymentRequisitions");
            DropIndex("dbo.Attachments", new[] { "PaymentRequisition_Id" });
            DropColumn("dbo.Attachments", "PaymentRequisition_Id");
            RenameTable(name: "dbo.Attachments", newName: "Attachements");
        }
    }
}
