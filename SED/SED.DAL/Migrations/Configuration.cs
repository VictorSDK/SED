namespace SED.DAL.Migrations
{
    using SED.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SED.DAL.SEDContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SED.DAL.SEDContext context)
        {
            var activityTypes = new List<ActivityType>
			{
				new ActivityType { ActivityTypeId = 1,   Name = "5k"},
				new ActivityType { ActivityTypeId = 2,   Name = "10k"},
                new ActivityType { ActivityTypeId = 3,   Name = "15k"},
                new ActivityType { ActivityTypeId = 4,   Name = "21k", Description= "Half Marathon"},
                new ActivityType { ActivityTypeId = 2,   Name = "42k", Description= "Marathon"},
			};
            activityTypes.ForEach(s => context.ActivityTypes.AddOrUpdate(a => a.ActivityTypeId, s));
            context.SaveChanges();

            var locations = new List<Location>
			{
				new Location { LocationId = 1, Name = "Bosque Venustiano Carranza"},
				new Location { LocationId = 2, Name = "CRIT Durango"},
				new Location { LocationId = 3, Name = "Plaza Mayor Tórreon Oficial"}
			};
            locations.ForEach(s => context.Locations.AddOrUpdate(l => l.LocationId, s));
            context.SaveChanges();


            var sportEvents = new List<SportEvent>
			{
				new SportEvent { 
					 SportEventId = 1
					,Name = "10K SIMSA"
					,Date = DateTime.Today.AddDays(7)
					,Activities = new List<Activity>
					{
						new Activity{
							ActivityType = activityTypes.First(s=>s.Name == "10k")
						}
					}
					,Comments = new Comment[]{
						new Comment() { CommentId = 100, Content = "Very good", Rating = 4, UserName ="SuperLegs", Date = new DateTime(2016, 7, 14, 8,0,0,DateTimeKind.Local) } ,
						new Comment() { CommentId = 101, Content = "It's Ok, no too many Kenianos", Rating = 3, UserName ="Maratoner", Date = new DateTime(2016, 7, 13, 7,0,0,DateTimeKind.Local) } 
					}
					,Location = locations.First(l => l.Name == "Bosque Venustiano Carranza")
					,Rating = 4
				},
				new SportEvent {
					 SportEventId = 2
					,Name = "5 y 10 K CRIT Laguna"
					,Date = DateTime.Today.AddDays(14)
					,Activities = new List<Activity>
					{
						new Activity {
							ActivityType = activityTypes.First(s=>s.Name == "5k")
						},
						new Activity {
							 ActivityType = activityTypes.First(s=>s.Name == "10k")
						},
					}
					,Comments = new Comment[]{
						new Comment() { CommentId = 201, Content = "Low attendance", Rating = 2, UserName ="MaxSpeed" , Date = new DateTime(2016, 7, 14, 8,0,0,DateTimeKind.Local)},
						new Comment() { CommentId = 202, Content = "Lack of security", Rating = 1,  UserName ="Flying Pancho", Date = new DateTime(2016, 7, 14, 8,0,0,DateTimeKind.Local) } 
					}
					,Location = locations.First(l => l.Name =="CRIT Durango")
					,Rating = 2
				}, 
				new SportEvent { 
					 SportEventId = 3
					,Name = "5K UAC - 10K Festejos de Tórreon - Por Equipos"
					,Date = DateTime.Today.AddDays(21)
					,Activities = new List<Activity>
					{
						new Activity {
							ActivityType = activityTypes.First(s=>s.Name == "5k")
						},
						new Activity {
							 ActivityType = activityTypes.First(s=>s.Name == "10k")
						},
					}
					,Comments = new Comment[]{
						new Comment() { CommentId = 302, Content = "Recovery kit should include a banana", Rating = 3, UserName ="Skinny", Date = new DateTime(2016, 7, 14, 8,0,0,DateTimeKind.Local) },
						new Comment() { CommentId = 303, Content = "Lot of prices", Rating = 4,  UserName ="AnonymousRunner", Date = new DateTime(2016, 7, 14, 8,0,0,DateTimeKind.Local) } 
					}
					,Location = new Location(){ LocationId = 3, Name = "Plaza Mayor Tórreon Oficial" }
					,Rating = 3
				} 
			};
            sportEvents.ForEach(s => context.SportEvents.AddOrUpdate(e => e.SportEventId, s));
            context.SaveChanges();

        }
    }
}
