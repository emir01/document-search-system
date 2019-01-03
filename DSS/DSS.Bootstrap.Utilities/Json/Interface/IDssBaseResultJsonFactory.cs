using System;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Common.Infrastructure.Web.Objects;

namespace DSS.Bootstrap.Utilities.Json.Interface
{
    public interface IDssBaseResultJsonFactory
    {
        /// <summary>
        /// Construct a json model from a base business result, involving no data processing
        /// </summary>
        /// <param name="dataResult"></param>
        /// <returns></returns>
        JsonModel Build(BaseResult dataResult);
    }
}