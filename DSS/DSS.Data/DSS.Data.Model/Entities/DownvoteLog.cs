using System;
using System.ComponentModel.DataAnnotations;

namespace DSS.Data.Model.Entities
{
    /// <summary>
    /// Database log entitiy storing information on user document downvotes.
    /// </summary>
    public class DownvoteLog : BaseEntitiy
    {
        #region Properties

        /// <summary>
        /// The Date when the User Downvoted the Document
        /// </summary>
        public DateTime DownvoteDate { get; set; }

        /// <summary>
        /// The comment/description given by the user as the reason for the downvote. Optional.
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// The user responsible for the downvote
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The document that was downvoted
        /// </summary>
        [Required]
        public Document Document { get; set; }

        #endregion
    }
}