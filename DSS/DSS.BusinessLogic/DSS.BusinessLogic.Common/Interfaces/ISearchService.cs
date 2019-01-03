using System.Collections.Generic;
using DSS.Common.ViewModels.Actions;
using DSS.Data.Model.Entities;

namespace DSS.BusinessLogic.Common.Interfaces
{
    /// <summary>
    /// Describes service BL layer functionality regarding document search.
    /// 
    /// The service will rely on low level lucene search services to provide content search as well as common document
    /// services to provide title/author keyword/category search functionality
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Process the search parameters and return documents based on the values for the search criteria.
        /// </summary>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        List<Document> Search(DocumentSearchViewModel searchParams);
    }
}
