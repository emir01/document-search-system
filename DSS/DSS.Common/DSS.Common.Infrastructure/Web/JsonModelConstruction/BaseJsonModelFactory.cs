using System;
using DSS.Common.Infrastructure.Web.JsonModelConstruction.Interfaces;
using DSS.Common.Infrastructure.Web.Objects;

namespace DSS.Common.Infrastructure.Web.JsonModelConstruction
{
    // TODO: Figure out structure map injections
    public class BaseJsonModelFactory<T> : IBaseJsonModelFactory<T>
    {
        /// <summary>
        /// Defines the most generic method of building Json Model object by setting all the
        /// JSON Model properties individually. 
        /// 
        /// The properties are set using callback functions from client code.
        /// 
        /// Some of the callback functions are optional as they are used to set non critical Json Model Properties
        /// </summary>
        /// <param name="businessResult">The business result object containing all the information including data to build the Json Model object</param>
        /// <param name="getStatus">Should return the True/False status of the business result object</param>
        /// <param name="getMessage">Should return the base message from the Json Result object </param>
        /// <param name="getData">Should return the data to be serialized and passed to the client in Json Format</param>
        /// <param name="getIsException">Should return if the business resulkt returned with an exception result</param>
        /// <param name="getExceptionMessage">Should return the base exception message</param>
        /// <param name="getExceptionObject">Should return the possible optional exception object</param>
        /// <returns></returns>
        public JsonModel Build(T businessResult, Func<T, bool> getStatus, Func<T, string> getMessage = null, Func<T, object> getData = null,
            Func<T, bool> getIsException = null, Func<T, string> getExceptionMessage = null, Func<T, object> getExceptionObject = null)
        {
            var jsonModel = new JsonModel();

            // get the status of the business result by invoking the funct callback
            jsonModel.Status = getStatus(businessResult);

            // ## Go over the other provided callbacks. If they are valid and not null we are 
            // going to invoke them to get the business result appropriate properties

            if (getMessage != null)
            {
                jsonModel.Message = getMessage(businessResult);
            }

            if (getData != null)
            {
                jsonModel.Data = getData(businessResult);
            }

            if (getIsException != null)
            {
                jsonModel.IsException = getIsException(businessResult);
            }
            else
            {
                jsonModel.IsException = false;
            }

            if (getExceptionMessage != null && jsonModel.IsException)
            {
                jsonModel.ExceptionMessage = getExceptionMessage(businessResult);
            }

            if (getExceptionObject != null && jsonModel.IsException)
            {
                jsonModel.ExceptionObject = getExceptionObject(businessResult);
            }

            return jsonModel;
        }
    }
}