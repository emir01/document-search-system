using System.Collections.Generic;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Data.Model.Entities;

namespace DSS.BusinessLogic.Common.Interfaces
{
    /// <summary>
    /// Basic interface describing the functionality for the document
    /// keyword service.
    /// </summary>
    public interface IKeywordService
    {
        DataResult<IList<Keyword>> GetAllKeywords();
    }
}
