using System.Collections.Generic;

namespace DSS.Common.ViewModels.Documents
{

    /// <summary>
    /// View model used to display status results for the file upload process
    /// </summary>
    public class DocumentsUploadResultsViewModel
    {
        #region Properties

        /// <summary>
        ///  The number of stored documents from the last file upload process
        /// </summary>
        public int StoredDocumentsCount { get; set; }

        /// <summary>
        /// The list of names of the documents that were actually saved in the system.
        /// </summary>
        public List<StoredDocument> StoredDocuments { get; set; }

        /// <summary>
        /// The list of names of uploaded actual files for which view model/document information is missing
        /// </summary>
        public List<string> OrphanedUploadedFileNames { get; set; }

        /// <summary>
        /// List of titles for document view model objects that do not have assosiated uploaded physical files.
        /// </summary>
        public List<string> OrphanedDocumentObjectTitles { get; set; } 

        #endregion

        #region Constructor

        /// <summary>
        /// Create the File Upload results view model with the default values for the result properites.
        /// </summary>
        public DocumentsUploadResultsViewModel()
        {
            StoredDocuments = new List<StoredDocument>();
            OrphanedUploadedFileNames = new List<string>();
            OrphanedDocumentObjectTitles = new List<string>();
        }

        #endregion

        #region Inner document class

        public sealed class StoredDocument
        {
            /// <summary>
            /// The title of the saved document in the system.
            /// </summary>
            public string DocumentSavedTitle { get; set; }
            
            /// <summary>
            /// The path of the uploaded document asosiated with the saved document
            /// </summary>
            public string DocumentFilePath { get; set; }
        }

        #endregion
    }
}
