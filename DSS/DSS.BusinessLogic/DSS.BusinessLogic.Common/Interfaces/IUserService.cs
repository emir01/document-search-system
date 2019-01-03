using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Data.Model.Entities;

namespace DSS.BusinessLogic.Common.Interfaces
{
    /// <summary>
    /// Describe user related services including the authentication service
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticate user credential information, the username and password, and return an authenticated user object
        /// </summary>
        /// <param name="username">The Users, username property used to distuingshi</param>
        /// <param name="password">The Users, password property used to authenticate users in the system</param>
        /// <returns></returns>
        DataResult<User> AuthenticateUser(string username, string password);


        /// <summary>
        /// Return a DSS user for the given username.
        /// </summary>
        /// <param name="username">The username of the DSS user</param>
        /// <returns></returns>
        DataResult<User> GetUserForUsername(string username);
    }
}
