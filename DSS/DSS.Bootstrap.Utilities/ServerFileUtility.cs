using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using DSS.Bootstrap.Utilities.Dtos;
using DSS.Bootstrap.Utilities.Interface;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.Enums;
using DSS.Common.ViewModels.Documents;

namespace DSS.Bootstrap.Utilities
{
    /// <summary>
    ///  Describes the basic functionality of a web file utility
    ///  to be used in the documents upload and download process.
    /// </summary>
    public class ServerFileUtility : IServerFileUtility
    {
        #region Properties

        private readonly IDocumentsService _documentsService;

        #endregion

        #region Constructor

        public ServerFileUtility(IDocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        #endregion

        #region Inetface Implementation

        /// <summary>
        ///  Used to process the files sent from the client to the server, finding matching document view model objects based on the key.    
        /// </summary>
        /// <param name="files">The posted files array.</param>
        /// <param name="documents">The document add view models posted from the client</param>
        /// <returns><see cref="FileUploadProcessingResults"/> object containing all the results.</returns>
        public FileUploadProcessingResults ProcessFiles(IEnumerable<HttpPostedFileBase> files, IEnumerable<AddDocumentViewModel> documents)
        {
            var results = new FileUploadProcessingResults();

            // process the posted document view models
            foreach (var addDocumentViewModel in documents)
            {
                if(addDocumentViewModel.DocumentKey.Contains("fakepath"))
                {
                    addDocumentViewModel.DocumentKey = addDocumentViewModel.DocumentKey.Replace("C:\\fakepath\\", "");
                }

                // process the file
                var postedFile = GetFileForKeyword(addDocumentViewModel.DocumentKey, files);

                // if a uploaded file can not be found continue with te next documet view model.
                if (postedFile == null)
                {
                    // There is no file for the view model
                    results.OrphanViewModels.Add(addDocumentViewModel);
                    continue;
                }

                // Try and save the file.
                var savedFilePath = SaveFileOnServer(postedFile);

                // Something went wrong if the saved path is empty
                if (string.IsNullOrWhiteSpace(savedFilePath))
                {
                    results.OrphanUploadedFiles.Add(postedFile);
                    results.OrphanViewModels.Add(addDocumentViewModel);
                }
                else
                {
                    // set the file path to the view model
                    addDocumentViewModel.Path = savedFilePath;
                    results.SavedFiles.Add(addDocumentViewModel);
                }
            }

            // return the result containing the processed stuff.
            return results;
        }

        /// <summary>
        /// Delete the document files  with the given paths from the server
        /// </summary>
        /// <param name="documentPaths">The paths of the documents we will remove from the server</param>
        /// <returns>A boolean value indicating the success of the operation.</returns>
        public bool RemoveDocuments(List<string> documentPaths)
        {
            try
            {
                // Go through all the files and delete them
                // from the server
                foreach (var documentPath in documentPaths)
                {
                    var file = new FileInfo(documentPath);

                    if(file.Exists)
                    {
                        file.Delete();    
                    }
                    else
                    {
                        // the file does not exsist so continue to the next file path
                        continue;
                    }
                }

                // if it all goes well return true
                return true;
            }
            catch(Exception)
            {
                // if an exception happens
                // then return false
                return false;    
            }
        }

        /// <summary>
        /// Based on the file extension and the allowed content types return a content type
        /// string to be used in the file download process.
        /// </summary>
        /// <param name="fileExtension">The extension of the uploaded file we want to dowload</param>
        /// <returns>Content type string to be used in the file download process</returns>
        public string GetContentType(string fileExtension)
        {
            var fex = fileExtension.ToLower();
            switch (fex)
            {
                case "pdf":
                    return "application/pdf";

                case "docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

                case "pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";

                case "xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                case "doc":
                    return "application/msword";

                case "xls":
                    return "application/msexcel";

                case "ppt":
                    return "application/mspowerpoint";

                default:
                    return "application/octet-stream";
            }
        }

        /// <summary>
        /// Check if a file exsists for the given file path.
        /// </summary>
        /// <param name="filePath">The server path for the file</param>
        /// <returns>Boolean flag indicating if the file exists for the given path</returns>
        public bool FileExsists(string filePath)
        {
            try
            {
                return File.Exists(filePath);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Saves the file on the server only if there is a matching posted document view model information
        /// </summary>
        /// <param name="file">The file that will be posted on the </param>
        /// <returns>The path of the file</returns>
        private string SaveFileOnServer(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                // get the intended path for the document
                var fileName = file.FileName;
                var filePath = Path.Combine(HttpContext.Current.Server.MapPath("Upload/"), fileName);

                // Check if a document with the given filepath already exsists based on
                // the file path.
                var documentInSystemResult = _documentsService.IsDocumentInSystem(filePath);

                if (documentInSystemResult.Status != ResultStatus.Success)
                {
                    return "";
                }
                else
                {
                    // The file is already in the system
                    if (documentInSystemResult.GetData() == true)
                    {
                        return "";
                    }
                    else
                    {
                        file.SaveAs(filePath);
                        return filePath;
                    }
                }
            }
            else
            {
                // if the file content is empty return an empty string
                // file has not been saved.
                return "";
            }
        }

        /// <summary>
        /// Return a http posted file based on the keyword.
        /// </summary>
        /// <param name="keyword">The keyword that will be used to find a file in the collection of http files</param>
        /// <param name="files">The list of files posted on the server</param>
        /// <returns>The posted file maching the AddDocmentViewModel with the given keyword </returns>
        private HttpPostedFileBase GetFileForKeyword(string keyword, IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var httpPostedFile in files)
            {
                var fileName = Path.GetFileName(httpPostedFile.FileName);

                if (fileName == keyword)
                {
                    return httpPostedFile;
                }
            }

            return null;
        }

        #endregion
    }
}