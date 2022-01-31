namespace Platinum.Life.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Attachments", "File");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attachments", "File", c => c.Binary());
        }
    }
}
