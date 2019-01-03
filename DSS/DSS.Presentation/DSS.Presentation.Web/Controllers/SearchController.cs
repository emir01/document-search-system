using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using DSS.Bootstrap.Utilities.Json.Interface;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.Common.ViewModels.Actions;
using DSS.Common.ViewModels.Documents;

namespace DSS.Presentation.Web.Controllers
{
    /// <summary>
    /// Controller handling document searching, which in turn turns out top be only displaying the document search view currently
    /// </summary>
    public class SearchController : Controller
    {
        #region Properties

        private readonly IDocumentsService _documentService;

        private readonly IDocumentSearchService _documentSearchService;

        private readonly IDssDataResultJsonFactory _jsonFactory;

        #endregion

        #region Constructor

        public SearchController(IDocumentsService documentService, IDocumentSearchService documentSearchService, IDssDataResultJsonFactory jsonFactory)
        {
            _documentService = documentService;
            _documentSearchService = documentSearchService;
            _jsonFactory = jsonFactory;
        }

        #endregion

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View();
        }

        #region Search Actions

        /// <summary>
        /// The main Search action for the document search functionality. As paramaters it takes the main criteria used during
        /// the client search
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(DocumentSearchFilterModel searchFilterModel)
        {
            var results = _documentSearchService.ProcessSearchRequest(searchFilterModel);

            return Json(_jsonFactory.Build(results, data => data.Select(Mapper.Map<DisplayDocumentViewModel>)));
        }

        #endregion
    }
}
