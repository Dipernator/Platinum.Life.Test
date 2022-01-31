namespace Platinum.Life.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HttpPostedFileBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attachments", "File", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Attachments", "File");
        }
    }
}
