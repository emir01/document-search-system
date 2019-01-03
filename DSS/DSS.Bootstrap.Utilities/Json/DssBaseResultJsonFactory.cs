using System;
using DSS.Bootstrap.Utilities.Json.Interface;
using DSS.BusinessLogic.Objects.Enums;
using DSS.BusinessLogic.Objects.OperationResults;
using DSS.Common.Infrastructure.Web.JsonModelConstruction;
using DSS.Common.Infrastructure.Web.Objects;

namespace DSS.Bootstrap.Utilities.Json
{
    /// <summary>
    /// Dss specific implementation of the Base Json Model Factory. All Function delegates except message are ignored in the construction 
    /// of the Json Model as we use specific DSS domain functionality to process the Base and Data Results.
    /// </summary>
    public class DssBaseResultJsonFactory : IDssBaseResultJsonFactory
    {
        #region Interface Implementation

        /// <summary>
        /// Construct a json model from a base business result, involving no data processing
        /// </summary>
        /// <param name="dataResult"></param>
        /// <returns></returns>
        public JsonModel Build(BaseResult dataResult)
        {
            var baseModelConstructor = new BaseJsonModelFactory<BaseResult>();
 
            // build the json model from the data result
            return baseModelConstructor.Build(
             businessResult: dataResult,

             getStatus: result => result.Status == ResultStatus.Success,

             getMessage: result => result.Message,

             getData: result => null,
             
             getIsException: result => result.Status == ResultStatus.Exception,

             getExceptionMessage: resut => resut.GetException().Message,

             getExceptionObject: result => null
             );
        }

        #endregion
    }
}
