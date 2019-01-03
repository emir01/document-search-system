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
    public class DocumentRepository : IRepository<Document>
    {
        #region Properties

        private DsContext _context;

        #endregion

        #region Constructor

        public DocumentRepository()
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
        public Document Create(Document entitiy)
        {
            // add the document to the context
            var storedDocument = _context.Documents.Add(entitiy);

            // save the changes and return the newly created entitiy
            _context.SaveChanges();

            // reset the context so we flush the object state manager
            // TODO: Bug because we might create new keywords/categories as we create documents
            // TODO : Look into different flow for document creation
            //_context = new DsContext();

            return storedDocument;
        }

        /// <summary>
        /// Ataches an entitiy to the context without calling the context save changes.
        /// </summary>
        /// <param name="entitiy">The entitiy that will be added to the context without saving changes</param>
        /// <returns>The atached created entitiy</returns>
        public Document CreateWithNoSave(Document entitiy)
        {
            // Add the document to the context with the added state without calling
            // save changes on the context
            var addedDocument = _context.Documents.Add(entitiy);

            _context.Entry(entitiy).State = EntityState.Added;

            return addedDocument;
        }

        /// <summary>
        /// The basic update function used to update already exsisting entities.
        /// </summary>
        /// <param name="entitiy">The entitiy that will be updated.</param>
        /// <returns>The updated entity.</returns>
        public Document Update(Document entitiy)
        {
            if (entitiy.Id == Guid.Empty)
            {
                // There is no id so this entity should be created
                return Create(entitiy);
            }

            // if it is detached from the context add it and set its state to modified
            if (_context.Entry(entitiy).State == EntityState.Detached)
            {
                _context.Documents.Attach(entitiy);
                _context.Entry(entitiy).State = EntityState.Modified;
            }

            // save the changes
            _context.SaveChanges();

            // return the entity
            return entitiy;
        }

        /// <summary>
        /// The basic function used to delete entities.
        /// </summary>
        /// <param name="entity">The entitiy to be deleted.</param>
        public void Delete(Document entity)
        {
            if (entity.Id == Guid.Empty)
            {
                throw new MissingIdForEntitiyException("Trying to delete an entity that does not have a set ID");
            }

            // Remove the entity from the Documents db collection
            // and save the changes
            _context.Documents.Remove(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// The basic function used to delete entities based on their unique id.
        /// </summary>
        /// <param name="id">The id of the entitiy we want to delete.</param>
        public void Delete(Guid id)
        {
            var document = _context.Documents.FirstOrDefault(x => x.Id == id);

            if (document == null)
            {
                throw new OperationOverNonExistingEntity("Trying to delete entity that does not exsist. You specified Id : " + id);
            }

            // remove the document and save the changes
            _context.Documents.Remove(document);
            _context.SaveChanges();
        }

        /// <summary>
        /// The basic function used to retrieve entities based on their unique id.
        /// </summary>
        /// <param name="id">The id of the entitiy we want to read.</param>
        /// <returns>The read entitiy with the given Id</returns>
        public Document Read(Guid id)
        {
            // Return a document based on the id
            var document = _context.Documents.FirstOrDefault(doc => doc.Id == id);
            return document;
        }

        /// <summary>
        /// The basic function that returns a list of all the entities.
        /// </summary>
        /// <returns>A collection of all the entities</returns>
        public IEnumerable<Document> ReadAll()
        {
            return _context.Documents;
        }

        /// <summary>
        /// Return a queryable collection of the entitiy wrapped by the repository
        /// </summary>
        /// <returns>IQueryable Collection of T</returns>
        public IQueryable<Document> ReadAllAsQueryable()
        {
            var queriedCategories = _context.Documents.AsQueryable()
                .Include(t => t.Keywords)
                .Include(t => t.Categories);
                //.Include(t => t.DocumentDownloads)
                //.Include(t => t.DocumentDownvotes)
                //.Include(t => t.DocumentUpvotes);

            return queriedCategories;
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
