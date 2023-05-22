using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using DataAccess.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess
{
    public class CustomerRepository<TEntity> : ICustomerRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Contexto
        /// </summary>
        JujuTestContext _context;
        /// <summary>
        /// Entidad
        /// </summary>
        protected DbSet<TEntity> _dbSet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public CustomerRepository(JujuTestContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }


        /// <summary>
        /// Consulta todas las entidades
        /// </summary>
        public virtual IQueryable<TEntity> GetAll
        {
            get { return _dbSet; }
        }

        /// <summary>
        /// Consulta una entidad por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity FindById(object id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Consulta si existe un nombre de usuario
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public virtual bool FindByName(string Name)
        {
            return _context.Customer.Any(e=> e.Name == Name);
        }

        /// <summary>
        /// Crea un entidad (Guarda)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();

            return entity;
        }


        /// <summary>
        /// Actualiza la entidad (GUARDA)
        /// </summary>
        /// <param name="editedEntity">Entidad editada</param>
        /// <param name="originalEntity">Entidad Original sin cambios</param>
        /// <param name="changed">Indica si se modifico la entidad</param>
        /// <returns></returns>
        public virtual TEntity Update(TEntity editedEntity, TEntity originalEntity, out bool changed)
        {
            try
            {
                _context.Entry(originalEntity).CurrentValues.SetValues(editedEntity);
                changed = _context.Entry(originalEntity).State == EntityState.Modified;
                _context.SaveChanges();
                return originalEntity;
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error al actualizar la entidad en la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar la entidad.", ex);
            }
        }
    
       

        /// <summary>
        /// Elimina una entidad (Guarda)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Delete(TEntity entity)
        {
            _dbSet.Remove(entity);

            _context.SaveChanges();

            return entity;
        }


        /// <summary>
        /// Guardar cambios
        /// </summary>
        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
