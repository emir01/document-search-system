using System.ComponentModel.DataAnnotations;

namespace DSS.Common.ViewModels.User
{
    /// <summary>
    /// View model that is used on the user login page to move around the user login information
    /// </summary>
    public class UserLoginViewModel
    {
        /// <summary>
        /// The users account username
        /// </summary>
        [Required(AllowEmptyStrings = false,ErrorMessage = "Username is required")]
        public string Username { get; set; }

        /// <summary>
        /// The users password
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        public string Password { get; set; }

        /// <summary>
        /// Flag regarding the persistent cookie information
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
