using System;
using System.Web.Security;
using DSS.Bootstrap.UserTracking.Interface;

namespace DSS.Bootstrap.UserTracking.Services
{
    /// <summary>
    /// Basic Web Forms authenticator used for authenticating users with Forms Authentication
    /// </summary>
    public class WebFormsAuthenticator : IAuthenticator
    {
        #region Implementation of IAuthenticator

        public string AuthenticateUser(string username, bool persistentCookie, string returnUrl)
        {
            try
            {
                FormsAuthentication.SetAuthCookie(username, persistentCookie);

                if (string.IsNullOrWhiteSpace(returnUrl))
                {
                    return FormsAuthentication.DefaultUrl;
                }
                else
                {
                    return returnUrl;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public bool ClearCurrentUserAuthentication()
        {
            try
            {
                FormsAuthentication.SignOut();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}