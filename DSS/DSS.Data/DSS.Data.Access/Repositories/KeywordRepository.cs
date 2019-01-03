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
    /// <summary>
    /// The interface implementation for the Keyword repository
    /// </summary>
    public class KeywordRepository : IRepository<Keyword>
    {
        #region Properties

        private readonly DsContext _context;

        #endregion

        #region Constructor

        public KeywordRepository()
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
        public Keyword Create(Keyword entitiy)
        {
            // add the keyword to the context
            var storedKeyword = _context.Keywords.Add(entitiy);

            // save the changes and return the newly created entitiy
            _context.SaveChanges();

            return storedKeyword;
        }

        /// <summary>
        /// Ataches an entitiy to the context without calling the context save changes.
        /// </summary>
        /// <param name="entitiy">The entitiy that will be added to the context without saving changes</param>
        /// <returns>The atached created entitiy</returns>
        public Keyword CreateWithNoSave(Keyword entitiy)
        {
            // only add the keyword to the contextr with the added state
            var addedKeyword = _context.Keywords.Add(entitiy);
            _context.Entry(entitiy).State = EntityState.Added;

            return addedKeyword;
        }

        /// <summary>
        /// The basic update function used to update already exsisting entities.
        /// </summary>
        /// <param name="entitiy">The entitiy that will be updated.</param>
        /// <returns>The updated entity.</returns>
        public Keyword Update(Keyword entitiy)
        {
            // If the entitiy does not have a guid set 
            // it must be new so we are going to create it.
            if(entitiy.Id == Guid.Empty)
            {
                return Create(entitiy);
            }

            // If the entitiy is detached atach it to the context as a modified
            // entitiy.
            if(_context.Entry(entitiy).State == EntityState.Detached)
            {
                _context.Keywords.Attach(entitiy);
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
        public void Delete(Keyword entity)
        {
            // to delete the entitiy its id must be set
            if(entity.Id == Guid.Empty)
            {
                throw new MissingIdForEntitiyException("Trying to delete a keyword that has no set id.");
            }

            // remove the entity and save the changes
            _context.Keywords.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// The basic function used to delete entities based on their unique id.
        /// </summary>
        /// <param name="id">The id of the entitiy we want to delete.</param>
        public void Delete(Guid id)
        {
            // try and retrieve an entitiy with the given id
            var keyword = _context.Keywords.FirstOrDefault(x => x.Id == id);

            if(keyword == null)
            {
                throw  new OperationOverNonExistingEntity("Trying to delete entitiy that does not exsist. Specified id "+id.ToString());
            }
            
            // remove the keyword and save the changes
            _context.Keywords.Remove(keyword);
            _context.SaveChanges();
        }

        /// <summary>
        /// The basic function used to retrieve entities based on their unique id.
        /// </summary>
        /// <param name="id">The id of the entitiy we want to read.</param>
        /// <returns>The read entitiy with the given Id</returns>
        public Keyword Read(Guid id)
        {
            //return a keyword with the given id
            var keyword = _context.Keywords.FirstOrDefault(key => key.Id == id);
            return keyword;
        }

        /// <summary>
        /// The basic function that returns a list of all the entities.
        /// </summary>
        /// <returns>A collection of all the entities</returns>
        public IEnumerable<Keyword> ReadAll()
        {
            return _context.Keywords.AsEnumerable();
        }

        public IQueryable<Keyword> ReadAllAsQueryable()
        {
            return _context.Keywords;
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