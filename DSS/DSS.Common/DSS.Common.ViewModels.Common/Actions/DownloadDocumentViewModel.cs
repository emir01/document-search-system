using DSS.Common.ViewModels.Documents;

namespace DSS.Common.ViewModels.Actions
{
    public class DownloadDocumentViewModel
    {
        #region Properties

        /// <summary>
        /// The document view model for the document we are retrieving.
        /// </summary>
        public DisplayDocumentViewModel Document { get; set; }

        /// <summary>
        /// Status indicator if the document retrieval was a success.
        /// </summary>
        public bool Retrieved { get; set; }

        /// <summary>
        /// The message related to the document download retrieval status.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The path of the document on the server which can be used in the document download request.
        /// </summary>
        public string Path { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Construct a document download view model/
        /// </summary>
        /// <param name="document">The view model used to display document information on the download page.</param>
        /// <param name="retrieved">The result flag indicating if a document was successfuly retrieved and we are ready for download</param>
        /// <param name="message">The message asosiated with the result flag.</param>
        /// <param name="path">The path for the given document on the server, to be used in the download process</param>
        public DownloadDocumentViewModel(DisplayDocumentViewModel document, bool retrieved, string message,
                                         string path = null)
        {
            Document = document;
            Retrieved = retrieved;
            Message = message;
            Path = path;
        }

        #endregion
    }
}
