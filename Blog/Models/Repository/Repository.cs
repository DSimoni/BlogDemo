using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataLayer.Model;
using Blog.Models.Repository;

namespace Blog.Models.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        //Here DBEntities is out context  
        protected readonly EFDataContext db;

        public Repository(EFDataContext context)
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
        public TEntity Get(int? id)
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
