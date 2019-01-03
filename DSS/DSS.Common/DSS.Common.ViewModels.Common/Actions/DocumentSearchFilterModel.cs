using System.Collections.Generic;

namespace DSS.Common.ViewModels.Actions
{
    /// <summary>
    /// The document serach view model contains all the criteria values
    /// used during the document search functionality
    /// </summary>
    public class DocumentSearchFilterModel
    {
        /// <summary>
        /// The number of documents to skip from the documents results after the applied criteria.
        /// Used for the paging/infinite scroll functionality.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// The number of documents to take from the returned documents after the applied criteria
        /// Used for the paging/infinite scroll functionality 
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// List of category criteria identifiers  to be used in the serach functionality
        /// </summary>
        public List<string> Categories { get; set; }
        
        /// <summary>
        /// List  of keywords identifiers to be used in the search functionality
        /// </summary>
        public List<string> Keywords { get; set; }

        /// <summary>
        /// The string criteria used to search and filter documents by the Lucene index score
        /// based on the documents inner contents
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// String criteria used to search documents based on the document title
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// String criteria used to serach documents based on the author
        /// </summary>
        public string Author { get; set; }
    }
}
