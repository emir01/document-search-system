using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Common.Utilities;
using DSS.BusinessLogic.Objects.Enums;
using DSS.Common.Infrastructure.Web.Objects;
using DSS.Common.ViewModels.Dashboards;
using DSS.Common.ViewModels.Documents;
using DSS.Data.Access.Interfaces;
using DSS.Data.Model.Entities;
using DSS.Data.Query.DataTables;
using DSS.Data.Query.Enums;
using DSS.Data.Query.FilterScaffolding;
using DSS.Data.Query.Filters;
using DSS.Data.Query.ParameterProcessors.Interfaces;
using DSS.Data.Query.QueryProcessObjects;
using DSS.Data.Query.QueryProcessors.Interface;
using StackExchange.Profiling;

namespace DSS.Presentation.Web.Controllers
{
    public class DocumentsDashboardController : Controller
    {
        #region Properties

        /// <summary>
        /// The document service used to talk to the basic document model/database.
        /// </summary>
        private readonly IDocumentsService _documentService;

        private readonly IDssIndexService _dssIndexService;

        private readonly IRepository<Document> _documentRepository;

        #region DD Data Query Services

        /// <summary>
        /// The document Data Query Processor
        /// </summary>
        private readonly IQueryProcessor<Document> _documentQueryProcessor;

        /// <summary>
        /// The Document Dashboard Parameters processor
        /// </summary>
        private readonly IQueryParameterProcessor<JQueryDataTableParams, Document> _documentParamsProcessor;

        #endregion

        #endregion

        #region Constructor

        public DocumentsDashboardController(IDocumentsService documentService, IDssIndexService dssIndexService, IRepository<Document> documentRepository, IQueryProcessor<Document> documentQueryProcessor, IQueryParameterProcessor<JQueryDataTableParams, Document> documentParamsProcessor)
        {
            _documentService = documentService;
            _dssIndexService = dssIndexService;
            _documentRepository = documentRepository;
            _documentQueryProcessor = documentQueryProcessor;
            _documentParamsProcessor = documentParamsProcessor;
        }

        #endregion

        //
        // GET: /Lucene/

        public ActionResult Index()
        {
            return View();
        }

        #region Dashboard Actions

