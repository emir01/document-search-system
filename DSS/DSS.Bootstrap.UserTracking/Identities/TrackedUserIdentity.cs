using System.Security.Principal;

namespace DSS.Bootstrap.UserTracking.Identities
{
    /// <summary>
    /// Basic custom implementation of the IIdentitiy interface, which is used in the custom user tracking
    /// and authorizaiton system
    /// </summary>
    public class TrackedUserIdentity:IIdentity
    {
        #region Properties

        /// <summary>
        /// The is authenticated flag for the identitys
        /// </summary>
        private readonly bool _isAuthenticated;

        /// <summary>
        /// The name flag for the identitiy
        /// </summary>
        private readonly string _name;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new Tracked User identity with the given basic properties
        /// </summary>
        /// <param name="isAuthenticated">Is the current tracked user identity authenticated by the domain systems</param>
        /// <param name="name">The unique name for the authenticated user </param>
        public TrackedUserIdentity(bool isAuthenticated, string name)
        {
            _isAuthenticated = isAuthenticated;
            _name = name;
        }

        #endregion
        
        #region Implementation of IIdentity

        /// <summary>
        /// The name property which has only a public get returning the _name field
        /// </summary>
        public string Name { get { return _name; } }

        /// <summary>
        /// Currently always using forms authentication
        /// </summary>
        public string AuthenticationType 
        {
            get { return "forms"; } 
        }

        /// <summary>
        /// The general indicator if the curent identitiy is for a
        /// authenticated user in the system or possibly only a guest.
        /// </summary>
        public bool IsAuthenticated { get { return _isAuthenticated; } }

        #endregion
    }
}