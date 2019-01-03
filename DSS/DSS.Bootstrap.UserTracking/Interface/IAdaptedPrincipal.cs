using System.Security.Principal;

namespace DSS.Bootstrap.UserTracking.Interface
{
    /// <summary>
    /// A custom IPrincipal Implementation that is described to 
    /// user a IDomainUserAdapter to pull and manage user information
    /// </summary>
    public interface IAdaptedPrinciple:IPrincipal
    {
        /// <summary>
        /// The Principal we will be using will contain 
        /// a domain user adapter object
        /// </summary>
        IDomainUserAdapter DomainUserAdapter { get; set; }
    }
}