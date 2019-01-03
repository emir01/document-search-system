using System;
using System.Security.Principal;
using DSS.Bootstrap.UserTracking.Identities;
using DSS.Bootstrap.UserTracking.Interface;

namespace DSS.Bootstrap.UserTracking.Principals
{
    /// <summary>
    /// The custom implementation of an Adapter Principal. Relies on a custom implementaion
    /// of a Domain User Adapter to get identitiy and role information.
    /// </summary>
    public class AuthenticatedUserModelPrincipal : IAdaptedPrinciple
    {
        #region Implementation of IAdaptedPrinciple

        public IDomainUserAdapter DomainUserAdapter { get; set; }

        #endregion

        #region Implementation of IPrincipal

        public bool IsInRole(string role)
        {
            CheckAdapter();

            return DomainUserAdapter.IsInRole(role);
        }

        public IIdentity Identity
        {
            get
            {
                CheckAdapter();

                var isAuthenticated = DomainUserAdapter.IsAuthenticated();
                var username = DomainUserAdapter.Username();

                return new TrackedUserIdentity(isAuthenticated, username);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check if the Domain User Adapter has been set. The Adapted Principal must have a valid Domain User Adapter
        /// </summary>
        private void CheckAdapter()
        {
            if (DomainUserAdapter == null)
            {
                throw new NullReferenceException("Domain User Adapter for Adapted Principal is null");
            }
        }

        #endregion
    }
}