using System.Collections.Generic;
using System.Web;
using DSS.Bootstrap.Utilities.Dtos;
using DSS.Common.ViewModels.Documents;

namespace DSS.Bootstrap.Utilities.Interface
{
    /// <summary>
    ///  Describes the basic functionality of a web file utility
    ///  to be used in the documents upload and download process.
    /// </summary>
    public interface IServerFileUtility
    {
        /// <summary>
        ///  Used to process the files sent from the client to the server, finding matching document view model objects based on the key.    
        /// </summary>
        /// <param name="files">The posted files array.</param>
        /// <param name="documents">The document add view models posted from the client</param>
        /// <returns><see cref="FileUploadProcessingResults"/> object containing all the results.</returns>
        FileUploadProcessingResults ProcessFiles(IEnumerable<HttpPostedFileBase> files, IEnumerable<AddDocumentViewModel> documents);

        /// <summary>
        /// Delete the document files  with the given paths from the server
        /// </summary>
        /// <param name="documentPaths">The paths of the documents we will remove from the server</param>
        /// <returns>A boolean value indicating the success of the operation.</returns>
        bool RemoveDocuments(List<string> documentPaths);

        /// <summary>
        /// Based on the file extension and the allowed content types return a content type
        /// string to be used in the file download process.
        /// </summary>
        /// <param name="fileExtension">The extension of the uploaded file we want to dowload</param>
        /// <returns>Content type string to be used in the file download process</returns>
        string GetContentType(string fileExtension);

        /// <summary>
        /// Check if a file exsists for the given file path.
        /// </summary>
        /// <param name="filePath">The server path for the file</param>
        /// <returns>Boolean flag indicating if the file exists for the given path</returns>
        bool FileExsists(string filePath);
    }
}
