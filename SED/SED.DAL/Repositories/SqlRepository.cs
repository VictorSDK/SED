using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SED.DAL
{
    //public interface IEntity
    //{
    //    int Id { get; }
    //}

    public interface IRepository<TEntity> where TEntity : class//, IEntity
    {
        IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);
        IList<TEntity> GetList(Func<TEntity, bool> where, params Expression<Func<TEntity, object>>[] navigationProperties);
        TEntity GetSingle(Func<TEntity, bool> filter, params Expression<Func<TEntity, object>>[] navigationProperties);
        void Insert(TEntity newEntity);
        void Delete(object Id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }

    public class SqlRepository<TEntity> : IRepository<TEntity> where TEntity : class//, IEntity
    {
        internal SEDContext context;
        internal DbSet<TEntity> dbSet;

        public SqlRepository(SEDContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IList<TEntity> GetAll(
            params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            List<TEntity> list;

            IQueryable<TEntity> dbQuery = dbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .ToList<TEntity>();

            return list;
        }

        public virtual IList<TEntity> GetList(
            Func<TEntity, bool> filter,
            params Expression<Func<TEntity, object>>[] navigationProperties )
        {
            List<TEntity> list;

            IQueryable<TEntity> dbQuery = dbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .Where(filter)
                .ToList<TEntity>();

            return list;
        }

        public virtual TEntity GetSingle(Func<TEntity, bool> filter,
             params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            TEntity item = null;
            IQueryable<TEntity> dbQuery = this.dbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(filter); //Apply where clause
            return item;
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            //dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
