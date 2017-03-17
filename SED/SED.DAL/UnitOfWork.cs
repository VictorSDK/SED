using SED.Models;
using System;

namespace SED.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        ISportEventRepository SportEvents { get; }
        IActivityRepository Activities { get; }
        IActivityTypeRepository ActivityTypes { get; }
        ISubscriberRepository Subscribers { get; }
        void Save();
    }

    public class UnitOfWork : IUnitOfWork 
    {
        private SEDContext context = new SEDContext();
        private ISportEventRepository sportEvents;
        private IActivityRepository activities;
        private IActivityTypeRepository activityTypes;
        private ISubscriberRepository subscribers;

        public ISportEventRepository SportEvents
        {
            get
            {
                if (this.sportEvents == null)
                {
                    this.sportEvents = new SportEventRepository(context);
                }
                return this.sportEvents;
            }
        }

        public IActivityRepository Activities
        {
            get
            {
                if (this.activities == null)
                {
                    this.activities = new ActivityRepository(context);
                }
                return this.activities;
            }
        }

        public IActivityTypeRepository ActivityTypes
        {
            get
            {
                if (this.activityTypes == null)
                {
                    this.activityTypes = new ActivityTypeRepository(context);
                }
                return this.activityTypes;
            }
        }

        public ISubscriberRepository Subscribers
        {
            get
            {
                if (this.subscribers == null)
                {
                    this.subscribers = new SubscriberRepository(context);
                }
                return this.subscribers;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
