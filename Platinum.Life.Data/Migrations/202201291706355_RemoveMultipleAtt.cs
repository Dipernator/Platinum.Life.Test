namespace Platinum.Life.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMultipleAtt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attachments", "PaymentRequisition_Id", "dbo.PaymentRequisitions");
            DropIndex("dbo.Attachments", new[] { "PaymentRequisition_Id" });
            DropColumn("dbo.Attachments", "PaymentRequisition_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attachments", "PaymentRequisition_Id", c => c.Int());
            CreateIndex("dbo.Attachments", "PaymentRequisition_Id");
            AddForeignKey("dbo.Attachments", "PaymentRequisition_Id", "dbo.PaymentRequisitions", "Id");
        }
    }
}
