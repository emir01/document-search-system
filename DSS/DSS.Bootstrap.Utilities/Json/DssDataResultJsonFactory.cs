using System;
using DSS.Bootstrap.Utilities.Json.Interface;
using DSS.BusinessLogic.Objects.Enums;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Common.Infrastructure.Web.JsonModelConstruction;
using DSS.Common.Infrastructure.Web.Objects;

namespace DSS.Bootstrap.Utilities.Json
{
    public class DssDataResultJsonFactory : IDssDataResultJsonFactory
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
        public JsonModel Build<T>(DataResult<T> dataResult, Func<T, object> getViewModelMappings = null)
        {
            var baseModelConstructor = new BaseJsonModelFactory<DataResult<T>>();

            return baseModelConstructor.Build(
                businessResult: dataResult,

                getStatus: result => result.Status == ResultStatus.Success,

                getMessage: result => result.Message,

                getData: result =>
                         {
                             // if the status is actually successfull
                             if (result.Status == ResultStatus.Success)
                             {
                                 var data = result.GetData();

                                 if (getViewModelMappings != null && (data != null))
                                 {
                                     return getViewModelMappings(result.GetData());
                                 }
                                 else
                                 {
                                     return result.GetData();
                                 }
                             }
                             else
                             {
                                 return null;
                             }
                         },

                getIsException: result => result.Status == ResultStatus.Exception,

                getExceptionMessage: resut => resut.GetException().Message,

                getExceptionObject: result => null
                );
        }
    }
}