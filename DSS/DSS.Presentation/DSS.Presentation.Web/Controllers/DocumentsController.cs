using System;
using System.Web.Mvc;
using AutoMapper;
using DSS.Bootstrap.Utilities.Interface;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.Enums;
using DSS.Common.ViewModels.Documents;

namespace DSS.Presentation.Web.Controllers
{
    public class DocumentsController : Controller
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

        public DocumentsController(IDocumentsService documentsService, IServerFileUtility serverFileUtility)
        {
            _documentsService = documentsService;
            _serverFileUtility = serverFileUtility;
        }

        #endregion

        #region Download Actions


        #endregion

        #region Main Document Details  View rendering Action

        /// <summary>
        /// The document details view that calls upon the Details partial view configured with showing every document action
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ViewResult Details(string id)
        {
            var renderingOptionsModel = new DocumentDetailsRenderOptions()
                                            {
                                                DocumentId = id,
                                                RenderDownload = true,
                                                RenderStats = true,
                                                RenderVoting = true
                                            };

            return View(renderingOptionsModel);
        }

        #endregion

        #region Process Actions

        /// <summary>
        /// Process an upvote on the document with the given document id
        /// for the current user
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProcessUpvote(string documentId)
        {
            return Json("Processed Upvote");
        }

        /// <summary>
        /// Process a downvote
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProcessDownvote(string documentId)
        {
            return Json("Process downvote");
        }

        #endregion

        #region Document Details Partial

        /// <summary>
        /// Render the document partial view that is configurable using the rendering options
        /// </summary>
        /// <param name="id"></param>
        /// <param name="renderingOptions"></param>
        /// <returns></returns>
        public PartialViewResult DetailsPartialView(string id, DocumentDetailsRenderOptions renderingOptions = null)
        {
            // try and parse the document id
            Guid documentId;
            Guid.TryParse(id, out documentId);

            if (documentId != Guid.Empty)
            {
                var documentRetrievalResult = _documentsService.GetDocumentById(documentId);

                if (documentRetrievalResult.Status == ResultStatus.Success)
                {
                    var viewModel = DocumentDetailsRenderViewModel.GetSuccessRenderer("Retrieved Document Success");

                    viewModel.DisplayDocumentViewModel = Mapper.Map<DisplayDocumentViewModel>(documentRetrievalResult.GetData());
                    
                    // the rendering options value is provided
                    if (renderingOptions != null)
                    {
                        viewModel.Options = renderingOptions;

                        // if the rendering option is provided and the render stats option is set to true
                        // we are going to be mapping stats from the document

                        if (renderingOptions.RenderStats)
                        {
                            // map the stat view model from the document
                            viewModel.DocumentStatsViewModel =
                                Mapper.Map<DocumentStatsViewModel>(documentRetrievalResult.GetData());
                        }
                    }

                    return PartialView(viewModel);
                }
                else
                {
                    return PartialView(DocumentDetailsRenderViewModel.GetFailedRenderer("Failed to retrieve document for the given Id. Please try again."));
                }
            }
            else
            {
                return PartialView(DocumentDetailsRenderViewModel.GetFailedRenderer("Could not parse Document Id. Please try again or return to the Search Page to View another document."));
            }
        }

        #endregion
    }
}