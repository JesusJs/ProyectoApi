using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Business.ServicesInterfaces
{
    public interface IPostService<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();
        TEntity Create(object id, TEntity entity);
        TEntity Update(object id, TEntity editedEntity, out bool changed);
        TEntity Delete(TEntity entity);
        void SaveChanges();
    }
}
