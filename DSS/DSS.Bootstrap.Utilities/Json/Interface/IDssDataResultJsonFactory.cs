using System;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Common.Infrastructure.Web.Objects;

namespace DSS.Bootstrap.Utilities.Json.Interface
{
    public interface IDssDataResultJsonFactory
    {
        /// <summary>
        /// Build a json model from the Data Result Object for the DSS implementation.
        /// 
        /// Method is generic to allow data results from any type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataResult">The data result object mapping the mapped data of type T</param>
        /// <param name="getViewModelMappings">Optional callback function for mapping the Data Result Raw DAta Object of type T to a DTO/View Model based data object </param>
        /// <returns></returns>
        JsonModel Build<T>(DataResult<T> dataResult, Func<T, object> getViewModelMappings = null);
    }
}