using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Data.RepositoryInterfaces
{
    public interface ICustomerRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll { get; }
        TEntity FindById(object id);
        bool FindByName(string name);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity editedEntity, TEntity originalEntity, out bool changed);
        TEntity Delete(TEntity entity);
        void SaveChanges();
    }
}
