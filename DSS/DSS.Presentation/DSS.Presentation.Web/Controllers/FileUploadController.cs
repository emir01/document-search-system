using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using DSS.Bootstrap.Utilities;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.Enums;
using DSS.Common.ViewModels.Documents;
using DSS.Data.Model.Entities;
using DSS.Presentation.Web.CustomBinders;

namespace DSS.Presentation.Web.Controllers
{
    /// <summary>
    /// Controller handling file uploads.
    /// </summary>
    [Authorize]
    public class FileUploadController : Controller
    {
        #region Properties

        /// <summary>
        /// Reference to the web file upload utility used to process uploaded files
        /// </summary>
        private readonly ServerFileUtility _serverFileUtility;

        private readonly IDocumentsService _documentService;

        /// <summary>
        /// The key used to access temp data information in the file upload process.
        /// </summary>
        private const string TempUploadFilesResut = "Temp_FileUploadResultData";

        #endregion

        public FileUploadController(IDocumentsService documentService, ServerFileUtility serverFileUtility)
        {
            _documentService = documentService;
            _serverFileUtility = serverFileUtility;
        }

        // The main upload screen
        // GET: /Files/
        public ActionResult Index()
        {
            return View();
        }

        #region Action

        // The post method for the main upload screen. The actual file upload handler
        // POST : /Files/
        [HttpPost]
        public ActionResult Index(IEnumerable<HttpPostedFileBase> files, [ModelBinder(typeof(TagEditPostedFormBinder))]IEnumerable<AddDocumentViewModel> documents)
        {
            #region Validation Checks

            // if any of the uploaded documents are missing vital information
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Uploaded documents missing vital information. Try again");
                // return the file upload view
                return View();
            }

            if (files == null || documents == null || files.Count() != documents.Count())
            {
                ModelState.AddModelError("", "Upload Failed. Try Again.");
                return View();
            }

            #endregion

            // Process the files using the file upload utility
            var processedFiles = _serverFileUtility.ProcessFiles(files.ToList(), documents);

            // Transform the successfully saved files to apropriate domain objects
            var savedDocuments = processedFiles.SavedFiles.Select(Mapper.Map<AddDocumentViewModel, Document>).ToList();

            var uniqueKeywords = Mapper.Map<List<AddDocumentViewModel>, List<Keyword>>(processedFiles.SavedFiles);

            var uniqueCategories = Mapper.Map<List<AddDocumentViewModel>, List<Category>>(processedFiles.SavedFiles);

            // Create the vi]ew model for the file upload results page.
            var fileUploadResultsViewModel = new DocumentsUploadResultsViewModel();

            // Set the results for the failed processed files
            // - set the names of the failed add document view models
            fileUploadResultsViewModel.OrphanedDocumentObjectTitles = processedFiles.OrphanViewModels.Select(x => x.Title).ToList();

            // - set the names of the failed to store uploaded files
            fileUploadResultsViewModel.OrphanedUploadedFileNames = processedFiles.OrphanUploadedFiles.Select(x => x.FileName).ToList();

            // If there is at least one saved uploaded document
            // Try and save entities to the database for the uploaded document
            if (savedDocuments.Count > 0)
            {
                // set aditional properties for the saved documents
                foreach (var savedDocument in savedDocuments)
                {
                    savedDocument.DateUploaded = DateTime.Now;
                    savedDocument.IsIndexed = false;
                }

                // call the document service save documents functionn to actually
                // store the documents in the database
                // The files are already uploaded and the paths have been set by the file upload utlity
                var result = _documentService.StoreDocuments(savedDocuments.ToList(), User.Identity.Name, uniqueCategories, uniqueKeywords);

                // If the save was a success
                if (result.Status == ResultStatus.Success)
                {
                    // everything went ok with saving the documents
                    fileUploadResultsViewModel.StoredDocumentsCount = savedDocuments.Count;
                    fileUploadResultsViewModel.StoredDocuments = savedDocuments.Select(x => new DocumentsUploadResultsViewModel.StoredDocument()
                    {
                        DocumentFilePath = x.Path,
                        DocumentSavedTitle = x.Title
                    }).ToList();
                }
                else
                {
                    // If something went wrong saving then set the saved document count to 0
                    fileUploadResultsViewModel.StoredDocumentsCount = 0;

                    // remove the uploaded documents from the server.
                    var documentPaths = savedDocuments.Select(x => x.Path).ToList();
                    _serverFileUtility.RemoveDocuments(documentPaths);
                }
            }

            // Combine the result from the document service with the result from the file upload
            // utility to display a propper file upload status screen.
            TempData[TempUploadFilesResut] = fileUploadResultsViewModel;

            return RedirectToAction("UploadResults");
        }

        // Used to display the results of the upload process
        // GET : /Files/UploadResults
        public ActionResult UploadResults()
        {
            // Check if a temp data object has been set
            var documentUploadResults = (DocumentsUploadResultsViewModel)TempData[TempUploadFilesResut];

            // If there is no set data for the upload result
            // redirect to the upload view
            if (documentUploadResults == null)
            {
                RedirectToAction("Index");
            }

            return View(documentUploadResults);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Return fake file upload resuts object for testing purposes for the results view
        /// </summary>
        /// <returns></returns>
        public DocumentsUploadResultsViewModel GetFakeFileUploadResuts()
        {
            var resultsViewModel = new DocumentsUploadResultsViewModel();

            resultsViewModel.StoredDocumentsCount = 4;

            for (var i = 0; i < resultsViewModel.StoredDocumentsCount; i++)
            {
                resultsViewModel.StoredDocuments.Add(new DocumentsUploadResultsViewModel.StoredDocument()
                {
                    DocumentFilePath = "/SomePath/SomeOtherPath" + i,
                    DocumentSavedTitle = "Some Document Title " + i
                });
            }

            // The number of document information objects that do not have
            // coresponding actual uploaded files
            const int orphanedDocumentViewModels = 2;

            // The number of actually uploaded document files that do not have 
            // coresponding document information objects
            const int orpanedPhysicalFiles = 1;

            for (var i = 0; i < orphanedDocumentViewModels; i++)
            {
                resultsViewModel.OrphanedDocumentObjectTitles.Add("Some Document Title " + i);
            }

            for (var i = 0; i < orpanedPhysicalFiles; i++)
            {
                resultsViewModel.OrphanedUploadedFileNames.Add("SomeFileName" + i+".doc");
            }

            return resultsViewModel;
        }

        #endregion
    }
}
