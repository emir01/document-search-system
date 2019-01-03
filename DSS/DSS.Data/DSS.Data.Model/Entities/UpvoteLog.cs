using System;
using System.ComponentModel.DataAnnotations;

namespace DSS.Data.Model.Entities
{
    /// <summary>
    /// Database entitiy containing information on user document upvotes.
    /// Bassically used to store when and which documents has the user upvoted.
    /// </summary>
    public class UpvoteLog : BaseEntitiy
    {
        #region Properties

        /// <summary>
        /// The Date when the upvote occured
        /// </summary>
        public DateTime UpvoteDate { get; set; }

        /// <summary>
        /// The reason the user upvoted the document.
        /// </summary>
        public string UpvoteDesription { get; set; }

        #endregion

        #region Navigation Variables

        /// <summary>
        /// The User that upvoted the given document
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The Document that was upvoted by the specific User
        /// </summary>
        [Required]
        public Document Document { get; set; }

        #endregion
    }
}