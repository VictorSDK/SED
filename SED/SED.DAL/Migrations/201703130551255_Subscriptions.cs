namespace SED.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subscriptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscriber",
                c => new
                    {
                        SubscriberId = c.Int(nullable: false, identity: true),
                        SportEventId = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SubscriberId)
                .ForeignKey("dbo.SportEvent", t => t.SportEventId, cascadeDelete: true)
                .Index(t => t.SportEventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subscriber", "SportEventId", "dbo.SportEvent");
            DropIndex("dbo.Subscriber", new[] { "SportEventId" });
            DropTable("dbo.Subscriber");
        }
    }
}
