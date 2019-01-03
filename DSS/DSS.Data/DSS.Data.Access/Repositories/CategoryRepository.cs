using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using DSS.Common.Exceptions.DB;
using DSS.Data.Access.Interfaces;
using DSS.Data.Model.Context;
using DSS.Data.Model.Entities;
using StructureMap;

namespace DSS.Data.Access.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        #region Properties

        private readonly DsContext _context;

        #endregion

        #region Constructor

        public CategoryRepository()
        {
            _context = (DsContext)ObjectFactory.GetInstance<DbContext>();
        }

        #endregion

        #region Interface Implementation

        /// <summary>
        /// The basic function used to create new entities.
        /// </summary>
        /// <param name="entitiy">The entity we will add to the database.</param>
        /// <returns>The create entitiy.</returns>
        public Category Create(Category entitiy)
        {
            // add the category to the context
            var storedCategory = _context.Categories.Add(entitiy);

            // save the changes and return the newly created entitiy
            _context.SaveChanges();
            return storedCategory;
        }

        /// <summary>
        /// Ataches an entitiy to the context without calling the context save changes.
        /// </summary>
        /// <param name="entitiy">The entitiy that will be added to the context without saving changes</param>
        /// <returns>The atached created entitiy</returns>
        public Category CreateWithNoSave(Category entitiy)
        {
            // we are going to atach with the added/new state without calling save changes
            // add an entry for the entitiy setting the state to added
            var addedEntitiy = _context.Categories.Add(entitiy);
            _context.Entry(entitiy).State = EntityState.Added;

            return addedEntitiy;
        }

        /// <summary>
        /// The basic update function used to update already exsisting entities.
        /// </summary>
        /// <param name="entitiy">The entitiy that will be updated.</param>
        /// <returns>The updated entity.</returns>
        public Category Update(Category entitiy)
        {
            // If the entitiy does not have a guid set 
            // it must be new so we are going to create it.
            if (entitiy.Id == Guid.Empty)
            {
                return Create(entitiy);
            }

            // If the entitiy is detached atach it to the context as a modified
            // entitiy.
            if (_context.Entry(entitiy).State == EntityState.Detached)
            {
                _context.Categories.Attach(entitiy);
                _context.Entry(entitiy).State = EntityState.Modified;
            }

            // save the changes and return the modified entitiy
            _context.SaveChanges();

            return entitiy;
        }

        /// <summary>
        /// The basic function used to delete entities.
        /// </summary>
        /// <param name="entity">The entitiy to be deleted.</param>
        public void Delete(Category entity)
        {
            // to delete the entitiy its id must be set
            if (entity.Id == Guid.Empty)
            {
                throw new MissingIdForEntitiyException("Trying to delete a category that has no set id.");
            }

            // remove the entity and save the changes
            _context.Categories.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// The basic function used to delete entities based on their unique id.
        /// </summary>
        /// <param name="id">The id of the entitiy we want to delete.</param>
        public void Delete(Guid id)
        {
            // try and retrieve an entitiy with the given id
            var entity = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (entity == null)
            {
                throw new OperationOverNonExistingEntity("Trying to delete entitiy that does not exsist. Specified id " + id.ToString());
            }

            // remove the entitiy and save the changes
            _context.Categories.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// The basic function used to retrieve entities based on their unique id.
        /// </summary>
        /// <param name="id">The id of the entitiy we want to read.</param>
        /// <returns>The read entitiy with the given Id</returns>
        public Category Read(Guid id)
        {
            //return a entity category with the given id
            var entity = _context.Categories.FirstOrDefault(key => key.Id == id);
            return entity;
        }

        /// <summary>
        /// The basic function that returns a list of all the entities.
        /// </summary>
        /// <returns>A collection of all the entities</returns>
        public IEnumerable<Category> ReadAll()
        {
            return _context.Categories.AsEnumerable();
        }

        public IQueryable<Category> ReadAllAsQueryable()
        {
            return _context.Categories;
        }

        /// <summary>
        /// Save context changes from the given repository.
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        #endregion
    }
}