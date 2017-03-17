namespace SED.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SportEvent", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.SportEvent", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SportEvent", "Date", c => c.DateTime());
            AlterColumn("dbo.SportEvent", "Name", c => c.String());
        }
    }
}
