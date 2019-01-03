namespace DSS.Bootstrap.UserTracking.Interface
{
    /// <summary>
    /// Describes an adapter interface that can be used by the AuthenticatedUserModelPrincipal object
    /// to talk to user domain objects without actually knowing the domain object type
    /// </summary>
    public interface IDomainUserAdapter
    {
        /// <summary>
        /// Check if a adapter domain user in a specific role
        /// </summary>
        /// <param name="roleName">The name/alias of the role</param>
        /// <returns></returns>
        bool IsInRole(string roleName);

        /// <summary>
        /// The adapter can also provide implementations for some of the principal functions
        /// </summary>
        /// <returns></returns>
        bool IsAuthenticated();

        /// <summary>
        /// Return the username of the logged user
        /// </summary>
        /// <returns></returns>
        string Username();

        /// <summary>
        /// Gets the Domain user which will be adapted to work under principal and identity relationships
        /// </summary>
        /// <param name="name">The identifier for the domain user</param>
        void GetAdaptedDomainUser(string name);
    }
}