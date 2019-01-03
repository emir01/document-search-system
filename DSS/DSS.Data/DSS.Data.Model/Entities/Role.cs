using System.Collections.Generic;

namespace DSS.Data.Model.Entities
{
    /// <summary>
    /// Role entitiy information, which is inheritly used to control application access for Users
    /// </summary>
    public class Role : BaseEntitiy
    {
        /// <summary>
        /// The title for the Role, used in displaying Role information on the UI
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Short description regarding the role and the authorized functionality
        /// for users with the given role
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Role alias value which is used to uniquely identify roles during
        /// design time.
        /// </summary>
        public string Alias { get; set; }

        #region Navigation Properties

        /// <summary>
        /// The Users that have been asigned the given Role
        /// </summary>
        public virtual IList<User> Users { get; set; }

        #endregion
    }
}
