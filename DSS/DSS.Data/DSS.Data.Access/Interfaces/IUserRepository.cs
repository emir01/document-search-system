using System.Collections.Generic;
using DSS.Data.Model.Entities;

namespace DSS.Data.Access.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        /// <summary>
        /// Return a list of User objects for given user credentials. For propper user credentials business logc
        /// the list should contain only one user object
        /// </summary>
        /// <param name="username">The username credential</param>
        /// <param name="password">The password credential</param>
        /// <returns>List of users</returns>
        IList<User> GetUsersForCredentials(string username, string password);
    }
}
