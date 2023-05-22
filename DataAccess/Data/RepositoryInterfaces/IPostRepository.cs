using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Data.RepositoryInterfaces
{
    public interface IPostRepository<TEntity> where TEntity : class, new()
    {
        IQueryable<TEntity> GetAll { get; }
        TEntity FindById(object id);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity editedEntity, TEntity originalEntity, out bool changed);
        TEntity Delete(TEntity entity);
        IEnumerable<Post> GetPostForUser(int Id);
        void SaveChanges();
    }
}
