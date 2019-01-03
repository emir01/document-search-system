using System;
using System.Linq;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Data.Access.Interfaces;
using DSS.Data.Model.Entities;

namespace DSS.BusinessLogic.Common.Services
{
    public class UserService : IUserService
    {
        #region Properties

        private readonly IUserRepository _userRepository;

        #endregion

        #region Constructor

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        #region Inteface Implementation

        /// <summary>
        /// Authenticate user credential information, the username and password, and return an authenticated user object
        /// </summary>
        /// <param name="username">The Users, username property used to distuingshi</param>
        /// <param name="password">The Users, password property used to authenticate users in the system</param>
        /// <returns></returns>
        public DataResult<User> AuthenticateUser(string username, string password)
        {
            var result = new DataResult<User>();

            try
            {
                var usersForCredentials = _userRepository.GetUsersForCredentials(username, password);

                if (usersForCredentials.Count == 0)
                {
                    result.SetFailiure("User Authentication failed. No user found.");
                }
                else if (usersForCredentials.Count > 1)
                {
                    result.SetFailiure("User Authentication failed. More than one user found for given credentials");
                }
                else
                {
                    var user = usersForCredentials.FirstOrDefault();

                    if (user == null)
                    {
                        result.SetFailiure("First user result is null");
                    }
                    else
                    {
                        result.SetSuccess("Retrieved user information");
                        result.SetData(user);
                    }
                }
            }
            catch (Exception ex)
            {
                result.SetException(ex, "Exception while authenticating user");
            }

            return result;
        }

        /// <summary>
        /// Return a DSS user for the given username.
        /// </summary>
        /// <param name="username">The username of the DSS user</param>
        /// <returns></returns>
        public DataResult<User> GetUserForUsername(string username)
        {
            var result = new DataResult<User>();
            try
            {
                var userData = _userRepository.ReadAllAsQueryable().FirstOrDefault(x => x.Username == username);

                result.SetData(userData);
                result.SetSuccess("Retrieved userdata for the given username");
            }
            catch (Exception ex)
            {
                result.SetException(ex,"Exception while trying to get DSS User for username");
            }

            return result;
        }

        #endregion
    }
}