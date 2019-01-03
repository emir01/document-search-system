using System;

namespace DSS.Common.ViewModels.Documents
{
    /// <summary>
    /// The View Model used to populate the Grid in the Documents administartion page.
    /// </summary>
    public class AdminDocumentGridViewModel
    {
        /// <summary>
        /// The id of the document
        /// </summary>
        public Guid Id { get; set; }
       
        /// <summary>
        /// The title of the document.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The path where the document has been stored.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The name of the author of the document.
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// CVS List of adtional document collaborators.
        /// </summary>
        public string Collaboratrs { get; set; }

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
        /// A descriptive view model only value for the is indexed property
        /// </summary>
        public string IsIndexed_Descriptive { get; set; }

        /// <summary>
        /// The date the document has been added to the index.
        /// </summary>
        public DateTime? DateIndexed { get; set; }
    }
}
