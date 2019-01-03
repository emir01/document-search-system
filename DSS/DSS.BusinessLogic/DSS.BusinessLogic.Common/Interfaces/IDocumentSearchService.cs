using System.Collections.Generic;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Common.ViewModels.Actions;
using DSS.Data.Model.Entities;

namespace DSS.BusinessLogic.Common.Interfaces
{
    /// <summary>
    /// Describe the DSS application main search interface which integrates the following to main subsystems
    /// 
    /// DataQuery functionality - Regular search over document properties
    /// Lucene Query functionality - Full text search document functionality
    /// 
    /// The returned result is a collection of document result view model object
    /// </summary>
    public interface IDocumentSearchService
    {
        /// <summary>
        /// For a given search request for a gien Document Search View Model returns a collection of 
        /// Documents.
        /// 
        /// If the skip parameter is provided it means we are using the same filter properties as the last 
        /// request and only need to return additional documents.
        /// </summary>
        /// <param name="searchFilterModel"></param>
        /// <returns></returns>
        DataResult<IList<Document>> ProcessSearchRequest(DocumentSearchFilterModel searchFilterModel);
    }
}

