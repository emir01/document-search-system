using System.Web.Mvc;
using System.Web.Security;
using DSS.Bootstrap.UserTracking.Interface;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.Enums;
using DSS.Common.ViewModels.User;

namespace DSS.Presentation.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        #region Properties

        private readonly IUserService _userService;

        private readonly IAuthenticator _authenticator;

        #endregion

        #region Constructor

        public AccountController(IUserService userService, IAuthenticator authenticator)
        {
            _userService = userService;
            _authenticator = authenticator;
        }

        #endregion

        #region Login Actions

        //
        // GET: /Account/Login

        /// <summary>
        /// Returns the main login view based on the login view model
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            var loginViewModel = new UserLoginViewModel();
            return View(loginViewModel);
        }

        //
        // POST /Account/Login

        /// <summary>
        /// The post login view for the login action
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(UserLoginViewModel loginViewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userAuthenticationResult = _userService.AuthenticateUser(loginViewModel.Username,
                                                                             loginViewModel.Password);

                if (userAuthenticationResult.Status == ResultStatus.Success)
                {
                    var authenticatedUser = userAuthenticationResult.GetData();

                    var whereToRedirect = _authenticator.AuthenticateUser(authenticatedUser.Username, loginViewModel.RememberMe, returnUrl);

                    // IF the Authenticator does not return a url something went wrong
                    if (whereToRedirect == "")
                    {
                        ModelState.AddModelError("", "Failed to authenticate. Please try again.");
                        return View(loginViewModel);    
                    }

                    return Redirect(whereToRedirect);
                }
                else
                {
                    // we are going to add an error to the model state 
                    ModelState.AddModelError("","Invalid username of password. Please try again");
                    return View(loginViewModel);
                }
            }
            else
            {
                return View(loginViewModel);    
            }
        }

        #endregion

        #region Log Out Actions

        /// <summary>
        /// Clear authentication and redirect the user to the common search view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            _authenticator.ClearCurrentUserAuthentication();

            return RedirectToAction("Index", "Search");
        }

        #endregion

        #region Rendering Actions

        public PartialViewResult LoggedUser()
        {
            // Get the username for the authenticated user
            var username = User.Identity.Name;
            ViewBag.Username = username;

            return PartialView();
        }

        #endregion
    }
}
