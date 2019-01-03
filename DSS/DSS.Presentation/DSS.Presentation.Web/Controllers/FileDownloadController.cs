using System;
using System.IO;
using System.Web.Mvc;
using DSS.Bootstrap.Utilities.Attributes;
using DSS.Bootstrap.Utilities.Interface;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.Enums;

namespace DSS.Presentation.Web.Controllers
{
    /// <summary>
    /// The main file download controller that will handle the file downloading process
    /// </summary>
    public class FileDownloadController : Controller
    {
        #region Properties
        /// <summary>
        /// The document service is used to interface with the DSS underling document storage and functionality
        /// </summary>
        private readonly IDocumentsService _documentsService;

        /// <summary>
        /// Server file utility for managing files upload and download from the web server
        /// </summary>
        private readonly IServerFileUtility _serverFileUtility;

        #endregion

        #region Constructor

        public FileDownloadController(IServerFileUtility serverFileUtility, IDocumentsService documentsService)
        {
            _serverFileUtility = serverFileUtility;
            _documentsService = documentsService;
        }

        #endregion

        #region Actions

        //
        // Get FileDownload/DownloadFile/{id}
        [FileDownload]
        public ActionResult DownloadFile(Guid id)
        {
            //try and get the document based on the id
            var documentRequest = _documentsService.GetDocumentById(id);

            if (documentRequest.Status == ResultStatus.Success)
            {
                var doc = documentRequest.GetData();
                var docPath = doc.Path;

                // check if doc path is provided and not null
                if (string.IsNullOrWhiteSpace(docPath))
                {
                    return Json("File Not Found", JsonRequestBehavior.AllowGet);
                }

                // get the name of the file
                var fileName = Path.GetFileName(docPath);

                // get the file extension
                var fileExtension = Path.GetExtension(docPath);

                // swith on the content type of the file based on the allowed file types
                var contentType = _serverFileUtility.GetContentType(fileExtension);

                if (_serverFileUtility.FileExsists(docPath))
                {
                    return File(docPath, contentType, fileName);
                }
                else
                {
                    return Json("File Not Found",JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                // if we could not find the file redirect the user to the search page
                return Json("File Not Found", JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
