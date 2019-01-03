using System.Collections.Generic;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Data.Model.Entities;

namespace DSS.BusinessLogic.Common.Interfaces
{
    /// <summary>
    /// The basic interface describing basic functionality related to the Document category entities.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Returns a list of all the categories wrapped in a data result object.
        /// </summary>
        /// <returns><see cref="DataResult{T}"/> object wrapping the list of categories</returns>
        DataResult<IList<Category>> GetAllCategories();
    }
}
