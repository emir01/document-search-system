namespace DSS.Bootstrap.UserTracking.Interface
{
    /// <summary>
    /// An interface describing bascis authentication related service utilities.
    /// </summary>
    public interface IAuthenticator
    {
        /// <summary>
        /// Authenticate the user for the given username and for the given persist login
        /// </summary>
        /// <param name="username">The username of the user that was just authorized</param>
        /// <param name="persistLogin">Should the login be persisted</param>
        /// <param name="returnUrl">Is there a return address where we shopuld redirect the user</param>
        /// <returns></returns>
        string AuthenticateUser(string username, bool persistLogin, string returnUrl);

        /// <summary>
        /// Clear the authentication for the current logged in user, if there is a user.
        /// </summary>
        /// <returns></returns>
        bool ClearCurrentUserAuthentication();
    }
}
