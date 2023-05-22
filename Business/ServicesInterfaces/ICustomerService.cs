using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Business.ServicesInterfaces
{
    public interface ICustomerService<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll();
        TEntity Create(string Name, TEntity entity);
        TEntity Update(object id, TEntity editedEntity, out bool changed);
        TEntity Delete(TEntity entity);
        void SaveChanges();
    }
}
