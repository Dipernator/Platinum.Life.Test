namespace Platinum.Life.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Attchmentagain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentRequisitions", "Attachment_Id", c => c.Int());
            CreateIndex("dbo.PaymentRequisitions", "Attachment_Id");
            AddForeignKey("dbo.PaymentRequisitions", "Attachment_Id", "dbo.Attachments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentRequisitions", "Attachment_Id", "dbo.Attachments");
            DropIndex("dbo.PaymentRequisitions", new[] { "Attachment_Id" });
            DropColumn("dbo.PaymentRequisitions", "Attachment_Id");
        }
    }
}
