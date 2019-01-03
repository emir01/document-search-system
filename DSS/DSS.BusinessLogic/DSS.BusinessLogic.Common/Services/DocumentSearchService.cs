using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DSS.BusinessLogic.Common.Interfaces;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Common.ViewModels.Actions;
using DSS.Data.Access.Interfaces;
using DSS.Data.Model.Entities;
using StackExchange.Profiling;

namespace DSS.BusinessLogic.Common.Services
{
    /// <summary>
    /// Describe the DSS application main search interface which integrates the following to main subsystems
    /// 
    /// DataQuery functionality - Regular search over document properties
    /// Lucene Query functionality - Full text search document functionality
    /// 
    /// The returned result is a collection of document result view model object
    /// </summary>
    public class DocumentSearchService : IDocumentSearchService
    {

        #region Properties

        private readonly IRepository<Document> _documentRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public DocumentSearchService(IRepository<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        #endregion

        #region Interface Impl

        /// <summary>
        /// For a given search request for a gien Document Search View Model returns a collection of 
        /// Documents.
        /// 
        /// If the skip parameter is provided it means we are using the same filter properties as the last 
        /// request and only need to return additional documents.
        /// </summary>
        /// <param name="searchFilterModel"></param>
        /// <returns></returns>
        public DataResult<IList<Document>> ProcessSearchRequest(DocumentSearchFilterModel searchFilterModel)
        {
            var result = new DataResult<IList<Document>>();
            try
            {
                var profiler = MiniProfiler.Current;

                IQueryable<Document> documentQuery;

                using (profiler.Step("Search Documents: Getting base query"))
                {
                    documentQuery = _documentRepository.ReadAllAsQueryable();
                }

                using (profiler.Step("Search Documents: Filtering"))
                {
                    // do filtering
                    documentQuery = DoFiltering(searchFilterModel, documentQuery);
                }

                using (profiler.Step("Search Documents: Ordering"))
                {
                    // ordering
                    documentQuery = documentQuery.OrderBy(ent => ent.Title);
                }

                using (profiler.Step("Search Documents: Paging"))
                {
                    // skip and take
                    documentQuery = documentQuery.Skip(searchFilterModel.Skip).Take(searchFilterModel.Take);
                }

                List<Document> documentList;

                using (profiler.Step("Search Documents: To List"))
                {
                    // Set the result as success and set the data
                    documentList = documentQuery.ToList();
                }

                result.SetData(documentList);
                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetException(ex, "Error when searching Documents");
            }

            return result;
        }

        #endregion

        #region Private Utilities

        /// <summary>
        /// Add the filtering LINQ calls over the document queryable
        /// </summary>
        /// <param name="searchFilterModel"></param>
        /// <param name="documentQuery"></param>
        /// <returns></returns>
        private IQueryable<Document> DoFiltering(DocumentSearchFilterModel searchFilterModel, IQueryable<Document> documentQuery)
        {
            documentQuery = from document in documentQuery

                            // search author name
                            where
                                searchFilterModel.Author == null ||
                                document.AuthorName.ToLower().Contains(searchFilterModel.Author.ToLower())

                            // search the title
                            where
                                searchFilterModel.Title == null ||
                                document.Title.ToLower().Contains(searchFilterModel.Title.ToLower())

                            // filter out the categories
                            //where searchFilterModel.Categories == null ||
                            //searchFilterModel.Categories.All(cat=>document.Categories.Any(c=>c.Name == cat))

                            select document;


            //execute the query and filter categories and keywords in memory
            var resultDocuments = documentQuery.ToList();

            // filter categories if there are any selected from the filter
            if (searchFilterModel.Categories != null && searchFilterModel.Categories.Any())
            {
                resultDocuments = resultDocuments.Where(doc => 
                    searchFilterModel.Categories.All(filterCategory=> doc.Categories.Any(docCategory=>docCategory.Name == filterCategory ))).ToList();
            }

            if (searchFilterModel.Keywords != null && searchFilterModel.Keywords.Any())
            {
                resultDocuments = resultDocuments.Where(doc => searchFilterModel.Keywords.All(filterKeyword => doc.Keywords.Any(docKeyword => docKeyword.Name == filterKeyword))).ToList();
            }
            
            return resultDocuments.AsQueryable();
        }

        #endregion
    }
}
