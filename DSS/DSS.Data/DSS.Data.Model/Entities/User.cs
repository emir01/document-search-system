using System.Collections.Generic;

namespace DSS.Data.Model.Entities
{
    /// <summary>
    /// The basic entity class used to users in the system.
    /// </summary>
    public class User : BaseEntitiy
    {

        #region Constructor

        /// <summary>
        /// Constructs a Base Entitiy
        /// </summary>
        public User()
        {
            UserRoles = new List<Role>();
        }

        #endregion

        /// <summary>
        /// The username used to log in the system.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password used to get authorized in the system.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        #region Navigation Properties

        /// <summary>
        /// The collection of user roles used to authorize users in the sytem.
        /// </summary>
        public virtual List<Role> UserRoles { get; set; }

        /// <summary>
        /// The currenty set 
        /// </summary>
        public virtual UserFeatureTier UserFeatureTier { get; set; } 
       
        /// <summary>
        /// The collection of uploaded documents for the user
        /// </summary>
        public virtual IList<Document> UploadedDocuments { get; set; }

        /// <summary>
        /// The list of downloaded documents for the user
        /// </summary>
        public virtual IList<DownloadLog> DownloadedDocuments { get; set; }

        /// <summary>
        /// The list of upvoted documents for the user
        /// </summary>
        public virtual IList<UpvoteLog> UpvotedDocuments { get; set; }

        /// <summary>
        /// The list of downvoted documents for the user/
        /// </summary>
        public virtual IList<DownvoteLog> DownvotedDocuments { get; set; } 

        #endregion
    }
}
