using System;
using System.Collections.Generic;
using System.Linq;

namespace DSS.Data.Access.Interfaces
{
    /// <summary>
    /// An interface describing the basic functionality for an entity interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// The basic function used to create new entities.
        /// </summary>
        /// <param name="entitiy">The entity we will add to the database.</param>
        /// <returns>The create entitiy.</returns>
        T Create(T entitiy);

        /// <summary>
        /// Ataches an entitiy to the context without calling the context save changes.
        /// </summary>
        /// <param name="entitiy">The entitiy that will be added to the context without saving changes</param>
        /// <returns>The atached created entitiy</returns>
        T CreateWithNoSave(T entitiy);

        /// <summary>
        /// The basic update function used to update already exsisting entities.
        /// </summary>
        /// <param name="entitiy">The entitiy that will be updated.</param>
        /// <returns>The updated entity.</returns>
        T Update(T entitiy);

        /// <summary>
        /// The basic function used to delete entities.
        /// </summary>
        /// <param name="entity">The entitiy to be deleted.</param>
        void Delete(T entity);
        
        /// <summary>
        /// The basic function used to delete entities based on their unique id.
        /// </summary>
        /// <param name="id">The id of the entitiy we want to delete.</param>
        void Delete(Guid id);

        /// <summary>
        /// The basic function used to retrieve entities based on their unique id.
        /// </summary>
        /// <param name="id">The id of the entitiy we want to read.</param>
        /// <returns>The read entitiy with the given Id</returns>
        T Read(Guid id);

        /// <summary>
        /// The basic function that returns a list of all the entities.
        /// </summary>
        /// <returns>A collection of all the entities</returns>
        IEnumerable<T> ReadAll();

        /// <summary>
        /// Return a queryable collection of the entitiy wrapped by the repository
        /// </summary>
        /// <returns>IQueryable Collection of T</returns>
        IQueryable<T> ReadAllAsQueryable();
        
        /// <summary>
        /// Save context changes from the given repository.
        /// </summary>
        void SaveChanges();
    }
}
