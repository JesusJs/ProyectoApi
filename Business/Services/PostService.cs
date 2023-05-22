using DataAccess;
using DataAccess.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Business.ServicesInterfaces;
using DataAccess.Data;

namespace Business
{
    public class PostService<TEntity> : IPostService<TEntity> where TEntity : class, new()
    {
        protected IPostRepository<TEntity> _postRepository;
        protected ICustomerRepository<Customer> _customerRepository;
        public PostService(IPostRepository<TEntity> PostRepository, ICustomerRepository<Customer> CustomerRepository)
        {
            _postRepository = PostRepository;
            _customerRepository = CustomerRepository;
        }

        #region Repository


        /// <summary>
        /// Consulta todas las entidades
        /// </summary>
        public virtual IQueryable<TEntity> GetAll()
        {
            return _postRepository.GetAll;
        }

        /// <summary>
        /// Crea un Post (Guarda)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Create(object id, TEntity entity)
        {
            try
            {
                var postEntity = entity as Post;
                var usuario = _customerRepository.FindById(id);
                if (usuario != null)
                {
                    if (postEntity.Body.Length > 20)
                    {
                        int maxLength = 97; 
                        if (postEntity.Body.Length > maxLength)
                        {
                            postEntity.Body = postEntity.Body.Substring(0, maxLength) + "...";
                        }
                    }
                    if (postEntity.Type == 1)
                    {
                        postEntity.Category = "Farándula";
                    }
                    else if (postEntity.Type == 2)
                    {
                        postEntity.Category = "Política";
                    }
                    else if (postEntity.Type == 3)
                    {
                        postEntity.Category = "Futbol";
                    }
                    var result = postEntity as TEntity;
                    var createdPost = _postRepository.Create(result);
                
                    return createdPost;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al crear la entidad.", ex);
            }
        }

        /// <summary>
        /// Actualiza un Post (GUARDA)
        /// </summary>
        /// <param name="editedEntity">Entidad editada</param>
        /// <param name="originalEntity">Entidad Original sin cambios</param>
        /// <param name="changed">Indica si se modifico la entidad</param>
        /// <returns></returns>
        public virtual TEntity Update(object id, TEntity editedEntity, out bool changed)
        {
            try
            {
                TEntity originalEntity = _postRepository.FindById(id);
                if (originalEntity != null)
                {
                    _postRepository.Update(editedEntity, originalEntity, out changed);
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
            return _postRepository.Delete(entity);
        }

        /// <summary>
        /// Guardar cambios
        /// </summary>
        public virtual void SaveChanges()
        {
            _postRepository.SaveChanges();
        }
        #endregion


    }
}