        /// <summary>
        /// Returns updated dashboard stats for the documents dashboard screen
        /// </summary>
        /// <returns>Json View Model object containing the document dashboard data</returns>
        public ActionResult UpdateDashboardStats()
        {
            var docDashboardViewModel = new DocumentsDashboardStatsModel();

            // as we are going to be using the index service we
            // should set the correct mapped index service path
            SetServerIndexPathToService();

            var optimzedResult = _dssIndexService.IsIndexOptimized();
            if (optimzedResult.Status == ResultStatus.Success)
            {
                docDashboardViewModel.IsIndexedOptimized = optimzedResult.GetData() ? "Yes" : "No";
            }
            else
            {
                docDashboardViewModel.IsIndexedOptimized = "N/S";
            }

            var indexedDocumentsResult = _dssIndexService.GetNumberOfIndexedDocuments();

            if (optimzedResult.Status == ResultStatus.Success)
            {
                docDashboardViewModel.DocumentsInIndex = indexedDocumentsResult.GetData();
            }
            else
            {
                docDashboardViewModel.DocumentsInIndex = 0;
            }

            // Get this directly from the repo for now
            // Move out to a generic service call that retrieves counts for certain filters on its own entitiy
            // using the repo get queryable function and generic expressions.
            var downloadableDocsCount = _documentRepository.ReadAllAsQueryable().Count(x => x.IsIndexed == true);
            docDashboardViewModel.SearchableDocuments = downloadableDocsCount;

            var allUploadedDocumentsCount = _documentRepository.ReadAllAsQueryable().Count();
            docDashboardViewModel.TotalUploadedDocuments = allUploadedDocumentsCount;

            var jsonResultViewModel = new JsonModel()
                                          {
                                              Status = true,
                                              Data = docDashboardViewModel
                                          };

            return Json(jsonResultViewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Dashboard Filter Actions

        /// <summary>
        /// Construct the View Model for the Document Dashboard filters and return the Partial
        /// View that renders the filter UI elements
        /// </summary>
        /// <returns></returns>
        public PartialViewResult DocumentDashboardFilters()
        {
            // Build the document dashboard filter view model
            var documentDashboardFilterModel = FilterScaffolder.GetFilterScaffoldModel<Document>();

            // Add the document title filter
            documentDashboardFilterModel.AddLeafNodeToBaseLevel(
                FilterLeafNode.Get()
                .SetName("DocumentTitle")
                .SetAnd()
                .SetDataType(FilterDataType.String)
                .SetOperationType(FilterOperationType.Contains)
                .SetLabel("Document Title")
                .SetTarget("Title")
                .SetCaseInsensitive());

            // Add the author name filter
            documentDashboardFilterModel.AddLeafNodeToBaseLevel(
                FilterLeafNode.Get()
                .SetName("AuthorName")
                .SetAnd()
                .SetDataType(FilterDataType.String)
                .SetOperationType(FilterOperationType.Contains)
                .SetTarget("AuthorName")
                .SetLabel("Author Name")
                .SetCaseInsensitive());

            // Add the filter status
            documentDashboardFilterModel.AddLeafNodeToBaseLevel(FilterLeafNode.Get()
                .SetName("DocumentStatus")
                .SetAnd()
                .SetDataType(FilterDataType.SimpleDropdown)
                .SetPrefilterData(GetStatusPreselect(), "Title", "Value")
                .SetServerDropdown()
                .SetDropdownValueType(DropdownValueDataType.Boolean)
                .SetTarget("IsIndexed")
                .SetLabel("Document Index Status")
            );

            return PartialView("DashboardFilters", documentDashboardFilterModel);
        }

        /// <summary>
        /// Create the status dropdown collection
        /// </summary>
        /// <returns></returns>
        private List<object> GetStatusPreselect()
        {
            var documentStatusFilters = new List<object>()
                                            {
                                                new {Title = "Both", Value = -1},
                                                new {Title = "Not Indexed", Value = 0},
                                                new {Title = "Indexed", Value = 1},
                                            };

            return documentStatusFilters;
        }

        #endregion

        #region Lucene Index Actions

        //
        // Ajax Post: /Lucene/AddToIndex/{id}
        [HttpPost]
        public ActionResult AddToIndex(Guid id)
        {
            var jsonViewModel = new JsonModel();

            SetServerIndexPathToService();

            var indexingResult = _dssIndexService.AddDocumentToIndex(id);

            jsonViewModel.Message = indexingResult.Message;

            // if the indexing process has gone through set the data to the id of the document
            if (indexingResult.Status == ResultStatus.Success)
            {
                jsonViewModel.Status = true;
                jsonViewModel.Data = indexingResult.GetData().Id;
            }
            else
            {
                jsonViewModel.Status = false;
                jsonViewModel.Data = null;
            }

            return Json(jsonViewModel);
        }

        // 
        // Post : /DocumentsAdministation/RemoveFromIndex/{Id}
        [HttpPost]
        public ActionResult RemoveFromIndex(Guid id)
        {
            // Have to call this like this to map proper path from the server
            SetServerIndexPathToService();

            var jsonResult = new JsonModel();

            var removeFromIndexResult = _dssIndexService.RemoveDocumentFromIndex(id);

            jsonResult.Message = removeFromIndexResult.Message;

            if (removeFromIndexResult.Status == ResultStatus.Success)
            {
                jsonResult.Status = true;
                jsonResult.Data = removeFromIndexResult.GetData().Id;
            }
            else
            {
                jsonResult.Status = false;
                jsonResult.Data = null;
            }

            return Json(jsonResult);
        }

        #endregion

        #region Grid Actions

        /// <summary>
        /// Processor Action for the Data Tables Client side element
        /// </summary>
        /// <param name="dataTablesParam"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ProcessDataTableRequest(JQueryDataTableParams dataTablesParam, FilterScaffoldModel<Document> filters)
        {
            // Profule the Document Querying functionality using mini profiler
            var profiler = MiniProfiler.Current;

            // Get the query params and process the query for the document queryable
            var queryParams = _documentParamsProcessor.ProcessParameters(dataTablesParam, filters);

            // Get and Profile the Documents Queryable collection
            var documents = _documentRepository.ReadAllAsQueryable();
            QueryResult<Document> queryProcessResult;
            using (profiler.Step("Document Query Processing"))
            {
                queryProcessResult = _documentQueryProcessor.ProcessQuery(documents, queryParams);
            }
            
            // Get and Profile The execution of the queryable collection for documents
            List<Document> queriedDocuments;
            using (profiler.Step("Document Query Execution"))
            {
               queriedDocuments = queryProcessResult.ProcessedData.ToList();
            }

            var mappedDocuments = queriedDocuments.Select(Mapper.Map<AdminDocumentGridViewModel>).ToList();

            JQueryDataTableResult jQueryResult = new JQueryDataTableResult()
                                                     {
                                                         aaData = mappedDocuments,
                                                         iTotalDisplayRecords = queryProcessResult.FilteredCount,
                                                         iTotalRecords = queryProcessResult.TotalCount,
                                                         sEcho = dataTablesParam.sEcho
                                                     };

            return Json(jQueryResult, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Utility

        private void SetServerIndexPathToService()
        {
            // setup the index service with the propper lucene index path based on the web server.
            var luceneIndexFolderName = ConfigurationManager.AppSettings[ConfigurationSettingKeys.LuceneIndexFolderName];

            // map the folder name to the server path
            var serverPath = HttpContext.Server.MapPath(luceneIndexFolderName);

            _dssIndexService.SetIndexLocation(serverPath);
        }

        #endregion
    }
}
