using System;
using System.Linq;
using DSS.Bootstrap.UserTracking.Interface;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.Enums;
using DSS.Data.Model.Entities;
using StructureMap;

namespace DSS.Bootstrap.UserTracking.Services
{
    /// <summary>
    /// An implementation of the Domain User Adapter for the DSS Domain Users
    /// </summary>
    public class DssTrackedUserAdapter : IDomainUserAdapter
    {
        #region Properties

        private User _domainUser;

        private bool _domainUserRead;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a DSS tracked User adapter
        /// </summary>
        public DssTrackedUserAdapter()
        {
            _domainUserRead = false;
        }

        #endregion

        #region Implementation of IDomainUserAdapter

        /// <summary>
        /// Check if the tracked user has a given role, with how the domain is handled
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool IsInRole(string roleName)
        {
            if (!_domainUserRead)
            {
                throw new ApplicationException("Must get adapted domain user before calling adapter methods");
            }

            if (_domainUser == null)
            {
                return false;
            }

            return
                _domainUser.UserRoles.Any(
                    x => string.Equals(roleName, x.Alias, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        ///  The user is trully authenticated if the domain user is set
        /// </summary>
        /// <returns></returns>
        public bool IsAuthenticated()
        {
            if (!_domainUserRead)
            {
                throw new ApplicationException("Must get adapted domain user before calling adapter methods");
            }

            return _domainUser != null;
        }

        public string Username()
        {
            if (!_domainUserRead)
            {
                throw new ApplicationException("Must get adapted domain user before calling adapter methods");
            }

            if (_domainUser != null)
            {
                return _domainUser.Username;
            }
            else
            {
                return "Guest";
            }
        }

        public void GetAdaptedDomainUser(string name)
        {
            // Domain user is read independent of the name parameter
            _domainUserRead = true;

            // if the name is empty or null, set state as not authenticated
            if (string.IsNullOrWhiteSpace(name))
            {
                _domainUser = null;
                return;
            }

            var userDomainService = ObjectFactory.GetInstance<IUserService>();
            var userResult = userDomainService.GetUserForUsername(name);

            if (userResult.Status == ResultStatus.Success)
            {
                _domainUser = userResult.GetData();
            }
            else
            {
                throw new ApplicationException("Could not load domain user during authorization");
            }
        }

        #endregion
    }
}