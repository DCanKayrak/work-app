using Core.Entity.Abstract;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private string creatingExceptionMessage;
        private string updatingExceptionMessage;
        private string readingExceptionMessage;
        private string deletingExceptionMessage;

        public EfEntityRepositoryBase()
        {
            this.creatingExceptionMessage = "Ekleme sırasında bir hata oluştu.";
            this.updatingExceptionMessage = "Güncellerken bir hata oluştu.";
            this.readingExceptionMessage = "Getirirken bir hata oluştu.";
            this.deletingExceptionMessage = "Silerken bir hata oluştu.";
        }
        public EfEntityRepositoryBase(
            string creatingExceptionMessage,
            string updatingExceptionMessage,
            string readingExceptionMessage,
            string deletingExceptionMessage
            )
        {
            this.creatingExceptionMessage = creatingExceptionMessage;
            this.updatingExceptionMessage = updatingExceptionMessage;
            this.readingExceptionMessage = readingExceptionMessage;
            this.deletingExceptionMessage = deletingExceptionMessage;
        }
        public void Create(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                try
                {
                    var ent = context.Entry(entity);
                    ent.State = EntityState.Added;
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(creatingExceptionMessage);
                }
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                try
                {
                    var ent = context.Entry(entity);
                    ent.State = EntityState.Deleted;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(deletingExceptionMessage);
                }
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                try
                {
                    TEntity? entity = context.Set<TEntity>().FirstOrDefault(filter);
                    return entity;
                }
                catch (Exception ex)
                {
                    throw new Exception(readingExceptionMessage);
                }
            }
        }
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                try
                {
                    var ent = context.Entry(entity);
                    ent.State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception(updatingExceptionMessage);
                }
            }
        }
    }
}