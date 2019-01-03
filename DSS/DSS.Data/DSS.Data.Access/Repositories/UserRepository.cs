using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using DSS.Data.Access.Interfaces;
using DSS.Data.Model.Context;
using DSS.Data.Model.Entities;
using StructureMap;

namespace DSS.Data.Access.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Properties

        private readonly DsContext _dsContext;

        #endregion

        #region Constructor

        public UserRepository()
        {
            _dsContext = (DsContext)ObjectFactory.GetInstance<DbContext>();
        }

        #endregion

        #region Implementation of IRepository<User>

        public User Create(User entitiy)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ataches an entitiy to the context without calling the context save changes.
        /// </summary>
        /// <param name="entitiy">The entitiy that will be added to the context without saving changes</param>
        /// <returns>The atached created entitiy</returns>
        public User CreateWithNoSave(User entitiy)
        {
            // add the user to the context with the add state without calling save changes
            var addedUser = _dsContext.Users.Add(entitiy);
            _dsContext.Entry(entitiy).State = EntityState.Added;

            return addedUser;
        }

        public User Update(User entitiy)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public User Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> ReadAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> ReadAllAsQueryable()
        {
            return _dsContext.Users.Include(t=>t.UserRoles).AsQueryable();
        }

        /// <summary>
        /// Save context changes from the given repository.
        /// </summary>
        public void SaveChanges()
        {
            _dsContext.SaveChanges();
        }

        #endregion

        #region Implementation of IUserRepository

        public IList<User> GetUsersForCredentials(string username, string password)
        {
            // run a query returning a list of users for the given credentials
            var users =
                _dsContext.Users.Include(t => t.UserRoles)
                    .Where(user => user.Username == username && user.Password == password);
         
            return users.ToList();
        }

        #endregion
    }
}
