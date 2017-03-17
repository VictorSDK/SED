using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SED.Models;
using System.Data.Entity;

namespace SED.DAL
{
    public interface IActivityRepository : IRepository<Activity> {}

    public class ActivityRepository : SqlRepository<Activity>, IActivityRepository 
    {
        public ActivityRepository(SEDContext context): base(context) {}
    }


    public interface IActivityTypeRepository : IRepository<ActivityType> {}

    public class ActivityTypeRepository : SqlRepository<ActivityType>, IActivityTypeRepository
    {
        public ActivityTypeRepository(SEDContext context) : base(context) {}
    }

    public interface ISubscriberRepository : IRepository<Subscriber> {}

    public class SubscriberRepository:SqlRepository<Subscriber>, ISubscriberRepository
    {
        public SubscriberRepository(SEDContext context) : base(context) {}
    }


    public interface ISportEventRepository : IRepository<SportEvent> {}

    public class SportEventRepository: SqlRepository<SportEvent>, ISportEventRepository
    {
        public SportEventRepository(SEDContext context): base(context) {}

        public override void Update(SportEvent sportEvent)
        {
            // Delete           
            var currentActivities = new HashSet<int>(sportEvent.Activities.Where(a => a.ActivityId != 0).Select(a => a.ActivityId));
            var deletedActivities = context.Activities.Where(a => a.SportEventId == sportEvent.SportEventId && !currentActivities.Contains(a.ActivityId));
            context.Activities.RemoveRange(deletedActivities);

            // Add or update
            foreach (var activity in sportEvent.Activities)
            {
                context.Entry(activity).State = activity.ActivityId == 0 ? EntityState.Added : EntityState.Modified;
            }

            base.Update(sportEvent);
        }
    }
}
