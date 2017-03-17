namespace SED.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Activity_Name : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activity", "Name");
        }
    }
}
