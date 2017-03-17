namespace SED.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivityType",
                c => new
                    {
                        ActivityTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ActivityTypeId);
            
            CreateTable(
                "dbo.Activity",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        SportEventId = c.Int(nullable: false),
                        ActivityTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityId)
                .ForeignKey("dbo.ActivityType", t => t.ActivityTypeId, cascadeDelete: true)
                .ForeignKey("dbo.SportEvent", t => t.SportEventId, cascadeDelete: true)
                .Index(t => t.SportEventId)
                .Index(t => t.ActivityTypeId);
            
            CreateTable(
                "dbo.SportEvent",
                c => new
                    {
                        SportEventId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(),
                        Rating = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SportEventId)
                .ForeignKey("dbo.Location", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        UserImageUrl = c.String(),
                        UserName = c.String(),
                        Content = c.String(),
                        Rating = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        SportEventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.SportEvent", t => t.SportEventId, cascadeDelete: true)
                .Index(t => t.SportEventId);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.LocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SportEvent", "LocationId", "dbo.Location");
            DropForeignKey("dbo.Comment", "SportEventId", "dbo.SportEvent");
            DropForeignKey("dbo.Activity", "SportEventId", "dbo.SportEvent");
            DropForeignKey("dbo.Activity", "ActivityTypeId", "dbo.ActivityType");
            DropIndex("dbo.Comment", new[] { "SportEventId" });
            DropIndex("dbo.SportEvent", new[] { "LocationId" });
            DropIndex("dbo.Activity", new[] { "ActivityTypeId" });
            DropIndex("dbo.Activity", new[] { "SportEventId" });
            DropTable("dbo.Location");
            DropTable("dbo.Comment");
            DropTable("dbo.SportEvent");
            DropTable("dbo.Activity");
            DropTable("dbo.ActivityType");
        }
    }
}
