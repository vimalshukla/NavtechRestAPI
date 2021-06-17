﻿using System;
using System.Collections.Generic;
using Database;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        //Here DBEntities is out context  
        protected readonly SimpleStoreEntities db;
        public Repository(SimpleStoreEntities context)
        {
            db = context;
        }
        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            db.Set<TEntity>().AddRange(entities);
        }
        public void Remove(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            db.Set<TEntity>().RemoveRange(entities);
        }
        public TEntity Get(int id)
        {
            return db.Set<TEntity>().Find(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return db.Set<TEntity>().Where(predicate);
        }
    }
}
