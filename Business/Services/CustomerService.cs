using DataAccess;
using DataAccess.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Business.ServicesInterfaces;
using PostEntity = DataAccess.Data.Post;
using DataAccess.Data;

namespace Business
{
    public class CustomerService<TEntity> : ICustomerService<TEntity> where TEntity : class, new()
    {
        protected ICustomerRepository<TEntity> _customerRepository;
        protected IPostRepository<PostEntity> _postRepository;
        public CustomerService(ICustomerRepository<TEntity> CustomerRepository, IPostRepository<PostEntity> PostRepository)
        {
            _customerRepository = CustomerRepository;
            _postRepository = PostRepository;
        }

        #region Repository


        /// <summary>
        /// Consulta todas las entidades
        /// </summary>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _customerRepository.GetAll;
        }

        /// <summary>
        /// Crea un entidad (Guarda)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Create(string Name,TEntity entity)
        {
            try
            {
                if (NameExists(Name))
                {
                    return null;
                }
                else
                {
                    return _customerRepository.Create(entity);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al crear la entidad.", ex);
            }
        }


        /// <summary>
        /// Consulta un usuario Existente.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool NameExists(string name)
        {
            var existingEntity = _customerRepository.FindByName(name);
            return existingEntity ;
        }


        /// <summary>
        /// Actualiza la entidad (GUARDA)
        /// </summary>
        /// <param name="editedEntity">Entidad editada</param>
        /// <param name="originalEntity">Entidad Original sin cambios</param>
        /// <param name="changed">Indica si se modifico la entidad</param>
        /// <returns></returns>
        public virtual TEntity Update(object id, TEntity editedEntity, out bool changed)
        {
            try
            {
                TEntity originalEntity = _customerRepository.FindById(id);
                if (originalEntity != null)
                {
                    _customerRepository.Update(editedEntity, originalEntity, out changed);
                    if (changed)
                    {
                        return originalEntity;
                    }
                }
                changed = false;
                return null;
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
            try
            {
                var Customer = entity as Customer;
                var postsToDelete = _postRepository.GetPostForUser(Customer.CustomerId).ToList();
                bool detele = true;
                foreach (var post in postsToDelete)
                {
                    var result = _postRepository.Delete(post);
                    if (result == null)
                    {
                        detele = false;
                        break;
                    }
                }
                if (detele)
                {
                    TEntity originalEntity = _customerRepository.Delete(entity);
                    return originalEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al actualizar la entidad.", ex);
            }
        }

        /// <summary>
        /// Guardar cambios
        /// </summary>
        public virtual void SaveChanges()
        {
            _customerRepository.SaveChanges();
        }
        #endregion


    }
}
