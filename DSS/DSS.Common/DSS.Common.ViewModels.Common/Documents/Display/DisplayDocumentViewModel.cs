using System;
using System.Collections.Generic;

namespace DSS.Common.ViewModels.Documents
{
    /// <summary>
    /// Class used as a simple display for the documents in the system.
    /// </summary>
    public class DisplayDocumentViewModel
    {
        /// <summary>
        /// The document id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The title of the document
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The Document Description entered when the document was uploaded.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The name of the author on the initial document
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Indicator wether the document is indexed in lucene
        /// </summary>
        public bool IsIndexed { get; set; }

        /// <summary>
        /// The list of categories for the document
        /// </summary>
        public List<string> Categories { get; set; }

        /// <summary>
        /// The list of keywords for the document
        /// </summary>
        public List<string> Keywords { get; set; }
    }
}
 