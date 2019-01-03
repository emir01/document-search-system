using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS.Data.Model.Entities
{
    /// <summary>
    /// The base document entity used to store related document information.
    /// </summary>
    public class Document: BaseEntitiy
    {
        #region Basic Properties
        
        /// <summary>
        /// The title of the document.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The short description for the document
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The path where the document has been stored.
        /// </summary>
        public string Path { get; set;}

        /// <summary>
        /// The name of the author of the document.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// The date the document was uploaded to the system.
        /// </summary>
        public DateTime DateUploaded { get; set; }
        
        /// <summary>
        /// The date the document was created/presented.
        /// </summary>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Indicator if the document content has been indexed in the lucene index.
        /// </summary>
        public bool IsIndexed { get; set; }
        
        /// <summary>
        /// The date the document has been added to the index.
        /// </summary>
        public DateTime? DateIndexed { get;set; }

        /// <summary>
        /// The foreigh key for the user that uploaded the document
        /// </summary>
        public Guid UserId { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// The list of categories associated with the document.
        /// </summary>
        public virtual IList<Category> Categories { get; set; }

        /// <summary>
        /// The list of keywords asosiated with the document.
        /// </summary>
        public virtual IList<Keyword> Keywords { get; set; }

        /// <summary>
        /// The user that uploaded the document.
        /// </summary>
        [ForeignKey("UserId")]
        public virtual User User { get; set;}

        /// <summary>
        /// Collection of document download logs for each downloaded user for a specific date
        /// </summary>
        public virtual IList<DownloadLog> DocumentDownloads { get; set; }

        /// <summary>
        /// Collection of document upvotes from specific users on given dates
        /// </summary>
        public virtual IList<UpvoteLog> DocumentUpvotes { get; set; }

        /// <summary>
        /// Collection of document downvotes from specific users on given dates.
        /// </summary>
        public virtual IList<DownvoteLog> DocumentDownvotes { get; set; }

        #endregion
    }
}
