using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DSS.Common.ViewModels.Documents
{
    /// <summary>
    /// Simple view model for adding/uploading documents.
    /// </summary>
    public class AddDocumentViewModel
    {
        #region Properties
        
        /// <summary>
        /// The title of the document
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// The date the document was created.
        /// </summary>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// The name of the author.
        /// </summary>
        [Required]
        public string AuthorName { get; set; }

        /// <summary>
        /// The path of the saved file for the document
        /// </summary>
        public string Path { get; set; }
        
        /// <summary>
        /// The document key is used to identify which document is asosiated with the document 
        /// view model information
        /// </summary>
        [Required]
        public string DocumentKey { get; set; }
        
        /// <summary>
        /// Non-comma separated values for keywords
        /// </summary>
        public List<string> KeywordsList { get; set; }

        /// <summary>
        /// The list of selected category Ids posted for the uploaded document
        /// </summary>
        public List<Guid> CategoryList { get; set; }

        #endregion

        #region Constructor

        public AddDocumentViewModel()
        {
            KeywordsList = new List<string>();
            CategoryList = new List<Guid>();
        }

        #endregion
    }
}
